using UnityEngine;
using System.Collections;

public class AgentBehaviour : MonoBehaviour
{
    #region PUBLIC VARS
    public Team team;
    public float hp;
    public float damage;
    public float attackDistance;
    #endregion

    #region HIDDEN VARS
    [HideInInspector]
    public AgentBehaviour _targetEnemy;
    [HideInInspector]
    public Animation animController;
    #endregion


    #region PRIVATE VARS
    private StateMachine _stateMachine;
    private NavMeshAgent _navController;
    private Material _mat;
    #endregion

    #region ININITAL SETTINGS
    void Awake()
    {
        animController = GetComponent<Animation>();
        _navController = GetComponent<NavMeshAgent>();
        _mat = GetComponentInChildren<Renderer>().material;
        _mat.SetFloat("_HitAmount", 0);
    }

    void Start()
    {
        _stateMachine = new StateMachine(this);
        GameController.Instance.AddAgent(this, team);
    }

    void Update()
    {
        _stateMachine.Update();
    }
    #endregion

    #region BEHAVIOUR LOGIC
    /// <summary>
    /// Mueve al agente a la posición asignada.
    /// </summary>
    /// <param name="position">Destino</param>
    public void Move (Vector3 position)
    {
        Vector3 dir = position - transform.position;
        dir.y = transform.forward.y;
        transform.forward = dir.normalized;        
        _navController.SetDestination(position);
        animController.Play("run");
    }

    /// <summary>
    /// Aplica daño sobre el enemigo.
    /// </summary>
    public void PerformDamage()
    {
        if (hp <= 0)
            return;
        else if (_targetEnemy != null && _targetEnemy.hp >= 0)
            Invoke("DoDelayedDamage", 1);
    }

    private void DoDelayedDamage()
    {
        if (hp <= 0)
            return;
        _targetEnemy.TakeDamage(damage * Random.Range(0.5f, 1.25f));
    }
    /// <summary>
    /// Recibe daño de algun enemigo.
    /// </summary>
    /// <param name="dmg"></param>
    public void TakeDamage (float dmg)
    {
        hp -= dmg;
        if (hp <= 0)
            TriggerEvent(FSMEvents.DEAD);
        else
            iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 1, "time", 0.3f, "onupdate", "HitTween", "oncomplete", "HitTweenBack"));
    }

    private void HitTween(float f)
    {
        _mat.SetFloat("_HitAmount", f);
    }

    private void HitTweenBack()
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", 1, "to", 0, "time", 0.3f, "onupdate", "HitTween"));
    }

    /// <summary>
    /// Busca y asigna un enemigo como objetivo.
    /// </summary>
    public void SearchEnemy()
    {
        var possibleEnemies = GameController.Instance.GetEnemies(team);

        float nearestDistance = float.MaxValue;
        float tempDistance = 0;
        foreach (var enemy in possibleEnemies)
        {
            tempDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (tempDistance < nearestDistance)
            {
                nearestDistance = tempDistance;
                _targetEnemy = enemy;
            }
        }
    }

    /// <summary>
    /// Desuscribe al agente del equipo en el que se encuentra y lo elimina.
    /// </summary>
    public void Die ()
    {
        GameController.Instance.RemoveAgent(this);
        gameObject.SetActive(false);
    }
    
    public void TriggerEvent(int eventId)
    {
        _stateMachine.TriggerEvent(eventId); 
    }

    public void SetEnemy(AgentBehaviour enemy)
    {
        _targetEnemy = enemy;
    }

    public AgentBehaviour GetCurrentEnemy()
    {
        return _targetEnemy;
    }
    #endregion
}