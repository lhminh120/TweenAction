using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TweenAction
{
    public class TweenAction : MonoBehaviour
    {
        private List<List<TweenActionBase>> _modifyList = new List<List<TweenActionBase>>();
        private Queue<List<TweenActionBase>> _executeList = new Queue<List<TweenActionBase>>();
        private Queue<List<TweenActionBase>> _tempList = new Queue<List<TweenActionBase>>();
        private List<TweenActionBase> _childBaseList;
        private float _lastDuration;
        private float _countUp;
        private bool _doneExecute = true;

        private void Awake()
        {
            var tweenActionBases = GetComponents<TweenActionBase>();
            if (tweenActionBases == null) return;
            _childBaseList = new List<TweenActionBase>();
            _modifyList.Add(_childBaseList);
            for (int i = 0, length = tweenActionBases.Length; i < length; i++)
            {
                tweenActionBases[i].Register();
            }
            _childBaseList = null;
            Run();
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
            _childBaseList = null;
            _doneExecute = false;
        }
        public TweenAction Add(TweenActionBase tweenActionBases)
        {
            if (_childBaseList == null) Append(tweenActionBases);
            else _childBaseList.Add(tweenActionBases);
            return this;

        }
        public TweenAction Append(TweenActionBase tweenActionBases)
        {
            BreakList();
            _childBaseList.Add(tweenActionBases);
            return this;
        }
        public TweenAction Insert(int index, TweenActionBase tweenActionBases)
        {
            if (_modifyList.Count > index && index >= 0)
                _modifyList[index].Add(tweenActionBases);
            else
                Debug.LogWarning("index is invalid number");
            return this;
        }
        public TweenAction BreakList()
        {
            _childBaseList = new List<TweenActionBase>();
            _modifyList.Add(_childBaseList);
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
            _childBaseList = null;
            _doneExecute = true;

        }
        public void StartAll()
        {
            ResetAll();
        }
        private void Execute()
        {
            if (_doneExecute) return;
            if (_countUp >= _lastDuration)
            {
                _countUp = 0;
                _lastDuration = 0;
                if (_executeList.Count > 0)
                {
                    if (_childBaseList != null)
                    {
                        for (int i = 0, length = _childBaseList.Count; i < length; i++)
                        {
                            _childBaseList[i].FinishProgressRightNow();
                        }
                    }
                    _childBaseList = _executeList.Dequeue();
                    for (int i = 0, length = _childBaseList.Count; i < length; i++)
                    {
                        if (_lastDuration < _childBaseList[i].GetDuration()) _lastDuration = _childBaseList[i].GetDuration();
                        _childBaseList[i].ResetExecute();
                    }
                    _tempList.Enqueue(_childBaseList);
                }
                else
                {
                    _doneExecute = true;
                }

            }
            else
            {
                _countUp += Time.deltaTime;
                for (int i = 0, length = _childBaseList.Count; i < length; i++)
                {
                    _childBaseList[i].ExecuteOverTime();
                }
            }
        }
        private void Update()
        {
            Execute();
        }
    }

}
