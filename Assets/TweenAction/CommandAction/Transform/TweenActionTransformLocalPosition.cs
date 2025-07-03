using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TweenAction
{
    public class TweenActionTransformLocalPosition : TweenActionComponent
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _position;
        private Vector3 _differentValue;
        protected override void Execute(float progress)
        {
            _target.localPosition = _target.localPosition + progress * _differentValue;
        }

        protected override void OnStartExecute()
        {
            _differentValue = _position - _target.localPosition;
        }
    }

}
