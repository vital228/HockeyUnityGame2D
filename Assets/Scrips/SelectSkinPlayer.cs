using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSkinPlayer: MonoBehaviour
{
    public GameObject[] skins;

    private void Start()
    {
        skins = new GameObject[transform.childCount];

        for (int i=0; i< transform.childCount; i++)
        {
            skins[i] = transform.GetChild(i).gameObject;
        }
        foreach (GameObject go in skins)
        {
            go.SetActive(false);
        }
    }
    public void SetSkin(int index)
    {
        skins[index].SetActive(true);
    }
}
