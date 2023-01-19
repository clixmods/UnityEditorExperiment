using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerObjec : MonoBehaviour
{
    private Vector2 _moveValue;

    #region Unity Event

    public void OnJump()
    {
        throw new NotImplementedException();
    }
    public void OnMoved(Vector2 value)
    {
        _moveValue = value;
    }
    public void PlayProutFx(Vector2 value)
    {
        Debug.Log("Play Prout FX");
    }

    #endregion
    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3) _moveValue * Time.deltaTime;
    }
}