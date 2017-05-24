using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    #region PUBLIC VARS
    public Text displayInfo;
    public Text gameOverText;
    [HideInInspector]
    public List<AgentBehaviour> teamA = new List<AgentBehaviour>();
    [HideInInspector]
    public List<AgentBehaviour> teamB = new List<AgentBehaviour>();
    #endregion
    #region SINGLETON
    public static GameController Instance { get { return _instance; } }
    private static GameController _instance;
    void Awake()    {        _instance = this;   gameOverText.gameObject.SetActive(false); }
    #endregion

    /// <summary>
    /// Agrega un nuevo agente a su equipo.
    /// </summary>
    /// <param name="agent">Agente a agregar</param>
    /// <param name="team">Equipo al cual agregar</param>
    public void AddAgent(AgentBehaviour agent, Team team)
    {
        if (team == Team.TEAM_A)
            teamA.Add(agent);
        else
            teamB.Add(agent);

        displayInfo.text = "Team <color=#FF0000><b>RED</b></color>: " + teamA.Count + "\nTeam <color=#0000FF><b>BLUE</b></color>: " + teamB.Count;
    }

    /// <summary>
    /// Remueve al agente de la lista del equipo correspondiente
    /// </summary>
    /// <param name="agent">Agente a remover</param>
    public void RemoveAgent(AgentBehaviour agent)
    {
        if (agent.team == Team.TEAM_A)
            teamA.Remove(agent);
        else
            teamB.Remove(agent);

        displayInfo.text = "Team <color=#FF0000><b>RED</b></color>: "+teamA.Count+"\nTeam <color=#0000FF><b>BLUE</b></color>: "+teamB.Count;

        CheckGameStatus();
    }

    /// <summary>
    /// Chequea el estado actual del juego y dispara los eventos de Game Over en caso de ser necesario.
    /// </summary>
    private void CheckGameStatus()
    {
        if (teamA.Count == 0 && teamB.Count == 0)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "EVERYBODY\n<size=150><b> DIES </b></size>";
        }

        if (teamA.Count == 0)
        {
            gameOverText.text = "TEAM <color=#0000FF>BLUE</color>\n<size=150><b> WINS </b></size>";
            gameOverText.gameObject.SetActive(true);
            foreach (var agent in teamB)
            {
                agent.TriggerEvent(FSMEvents.WIN_MATCH);
            }
        }


        if (teamB.Count == 0)
        {
            gameOverText.text = "TEAM <color=#FF0000>RED</color>\n<size=150><b> WINS </b></size>";
            gameOverText.gameObject.SetActive(true);
            foreach (var agent in teamA)
            {
                agent.TriggerEvent(FSMEvents.WIN_MATCH);
            }
        }
    }

    /// <summary>
    /// Obtiene un listado de los enemigos que se encuentran vivos.
    /// </summary>
    /// <param name="agentTeam">Equipo actual</param>
    /// <returns></returns>
    public AgentBehaviour[] GetEnemies(Team agentTeam)
    {
        return agentTeam == Team.TEAM_A ? teamB.ToArray() : teamA.ToArray();
    }
}

public enum Team
{
    TEAM_A,
    TEAM_B
}