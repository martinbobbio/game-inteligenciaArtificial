public class StateMachine
{
    private int _currentState;
    private State[] _states;

    // TODO: Completar los valores de la configMatrix
    private int[,] configMatrix = new int[,] { {1, 0, 2, 3},
                                               {1, 0, 2, 3},
                                               {2, 2, 2, 2},
                                               {3, 3, 3, 3}};
 
    public StateMachine(AgentBehaviour agent)
    {
        // Inicializo los estados
        _states = new State[4];
        _states[0] = new IdleState(agent);
        _states[1] = new AttackState(agent);
        _states[2] = new DeathState(agent);
        _states[3] = new CelebrateState(agent);
        _currentState = 0;
    }

    /// <summary>
    /// Recibe un evento y -en caso de ser necesario- realiza la transición hacia un nuevo estado
    /// </summary>
    /// <param name="eventID">Código de identificación del evento</param>
	
	public void TriggerEvent(int eventID)
    {
        // TODO: Recibir el evento y realizar la transicion de estados (si es necesaria).
		int nextState = configMatrix[_currentState,eventID];
		
		if(_currentState != nextState)
		{
			_states[_currentState].Dismiss();
			_currentState = nextState;
			_states[_currentState].Initialize();			
		}
    }
	
    

    /// <summary>
    /// Actualización de la lógica de estados.
    /// </summary>
    public void Update()
    {
        // TODO: Llamar al metodo de actualización del estado correspondiente.
		_states[_currentState].Update();
		
		
    }
}
