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
            GetCAControl().BreakList();
            if (_duration > 0)
            {
                GetCAControl().AddCABaseToList(this);
                GetCAControl().BreakList();
            }
        }

        protected override void Excute()
        {
        }
    }

}
