using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    public class ContainerCreator : MonoBehaviour
    {
        public GunContainer gunContainer;


        public TextMeshProUGUI gunTypeText,gunIndex;
        public Image gunSprite;
        private void Start()
        {
            gunTypeText.text = gunContainer.gunTypeText;
            gunIndex.text = gunContainer.indexNumber;
            gunSprite.sprite = gunContainer.gunImage;
        }
    }
}
