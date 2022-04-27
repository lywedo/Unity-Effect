using System;
using System.Collections;
using DG.Tweening;
using Metamesh;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

namespace UnityTemplateProjects
{
    public class ClickSpawnSphere : MonoBehaviour
    {
        Vector3 screenPosition; //将物体从世界坐标转换为屏幕坐标
        Vector3 mousePositionOnScreen; //获取到点击屏幕的屏幕坐标
        Vector3 mousePositionInWorld; //将点击屏幕的屏幕坐标转换为世界坐标
        private Vector3 _cachePos;
        public VisualEffect VisualEffect;
        public GameObject spPrefab;

        public SphereController[] sps;

        public int ShowIndex = 0;
        // public GameObject TestGo;


        private bool _instantiateLock = false;
        private void Update()
        {
            // TestGo.transform.position = MouseFollow();
            _cachePos = MouseFollow();
            
            if (Input.GetMouseButtonUp(0))
            {
                if (!_instantiateLock)
                {
                    _instantiateLock = true;
                    // var instantiate = Instantiate(spPrefab);
                    sps[ShowIndex].transform.position = _cachePos;
                    sps[ShowIndex].transform.localScale = Vector3.zero;
                    sps[ShowIndex].Scale();
                    // StartCoroutine(Scale(sps[ShowIndex]));
                    
                    // sps[ShowIndex].transform.localScale = Vector3.one;
                    ShowIndex++;
                    if (ShowIndex >= sps.Length)
                    {
                        ShowIndex = 0;
                    }
                    Debug.Log(_cachePos);
                    // instantiate.transform.position = _cachePos;
                    _instantiateLock = false;
                }
                
            }
        }

        IEnumerator Scale(GameObject go)
        {
            yield return new WaitForSeconds(go.transform.DOScale(Vector3.zero, 0.5f).Duration());
            go.transform.DOScale(Vector3.one, 1f);
            // yield return 0;
        }

        public Vector3 MouseFollow()
        {
            //获取鼠标在相机中（世界中）的位置，转换为屏幕坐标；
            screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            //获取鼠标在场景中坐标
            mousePositionOnScreen = Input.mousePosition;
            //让场景中的Z=鼠标坐标的Z
            mousePositionOnScreen.z = screenPosition.z;
            //将相机中的坐标转化为世界坐标
            mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
            //物体跟随鼠标移动
            //transform.position = mousePositionInWorld;
            //物体跟随鼠标X轴移动
            return new Vector3(mousePositionInWorld.x, mousePositionInWorld.y, mousePositionInWorld.z);
        }
    }
}