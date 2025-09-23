using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 영웅의 움직임 관련 UI를 관리하는 클래스
/// </summary>
public class PlaySceneView : MonoBehaviour
{
    [Header("컴포넌트 참조")]
    [SerializeField] GameObject _moveControllerPanel;
    [SerializeField] TextMeshProUGUI _remainingDiceCountText;
    [SerializeField] TextMeshProUGUI _stopText;
    [SerializeField] GameObject _clearPanel;
    [SerializeField] GameObject _deadPanel;
    [SerializeField] Button _clearRestartButton;
    [SerializeField] Button _clearExitButton;
    [SerializeField] Button _deadRestartButton;
    [SerializeField] Button _deadExitButton;
    [SerializeField] Button _heroTakeDamageButton;

    public GameObject MoveControllerPanel => _moveControllerPanel;

    public event Action OnHeroTake19Damage;

    public void Initialize()
    {
        _clearRestartButton.onClick.AddListener(() => SceneManager.LoadScene("02_Main"));
        _clearExitButton.onClick.AddListener(() => Application.Quit());
        _deadRestartButton.onClick.AddListener(() => SceneManager.LoadScene("02_Main"));
        _deadExitButton.onClick.AddListener(() => Application.Quit());
        _heroTakeDamageButton.onClick.AddListener(HeroTake19DamageOnClicked);

        _clearPanel.SetActive(false);
        _deadPanel.SetActive(false);
    }

    public void UpdateTextUI(int remainingCount)
    {
        SetRemainingDiceCountText(remainingCount);
        SetStopText(remainingCount);
    }

    /// <summary>
    /// 남은 주사위의 값에 따라 남은 주사위 값 텍스트를 업데이트
    /// </summary>
    /// <param name="remainingCount"></param>
    public void SetRemainingDiceCountText(int remainingCount)
    {
        _remainingDiceCountText.text = $"남은 주사위 값 : {remainingCount}";
    }

    /// <summary>
    /// 남은 주사위의 값에 따라 정지 버튼의 텍스트를 업데이트
    /// </summary>
    /// <param name="remainingCount">남은 주사위 값</param>
    public void SetStopText(int remainingCount)
    {
        _stopText.text = $"정지\n(체력 {remainingCount}회복)";
    }

    /// <summary>
    /// 게임 클리어 시 클리어 패널을 보여주는 함수
    /// </summary>
    public void ShowClearPanel()
    {
        _clearPanel.SetActive(true);
    }

    /// <summary>
    /// 게임 오버 시 사망 패널을 보여주는 함수
    /// </summary>
    public void ShowDeadPanel()
    {
        _deadPanel.SetActive(true);
    }

    /// <summary>
    /// 치트로 영웅이 19 데미지를 입도록 하는 함수
    /// </summary>
    public void HeroTake19DamageOnClicked()
    {
        OnHeroTake19Damage?.Invoke();
    }
}
