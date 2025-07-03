using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TweenAction
{
    public class TweenActionImageColor : TweenActionComponent
    {
        [SerializeField] private Image _target;
        [SerializeField] private Color _color;
        private Color _differentValue;
        protected override void Execute(float progress)
        {
            _target.color = _target.color + _differentValue * progress;
        }

        protected override void OnStartExecute()
        {
            _differentValue = _color - _target.color;
        }
    }

}
