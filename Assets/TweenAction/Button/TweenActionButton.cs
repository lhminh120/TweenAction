using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TweenAction
{
    public class TweenActionButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public enum ButtonAction
        {
            DOWN,
            UP,
            ENTER,
            EXIT,
            NONE
        }
        [Header("Click Effect Setting")]
        [SerializeField] private Tween[] _actionOnDowns;
        [SerializeField] private Tween[] _actionOnUps;
        [SerializeField] private Tween[] _actionOnEnters;
        [SerializeField] private Tween[] _actionOnExits;
        private bool _pressing = false;
        [Header("Click Effect Event")]
        public UnityEvent _onDown;
        public UnityEvent _onClick;
        public UnityEvent _onUp;
        public UnityEvent _onLongPress;
        private Tween[] _latestActions = null;

        public void OnPointerUp(PointerEventData eventData)
        {
            _pressing = false;
            _onUp?.Invoke();
            ActionWithButton(ButtonAction.UP);
            // if (_soundOnButtonUp != SoundLibrary.SoundEffectName.NONE)
            // {
            //     if (_useOnlyOneSoundEffect)
            //     {
            //         GameManager.Instance._soundManager.PlayEffectSoundOnlyOne(_soundOnButtonUp);
            //     }
            //     else
            //     {
            //         GameManager.Instance._soundManager.PlayEffectSound(_soundOnButtonUp);
            //     }

            // }
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            _onClick?.Invoke();
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            _pressing = true;
            _onDown?.Invoke();
            ActionWithButton(ButtonAction.DOWN);

        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            ActionWithButton(ButtonAction.ENTER);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            ActionWithButton(ButtonAction.EXIT);
        }
        private void Update()
        {
            if (_pressing)
            {
                _onLongPress?.Invoke();
            }
        }
        private void ActionWithButton(ButtonAction buttonAction)
        {
            if (_latestActions != null) StopSpecAction(_latestActions);
            if (buttonAction == ButtonAction.DOWN)
            {
                if (_actionOnDowns != null) StartSpecAction(_actionOnDowns);
                _latestActions = _actionOnDowns;
            }
            if (buttonAction == ButtonAction.UP)
            {
                if (_actionOnUps != null) StartSpecAction(_actionOnUps);
                _latestActions = _actionOnUps;
            }
            if (buttonAction == ButtonAction.ENTER)
            {
                if (_actionOnEnters != null) StartSpecAction(_actionOnEnters);
                _latestActions = _actionOnEnters;
            }
            if (buttonAction == ButtonAction.EXIT)
            {
                if (_actionOnExits != null) StartSpecAction(_actionOnExits);
                _latestActions = _actionOnExits;
            }
        }
        private void StopSpecAction(Tween[] listAction)
        {
            for (int i = 0, length = listAction.Length; i < length; i++)
            {
                listAction[i].StopAll();
            }
        }
        private void StartSpecAction(Tween[] listAction)
        {
            for (int i = 0, length = listAction.Length; i < length; i++)
            {
                listAction[i].StartAll();
            }
        }
    }

}
