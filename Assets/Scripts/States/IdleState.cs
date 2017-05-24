using UnityEngine;
using System.Collections;

public class IdleState : State
{
    public IdleState(AgentBehaviour agent) : base(agent) {   }


    private float _tick = 0;
    private float _attackProbability = 30f;

    public override void Initialize()
    {
        base.Initialize();
        _agent.animController.Play("idle");
        _tick = 0;
    }

    public override void Update()
    {
        base.Update();

        if (_tick >= 0)
        {
            _tick -= Time.deltaTime;
            return;
        }
        else
        {
            _tick = 0;

            if (Random.Range(0, 100f) <= _attackProbability)
            {
                _agent.SearchEnemy();
                if (_agent._targetEnemy != null)
                    _agent.TriggerEvent(FSMEvents.ENEMY_SPOTTED);
            }            
        }
    }

    public override void Dismiss()
    {
        base.Dismiss();
        //Nothing to do...
    }
}
