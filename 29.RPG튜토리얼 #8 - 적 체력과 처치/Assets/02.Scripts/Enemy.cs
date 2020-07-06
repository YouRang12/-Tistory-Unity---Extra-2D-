using System.Collections;
using System.Collections.Generic;
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
}
