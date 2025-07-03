using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TweenAction
{
    public class TweenActionTransformRotate : TweenActionComponent
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _angle;
        private Vector3 _differentValue;
        protected override void Execute(float progress)
        {
            _target.localEulerAngles = _target.localEulerAngles + progress * _differentValue;
        }

        protected override void OnStartExecute()
        {
            _differentValue = _angle - _target.localEulerAngles;
        }
    }

}
