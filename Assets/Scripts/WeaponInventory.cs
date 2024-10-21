using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInventory : MonoBehaviour
{
    [SerializeField] private Image weaponInventory;

    [SerializeField] private GameObject[] weapons;

    private void Update()
    {
        if(Input.GetKeyDown("q"))
        {
            weaponInventory.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void SelectWeapon(int index)
    {
        for(int i=0; i<weapons.Length; i++)
        {
            if(i == index)
            {
                weapons[i].SetActive(true);
            }
            else weapons[i].SetActive(false);
        }

        weaponInventory.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
