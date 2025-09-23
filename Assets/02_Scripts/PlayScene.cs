using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PlayScene을 관리하는 클래스
/// </summary>
public class PlayScene : MonoBehaviour
{
    [Header("컴포넌트 참조")]
    [SerializeField] Hero _hero;
    [SerializeField] PlatformSpawner _platformSpawner;
    [SerializeField] EnemySpanwer _enemySpawner;
    [SerializeField] DiceController _diceController;
    [SerializeField] BattleController _battleController;
    [SerializeField] PlaySceneView _view;

    [SerializeField] GameObject _mainView; // 메인 뷰 (영웅 이동 및 주사위 굴림 UI 포함)

    [Header("Read Only")]
    [SerializeField] bool _isMoving = false; // 플레이어가 이동 중인지 여부
    [SerializeField] int _remainingMoveCount; // 남은 이동 횟수

    public event Action OnMoveFinished; // 이동이 끝났을 때 발생하는 이벤트

    void Start()
    {
        Initialize();
        _diceController.TogglePanel(_isMoving);

        _hero.OnEnemyEncountered += EnemyDetected;
        _hero.OnGoalReached += GoalReached;
        _hero.HeroModel.OnDead += _view.ShowDeadPanel;
        _diceController.OnRolled += OnDiceRolled;
        _battleController.OnBattleEnded += BattleEnded;
        _view.OnHeroTake19Damage += HeroTake19Damage;

        _hero.Initialize();
        _platformSpawner.Initialize();
        _enemySpawner.Initialize();
        _diceController.Initialize();
        _battleController.Initialize();
        _view.Initialize();
    }

    /// <summary>
    /// 세팅 초기화 함수
    /// </summary>
    void Initialize()
    {
        MoveFinished();
    }

    /// <summary>
    /// 전진 버튼 클릭 시 호출되는 함수
    /// </summary>
    public void OnProceedButtonClicked()
    {
        _hero.Move();
        _remainingMoveCount--;
        _view.UpdateTextUI(_remainingMoveCount);

        if (_remainingMoveCount <= 0)
        {
            MoveFinished();
        }

        _hero.CheckGoal();
        _hero.CheckEnemy();
    }

    /// <summary>
    /// 정지 버튼 클릭 시 호출되는 함수
    /// </summary>
    public void OnStopButtonCliced()
    {
        _hero.HeroModel.Heal(_remainingMoveCount);

        MoveFinished();
    }

    /// <summary>
    /// 주사위가 굴려졌을 때 호출되는 함수
    /// </summary>
    /// <param name="count">이동할 횟수</param>
    void OnDiceRolled(int count)
    {
        _isMoving = true;
        _remainingMoveCount = count;
        _diceController.TogglePanel(_isMoving);

        _view.MoveControllerPanel.gameObject.SetActive(_isMoving);
        _view.UpdateTextUI(_remainingMoveCount);
    }

    /// <summary>
    /// 영웅의 이동을 끝낼때마다 호출되는 함수
    /// </summary>
    void MoveFinished()
    {
        _isMoving = false;
        _remainingMoveCount = 0;
        _view.MoveControllerPanel.gameObject.SetActive(_isMoving);
        _diceController.TogglePanel(_isMoving);

        _view.UpdateTextUI(_remainingMoveCount);

        OnMoveFinished?.Invoke();
    }

    /// <summary>
    /// 적을 감지했을 때 호출되는 함수
    /// </summary>
    void EnemyDetected(Enemy enemy)
    {
        _mainView.SetActive(false);

        _battleController.StartBattle(_hero, enemy);
    }

    /// <summary>
    /// 적과의 전투가 끝났을 때 호출되는 함수
    /// </summary>
    void BattleEnded()
    {
        _mainView.SetActive(true);
    }

    /// <summary>
    /// Goal에 도달했을 때 호출되는 함수
    /// </summary>
    void GoalReached()
    {
        _view.ShowClearPanel();
    }

    /// <summary>
    /// 테스트 용으로 영웅에게 19 데미지를 입히는 함수
    /// </summary>
    void HeroTake19Damage()
    {
        _hero.TakeDamage(19);
    }
}