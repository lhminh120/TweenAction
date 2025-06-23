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
        protected virtual void Awake()
        {
            _tweenActionControl = GetComponent<TweenActionControl>();
        }
        protected TweenActionControl GetCAControl()
        {
            if (_tweenActionControl == null) _tweenActionControl = GetComponent<TweenActionControl>();
            return _tweenActionControl;
        }
        public float GetDuration() => _duration;
        public virtual void ResetExcute()
        {
            _countUp = 0;
        }
        public abstract void Register();
        protected abstract void Excute();
        public void ExcuetOverTime()
        {
            if (_countUp < _duration)
            {
                Excute();
                _countUp += Time.deltaTime;

            }
        }
        public abstract void FinishProgressRightNow();
    }

}
