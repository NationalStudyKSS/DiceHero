using UnityEngine;

public class EnemyModel : CombatCharacterModel
{
    public override void Initialize()
    {
        // 적의 체력을 랜덤으로 설정
        _maxHp = Random.Range(1, 6); // 1~6 사이의 랜덤 값

        base.Initialize();
    }
}