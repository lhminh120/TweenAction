using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TweenAction
{
    public class TweenActionTransformScale : TweenActionComponent
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _scale;
        private Vector3 _original;
        protected override void Execute(float progress)
        {
            _target.localScale = Utilities.SmoothVector3(_original, _scale, progress);
        }

        protected override void OnStartExecute()
        {
            _original = _target.localScale;
        }

    }

}
