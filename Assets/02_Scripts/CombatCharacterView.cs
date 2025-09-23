using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatCharacterView : MonoBehaviour
{
    [Header("������Ʈ ����")]
    [SerializeField] TextMeshProUGUI _hpText; // ü�� �ؽ�Ʈ ������Ʈ
    [SerializeField] Image _hpBar; // ü�� �� �̹��� ������Ʈ

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