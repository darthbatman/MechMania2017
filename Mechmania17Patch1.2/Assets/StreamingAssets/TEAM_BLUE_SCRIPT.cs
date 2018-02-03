using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------- CHANGE THIS NAME HERE -------
public class hbdsankruth : MonoBehaviour
{
    //---------- CHANGE THIS NAME HERE -------
    public static hbdsankruth AddYourselfTo(GameObject host) {
        //---------- CHANGE THIS NAME HERE -------
        return host.AddComponent<hbdsankruth>();
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

    private bool characterOneShouldCaptureObjective;
    private bool characterTwoShouldCaptureObjective;
    private bool characterThreeShouldCaptureObjective;

    private bool characterOneShouldSpawnCamp;
    private bool characterTwoShouldSpawnCamp;
    private bool characterThreeShouldSpawnCamp;

    private bool characterOneShouldRotate;
    private bool characterTwoShouldRotate;
    private bool characterThreeShouldRotate;

    private bool characterOneShouldGetItems;

    private int degrees1;
    private int degrees2;
    private int degrees3;

    private float timer = 0;

    void Start()
    {
        character1 = transform.Find("Character1").gameObject.GetComponent<CharacterScript>();
        character2 = transform.Find("Character2").gameObject.GetComponent<CharacterScript>();
        character3 = transform.Find("Character3").gameObject.GetComponent<CharacterScript>();

        middleObjective = GameObject.Find("MiddleObjective").GetComponent<ObjectiveScript>();
        leftObjective = GameObject.Find("LeftObjective").GetComponent<ObjectiveScript>();
        rightObjective = GameObject.Find("RightObjective").GetComponent<ObjectiveScript>();

        teamName = "none";

        characterOneShouldCaptureObjective = false;
        characterTwoShouldCaptureObjective = true;
        characterThreeShouldCaptureObjective = true;

        characterOneShouldSpawnCamp = false;
        characterTwoShouldSpawnCamp = false;
        characterThreeShouldSpawnCamp = false;

        characterOneShouldRotate = true;
        characterTwoShouldRotate = true;
        characterThreeShouldRotate = true;

        characterOneShouldGetItems = true;

        degrees1 = 1;
        degrees2 = 1;
        degrees3 = 1;

        InvokeRepeating("gameTimer", 0.0f, 0.5f);

    }
    /*^^^^ DO NOT MODIFY ^^^^*/

    /* Your code below this line */
    // Update() is called every frame

    public void doesItemExist(CharacterScript character) {

        if (character.FindClosestItem().transform.position != new Vector3(0, 0, 0)) {
            
            characterOneShouldRotate = true;

            characterOneShouldGetItems = true;

            characterOneShouldCaptureObjective = false;

            characterOneShouldSpawnCamp = false;

        }

    }

    public void goAfterItems(CharacterScript character) {

        if (character.FindClosestItem().transform.position != new Vector3(0, 0, 0)) {
            
            character.MoveChar(character1.FindClosestItem().transform.position);

        } else {
            
            characterOneShouldGetItems = false;

            characterOneShouldCaptureObjective = true;

        }

    }

    public void goAfterClosest(CharacterScript character) {

        switch ((int)character.FindClosestObjective().x) {

            case 0:

                if (middleObjective.getControllingTeam() != character.getTeam()) {

                    if (teamName == "blue") {

                        character.MoveChar(new Vector3(3.5f, 0, -3.5f));

                    } else {

                        character.MoveChar(new Vector3(-3.5f, 0, 3.5f));

                    }

                } else {

                    goAfterUncaptured(character);

                }

                break;

            case -40:

                if (leftObjective.getControllingTeam() != character.getTeam()) {

                    character.MoveChar(new Vector3(-35f, 0f, -22f));

                } else {

                    goAfterUncaptured(character);

                }

                break;

            case 40:

                if (rightObjective.getControllingTeam() != character.getTeam()) {

                    character.MoveChar(new Vector3(35f, 0f, 22f));

                } else {

                    goAfterUncaptured(character);

                }

                break;

        }
    }

    public void goAfterUncaptured(CharacterScript character) {

        if (middleObjective.getControllingTeam() != character.getTeam()) {

            if (teamName == "blue") {

                character.MoveChar(new Vector3(3.5f, 0, -3.5f));

            } else {

                character.MoveChar(new Vector3(-3.5f, 0, 3.5f));

            }

        }
        else {

            if (leftObjective.getControllingTeam() != character.getTeam()) {

                character.MoveChar(new Vector3(-35f, 0f, -22f));

            }

            if (rightObjective.getControllingTeam() != character.getTeam()) {

                character.MoveChar(new Vector3(35f, 0f, 22f));

            }

        }

    }

    void rotate(bool ch1, bool ch2, bool ch3) {

        if (ch1) {

            character1.rotateAngle(degrees1 * 140);

        }

        if (ch2) {

            character2.rotateAngle(degrees2 * 140);

        }

        if (ch3) {

            character3.rotateAngle(degrees3 * -140);

        }

    }


    void captureObjectives(bool ch1, bool ch2, bool ch3) {

        if (ch1) {

            goAfterClosest(character1);

        }

        if (ch2) {

            goAfterClosest(character2);

        }

        if (ch3) {

            goAfterClosest(character3);

        }

    }

    void checkAndSetTeam() {

        if (teamName == "none") {

            if (character1.getZone() == zone.BlueBase) {

                teamName = "blue";

            } else {

                teamName = "red";

            }

        }

    }

    void beginSpawnCamping(bool ch1, bool ch2, bool ch3) {

        if (teamName == "blue") {

            if (ch1) {

                if (timer % 2 == 0) {

                    character1.MoveChar(new Vector3(-50f, 0f, -28f));

                } else {

                    character1.MoveChar(new Vector3(-50f, 0f, -24f));

                }
                
                characterOneShouldRotate = false;
                if (character1.isDoneMoving())
                    character1.SetFacing(leftObjective.transform.position);

            }

            if (ch2) {

                if (teamName == "blue") {

                    if (timer % 2 == 0) {

                        character2.MoveChar(new Vector3(12f, 0f, -8f));

                    } else {

                        character2.MoveChar(new Vector3(16.5f, 0f, -5.5f));

                    }

                } else {

                    if (timer % 2 == 0) {

                        character2.MoveChar(new Vector3(-12f, 0f, 8f));

                    } else {

                        character2.MoveChar(new Vector3(-16.5f, 0f, 5.5f));

                    }

                }

                characterTwoShouldRotate = false;
                if (character2.isDoneMoving())
                    character2.SetFacing(middleObjective.transform.position);

            }

            if (ch3) {

                if (timer % 2 == 0) {

                    character3.MoveChar(new Vector3(50f, 0f, 28f));

                } else {

                    character3.MoveChar(new Vector3(50f, 0f, 24f));

                }

                characterThreeShouldRotate = false;
                if (character3.isDoneMoving())
                    character3.SetFacing(rightObjective.transform.position);

            }

        } else {

            if (ch1) {

                character1.MoveChar(new Vector3(-50f, 0f, -28f));
                characterOneShouldRotate = false;
                if (character1.isDoneMoving())
                    character1.SetFacing(leftObjective.transform.position);

            }

            if (ch2) {

                character2.MoveChar(new Vector3(-12f, 0f, 8f));
                characterTwoShouldRotate = false;
                if (character2.isDoneMoving())
                    character2.SetFacing(middleObjective.transform.position);

            }

            if (ch3) {

                character3.MoveChar(new Vector3(50f, 0f, 28f));
                characterThreeShouldRotate = false;
                if (character3.isDoneMoving())
                    character3.SetFacing(rightObjective.transform.position);

            }

        }

    }

    void checkVisibleEnemies() {

        if (character1.visibleEnemyLocations.Count > 0) {

            character1.SetFacing(character1.visibleEnemyLocations[0]);

            characterOneShouldRotate = false;

            degrees1 = degrees1 * -1;

        }
        else {

            characterOneShouldRotate = true;

        }

        if (character2.visibleEnemyLocations.Count > 0) {

            character2.SetFacing(character2.visibleEnemyLocations[0]);

            characterTwoShouldRotate = false;

            degrees2 = degrees2 * -1;

        }
        else {

            characterTwoShouldRotate = true;

        }

        if (character3.visibleEnemyLocations.Count > 0) {

            character3.SetFacing(character3.visibleEnemyLocations[0]);

            characterThreeShouldRotate = false;

            degrees3 = degrees3 * -1;

        }
        else {

            characterThreeShouldRotate = true;

        }

    }

    void Update()
    {
        
        if (character1.getZone() == zone.BlueBase || character1.getZone() == zone.RedBase)
            character1.setLoadout(loadout.SHORT);

        if (character2.getZone() == zone.BlueBase || character2.getZone() == zone.RedBase)
            character2.setLoadout(loadout.SHORT);

        if (character2.getZone() == zone.BlueBase || character2.getZone() == zone.RedBase)
            character3.setLoadout(loadout.SHORT);

        checkAndSetTeam();

        checkVisibleEnemies();

        rotate(characterOneShouldRotate, characterTwoShouldRotate, characterThreeShouldRotate);

        if (characterOneShouldGetItems) {

            goAfterItems(character1);

        }

        beginSpawnCamping(characterOneShouldSpawnCamp, characterTwoShouldSpawnCamp, characterThreeShouldSpawnCamp);

        captureObjectives(characterOneShouldCaptureObjective, characterTwoShouldCaptureObjective, characterThreeShouldCaptureObjective);

        if (leftObjective.getControllingTeam() == character1.getTeam() &&
            middleObjective.getControllingTeam() == character1.getTeam() &&
            rightObjective.getControllingTeam() == character1.getTeam()) {

            characterOneShouldCaptureObjective = false;
            characterTwoShouldCaptureObjective = false;
            characterThreeShouldCaptureObjective = false;

            characterOneShouldSpawnCamp = true;
            characterTwoShouldSpawnCamp = true;
            characterThreeShouldSpawnCamp = true;

        }
        else {

            characterOneShouldCaptureObjective = true;
            characterTwoShouldCaptureObjective = true;
            characterThreeShouldCaptureObjective = true;

            characterOneShouldSpawnCamp = false;
            characterTwoShouldSpawnCamp = false;
            characterThreeShouldSpawnCamp = false;


        }

        doesItemExist(character1);

    } 

    public void gameTimer()
    {
        timer += 1;
    }

}
