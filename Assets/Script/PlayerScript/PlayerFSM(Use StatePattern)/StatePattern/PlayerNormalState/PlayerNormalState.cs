using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerNormalState : PlayerState
{
    public PlayerNormalState(Transform _playerTransform, Rigidbody _playerRigid, Animator _anime, GameObject _MainCam)
        : base(_playerTransform, _playerRigid, _anime, _MainCam)
    {
        PlayerData.playerData.OnSword = false;
    }
}
