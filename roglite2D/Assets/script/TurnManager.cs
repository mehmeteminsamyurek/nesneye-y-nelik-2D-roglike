using UnityEngine;


public class TurnManager // level kontrol ediyor 
{
    public event System.Action OnTick;
    private int m_TurnCount;

    public TurnManager()
    {
        m_TurnCount = 1;
    }

    public void Tick()// tur geçişi eventi
    {
        OnTick?.Invoke();
        m_TurnCount += 1;
        
        
    }


}
