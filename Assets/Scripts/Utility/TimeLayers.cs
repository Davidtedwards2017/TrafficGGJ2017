using UnityEngine;
using System.Collections;
using System.Diagnostics;

// You have to instantiate this class to use it

public class TimeLayers : MonoBehaviour {
	
	private static TimeLayers _instance;
	public static TimeLayers instance
	{
		get 
		{
			if(_instance == null) 
			{
				_instance = GameObject.FindObjectOfType<TimeLayers>();
				//Enables object persistence between different scenes
				//DontDestroyOnLoad(_instance.gameObject);	
			}
			return _instance;
		}
	}		
	
	Stopwatch    timer;
	float        deltaMilliSec = 0 ;
	public static float resolution  { get; private set; } //milliseconds
	
	//time scales
	public static float userTimeScale { get; private set; } //always 1
	public static float cutsceneTimeScale { get; set; }
	public static float menuTimeScale { get; set; }
	
	public static float gameTimeScale //physics and gameplay
	{
		get {
			return Time.timeScale;
		}
		set {
			Time.timeScale = value;
		}
	}
	
	//delta times (seconds)
	public static float userDeltaTime {get; private set;}
	public static float cutsceneDeltaTime {get; private set;}
	public static float menuDeltaTime {get; private set;}
	public static float gameDeltaTime
	{
		get{
			return Time.deltaTime;
		}
		private set {
			
		}
	}
	
	void Awake() {
		timer = new Stopwatch();
		resolution = 1000.0f/(float)(Stopwatch.Frequency);
		userTimeScale = 1;
		cutsceneTimeScale = 1;
		gameTimeScale = 1;
		menuTimeScale = 1;
		enabled = true;
		UpdateDeltaTimes();
		
		if(resolution>16.0f)
			UnityEngine.Debug.Log ("TimeLayers resolution is greater than maximum admitted for 60 fps");
		//!--
		DontDestroyOnLoad(this); //You may not want this, in that case comment.
	}
	
	void Start () {
		timer.Start();
		StartCoroutine("deltaUpdate");
	}
	
	void OnLevelWasLoaded(int i ){
		timer.Reset();
		timer.Start();
		PollTime();
		UpdateDeltaTimes();
	}
	
	IEnumerator deltaUpdate(){
		while(true){
			PollTime();
			UpdateDeltaTimes();
			yield return null;
		}
	}
	
	void PollTime(){
		timer.Stop ();
		deltaMilliSec = (float)(timer.ElapsedMilliseconds)/1000.0f;
		timer.Start ();
	}
	
	void OnDestroy(){
		StopCoroutine("deltaUpdate");
	}
	
	void UpdateDeltaTimes(){
		userDeltaTime     = deltaMilliSec; //always scale by 1
		cutsceneDeltaTime = deltaMilliSec * cutsceneTimeScale;
		menuDeltaTime     = deltaMilliSec * menuTimeScale;
		//gameDeltaTime     = deltaMilliSec * gameTimeScale; // KEEP COMMENTED (for documentation only)
	}
}