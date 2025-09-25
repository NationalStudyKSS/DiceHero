using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatCharacterView : MonoBehaviour
{
    [Header("컴포넌트 참조")]
    [SerializeField] TextMeshProUGUI _hpText; // 체력 텍스트 컴포넌트
    [SerializeField] Image _hpBar; // 체력 바 이미지 컴포넌트

    public void Initialize()
    {

    }

    public void UpdateView(float currentHp, float maxHp)
    {
        UpdateHpText(currentHp, maxHp);
        UpdateHpBar(currentHp, maxHp);
    }

    void UpdateHpText(float currentHp, float maxHp)
    {
        _hpText.text = $"HP: {currentHp}/{maxHp}";
    }

    void UpdateHpBar(float currentHp, float maxHp)
    {
        _hpBar.fillAmount = currentHp / maxHp;
    }
}