using System.Collections;

namespace Utility.CoroutineHelper
{
	public class CoroutineTask
	{
		#region Variables
		private CoroutineTaskManager.TaskState task;

		/// Returns true if and only if the coroutine is running.  Paused tasks
		/// are considered to be running.
		public bool Running => task.Running;

		/// Returns true if and only if the coroutine is currently paused.
		public bool Paused => task.Paused;

		/// Delegate for termination subscribers.  manual is true if and only if
		/// the coroutine was stopped with an explicit call to Stop().
		public delegate void FinishedHandler(bool manual);

		/// Termination event.  Triggered when the coroutine completes execution.
		public event FinishedHandler Finished;
		#endregion

		#region Constructor
		/// Creates a new Task object for the given coroutine.
		///
		/// If autoStart is true (default) the task is automatically started
		/// upon construction.
		public CoroutineTask(IEnumerator c, bool autoStart = true)
		{
			task = CoroutineTaskManager.CreateTask(c);
			task.Finished += TaskFinished;
			if (autoStart)
				Start();
		}
		#endregion

		#region Start
		/// Begins execution of the coroutine
		public void Start()
		{
			task.Start();
		}
		#endregion

		#region Stop
		/// Discontinues execution of the coroutine at its next yield.
		public void Stop()
		{
			task.Stop();
		}
		#endregion

		#region Pause
		public void Pause()
		{
			task.Pause();
		}
		#endregion

		#region Unpause
		public void Unpause()
		{
			task.Unpause();
		}
		#endregion

		#region TaskFinished
		private void TaskFinished(bool manual) => Finished?.Invoke(manual);
		#endregion
	}
}
