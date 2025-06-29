using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TweenAction
{
    [RequireComponent(typeof(TweenAction))]
    public abstract class TweenActionBase : MonoBehaviour
    {
        [SerializeField] protected float _duration;
        [SerializeField] protected GlobalVariables.LeanEase _leanStyle;
        private TweenAction _tweenActionControl;
        protected float _countUp = 0;
        protected TweenAction Tween()
        {
            if (_tweenActionControl == null)
            {
                _tweenActionControl = GetComponent<TweenAction>();
                if (_tweenActionControl == null)
                    _tweenActionControl = gameObject.AddComponent<TweenAction>();
            }
            return _tweenActionControl;
        }
        public float GetDuration() => _duration;
        public virtual void ResetExecute()
        {
            _countUp = 0;
        }
        public virtual void Register()
        {
            Tween().Add(this);
        }
        protected abstract void Execute();
        public void ExecuteOverTime()
        {
            if (_countUp < _duration)
            {
                Execute();
                _countUp += Time.deltaTime;

            }
        }
        public abstract void FinishProgressRightNow();
    }

}
