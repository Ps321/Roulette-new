using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Commands;

public class Buttons1 : MonoBehaviour
{
   public Sprite[] originalSprite;
    public Sprite[] highlightedSprite;
    public Button[] bb;
    private Button currentHighlightedButton;

    public AudioSource audio;
    public int c=0;
public Button B;

/// <summary>
/// Start is called on the frame when a script is enabled just before
/// any of the Update methods is called the first time.
/// </summary>
void Start()
{
    ButtonDict.highlightedButton=B;
}

    public void OnClick(int num)
    {
        audio.Play();
        CompareGameObjects();
        if (ButtonDict.highlightedButton == null)
        {
            ButtonDict.highlightedButton = gameObject.GetComponent<Button>();
            ButtonDict.highlightedButton.image.sprite = highlightedSprite[num];
        }
        else
        {
            ButtonDict.highlightedButton.image.sprite = originalSprite[c];
           ButtonDict.highlightedButton = gameObject.GetComponent<Button>();
          gameObject.GetComponent<Button>().image.sprite = highlightedSprite[num];
        }
    }

    void CompareGameObjects()
{
    for(int i=0;i<7;i++)
    if(ButtonDict.highlightedButton.name == bb[i].name)
    {
        c=i;
      // ButtonDict.highlightedButton.image.sprite = originalSprite[c];
       break;
    }
 
}
}
