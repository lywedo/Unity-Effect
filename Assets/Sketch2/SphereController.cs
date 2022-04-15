using DG.Tweening;
using UnityEngine;
using UnityEngine.Video;

namespace UnityTemplateProjects
{
    public class SphereController : MonoBehaviour
    {
        public VideoPlayer Player;
        public RectTransform canvasRectTransform;

        public void Scale()
        {
            transform.DOScale(Vector3.one, 1).onComplete = () =>
            {
                Play();
            };
            
        }
        public void Play()
        {
            Player.transform.localPosition = WorldToUgui(transform.position);
            Player.transform.DOScale(Vector3.one, 1);
            // Player.transform.position = transform.position;
            Player.Prepare();
            Player.Play();
        }
        
        public Vector2 WorldToUgui(Vector3 position)
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(position); //世界坐标转换为屏幕坐标
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            screenPoint -= screenSize / 2; //将屏幕坐标变换为以屏幕中心为原点
            Vector2 anchorPos = screenPoint / screenSize * canvasRectTransform.sizeDelta; //缩放得到UGUI坐标
            return anchorPos;
        }
    }
}