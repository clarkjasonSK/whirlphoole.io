using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXHandler : Singleton<VFXHandler>, ISingleton, IPoolHandler, IEventObserver
{
    #region ISingleton Variables
    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion

    [SerializeField] private VFXObjectPool _vfx_op;
    public VFXObjectPool VFXObjectPool
    {
        set { _vfx_op = value; }
    }

    #region Cache Params
    private PointsVFX pointsRef;
    private Prop absorbedPropRef;
    private Hole absorbedHoleRef;
    private Player playerRef;
    #endregion
    public void Initialize()
    {
        if (_vfx_op == null)
            _vfx_op = GetComponent<VFXObjectPool>();
        AddEventObservers();

        isDone = true;
    }
    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.PROP_ABSORBED, onPropAbsorbed);
        EventBroadcaster.Instance.AddObserver(EventKeys.HOLE_ABSORBED, onHoleAbsorbed);

        EventBroadcaster.Instance.AddObserver(EventKeys.HOLE_LEVEL_UP, OnHoleLevelUp);
    }
    private void onPropAbsorbed(EventParameters param)
    {
        //Debug.Log("Hole absorbed for VFX ");
        absorbedPropRef = param.GetParameter<Prop>(EventParamKeys.PROP_PARAM, null);
        pointsRef = _vfx_op.getPointsVFX().GetComponent<PointsVFX>();

        pointsRef.PointsText = "+ " + absorbedPropRef.PropPoints;
        pointsRef.transform.localPosition = absorbedPropRef.transform.localPosition;

    }

    private void onHoleAbsorbed(EventParameters param)
    {
        // GETTING HOLE_PARAM_2, THE ONE THAT WAS ABSORBED, 
        absorbedHoleRef = param.GetParameter<Hole>(EventParamKeys.HOLE_PARAM_2, null);

        pointsRef = _vfx_op.getPointsVFX().GetComponent<PointsVFX>();

        pointsRef.PointsText = "+ " + absorbedHoleRef.HoleExperience;

        pointsRef.transform.localPosition = absorbedHoleRef.transform.localPosition;
    }
    private void OnHoleLevelUp(EventParameters param)
    {
        playerRef = param.GetParameter<Player>(EventParamKeys.PLAYER_PARAM, null);

        playerRef.UpdateLevelScale();
    }

    public void DeactivateObject(GameObject obj)
    {
        _vfx_op.GameObjectPool.Release(obj);
    }

}
