using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TweenAction
{
    [RequireComponent(typeof(TweenActionControl))]
    public abstract class TweenActionBase : MonoBehaviour
    {
        [SerializeField] protected float _duration;
        [SerializeField] protected GlobalVariables.LeanEase _leanStyle;
        private TweenActionControl _tweenActionControl;
        protected float _countUp = 0;
        protected TweenActionControl GetTweenActionControl()
        {
            if (_tweenActionControl == null) _tweenActionControl = GetComponent<TweenActionControl>();
            return _tweenActionControl;
        }
        public float GetDuration() => _duration;
        public virtual void ResetExecute()
        {
            _countUp = 0;
        }
        public abstract void Register();
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
