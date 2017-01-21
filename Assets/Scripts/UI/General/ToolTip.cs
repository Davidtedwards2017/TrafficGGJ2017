using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToolTip : Singleton<ToolTip>
{	

	public Text text;
	public CanvasGroup canvasGroup;

	public Vector3 offset;
    
	// Use this for initialization
	void Start () {
		if(!text) text = GetComponent<Text>();
		if(!canvasGroup) canvasGroup = GetComponent<CanvasGroup>();
		Clear();
	}
	
	// Update is called once per frame
	void Update () {
        Position();
        
	}

    void Position()
    {
        if (!(canvasGroup.alpha > 0)) return;

        transform.SetAsLastSibling();

        Vector3 mousePos = Input.mousePosition;

        //Vector3 targetScreenPos = mousePos + offset;
        Vector3 targetScreenPos = mousePos;

        Vector2 size = text.rectTransform.sizeDelta;
        offset = new Vector2(size.x * 0.5f + 10f, size.y * 0.5f + 10f);

        if (targetScreenPos.x + size.x + 10f > Screen.width)
        {
            offset -= new Vector3(size.x + 20f, 0f, 0f);
        }

        if (targetScreenPos.y + size.y + 10f > Screen.height)
        {
            offset -= new Vector3(0f, size.y + 20f, 0f);
        }

        Vector2 pos;
               
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent as RectTransform, targetScreenPos + offset, UIManager.instance.uiCamera, out pos);

        Vector3 result = (UIManager.instance.uiCanvas.transform as RectTransform).TransformPoint(pos);
        transform.position = result;
    }

    public static void Display(string message)
    {
        instance.text.text = message;
        //instance.offset = newOffset;
        
        //instance.canvasGroup.SetInteractive(true);
        instance.canvasGroup.Show();
    }

	public static void Clear() {
        //instance.canvasGroup.SetInteractive(false);
        instance.canvasGroup.Hide();
		//instance.text.text = "";
		//instance.enabled = false;
	}
}
