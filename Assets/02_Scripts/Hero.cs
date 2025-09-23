using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 영웅을 구성하는 클래스
/// </summary>
public class Hero : MonoBehaviour
{
    [SerializeField] HeroModel _model; // 영웅의 데이터 모델
    [SerializeField] HeroView _view;   // 영웅의 뷰 컴포넌트

    public event Action OnMoved; // 이동이 완료되었을 때 발생하는 이벤트
    public event Action OnGoalReached; // 골에 도달했을 때 발생하는 이벤트
    public event Action<Enemy> OnEnemyEncountered; // 적과 조우했을 때 발생하는 이벤트

    public HeroModel HeroModel => _model;
    public HeroView View => _view;

    public void Initialize()
    {
        _model.OnHpChanged += _view.UpdateView;

        _model.Initialize();
        _view.Initialize();
    }

    /// <summary>
    /// 영웅을 한 칸 앞으로 이동시키는 함수
    /// </summary>
    public void Move()
    {
        transform.position += Vector3.forward;
        OnMoved?.Invoke();
    }

    /// <summary>
    /// Model의 TakeDamage 함수를 호출하는 함수
    /// </summary>
    /// <param name="amount"></param>
    public void TakeDamage(int amount)
    {
        _model.TakeDamage(amount);
    }

    /// <summary>
    /// 영웅의 아래쪽에 레이를 쏴서 골에 도달했는지 확인하는 함수
    /// </summary>
    public void CheckGoal()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1.0f))
        {
            if (hitInfo.collider.CompareTag("Goal"))
            {
                Debug.Log("골 도착!");
                OnGoalReached?.Invoke();
            }
        }
    }

    /// <summary>
    /// 영웅의 오른쪽에 레이를 쏴서 적이 있는지 확인하는 함수
    /// </summary>
    public void CheckEnemy()
    {
        Ray ray = new Ray(transform.position, Vector3.right);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 2.0f))
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                Debug.Log("적 발견!");
                Enemy enemy = hitInfo.collider.GetComponent<Enemy>();
                OnEnemyEncountered?.Invoke(enemy);
            }
        }
    }
}
