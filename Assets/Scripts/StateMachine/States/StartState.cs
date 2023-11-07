using Code.Datas;
using Code.Services;
using Code.StateMachine;

public class StartState : IState
{
    public StartState(SceneData sceneData, AudioService audioService)
    {
        _sceneData = sceneData;
        _audio = audioService;
    }

    private SceneData _sceneData;
    private AudioService _audio;


    public void Enter()
    {
        //enable cubes starter
        _sceneData.CubesStarter.Activated = true;
        
        //play background music
        _audio.PlayMusic();
    }

    public void Exit()
    {
    }
}