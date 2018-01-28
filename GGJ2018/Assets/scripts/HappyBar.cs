using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HappyBar : MonoBehaviour {

    [SerializeField]
    private Image content;

    [SerializeField]
    private Color fullColor;
    [SerializeField]
    private Color lowColor;
    private float fillAmount;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void RepaintHappiness(double d)
    {
        content.fillAmount = (float)d;
        content.color = Color.Lerp(lowColor, fullColor, (float)d);
    }



}
