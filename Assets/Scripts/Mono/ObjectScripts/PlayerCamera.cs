using AIRogue.Logic.Events;
using UnityEngine;

public class UnitCamera: MonoBehaviour
{
	public GameObject Target;

	// Use this for initialization
	void Start()
	{
		EventManager.Instance.AddListener<UnitSelectedEvent>( OnUnitSelected );
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnUnitSelected(UnitSelectedEvent e)
	{
		Target = e.SelectedUnit.GameObject;
	}
}