using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 적을 랜덤한 발판위에 생성하는 Spawner 클래스
/// </summary>
public class EnemySpanwer : MonoBehaviour
{
    [Header("컴포넌트 참조")]
    [SerializeField] GameObject _enemyPrefab; // 생성할 적 프리팹

    [Header("설정값")]
    [SerializeField] int _enemyCount = 5; // 생성할 적의 수
    [SerializeField] int _minPlatformIndex = 1; // 적이 생성될 수 있는 최소 발판 인덱스
    [SerializeField] int _maxPlatformIndex = 29; // 적이 생성될 수 있는 최대 발판 인덱스

    public void Initialize()
    {
        SpawnEnemy();
    }

    /// <summary>
    /// 적을 생성하는 함수
    /// 적을 생성한 후에 초기화 함수를 호출
    /// </summary>
    void SpawnEnemy()
    {
        // 정해진 범위 내에서 리스트 만들기
        List<int> numbers = Enumerable.Range(_minPlatformIndex, _maxPlatformIndex - _minPlatformIndex).ToList();

        // 무작위로 _nemyCount 개수만큼 선택
        List<int> fiveNumbers = numbers.OrderBy(x => Random.value).Take(_enemyCount).ToList();

        Debug.Log(string.Join(", ", fiveNumbers));

        foreach (int index in fiveNumbers)
        {
            Vector3 position = new Vector3(1f, 1f, index * 1.0f);
            GameObject enemyObj = Instantiate(_enemyPrefab, position, Quaternion.identity, transform);
            enemyObj.name = $"Enemy_{index}";
            Enemy enemy = enemyObj.GetComponent<Enemy>();
            enemy.Initialize();
        }
    }
}
