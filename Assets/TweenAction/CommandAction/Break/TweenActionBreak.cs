using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TweenAction
{
    public class TweenActionBreak : TweenComponent
    {
        protected override TweenOrder RegisterOrder()
        {
            return new TweenOrder(_duration);
        }
        public override (bool, TweenOrder) Register()
        {
            return (true, RegisterOrder());
        }

        protected override void Execute(float progress)
        {
        }

        protected override void OnStartExecute()
        {

        }
    }

}
