using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TweenAction
{
    public class TweenActionRectTransformLocalPosition : TweenComponent
    {
        [SerializeField] private RectTransform _target;
        [SerializeField] private Vector3 _position;
        private Vector3 _differentValue;
        private Vector3 _originalValue;
        protected override void Execute(float progress)
        {
            _target.localPosition = _originalValue + progress * _differentValue;
        }
        protected override void OnStartExecute()
        {
            _originalValue = _target.localPosition;
            _differentValue = _position - _target.localPosition;
        }
    }

}
