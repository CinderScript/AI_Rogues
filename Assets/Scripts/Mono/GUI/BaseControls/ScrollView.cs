using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace IronGrimoire.Gui
{
	[RequireComponent( typeof( ToggleGroup ) )]
	public class ScrollView : MonoBehaviour
	{
		[Header( "Scroll View Setup" )]
		public SelectableListItem ListItemTemplatePrefab;
		public bool MultiSelect = false;
		[Tooltip( "Only enable these buttons when items have been selected." )]
		public List<Selectable> EnableOnSelected;

		[Header( "GUI Events" )]
		public UnityEvent OnItemSelected = new UnityEvent();
		[Header( "(Multiselect Only)" )]
		public UnityEvent OnItemDeselected = new UnityEvent();

		[Header( "Runtime Properties" )]
		public List<SelectableListItem> SelectedItems;
		public SelectableListItem SelectedItem_Last;
		public List<SelectableListItem> Items;

		public Transform contentPanel;
		private ToggleGroup toggleGroup;

		void Awake()
		{
			contentPanel = GetComponentInChildren<ContentSizeFitter>().transform;

			toggleGroup = GetComponent<ToggleGroup>();
			SelectableListItem[] existingItems = contentPanel.GetComponentsInChildren<SelectableListItem>();
			foreach (var item in existingItems)
			{
				Add( item );
			}
		}
		void Start()
		{
			SetDependantSelectablesInteractivity();

			//for (int i = 0; i < 10; i++)
			//{
			//	AddTemplatedItem();
			//}
		}

		public SelectableListItem AddTemplatedItem()
		{
			SelectableListItem newItem = Instantiate(
				ListItemTemplatePrefab.gameObject ).
				GetComponent<SelectableListItem>();

			Add( newItem );
			return newItem;
		}

		private void Add(SelectableListItem item)
		{
			item.transform.SetParent( contentPanel,false );
			item.OnSelectionChanged += Item_OnSelectionChanged;
			item.gameObject.name = $"ListItem ({Items.Count})";

			if (!MultiSelect)
			{
				item.Toggle.group = toggleGroup;
			}

			Items.Add( item );
		}
		private void Item_OnSelectionChanged(SelectableListItem item, bool selected)
		{
			if (selected)
			{
				SelectedItem_Last = item;
				SelectedItems.Add( item );
				SelectedItems = SelectedItems.OrderBy( listItem => listItem.gameObject.name ).ToList();
				OnItemSelected?.Invoke();
			}
			else
			{
				SelectedItems.Remove( item );
				if (MultiSelect)
				{
					OnItemDeselected?.Invoke();
				}
			}

			// Enable/Disable any selectables indicated by this ScrollView
			SetDependantSelectablesInteractivity();
		}
		/// <summary>
		/// Makes any ugui Selectable in <see cref="EnableOnSelected"/> interactive when 
		/// items are selected in this ScrollView.
		/// </summary>
		private void SetDependantSelectablesInteractivity()
		{
			if (SelectedItems.Count < 1)
			{
				foreach (var selectable in EnableOnSelected)
					selectable.interactable = false;
			}
			else
			{
				foreach (var selectable in EnableOnSelected)
					selectable.interactable = true;
			}
		}
	}
}