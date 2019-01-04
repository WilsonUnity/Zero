﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zero;
using IL.Zero;

namespace IL.Demo
{
    class HelpWin : AView
    {
        UnityEngine.Object[] _tipSpriteList;
        String[] _tipStrList;        
        Image _tip;
        Text _textTip;
        Button _btnClose;

        int _page = 0;

        protected override void OnData(object data)
        {
            
        }

        protected override void OnDisable()
        {
            UIEventListener.Get(GameObject).onClick -= ShowNextPage;
            _btnClose.onClick.RemoveListener(Destroy);
            
        }

        protected override void OnEnable()
        {
            UIEventListener.Get(GameObject).onClick += ShowNextPage;
            _btnClose.onClick.AddListener(Destroy);
        }

        protected override void OnInit()
        {
            _tip = GameObject.GetComponent<Image>();
            var resData = GameObject.GetComponent<ObjectBindingData>();
            var list = resData.Find("tips").Value.list;
            _tipSpriteList = list;
            var stringData = GameObject.GetComponent<StringBindingData>();
            _tipStrList = stringData.Find("tips").Value.list;
            _btnClose = GetChildComponent<Button>("BtnClose");
            _textTip = GetChildComponent<Text>("TextTip");
            
            //gameObject.transform.localScale = Vector3.zero;
            //gameObject.transform.DOScale(1, 0.5f).SetEase(Ease.OutBounce);
        }

        void ShowNextPage(PointerEventData e)
        {
            _page++;
            if(_page >= _tipStrList.Length)
            {
                return;
            }
            else if (_page < 0)
            {
                _page = 0;
            }

            _tip.sprite = Texture2DUtil.ToSprite(_tipSpriteList[_page] as Texture2D);
            _textTip.text = _tipStrList[_page];
        }
    }
}
