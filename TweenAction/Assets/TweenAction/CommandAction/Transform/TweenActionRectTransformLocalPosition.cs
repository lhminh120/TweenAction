using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TweenAction
{
    public class TweenActionRectTransformLocalPosition : TweenActionComponent
    {
        [SerializeField] private RectTransform _target;
        [SerializeField] private Vector3 _position;
        private Vector3 _original;
        protected override void Execute(float progress)
        {
            _target.localPosition = Utilities.SmoothVector3(_original, _position, progress);
        }
        protected override void OnStartExecute()
        {
            _original = _target.localPosition;
        }
    }

}
