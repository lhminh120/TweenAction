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
        protected override void Excute()
        {
            _target.position = Utilities.SmoothVector3(_original, _position, Utilities.Smooth(_leanStyle, _countUp / _duration));
        }

        public override void Register()
        {
            GetCAControl().AddCABaseToList(this);
        }
        public override void ResetExcute()
        {
            base.ResetExcute();
            _original = _target.position;
        }

        public override void FinishProgressRightNow()
        {
            _target.position = _position;
        }
    }

}
