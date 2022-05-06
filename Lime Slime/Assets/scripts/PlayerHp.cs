using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public Transform spawnPoint;
    
    public Text max_HP; 
    public Text current_HP;
   // hello.text = " hello 3";


    public Slider slider;
    public int max_Health = 100;
    public int current_Health;

    


    void Start()
    {
        current_Health = max_Health;
        slider.minValue = 0;
        slider.maxValue = current_Health; //set limit to assigned value sa max health

        slider.value = current_Health; //para ful hp sa inot
    }
    void Update()
    {
        slider.value = current_Health; //para si current health pirmi ma update

        current_HP.text = current_Health.ToString(); 
        max_HP.text = max_Health.ToString();

    }

    public void TakeDamage(int damage)
    {
        current_Health -= damage;
        Debug.Log(current_Health);
        Debug.Log(current_Health);
        if (current_Health <= 0)
        {
            current_Health = 0;   
            Die();
        }
        




    }
    void Die()
    {
        //   Debug.Log("Player died");
        transform.parent.position = spawnPoint.transform.position;
        current_Health = max_Health;
    }

}
/*
 * 
 * 
    public Slider slider; // add using UnityEngine.UI;
    public float healthValue = 0;

    void Start()
    {
        slider.minValue = 0;
        slider.maxValue = 100;

        slider.value = 100;
    }
    void Update()
    {
        slider.value = healthValue;


    }
 * 
 * 
 * 
 
 */