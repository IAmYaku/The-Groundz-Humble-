using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObject : MonoBehaviour
{
    public GameObject controlPanel;
    public GameObject buttonsAnchor;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void DisableButtonAnchor()
    {
        controlPanel.SetActive(true);

        if (controlPanel == enabled)
        {
            buttonsAnchor.SetActive(false);
        }
        
    }

    public void EnableButtonAnchor()
    {
        buttonsAnchor.SetActive(true);

        if (buttonsAnchor == enabled)
        {
            controlPanel.SetActive(false);
        }
    }
}
