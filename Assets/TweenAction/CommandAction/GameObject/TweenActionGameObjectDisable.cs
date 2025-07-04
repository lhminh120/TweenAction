using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TweenAction
{
    public class TweenActionGameObjectDisable : TweenComponent
    {
        [SerializeField] private GameObject _target;

        protected override void Execute(float progress)
        {
            if (progress >= 1)
                _target.SetActive(false);
        }

        protected override void OnStartExecute()
        {

        }
    }

}
