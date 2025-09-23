using System;
using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// ������ ������ �����ϴ� Ŭ����
/// </summary>
public class BattleController : MonoBehaviour
{
    [Header("������Ʈ ����")]
    [SerializeField] DiceController _diceController; // �ֻ��� ��Ʈ�ѷ�
    [SerializeField] TextMeshProUGUI[] _enemyEncounterTexts; // �� ���� �ؽ�Ʈ��
    [SerializeField] GameObject _diceControllerPanel; // �ֻ��� ��Ʈ�ѷ� �г�
    [SerializeField] TextMeshProUGUI _damageText; // ������ �ؽ�Ʈ

    [Header("Settings")]
    [SerializeField] float _textDisplayDuration = 0.5f; // �� �ؽ�Ʈ�� ǥ�õǴ� �ð�

    Hero _hero;
    Enemy _targetEnemy;
    Enemy _prevEnemy;

    Coroutine _hitAndTakeDamageRoutine;

    public event Action OnBattleEnded; // ������ ����Ǿ��� �� �߻��ϴ� �̺�Ʈ

    public void Initialize()
    {
        _diceController.Initialize();
    }

    /// <summary>
    /// �� ���� �� ȣ��Ǵ� �������� �Լ�
    /// _targetEnemy�� �� ���� ���� �� ���� ����
    /// </summary>
    /// <param name="enemy">������ ��</param>
    public void StartBattle(Hero hero, Enemy enemy)
    {
        // ���� Enemy �̺�Ʈ ����
        if (_prevEnemy != null && _prevEnemy.EnemyModel != null)
            _prevEnemy.EnemyModel.OnDead -= EndBattle;

        _diceController.OnRolled -= DiceRolled;
        _diceController.OnRolled += DiceRolled;

        _hero = hero;
        _targetEnemy = enemy;

        // �� �� �̺�Ʈ ����
        _targetEnemy.EnemyModel.OnDead += EndBattle;

        // �� ���� ����
        _prevEnemy = _targetEnemy;

        Debug.Log($"�� {enemy.name}�� ���� ����!");
        StartCoroutine(EnemyEncounterTextRoutine());
    }

    /// <summary>
    /// ���� �������� �� �ؽ�Ʈ�� ���������� �����ִ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator EnemyEncounterTextRoutine()
    {
        // 4���� �ؽ�Ʈ�� ���������� ��Ÿ���� ������� ȿ��
        foreach (var text in _enemyEncounterTexts)
        {
            text.gameObject.SetActive(true);
            yield return new WaitForSeconds(_textDisplayDuration);
        }

        // ��� �ؽ�Ʈ�� ǥ�õ� �� ��� ���
        yield return new WaitForSeconds(1.0f);

        // �ؽ�Ʈ���� ��� ��Ȱ��ȭ
        foreach (var text in _enemyEncounterTexts)
        {
            text.gameObject.SetActive(false);
        }

        // �� ���� �ؽ�Ʈ�� ��� ����� �� �ֻ��� ��Ʈ�ѷ� �г� Ȱ��ȭ
        _diceControllerPanel.SetActive(true);
        _damageText.gameObject.SetActive(true);
    }

    /// <summary>
    /// �ֻ����� ������ �� ȣ��Ǵ� �Լ�
    /// </summary>
    /// <param name="resultValue">�ֻ��� ��</param>
    public void DiceRolled(int resultValue)
    {
        // �ֻ����� �ߺ����� ������ ���� �����ϱ� ���� ���� �߿��� �ֻ��� �г� ��Ȱ��ȭ
        _diceControllerPanel.SetActive(false);

        Debug.Log($"�� {_targetEnemy.name}���� {resultValue}�� ���ظ� �������ϴ�.");
        
        UpdateDamageText(resultValue);

        _hitAndTakeDamageRoutine = StartCoroutine(HitAndTakeDamageRoutine(resultValue, _targetEnemy.EnemyModel.Damage));
    }

    IEnumerator HitAndTakeDamageRoutine(int damageToEnemy, int damageToPlayer)
    {
        yield return new WaitForSeconds(0.5f);
        _targetEnemy.TakeDamage(damageToEnemy);

        // ���� ��������� �÷��̾ ���ظ� ���� ����
        if (_targetEnemy == null) yield break;

        yield return new WaitForSeconds(0.5f);
        _hero.TakeDamage(damageToPlayer);

        // ������ ������ �ʾҴٸ� �ٽ� �ֻ��� �г� Ȱ��ȭ
        if (_targetEnemy != null)
            _diceControllerPanel.SetActive(true);
    }

    /// <summary>
    /// ������ ����Ǿ��� �� ȣ��Ǵ� �Լ�
    /// </summary>
    public void EndBattle()
    {
        _diceController.OnRolled -= DiceRolled;
        _diceControllerPanel.SetActive(false);
        _damageText.gameObject.SetActive(false);

        Debug.Log($"�� {_targetEnemy.name}���� ������ ����Ǿ����ϴ�.");

        StopAllCoroutines();
        _targetEnemy = null;
        // _prevEnemy�� ���� ���� ���ۿ��� ����
        OnBattleEnded?.Invoke();
    }

    public void UpdateDamageText(int diceValue)
    {
        if (_targetEnemy != null)
        {
            _damageText.text = $"�̹� ���� ������ : {diceValue}";
        }
    }
}