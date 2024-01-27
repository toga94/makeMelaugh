using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using CodeMonkey.Utils;

[Serializable]
public class Point {
    public GameObject target;
    public GameObject pointer;
    public RectTransform rectTransform;

    public Point(GameObject target, GameObject pointer, RectTransform rectTransform)
    {
        this.target = target;
        this.pointer = pointer;
        this.rectTransform = rectTransform;
    }
}

public class OffScreenPointer : MonoBehaviour
{
    [SerializeField] private GameObject[] targets;
    [SerializeField] private List<Point> points;
    [SerializeField] private LeanGameObjectPool objectPool;
    [SerializeField] private Camera cam;
    void Start()
    {
        cam = Camera.main;
        foreach (var target in targets)
        {
            var pointer = objectPool.Spawn(target.transform.position, Quaternion.identity, transform);
            points.Add(new Point(target, pointer, pointer.GetComponent<RectTransform>()));
        }
    }

    void Update()
    {
        foreach (var point in points)
        {
            Vector3 toPosition = point.target.transform.position;
            Vector3 fromPosition = cam.transform.position;
            fromPosition.z = 0f;
            Vector3 dir = (toPosition - fromPosition).normalized;
            float angle = UtilsClass.GetAngleFromVectorFloat(dir);
            point.rectTransform.localEulerAngles = new Vector3(0, 0, angle);
            Vector3 targetPositionScreenPoint = cam.WorldToScreenPoint(point.target.transform.position);
            bool isOffScreen = targetPositionScreenPoint.x <= 0 || targetPositionScreenPoint.x >= Screen.width 
                || targetPositionScreenPoint.y <= 0 || targetPositionScreenPoint.y >= Screen.height;

            if (isOffScreen)
            {
                point.pointer.SetActive(true);
                Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
                if (cappedTargetScreenPosition.x <= 0) cappedTargetScreenPosition.x = 0;
                if (cappedTargetScreenPosition.x >= Screen.width) cappedTargetScreenPosition.x = Screen.width;
                if (cappedTargetScreenPosition.y <= 0) cappedTargetScreenPosition.y = 0;
                if (cappedTargetScreenPosition.y >= Screen.height) cappedTargetScreenPosition.y = Screen.height;


                point.rectTransform.position = cappedTargetScreenPosition;

            }
            else {
                point.pointer.SetActive(false);
            }
   
        }
    }
}
