using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAction : ScriptableObject
{
    public BattleEntity character;
    public BattleEntity skillTarget;
    public BaseSkill skillToUse;
    public float characterSpeed;
}
