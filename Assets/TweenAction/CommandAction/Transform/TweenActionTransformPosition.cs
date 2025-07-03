using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TweenAction
{
    public class TweenActionTransformPosition : TweenActionComponent
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _position;
        private Vector3 _differentValue;
        protected override void Execute(float progress)
        {
            _target.position = _target.position + progress * _differentValue;
        }

        protected override void OnStartExecute()
        {
            _differentValue = _position - _target.position;
        }

    }

}
