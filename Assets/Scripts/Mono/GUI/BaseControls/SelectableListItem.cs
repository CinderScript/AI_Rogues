using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IronGrimoire.Gui
{
	[RequireComponent( typeof( Toggle ) )]
	public class SelectableListItem : MonoBehaviour
	{
		public object Tagged { get; set; }
		public Action<SelectableListItem, bool> OnSelectionChanged { get; set; }
		public Toggle Toggle { get; private set; }


		protected virtual void Awake()
		{
			Toggle = GetComponent<Toggle>();
			Toggle.onValueChanged.AddListener( OnValueChanged );
		}

		/// <summary>
		/// Call this initialization if this ListItem is added to a list in an Awake() cycle before this
		/// item's Awake() has time to run.  Returns true if the toggle is assigned and false if
		/// the toggle has already been asigned;
		/// </summary>
		/// <returns>Toggle was set</returns>
		public bool InitializeToggle()
		{
			if (Toggle)
			{
				return false;
			}
			else
			{
				Toggle = GetComponent<Toggle>();
				return true;
			}
		}

		void OnValueChanged(bool isSelected)
		{
			OnSelectionChanged?.Invoke( this, isSelected );
		}
	}
}