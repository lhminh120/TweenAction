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
            GetTweenActionControl().BreakList();
            if (_duration > 0)
            {
                GetTweenActionControl().AddTweenActionBaseToList(this);
                GetTweenActionControl().BreakList();
            }
        }

        protected override void Execute()
        {
        }
    }

}
