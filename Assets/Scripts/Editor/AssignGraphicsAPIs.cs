using UnityEngine;
using UnityEditor;

public class AssignGraphicsAPIs : ScriptableWizard
{
	[MenuItem("Custom/Switch Mac Graphics APIs")]
	static void CreateWizard()
	{
		ScriptableWizard.DisplayWizard("Assign Graphics API", typeof(AssignGraphicsAPIs), "Assign");
	}
	
	void OnWizardCreate()
	{
		UnityEditor.PlayerSettings.SetUseDefaultGraphicsAPIs(UnityEditor.BuildTarget.StandaloneLinux64, false);
		UnityEditor.PlayerSettings.SetGraphicsAPIs(UnityEditor.BuildTarget.StandaloneLinux64, new UnityEngine.Rendering.GraphicsDeviceType[] {UnityEngine.Rendering.GraphicsDeviceType.OpenGLCore, UnityEngine.Rendering.GraphicsDeviceType.OpenGL2});
		Debug.Log(UnityEditor.PlayerSettings.GetUseDefaultGraphicsAPIs(UnityEditor.BuildTarget.StandaloneLinux64));
		for (int i = 0; i < UnityEditor.PlayerSettings.GetGraphicsAPIs(UnityEditor.BuildTarget.StandaloneLinux64).Length; i++)
		{
			Debug.Log(UnityEditor.PlayerSettings.GetGraphicsAPIs(UnityEditor.BuildTarget.StandaloneLinux64)[i]);
		}
	}
	
}