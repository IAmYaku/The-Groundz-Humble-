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
            case "TheGroundzEastButton":
                GlobalConfiguration.instance.SetStage("theGroundzEast");
                GlobalConfiguration.instance.TurnThemeMusic(false);
                SceneManager.LoadScene("TheGroundzEast");
                break;
            case "TheGymButton":
                GlobalConfiguration.instance.SetStage("theGym");
                GlobalConfiguration.instance.TurnThemeMusic(false);
                SceneManager.LoadScene("TheGym");
                break;
            case "TheBlockButton":
                GlobalConfiguration.instance.SetStage("theBlock");
                GlobalConfiguration.instance.TurnThemeMusic(false);
                SceneManager.LoadScene("TheBlock");
                break;

            case "TheLibraryButton":
                GlobalConfiguration.instance.SetStage("theBlock");
                GlobalConfiguration.instance.TurnThemeMusic(false);
                SceneManager.LoadScene("TheBlock");
                break;

            case "TheGroundzWestButton":
                GlobalConfiguration.instance.SetStage("theBlock");
                GlobalConfiguration.instance.TurnThemeMusic(false);
                SceneManager.LoadScene("TheBlock");
                break;
            case "TheBackyardButton":
                GlobalConfiguration.instance.SetStage("theBackyard");
                GlobalConfiguration.instance.TurnThemeMusic(false);
                SceneManager.LoadScene("TheBackyard");
                break;

        }
    }
}
