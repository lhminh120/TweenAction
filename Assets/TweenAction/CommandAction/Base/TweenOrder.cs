using System;
using UnityEngine;

namespace TweenAction
{
    public class TweenOrder
    {
        private GlobalVariables.LeanEase _leanEase;
        private float _duration;
        private float _countUp = 0;
        private Action<float> _onUpdate;
        private Action _onStart;
        private Action _onComplete;

        public TweenOrder(float duration, Action<float> onUpdate = null, GlobalVariables.LeanEase leanEase = GlobalVariables.LeanEase.Linear)
        {
            _onUpdate = onUpdate;
            _duration = duration;
            _leanEase = leanEase;
        }
        public float GetDuration() => _duration;
        public void BeforeExecute()
        {
            _onStart?.Invoke();
            _countUp = 0;
        }
        public TweenOrder OnStart(Action onStart)
        {
            _onStart = onStart;
            return this;
        }
        public TweenOrder OnComplete(Action onComplete)
        {
            _onComplete = onComplete;
            return this;
        }
        private void Execute()
        {
            _onUpdate?.Invoke(Utilities.Smooth(_leanEase, _countUp / _duration));
        }
        public void ExecuteOverTime()
        {
            if (_countUp < _duration)
            {
                Execute();
                _countUp += Time.deltaTime;
                if (_countUp >= _duration)
                    _onComplete?.Invoke();
            }
        }
        public void FinishProgressRightNow()
        {
            _onUpdate?.Invoke(1);
        }
    }
}
