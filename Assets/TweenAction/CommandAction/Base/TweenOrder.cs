using System;
using UnityEngine;

namespace TweenAction
{
    public class TweenOrder
    {
        private GlobalVariables.LeanEase _leanEase;
        private float _duration;
        private float _countUp = 0;
        private Action<float> _updateAction;
        private Action _onStart;
        public TweenOrder(float duration, Action<float> updateAction = null, GlobalVariables.LeanEase leanEase = GlobalVariables.LeanEase.Linear)
        {
            _updateAction = updateAction;
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
        private void Execute()
        {
            _updateAction?.Invoke(Utilities.Smooth(_leanEase, _countUp / _duration));
        }
        public void ExecuteOverTime()
        {
            if (_countUp < _duration)
            {
                Execute();
                _countUp += Time.deltaTime;

            }
        }
        public void FinishProgressRightNow()
        {
            _updateAction?.Invoke(1);
        }
    }
}
