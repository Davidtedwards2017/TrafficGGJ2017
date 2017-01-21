//http://www.whydoidoit.com
//Copyright (C) 2012 Mike Talbot
//
//Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;

public class CoroutineReturn
{
	public virtual bool finished {get;set;}
	public virtual bool cancel {get;set;}
}

public class WaitForAnimation : CoroutineReturn
{
	private GameObject _go;
	private string _name;
	private float _time;
	private float _weight;
//	private bool first = true;
	private int startFrame;
	public string name
	{
		get
		{
			return _name;
		}
	}
	
	public WaitForAnimation(GameObject go, string name, float time=1f, float weight = -1)
	{
		startFrame = Time.frameCount;
		_go = go;
		_name = name;
		_time = Mathf.Clamp01(time);
		_weight = weight;
	}
	
	public override bool finished {
		get {
			if(Time.frameCount <= startFrame+1)
			{
//				first = false;
				return false;
			}
			if(_weight == -1)
			{
				return !_go.GetComponent<Animation>()[_name].enabled || _go.GetComponent<Animation>()[_name].normalizedTime >= _time || _go.GetComponent<Animation>()[_name].weight == 0 || _go.GetComponent<Animation>()[_name].speed == 0;
			}
			else
			{
				if(_weight < 0.5)
				{
	//				var w = _go.GetComponent<Animation>()[_name].weight;
					return _go.GetComponent<Animation>()[_name].weight <= Mathf.Clamp01(_weight);
				}
				return _go.GetComponent<Animation>()[_name].weight >= Mathf.Clamp01(_weight);
			}
		}
		set {
			base.finished = value;
		}
	}
	
}

public class WaitForUnscaledSeconds : CoroutineReturn
{
	private float delay;
	
	public WaitForUnscaledSeconds(float seconds)
	{
		delay = seconds;
	}
	
	public override bool finished {
		get {
			
			if(Time.unscaledDeltaTime < Time.maximumDeltaTime)
			delay -= Time.unscaledDeltaTime;
			if(delay <= 0f) return true;
			return false;
		}
		set {
			base.finished = value;
		}	
	}
}

public class WaitForUnscaledSecondsSkippable : CoroutineReturn
{
    private float delay;

    public WaitForUnscaledSecondsSkippable(float seconds)
    {
        delay = seconds;
    }

    public override bool finished
    {
        get
        {            
            if(Input.GetButton("SkipAutoPause")) return true; //Skip if button is down

            if (Time.unscaledDeltaTime < Time.maximumDeltaTime)
            {
                delay -= Time.unscaledDeltaTime;                
            }

            if (delay <= 0f) return true;

            return false;
        }
        set
        {
            base.finished = value;
        }
    }
}

//public class WaitForModalWindow : CoroutineReturn
//{
//	public WaitForModalWindow(UnityAction choice)
//	{
//		choice();
//	}
//
//    public WaitForModalWindow(ModalInfo info)
//    {
//        ModalWindow.Choice(info);
//    }
//
//	public override bool finished {
//		get {
//			
//			if(ModalWindow.instance.gameObject.activeSelf) return false;
//			return true;
//			
//		}
//		set {
//			base.finished = value;
//		}	
//	}
//}

//public class WaitForCinematicSequence : CoroutineReturn
//{
//    private Cinematics.Sequence value = null;
//    public WaitForCinematicSequence(Cinematics.Sequence value)
//    {
//        this.value = value;
//        value.Begin();
//    }
//
//    public override bool finished
//    {
//        get
//        {
//
//            if (!value.complete) return false;
//            return true;
//
//        }
//        set
//        {
//            base.finished = value;
//        }
//    }
//}

public static class TaskHelpers
{
	public static WaitForAnimation WaitForAnimation(this GameObject go, string name, float time = 1f)
	{
		return new WaitForAnimation(go, name, time,-1);
	}
	public static WaitForAnimation WaitForAnimationWeight(this GameObject go, string name, float weight=0f)
	{
		return new WaitForAnimation(go, name, 0, weight);
	}
}

public class RadicalRoutine  {
	
	private bool cancel;
	public IEnumerator enumerator;
	public event Action Cancelled;
	public event Action Finished;
	
	public void Cancel()
	{
		cancel = true;
	}
	
	private static RadicalRoutine own = new RadicalRoutine();
	
	public static IEnumerator Run(IEnumerator extendedCoRoutine)
	{
		return own.Execute(extendedCoRoutine);
	}
	
	public static RadicalRoutine Create(IEnumerator extendedCoRoutine)
	{
		var rr = new RadicalRoutine();
		rr.enumerator = rr.Execute(extendedCoRoutine);
		return rr;
	}
	
	private IEnumerator Execute(IEnumerator extendedCoRoutine)
	{
		while(!cancel && extendedCoRoutine != null && extendedCoRoutine.MoveNext())
		{
			var v = extendedCoRoutine.Current;
			var cr = v as CoroutineReturn;
			if(cr != null)
			{
				if(cr.cancel) 
				{
					cancel = true;
					break;
				}
				while(!cr.finished)
				{
					if(cr.cancel) 
					{
						cancel = true;
						break;
					}
					yield return new WaitForEndOfFrame();
				}
			} else 
			{
				yield return v;
			}
		}
		
		if(cancel && Cancelled != null)
		{
			Cancelled();
		}
		if(Finished != null)
		{
			Finished();
		}
		
	}
	
	
}
