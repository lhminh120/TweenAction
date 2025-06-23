using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TweenAction
{
    public class TweenActionControl : MonoBehaviour
    {
        private Queue<List<TweenActionBase>> _excuteList = new Queue<List<TweenActionBase>>();
        private Queue<List<TweenActionBase>> _tempList = new Queue<List<TweenActionBase>>();
        private List<TweenActionBase> _childBaseList;
        private float _lastDuration;
        private float _countUp;
        private bool _doneExcute = true;
        private void Awake()
        {
            var caBases = GetComponents<TweenActionBase>();
            _childBaseList = new List<TweenActionBase>();
            _excuteList.Enqueue(_childBaseList);
            for (int i = 0, length = caBases.Length; i < length; i++)
            {
                caBases[i].Register();
            }
            _childBaseList = null;
        }
        public void ResetAll()
        {
            while (_excuteList.Count > 0)
            {
                _tempList.Enqueue(_excuteList.Dequeue());
            }
            while (_tempList.Count > 0)
            {
                _excuteList.Enqueue(_tempList.Dequeue());
            }
            _countUp = 0;
            _lastDuration = 0;
            _childBaseList = null;
            _doneExcute = false;
        }
        public void AddCABaseToList(TweenActionBase caBases)
        {
            _childBaseList.Add(caBases);
        }
        public void BreakList()
        {
            _childBaseList = new List<TweenActionBase>();
            _excuteList.Enqueue(_childBaseList);
        }
        public void StopAll()
        {
            while (_excuteList.Count > 0)
            {
                _tempList.Enqueue(_excuteList.Dequeue());
            }
            while (_tempList.Count > 0)
            {
                var caControls = _tempList.Dequeue();
                for (int i = 0, length = caControls.Count; i < length; i++)
                {
                    caControls[i].FinishProgressRightNow();
                }
                _excuteList.Enqueue(caControls);
            }
            _countUp = 0;
            _lastDuration = 0;
            _childBaseList = null;
            _doneExcute = true;

        }
        public void StartAll()
        {
            ResetAll();
        }
        private void Excute()
        {
            if (_doneExcute) return;
            if (_countUp >= _lastDuration)
            {
                _countUp = 0;
                _lastDuration = 0;
                if (_excuteList.Count > 0)
                {
                    if (_childBaseList != null)
                    {
                        for (int i = 0, length = _childBaseList.Count; i < length; i++)
                        {
                            _childBaseList[i].FinishProgressRightNow();
                        }
                    }
                    _childBaseList = _excuteList.Dequeue();
                    for (int i = 0, length = _childBaseList.Count; i < length; i++)
                    {
                        if (_lastDuration < _childBaseList[i].GetDuration()) _lastDuration = _childBaseList[i].GetDuration();
                        _childBaseList[i].ResetExcute();
                    }
                    _tempList.Enqueue(_childBaseList);
                }
                else
                {
                    _doneExcute = true;
                }

            }
            else
            {
                _countUp += Time.deltaTime;
                for (int i = 0, length = _childBaseList.Count; i < length; i++)
                {
                    _childBaseList[i].ExcuetOverTime();
                }
            }
        }
        private void Update()
        {
            Excute();
        }
    }

}
