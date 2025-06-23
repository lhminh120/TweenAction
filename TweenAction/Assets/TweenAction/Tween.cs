using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TweenAction
{
    public class Tween : MonoBehaviour//SingletonOneScene<Tween>
    {
        public static Tween Instance;
        private void Awake()
        {
            Instance = this;
        }
        //#region Basic
        //private static List<Tween> _listAllTweens = new List<Tween>();
        //public static Tween Target(GameObject go)
        //{
        //    Tween tween = go.GetComponent<Tween>();
        //    if (tween == null)
        //    {
        //        tween = go.AddComponent<Tween>();
        //        tween.SetUpComponent();
        //        _listAllTweens.Add(tween);
        //    }_duration
        //    return tween;
        //}
        //public static void ClearTarget(GameObject go)
        //{
        //    Tween tween = go.GetComponent<Tween>();
        //    if (tween != null)
        //    {
        //        tween.StopOrder();
        //    }
        //}
        //public static void ClearAll()
        //{
        //    for (int i = 0, length = _listAllTweens.Count; i < length; i++)
        //    {
        //        _listAllTweens[i].StopOrder();
        //    }
        //}
        //public void Start()
        //{
        //    RunOrder();
        //}
        //private void EndOrder(Action callBackWhenDone = null)
        //{
        //    _currentOrder = null;
        //    callBackWhenDone?.Invoke();
        //    RunOrder();
        //}
        //private void RunOrder()
        //{
        //    if (_listOrder.Count <= 0) return;
        //    if (_currentOrder == null && _listOrder.Count > 0)
        //    {
        //        _currentOrder = _listOrder.Dequeue();
        //        StartCoroutine(_currentOrder);
        //    }
        //}
        //private Transform _trans;
        //private SpriteRenderer _spriteRenderer;
        //private Queue<IEnumerator> _listOrder = new Queue<IEnumerator>();
        //private IEnumerator _currentOrder;
        private WaitForEndOfFrame _waitForEndOfFrame = new WaitForEndOfFrame();
        //public void StopOrder()
        //{
        //    StopCoroutine(_currentOrder);
        //    _currentOrder = null;
        //    _listOrder.Clear();
        //}
        //public void SetUpComponent()
        //{
        //    _trans = transform;
        //    _spriteRenderer = GetComponent<SpriteRenderer>();
        //}
        //#endregion
        #region Move
        private Vector3 _moveToDir;
        public IEnumerator IEMoveTo(Transform trans, Vector3 position, float moveTime, Action onDone)
        {
            _moveToDir = (position - trans.position) / moveTime;
            var time = 0f;
            while (time < moveTime)
            {
                time += Time.deltaTime;
                trans.position += _moveToDir * Time.deltaTime;
                yield return _waitForEndOfFrame;
            }
            trans.position = position;
            onDone();
        }

        public IEnumerator IEMoveToLocal(Transform trans, Vector3 position, float moveTime, Action onDone)
        {
            _moveToDir = (position - trans.localPosition) / moveTime;
            var time = 0f;
            while (time < moveTime)
            {
                time += Time.deltaTime;
                trans.localPosition += _moveToDir * Time.deltaTime;
                yield return _waitForEndOfFrame;
            }
            trans.localPosition = position;
            onDone();
        }


        public IEnumerator IEMoveStyleJumpFallSpeedStyle(Transform trans, Vector3 acceleration, Vector3 velocity, float moveTime, Vector3 destination, Action onDone)
        {
            var time = 0f;
            var firstPostion = trans.position;
            while (time < moveTime)
            {
                time += Time.deltaTime;
                var currentTime = 0.5f * time * time;
                trans.position = firstPostion + velocity * time + acceleration * currentTime;
                yield return _waitForEndOfFrame;
            }
            trans.position = destination;
            onDone();
        }
        #endregion
        #region Scale
        public IEnumerator IELocalScaleTo(Transform trans, Vector3 localScale, float moveTime, Action onDone)
        {
            _moveToDir = (localScale - trans.localScale) / moveTime;
            var time = 0f;
            while (time < moveTime)
            {
                time += Time.deltaTime;
                trans.localScale += _moveToDir * Time.deltaTime;
                yield return _waitForEndOfFrame;
            }
            trans.localScale = localScale;
            onDone();
        }
        #endregion
        #region Invoke
        public IEnumerator IEDelayInvoke(float delayTime, Action onDone)
        {
            yield return new WaitForSeconds(delayTime);
            onDone();
        }
        #endregion
        #region Sprite Scale
        private Vector2 _spriteScale = Vector2.zero;
        public IEnumerator IESpriteRendererScaleX(SpriteRenderer spriteRenderer, float scaleX, float scaleTime, Action onDone)
        {
            _spriteScale = spriteRenderer.size;
            _spriteScale.x = scaleX;
            var tempScale = (_spriteScale - spriteRenderer.size) / scaleTime;
            var time = 0f;
            while (time < scaleTime)
            {
                time += Time.deltaTime;
                spriteRenderer.size += tempScale * Time.deltaTime;
                yield return _waitForEndOfFrame;
            }
            spriteRenderer.size = _spriteScale;
            onDone();
        }
        public IEnumerator IESpriteRendererScaleY(SpriteRenderer spriteRenderer, float scaleY, float scaleTime, Action onDone)
        {
            _spriteScale = spriteRenderer.size;
            _spriteScale.y = scaleY;
            var tempScale = (_spriteScale - spriteRenderer.size) / scaleTime;
            var time = 0f;
            while (time < scaleTime)
            {
                time += Time.deltaTime;
                spriteRenderer.size += tempScale * Time.deltaTime;
                yield return _waitForEndOfFrame;
            }
            spriteRenderer.size = _spriteScale;
            onDone();
        }
        public IEnumerator IESpriteRendererScale(SpriteRenderer spriteRenderer, Vector2 scale, float scaleTime, Action onDone)
        {
            _spriteScale = scale;
            var tempScale = (_spriteScale - spriteRenderer.size) / scaleTime;
            var time = 0f;
            while (time < scaleTime)
            {
                time += Time.deltaTime;
                spriteRenderer.size += tempScale * Time.deltaTime;
                yield return _waitForEndOfFrame;
            }
            spriteRenderer.size = _spriteScale;
            onDone();
        }
        #endregion
        //private void OnDisable()
        //{
        //    StopOrder();
        //}
        public void DestroyOrder(TweenOrder order)
        {
            if (_listOrder.ContainsKey(order.GetGameObject()))
            {
                var go = order.GetGameObject();
                var tempOrder = _listOrder[go][_listOrder[go].Count - 1];
                _listOrder[go][_listOrder[go].Count - 1] = _listOrder[go][order.GetID()];
                _listOrder[go][order.GetID()] = tempOrder;
                _listOrder[go][order.GetID()].SetID(order.GetID());
                tempOrder = _listOrder[go][_listOrder[go].Count - 1];
                _listOrder[go].RemoveAt(_listOrder[go].Count - 1);
                tempOrder.StopOrder();
                _poolOrder.Push(tempOrder);
            }
        }
        public void StopTween(GameObject go)
        {
            if (_listOrder.ContainsKey(go))
            {
                for (int i = 0, length = _listOrder[go].Count; i < length; i++)
                {
                    _listOrder[go][i].StopOrder();
                    _poolOrder.Push(_listOrder[go][i]);
                }
                _listOrder[go].Clear();
            }
        }
        private Dictionary<GameObject, List<TweenOrder>> _listOrder = new Dictionary<GameObject, List<TweenOrder>>();
        private Stack<TweenOrder> _poolOrder = new Stack<TweenOrder>();
        private TweenOrder SpawnOrder(GameObject go)
        {
            TweenOrder order;
            if (_poolOrder.Count > 0)
            {
                order = _poolOrder.Pop();
                order.SetOrderData(go);
            }
            else
            {
                order = new TweenOrder(go);
            }

            return order;
        }
        public TweenOrder Target(GameObject go)
        {
            var order = SpawnOrder(go);
            if (_listOrder.ContainsKey(go))
            {
                order.SetID(_listOrder[go].Count);
                _listOrder[go].Add(order);
            }
            else
            {
                order.SetID(0);
                _listOrder.Add(go, new List<TweenOrder>() { order });
            }
            return order;
        }
        public void StartOrder(IEnumerator order) => StartCoroutine(order);
        public void StopOrder(IEnumerator order) => StopCoroutine(order);
    }
    public class TweenOrder
    {
        private int _id;
        private GameObject _go;
        private Transform _trans;
        private SpriteRenderer _spriteRenderer;
        private IEnumerator _currentOrder;
        private Queue<IEnumerator> _listOrder = new Queue<IEnumerator>();
        public TweenOrder(GameObject go)
        {
            SetOrderData(go);
        }
        public void SetOrderData(GameObject go)
        {
            _go = go;
            _trans = go.transform;
            _spriteRenderer = go.GetComponent<SpriteRenderer>();
        }
        public void SetID(int id) => _id = id;
        public int GetID() => _id;
        public GameObject GetGameObject() => _go;
        public void Start() => RunOrder();
        private void EndOrder(Action callBackWhenDone = null)
        {
            _currentOrder = null;
            callBackWhenDone?.Invoke();
            RunOrder();
        }
        private void RunOrder()
        {
            if (_listOrder.Count <= 0) Tween.Instance.DestroyOrder(this);
            if (_currentOrder == null && _listOrder.Count > 0)
            {
                _currentOrder = _listOrder.Dequeue();
                Tween.Instance.StartOrder(_currentOrder);
            }
        }
        public void StopTween()
        {
            Tween.Instance.StopTween(_go);
        }
        public void StopOrder()
        {
            if (_currentOrder != null)
            {
                Tween.Instance.StopOrder(_currentOrder);
            }
            _currentOrder = null;
            _listOrder.Clear();
        }
        #region Move
        public Transform GetTransform() => _trans;
        public TweenOrder MoveTo(Vector3 position, float moveTime, Action callBackWhenDone = null)
        {
            var newOrder = Tween.Instance.IEMoveTo(_trans, position, moveTime, () => EndOrder(callBackWhenDone));
            if (newOrder != null) _listOrder.Enqueue(newOrder);
            return this;
        }
        public TweenOrder MoveToLocal(Vector3 position, float moveTime, Action callBackWhenDone = null)
        {
            var newOrder = Tween.Instance.IEMoveToLocal(_trans, position, moveTime, () => EndOrder(callBackWhenDone));
            if (newOrder != null) _listOrder.Enqueue(newOrder);
            return this;
        }
        public enum MoveStyle
        {
            JumpFallSpeedStyle,
        }
        public TweenOrder MoveTo(Vector3 position, float moveTime, MoveStyle moveStyle, Action callBackWhenDone = null)
        {
            var acceleration = (_trans.position - position) * (2 / (moveTime * moveTime));
            var velocity = new Vector3(acceleration.x * (-1f) * moveTime, acceleration.y * (-1f) * moveTime, acceleration.z * (-1f) * moveTime);
            switch (moveStyle)
            {
                case MoveStyle.JumpFallSpeedStyle:
                    var newOrder = Tween.Instance.IEMoveStyleJumpFallSpeedStyle(_trans, acceleration, velocity, moveTime, position, () => EndOrder(callBackWhenDone));
                    if (newOrder != null) _listOrder.Enqueue(newOrder);
                    break;
                default:
                    break;
            }
            return this;
        }
        #endregion
        #region Scale
        public TweenOrder LocalScaleTo(Vector3 localScale, float localScaleTime, Action callBackWhenDone = null)
        {
            var newOrder = Tween.Instance.IELocalScaleTo(_trans, localScale, localScaleTime, () => EndOrder(callBackWhenDone));
            if (newOrder != null) _listOrder.Enqueue(newOrder);
            return this;
        }
        #endregion
        #region Invoke
        public TweenOrder DelayInvoke(float delayTime, Action callBackWhenDone = null)
        {
            var newOrder = Tween.Instance.IEDelayInvoke(delayTime, () => EndOrder(callBackWhenDone));
            if (newOrder != null) _listOrder.Enqueue(newOrder);
            return this;
        }
        #endregion
        #region Sprite Scale
        private Vector2 _spriteScale = Vector2.zero;
        public TweenOrder SpriteRendererScaleX(float scaleX, float scaleTime, Action callBackWhenDone = null)
        {
            var newOrder = Tween.Instance.IESpriteRendererScaleX(_spriteRenderer, scaleX, scaleTime, () => EndOrder(callBackWhenDone));
            if (newOrder != null) _listOrder.Enqueue(newOrder);
            return this;
        }
        public TweenOrder SpriteRendererScaleY(float scaleY, float scaleTime, Action callBackWhenDone = null)
        {
            var newOrder = Tween.Instance.IESpriteRendererScaleY(_spriteRenderer, scaleY, scaleTime, () => EndOrder(callBackWhenDone));
            if (newOrder != null) _listOrder.Enqueue(newOrder);
            return this;
        }
        public TweenOrder SpriteRendererScale(Vector2 scale, float scaleTime, Action callBackWhenDone = null)
        {
            var newOrder = Tween.Instance.IESpriteRendererScale(_spriteRenderer, scale, scaleTime, () => EndOrder(callBackWhenDone));
            if (newOrder != null) _listOrder.Enqueue(newOrder);
            return this;
        }
        #endregion
    }
}
