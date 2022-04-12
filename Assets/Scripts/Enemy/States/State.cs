using UnityEngine;

public abstract class State : ScriptableObject
{
    public bool IsFinished { get; protected set; }
    [HideInInspector] public Infected Infected;
    
    public virtual void Init() { }

    public abstract void Do();
}
