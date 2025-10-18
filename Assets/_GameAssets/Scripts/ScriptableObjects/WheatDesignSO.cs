using UnityEngine;

[CreateAssetMenu(fileName = "WheatDesignSO, menuName = ScripttableObjects/WheatDesignSO")]
public class WheatDesignSO : ScriptableObject
{
    [SerializeField] private float increaseDecreaseMultiplier;
    [SerializeField] private float resetBoostDuration;

    public float IncreaseDecreaseMultiplier => increaseDecreaseMultiplier;
    public float ResetBoostDuration => resetBoostDuration;
}
