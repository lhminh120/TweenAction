using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TweenAction
{
    [RequireComponent(typeof(Tween))]
    public abstract class TweenActionComponent : MonoBehaviour
    {
        [SerializeField] protected float _duration;
        [SerializeField] protected GlobalVariables.LeanEase _leanStyle;
        [SerializeField] protected UnityAction _onStart;
        [SerializeField] protected UnityAction _onComplete;
        protected virtual TweenOrder RegisterOrder()
        {
            return new TweenOrder(_duration, Execute, _leanStyle)
                        .OnStart(() =>
                        {
                            OnStartExecute();
                            _onStart?.Invoke();
                        })
                        .OnComplete(() =>
                        {
                            _onComplete?.Invoke();
                        });
        }
        public virtual (bool, TweenOrder) Register()
        {
            return (false, RegisterOrder());
        }
        protected abstract void Execute(float progress);
        protected abstract void OnStartExecute();
    }

}
