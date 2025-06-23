using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TweenAction
{
    public class TweenActionTransformRotate : TweenActionBase
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _angle;
        private Vector3 _original;
        protected override void Excute()
        {
            _target.localEulerAngles = Utilities.SmoothVector3(_original, _angle, Utilities.Smooth(_leanStyle, _countUp / _duration));
        }

        public override void Register()
        {
            GetCAControl().AddCABaseToList(this);
        }
        public override void ResetExcute()
        {
            base.ResetExcute();
            _original = _target.localEulerAngles;
        }

        public override void FinishProgressRightNow()
        {
            _target.localEulerAngles = _angle;
        }
    }

}
