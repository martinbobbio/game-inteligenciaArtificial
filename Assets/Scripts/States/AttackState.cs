using UnityEngine;
using System.Collections;

public class AttackState : State
{
    public AttackState(AgentBehaviour agent) : base(agent) { }

    private AgentBehaviour _enemy;
    private float _cooldownTick;

    public override void Initialize()
    {
        base.Initialize();
        _enemy = _agent.GetCurrentEnemy();        
    }

    public override void Update()
    {
        base.Update();

        if (_cooldownTick > 0)
        {
            _cooldownTick -= Time.deltaTime;
            return;
        }

        if (_enemy == null || _enemy.hp <= 0)
        {
            _agent.TriggerEvent(FSMEvents.ENEMY_KILLED);
            return;
        }

        var vTemp = (_enemy.transform.position - _agent.transform.position).normalized;
        if(vTemp != Vector3.zero)
            _agent.transform.forward = vTemp;

        if (Vector3.Distance(_agent.transform.position, _enemy.transform.position) <= _agent.attackDistance)
        {
            _cooldownTick = Random.Range(0.75f, 1);
            _agent.animController.Play("attack");
            _agent.PerformDamage();
        }
        else
        {
            _agent.Move(_enemy.transform.position + new Vector3(Random.value, 0, Random.value));
        }
    }

    public override void Dismiss()
    {
        base.Dismiss();
        //Nothing to do...
    }
}
