using UnityEngine;

public abstract class State : ScriptableObject
{
    public bool IsFinished { get; protected set; }
    [HideInInspector] public Enemy Enemy;
    
    public virtual void Init() { }

    public abstract void Do();
}
