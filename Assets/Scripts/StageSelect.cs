using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour {

public void selectScene()
    {
        GlobalConfiguration.instance.SetGameStarted(true);

        switch (this.gameObject.name)
        {
            case "TheGroundzButton":
                GlobalConfiguration.instance.SetStage("theGroundz");
                SceneManager.LoadScene("TheGroundz");
                break;
            case "TheGymButton":
                GlobalConfiguration.instance.SetStage("theGym");
                SceneManager.LoadScene("TheGym");
                break;
            case "TheBlockButton":
                GlobalConfiguration.instance.SetStage("theBlock");
                SceneManager.LoadScene("TheBlock");
                break;
         }
    }
}
