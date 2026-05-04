using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerSwordState : PlayerState
{
    public PlayerSwordState(Transform _playerTransform, Rigidbody _playerRigid, Animator _anime, GameObject _MainCam)
        : base(_playerTransform, _playerRigid, _anime, _MainCam)
    {
        PlayerData.playerData.OnSword = true;
    }
}
