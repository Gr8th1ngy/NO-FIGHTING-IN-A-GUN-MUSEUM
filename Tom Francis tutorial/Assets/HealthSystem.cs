using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthSystem : MonoBehaviour
{
    [FormerlySerializedAs("health")] // Tells Unity not to lose data when renaming variable
    public float maxHealth;
    public GameObject healthbarPrefab;
    public GameObject deathEffectPrefab;

    HealthBarBehaviour myHealthBar;
    float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        GameObject healthBarObject = Instantiate(healthbarPrefab, References.theCanvas.transform);
        myHealthBar = healthBarObject.GetComponent<HealthBarBehaviour>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Make health bar reflect health
        myHealthBar.ShowHealthFraction(currentHealth / maxHealth);

        // Health bar move to current position
        myHealthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2);
    }

    private void OnDestroy()
    {
        if (myHealthBar)
        {
            Destroy(myHealthBar.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                if (deathEffectPrefab != null)
                {
                    Instantiate(deathEffectPrefab, transform.position, transform.rotation);
                }

                Destroy(gameObject);
            }
        }
    }

    public void KillMe()
    {
        TakeDamage(currentHealth);
    }
}
