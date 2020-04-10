using UnityEngine;
using System.Collections;

namespace Utility.CoroutineHelper
{
	public class CoroutineTaskManager : MonoBehaviour
	{
		#region TaskState
		public class TaskState
		{
			#region Variables
			private IEnumerator coroutine;
			private bool stopped;

			public bool Running { get; private set; }
			public bool Paused { get; private set; }

			public delegate void FinishedHandler(bool manual);
			public event FinishedHandler Finished;
			#endregion

			#region Constructor
			public TaskState(IEnumerator c)
			{
				coroutine = c;
			}
			#endregion

			#region Pause
			public void Pause()
			{
				Paused = true;
			}
			#endregion

			#region Unpause
			public void Unpause()
			{
				Paused = false;
			}
			#endregion

			#region Start
			public void Start()
			{
				Running = true;
				singleton.StartCoroutine(CallWrapper());
			}
			#endregion

			#region Stop
			public void Stop()
			{
				stopped = true;
				Running = false;
			}
			#endregion

			#region CallWrapper
			private IEnumerator CallWrapper()
			{
				yield return null;
				IEnumerator e = coroutine;
				while (Running)
				{
					if (Paused)
						yield return null;
					else
					{
						if (e != null && e.MoveNext())
						{
							yield return e.Current;
						}
						else
						{
							Running = false;
						}
					}
				}

				Finished?.Invoke(stopped);
			}
			#endregion
		}
		#endregion

		#region Variables
		private static CoroutineTaskManager singleton;
		#endregion

		#region CreateTask
		public static TaskState CreateTask(IEnumerator coroutine)
		{
			if (singleton == null)
			{
				GameObject go = new GameObject("CoroutineTaskManager");
				singleton = go.AddComponent<CoroutineTaskManager>();
			}
			return new TaskState(coroutine);
		}
		#endregion
	}
}
