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
		[Tooltip( "Searches Scrollview's Content for ListItems added via the editor." )]
		public bool AddItemsFoundOnStartup = false;
		[Tooltip( "Only enables these buttons when items have been selected." )]
		public List<Selectable> EnableOnSelected;

		[Header( "GUI Events" )]
		public UnityEvent OnItemSelected = new UnityEvent();
		[Header( "(Multiselect Only)" )]
		public UnityEvent OnItemDeselected = new UnityEvent();

		[Header( "Runtime Properties" )]
		public List<SelectableListItem> Selected;
		public SelectableListItem SelectedItem_Last;
		public List<SelectableListItem> Items;

		public Transform contentPanel;
		private ToggleGroup toggleGroup;

		void Awake()
		{
			contentPanel = GetComponentInChildren<ContentSizeFitter>().transform;
			toggleGroup = GetComponent<ToggleGroup>();

			// needs to be called in Awake() so that items added on a screens Start() happen after this 
			// search for existing items.
			AddExistingItems( AddItemsFoundOnStartup );
		}
		void Start()
		{
			SetDependantSelectablesInteractivity();
		}

		public SelectableListItem AddTemplatedItem()
		{
			SelectableListItem newItem = Instantiate(
				ListItemTemplatePrefab.gameObject ).
				GetComponent<SelectableListItem>();

			Add( newItem );
			return newItem;
		}
		public void Clear()
		{
			foreach (Transform item in contentPanel)
			{
				Destroy( item.gameObject );
			}

			Items.Clear();
			Selected.Clear();
			SelectedItem_Last = null;

			SetDependantSelectablesInteractivity();
		}

		private void Add(SelectableListItem item)
		{
			item.transform.SetParent( contentPanel, false );
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
				// if item is already selected, don't reselect.  
				// (happens when part of toggle group (!Multiselect) and AllowSwitchOff is false)
				// (i.e. clicking the same item when only one item can be selected)
				if ( !Selected.Contains(item) )
				{
					SelectedItem_Last = item;
					Selected.Add( item );
					Selected = Selected.OrderBy( listItem => listItem.gameObject.name ).ToList();
					OnItemSelected?.Invoke();
				}
			}
			else
			{
				Selected.Remove( item );
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
		void SetDependantSelectablesInteractivity()
		{
			if (Selected.Count < 1)
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
		void AddExistingItems(bool add)
		{
			if (add)
			{
				var existingItems = contentPanel.GetComponentsInChildren<SelectableListItem>();
				foreach (var item in existingItems)
				{
					item.InitializeToggle();
					Add( item );
				}
			}
			else
			{
				Clear();
			}
		}
	}
}