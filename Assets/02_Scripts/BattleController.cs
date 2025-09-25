using System;
using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// 적과의 전투를 관리하는 클래스
/// </summary>
public class BattleController : MonoBehaviour
{
    [Header("컴포넌트 참조")]
    [SerializeField] DiceController _diceController; // 주사위 컨트롤러
    [SerializeField] TextMeshProUGUI[] _enemyEncounterTexts; // 적 조우 텍스트들
    [SerializeField] GameObject _diceControllerPanel; // 주사위 컨트롤러 패널
    [SerializeField] TextMeshProUGUI _damageText; // 데미지 텍스트

    [Header("Settings")]
    [SerializeField] float _textDisplayDuration = 0.5f; // 각 텍스트가 표시되는 시간

    Hero _hero;
    Enemy _targetEnemy;
    Enemy _prevEnemy;

    Coroutine _hitAndTakeDamageRoutine;

    public event Action OnBattleEnded; // 전투가 종료되었을 때 발생하는 이벤트

    public void Initialize()
    {
        _diceController.Initialize();
    }

    /// <summary>
    /// 적 조우 시 호출되는 전투시작 함수
    /// _targetEnemy에 적 정보 저장 후 전투 시작
    /// </summary>
    /// <param name="enemy">조우한 적</param>
    public void StartBattle(Hero hero, Enemy enemy)
    {
        // 이전 Enemy 이벤트 해제
        if (_prevEnemy != null && _prevEnemy.EnemyModel != null)
            _prevEnemy.EnemyModel.OnDead -= EndBattle;

        _diceController.OnRolled -= DiceRolled;
        _diceController.OnRolled += DiceRolled;

        _hero = hero;
        _targetEnemy = enemy;

        // 새 적 이벤트 구독
        _targetEnemy.EnemyModel.OnDead += EndBattle;

        // 적 참조 갱신
        _prevEnemy = _targetEnemy;

        Debug.Log($"적 {enemy.name}과 전투 시작!");
        StartCoroutine(EnemyEncounterTextRoutine());
    }

    /// <summary>
    /// 적을 조우했을 때 텍스트를 순차적으로 보여주는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator EnemyEncounterTextRoutine()
    {
        // 4개의 텍스트가 순차적으로 나타났다 사라지는 효과
        foreach (var text in _enemyEncounterTexts)
        {
            text.gameObject.SetActive(true);
            yield return new WaitForSeconds(_textDisplayDuration);
        }

        // 모든 텍스트가 표시된 후 잠시 대기
        yield return new WaitForSeconds(1.0f);

        // 텍스트들을 모두 비활성화
        foreach (var text in _enemyEncounterTexts)
        {
            text.gameObject.SetActive(false);
        }

        // 적 조우 텍스트가 모두 사라진 후 주사위 컨트롤러 패널 활성화
        _diceControllerPanel.SetActive(true);
        _damageText.gameObject.SetActive(true);
    }

    /// <summary>
    /// 주사위를 굴렸을 때 호출되는 함수
    /// </summary>
    /// <param name="resultValue">주사위 값</param>
    public void DiceRolled(int resultValue)
    {
        // 주사위를 중복으로 굴리는 것을 방지하기 위해 공격 중에는 주사위 패널 비활성화
        _diceControllerPanel.SetActive(false);

        Debug.Log($"적 {_targetEnemy.name}에게 {resultValue}의 피해를 입혔습니다.");
        
        UpdateDamageText(resultValue);

        _hitAndTakeDamageRoutine = StartCoroutine(HitAndTakeDamageRoutine(resultValue, _targetEnemy.EnemyModel.Damage));
    }

    IEnumerator HitAndTakeDamageRoutine(int damageToEnemy, int damageToPlayer)
    {
        yield return new WaitForSeconds(0.5f);
        _targetEnemy.TakeDamage(damageToEnemy);

        // 적이 사망했으면 플레이어가 피해를 입지 않음
        if (_targetEnemy == null) yield break;

        yield return new WaitForSeconds(0.5f);
        _hero.TakeDamage(damageToPlayer);

        // 전투가 끝나지 않았다면 다시 주사위 패널 활성화
        if (_targetEnemy != null)
            _diceControllerPanel.SetActive(true);
    }

    /// <summary>
    /// 전투가 종료되었을 때 호출되는 함수
    /// </summary>
    public void EndBattle()
    {
        _diceController.OnRolled -= DiceRolled;
        _diceControllerPanel.SetActive(false);
        _damageText.gameObject.SetActive(false);

        Debug.Log($"적 {_targetEnemy.name}과의 전투가 종료되었습니다.");

        StopAllCoroutines();
        _targetEnemy = null;
        // _prevEnemy는 다음 전투 시작에서 해제
        OnBattleEnded?.Invoke();
    }

    public void UpdateDamageText(int diceValue)
    {
        if (_targetEnemy != null)
        {
            _damageText.text = $"이번 공격 데미지 : {diceValue}";
        }
    }
}