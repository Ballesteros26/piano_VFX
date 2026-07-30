using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	// Token: 0x02000105 RID: 261
	internal class TreeView : VisualElement
	{
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x0001FB3C File Offset: 0x0001DD3C
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x0001FB54 File Offset: 0x0001DD54
		public Func<VisualElement> makeItem
		{
			get
			{
				return this.m_MakeItem;
			}
			set
			{
				bool flag = this.m_MakeItem == value;
				if (!flag)
				{
					this.m_MakeItem = value;
					this.ListViewRefresh();
				}
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060007B6 RID: 1974 RVA: 0x0001FB84 File Offset: 0x0001DD84
		// (remove) Token: 0x060007B7 RID: 1975 RVA: 0x0001FBBC File Offset: 0x0001DDBC
		[field: DebuggerBrowsable(0)]
		public event Action<IEnumerable<ITreeViewItem>> onItemsChosen;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060007B8 RID: 1976 RVA: 0x0001FBF4 File Offset: 0x0001DDF4
		// (remove) Token: 0x060007B9 RID: 1977 RVA: 0x0001FC2C File Offset: 0x0001DE2C
		[field: DebuggerBrowsable(0)]
		public event Action<IEnumerable<ITreeViewItem>> onSelectionChange;

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x0001FC61 File Offset: 0x0001DE61
		public ITreeViewItem selectedItem
		{
			get
			{
				return (this.m_SelectedItems.Count == 0) ? null : Enumerable.First<ITreeViewItem>(this.m_SelectedItems);
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x0001FC80 File Offset: 0x0001DE80
		public IEnumerable<ITreeViewItem> selectedItems
		{
			get
			{
				bool flag = this.m_SelectedItems != null;
				IEnumerable<ITreeViewItem> enumerable;
				if (flag)
				{
					enumerable = this.m_SelectedItems;
				}
				else
				{
					this.m_SelectedItems = new List<ITreeViewItem>();
					foreach (ITreeViewItem treeViewItem in this.items)
					{
						foreach (int num in this.m_ListView.currentSelectionIds)
						{
							bool flag2 = treeViewItem.id == num;
							if (flag2)
							{
								this.m_SelectedItems.Add(treeViewItem);
							}
						}
					}
					enumerable = this.m_SelectedItems;
				}
				return enumerable;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x0001FD5C File Offset: 0x0001DF5C
		// (set) Token: 0x060007BD RID: 1981 RVA: 0x0001FD74 File Offset: 0x0001DF74
		public Action<VisualElement, ITreeViewItem> bindItem
		{
			get
			{
				return this.m_BindItem;
			}
			set
			{
				this.m_BindItem = value;
				this.ListViewRefresh();
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x0001FD85 File Offset: 0x0001DF85
		// (set) Token: 0x060007BF RID: 1983 RVA: 0x0001FD8D File Offset: 0x0001DF8D
		public Action<VisualElement, ITreeViewItem> unbindItem { get; set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x0001FD98 File Offset: 0x0001DF98
		// (set) Token: 0x060007C1 RID: 1985 RVA: 0x0001FDB0 File Offset: 0x0001DFB0
		public IList<ITreeViewItem> rootItems
		{
			get
			{
				return this.m_RootItems;
			}
			set
			{
				this.m_RootItems = value;
				this.Refresh();
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x0001FDC1 File Offset: 0x0001DFC1
		public IEnumerable<ITreeViewItem> items
		{
			get
			{
				return TreeView.GetAllItems(this.m_RootItems);
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x0001FDCE File Offset: 0x0001DFCE
		public float resolvedItemHeight
		{
			get
			{
				return this.m_ListView.resolvedItemHeight;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x0001FDDC File Offset: 0x0001DFDC
		// (set) Token: 0x060007C5 RID: 1989 RVA: 0x0001FDF9 File Offset: 0x0001DFF9
		public int itemHeight
		{
			get
			{
				return this.m_ListView.itemHeight;
			}
			set
			{
				this.m_ListView.itemHeight = value;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x0001FE0C File Offset: 0x0001E00C
		// (set) Token: 0x060007C7 RID: 1991 RVA: 0x0001FE29 File Offset: 0x0001E029
		public bool showBorder
		{
			get
			{
				return this.m_ListView.showBorder;
			}
			set
			{
				this.m_ListView.showBorder = value;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x0001FE3C File Offset: 0x0001E03C
		// (set) Token: 0x060007C9 RID: 1993 RVA: 0x0001FE59 File Offset: 0x0001E059
		public SelectionType selectionType
		{
			get
			{
				return this.m_ListView.selectionType;
			}
			set
			{
				this.m_ListView.selectionType = value;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x0001FE6C File Offset: 0x0001E06C
		// (set) Token: 0x060007CB RID: 1995 RVA: 0x0001FE89 File Offset: 0x0001E089
		public AlternatingRowBackground showAlternatingRowBackgrounds
		{
			get
			{
				return this.m_ListView.showAlternatingRowBackgrounds;
			}
			set
			{
				this.m_ListView.showAlternatingRowBackgrounds = value;
			}
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0001FE9C File Offset: 0x0001E09C
		public TreeView()
		{
			this.m_SelectedItems = null;
			this.m_ExpandedItemIds = new List<int>();
			this.m_ItemWrappers = new List<TreeView.TreeViewItemWrapper>();
			this.m_ListView = new ListView();
			this.m_ListView.name = TreeView.s_ListViewName;
			this.m_ListView.itemsSource = this.m_ItemWrappers;
			this.m_ListView.viewDataKey = TreeView.s_ListViewName;
			this.m_ListView.AddToClassList(TreeView.s_ListViewName);
			base.hierarchy.Add(this.m_ListView);
			this.m_ListView.makeItem = new Func<VisualElement>(this.MakeTreeItem);
			this.m_ListView.bindItem = new Action<VisualElement, int>(this.BindTreeItem);
			this.m_ListView.unbindItem = new Action<VisualElement, int>(this.UnbindTreeItem);
			this.m_ListView.getItemId = new Func<int, int>(this.GetItemId);
			this.m_ListView.onItemsChosen += new Action<IEnumerable<object>>(this.OnItemsChosen);
			this.m_ListView.onSelectionChange += new Action<IEnumerable<object>>(this.OnSelectionChange);
			this.m_ScrollView = this.m_ListView.m_ScrollView;
			this.m_ScrollView.contentContainer.RegisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
			base.RegisterCallback<MouseUpEvent>(new EventCallback<MouseUpEvent>(this.OnTreeViewMouseUp), TrickleDown.TrickleDown);
			base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0002001A File Offset: 0x0001E21A
		public TreeView(IList<ITreeViewItem> items, int itemHeight, Func<VisualElement> makeItem, Action<VisualElement, ITreeViewItem> bindItem)
			: this()
		{
			this.m_ListView.itemHeight = itemHeight;
			this.m_MakeItem = makeItem;
			this.m_BindItem = bindItem;
			this.m_RootItems = items;
			this.Refresh();
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0002004E File Offset: 0x0001E24E
		public void Refresh()
		{
			this.RegenerateWrappers();
			this.ListViewRefresh();
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00020060 File Offset: 0x0001E260
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
			base.OverwriteFromViewData(this, fullHierarchicalViewDataKey);
			this.Refresh();
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0002008C File Offset: 0x0001E28C
		public static IEnumerable<ITreeViewItem> GetAllItems(IEnumerable<ITreeViewItem> rootItems)
		{
			bool flag = rootItems == null;
			if (flag)
			{
				yield break;
			}
			Stack<IEnumerator<ITreeViewItem>> iteratorStack = new Stack<IEnumerator<ITreeViewItem>>();
			IEnumerator<ITreeViewItem> currentIterator = rootItems.GetEnumerator();
			for (;;)
			{
				bool hasNext = currentIterator.MoveNext();
				bool flag2 = !hasNext;
				if (flag2)
				{
					bool flag3 = iteratorStack.Count > 0;
					if (!flag3)
					{
						break;
					}
					currentIterator = iteratorStack.Pop();
				}
				else
				{
					ITreeViewItem currentItem = currentIterator.Current;
					yield return currentItem;
					bool hasChildren = currentItem.hasChildren;
					if (hasChildren)
					{
						iteratorStack.Push(currentIterator);
						currentIterator = currentItem.children.GetEnumerator();
					}
					currentItem = null;
				}
			}
			yield break;
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0002009C File Offset: 0x0001E29C
		public void OnKeyDown(KeyDownEvent evt)
		{
			int selectedIndex = this.m_ListView.selectedIndex;
			bool flag = true;
			KeyCode keyCode = evt.keyCode;
			if (keyCode != KeyCode.RightArrow)
			{
				if (keyCode != KeyCode.LeftArrow)
				{
					flag = false;
				}
				else
				{
					bool flag2 = this.IsExpandedByIndex(selectedIndex);
					if (flag2)
					{
						this.CollapseItemByIndex(selectedIndex);
					}
				}
			}
			else
			{
				bool flag3 = !this.IsExpandedByIndex(selectedIndex);
				if (flag3)
				{
					this.ExpandItemByIndex(selectedIndex);
				}
			}
			bool flag4 = flag;
			if (flag4)
			{
				evt.StopPropagation();
			}
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00020114 File Offset: 0x0001E314
		public void SetSelection(int id)
		{
			this.SetSelection(new int[] { id });
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00020128 File Offset: 0x0001E328
		public void SetSelection(IEnumerable<int> ids)
		{
			this.SetSelectionInternal(ids, true);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00020134 File Offset: 0x0001E334
		public void SetSelectionWithoutNotify(IEnumerable<int> ids)
		{
			this.SetSelectionInternal(ids, false);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00020140 File Offset: 0x0001E340
		internal void SetSelectionInternal(IEnumerable<int> ids, bool sendNotification)
		{
			bool flag = ids == null;
			if (!flag)
			{
				List<int> list = Enumerable.ToList<int>(Enumerable.Select<int, int>(ids, (int id) => this.GetItemIndex(id, true)));
				this.ListViewRefresh();
				this.m_ListView.SetSelectionInternal(list, sendNotification);
			}
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00020188 File Offset: 0x0001E388
		public void AddToSelection(int id)
		{
			int itemIndex = this.GetItemIndex(id, true);
			this.ListViewRefresh();
			this.m_ListView.AddToSelection(itemIndex);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x000201B4 File Offset: 0x0001E3B4
		public void RemoveFromSelection(int id)
		{
			int itemIndex = this.GetItemIndex(id, false);
			this.m_ListView.RemoveFromSelection(itemIndex);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x000201D8 File Offset: 0x0001E3D8
		private int GetItemIndex(int id, bool expand = false)
		{
			ITreeViewItem treeViewItem = this.FindItem(id);
			bool flag = treeViewItem == null;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("id", id, "TreeView: Item id not found.");
			}
			if (expand)
			{
				bool flag2 = false;
				for (ITreeViewItem treeViewItem2 = treeViewItem.parent; treeViewItem2 != null; treeViewItem2 = treeViewItem2.parent)
				{
					bool flag3 = !this.m_ExpandedItemIds.Contains(treeViewItem2.id);
					if (flag3)
					{
						this.m_ExpandedItemIds.Add(treeViewItem2.id);
						flag2 = true;
					}
				}
				bool flag4 = flag2;
				if (flag4)
				{
					this.RegenerateWrappers();
				}
			}
			int i;
			for (i = 0; i < this.m_ItemWrappers.Count; i++)
			{
				bool flag5 = this.m_ItemWrappers[i].id == id;
				if (flag5)
				{
					break;
				}
			}
			return i;
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x000202BB File Offset: 0x0001E4BB
		public void ClearSelection()
		{
			this.m_ListView.ClearSelection();
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x000202CA File Offset: 0x0001E4CA
		public void ScrollTo(VisualElement visualElement)
		{
			this.m_ListView.ScrollTo(visualElement);
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x000202DC File Offset: 0x0001E4DC
		public void ScrollToItem(int id)
		{
			int itemIndex = this.GetItemIndex(id, true);
			this.Refresh();
			this.m_ListView.ScrollToItem(itemIndex);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00020308 File Offset: 0x0001E508
		public bool IsExpanded(int id)
		{
			return this.m_ExpandedItemIds.Contains(id);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00020328 File Offset: 0x0001E528
		public void CollapseItem(int id)
		{
			bool flag = this.FindItem(id) == null;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("id", id, "TreeView: Item id not found.");
			}
			for (int i = 0; i < this.m_ItemWrappers.Count; i++)
			{
				bool flag2 = this.m_ItemWrappers[i].item.id == id;
				if (flag2)
				{
					bool flag3 = this.IsExpandedByIndex(i);
					if (flag3)
					{
						this.CollapseItemByIndex(i);
						return;
					}
				}
			}
			bool flag4 = !this.m_ExpandedItemIds.Contains(id);
			if (flag4)
			{
				return;
			}
			this.m_ExpandedItemIds.Remove(id);
			this.Refresh();
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x000203D4 File Offset: 0x0001E5D4
		public void ExpandItem(int id)
		{
			bool flag = this.FindItem(id) == null;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("id", id, "TreeView: Item id not found.");
			}
			for (int i = 0; i < this.m_ItemWrappers.Count; i++)
			{
				bool flag2 = this.m_ItemWrappers[i].item.id == id;
				if (flag2)
				{
					bool flag3 = !this.IsExpandedByIndex(i);
					if (flag3)
					{
						this.ExpandItemByIndex(i);
						return;
					}
				}
			}
			bool flag4 = this.m_ExpandedItemIds.Contains(id);
			if (flag4)
			{
				return;
			}
			this.m_ExpandedItemIds.Add(id);
			this.Refresh();
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00020480 File Offset: 0x0001E680
		public ITreeViewItem FindItem(int id)
		{
			foreach (ITreeViewItem treeViewItem in this.items)
			{
				bool flag = treeViewItem.id == id;
				if (flag)
				{
					return treeViewItem;
				}
			}
			return null;
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x000204E0 File Offset: 0x0001E6E0
		private void ListViewRefresh()
		{
			this.m_ListView.Refresh();
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x000204F0 File Offset: 0x0001E6F0
		private void OnItemsChosen(IEnumerable<object> chosenItems)
		{
			bool flag = this.onItemsChosen == null;
			if (!flag)
			{
				List<ITreeViewItem> list = new List<ITreeViewItem>();
				foreach (object obj in chosenItems)
				{
					TreeView.TreeViewItemWrapper treeViewItemWrapper = (TreeView.TreeViewItemWrapper)obj;
					list.Add(treeViewItemWrapper.item);
				}
				this.onItemsChosen.Invoke(list);
			}
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00020570 File Offset: 0x0001E770
		private void OnSelectionChange(IEnumerable<object> selectedListItems)
		{
			bool flag = this.m_SelectedItems == null;
			if (flag)
			{
				this.m_SelectedItems = new List<ITreeViewItem>();
			}
			this.m_SelectedItems.Clear();
			foreach (object obj in selectedListItems)
			{
				this.m_SelectedItems.Add(((TreeView.TreeViewItemWrapper)obj).item);
			}
			Action<IEnumerable<ITreeViewItem>> action = this.onSelectionChange;
			if (action != null)
			{
				action.Invoke(this.m_SelectedItems);
			}
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00020608 File Offset: 0x0001E808
		private void OnTreeViewMouseUp(MouseUpEvent evt)
		{
			this.m_ScrollView.contentContainer.Focus();
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0002061C File Offset: 0x0001E81C
		private void OnItemMouseUp(MouseUpEvent evt)
		{
			bool flag = (evt.modifiers & EventModifiers.Alt) == EventModifiers.None;
			if (!flag)
			{
				VisualElement visualElement = evt.currentTarget as VisualElement;
				Toggle toggle = visualElement.Q(TreeView.s_ItemToggleName, null);
				int num = (int)toggle.userData;
				ITreeViewItem item = this.m_ItemWrappers[num].item;
				bool flag2 = this.IsExpandedByIndex(num);
				bool flag3 = !item.hasChildren;
				if (!flag3)
				{
					HashSet<int> hashSet = new HashSet<int>(this.m_ExpandedItemIds);
					bool flag4 = flag2;
					if (flag4)
					{
						hashSet.Remove(item.id);
					}
					else
					{
						hashSet.Add(item.id);
					}
					foreach (ITreeViewItem treeViewItem in TreeView.GetAllItems(item.children))
					{
						bool hasChildren = treeViewItem.hasChildren;
						if (hasChildren)
						{
							bool flag5 = flag2;
							if (flag5)
							{
								hashSet.Remove(treeViewItem.id);
							}
							else
							{
								hashSet.Add(treeViewItem.id);
							}
						}
					}
					this.m_ExpandedItemIds = Enumerable.ToList<int>(hashSet);
					this.Refresh();
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00020764 File Offset: 0x0001E964
		private VisualElement MakeTreeItem()
		{
			VisualElement visualElement = new VisualElement
			{
				name = TreeView.s_ItemName,
				style = 
				{
					flexDirection = FlexDirection.Row
				}
			};
			visualElement.AddToClassList(TreeView.s_ItemName);
			visualElement.RegisterCallback<MouseUpEvent>(new EventCallback<MouseUpEvent>(this.OnItemMouseUp), TrickleDown.NoTrickleDown);
			VisualElement visualElement2 = new VisualElement
			{
				name = TreeView.s_ItemIndentsContainerName,
				style = 
				{
					flexDirection = FlexDirection.Row
				}
			};
			visualElement2.AddToClassList(TreeView.s_ItemIndentsContainerName);
			visualElement.hierarchy.Add(visualElement2);
			Toggle toggle = new Toggle
			{
				name = TreeView.s_ItemToggleName
			};
			toggle.AddToClassList(Foldout.toggleUssClassName);
			toggle.RegisterValueChangedCallback(new EventCallback<ChangeEvent<bool>>(this.ToggleExpandedState));
			visualElement.hierarchy.Add(toggle);
			VisualElement visualElement3 = new VisualElement
			{
				name = TreeView.s_ItemContentContainerName,
				style = 
				{
					flexGrow = 1f
				}
			};
			visualElement3.AddToClassList(TreeView.s_ItemContentContainerName);
			visualElement.Add(visualElement3);
			bool flag = this.m_MakeItem != null;
			if (flag)
			{
				visualElement3.Add(this.m_MakeItem.Invoke());
			}
			return visualElement;
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x000208A0 File Offset: 0x0001EAA0
		private void UnbindTreeItem(VisualElement element, int index)
		{
			bool flag = this.unbindItem == null;
			if (!flag)
			{
				ITreeViewItem item = this.m_ItemWrappers[index].item;
				VisualElement visualElement = element.Q(TreeView.s_ItemContentContainerName, null).ElementAt(0);
				this.unbindItem.Invoke(visualElement, item);
			}
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x000208F0 File Offset: 0x0001EAF0
		private void BindTreeItem(VisualElement element, int index)
		{
			ITreeViewItem item = this.m_ItemWrappers[index].item;
			VisualElement visualElement = element.Q(TreeView.s_ItemIndentsContainerName, null);
			visualElement.Clear();
			for (int i = 0; i < this.m_ItemWrappers[index].depth; i++)
			{
				VisualElement visualElement2 = new VisualElement();
				visualElement2.AddToClassList(TreeView.s_ItemIndentName);
				visualElement.Add(visualElement2);
			}
			Toggle toggle = element.Q(TreeView.s_ItemToggleName, null);
			toggle.SetValueWithoutNotify(this.IsExpandedByIndex(index));
			toggle.userData = index;
			bool hasChildren = item.hasChildren;
			if (hasChildren)
			{
				toggle.visible = true;
			}
			else
			{
				toggle.visible = false;
			}
			bool flag = this.m_BindItem == null;
			if (!flag)
			{
				VisualElement visualElement3 = element.Q(TreeView.s_ItemContentContainerName, null).ElementAt(0);
				this.m_BindItem.Invoke(visualElement3, item);
			}
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x000209E4 File Offset: 0x0001EBE4
		private int GetItemId(int index)
		{
			return this.m_ItemWrappers[index].id;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00020A0C File Offset: 0x0001EC0C
		private bool IsExpandedByIndex(int index)
		{
			return this.m_ExpandedItemIds.Contains(this.m_ItemWrappers[index].id);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00020A40 File Offset: 0x0001EC40
		private void CollapseItemByIndex(int index)
		{
			bool flag = !this.m_ItemWrappers[index].item.hasChildren;
			if (!flag)
			{
				this.m_ExpandedItemIds.Remove(this.m_ItemWrappers[index].item.id);
				int num = 0;
				int num2 = index + 1;
				int depth = this.m_ItemWrappers[index].depth;
				while (num2 < this.m_ItemWrappers.Count && this.m_ItemWrappers[num2].depth > depth)
				{
					num++;
					num2++;
				}
				this.m_ItemWrappers.RemoveRange(index + 1, num);
				this.ListViewRefresh();
				base.SaveViewData();
			}
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00020B00 File Offset: 0x0001ED00
		private void ExpandItemByIndex(int index)
		{
			bool flag = !this.m_ItemWrappers[index].item.hasChildren;
			if (!flag)
			{
				List<TreeView.TreeViewItemWrapper> list = new List<TreeView.TreeViewItemWrapper>();
				this.CreateWrappers(this.m_ItemWrappers[index].item.children, this.m_ItemWrappers[index].depth + 1, ref list);
				this.m_ItemWrappers.InsertRange(index + 1, list);
				this.m_ExpandedItemIds.Add(this.m_ItemWrappers[index].item.id);
				this.ListViewRefresh();
				base.SaveViewData();
			}
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00020BA8 File Offset: 0x0001EDA8
		private void ToggleExpandedState(ChangeEvent<bool> evt)
		{
			Toggle toggle = evt.target as Toggle;
			int num = (int)toggle.userData;
			bool flag = this.IsExpandedByIndex(num);
			Assert.AreNotEqual<bool>(flag, evt.newValue);
			bool flag2 = flag;
			if (flag2)
			{
				this.CollapseItemByIndex(num);
			}
			else
			{
				this.ExpandItemByIndex(num);
			}
			this.m_ScrollView.contentContainer.Focus();
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00020C0C File Offset: 0x0001EE0C
		private void CreateWrappers(IEnumerable<ITreeViewItem> treeViewItems, int depth, ref List<TreeView.TreeViewItemWrapper> wrappers)
		{
			foreach (ITreeViewItem treeViewItem in treeViewItems)
			{
				TreeView.TreeViewItemWrapper treeViewItemWrapper = new TreeView.TreeViewItemWrapper
				{
					depth = depth,
					item = treeViewItem
				};
				wrappers.Add(treeViewItemWrapper);
				bool flag = this.m_ExpandedItemIds.Contains(treeViewItem.id) && treeViewItem.hasChildren;
				if (flag)
				{
					this.CreateWrappers(treeViewItem.children, depth + 1, ref wrappers);
				}
			}
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00020CA8 File Offset: 0x0001EEA8
		private void RegenerateWrappers()
		{
			this.m_ItemWrappers.Clear();
			bool flag = this.m_RootItems == null;
			if (!flag)
			{
				this.CreateWrappers(this.m_RootItems, 0, ref this.m_ItemWrappers);
			}
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00020CE8 File Offset: 0x0001EEE8
		private void OnCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			int itemHeight = this.m_ListView.itemHeight;
			int num;
			bool flag = !this.m_ListView.m_ItemHeightIsInline && e.customStyle.TryGetValue(ListView.s_ItemHeightProperty, out num);
			if (flag)
			{
				this.m_ListView.m_ItemHeight = num;
			}
			bool flag2 = this.m_ListView.m_ItemHeight != itemHeight;
			if (flag2)
			{
				this.m_ListView.Refresh();
			}
		}

		// Token: 0x04000380 RID: 896
		private static readonly string s_ListViewName = "unity-tree-view__list-view";

		// Token: 0x04000381 RID: 897
		private static readonly string s_ItemName = "unity-tree-view__item";

		// Token: 0x04000382 RID: 898
		private static readonly string s_ItemToggleName = "unity-tree-view__item-toggle";

		// Token: 0x04000383 RID: 899
		private static readonly string s_ItemIndentsContainerName = "unity-tree-view__item-indents";

		// Token: 0x04000384 RID: 900
		private static readonly string s_ItemIndentName = "unity-tree-view__item-indent";

		// Token: 0x04000385 RID: 901
		private static readonly string s_ItemContentContainerName = "unity-tree-view__item-content";

		// Token: 0x04000386 RID: 902
		private Func<VisualElement> m_MakeItem;

		// Token: 0x04000389 RID: 905
		private List<ITreeViewItem> m_SelectedItems;

		// Token: 0x0400038A RID: 906
		private Action<VisualElement, ITreeViewItem> m_BindItem;

		// Token: 0x0400038C RID: 908
		private IList<ITreeViewItem> m_RootItems;

		// Token: 0x0400038D RID: 909
		[SerializeField]
		private List<int> m_ExpandedItemIds;

		// Token: 0x0400038E RID: 910
		private List<TreeView.TreeViewItemWrapper> m_ItemWrappers;

		// Token: 0x0400038F RID: 911
		private readonly ListView m_ListView;

		// Token: 0x04000390 RID: 912
		private readonly ScrollView m_ScrollView;

		// Token: 0x02000106 RID: 262
		public new class UxmlFactory : UxmlFactory<TreeView, TreeView.UxmlTraits>
		{
		}

		// Token: 0x02000107 RID: 263
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x170001D9 RID: 473
			// (get) Token: 0x060007F3 RID: 2035 RVA: 0x00020DA8 File Offset: 0x0001EFA8
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}

			// Token: 0x060007F4 RID: 2036 RVA: 0x00020DC8 File Offset: 0x0001EFC8
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				int num = 0;
				bool flag = this.m_ItemHeight.TryGetValueFromBag(bag, cc, ref num);
				if (flag)
				{
					((TreeView)ve).itemHeight = num;
				}
				((TreeView)ve).showBorder = this.m_ShowBorder.GetValueFromBag(bag, cc);
				((TreeView)ve).selectionType = this.m_SelectionType.GetValueFromBag(bag, cc);
				((TreeView)ve).showAlternatingRowBackgrounds = this.m_ShowAlternatingRowBackgrounds.GetValueFromBag(bag, cc);
			}

			// Token: 0x04000391 RID: 913
			private readonly UxmlIntAttributeDescription m_ItemHeight = new UxmlIntAttributeDescription
			{
				name = "item-height",
				defaultValue = ListView.s_DefaultItemHeight
			};

			// Token: 0x04000392 RID: 914
			private readonly UxmlBoolAttributeDescription m_ShowBorder = new UxmlBoolAttributeDescription
			{
				name = "show-border",
				defaultValue = false
			};

			// Token: 0x04000393 RID: 915
			private readonly UxmlEnumAttributeDescription<SelectionType> m_SelectionType = new UxmlEnumAttributeDescription<SelectionType>
			{
				name = "selection-type",
				defaultValue = SelectionType.Single
			};

			// Token: 0x04000394 RID: 916
			private readonly UxmlEnumAttributeDescription<AlternatingRowBackground> m_ShowAlternatingRowBackgrounds = new UxmlEnumAttributeDescription<AlternatingRowBackground>
			{
				name = "show-alternating-row-backgrounds",
				defaultValue = AlternatingRowBackground.None
			};
		}

		// Token: 0x02000109 RID: 265
		private struct TreeViewItemWrapper
		{
			// Token: 0x170001DC RID: 476
			// (get) Token: 0x060007FE RID: 2046 RVA: 0x00020F84 File Offset: 0x0001F184
			public int id
			{
				get
				{
					return this.item.id;
				}
			}

			// Token: 0x170001DD RID: 477
			// (get) Token: 0x060007FF RID: 2047 RVA: 0x00020F91 File Offset: 0x0001F191
			public bool hasChildren
			{
				get
				{
					return this.item.hasChildren;
				}
			}

			// Token: 0x04000399 RID: 921
			public int depth;

			// Token: 0x0400039A RID: 922
			public ITreeViewItem item;
		}
	}
}
