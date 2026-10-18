using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum GameState { Playing, GameOver, Won }
    private GameState _currentState = GameState.Playing;

    public void PlayerCaught()
    {
        if (_currentState == GameState.Playing)
        {
            Debug.Log("Game Over");
            _currentState = GameState.GameOver;
        }
    }
}
