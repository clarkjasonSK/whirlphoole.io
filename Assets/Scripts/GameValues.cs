using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameValue", menuName = "ScriptableObjects/GameValues")]
public class GameValues : ScriptableObject
{
    #region Game Values

    #region Player Values

    [SerializeField] [Range(0.1f, 2.0f)] public float PlayerCursorOffset;
    [SerializeField] [Range(0.1f, 20f)] public float PlayerBaseSpeed;
    // USAGE: movespeed = PlayerBaseSpeed - (_player_level * PlayerSpeedDecreaseMultiplier)
    [SerializeField] [Range(0.1f, 1.0f)] public float PlayerSpeedDecreaseMultiplier;
    [SerializeField] [Range(0.1f, 1.0f)] public float PlayerSpeedDecreaseFloor;

    #endregion

    #region Hole Values
    // USAGE: replaces hole transform.localScale;
    [SerializeField] [Range(0.1f, 1.5f)] public float HoleBaseSize;
    [SerializeField] [Range(0.1f, 10f)] public float HoleGrowSpeed;

    // USAGE: size += _hole_level* HoleBaseSize*HoleSizeMultiplier
    [SerializeField][Range(0.1f, 1.5f)] public float HoleSizeMultiplier;

    [SerializeField] [Range(1f, 10f)] public float HolePullStrength;
    [SerializeField] [Range(10f, 100f)] public float HoleWhirlStrength;
    [SerializeField] [Range(1f, 30f)] public float HoleAbsorbStrength;
    [SerializeField] [Range(1, 100)] public int HoleExpThreshold;

    //USAGE: HoleExpThreshold  + (HoleExpThreshold *(_hole_level -1)* HoleExpThresholdMultiplier)
    [SerializeField][Range(0.5f, 1.5f)] public float HoleExpThresholdMultiplier;

    [SerializeField] [Range(1, 5)] public int HoleAbsorbDifference;

    // the multiplier to be multiplied to the exp points from a hole absorbed by a bigger hole
    [SerializeField] [Range(0.1f, 1.5f)] public float HoleCannibalExpMultiplier;

    #endregion

    #region Prop Fields
    [SerializeField] [Range(0.1f, 2f)] public float PropSpawnRate;
    [SerializeField] [Range(0, 100)] public int PropSpawnSizeFloor;
    [SerializeField] [Range(-1, 100)] public int PropSpawnSizeCeiling;

    [SerializeField] [Range(1, 50)] public int PropsPointsBase;
    [SerializeField] [Range(1, 20)] public int PropsPointsMultiplier;

    // USAGE: replaces prop transform.localScale;
    [SerializeField] [Range(0.1f, 2f)] public float PropsScaleBase;
    [SerializeField] [Range(0.01f, 1.0f)] public float PropScaleDespawn;
    // USAGE: _prop_size += _prop_size* PropsScaleBase* PropsScaleMultiplier
    [SerializeField] [Range(0.1f, 1.5f)] public float PropsScaleMultiplier;

    [SerializeField] [Range(0.5f, 2.0f)] public float PropAnchorAim;
    #endregion


    #endregion
}
