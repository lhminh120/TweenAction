using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TweenAction
{
    [DisallowMultipleComponent]
    public class TweenAction : MonoBehaviour
    {
        private List<List<TweenOrder>> _modifyList = new List<List<TweenOrder>>();
        private Queue<List<TweenOrder>> _executeList = new Queue<List<TweenOrder>>();
        private Queue<List<TweenOrder>> _tempList = new Queue<List<TweenOrder>>();
        private List<TweenOrder> _childList;
        private float _lastDuration;
        private float _countUp;
        private bool _doneExecute = true;
        private bool _isPausing = false;

        private void Awake()
        {
            // var tweenActionBases = GetComponents<TweenActionComponent>();
            // if (tweenActionBases == null) return;
            // _childBaseList = new List<TweenActionComponent>();
            // _modifyList.Add(_childBaseList);
            // for (int i = 0, length = tweenActionBases.Length; i < length; i++)
            // {
            //     tweenActionBases[i].Register();
            // }
            // _childBaseList = null;
            // Run();
        }
        public static TweenAction Target(GameObject target)
        {
            var tweenAction = target.GetComponent<TweenAction>();
            if (tweenAction == null)
                tweenAction = target.AddComponent<TweenAction>();
            return tweenAction;
        }
        public void ResetAll()
        {
            while (_executeList.Count > 0)
            {
                _tempList.Enqueue(_executeList.Dequeue());
            }
            while (_tempList.Count > 0)
            {
                _executeList.Enqueue(_tempList.Dequeue());
            }
            _countUp = 0;
            _lastDuration = 0;
            _childList = null;
            _doneExecute = false;
        }
        public TweenAction Append(TweenOrder tweenOrder)
        {
            if (_childList == null) BreakAndAppend(tweenOrder);
            else _childList.Add(tweenOrder);
            return this;

        }
        public TweenAction BreakAndAppend(TweenOrder tweenOrder)
        {
            BreakList();
            _childList.Add(tweenOrder);
            return this;
        }
        public TweenAction Insert(int index, TweenOrder tweenOrder)
        {
            if (_modifyList.Count > index && index >= 0)
                _modifyList[index].Add(tweenOrder);
            else
                Debug.LogWarning("index is invalid number");
            return this;
        }
        public TweenAction BreakList()
        {
            _childList = new List<TweenOrder>();
            _modifyList.Add(_childList);
            return this;
        }
        public TweenAction Run()
        {
            for (int i = 0, length = _modifyList.Count; i < length; i++)
            {
                _executeList.Enqueue(_modifyList[i]);
            }
            _modifyList.Clear();
            return this;
        }
        public void StopAll()
        {
            while (_executeList.Count > 0)
            {
                _tempList.Enqueue(_executeList.Dequeue());
            }
            while (_tempList.Count > 0)
            {
                var tweenActionControls = _tempList.Dequeue();
                for (int i = 0, length = tweenActionControls.Count; i < length; i++)
                {
                    tweenActionControls[i].FinishProgressRightNow();
                }
                _executeList.Enqueue(tweenActionControls);
            }
            _countUp = 0;
            _lastDuration = 0;
            _childList = null;
            _doneExecute = true;

        }
        public void StartAll()
        {
            ResetAll();
        }
        public void Pause()
        {
            _isPausing = true;
        }
        public void Resume()
        {
            _isPausing = false;
        }
        private void Execute()
        {
            if (_doneExecute) return;
            if (_isPausing) return;
            if (_countUp >= _lastDuration)
            {
                _countUp = 0;
                _lastDuration = 0;
                if (_executeList.Count > 0)
                {
                    if (_childList != null)
                    {
                        for (int i = 0, length = _childList.Count; i < length; i++)
                        {
                            _childList[i].FinishProgressRightNow();
                        }
                    }
                    _childList = _executeList.Dequeue();
                    for (int i = 0, length = _childList.Count; i < length; i++)
                    {
                        if (_lastDuration < _childList[i].GetDuration()) _lastDuration = _childList[i].GetDuration();
                        _childList[i].BeforeExecute();
                    }
                    _tempList.Enqueue(_childList);
                }
                else
                {
                    _doneExecute = true;
                }

            }
            else
            {
                _countUp += Time.deltaTime;
                for (int i = 0, length = _childList.Count; i < length; i++)
                {
                    _childList[i].ExecuteOverTime();
                }
            }
        }
        private void Update()
        {
            Execute();
        }
    }

}
