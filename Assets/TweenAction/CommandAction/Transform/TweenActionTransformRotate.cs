using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TweenAction
{
    public class TweenActionTransformRotate : TweenActionComponent
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _angle;
        private Vector3 _original;
        protected override void Execute(float progress)
        {
            _target.localEulerAngles = Utilities.SmoothVector3(_original, _angle, progress);
        }

        protected override void OnStartExecute()
        {
            _original = _target.localEulerAngles;
        }
    }

}
