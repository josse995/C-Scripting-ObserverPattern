using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour, IEndGameObserver
{
	#region Field Declarations

	[Header("UI Components")]
    [Space]
	public Text scoreText;
    public StatusText statusText;
    public Button restartButton;

    [Header("Ship Counter")]
    [SerializeField]
    [Space]
    private Image[] shipImages;

    private GameSceneController gameScene;

    #endregion

    #region Startup

    private void Awake()
    {
        statusText.gameObject.SetActive(false);
    }

    private void Start()
    {
        gameScene = FindObjectOfType<GameSceneController>();

        gameScene.AddObserver(this);

        gameScene.ScoreUpdatedOnKill += GameScene_ScoreUpdatedOnKill;
        gameScene.LifeLost += GameScene_LifeLost;
    }

    private void GameScene_LifeLost(int lives)
    {
        HideShip(lives);
    }

    private void GameScene_ScoreUpdatedOnKill(int pointValue)
    {
        UpdateScore(pointValue);
    }

    #endregion

    #region Public methods

    private void UpdateScore(int score)
    {
        scoreText.text = score.ToString("D5");
    }

    public void ShowStatus(string newStatus)
    {
        statusText.gameObject.SetActive(true);
        StartCoroutine(statusText.ChangeStatus(newStatus));
    }

    public void HideShip(int imageIndex)
    {
        shipImages[imageIndex].gameObject.SetActive(false);
    }

    public void ResetShips()
    {
        foreach (Image ship in shipImages)
            ship.gameObject.SetActive(true);
    }

    public void Notify()
    {
        ShowStatus("Game Over");
    }

    #endregion
}
