using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TweenAction
{
    [RequireComponent(typeof(TweenAction))]
    public abstract class TweenActionComponent : MonoBehaviour
    {
        [SerializeField] protected float _duration;
        [SerializeField] protected GlobalVariables.LeanEase _leanStyle;
        protected virtual TweenOrder RegisterOrder()
        {
            return new TweenOrder(_duration, Execute, _leanStyle).OnStart(OnStartExecute);
        }
        public virtual (bool, TweenOrder) Register()
        {
            return (false, RegisterOrder());
        }
        protected abstract void Execute(float progress);
        protected abstract void OnStartExecute();
    }

}
