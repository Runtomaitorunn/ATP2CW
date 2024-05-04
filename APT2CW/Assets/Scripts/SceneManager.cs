using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public bool goToScene2;
    public bool goToScene3;
    public bool goToScene4;



    public void GoToNextScene()
    {
        if (goToScene2)
        {
            // Transition 
            SceneTransition();


            // direct to next scene
            
        }
    }
    public void SceneTransition()
    {
        //animation

        //sound
    }
}
