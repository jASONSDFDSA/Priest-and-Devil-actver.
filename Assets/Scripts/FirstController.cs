using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, ISceneController, IUserAction {

	public CCActionManager actionManager { get; set;}
	public GameObject move1,move2;
    public int leftPriestCount = 3;
    public int rightPriestCount = 0;
    public int leftDevilCount = 3;
    public int rightDevilCount = 0;
    public int peopleOnBoat = 0;
    public bool isMoving = false;
    public bool isRight = false;
    public GameObject priest;
    public GameObject devil;
    
    private GameObject[] priests = new GameObject[3];
    private GameObject[] devils = new GameObject[3];
    private GameObject boat;
    private bool win = false;
    private bool firstSeatFull = false;
    private bool lastSeatFull = false;
    private int priestCount = 0;
    private int devilCount = 0;
    void Awake()
    {
        SSDirector director = SSDirector.getInstance();
        director.setFPS(60);
        director.currentSceneController = this;
        director.currentSceneController.LoadResources();
    }

    public void LoadResources()
    {
        GameObject river = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/River"), new Vector3(0, 0, -10), transform.rotation);
        river.name = "River";
        Debug.Log("Load River...");
        
        GameObject boat = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Boat"), new Vector3(0, 0.5f, -15), transform.rotation);
        boat.name = "Boat";
        Debug.Log("Load Boat...");
    }

    public void Init()
    {
        leftPriestCount = 3;
        rightPriestCount = 0;
        leftDevilCount = 3;
        rightDevilCount = 0;
        isMoving = false;
        isRight = false;
        win = false;
        firstSeatFull = false;
        lastSeatFull = false;
        priestCount = 0;
        devilCount = 0;
        peopleOnBoat = 0;
        GameObject[] obj = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject child in obj)
        {
            if(child.name == "Priest1"||child.name == "Devil1"
            || child.name == "Priest2"||child.name == "Devil2"
            || child.name == "Priest3"||child.name == "Devil3") Destroy(child);
            if (child.name == "Boat")
            {
                child.transform.position = new Vector3(0, 0.5f, -15);
                boat = child;
            }
        }
        for (int i = 0; i < 3; i++)
        {
            devils[i] = Instantiate(devil, new Vector3(-17 + 2*i, 1, -19), transform.rotation);
            devils[i].GetComponent<settings>().onBoat = false;
            devils[i].GetComponent<settings>().isRight = false;
            devils[i].name = "Devil" + (i + 1).ToString();
            priests[i] = Instantiate(priest, new Vector3(13 + 2*i, 1, -19), transform.rotation);
            priests[i].GetComponent<settings>().onBoat = false;
            priests[i].GetComponent<settings>().isRight = false;
            priests[i].name = "Priest" + (i + 1).ToString();
        }
    }
    
    public void moveOnBoat(int index, bool isPriest)
    {
        GameObject entity = null;
        if (isPriest)
        {
            entity = priests[index];
        }
        else
        {
            entity = devils[index];
        }
        if (!isMoving)
        {
            // Move on boat.
            if (!entity.GetComponent<settings>().onBoat)
            {
                if (entity.GetComponent<settings>().isRight == isRight)
                {
                    if (peopleOnBoat == 0)
                    {
                        entity.transform.position = boat.transform.position + new Vector3(0, 0, 0.9f);
                        entity.transform.parent = boat.transform;
                        peopleOnBoat++;
                        firstSeatFull = true;
                        entity.GetComponent<settings>().onBoat = true;
                        if (entity.name == "Priest1" || entity.name == "Priest2" || entity.name == "Priest3")
                        {
                            priestCount++;
                        }
                        else
                        {
                            devilCount++;
                        }
                    }
                    else if (peopleOnBoat == 1)
                    {
                        if (firstSeatFull)
                        {
                            entity.transform.position = boat.transform.position + new Vector3(0, 0, -0.9f);
                            lastSeatFull = true;
                        }
                        else if (lastSeatFull)
                        {
                            entity.transform.position = boat.transform.position + new Vector3(0, 0, 0.9f);
                            firstSeatFull = true;
                        }

                        entity.transform.parent = boat.transform;
                        peopleOnBoat++;
                        entity.GetComponent<settings>().onBoat = true;
                        if (entity.name == "Priest1" || entity.name == "Priest2" || entity.name == "Priest3")
                        {
                            priestCount++;
                        }
                        else
                        {
                            devilCount++;
                        }
                    }
                    else return;
                }
                else
                { 
                    if (peopleOnBoat == 0 && entity.GetComponent<settings>().isRight == isRight)
                    {
                        entity.transform.position = boat.transform.position + new Vector3(0, 0, 0.9f);
                        entity.transform.parent = boat.transform;
                        peopleOnBoat++;
                        firstSeatFull = true;
                        entity.GetComponent<settings>().onBoat = true;
                        if (entity.name == "Priest1" || entity.name == "Priest2" || entity.name == "Priest3")
                        {
                            priestCount++;
                        }
                        else
                        {
                            devilCount++;
                        }
                    }
                    else if (peopleOnBoat == 1 && entity.GetComponent<settings>().isRight == isRight)
                    {
                        if (firstSeatFull)
                        {
                            entity.transform.position = boat.transform.position + new Vector3(0, 0, -0.9f);
                            lastSeatFull = true;
                        }
                        else if (lastSeatFull)
                        {
                            entity.transform.position = boat.transform.position + new Vector3(0, 0, 0.9f);
                            firstSeatFull = true;
                        }

                        entity.transform.parent = boat.transform;
                        peopleOnBoat++;
                        entity.GetComponent<settings>().onBoat = true;
                        if (entity.name == "Priest1" || entity.name == "Priest2" || entity.name == "Priest3")
                        {
                            priestCount++;
                        }
                        else
                        {
                            devilCount++;
                        }
                    }
                    else return;
                }
            }
            
            // Move on land.
            else
            {
                if (entity.transform.position == boat.transform.position + new Vector3(0, 0, 0.9f))
                {
                    firstSeatFull = false;
                }
                else
                {
                    lastSeatFull = false;
                }

                if (!isRight)
                {
                    if (isPriest)
                    {
                        entity.transform.position = new Vector3(13 + 2 * index, 1, -19);
                        entity.GetComponent<settings>().onBoat = false;
                        priestCount--;
                    }
                    else
                    {
                        entity.transform.position = new Vector3(-17 + 2 * index, 1, -19);
                        entity.GetComponent<settings>().onBoat = false;
                        devilCount--;
                    }
                }
                else
                {
                    if (isPriest)
                    {
                        entity.transform.position = new Vector3(13 + 2 * index, 1, -1);
                        entity.GetComponent<settings>().onBoat = false;
                        priestCount--;
                    }
                    else
                    {
                        entity.transform.position = new Vector3(-17 + 2 * index, 1, -1);
                        entity.GetComponent<settings>().onBoat = false;
                        devilCount--;
                    }
                }

                // Set position.
                entity.GetComponent<settings>().isRight = isRight;
                peopleOnBoat--;
                entity.transform.parent = null;
            }
        }
    }
    
    public void boatMov()
    {
        if (priestCount >= 1)
        {
            isMoving = true;
            if (!isRight)
            {
                leftDevilCount = 3 - devilCount - rightDevilCount;
                leftPriestCount = 3 - priestCount - rightPriestCount;
            }
            else
            {
                rightDevilCount = 3 - devilCount - leftDevilCount;
                rightPriestCount = 3 - priestCount - leftPriestCount;
            }
        }
        else
        {
            return;
        }
    }
    
    public bool Judge()
    {
        if ((leftDevilCount > leftPriestCount && leftPriestCount != 0) ||
            (rightDevilCount > rightPriestCount && rightPriestCount != 0))
        {
            win = false;
            return true;
        }
        else if(rightPriestCount+priestCount==3&&rightDevilCount+devilCount==3)
        {
            win = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GameOver()
    {
        if (win)
        {
            GUI.Box(new Rect(Screen.width/2 - 100, Screen.height/2-100, 200, 200), "Congratulations!");
        }
        else
        {
            GUI.Box(new Rect(Screen.width/2 - 100, Screen.height/2-100, 200, 200), "Game Over");
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    
    void Update()
    {
        if (isMoving)
        {
            if (!isRight)
            {
                boat.transform.position += new Vector3(0, 0, 10 * Time.deltaTime);
            }
            else
            {
                boat.transform.position += new Vector3(0, 0, -10 * Time.deltaTime);
            }
        }

        if (boat.transform.position.z < -15 || boat.transform.position.z > -5)
        {
            isMoving = false;
            if (isRight)
            {
                boat.transform.position = new Vector3(0, 0.5f, -15);
            }
            else
            {
                boat.transform.position = new Vector3(0, 0.5f, -5);
            }

            isRight = !isRight;
        }
    }
}
