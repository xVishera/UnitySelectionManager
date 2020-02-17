using Assets.Engine.Enums;
using Assets.Engine.Scriptables;
using Assets.Engine.Structs;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Engine.Managers
{
#pragma warning disable IDE0044 // Add readonly modifier
#pragma warning disable IDE0051 // Remove unused private members
#pragma warning disable CS0414 // Value never used
#pragma warning disable CS0649 // never assigned
    public class SelectionManager : MonoBehaviour
    {
        #region Private variables
        [SerializeField] private TMP_Text title = null;
        [SerializeField] private MapScriptable[] maps = null;

        [SerializeField] private Image leftThumbnail;
        [SerializeField] private Image midThumbnail;
        [SerializeField] private Image rightThumbnail;

        private Animator animator;
        private Transform currentCenter;
        private int currentCenterId;
        private int leftThumbnailId;
        private int midThumbnailId;
        private int rightThumbnailId;
        private SelectorIndexer indexer = new SelectorIndexer().Init();
        #endregion Private variables

        #region Public methods
        public void ChangeThumbnail(SelectorPositionEnum selector)
        {
            switch (selector)
            {
                case SelectorPositionEnum.LeftBackward:
                    indexer.LeftIndex = OnChangeThumbnail(leftThumbnail, indexer.GetLowestIndex, -1);
                    break;
                case SelectorPositionEnum.LeftForward:
                    indexer.LeftIndex = OnChangeThumbnail(leftThumbnail, indexer.GetMaxIndex, 1);
                    break;
                case SelectorPositionEnum.MidBackward:
                    indexer.MidIndex = OnChangeThumbnail(midThumbnail, indexer.GetLowestIndex, -1);
                    break;
                case SelectorPositionEnum.MidForward:
                    indexer.MidIndex = OnChangeThumbnail(midThumbnail, indexer.GetMaxIndex, 1);
                    break;
                case SelectorPositionEnum.RightBackward:
                    indexer.RightIndex = OnChangeThumbnail(rightThumbnail, indexer.GetLowestIndex, -1);
                    break;
                case SelectorPositionEnum.RightForward:
                    indexer.RightIndex = OnChangeThumbnail(rightThumbnail, indexer.GetMaxIndex, 1);
                    break;
            }
        }

        public void GoForward()
        {
            animator.SetTrigger("Forward");
            SwitchCurrentCenter(DirectionEnum.Forward);
        }

        public void GoBackward()
        {
            animator.SetTrigger("Backward");
            SwitchCurrentCenter(DirectionEnum.Backward);
        }
        #endregion Public methods

        #region Private methods
        private int OnChangeThumbnail(Image img, Func<int> func, int aux)
        {
            int index = func.Invoke() + aux;
            index = ClampIndex(index);
            index = GetNotUsedIndex(aux, index);
            img.sprite = maps[index].Thumbnail;
            return index;
        }

        private int GetNotUsedIndex(int aux, int index)
        {
            while (index == indexer.LeftIndex || index == indexer.MidIndex || index == indexer.RightIndex)
            {
                index += aux;
                index = ClampIndex(index);
            }

            return index;
        }

        private int ClampIndex(int index)
        {
            if (index < 0)
            {
                index = maps.Length - 1;
            }

            if (index > maps.Length - 1)
            {
                index = 0;
            }

            return index;
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
            currentCenter = midThumbnail.transform;
            InitIds();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                GoBackward();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                GoForward();
            }

            currentCenter.SetAsLastSibling();
        }

        private void InitIds()
        {
            currentCenterId = currentCenter.transform.GetInstanceID();
            leftThumbnailId = leftThumbnail.transform.GetInstanceID();
            midThumbnailId = midThumbnail.transform.GetInstanceID();
            rightThumbnailId = rightThumbnail.transform.GetInstanceID();
        }

        private void SwitchCurrentCenter(DirectionEnum direction)
        {
            if (currentCenterId == midThumbnailId)
            {
                currentCenter = (direction == DirectionEnum.Backward ? leftThumbnail : rightThumbnail).transform;
            }
            else if (currentCenterId == leftThumbnailId)
            {
                currentCenter = (direction == DirectionEnum.Backward ? rightThumbnail : midThumbnail).transform;
            }
            else if (currentCenterId == rightThumbnailId)
            {
                currentCenter = (direction == DirectionEnum.Backward ? midThumbnail : leftThumbnail).transform;
            }

            currentCenterId = currentCenter.transform.GetInstanceID();
        }
        #endregion Private methods
    }
}
