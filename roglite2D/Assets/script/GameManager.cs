using UnityEngine;
using UnityEngine.UIElements;
public class GameManager : MonoBehaviour // oyunu kontrol ediyor ve bazende class arası ilişki kurduruyor
{
    private VisualElement m_GameOverPanel;
    private Label m_GameOverMessage;

    public static GameManager Instance { get; private set; }
    public int m_FoodAmount = 100;
    public BoardManager BoardManager;
    public PlayerController PlayerController;
    public UIDocument UIDoc;
    private Label m_FoodLabel;
    private int m_CurrentLevel = 1;
    public TurnManager TurnManager { get; private set; }

    private void Awake() 
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        m_GameOverPanel = UIDoc.rootVisualElement.Q<VisualElement>("GameOverPanel");
        m_GameOverMessage = m_GameOverPanel.Q<Label>("GameOverMessage");

        m_GameOverPanel.style.visibility = Visibility.Hidden;
        TurnManager = new TurnManager();
        TurnManager.OnTick += OnTurnHappen;

        NewLevel();

        m_FoodLabel = UIDoc.rootVisualElement.Q<Label>("FoodLabel");
        m_FoodLabel.text = "Food : " + m_FoodAmount;

       
        StartNewGame();
    }

    void OnTurnHappen() // hareket edince yemek azalması
    {
        ChangeFood(-1);
    }
    public void ChangeFood(int amount) // açlığın deyişiğmi
    {
        m_FoodAmount += amount;
        m_FoodLabel.text = "Food : " + m_FoodAmount;
        if (m_FoodAmount <= 0)
        {
            PlayerController.GameOver();
            m_GameOverPanel.style.visibility = Visibility.Visible;
            m_GameOverMessage.text = "Game Over!\n\nYou traveled through " + m_CurrentLevel + " levels";

        }
    }
    public void NewLevel() 
    {
        BoardManager.Clean();
        BoardManager.Init();
        PlayerController.Spawn(BoardManager, new Vector2Int(1, 1));

        m_CurrentLevel++;
    }
    public void StartNewGame() 
    {
        m_GameOverPanel.style.visibility = Visibility.Hidden;

        m_CurrentLevel = 1;
        m_FoodAmount = 20;
        m_FoodLabel.text = "Food : " + m_FoodAmount;

        BoardManager.Clean();
        BoardManager.Init();

        //PlayerController.Init();
        PlayerController.Spawn(BoardManager, new Vector2Int(1, 1));
    }


}