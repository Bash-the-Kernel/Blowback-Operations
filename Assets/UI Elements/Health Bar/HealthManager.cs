using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
<<<<<<< Updated upstream
=======
using System.Linq;
>>>>>>> Stashed changes

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

<<<<<<< Updated upstream
    float headMaxHealth = 70;
    float torsoMaxHealth = 100;
    float left_ArmMaxHealth , right_ArmMaxHealth = 60;
    float left_legMaxHealth , right_legMaxHealth = 80;

    List<KeyValuePair<string, int>> bodyParts = new List<KeyValuePair<string, int>>();
=======
    string bodyPart = "headHealth";

    float headMaxHealth = 70;
    float torsoMaxHealth = 100;
    float left_ArmMaxHealth , right_ArmMaxHealth = 60;
    float left_LegMaxHealth , right_LegMaxHealth = 80;

    Dictionary<string, int> bodyParts = new Dictionary<string, int>(); 
    //ICollection<KeyValuePair<string, int>> bodyParts = new Dictionary<string, int>();
    
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
        bodyParts.Add(new KeyValuePair< string , int >("head" , 70 ));
        bodyParts.Add(new KeyValuePair< string , int >("torsoHealth" , 100 ));
        bodyParts.Add(new KeyValuePair< string , int >("left_ArmHealth" , 60 ));
        bodyParts.Add(new KeyValuePair< string , int >("right_ArmHealth" , 60 ));
        bodyParts.Add(new KeyValuePair< string , int >("left_legHealth" , 80 ));
        bodyParts.Add(new KeyValuePair< string , int >("right_legHealth" , 80 ));
=======
        bodyParts.Add("headHealth" , 70 );
        bodyParts.Add("torsoHealth" , 100 );
        bodyParts.Add("left_ArmHealth" , 60 );
        bodyParts.Add("right_ArmHealth" , 60 );
        bodyParts.Add("left_LegHealth" , 80 );
        bodyParts.Add("right_LegHealth" , 80 );
>>>>>>> Stashed changes

    }

    void HealthBarFiller()
    {
<<<<<<< Updated upstream
        Debug.Log( bodyParts[1] );
        headHealth.fillAmount = Mathf.Lerp( headHealth.fillAmount , health / maxHealth , changeSpeed );
        
    }

    public void decreaseHealth( string inputs )
    {
        var result = bodyParts.Where(kvp => kvp.Value == "head");
        Debug.Log( result );
        Debug.Log( bodyParts[1] );
=======
        //Debug.Log( bodyParts[1] );
        headHealth.fillAmount = Mathf.Lerp( headHealth.fillAmount , bodyParts[bodyPart] / headMaxHealth , changeSpeed );
        
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
>>>>>>> Stashed changes
        string temp = inputs.Substring( 0 , 3 );
        int damage = int.Parse( temp );
        int ind = inputs.LastIndexOf( ':' );
        temp = inputs.Substring(ind + 1);
        string bodyPart = temp;

<<<<<<< Updated upstream
        var random = new System.Random();
        int randVal = random.Next(bodyParts.Count);
        Debug.Log( bodyParts[randVal] );
=======
        Debug.Log(determinePart(bodyPart));
        
        Debug.Log(bodyParts[bodyPart]);
        if ( bodyParts[bodyPart] > 0 )
            bodyParts[bodyPart] -= damage;
>>>>>>> Stashed changes


    }
    
    public void increaseHealth( float increasedHealth )
    {

        if ( health < maxHealth )
            health += increasedHealth; 

    }

    void colourChanger()
    {

<<<<<<< Updated upstream
        Color healthColour = Color.Lerp( Color.red , Color.green , ( health / maxHealth ));
=======
        Color healthColour = Color.Lerp( Color.red , Color.green , ( bodyParts[bodyPart] / headMaxHealth ));
>>>>>>> Stashed changes
        headHealth.color = healthColour;

    }
}