using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class ShopScroll : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private HorizontalLayoutGroup _horizontalLayoutGroup;
    [SerializeField] private float _lerpSpeed = 3;
    [SerializeField] private float _stopVelocityX = 200;
    [SerializeField] private float _minItemSize = 200;
    [SerializeField] private float _maxItemSize = 500;

    private bool _isInitialized;
    private bool _isDragging;
    private float _correctivePositionX;
    private RectTransform _content;
    private List<RectTransform> _items = new List<RectTransform>();

    private void Awake()
    {
        _content = _scrollRect.content;

        float center = -_scrollRect.viewport.transform.localPosition.x;
        _correctivePositionX = center - _maxItemSize;
        _horizontalLayoutGroup.padding = new RectOffset((int)center, (int)center, _horizontalLayoutGroup.padding.top, _horizontalLayoutGroup.padding.bottom);
    }

    private void Update()
    {
        if (_isInitialized == false)
        {
            return;
        }

        int nearestIndex = 0;
        float nearestDistance = float.MaxValue;
        float center = _scrollRect.transform.position.x;

        for (int i = 0; i < _items.Count; i++)
        {
            float itemDistance = Mathf.Abs(center - _items[i].position.x);

            if (itemDistance < nearestDistance)
            {
                nearestDistance = itemDistance;
                nearestIndex = i;
            }

            float size = Mathf.Lerp(_maxItemSize, _minItemSize, itemDistance / center);
            _items[i].sizeDelta = CalculateSize(_items[i].sizeDelta, size);
        }

        if (_isDragging == false)
        {
            if (Mathf.Abs(_scrollRect.velocity.x) < _stopVelocityX)
            {
                ScrollTo(nearestIndex);
            }
        }
    }

    public void Initialize(List<ItemView> items)
    {
        if (_isInitialized)
            throw new InvalidOperationException("Already initialized");

        items.ForEach(item => _items.Add((RectTransform)item.transform));
        _isInitialized = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDragging = true;
        _scrollRect.inertia = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;
    }

    private void ScrollTo(int index)
    {
        _scrollRect.inertia = false;

        RectTransform item = _items[index];
        float contentTargetPositionX = -1 * Mathf.Clamp(item.anchoredPosition.x - item.sizeDelta.x - _correctivePositionX, 0, _content.sizeDelta.x);
        Vector2 nextContentPosition = new Vector2(contentTargetPositionX, _content.anchoredPosition.y);

        _content.anchoredPosition = Vector2.Lerp(_content.anchoredPosition, nextContentPosition, _lerpSpeed * Time.deltaTime);
    }

    private Vector2 CalculateSize(Vector2 from, float to)
    {
        return Vector2.Lerp(from, Vector2.one * to, 0.5f);
    }
}
