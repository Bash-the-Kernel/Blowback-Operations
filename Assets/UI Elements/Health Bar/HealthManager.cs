using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class HealthManager : MonoBehaviour
{
    // definitions
    public Text healthText;
    public Text headHealthText;
    public Text torsoHealthText;
    public Text leftArmHealthText;
    public Text rightArmHealthText;
    public Text leftLegHealthText;
    public Text rightLegHealthText;
    
    public Image headHealth;
    public Image torsoHealth;
    public Image leftArmHealth;
    public Image rightArmHealth;
    public Image leftLegHealth;
    public Image rightLegHealth;

    public Color healthColourHead = Color.green;
    public Color healthColourTorso = Color.green;
    public Color healthColourLeftArm = Color.green;
    public Color healthColourRightArm = Color.green;
    public Color healthColourLeftLeg = Color.green;
    public Color healthColourRightLeg = Color.green;

    float health , maxHealth = 100;

    string bodyPart = "headHealth";

    float headMaxHealth = 70;
    float torsoMaxHealth = 100;
    float armMaxHealth = 60;
    float legMaxHealth = 80;

    Dictionary<string, int> bodyParts = new Dictionary<string, int>(); 
    
    float changeSpeed;
    

    private void Start()
    {

        health = maxHealth;
        defineThings();


    }

    private void Update()
    {
        // establish health percentage texts
        healthText.text = "Health: " + health + "%";
        headHealthText.text = "Head Health: " + (( (float)bodyParts["headHealth"] / headMaxHealth ) * 100 ) + "%";
        torsoHealthText.text = "Torso Health: " + (( (float)bodyParts["torsoHealth"] / torsoMaxHealth ) * 100 ) + "%";
        leftArmHealthText.text = "Left Arm Health: " + (( (float)bodyParts["leftArmHealth"] / armMaxHealth ) * 100 ) + "%";
        rightArmHealthText.text = "Right Arm Health: " + (( (float)bodyParts["rightArmHealth"] / armMaxHealth ) * 100 ) + "%";
        leftLegHealthText.text = "Left Leg Health: " + (( (float)bodyParts["leftLegHealth"] / legMaxHealth ) * 100 ) + "%";
        rightLegHealthText.text = "Right LegHealth: " + (( (float)bodyParts["rightLegHealth"] / legMaxHealth ) * 100 ) + "%";
        
        // not being used, but in future i may make slow transition between colours
        changeSpeed = 5 * Time.deltaTime;

        colourChanger();
        

    }
    

    void defineThings()
    {
        // define body parts and healths
        bodyParts.Add("headHealth" , 70 );
        bodyParts.Add("torsoHealth" , 100 );
        bodyParts.Add("leftArmHealth" , 60 );
        bodyParts.Add("rightArmHealth" , 60 );
        bodyParts.Add("leftLegHealth" , 80 );
        bodyParts.Add("rightLegHealth" , 80 );

    }



    public void decreaseHealth( string inputs )
    {
  
        //Debug.Log( bodyParts[1] );
        string temp = inputs.Substring( 0 , 3 );
        int damage = int.Parse( temp );
        int ind = inputs.LastIndexOf( ':' );
        temp = inputs.Substring(ind + 1);
        bodyPart = temp;

        //Debug.Log(determinePart(bodyPart));
        
        //Debug.Log(bodyParts[bodyPart]);
        if ( bodyParts[bodyPart] > 0 )
            bodyParts[bodyPart] -= damage;


    }
    
    public void increaseHealth( string inputs )
    {

        string temp = inputs.Substring( 0 , 3 );
        int healing = int.Parse( temp );
        int ind = inputs.LastIndexOf( ':' );
        temp = inputs.Substring(ind + 1);
        bodyPart = temp;
        //Debug.Log(bodyPart);
        switch (bodyPart)
        {
            case "headHealth":
                if ( bodyParts[bodyPart] < headMaxHealth )
                    bodyParts[bodyPart] += healing; 
                    if ( bodyParts[bodyPart] > headMaxHealth )
                        bodyParts[bodyPart] = (int)headMaxHealth;
                    Debug.Log(bodyParts[bodyPart]);
                    break;
            case "torsoHealth":
                if ( bodyParts[bodyPart] < torsoMaxHealth )
                    bodyParts[bodyPart] += healing; 
                    if ( bodyParts[bodyPart] > torsoMaxHealth )
                        bodyParts[bodyPart] = (int)torsoMaxHealth;
                    break;
            case "leftArmHealth":
            case "rightArmHealth":
                if ( bodyParts[bodyPart] < armMaxHealth )
                    bodyParts[bodyPart] += healing; 
                    if ( bodyParts[bodyPart] > armMaxHealth )
                        bodyParts[bodyPart] = (int)armMaxHealth;
                    break;
            case "leftLegHealth":
            case "rightLegHealth":
                if ( bodyParts[bodyPart] < legMaxHealth )
                    bodyParts[bodyPart] += healing; 
                    if ( bodyParts[bodyPart] > legMaxHealth )
                        bodyParts[bodyPart] = (int)legMaxHealth;
                    break;
        }



    }

    void colourChanger()
    {
        switch (bodyPart)
        {
            case "headHealth":
                healthColourHead = Color.Lerp( Color.red , Color.green , ( bodyParts[bodyPart] / headMaxHealth ));
                break;
            case "torsoHealth":
                healthColourTorso = Color.Lerp( Color.red , Color.green , ( bodyParts[bodyPart] / torsoMaxHealth ));
                break;
            case "leftArmHealth":
                healthColourLeftArm = Color.Lerp( Color.red , Color.green , ( bodyParts[bodyPart] / armMaxHealth ));
                break;
            case "rightArmHealth":
                healthColourRightArm = Color.Lerp( Color.red , Color.green , ( bodyParts[bodyPart] / armMaxHealth ));
                break;
            case "leftLegHealth":
                healthColourLeftLeg = Color.Lerp( Color.red , Color.green , ( bodyParts[bodyPart] / legMaxHealth ));
                break;
            case "rightLegHealth":
                healthColourRightLeg = Color.Lerp( Color.red , Color.green , ( bodyParts[bodyPart] / legMaxHealth ));
                break;
        }

        headHealth.color = healthColourHead;
        torsoHealth.color = healthColourTorso;
        leftArmHealth.color = healthColourLeftArm;
        rightArmHealth.color = healthColourRightArm;
        leftLegHealth.color = healthColourLeftLeg;
        rightLegHealth.color = healthColourRightLeg;

    }
}