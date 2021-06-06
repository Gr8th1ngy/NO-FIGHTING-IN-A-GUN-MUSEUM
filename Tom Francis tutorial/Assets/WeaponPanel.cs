using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponPanel : MonoBehaviour
{
    public TextMeshProUGUI weaponName;
    public TextMeshProUGUI ammoCount;

    public WeaponBehaviour myWeapon;

    public void AssignWeapon(WeaponBehaviour newWeapon)
    {
        myWeapon = newWeapon;
        weaponName.text = newWeapon.GetComponent<Useable>().displayName;
    }

    // Update is called once per frame
    void Update()
    {
        if (myWeapon != null)
        {
            ammoCount.text = myWeapon.ammo.ToString();
        }
    }
}
