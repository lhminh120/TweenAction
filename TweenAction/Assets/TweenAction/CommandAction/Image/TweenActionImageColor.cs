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
        protected override void Execute()
        {
            _target.color = Utilities.SmoothColor(_original, _color, Utilities.Smooth(_leanStyle, _countUp / _duration));
        }

        public override void Register()
        {
            GetTweenActionControl().AddTweenActionBaseToList(this);
        }
        public override void ResetExecute()
        {
            base.ResetExecute();
            _original = _target.color;
        }

        public override void FinishProgressRightNow()
        {
            _target.color = _color;
        }
    }

}
