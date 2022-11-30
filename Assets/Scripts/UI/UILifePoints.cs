using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityTemplateProjects;

[RequireComponent(typeof(Slider))]
public class UILifePoints : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private DataPlayer _dataPlayer;
    
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        _dataPlayer.HealthChanged += OnHealthChanged;
        OnHealthChanged();
        StartCoroutine(DiscreaseLifeCoroutine());

    }

    private IEnumerator DiscreaseLifeCoroutine()
    {
        while (_dataPlayer.CurrentLifePoints > 0)
        {
            yield return new WaitForSeconds(2);

            _dataPlayer.CurrentLifePoints -= 10;
        }
        
    }

    private void OnHealthChanged()
    {
        slider.value = (float)_dataPlayer.CurrentLifePoints / _dataPlayer.Health;
    }

    private void OnValidate()
    {
        slider ??= GetComponent<Slider>();
    }
}
