using UnityEngine;
using UnityEngine.InputSystem;
using static BoardManager;

public class PlayerController : MonoBehaviour // hareket ediyor ve etkileşimleri kontrol ediyor
{
    private BoardManager m_Board;
    private Vector2Int m_CellPosition;
    private CellData CellData;
    private bool m_IsGameOver;
    public  Vector2Int Cell;

    public void Spawn(BoardManager boardManager, Vector2Int cell)
    {
        m_Board = boardManager;
        MoveTo(cell);
    }
    public void Init() 
    {
        m_IsGameOver = false;
    }

    public void MoveTo(Vector2Int cell)
    {
        
        m_CellPosition = cell;
        transform.position = m_Board.CellToWorld(m_CellPosition);
    }
    public void GameOver()
    {
        m_IsGameOver = true;
    }

    private void Update()
    {
        if (m_IsGameOver)
        {
            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                GameManager.Instance.StartNewGame();
                m_IsGameOver = false;
            }

            return;
        }
        Cell = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));

        Vector2Int newCellTarget = m_CellPosition;
        bool hasMoved = false;

        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            newCellTarget.y += 1;
            hasMoved = true;
        }
        else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            newCellTarget.y -= 1;
            hasMoved = true;
        }
        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            newCellTarget.x += 1;
            hasMoved = true;
        }
        else if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            newCellTarget.x -= 1;
            hasMoved = true;
        }

        if (hasMoved) // hareket etme koşulnun olduğu yer ve sonrasında etkileşimleri kontrol ediyor
        {
            GameManager.Instance.TurnManager.Tick();
            var cellData = m_Board.GetCellData(newCellTarget);

            if (cellData != null && cellData.Passable)
            {
                MoveTo(newCellTarget);
                if (cellData.ContainedObject != null && cellData.ContainedObject.PlayerWantsToEnter())
                {
                    cellData.ContainedObject.PlayerEntered();
                }
                
            }
            else if(cellData != null && !cellData.Passable)
            {                               
                    cellData.ContainedObject.PlayerWantsToEnter();
                    if (cellData.ContainedObject.PlayerWantsToEnter())
                    {
                        cellData.Passable = true;
                    }                                
            }
        }
        
    }

}