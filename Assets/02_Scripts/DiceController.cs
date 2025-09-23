using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 플레이어의 입력을 받아 주사위를 굴리는 클래스
/// </summary>
public class DiceController : MonoBehaviour
{
    [Header("컴포넌트 참조")]
    [SerializeField] Button _rollDiceButton;
    [SerializeField] GameObject _panel;

    [SerializeField] int _minValue = 1;
    [SerializeField] int _maxValue = 6;

    [Header("Read Only")]
    [SerializeField] int _resultValue;

    public event Action<int> OnRolled; // 주사위를 굴렸을 때 발생하는 이벤트

    public int ResultValue => _resultValue;     // 주사위 결과값 읽기 전용 프로퍼티

    public void Initialize()
    {
        _rollDiceButton.onClick.AddListener(OnClickedRollDice);
    }

    /// <summary>
    /// 주사위 굴림 버튼 클릭 시 호출되는 함수
    /// 주사위를 굴려 나온 값을 _resultValue에 저장하고 OnRolled 이벤트 발생
    /// </summary>
    public void OnClickedRollDice()
    {
        _resultValue = UnityEngine.Random.Range(_minValue, _maxValue + 1);
        Debug.Log($"주사위 굴림: {_resultValue}");

        OnRolled?.Invoke(_resultValue);
    }

    /// <summary>
    /// 주사위를 굴리는 패널을 이동 중일 때 숨기고(false), 이동이 멈췄을 때 다시 보이게(true) 토글하는 함수
    /// </summary>
    /// <param name="isMoving">이동 중인지 여부</param>
    public void TogglePanel(bool isMoving)
    {
        _panel.SetActive(!isMoving);
    }
}