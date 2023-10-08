using UnityEngine;

public class GameObjectExecutoner : MonoBehaviour, IEffectExecution
{
    [SerializeField] GameObject _object;
    public void Execute()
    {
        _object.SetActive(false);
    }
}
