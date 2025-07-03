using UnityEngine;
using TweenAction;

public class MoveFowardAndBackward : MonoBehaviour
{
    [SerializeField] private GlobalVariables.LeanEase _leanEase;
    [SerializeField] private Transform _transform;
    [SerializeField] private Vector3 _positionA;
    [SerializeField] private Vector3 _positionB;
    private void Awake()
    {
        _transform.position = _positionA;
        Tween.Target(gameObject)
            .BreakAndAppend(1, (progress) => { _transform.position = _positionA + (_positionB - _positionA) * progress; })
            .BreakAndAppend(1, (progress) => { _transform.position = _positionB + (_positionA - _positionB) * progress; })
            .Repeat(-1)
            .StartAll();
    }
}