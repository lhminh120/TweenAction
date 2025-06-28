using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TweenAction
{
    public class TweenActionGameObjectDisable : TweenActionBase
    {
        [SerializeField] private GameObject _target;
        protected override void Execute()
        {

        }

        public override void Register()
        {
            GetTweenActionControl().AddTweenActionBaseToList(this);
        }
        public override void ResetExecute()
        {
            base.ResetExecute();
        }

        public override void FinishProgressRightNow()
        {
            _target.SetActive(false);
        }
    }

}
