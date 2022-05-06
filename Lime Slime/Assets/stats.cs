using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stats : MonoBehaviour
{
    // ChatInputField.


    public GameObject statsOpen;
    public GameObject statsclose;
    public GameObject showStats;


    public void openStas()
    {
        statsOpen.gameObject.SetActive(false);


        showStats.gameObject.SetActive(true);        
        statsclose.gameObject.SetActive(true);      
    }

    public void closeStats()
    {
        statsOpen.gameObject.SetActive(true);


        showStats.gameObject.SetActive(false);
        statsclose.gameObject.SetActive(false);
    }

}
