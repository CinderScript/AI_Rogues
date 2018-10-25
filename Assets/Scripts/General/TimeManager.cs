using UnityEngine;

public class TimeManager {

	private static TimeManager instance = null;
	public static TimeManager Instance
	{
		get {
				if (instance == null)
				{
					instance = new TimeManager();
				}
				return instance;
			}
	}
	private TimeManager()
	{
		timeScale_original = Time.timeScale;
		fixedDeltaTime_original = Time.fixedDeltaTime;
		maximumDeltaTime_original = Time.maximumDeltaTime;
	}

	private float timeScale_original;
	private float fixedDeltaTime_original;
	private float maximumDeltaTime_original;

	/// <summary>
	/// Sets the gameplay speed (update, fixedupdate, and physics) where 0
	/// is time stopped and 1 is default speed.
	/// </summary>
	/// <param name="multiplyer"></param>
	public void SetGameplaySpeed(float multiplyer)
	{
		Time.timeScale = timeScale_original * multiplyer;
		Time.fixedDeltaTime = fixedDeltaTime_original * multiplyer;
		Time.maximumDeltaTime = maximumDeltaTime_original * multiplyer;
	}
}