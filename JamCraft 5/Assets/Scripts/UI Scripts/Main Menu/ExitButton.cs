using UnityEngine;

namespace JamCraft5.UI
{
	public class ExitButton : MonoBehaviour
	{
		#region Quit
		public void Quit()
		{
#if UNITY_EDITOR
			Debug.Log("Quitting...");
#endif
			Application.Quit();
		}
		#endregion
	}
}
