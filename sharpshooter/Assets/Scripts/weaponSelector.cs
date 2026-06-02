using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class weaponSelector : MonoBehaviour
{

    [SerializeField] private List<GameObject> weapons = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private static GameObject selectedWeapon;
    
    public static GameObject GetWeapon()
    {
        if (selectedWeapon == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Pistol");
            return prefab;
        }

        return selectedWeapon;
    }

    public void setWeapon(int index)
    {
        selectedWeapon =  weapons[index];
        SceneManager.LoadScene("game");
    }
}
