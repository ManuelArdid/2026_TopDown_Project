using UnityEngine;

public class Coin : MonoBehaviour, IVisible2D
{
    public int GetPriority()
    {
        return 0;
    }

    public IVisible2D.Side GetSide()
    {
        return IVisible2D.Side.Neutral;
    }
}
