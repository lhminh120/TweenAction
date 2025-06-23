using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TweenAction
{
    public class TweenActionTransformScale : TweenActionBase
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _scale;
        private Vector3 _original;
        protected override void Excute()
        {
            _target.localScale = Utilities.SmoothVector3(_original, _scale, Utilities.Smooth(_leanStyle, _countUp / _duration));
        }

        public override void Register()
        {
            GetCAControl().AddCABaseToList(this);
        }
        public override void ResetExcute()
        {
            base.ResetExcute();
            _original = _target.localScale;
        }

        public override void FinishProgressRightNow()
        {
            _target.localScale = _scale;
        }
    }

}
