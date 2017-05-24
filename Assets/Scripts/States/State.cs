public class State
{
    protected AgentBehaviour _agent;

    public State (AgentBehaviour agent)
    {
        _agent = agent;
    }

    public virtual void Initialize () { }
    public virtual void Update () {  }
    public virtual void Dismiss () { }
}
