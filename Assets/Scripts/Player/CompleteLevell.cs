using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompleteLevel : MonoBehaviour
{
    [SerializeField] private GameObject portal;
    [SerializeField] private int rquiredCandy;
    [SerializeField] private Image[] candyImages;

    private int curCandy = 0;
    // Start is called before the first frame update
    void Start()
    {
        portal.SetActive(false);
         

        foreach (Image curImage in candyImages)
        {
            Color startColor = curImage.color;
            startColor.a = 0.3f;
            curImage.color = startColor;    
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FindCandy()
    {

        Debug.Log(curCandy + 1);
        Color imageColor = candyImages[curCandy].color;
        imageColor.a = 1;
        candyImages[curCandy].color = imageColor;

        curCandy += 1;
        
        if (curCandy == rquiredCandy)
        {
            portal.SetActive(true);
        }
    }
}
