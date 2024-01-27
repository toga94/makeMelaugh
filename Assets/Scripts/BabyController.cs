using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BabyController : MonoBehaviour
{
    [SerializeField] private Slider _emosionalIndicator;
    [SerializeField] private Animator _animator;
    [SerializeField] private Image _babyBackground;
    private float _emosion;
    private Color[] _colors;
    private void Start()
    {
        _emosionalIndicator.value = 0;
        _colors = new Color[3]
        {
            new Color32(15, 137, 0, 179),
            new Color32(192, 190, 95, 179),
            new Color32(195, 34, 31, 179),
        };

    }

    private void Update()
    {
        _emosion = _emosionalIndicator.value;
        BabyState(_emosion);
    }
    
    private void BabyState(float emosion)
    {
        if (emosion <= -0.24)
        {
            _animator.SetTrigger("isLaugh");
            _babyBackground.color = _colors[0];

        }
        if (emosion > -0.24 && emosion <= 0.27)
        {
            _animator.SetTrigger("isIdle");
            _babyBackground.color = _colors[1];
        }
        if(emosion > 0.27) {
            _animator.SetTrigger("isCry");
            _babyBackground.color = _colors[2];
        }
    }
}
