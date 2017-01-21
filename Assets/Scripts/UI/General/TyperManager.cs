using UnityEngine;
using System.Collections;

public class TyperManager : MonoBehaviour {
	private Typer typer;
//	private Animator menuAnim;
	private bool menuOn = false;
	
	void Awake()
	{
		typer = GetComponentInChildren<Typer>();
//		menuAnim = GetComponent<Animator>();
	}
	
	public void BeginMenu()
	{
		if(!menuOn)
		{
//			menuAnim.SetTrigger("FadeIn");
			typer.StartCoroutine("TypeIn");
			menuOn = true;
		}
		else
		{
//			menuAnim.SetTrigger("FadeOut");
			typer.StartCoroutine("TypeOff");
			menuOn = false;		
		}
		
		
	}
	
}
