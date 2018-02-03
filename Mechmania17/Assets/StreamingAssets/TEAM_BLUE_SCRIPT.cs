using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------- CHANGE THIS NAME HERE -------
public class TEAM_BLUE_SCRIPT : MonoBehaviour
{
    //---------- CHANGE THIS NAME HERE -------
    public static TEAM_BLUE_SCRIPT AddYourselfTo(GameObject host) {
        //---------- CHANGE THIS NAME HERE -------
        return host.AddComponent<TEAM_BLUE_SCRIPT>();
    }

    /*vvvv DO NOT MODIFY vvvvv*/
    [SerializeField]
    public CharacterScript character1;
    [SerializeField]
    public CharacterScript character2;
    [SerializeField]
    public CharacterScript character3; 

    private ObjectiveScript middleObjective;
    private ObjectiveScript leftObjective;
    private ObjectiveScript rightObjective;

    private string teamName;

    void Start()
    {
        character1 = transform.Find("Character1").gameObject.GetComponent<CharacterScript>();
        character2 = transform.Find("Character2").gameObject.GetComponent<CharacterScript>();
        character3 = transform.Find("Character3").gameObject.GetComponent<CharacterScript>();

        middleObjective = GameObject.Find("MiddleObjective").GetComponent<ObjectiveScript>();
        leftObjective = GameObject.Find("LeftObjective").GetComponent<ObjectiveScript>();
        rightObjective = GameObject.Find("RightObjective").GetComponent<ObjectiveScript>();

        teamName = "none";
    }
    /*^^^^ DO NOT MODIFY ^^^^*/

    /* Your code below this line */
    // Update() is called every frame

    void beginSpawnCamping() {

        if (teamName == "blue") {

            character1.MoveChar(new Vector3(-55f, 0f, 28f));

        } else {

            character1.MoveChar(new Vector3(60f, 0f, -38f));

        }

    }

    void Update()
	{

        if (teamName == "none") {

            if (character1.getZone() == zone.BlueBase) {

                teamName = "blue";

            } else {

                teamName = "red";

            }

        }
    
        beginSpawnCamping();

        character1.rotateangle(10);


        //character1.MoveChar(middleObjective.transform.position);

        // character3.rotateangle(10);

    } 
}
