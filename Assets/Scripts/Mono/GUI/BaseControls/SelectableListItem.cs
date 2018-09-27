using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IronGrimoire.Gui
{
	[RequireComponent( typeof( Toggle ) )]
	public class SelectableListItem : MonoBehaviour
	{
		public object TaggedObject { get; set; }
		public Action<SelectableListItem, bool> OnSelectionChanged { get; set; }
		public Toggle Toggle { get; private set; }


		void Awake()
		{
			Toggle = GetComponent<Toggle>();
			Toggle.onValueChanged.AddListener( OnValueChanged );
		}

		private void OnValueChanged(bool isSelected)
		{
			OnSelectionChanged?.Invoke( this, isSelected );
		}
	}
}