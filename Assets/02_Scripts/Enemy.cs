using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy를 구성하는 클래스
/// </summary>
public class Enemy : MonoBehaviour
{
    [Header("컴포넌트 참조")]
    [SerializeField] EnemyModel _model;
    [SerializeField] EnemyView _view;

    public EnemyModel EnemyModel => _model;
    public EnemyView View => _view;

    public void Initialize()
    {
        _model.OnHpChanged += _view.UpdateView;

        _model.Initialize();
        _view.Initialize();
    }

    /// <summary>
    /// Model의 TakeDamage 함수를 호출하는 함수
    /// </summary>
    /// <param name="amount"></param>
    public void TakeDamage(int amount)
    {
        _model.TakeDamage(amount);
    }
}
