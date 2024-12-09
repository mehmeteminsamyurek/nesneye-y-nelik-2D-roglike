using UnityEngine;

public class CellObject : MonoBehaviour //abstırack class etkileşimi nesneler
{
    protected Vector2Int m_Cell;

    public virtual void Init(Vector2Int cell)
    {
        m_Cell = cell;
    }

    public virtual void PlayerEntered()
    {
        
    }
    public virtual bool PlayerWantsToEnter() // karakter geçebilir
    {
        return true;
    }
}
