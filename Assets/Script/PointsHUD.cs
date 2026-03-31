using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PointsHUD : MonoBehaviour
{
    [SerializeField] Text pointText;

    int points = 0;

    private void Awake()
    {
        UpdateHUD();
    }

    public int Points 
    {
        get { return points; }
        set
        {
            points = value;
            UpdateHUD();
        }
    }

    private void UpdateHUD()
    {
        pointText.text = points.ToString(); 
    }
}
