using UnityEngine;
using System.Collections;

public class CelebrateState : State
{
    public CelebrateState(AgentBehaviour agent) : base(agent) { }
    

    public override void Initialize()
    {
        base.Initialize();
        _agent.animController.Play("jump");
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
