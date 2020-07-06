using UnityEngine;

public class Enemy : NPC
{
    [SerializeField]
    private CanvasGroup healthGroup;

    public override Transform Select()
    {
        // 체력바 보여주기
        healthGroup.alpha = 1;

        return base.Select();
    }


    public override void DeSelect()
    {
        // 체력바 숨기기
        healthGroup.alpha = 0;

        base.DeSelect();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        // NPC.cs 델리게이트에 등록된 함수를 호출
        OnHealthChanged(health.MyCurrentValue);
    }
}
