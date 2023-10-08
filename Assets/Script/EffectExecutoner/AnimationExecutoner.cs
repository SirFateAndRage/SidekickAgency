using UnityEngine;

public class AnimationExecutoner : MonoBehaviour, IEffectExecution
{
    [SerializeField] Animator _animator;
    public void Execute()
    {

        _animator.SetBool("Execute", true);
    }
}
