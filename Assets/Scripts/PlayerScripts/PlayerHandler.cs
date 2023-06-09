using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : Singleton<PlayerHandler>, ISingleton, IEventObserver
{
    #region ISingleton Variables
    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion

    //[SerializeField] private Transform _player_spawn_transform;
    //[SerializeField] private Hole _player_hole_template;


    [SerializeField] private PlayerStaff _player_staff;
    public PlayerStaff PlayerStaff
    {
        get { return _player_staff; }
        set { _player_staff = value; }
    }

    [SerializeField] private Hole _target_hole;
    public Hole GetTargetHole
    {
        get { return _target_hole; }
    }

    #region Cache Variables
    private Player playerRef;
    private Hole holeRef;
    #endregion

    public void Initialize()
    {
        switch (GameManager.Instance.GameMode)
        {
            case GameMode.NONE_SELECTED:
                Debug.Log("Game Not Initialized");
                break;
            case GameMode.SINGLE_PLAYER:
                initializePlayers(1);
                break;
            case GameMode.TWO_PLAYER:
                initializePlayers(2);
                break;
        }

        AddEventObservers();

        isDone = true;
    }

    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.HOLE_LEVEL_UP, OnHoleLevelUp);
        EventBroadcaster.Instance.AddObserver(EventKeys.PLAYER_SO_UPDATE, OnSOUpdate);
    }


    private void initializePlayers(int playerAmount)
    {
        for(int i=0; i < playerAmount; i++)
        {
            spawnPlayerHole(ScriptableObjectsHelper.GetScriptableObject<PlayerScriptableObject>(FileNames.PLAYER_SO + "" + (i + 1)));
        }
    }

    private void spawnPlayerHole(PlayerScriptableObject playerSO)
    {
        holeRef = GameObject.Instantiate(_player_staff.PlayerHoleTemplate, _player_staff.PlayerSpawnTransform);
        holeRef.gameObject.SetActive(true);

        playerSO.ResetValues(holeRef.HoleNxtLvl);
        holeRef.PlayerHole.InitializePlayer(playerSO);
        UIManager.Instance.SetPlayerUI(holeRef.PlayerHole.PlayerID, CameraHandler.Instance.GetCameraForUI(holeRef.PlayerHole.PlayerID));

        holeRef.transform.localPosition = _player_staff.PlayerSpawners[Random.Range(0, _player_staff.PlayerSpawners.Count)].transform.localPosition;

        // only used in single player 
        _target_hole = holeRef;
    }

    #region Event Broadcaster Notifications
    private void OnSOUpdate(EventParameters param)
    {
        playerRef = param.GetParameter<Player>(EventParamKeys.PLAYER_PARAM, null);
        holeRef = param.GetParameter<Hole>(EventParamKeys.HOLE_PARAM, null);
        if (playerRef != null && holeRef != null)
        {
            playerRef.UpdateScores(holeRef.HoleExperience, holeRef.HoleNxtLvl);
        }
    }

    private void OnHoleLevelUp(EventParameters param)
    {
        //Debug.Log("levelling up");
        playerRef = param.GetParameter<Player>(EventParamKeys.PLAYER_PARAM, null);

        if (playerRef.PlayerHoleLevel > GameManager.Instance.CurrentBiggestHole)
        {
            GameManager.Instance.CurrentBiggestHole = playerRef.PlayerHoleLevel;
        }
    }

    #endregion
}
