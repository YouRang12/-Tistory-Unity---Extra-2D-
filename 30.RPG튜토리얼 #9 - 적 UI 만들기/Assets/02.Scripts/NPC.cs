using UnityEngine;

// 델리게이트 => 콜백함수 용도로 사용
public delegate void HealthChanged(float health);

public delegate void CharacterRemoved();

public class NPC : Character
{
    // event로 델리게이트를 담아놓음(안전함)
    public event HealthChanged healthChanged;
    public event CharacterRemoved characterRemoved;

    [SerializeField]
    private Sprite Portrait;

    public Sprite MyPortrait
    {
        get
        {
            return Portrait;
        }
    }

    public virtual void DeSelect()
    {
        healthChanged -= new HealthChanged(UIManager.MyInstance.UpdateTargetFrame);
        characterRemoved -= new CharacterRemoved(UIManager.MyInstance.HideTargetFrame);
    }

    public virtual Transform Select()
    {
        return hitBox;
    }

    public void OnHealthChanged(float health)
    {
        if (healthChanged != null)
        {
            healthChanged(health);
        }
    }

    // 사망시 타겟의 오브젝트가 삭제됨
    public void OnCharacterRemoved()
    {
        if (characterRemoved != null)
        {
            characterRemoved();
        }

        Destroy(this.gameObject);
    }
}
