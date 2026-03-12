using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float startingTime = 30f;
    public Text timerText;

    [HideInInspector] public float currentTime;

    public static UnityEvent onGameStart = new UnityEvent();
    public static UnityEvent onGameEnd = new UnityEvent();

    private bool gameActive = false;

    void Start()
    {
        StartGame();
    }

    void OnDestroy()
    {
        onGameStart.RemoveAllListeners();
        onGameEnd.RemoveAllListeners();
    }

    void Update()
    {
        if (!gameActive) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            EndGame();
        }

        if (timerText != null)
            timerText.text = Mathf.CeilToInt(currentTime).ToString();
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
        if (timerText != null)
            timerText.text = "0";
        onGameEnd.Invoke();
    }

    public void AddTime(float amount)
    {
        currentTime += amount;
    }
}