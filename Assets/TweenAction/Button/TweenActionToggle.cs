using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace TweenAction
{
    public class TweenActionToggle : MonoBehaviour, IPointerDownHandler
    {

        [Header("Click Effect Setting")]
        [SerializeField] private Tween[] _onToggleOn;
        [SerializeField] private Tween[] _onToggleOff;
        [Header("Click Event Setting")]
        [SerializeField] private UnityEvent _onToggleOnEvent;
        [SerializeField] private UnityEvent _onToggleOffEvent;
        [SerializeField] private bool _isOn = true;
        private void StartSpecAction(Tween[] listAction)
        {
            for (int i = 0, length = listAction.Length; i < length; i++)
            {
                listAction[i].StartAll();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isOn = !_isOn;
            ActionToggle();
        }
        public void SetupToggleValue(bool isOn)
        {
            _isOn = isOn;
            StartSpecAction(_isOn ? _onToggleOn : _onToggleOff);
        }
        public bool GetToggleState() => _isOn;
        private void ActionToggle()
        {
            StartSpecAction(_isOn ? _onToggleOn : _onToggleOff);


            if (_isOn)
                _onToggleOnEvent?.Invoke();
            else
                _onToggleOffEvent?.Invoke();
        }
    }

}
