using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image foreground;

    private void Awake()
    {      
        GetComponentInParent<Health>().OnHealthChanged += HandleHealthChanged;
    }

    private void HandleHealthChanged(float pct)
    {
        foreground.fillAmount = pct;
    }
    
    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }
}
