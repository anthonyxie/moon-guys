using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public bool lineUpdating = true;
    public float PercentHead = 0.1f;
    public Vector3 forceArrow;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lineUpdating) {

            Vector3 ArrowOrigin = this.transform.localPosition;
            Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 ArrowTarget =  new Vector3(screenToWorld.x, screenToWorld.y, 0) - this.transform.parent.position;

            float mag = ArrowTarget.magnitude;
            if (mag > 6) {
                ArrowTarget = ArrowTarget.normalized * 6;
            }

            forceArrow = ArrowTarget - ArrowOrigin;
            lineRenderer.widthCurve = new AnimationCurve( new Keyframe(0, 0.4f)
             , new Keyframe(0.999f - PercentHead, 0.4f)  // neck of arrow
             , new Keyframe(1 - PercentHead, 1f)  // max width of arrow head
             , new Keyframe(1, 0f));  // tip of arrow
            lineRenderer.SetPositions(new Vector3[] {
              ArrowOrigin
              , Vector3.Lerp(ArrowOrigin, ArrowTarget, 0.999f - PercentHead)
              , Vector3.Lerp(ArrowOrigin, ArrowTarget, 1 - PercentHead)
              , ArrowTarget });
        }
    }
}
