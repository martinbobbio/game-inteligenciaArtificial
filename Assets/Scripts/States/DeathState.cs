using UnityEngine;
using System.Collections;

public class DeathState : State
{
    public DeathState(AgentBehaviour agent) : base(agent) { }

    public override void Initialize()
    {
        base.Initialize();
        _agent.Die();
    }

    public override void Update()
    {
        base.Update();
        //Nothing to do...
    }

    public override void Dismiss()
    {
        base.Dismiss();
        //Nothing to do...
    }
}
