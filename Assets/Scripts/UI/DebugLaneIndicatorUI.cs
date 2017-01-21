using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DebugLaneIndicatorUI : MonoBehaviour {

    public Color ClosedColor = Color.red;
    public Color OpenedColor = Color.green;

    public DataTypes.Direction Direction;
    public Image Image;

    public void Start()
    {
        MessageController.StartListening("LaneOpened", LaneOpened);
        MessageController.StartListening("LaneClosed", LaneClosed);
        Image.color = ClosedColor;

    }

    public void LaneOpened(object[] args)
    {
        if(!Direction.Equals((DataTypes.Direction)args[0]))
        {
            return;
        }
        Image.color = OpenedColor;
    }

    public void LaneClosed(object[] args)
    {
        if (!Direction.Equals((DataTypes.Direction)args[0]))
        {
            return;
        }
        Image.color = ClosedColor;
    }
}
