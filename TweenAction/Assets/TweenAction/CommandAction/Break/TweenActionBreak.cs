using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TweenAction
{
    public class TweenActionBreak : TweenActionBase
    {
        public override void FinishProgressRightNow()
        {

        }

        public override void Register()
        {
            Tween().BreakList();
            if (_duration > 0)
            {
                Tween().Add(this).BreakList();
            }
        }

        protected override void Execute()
        {
        }
    }

}
