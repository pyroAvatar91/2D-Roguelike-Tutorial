using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour {
    
    public static GameManager Instance { get ; private set; }
    public TurnManager TurnManager { get; private set; }
    
    public BoardManager BoardManager;
    public PlayerController PlayerController;
    public UIDocument UIDocument;

    private int foodAmount = 100;
    private Label foodLabel;
    

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    
    private void Start() {
        foodLabel = UIDocument.rootVisualElement.Q<Label>("FoodLabel");
        foodLabel.text = "Food: " + foodAmount;
        
        TurnManager = new TurnManager();
        TurnManager.OnTick += OnTurnHappen;
        
        BoardManager.Init();
        PlayerController.Spawn(BoardManager, new Vector2Int(1, 1));
    }

    private void OnTurnHappen() {
        const int onTurnFoodChange = -1;
        ChangeFood(onTurnFoodChange);
    }

    public void ChangeFood(int amount) {
        foodAmount += amount;
        foodLabel.text = "Food: " + foodAmount;
    }


}
