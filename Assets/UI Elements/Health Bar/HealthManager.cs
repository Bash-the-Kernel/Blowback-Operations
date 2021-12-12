using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Text healthText;
    public Image headHealth;
    public Image torsoHealth;
    public Image leftArmHealth;
    public Image rightArmHealth;
    public Image leftLegHealth;
    public Image rightLegHealth;

    
    float health , maxHealth = 100;

    float headMaxHealth = 70;
    float torsoMaxHealth = 100;
    float left_ArmMaxHealth , right_ArmMaxHealth = 60;
    float left_legMaxHealth , right_legMaxHealth = 80;

    List<KeyValuePair<string, int>> bodyParts = new List<KeyValuePair<string, int>>();

    float changeSpeed;
    
    private void Start()
    {

        health = maxHealth;
        defineThings();


    }

    private void Update()
    {

        healthText.text = "Health: " + health + "%";
        if ( health > maxHealth ) health = maxHealth;

        changeSpeed = 5 * Time.deltaTime;

        HealthBarFiller();
        colourChanger();
        

    }
    

    void defineThings()
    {
        bodyParts.Add(new KeyValuePair< string , int >("head" , 70 ));
        bodyParts.Add(new KeyValuePair< string , int >("torsoHealth" , 100 ));
        bodyParts.Add(new KeyValuePair< string , int >("left_ArmHealth" , 60 ));
        bodyParts.Add(new KeyValuePair< string , int >("right_ArmHealth" , 60 ));
        bodyParts.Add(new KeyValuePair< string , int >("left_legHealth" , 80 ));
        bodyParts.Add(new KeyValuePair< string , int >("right_legHealth" , 80 ));

    }

    void HealthBarFiller()
    {
        Debug.Log( bodyParts[1] );
        headHealth.fillAmount = Mathf.Lerp( headHealth.fillAmount , health / maxHealth , changeSpeed );
        
    }

    public void decreaseHealth( string inputs )
    {

        Debug.Log( bodyParts[1] );
        string temp = inputs.Substring( 0 , 3 );
        int damage = int.Parse( temp );
        int ind = inputs.LastIndexOf( ':' );
        temp = inputs.Substring(ind + 1);
        string bodyPart = temp;

        var random = new System.Random();
        int randVal = random.Next(bodyParts.Count);
        Debug.Log( bodyParts[randVal] );


    }
    
    public void increaseHealth( float increasedHealth )
    {

        if ( health < maxHealth )
            health += increasedHealth; 

    }

    void colourChanger()
    {

        Color healthColour = Color.Lerp( Color.red , Color.green , ( health / maxHealth ));
        headHealth.color = healthColour;

    }
}