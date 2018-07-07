using AIRogue.Logic.Events;
using UnityEngine;

public class UnitCamera : MonoBehaviour
{
	public GameObject Target = null;

	[Header( "Offsets" )]
	public int HeightOffest;
	public int LengthOffest;

	void Awake()
	{
		EventManager.Instance.AddListener<UnitSelectedEvent>( OnUnitSelected );
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void LateUpdate()
	{
		Vector3 offset = new Vector3(
			Target.transform.position.x,
			Target.transform.position.y + HeightOffest,
			Target.transform.position.z - LengthOffest );

		transform.position = offset;
	}

	// Game Event Listener
	void OnUnitSelected(UnitSelectedEvent e)
	{
		Target = e.SelectedUnit.gameObject;
		// Debug.Log( "A unit was selected: " + Target.name );
	}
}