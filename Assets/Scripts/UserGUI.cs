using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
    private IUserAction action;
    
    // Start is called before the first frame update
    void Start()
    {
        action = SSDirector.getInstance().currentSceneController as IUserAction;
    }

    // Update is called once per frame
    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width/2 - 440, Screen.height/2, 100, 30), "Restart")) action.Init();
        if (!action.Judge())
        {
            if (GUI.Button(new Rect(Screen.width/2 - 330, Screen.height/2, 100, 30), "MovePriest1")) action.moveOnBoat(0,true);
            if (GUI.Button(new Rect(Screen.width/2 - 220, Screen.height/2, 100, 30), "MovePriest2")) action.moveOnBoat(1,true);
            if (GUI.Button(new Rect(Screen.width/2 - 110, Screen.height/2, 100, 30), "MovePriest3")) action.moveOnBoat(2,true);
            if (GUI.Button(new Rect(Screen.width/2, Screen.height/2, 100, 30), "MoveDevil1")) action.moveOnBoat(0,false);
            if (GUI.Button(new Rect(Screen.width/2 + 110, Screen.height/2, 100, 30), "MoveDevil2")) action.moveOnBoat(1,false);
            if (GUI.Button(new Rect(Screen.width/2 + 220, Screen.height/2, 100, 30), "MoveDevil3")) action.moveOnBoat(2,false);
            if (GUI.Button(new Rect(Screen.width/2 + 330, Screen.height/2, 100, 30), "Crossing")) action.boatMov();
        }
        else
        {
            action.GameOver();
        }
    }
}
