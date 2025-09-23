using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 발판을 생성하는 클래스
/// </summary>
public class PlatformSpawner : MonoBehaviour
{
    [Header("컴포넌트 참조")]
    [SerializeField] GameObject _platformPrefab; // 생성할 발판 프리팹

    [SerializeField] int _count = 30; // 생성할 발판의 개수

    public void Initialize()
    {
        // count 개수만큼 발판 생성
        CreatePlatforms(_count);
    }

    /// <summary>
    /// count 개수만큼 발판을 생성하는 함수
    /// </summary>
    /// <param name="count">발판을 생성할 개수</param>
    public void CreatePlatforms(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 position = new Vector3(0, 0, i * 1.0f);

            GameObject platform = Instantiate(_platformPrefab, position, Quaternion.identity, transform);

            if (i == count - 1) // 마지막 발판일 때
            {
                platform.name = "LastPlatform";
                platform.GetComponent<Renderer>().material.color = Color.green;
                platform.tag = "Goal";
            }
        }
    }
}
