using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;


namespace BoTing.GamePublic
{
    public class SliderView : UIComponentBase, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        /// <summary>
        /// 阈值，手势滑动启动多少距离启动滑动界面.
        /// The threshold for the dragged distance to start to slide the view.
        /// </summary>
        public float sliderThreshold = 30;

        /// <summary>
        /// 每隔多少秒滑动一次.
        /// </summary>
        public float sliderInterval = 1;

        /// <summary>
        /// 3个预设的面板，用来放内容和执行滑动。
        /// The three slider component panels which are used to contain and slide the content images.
        /// </summary>
        private List<RectTransform> sliderPanels = new List<RectTransform>();

        protected RectTransform PrevPanel
        {
            get { return sliderPanels[0]; }
        }

        protected RectTransform CurrentPanel
        {
            get { return sliderPanels[1]; }
        }

        protected RectTransform NextPanel
        {
            get { return sliderPanels[2]; }
        }

        private Vector3 prevFixedPosition;
        private Vector3 currentFixedPosition;
        private Vector3 nextFixedPosition;
        private float spacing;

        private Vector2 beginDragPosition;

        private List<LTDescr> ltIds = new List<LTDescr>();

        protected virtual bool IsSlidable
        {
            get
            {
                return false;
            }
        }
        protected bool isSliding = false;

        protected override void OnAwakeView()
        {
            base.OnAwakeView();
            if (gameObject.transform.childCount > 3)
            {
                sliderPanels.Add(gameObject.transform.GetChild(0) as RectTransform);
                sliderPanels.Add(gameObject.transform.GetChild(1) as RectTransform);
                sliderPanels.Add(gameObject.transform.GetChild(2) as RectTransform);
            }
        }

        // Use this for initialization
        protected override void OnStartView()
        {
            base.OnStartView();

            prevFixedPosition = PrevPanel.transform.localPosition;
            currentFixedPosition = CurrentPanel.transform.localPosition;
            nextFixedPosition = NextPanel.transform.localPosition;
            spacing = currentFixedPosition.x - prevFixedPosition.x - CurrentPanel.rect.width;
        }
        /// <summary>
        /// 调用此方法来初始化视图数据.
        /// Call this method to initialize the data source.
        /// </summary>
        protected virtual void InitializeData()
        {
        }

        /// <summary>
        /// 调用此方法来初始化视图.
        /// Call this method to initialize the tree view.
        /// </summary>
        protected virtual void InitializeView()
        {
            ResetSliderPanelsPosition();
            if (IsSlidable)
            {
                StartSliderTimer();
            }
        }

        /// <summary>
        /// 滑动启动时调用此函数，请在这个函数里准备好即将显示的界面.
        /// This method is called when the sliding process is starting.
        /// Please prepare the View which is being sliding to.
        /// </summary>
        /// <param name="toLeft"></param>
        protected virtual void OnBeginSlide(bool toLeft)
        {
        }

        /// <summary>
        /// 滑动结束时调用此函数.
        /// This method is called when the sliding process is ended.
        /// </summary>
        /// <param name="toLeft"></param>
        protected virtual void OnEndSlide(bool toLeft)
        {
        }

        /// <summary>
        /// 重写此方法来处理当前View点击事件.
        /// Override this method to handle the Click event.    
        /// </summary>
        protected virtual void OnViewClick()
        {

        }

        #region EventSystem
        public void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag -- IN");
            beginDragPosition = eventData.position;
        }

        public void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
        {
            //Debug.Log("OnDrag -- IN");
        }

        public void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
        {
            Debug.Log("OnEndDrag -- IN");

            var delta = eventData.position.x - beginDragPosition.x;
            if (!IsSlidable || Mathf.Abs(delta) < sliderThreshold)
            {
                return;
            }

            if (delta <= 0)
            {
                SlideLeft();
            }
            else
            {
                SlideRight();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnViewClick();
        }
        #endregion EventSystem


        protected void SlideLeft()
        {
            DoSlide(true);
        }

        protected void SlideRight()
        {
            DoSlide(false);
        }

        protected void DoSlide(bool toLeft)
        {
            if (!IsSlidable || isSliding)
            {
                return;
            }

            isSliding = true;
            StopAnimations();

            OnBeginSlide(toLeft);

            DoSlideAnimation(PrevPanel, toLeft);
            DoSlideAnimation(CurrentPanel, toLeft);
            DoSlideAnimation(NextPanel, toLeft, () =>
            {
                isSliding = false;

                SortSliderPanels(toLeft);
                ResetSliderPanelsPosition();

                OnEndSlide(toLeft);
            });
        }

        protected virtual void DoSlideAnimation(RectTransform rectTransform, bool toLeft, System.Action onComplete = null, System.Action<float> onUpdate = null)
        {
            var distance = toLeft ? -rectTransform.rect.width : rectTransform.rect.width;
            var to = new Vector3(rectTransform.localPosition.x + distance, rectTransform.localPosition.y, rectTransform.localPosition.z);
            var ltId = LeanTween.moveLocal(rectTransform.gameObject, to, 1.0f).setEase(LeanTweenType.easeOutCirc).setOnComplete(onComplete).setOnUpdate(onUpdate);
            ltIds.Add(ltId);
        }

        protected void SortSliderPanels(bool toLeft)
        {
            if(toLeft)
            {
                var prevPanel = sliderPanels[0];
                sliderPanels.RemoveAt(0);
                sliderPanels.Add(prevPanel);
            }
            else
            {
                var nextPanel = sliderPanels[sliderPanels.Count - 1];
                sliderPanels.RemoveAt(sliderPanels.Count - 1);
                sliderPanels.Insert(0, nextPanel);
            }
        }

        protected void ResetSliderPanelsPosition()
        {
            PrevPanel.transform.localPosition = prevFixedPosition;
            CurrentPanel.transform.localPosition = currentFixedPosition;
            NextPanel.transform.localPosition = nextFixedPosition;
        }

        protected void StopAnimations()
        {
            foreach (var id in ltIds)
            {
                id.cancel();
            }
            ltIds.Clear();
        }

        protected void StartSliderTimer()
        {
            StartCoroutine(DoSliderTimer());
        }

        protected IEnumerator DoSliderTimer()
        {
            while (true)
            {
                yield return new WaitForSeconds(sliderInterval);
                SlideLeft();
            }
        }
    }
}