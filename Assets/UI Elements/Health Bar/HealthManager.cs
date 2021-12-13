using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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

    string bodyPart = "headHealth";

    float headMaxHealth = 70;
    float torsoMaxHealth = 100;
    float left_ArmMaxHealth , right_ArmMaxHealth = 60;
    float left_LegMaxHealth , right_LegMaxHealth = 80;

    Dictionary<string, int> bodyParts = new Dictionary<string, int>(); 
    //ICollection<KeyValuePair<string, int>> bodyParts = new Dictionary<string, int>();
    

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
        bodyParts.Add("headHealth" , 70 );
        bodyParts.Add("torsoHealth" , 100 );
        bodyParts.Add("left_ArmHealth" , 60 );
        bodyParts.Add("right_ArmHealth" , 60 );
        bodyParts.Add("left_LegHealth" , 80 );
        bodyParts.Add("right_LegHealth" , 80 );
    }

    void HealthBarFiller()
    {
        headHealth.fillAmount = Mathf.Lerp( headHealth.fillAmount , health / maxHealth , changeSpeed );
        
    }


    static string determinePart(string value)
    {
        switch (value)
        {
            case "head":
                return "chuck";
            case "torso":
                return "suck and fuck";
            case "left_armHealth":
            case "right_armHealth":
                return "sneed";
            case "left_legHealth":
            case "right_legHealth":
                return "seed and feed";
            default:
                return "city slicker";
        }
    }

    public void decreaseHealth( string inputs )
    {
  
        //Debug.Log( bodyParts[1] );
        string temp = inputs.Substring( 0 , 3 );
        int damage = int.Parse( temp );
        int ind = inputs.LastIndexOf( ':' );
        temp = inputs.Substring(ind + 1);
        string bodyPart = temp;

        Debug.Log(determinePart(bodyPart));
        
        Debug.Log(bodyParts[bodyPart]);
        if ( bodyParts[bodyPart] > 0 )
            bodyParts[bodyPart] -= damage;


    }
    
    public void increaseHealth( float increasedHealth )
    {

        if ( health < maxHealth )
            health += increasedHealth; 

    }

    void colourChanger()
    {

        Color healthColour = Color.Lerp( Color.red , Color.green , ( bodyParts[bodyPart] / headMaxHealth ));

        headHealth.color = healthColour;

    }
}