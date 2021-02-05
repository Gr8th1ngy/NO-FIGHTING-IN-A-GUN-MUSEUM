using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed;

    public List<WeaponBehaviour> weapons = new List<WeaponBehaviour>();
    int selectedWeaponIndex;

    //// Start is called before the first frame update
    void Start()
    {
        References.thePlayer = this;
        selectedWeaponIndex = 0;
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
        if (weapons.Count > 0 && Input.GetButton("Fire1"))
        {
            weapons[selectedWeaponIndex].Fire(cursorPosition);
        }

        // Change weapon
        if (Input.GetButtonDown("Fire2"))
        {
            ChangeWeaponIndex(selectedWeaponIndex + 1);
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

    public void SelectLatestWeapon()
    {
        ChangeWeaponIndex(weapons.Count - 1);
    }

    private void ChangeWeaponIndex(int index)
    {
        selectedWeaponIndex = index;
        if (selectedWeaponIndex >= weapons.Count)
        {
            selectedWeaponIndex = 0;
        }

        // Change weapon look
        for (int i = 0; i < weapons.Count; i++)
        {
            if (i == selectedWeaponIndex)
            {
                weapons[i].gameObject.SetActive(true);
            }
            else
            {
                weapons[i].gameObject.SetActive(false);
            }
        }
    }
}
