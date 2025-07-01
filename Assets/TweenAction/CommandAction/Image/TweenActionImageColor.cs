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
        private Color _original;
        protected override void Execute(float progress)
        {
            _target.color = Utilities.SmoothColor(_original, _color, progress);
        }

        protected override void OnStartExecute()
        {
            _original = _target.color;
        }
    }

}
