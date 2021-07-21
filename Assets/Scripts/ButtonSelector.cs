using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelector : MonoBehaviour
{
    public List<Button> buttons = new List<Button>(); // Mack = 0; King =1; Nina = 2;
   public Button selected_current;
    public Color selectedColor;
    public Color nonSelectedColor;

    void Start()
    {
        if (buttons.Count> 0)
        {
            int index = 0;

            selected_current = buttons[index];
            ColorBlock s_ColorBlock = selected_current.colors;
            s_ColorBlock.normalColor = selectedColor;
            selected_current.GetComponent<Button>().colors = s_ColorBlock;


            for (int i =0; i< buttons.Count; i++)
            {
                if (i != index)
                {
                    Button nonselected = buttons[i];
                    ColorBlock n1_ColorBlock = nonselected.colors;
                    n1_ColorBlock.normalColor = nonSelectedColor;
                    nonselected.GetComponent<Button>().colors = n1_ColorBlock;
                }
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ButtonSelect(int index)
    {
        if (buttons.Count > 0) { 

            selected_current = buttons[index];
            ColorBlock s_ColorBlock = selected_current.colors;
            s_ColorBlock.normalColor = selectedColor;
            selected_current.GetComponent<Button>().colors = s_ColorBlock;

            for (int i = 0; i < buttons.Count; i++)
            {
                if (i != index)
                {
                    Button nonselected = buttons[i];
                    ColorBlock n1_ColorBlock = nonselected.colors;
                    n1_ColorBlock.normalColor = nonSelectedColor;
                    nonselected.GetComponent<Button>().colors = n1_ColorBlock;
                }
            }
        }
    }

}
