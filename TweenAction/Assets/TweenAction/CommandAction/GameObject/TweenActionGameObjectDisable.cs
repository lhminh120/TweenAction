using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TweenAction
{
    public class TweenActionGameObjectDisable : TweenActionBase
    {
        [SerializeField] private GameObject _target;
        protected override void Excute()
        {

        }

        public override void Register()
        {
            GetCAControl().AddCABaseToList(this);
        }
        public override void ResetExcute()
        {
            base.ResetExcute();
        }

        public override void FinishProgressRightNow()
        {
            _target.SetActive(false);
        }
    }

}
