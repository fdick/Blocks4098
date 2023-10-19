using System.Collections;
using System.Collections.Generic;
using Code.Datas;
using Code.StateMachine;
using UnityEngine;

public class StartState : IState
{
    public StartState(SceneData sceneData)
    {
        _sceneData = sceneData;
    }

    private SceneData _sceneData;


    public void Enter()
    {
        //enable cubes starter
        _sceneData.CubesStarter.Activated = true;
    }

    public void Exit()
    {
    }
}