using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TweenAction
{
    public class TweenActionImageColor : TweenActionBase
    {
        [SerializeField] private Image _target;
        [SerializeField] private Color _color;
        private Color _original;
        protected override void Excute()
        {
            _target.color = Utilities.SmoothColor(_original, _color, Utilities.Smooth(_leanStyle, _countUp / _duration));
        }

        public override void Register()
        {
            GetCAControl().AddCABaseToList(this);
        }
        public override void ResetExcute()
        {
            base.ResetExcute();
            _original = _target.color;
        }

        public override void FinishProgressRightNow()
        {
            _target.color = _color;
        }
    }

}
