using HTC.UnityPlugin.ColliderEvent;
using HTC.UnityPlugin.Utility;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abgewandelte Klasse zum Darstellen eines Drückbaren Buttons.
/// Grundlegend war das Skript "ResetButton" im VIU Beispiel "Collider Event".
/// </summary>
public class ResetPinsAndBallsLane3 : MonoBehaviour
    , IColliderEventPressUpHandler
    , IColliderEventPressEnterHandler
    , IColliderEventPressExitHandler
{
    public Transform buttonObject;
    public Vector3 buttonDownDisplacement;

    [SerializeField]
    private ColliderButtonEventData.InputButton m_activeButton = ColliderButtonEventData.InputButton.Trigger;
    private RigidPose[] resetPoses;
    private HashSet<ColliderButtonEventData> pressingEvents = new HashSet<ColliderButtonEventData>();
    private readonly int LANE_THREE = 3;

    /// <summary>
    /// Der genutzte Button am ViveController. Einstellbar in der Szene in Unity.
    /// </summary>
    public ColliderButtonEventData.InputButton activeButton
    {
        get
        {
            return m_activeButton;
        }
        set
        {
            m_activeButton = value;
            // set all child MaterialChanger heighlightButton to value;
            var changers = ListPool<MaterialChanger>.Get();
            GetComponentsInChildren(changers);
            for (int i = changers.Count - 1; i >= 0; --i) { changers[i].heighlightButton = value; }
            ListPool<MaterialChanger>.Release(changers);
        }
    }

    private void Start()
    {
    }

#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        activeButton = m_activeButton;
    }
#endif

    /// <summary>
    /// Methode wird aufgerufen, wenn der Button gedrückt wird.
    /// </summary>
    /// <param name="eventData">Enthält Infos über das ausgelöste Event. Beispielsweise den gedrückten Knopf am Controller.</param>
    public void OnColliderEventPressUp(ColliderButtonEventData eventData)
    {
        //Nur der Trigger löst die gewünschte Aktion aus.
        if (eventData.button.ToString().Equals("Trigger"))
        {
            if (GameObject.Find("ResetButtonPinsAndBalls3").activeSelf == true) { GameObject.Find("GameController").GetComponent<Bowling>().resetBallsAndPins(LANE_THREE); }
        }
    }

    /// <summary>
    /// Wird aufgerufen wenn der Button gedrückt wird.
    /// Zusätzlich wird der Button in seine "gedrückte" Position gesetzt.
    /// </summary>
    /// <param name="eventData">Enthält Infos über das ausgelöste Event.</param>
    public void OnColliderEventPressEnter(ColliderButtonEventData eventData)
    {
        if (eventData.button == m_activeButton && pressingEvents.Add(eventData) && pressingEvents.Count == 1)
        {
            buttonObject.localPosition += buttonDownDisplacement;
        }
    }

    /// <summary>
    /// Wird aufgerufen wenn der Button losgelassen wird.
    /// Zusätzlich wird der Button wieder an die ursprüngliche Position gesetzt.
    /// </summary>
    /// <param name="eventData">Enthält Infos über das ausgelöste Event.</param>
    public void OnColliderEventPressExit(ColliderButtonEventData eventData)
    {
        if (pressingEvents.Remove(eventData) && pressingEvents.Count == 0)
        {
            buttonObject.localPosition -= buttonDownDisplacement;
        }
    }
}
