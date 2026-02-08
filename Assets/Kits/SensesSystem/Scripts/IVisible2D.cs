using UnityEngine;

public interface IVisible2D 
{
    enum Side{
        Friendly,
        Enemy,
        Neutral
    }
    public int GetPriority();
    public Side GetSide();
}
