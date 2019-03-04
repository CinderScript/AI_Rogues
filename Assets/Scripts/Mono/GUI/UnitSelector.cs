using AIRogue.Events;
using AIRogue.GameObjects;

using UnityEngine;

public class UnitSelector : MonoBehaviour {

	public Camera cam;
	public string targetsTag;

	private void Start()
	{
		cam = gameObject.GetComponent<Camera>();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown( 0 ))
		{
			Ray ray = cam.ScreenPointToRay( Input.mousePosition );
			RaycastHit hit;
			if (Physics.Raycast( ray, out hit ))
			{
				if (hit.transform.CompareTag( targetsTag ))
				{
					// add unit properties to Unit prefab and add a squad ID  and unit ID variable
					Unit unit = hit.transform.GetComponent<Unit>();

					/* QUEUE GAME EVENT */
					EventManager.Instance.QueueEvent( new PlayerLeaderChangedEvent( unit ) );
				}
			}
		}
	}
}
