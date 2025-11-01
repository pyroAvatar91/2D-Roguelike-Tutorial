using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {

    
    private BoardManager board;
    private Vector2Int playerPosition;


    public void Spawn(BoardManager boardManager, Vector2Int cell) {
        board = boardManager;
        MoveTo(cell);
    }

    public void MoveTo(Vector2Int cell) {
        playerPosition = cell;
        transform.position = board.CellToWorld(playerPosition);
    }
    
    private void Update() {
        Vector2Int newCellTarget = playerPosition;
        bool hasMoved = false;

        if (Keyboard.current.upArrowKey.wasPressedThisFrame) {
            newCellTarget.y += 1;
            hasMoved = true;
        }
        else if (Keyboard.current.downArrowKey.wasPressedThisFrame) {
            newCellTarget.y -= 1;
            hasMoved = true;
        }
        else if (Keyboard.current.leftArrowKey.wasPressedThisFrame) {
            newCellTarget.x -= 1;
            hasMoved = true;
        }
        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame) {
            newCellTarget.x += 1;
            hasMoved = true;
        }

        if (hasMoved) {
            BoardManager.CellData cellData = board.GetCellData(newCellTarget); //check if new position is passable and move there

            if (cellData != null && cellData.Passable) {
                GameManager.Instance.TurnManager.Tick();
                MoveTo(newCellTarget);

                if (cellData.ContainedObject != null) {
                    cellData.ContainedObject.PlayerEntered();
                }
            }
        }
        
    }


    
}
