using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 전투 가능한 캐릭터의 로직적인 모델을 구성하는 클래스
/// </summary>
public class CombatCharacterModel : MonoBehaviour
{
    [SerializeField] protected int _maxHp;      // 최대 체력
    [SerializeField] protected int _currentHp;  // 현재 체력
    [SerializeField] protected int _damage;     // 공격력

    Coroutine _dieRoutine;

    public int MaxHp => _maxHp;
    public int CurrentHp => _currentHp;
    public int Damage => _damage;

    public event Action<float, float> OnHpChanged;   // 체력이 변경되었을 때 발생하는 이벤트(현재 체력, 최대 체력)
    public event Action OnDead;                      // 캐릭터가 사망했을 때 발생하는 이벤트

    public virtual void Initialize()
    {
        _currentHp = _maxHp;
        // View 갱신을 위해 초기화 시점에 이벤트 호출
        OnHpChanged?.Invoke(_currentHp, _maxHp);
    }

    /// <summary>
    /// amount만큼의 데미지를 입는 함수
    /// </summary>
    /// <param name="amount">가해진 데미지양</param>
    public void TakeDamage(int amount)
    {
        _currentHp -= amount;

        // 체력이 0 이하로 떨어지면 사망 처리
        if (_currentHp <= 0)
        {
            _currentHp = 0;
            Die();
        }

        Debug.Log($"{gameObject.name}이(가) {amount}만큼의 데미지를 입었습니다. 현재 체력: {_currentHp}/{_maxHp}");
        OnHpChanged?.Invoke(_currentHp, _maxHp);
    }

    /// <summary>
    /// amount만큼의 체력을 회복하는 함수
    /// </summary>
    /// <param name="amount">회복할 양</param>
    public void Heal(int amount)
    {
        _currentHp += amount;

        if (_currentHp > _maxHp)
            _currentHp = _maxHp;

        OnHpChanged?.Invoke(_currentHp, _maxHp);
        Debug.Log($"{gameObject.name}이(가) {amount}만큼 회복했습니다. 현재 체력: {_currentHp}/{_maxHp}");
    }

    /// <summary>
    /// 체력이 0이 되어 사망했을 때 호출되는 함수
    /// </summary>
    public void Die()
    {
        Debug.Log($"{gameObject.name} 사망");
        OnDead?.Invoke();
        _dieRoutine = StartCoroutine(DieRoutine());
    }

    /// <summary>
    /// 적이 사망했을 때 실행될 코루틴
    /// 애니메이션 등을 추가하여 구현 가능
    /// </summary>
    /// <returns></returns>
    IEnumerator DieRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        OnDead = null;
        Destroy(gameObject);
    }
}
