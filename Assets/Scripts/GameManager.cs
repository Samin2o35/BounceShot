using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public float startingTime = 30f;

    [HideInInspector] public float currentTime;

    public static UnityEvent onGameStart = new UnityEvent();
    public static UnityEvent onGameEnd = new UnityEvent();

    private bool gameActive = false;
    
    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (!gameActive) return;

        currentTime -= Time.deltaTime;

        if(currentTime <= 0f)
        {
            currentTime = 0f;
            EndGame();
        }
    }

    public void StartGame()
    {
        currentTime = startingTime;
        gameActive = true;
        onGameStart.Invoke();
    }

    public void EndGame()
    {
        gameActive = false;
        onGameEnd.Invoke();
    }

    // call to increase time when enemy killed
    public void AddTime(float amount)
    {
        currentTime += amount;
    }
}
