using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentHandler : MonoBehaviour
{
   [SerializeField] private MoveHandler[] _moveGroundHandlers;

   public void StartEnvMovement()
   {
      foreach (var mover in _moveGroundHandlers)
         mover.EnableMove = true;
   }

   public void StopEnvMovement()
   {
      foreach (var mover in _moveGroundHandlers)
         mover.EnableMove = false;
   }

 
}
