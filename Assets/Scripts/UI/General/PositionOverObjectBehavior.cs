using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PositionOverObjectBehavior : MonoBehaviour {
	
	public Camera displayCamera;
	public Canvas displayCanvas;
	public Camera objectCamera;
	public Transform target;
	public Vector2 offset;
	bool initialized = false;
    
    public MonoBehaviour disableIfNoTarget;

	void Start() {
		if(displayCanvas != null && displayCamera != null && objectCamera != null) initialized = true;
	
	}

	public void Initialize(Canvas canvas, Transform _target, Vector2 _offset) {
        if (canvas != null) displayCanvas = canvas;
        
        if (displayCanvas == null) displayCanvas = GetComponentInParent<Canvas>();
		
		transform.localScale = Vector3.one;
		transform.localRotation = Quaternion.identity;
		
		if(displayCamera == null) displayCamera = displayCanvas.worldCamera ?? Camera.main;
		target = _target;
		offset = _offset;
		
		initialized = true;
	
	}

    void Update()
    {        
        Position();
    }

	// Update is called once per frame
	void FixedUpdate () {
        //if(RTSCamera.instance.followTarget != null) Position();
	}

    void Position()
    {
        if (disableIfNoTarget) disableIfNoTarget.enabled = (target != null);
        if (!initialized || !target) return;

        if (displayCanvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            PositionCamera();
        }
        else
        {
            PositionOverlay();
        }
    }

	void PositionCamera() {
		Vector2 targetScreenPos = objectCamera.WorldToScreenPoint(target.position);	
		
		Vector2 pos;		
		
		RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent as RectTransform, targetScreenPos + offset, displayCamera, out pos);
		
		Vector3 result = (displayCanvas.transform as RectTransform).TransformPoint(pos);
		transform.position = result;	
	}
	
	public void PositionOverlay() {
		Vector2 targetScreenPos = Camera.main.WorldToScreenPoint(target.position);	
		
		//Vector2 pos;		
		
		//RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent as RectTransform, targetScreenPos + offset, null, out pos);
		
		Vector3 result = targetScreenPos + offset;
		transform.position = result;
	}
}