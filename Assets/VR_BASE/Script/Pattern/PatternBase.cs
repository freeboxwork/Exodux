using UnityEngine;

public abstract class PatternBase : MonoBehaviour
{

    public PatternType.Type patternType;
    public bool enableEvent = false;
    public abstract void EventStart(ScenarioData data);
    public abstract void SetGoalData(ScenarioData data);
    public abstract void ResetGoalData();
    public abstract void StepClear();
    public PatternManager patternManager;

    public void EnableEvent(bool value)
    {
        enableEvent = value;
    }

}
