using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TweenAction
{
    public class TweenActionTransformPosition : TweenActionBase
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _position;
        private Vector3 _original;
        protected override void Execute()
        {
            _target.position = Utilities.SmoothVector3(_original, _position, Utilities.Smooth(_leanStyle, _countUp / _duration));
        }

        public override void Register()
        {
            GetTweenActionControl().AddTweenActionBaseToList(this);
        }
        public override void ResetExecute()
        {
            base.ResetExecute();
            _original = _target.position;
        }

        public override void FinishProgressRightNow()
        {
            _target.position = _position;
        }
    }

}
