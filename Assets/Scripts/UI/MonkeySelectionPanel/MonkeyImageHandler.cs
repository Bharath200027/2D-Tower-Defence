using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ServiceLocator.UI
{
    public class MonkeyImageHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler
    {
        private Image monkeyImage;
        private MonkeyCellController owner;
        private Sprite spriteToSet;
        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        private Vector3 originalPosition;
        private Vector3 originalAnchoredPosition;
        private Vector3 mouseStartPoint;
        private bool isDragging;

        public void ConfigureImageHandler(Sprite spriteToSet, MonkeyCellController owner)
        {
            this.spriteToSet = spriteToSet;
            this.owner = owner;
        }

        void Update()
        {
            if (!isDragging)
            {
                return;
            }

        }

        public void OnPointerMove(Vector3 position)
        {
            Debug.Log(position);
        }

        private void Awake()
        {
            monkeyImage = GetComponent<Image>();
            monkeyImage.sprite = spriteToSet;
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            originalPosition = rectTransform.position;
            originalAnchoredPosition = rectTransform.anchoredPosition;
        }


        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
            rectTransform.anchoredPosition += eventData.delta;
            owner.MonkeyDraggedAt(rectTransform.position);
        }


        public void OnEndDrag(PointerEventData eventData)
        {
            ResetMonkey();
            owner.MonkeyDroppedAt(eventData.position);
        }

        private void ResetMonkey()
        {
            monkeyImage.color = new Color(1, 1, 1, 1f);
            rectTransform.position = originalPosition;
            rectTransform.anchoredPosition = originalAnchoredPosition;
            GetComponent<LayoutElement>().enabled = false;
            GetComponent<LayoutElement>().enabled = true;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            monkeyImage.color = new Color(1, 1, 1, 0.6f);
        }
    }
}