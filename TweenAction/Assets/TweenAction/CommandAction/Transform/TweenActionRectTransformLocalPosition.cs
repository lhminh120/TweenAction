using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TweenAction
{
    public class TweenActionRectTransformLocalPosition : TweenActionBase
    {
        [SerializeField] private RectTransform _target;
        [SerializeField] private Vector3 _position;
        private Vector3 _original;
        protected override void Excute()
        {
            _target.localPosition = Utilities.SmoothVector3(_original, _position, Utilities.Smooth(_leanStyle, _countUp / _duration));
        }

        public override void Register()
        {
            GetCAControl().AddCABaseToList(this);
        }
        public override void ResetExcute()
        {
            base.ResetExcute();
            _original = _target.localPosition;
        }

        public override void FinishProgressRightNow()
        {
            _target.localPosition = _position;
        }
    }

}
