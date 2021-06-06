using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed;
    public int score;
    
    public WeaponBehaviour mainWeapon;
    public WeaponBehaviour secondaryWeapon;

    private void Awake()
    {
        References.thePlayer = this;
    }

    //// Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // WASD to move
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Rigidbody ourRigidBody = GetComponent<Rigidbody>();
        ourRigidBody.velocity = inputVector * speed;

        // Map cursor to playing plane
        Ray rayFromCameraToCursor = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        playerPlane.Raycast(rayFromCameraToCursor, out float distanceFromCamera);
        Vector3 cursorPosition = rayFromCameraToCursor.GetPoint(distanceFromCamera);

        // Look at target position before moving
        Vector3 lookAtPosition = cursorPosition;
        transform.LookAt(lookAtPosition);

        // Click to fire
        if (mainWeapon != null && Input.GetButton("Fire1"))
        {
            mainWeapon.Fire(cursorPosition);
        }

        // Change weapon
        if (Input.GetButtonDown("Fire2"))
        {
            SwitchWeapon();
        }

        // Use the nearest usable
        if (Input.GetButtonDown("Use"))
        {
            Useable nearestUseable = null;
            float nearestDistance = 3; // Max pickup distance

            foreach (var thisUseable in References.useables)
            {
                float thisDistance = Vector3.Distance(transform.position, thisUseable.transform.position);
                if (thisDistance <= nearestDistance)
                {
                    nearestUseable = thisUseable;
                    nearestDistance = thisDistance;
                }
            }

            if (nearestUseable != null)
            {
                nearestUseable.Use();
            }
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        References.theCanvas.scoreText.text = score.ToString();
    }

    public void PickUpWeapon(WeaponBehaviour weapon)
    {
        if (mainWeapon == null)
        {
            // use new weapon if we don't have any weapon
            SetAsMainWeapon(weapon);
        }
        else if (secondaryWeapon == null)
        {
            // put into the secondary slot if it's empty
            SetAsSecondaryWeapon(weapon);
        }
        else
        {
            // full inventory
            // drop current main weapon and equip new weapon as main
            mainWeapon.Drop();
            SetAsMainWeapon(weapon);
        }
    }

    void SetAsMainWeapon(WeaponBehaviour weapon)
    {
        mainWeapon = weapon;
        References.theCanvas.mainWeaponPanel.AssignWeapon(weapon);
        weapon.gameObject.SetActive(true);
    }

    void SetAsSecondaryWeapon(WeaponBehaviour weapon)
    {
        secondaryWeapon = weapon;
        References.theCanvas.secondaryWeaponPanel.AssignWeapon(weapon);
        weapon.gameObject.SetActive(false);
    }

    private void SwitchWeapon()
    {
        if (mainWeapon != null && secondaryWeapon != null)
        {
            WeaponBehaviour oldMainWeapon = mainWeapon;
            SetAsMainWeapon(secondaryWeapon);
            SetAsSecondaryWeapon(oldMainWeapon);
        }
    }
}
