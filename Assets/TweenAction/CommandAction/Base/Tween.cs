using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TweenAction
{
    [DisallowMultipleComponent]
    public class Tween : MonoBehaviour
    {
        private List<List<TweenOrder>> _modifyList = new List<List<TweenOrder>>();
        private Queue<List<TweenOrder>> _executeList = new Queue<List<TweenOrder>>();
        private Queue<List<TweenOrder>> _tempList = new Queue<List<TweenOrder>>();
        private List<TweenOrder> _childList;
        private float _lastDuration;
        private float _countUp;
        private bool _doneExecute = true;
        private bool _isPausing = false;
        private int _repeatTime = 0;
        private int _countCurrentRepeatTime = 0;

        private void Awake()
        {
            var tweenActionBases = GetComponents<TweenComponent>();
            if (tweenActionBases == null) return;
            for (int i = 0, length = tweenActionBases.Length; i < length; i++)
            {
                (var isBreak, var tweenOrder) = tweenActionBases[i].Register();
                if (isBreak)
                    BreakAndAppend(tweenOrder);
                else
                    Append(tweenOrder);
            }
            RunAction();
        }
        public static Tween Target(GameObject target)
        {
            var tweenAction = target.GetComponent<Tween>();
            if (tweenAction == null)
                tweenAction = target.AddComponent<Tween>();
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
            _countCurrentRepeatTime = 0;
        }
        /// <summary>
        /// Append action into tween, appended actions will run at the same time
        /// </summary>
        /// <param name="duration">duration for this action</param>
        /// <param name="updateAction">run every frame until duration ends</param>
        /// <param name="leanEase"></param>
        /// <param name="onStart">will be called before starting action</param>
        /// <param name="onComplete">will be called before action ends</param>
        /// <returns></returns>
        public Tween Append(float duration, Action<float> updateAction = null, GlobalVariables.LeanEase leanEase = GlobalVariables.LeanEase.Linear, Action onStart = null, Action onComplete = null)
        {
            return Append(new TweenOrder(duration, updateAction, leanEase).OnStart(onStart).OnComplete(onComplete));
        }
        public Tween Append(TweenOrder tweenOrder)
        {
            if (_childList == null) BreakAndAppend(tweenOrder);
            else _childList.Add(tweenOrder);
            return this;

        }
        /// <summary>
        /// Create new list actions to append. Each action in the same appended list will run at the same time, and each list will run after each other
        /// </summary>
        /// <param name="duration">duration for this action</param>
        /// <param name="updateAction">run every frame until duration ends</param>
        /// <param name="leanEase"></param>
        /// <param name="onStart">will be called before starting action</param>
        /// <param name="onComplete">will be called before action ends</param>
        /// <returns></returns>
        public Tween BreakAndAppend(float duration, Action<float> updateAction = null, GlobalVariables.LeanEase leanEase = GlobalVariables.LeanEase.Linear, Action onStart = null, Action onComplete = null)
        {
            return BreakAndAppend(new TweenOrder(duration, updateAction, leanEase).OnStart(onStart).OnComplete(onComplete));
        }
        public Tween BreakAndAppend(TweenOrder tweenOrder)
        {
            BreakList();
            _childList.Add(tweenOrder);
            return this;
        }
        /// <summary>
        /// Insert action into list with index provide
        /// </summary>
        /// <param name="index">index of the list you want to insert</param>
        /// <param name="duration">duration for this action</param>
        /// <param name="updateAction">run every frame until duration ends</param>
        /// <param name="leanEase"></param>
        /// <param name="onStart">will be called before starting action</param>
        /// <param name="onComplete">will be called before action ends</param>
        /// <returns></returns>
        public Tween Insert(int index, float duration, Action<float> updateAction = null, GlobalVariables.LeanEase leanEase = GlobalVariables.LeanEase.Linear, Action onStart = null, Action onComplete = null)
        {
            return Insert(index, new TweenOrder(duration, updateAction, leanEase).OnStart(onStart).OnComplete(onComplete));
        }
        public Tween Insert(int index, TweenOrder tweenOrder)
        {
            if (_modifyList.Count > index && index >= 0)
                _modifyList[index].Add(tweenOrder);
            else
                Debug.LogWarning("index is invalid number");
            return this;
        }
        public Tween BreakList()
        {
            _childList = new List<TweenOrder>();
            _modifyList.Add(_childList);
            return this;
        }
        /// <summary>
        /// Set repeat time for the whole Tween
        /// </summary>
        /// <param name="repeatTime">repeat time, if equals -1 means infinite</param>
        /// <returns></returns>
        public Tween Repeat(int repeatTime)
        {
            _repeatTime = repeatTime;
            _countCurrentRepeatTime = 0;
            return this;
        }
        /// <summary>
        /// Must be called after setting all actions for this tween
        /// </summary>
        /// <returns></returns>
        public Tween RunAction()
        {
            for (int i = 0, length = _modifyList.Count; i < length; i++)
            {
                _executeList.Enqueue(_modifyList[i]);
            }
            _modifyList.Clear();
            ResetAll();
            _isPausing = false;
            return this;
        }
        /// <summary>
        /// Stop all actions in this tween and show the final result of this tween
        /// </summary>
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
            _countCurrentRepeatTime = 0;
        }
        /// <summary>
        /// Start run this tween
        /// </summary>
        public void StartAll()
        {
            RunAction();
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
                if (_childList != null)
                {
                    for (int i = 0, length = _childList.Count; i < length; i++)
                    {
                        _childList[i].FinishProgressRightNow();
                    }
                }
                if (_executeList.Count > 0)
                {
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
                    if (_repeatTime == -1 || (_repeatTime > 0 && _countCurrentRepeatTime < _repeatTime))
                    {
                        if (_repeatTime > 0) _countCurrentRepeatTime++;
                        ResetAll();
                    }
                    else
                        _doneExecute = true;
                }

            }
            if (!_doneExecute && _countUp < _lastDuration)
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
