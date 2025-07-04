using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TweenAction
{
    public class TweenActionTransformRotate : TweenComponent
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _angle;
        private Vector3 _differentValue;
        private Vector3 _originalValue;
        protected override void Execute(float progress)
        {
            _target.localEulerAngles = _originalValue + progress * _differentValue;
        }

        protected override void OnStartExecute()
        {
            _originalValue = _target.localEulerAngles;
            _differentValue = _angle - _target.localEulerAngles;
        }
    }

}
