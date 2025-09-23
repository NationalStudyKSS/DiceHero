using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 인트로 씬을 관리하는 클래스
/// </summary>
public class IntroScene : MonoBehaviour
{
    [Header("컴포넌트 참조")]
    [SerializeField] Button _startButton;
    [SerializeField] Button _exitButton;

    void Start()
    {
        // 버튼 클릭 이벤트에 함수 등록
        _startButton.onClick.AddListener(OnStartButtonClicked);
        _exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    /// <summary>
    /// 게임 시작 버튼 클릭 시 호출되는 함수
    /// </summary>
    void OnStartButtonClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("02_Main");
    }

    /// <summary>
    /// 게임 종료 버튼 클릭 시 호출되는 함수
    /// </summary>
    void OnExitButtonClicked()
    {
        Application.Quit();
    }
}
