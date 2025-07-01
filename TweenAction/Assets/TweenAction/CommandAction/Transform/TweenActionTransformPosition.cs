using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TweenAction
{
    public class TweenActionTransformPosition : TweenActionComponent
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _position;
        private Vector3 _original;
        protected override void Execute(float progress)
        {
            _target.position = Utilities.SmoothVector3(_original, _position, progress);
        }

        protected override void OnStartExecute()
        {
            _original = _target.position;
        }

    }

}
