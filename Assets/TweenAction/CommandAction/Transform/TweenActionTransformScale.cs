using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TweenAction
{
    public class TweenActionTransformScale : TweenActionComponent
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _scale;
        private Vector3 _differentValue;
        protected override void Execute(float progress)
        {
            _target.localScale = _target.localScale + progress * _differentValue;
        }

        protected override void OnStartExecute()
        {
            _differentValue = _scale - _target.localScale;
        }

    }

}
