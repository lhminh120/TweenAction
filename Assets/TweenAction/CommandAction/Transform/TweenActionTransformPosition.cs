using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TweenAction
{
    public class TweenActionTransformPosition : TweenComponent
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _position;
        private Vector3 _differentValue;
        private Vector3 _originalValue;
        protected override void Execute(float progress)
        {
            _target.position = _originalValue + progress * _differentValue;
        }

        protected override void OnStartExecute()
        {
            _originalValue = _target.position;
            _differentValue = _position - _target.position;
        }

    }

}
