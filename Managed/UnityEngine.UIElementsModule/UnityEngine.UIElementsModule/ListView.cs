using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace UnityEngine.UIElements
{
	// Token: 0x020000D6 RID: 214
	public class ListView : BindableElement, ISerializationCallbackReceiver
	{
		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060005F2 RID: 1522 RVA: 0x0001754C File Offset: 0x0001574C
		// (remove) Token: 0x060005F3 RID: 1523 RVA: 0x00017584 File Offset: 0x00015784
		[Obsolete("onItemChosen is obsolete, use onItemsChosen instead")]
		[field: DebuggerBrowsable(0)]
		public event Action<object> onItemChosen;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060005F4 RID: 1524 RVA: 0x000175BC File Offset: 0x000157BC
		// (remove) Token: 0x060005F5 RID: 1525 RVA: 0x000175F4 File Offset: 0x000157F4
		[field: DebuggerBrowsable(0)]
		public event Action<IEnumerable<object>> onItemsChosen;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060005F6 RID: 1526 RVA: 0x0001762C File Offset: 0x0001582C
		// (remove) Token: 0x060005F7 RID: 1527 RVA: 0x00017664 File Offset: 0x00015864
		[Obsolete("onSelectionChanged is obsolete, use onSelectionChange instead")]
		[field: DebuggerBrowsable(0)]
		public event Action<List<object>> onSelectionChanged;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060005F8 RID: 1528 RVA: 0x0001769C File Offset: 0x0001589C
		// (remove) Token: 0x060005F9 RID: 1529 RVA: 0x000176D4 File Offset: 0x000158D4
		[field: DebuggerBrowsable(0)]
		public event Action<IEnumerable<object>> onSelectionChange;

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x0001770C File Offset: 0x0001590C
		// (set) Token: 0x060005FB RID: 1531 RVA: 0x00017724 File Offset: 0x00015924
		public IList itemsSource
		{
			get
			{
				return this.m_ItemsSource;
			}
			set
			{
				this.m_ItemsSource = value;
				this.Refresh();
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x00017738 File Offset: 0x00015938
		// (set) Token: 0x060005FD RID: 1533 RVA: 0x00017750 File Offset: 0x00015950
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
					this.Refresh();
				}
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x0001777E File Offset: 0x0001597E
		// (set) Token: 0x060005FF RID: 1535 RVA: 0x00017786 File Offset: 0x00015986
		public Action<VisualElement, int> unbindItem { get; set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x00017790 File Offset: 0x00015990
		// (set) Token: 0x06000601 RID: 1537 RVA: 0x000177A8 File Offset: 0x000159A8
		public Action<VisualElement, int> bindItem
		{
			get
			{
				return this.m_BindItem;
			}
			set
			{
				this.m_BindItem = value;
				this.Refresh();
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x000177BC File Offset: 0x000159BC
		// (set) Token: 0x06000603 RID: 1539 RVA: 0x000177D4 File Offset: 0x000159D4
		internal Func<int, int> getItemId
		{
			get
			{
				return this.m_GetItemId;
			}
			set
			{
				this.m_GetItemId = value;
				this.Refresh();
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x000177E8 File Offset: 0x000159E8
		public float resolvedItemHeight
		{
			get
			{
				float scaledPixelsPerPoint = base.scaledPixelsPerPoint;
				return Mathf.Round((float)this.itemHeight * scaledPixelsPerPoint) / scaledPixelsPerPoint;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x00017814 File Offset: 0x00015A14
		internal List<ListView.RecycledItem> Pool
		{
			get
			{
				return this.m_Pool;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x0001782C File Offset: 0x00015A2C
		// (set) Token: 0x06000607 RID: 1543 RVA: 0x00017844 File Offset: 0x00015A44
		public int itemHeight
		{
			get
			{
				return this.m_ItemHeight;
			}
			set
			{
				this.m_ItemHeightIsInline = true;
				bool flag = this.m_ItemHeight != value;
				if (flag)
				{
					this.m_ItemHeight = value;
					this.Refresh();
				}
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x0001787C File Offset: 0x00015A7C
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x00017899 File Offset: 0x00015A99
		public bool showBorder
		{
			get
			{
				return base.ClassListContains(ListView.borderUssClassName);
			}
			set
			{
				base.EnableInClassList(ListView.borderUssClassName, value);
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x000178AC File Offset: 0x00015AAC
		// (set) Token: 0x0600060B RID: 1547 RVA: 0x000178E0 File Offset: 0x00015AE0
		public bool reorderable
		{
			get
			{
				ListViewDragger dragger = this.m_Dragger;
				IListViewDragAndDropController listViewDragAndDropController = ((dragger != null) ? dragger.dragAndDropController : null);
				return listViewDragAndDropController != null && listViewDragAndDropController.enableReordering;
			}
			set
			{
				ListViewDragger dragger = this.m_Dragger;
				bool flag = ((dragger != null) ? dragger.dragAndDropController : null) == null;
				if (flag)
				{
					if (value)
					{
						this.SetDragAndDropController(new ListViewReorderableDragAndDropController(this));
					}
				}
				else
				{
					IListViewDragAndDropController dragAndDropController = this.m_Dragger.dragAndDropController;
					bool flag2 = dragAndDropController != null;
					if (flag2)
					{
						dragAndDropController.enableReordering = value;
					}
				}
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x00017938 File Offset: 0x00015B38
		internal List<int> currentSelectionIds
		{
			get
			{
				return this.m_SelectedIds;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x00017940 File Offset: 0x00015B40
		// (set) Token: 0x0600060E RID: 1550 RVA: 0x0001796D File Offset: 0x00015B6D
		public int selectedIndex
		{
			get
			{
				return (this.m_SelectedIndices.Count == 0) ? (-1) : Enumerable.First<int>(this.m_SelectedIndices);
			}
			set
			{
				this.SetSelection(value);
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x00017978 File Offset: 0x00015B78
		public IEnumerable<int> selectedIndices
		{
			get
			{
				return this.m_SelectedIndices;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x00017980 File Offset: 0x00015B80
		public object selectedItem
		{
			get
			{
				return (this.m_SelectedItems.Count == 0) ? null : Enumerable.First<object>(this.m_SelectedItems);
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0001799D File Offset: 0x00015B9D
		public IEnumerable<object> selectedItems
		{
			get
			{
				return this.m_SelectedItems;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x000179A5 File Offset: 0x00015BA5
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_ScrollView.contentContainer;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x000179B4 File Offset: 0x00015BB4
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x000179CC File Offset: 0x00015BCC
		public SelectionType selectionType
		{
			get
			{
				return this.m_SelectionType;
			}
			set
			{
				this.m_SelectionType = value;
				bool flag = this.m_SelectionType == SelectionType.None;
				if (flag)
				{
					this.ClearSelection();
				}
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x000179F8 File Offset: 0x00015BF8
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x00017A10 File Offset: 0x00015C10
		public AlternatingRowBackground showAlternatingRowBackgrounds
		{
			get
			{
				return this.m_ShowAlternatingRowBackgrounds;
			}
			set
			{
				bool flag = this.m_ShowAlternatingRowBackgrounds == value;
				if (!flag)
				{
					this.m_ShowAlternatingRowBackgrounds = value;
					this.Refresh();
				}
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x00017A3B File Offset: 0x00015C3B
		// (set) Token: 0x06000618 RID: 1560 RVA: 0x00017A43 File Offset: 0x00015C43
		public bool showBoundCollectionSize { get; set; } = true;

		// Token: 0x06000619 RID: 1561 RVA: 0x00017A4C File Offset: 0x00015C4C
		public ListView()
		{
			base.AddToClassList(ListView.ussClassName);
			this.selectionType = SelectionType.Single;
			this.m_ScrollOffset = 0f;
			this.m_ScrollView = new ScrollView();
			this.m_ScrollView.viewDataKey = "list-view__scroll-view";
			this.m_ScrollView.StretchToParentSize();
			this.m_ScrollView.verticalScroller.valueChanged += new Action<float>(this.OnScroll);
			base.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnSizeChanged), TrickleDown.NoTrickleDown);
			base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), TrickleDown.NoTrickleDown);
			this.m_ScrollView.contentContainer.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
			this.m_ScrollView.contentContainer.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnDetachFromPanel), TrickleDown.NoTrickleDown);
			base.hierarchy.Add(this.m_ScrollView);
			this.m_ScrollView.contentContainer.focusable = true;
			this.m_ScrollView.contentContainer.usageHints &= ~UsageHints.GroupTransform;
			this.m_EmptyRows = new VisualElement();
			this.m_EmptyRows.AddToClassList(ListView.s_BackgroundFillUssClassName);
			base.focusable = true;
			base.isCompositeRoot = true;
			base.delegatesFocus = true;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00017BFD File Offset: 0x00015DFD
		public ListView(IList itemsSource, int itemHeight, Func<VisualElement> makeItem, Action<VisualElement, int> bindItem)
			: this()
		{
			this.m_ItemsSource = itemsSource;
			this.m_ItemHeight = itemHeight;
			this.m_ItemHeightIsInline = true;
			this.m_MakeItem = makeItem;
			this.m_BindItem = bindItem;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00017C2C File Offset: 0x00015E2C
		private void OnAttachToPanel(AttachToPanelEvent evt)
		{
			bool flag = evt.destinationPanel == null;
			if (!flag)
			{
				this.m_ScrollView.contentContainer.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
				this.m_ScrollView.contentContainer.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
				this.m_ScrollView.contentContainer.RegisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00017CA4 File Offset: 0x00015EA4
		private void OnDetachFromPanel(DetachFromPanelEvent evt)
		{
			bool flag = evt.originPanel == null;
			if (!flag)
			{
				this.m_ScrollView.contentContainer.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
				this.m_ScrollView.contentContainer.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
				this.m_ScrollView.contentContainer.UnregisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00017D1C File Offset: 0x00015F1C
		public void OnKeyDown(KeyDownEvent evt)
		{
			bool flag = evt == null || !this.HasValidDataAndBindings();
			if (!flag)
			{
				bool flag2 = true;
				bool flag3 = true;
				KeyCode keyCode = evt.keyCode;
				if (keyCode <= KeyCode.Escape)
				{
					if (keyCode == KeyCode.Return)
					{
						Action<object> action = this.onItemChosen;
						if (action != null)
						{
							action.Invoke(this.m_ItemsSource[this.selectedIndex]);
						}
						Action<IEnumerable<object>> action2 = this.onItemsChosen;
						if (action2 != null)
						{
							action2.Invoke(this.m_SelectedItems);
						}
						goto IL_01B2;
					}
					if (keyCode == KeyCode.Escape)
					{
						this.ClearSelection();
						flag3 = false;
						goto IL_01B2;
					}
				}
				else
				{
					if (keyCode == KeyCode.A)
					{
						bool actionKey = evt.actionKey;
						if (actionKey)
						{
							this.SelectAll();
							flag3 = false;
						}
						goto IL_01B2;
					}
					switch (keyCode)
					{
					case KeyCode.UpArrow:
					{
						bool flag4 = this.selectedIndex > 0;
						if (flag4)
						{
							this.selectedIndex--;
						}
						goto IL_01B2;
					}
					case KeyCode.DownArrow:
					{
						bool flag5 = this.selectedIndex + 1 < this.itemsSource.Count;
						if (flag5)
						{
							this.selectedIndex++;
						}
						goto IL_01B2;
					}
					case KeyCode.Home:
						this.selectedIndex = 0;
						goto IL_01B2;
					case KeyCode.End:
						this.selectedIndex = this.itemsSource.Count - 1;
						goto IL_01B2;
					case KeyCode.PageUp:
						this.selectedIndex = Math.Max(0, this.selectedIndex - (int)(this.m_LastHeight / this.resolvedItemHeight));
						goto IL_01B2;
					case KeyCode.PageDown:
						this.selectedIndex = Math.Min(this.itemsSource.Count - 1, this.selectedIndex + (int)(this.m_LastHeight / this.resolvedItemHeight));
						goto IL_01B2;
					}
				}
				flag2 = false;
				flag3 = false;
				IL_01B2:
				bool flag6 = flag2;
				if (flag6)
				{
					evt.StopPropagation();
				}
				bool flag7 = flag3;
				if (flag7)
				{
					this.ScrollToItem(this.selectedIndex);
				}
			}
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00017F00 File Offset: 0x00016100
		public void ScrollToItem(int index)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (flag)
			{
				throw new InvalidOperationException("Can't scroll without valid source, bind method, or factory method.");
			}
			bool flag2 = this.m_VisibleItemCount == 0 || index < -1;
			if (!flag2)
			{
				float resolvedItemHeight = this.resolvedItemHeight;
				bool flag3 = index == -1;
				if (flag3)
				{
					int num = (int)(this.m_LastHeight / resolvedItemHeight);
					bool flag4 = this.itemsSource.Count < num;
					if (flag4)
					{
						this.m_ScrollView.scrollOffset = new Vector2(0f, 0f);
					}
					else
					{
						this.m_ScrollView.scrollOffset = new Vector2(0f, (float)this.itemsSource.Count * resolvedItemHeight);
					}
				}
				else
				{
					bool flag5 = this.m_FirstVisibleIndex > index;
					if (flag5)
					{
						this.m_ScrollView.scrollOffset = Vector2.up * resolvedItemHeight * (float)index;
					}
					else
					{
						int num2 = (int)(this.m_LastHeight / resolvedItemHeight);
						bool flag6 = index < this.m_FirstVisibleIndex + num2;
						if (!flag6)
						{
							bool flag7 = (int)(this.m_LastHeight - (float)num2 * resolvedItemHeight) != 0;
							int num3 = index - num2;
							bool flag8 = flag7;
							if (flag8)
							{
								num3++;
							}
							this.m_ScrollView.scrollOffset = Vector2.up * resolvedItemHeight * (float)num3;
						}
					}
				}
			}
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001804C File Offset: 0x0001624C
		private void OnMouseDown(MouseDownEvent evt)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				bool flag2 = evt.button != 0;
				if (!flag2)
				{
					this.DoSelect(evt.localMousePosition, evt.clickCount, evt.actionKey, evt.shiftKey);
				}
			}
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00018098 File Offset: 0x00016298
		private void OnPointerDown(PointerDownEvent evt)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				bool flag2 = !evt.isPrimary;
				if (!flag2)
				{
					bool flag3 = evt.button != 0;
					if (!flag3)
					{
						bool flag4 = evt.pointerType != PointerType.mouse;
						if (flag4)
						{
							this.m_TouchDownTime = evt.timestamp;
							this.m_TouchDownPosition = evt.position;
						}
						else
						{
							this.DoSelect(evt.localPosition, evt.clickCount, evt.actionKey, evt.shiftKey);
						}
					}
				}
			}
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00018128 File Offset: 0x00016328
		private void OnPointerUp(PointerUpEvent evt)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				bool flag2 = !evt.isPrimary;
				if (!flag2)
				{
					bool flag3 = evt.button != 0;
					if (!flag3)
					{
						bool flag4 = evt.pointerType != PointerType.mouse;
						if (flag4)
						{
							long num = evt.timestamp - this.m_TouchDownTime;
							Vector3 vector = evt.position - this.m_TouchDownPosition;
							bool flag5 = num < 500L && vector.sqrMagnitude <= 100f;
							if (flag5)
							{
								this.DoSelect(evt.localPosition, evt.clickCount, evt.actionKey, evt.shiftKey);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x000181F0 File Offset: 0x000163F0
		private void DoSelect(Vector2 localPosition, int clickCount, bool actionKey, bool shiftKey)
		{
			int num = (int)(localPosition.y / this.resolvedItemHeight);
			bool flag = num > this.m_ItemsSource.Count - 1;
			if (!flag)
			{
				int idFromIndex = this.GetIdFromIndex(num);
				if (clickCount != 1)
				{
					if (clickCount == 2)
					{
						bool flag2 = this.onItemsChosen != null;
						if (flag2)
						{
							this.ProcessSingleClick(num);
						}
						Action<IEnumerable<object>> action = this.onItemsChosen;
						if (action != null)
						{
							action.Invoke(this.m_SelectedItems);
						}
					}
				}
				else
				{
					bool flag3 = this.selectionType == SelectionType.None;
					if (!flag3)
					{
						bool flag4 = this.selectionType == SelectionType.Multiple && actionKey;
						if (flag4)
						{
							this.m_RangeSelectionOrigin = num;
							bool flag5 = this.m_SelectedIds.Contains(idFromIndex);
							if (flag5)
							{
								this.RemoveFromSelection(num);
							}
							else
							{
								this.AddToSelection(num);
							}
						}
						else
						{
							bool flag6 = this.selectionType == SelectionType.Multiple && shiftKey;
							if (flag6)
							{
								bool flag7 = this.m_RangeSelectionOrigin == -1;
								if (flag7)
								{
									this.m_RangeSelectionOrigin = num;
									this.SetSelection(num);
								}
								else
								{
									this.ClearSelectionWithoutValidation();
									bool flag8 = num < this.m_RangeSelectionOrigin;
									if (flag8)
									{
										for (int i = num; i <= this.m_RangeSelectionOrigin; i++)
										{
											this.AddToSelection(i);
										}
									}
									else
									{
										for (int j = this.m_RangeSelectionOrigin; j <= num; j++)
										{
											this.AddToSelection(j);
										}
									}
								}
							}
							else
							{
								bool flag9 = this.selectionType == SelectionType.Multiple && this.m_SelectedIndices.Contains(num);
								if (!flag9)
								{
									this.m_RangeSelectionOrigin = num;
									this.SetSelection(num);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x000183A8 File Offset: 0x000165A8
		private void ProcessSingleClick(int clickedIndex)
		{
			this.m_RangeSelectionOrigin = clickedIndex;
			this.SetSelection(clickedIndex);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x000183BC File Offset: 0x000165BC
		private void OnMouseUp(MouseUpEvent evt)
		{
			int num = (int)(evt.localMousePosition.y / (float)this.itemHeight);
			bool flag = this.selectionType == SelectionType.Multiple && !evt.shiftKey && !evt.actionKey && this.m_SelectedIndices.Count > 1 && this.m_SelectedIndices.Contains(num);
			if (flag)
			{
				this.ProcessSingleClick(num);
			}
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00018424 File Offset: 0x00016624
		internal void SelectAll()
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				bool flag2 = this.selectionType != SelectionType.Multiple;
				if (!flag2)
				{
					for (int i = 0; i < this.itemsSource.Count; i++)
					{
						int idFromIndex = this.GetIdFromIndex(i);
						object obj = this.m_ItemsSource[i];
						foreach (ListView.RecycledItem recycledItem in this.m_Pool)
						{
							bool flag3 = recycledItem.id == idFromIndex;
							if (flag3)
							{
								recycledItem.SetSelected(true);
							}
						}
						bool flag4 = !this.m_SelectedIds.Contains(idFromIndex);
						if (flag4)
						{
							this.m_SelectedIds.Add(idFromIndex);
							this.m_SelectedIndices.Add(i);
							this.m_SelectedItems.Add(obj);
						}
					}
					this.NotifyOfSelectionChange();
					base.SaveViewData();
				}
			}
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00018540 File Offset: 0x00016740
		private int GetIdFromIndex(int index)
		{
			bool flag = this.m_GetItemId == null;
			int num;
			if (flag)
			{
				num = index;
			}
			else
			{
				num = this.m_GetItemId.Invoke(index);
			}
			return num;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00018570 File Offset: 0x00016770
		public void AddToSelection(int index)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				this.AddToSelectionWithoutValidation(index);
				this.NotifyOfSelectionChange();
				base.SaveViewData();
			}
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x000185A4 File Offset: 0x000167A4
		private void AddToSelectionWithoutValidation(int index)
		{
			bool flag = this.m_SelectedIndices.Contains(index);
			if (!flag)
			{
				int idFromIndex = this.GetIdFromIndex(index);
				object obj = this.m_ItemsSource[index];
				foreach (ListView.RecycledItem recycledItem in this.m_Pool)
				{
					bool flag2 = recycledItem.id == idFromIndex;
					if (flag2)
					{
						recycledItem.SetSelected(true);
					}
				}
				this.m_SelectedIds.Add(idFromIndex);
				this.m_SelectedIndices.Add(index);
				this.m_SelectedItems.Add(obj);
			}
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00018660 File Offset: 0x00016860
		public void RemoveFromSelection(int index)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				this.RemoveFromSelectionWithoutValidation(index);
				this.NotifyOfSelectionChange();
				base.SaveViewData();
			}
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00018694 File Offset: 0x00016894
		private void RemoveFromSelectionWithoutValidation(int index)
		{
			bool flag = !this.m_SelectedIndices.Contains(index);
			if (!flag)
			{
				int idFromIndex = this.GetIdFromIndex(index);
				object obj = this.m_ItemsSource[index];
				foreach (ListView.RecycledItem recycledItem in this.m_Pool)
				{
					bool flag2 = recycledItem.id == idFromIndex;
					if (flag2)
					{
						recycledItem.SetSelected(false);
					}
				}
				this.m_SelectedIds.Remove(idFromIndex);
				this.m_SelectedIndices.Remove(index);
				this.m_SelectedItems.Remove(obj);
			}
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00018754 File Offset: 0x00016954
		public void SetSelection(int index)
		{
			bool flag = index < 0;
			if (flag)
			{
				this.ClearSelection();
			}
			else
			{
				this.SetSelection(new int[] { index });
			}
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00018785 File Offset: 0x00016985
		public void SetSelection(IEnumerable<int> indices)
		{
			this.SetSelectionInternal(indices, true);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00018791 File Offset: 0x00016991
		public void SetSelectionWithoutNotify(IEnumerable<int> indices)
		{
			this.SetSelectionInternal(indices, false);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x000187A0 File Offset: 0x000169A0
		internal void SetSelectionInternal(IEnumerable<int> indices, bool sendNotification)
		{
			bool flag = !this.HasValidDataAndBindings() || indices == null;
			if (!flag)
			{
				this.ClearSelectionWithoutValidation();
				foreach (int num in indices)
				{
					this.AddToSelectionWithoutValidation(num);
				}
				if (sendNotification)
				{
					this.NotifyOfSelectionChange();
				}
				base.SaveViewData();
			}
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001881C File Offset: 0x00016A1C
		private void NotifyOfSelectionChange()
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				Action<IEnumerable<object>> action = this.onSelectionChange;
				if (action != null)
				{
					action.Invoke(this.m_SelectedItems);
				}
				Action<List<object>> action2 = this.onSelectionChanged;
				if (action2 != null)
				{
					action2.Invoke(this.m_SelectedItems);
				}
			}
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0001886C File Offset: 0x00016A6C
		public void ClearSelection()
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				this.ClearSelectionWithoutValidation();
				this.NotifyOfSelectionChange();
			}
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00018898 File Offset: 0x00016A98
		private void ClearSelectionWithoutValidation()
		{
			foreach (ListView.RecycledItem recycledItem in this.m_Pool)
			{
				recycledItem.SetSelected(false);
			}
			this.m_SelectedIds.Clear();
			this.m_SelectedIndices.Clear();
			this.m_SelectedItems.Clear();
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00018914 File Offset: 0x00016B14
		public void ScrollTo(VisualElement visualElement)
		{
			this.m_ScrollView.ScrollTo(visualElement);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00018924 File Offset: 0x00016B24
		internal void SetDragAndDropController(IListViewDragAndDropController dragAndDropController)
		{
			bool flag = this.m_Dragger == null;
			if (flag)
			{
				this.m_Dragger = new ListViewDragger(this);
			}
			this.m_Dragger.dragAndDropController = dragAndDropController;
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00018958 File Offset: 0x00016B58
		internal IListViewDragAndDropController GetDragAndDropController()
		{
			ListViewDragger dragger = this.m_Dragger;
			return (dragger != null) ? dragger.dragAndDropController : null;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001897C File Offset: 0x00016B7C
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
			base.OverwriteFromViewData(this, fullHierarchicalViewDataKey);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x000189A4 File Offset: 0x00016BA4
		private void OnScroll(float offset)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				this.m_ScrollOffset = offset;
				float resolvedItemHeight = this.resolvedItemHeight;
				int num = (int)(offset / resolvedItemHeight);
				this.m_ScrollView.contentContainer.style.height = (float)this.itemsSource.Count * resolvedItemHeight;
				bool flag2 = num != this.m_FirstVisibleIndex;
				if (flag2)
				{
					this.m_FirstVisibleIndex = num;
					bool flag3 = this.m_Pool.Count > 0;
					if (flag3)
					{
						bool flag4 = this.m_FirstVisibleIndex < this.m_Pool[0].index;
						if (flag4)
						{
							int num2 = this.m_Pool[0].index - this.m_FirstVisibleIndex;
							List<ListView.RecycledItem> scrollInsertionList = this.m_ScrollInsertionList;
							int num3 = 0;
							while (num3 < num2 && this.m_Pool.Count > 0)
							{
								ListView.RecycledItem recycledItem = this.m_Pool[this.m_Pool.Count - 1];
								scrollInsertionList.Add(recycledItem);
								this.m_Pool.RemoveAt(this.m_Pool.Count - 1);
								recycledItem.element.SendToBack();
								num3++;
							}
							this.m_ScrollInsertionList = this.m_Pool;
							this.m_Pool = scrollInsertionList;
							this.m_Pool.AddRange(this.m_ScrollInsertionList);
							this.m_ScrollInsertionList.Clear();
						}
						else
						{
							bool flag5 = this.m_FirstVisibleIndex < this.m_Pool[this.m_Pool.Count - 1].index;
							if (flag5)
							{
								List<ListView.RecycledItem> scrollInsertionList2 = this.m_ScrollInsertionList;
								int num4 = 0;
								while (this.m_FirstVisibleIndex > this.m_Pool[num4].index)
								{
									ListView.RecycledItem recycledItem2 = this.m_Pool[num4];
									scrollInsertionList2.Add(recycledItem2);
									num4++;
									recycledItem2.element.BringToFront();
								}
								this.m_Pool.RemoveRange(0, num4);
								this.m_Pool.AddRange(scrollInsertionList2);
								scrollInsertionList2.Clear();
							}
						}
						int num5 = 0;
						while (num5 < this.m_Pool.Count && num5 + this.m_FirstVisibleIndex < this.itemsSource.Count)
						{
							this.Setup(this.m_Pool[num5], num5 + this.m_FirstVisibleIndex);
							num5++;
						}
					}
				}
			}
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00018C30 File Offset: 0x00016E30
		private bool HasValidDataAndBindings()
		{
			return this.itemsSource != null && this.makeItem != null && this.bindItem != null;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00018C60 File Offset: 0x00016E60
		public void Refresh()
		{
			foreach (ListView.RecycledItem recycledItem in this.m_Pool)
			{
				recycledItem.DetachElement();
			}
			this.m_Pool.Clear();
			this.m_ScrollView.Clear();
			this.m_VisibleItemCount = 0;
			this.m_SelectedIndices.Clear();
			this.m_SelectedItems.Clear();
			bool flag = this.m_SelectedIds.Count > 0;
			if (flag)
			{
				for (int i = 0; i < this.m_ItemsSource.Count; i++)
				{
					bool flag2 = !this.m_SelectedIds.Contains(this.GetIdFromIndex(i));
					if (!flag2)
					{
						this.m_SelectedIndices.Add(i);
						this.m_SelectedItems.Add(this.m_ItemsSource[i]);
					}
				}
			}
			bool flag3 = !this.HasValidDataAndBindings();
			if (!flag3)
			{
				this.m_LastHeight = this.m_ScrollView.layout.height;
				bool flag4 = float.IsNaN(this.m_LastHeight);
				if (!flag4)
				{
					this.m_FirstVisibleIndex = (int)(this.m_ScrollOffset / this.resolvedItemHeight);
					this.ResizeHeight(this.m_LastHeight);
				}
			}
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00018DC0 File Offset: 0x00016FC0
		private void ResizeHeight(float height)
		{
			float resolvedItemHeight = this.resolvedItemHeight;
			float num = (float)this.itemsSource.Count * resolvedItemHeight;
			this.m_ScrollView.contentContainer.style.height = num;
			float num2 = Mathf.Max(0f, num - this.m_ScrollView.contentViewport.layout.height);
			this.m_ScrollView.verticalScroller.highValue = Mathf.Min(Mathf.Max(this.m_ScrollOffset, this.m_ScrollView.verticalScroller.highValue), num2);
			this.m_ScrollView.verticalScroller.value = Mathf.Min(this.m_ScrollOffset, this.m_ScrollView.verticalScroller.highValue);
			int num3 = Math.Min((int)(height / resolvedItemHeight) + 2, this.itemsSource.Count);
			bool flag = this.m_VisibleItemCount != num3;
			if (flag)
			{
				bool flag2 = this.m_VisibleItemCount > num3;
				if (flag2)
				{
					int num4 = this.m_VisibleItemCount - num3;
					for (int i = 0; i < num4; i++)
					{
						int num5 = this.m_Pool.Count - 1;
						ListView.RecycledItem recycledItem = this.m_Pool[num5];
						recycledItem.element.RemoveFromHierarchy();
						recycledItem.DetachElement();
						this.m_Pool.RemoveAt(num5);
					}
				}
				else
				{
					int num6 = num3 - this.m_VisibleItemCount;
					for (int j = 0; j < num6; j++)
					{
						int num7 = j + this.m_FirstVisibleIndex + this.m_VisibleItemCount;
						VisualElement visualElement = this.makeItem.Invoke();
						ListView.RecycledItem recycledItem2 = new ListView.RecycledItem(visualElement);
						this.m_Pool.Add(recycledItem2);
						visualElement.AddToClassList("unity-listview-item");
						visualElement.style.marginTop = 0f;
						visualElement.style.marginBottom = 0f;
						visualElement.style.position = Position.Absolute;
						visualElement.style.left = 0f;
						visualElement.style.right = 0f;
						visualElement.style.height = resolvedItemHeight;
						bool flag3 = num7 < this.itemsSource.Count;
						if (flag3)
						{
							this.Setup(recycledItem2, num7);
						}
						else
						{
							visualElement.style.visibility = Visibility.Hidden;
						}
						base.Add(visualElement);
					}
				}
				this.m_VisibleItemCount = num3;
			}
			this.m_LastHeight = height;
			this.UpdateBackground();
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00019074 File Offset: 0x00017274
		private void Setup(ListView.RecycledItem recycledItem, int newIndex)
		{
			int idFromIndex = this.GetIdFromIndex(newIndex);
			recycledItem.element.style.visibility = Visibility.Visible;
			bool flag = recycledItem.index == newIndex;
			if (!flag)
			{
				this.m_LastItemIndex = newIndex;
				bool flag2 = this.showAlternatingRowBackgrounds != AlternatingRowBackground.None && newIndex % 2 == 1;
				if (flag2)
				{
					recycledItem.element.AddToClassList(ListView.itemAlternativeBackgroundUssClassName);
				}
				else
				{
					recycledItem.element.RemoveFromClassList(ListView.itemAlternativeBackgroundUssClassName);
				}
				bool flag3 = recycledItem.index != -1;
				if (flag3)
				{
					Action<VisualElement, int> unbindItem = this.unbindItem;
					if (unbindItem != null)
					{
						unbindItem.Invoke(recycledItem.element, recycledItem.index);
					}
				}
				float resolvedItemHeight = this.resolvedItemHeight;
				recycledItem.index = newIndex;
				recycledItem.id = idFromIndex;
				recycledItem.element.style.top = (float)recycledItem.index * resolvedItemHeight;
				recycledItem.element.style.bottom = (float)(this.itemsSource.Count - recycledItem.index - 1) * resolvedItemHeight;
				this.bindItem.Invoke(recycledItem.element, recycledItem.index);
				recycledItem.SetSelected(this.m_SelectedIds.Contains(idFromIndex));
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x000191B0 File Offset: 0x000173B0
		private void UpdateBackground()
		{
			float num = this.m_ScrollView.contentViewport.layout.size.y - this.m_ScrollView.contentContainer.layout.size.y;
			bool flag = this.showAlternatingRowBackgrounds != AlternatingRowBackground.All || num <= 0f;
			if (flag)
			{
				this.m_EmptyRows.RemoveFromHierarchy();
			}
			else
			{
				bool flag2 = this.m_EmptyRows.parent == null;
				if (flag2)
				{
					this.m_ScrollView.contentViewport.Add(this.m_EmptyRows);
				}
				float resolvedItemHeight = this.resolvedItemHeight;
				int num2 = Mathf.FloorToInt(num / resolvedItemHeight) + 1;
				bool flag3 = num2 > this.m_EmptyRows.childCount;
				if (flag3)
				{
					int num3 = num2 - this.m_EmptyRows.childCount;
					for (int i = 0; i < num3; i++)
					{
						VisualElement visualElement = new VisualElement();
						visualElement.style.flexShrink = 0f;
						this.m_EmptyRows.Add(visualElement);
					}
				}
				int num4 = this.m_LastItemIndex;
				foreach (VisualElement visualElement2 in this.m_EmptyRows.hierarchy.Children())
				{
					num4++;
					visualElement2.style.height = resolvedItemHeight;
					visualElement2.EnableInClassList(ListView.itemAlternativeBackgroundUssClassName, num4 % 2 == 1);
				}
			}
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00019354 File Offset: 0x00017554
		private void OnSizeChanged(GeometryChangedEvent evt)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				bool flag2 = Mathf.Approximately(evt.newRect.height, evt.oldRect.height);
				if (!flag2)
				{
					this.ResizeHeight(evt.newRect.height);
				}
			}
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x000193B0 File Offset: 0x000175B0
		private void OnCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			int num;
			bool flag = !this.m_ItemHeightIsInline && e.customStyle.TryGetValue(ListView.s_ItemHeightProperty, out num);
			if (flag)
			{
				bool flag2 = this.m_ItemHeight != num;
				if (flag2)
				{
					this.m_ItemHeight = num;
					this.Refresh();
				}
			}
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x000062F3 File Offset: 0x000044F3
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00019401 File Offset: 0x00017601
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.Refresh();
		}

		// Token: 0x040002A6 RID: 678
		private IList m_ItemsSource;

		// Token: 0x040002A7 RID: 679
		private Func<VisualElement> m_MakeItem;

		// Token: 0x040002A9 RID: 681
		private Action<VisualElement, int> m_BindItem;

		// Token: 0x040002AA RID: 682
		private Func<int, int> m_GetItemId;

		// Token: 0x040002AB RID: 683
		[SerializeField]
		internal int m_ItemHeight = ListView.s_DefaultItemHeight;

		// Token: 0x040002AC RID: 684
		[SerializeField]
		internal bool m_ItemHeightIsInline;

		// Token: 0x040002AD RID: 685
		[SerializeField]
		private float m_ScrollOffset;

		// Token: 0x040002AE RID: 686
		[SerializeField]
		private readonly List<int> m_SelectedIds = new List<int>();

		// Token: 0x040002AF RID: 687
		private readonly List<int> m_SelectedIndices = new List<int>();

		// Token: 0x040002B0 RID: 688
		private readonly List<object> m_SelectedItems = new List<object>();

		// Token: 0x040002B1 RID: 689
		private int m_RangeSelectionOrigin = -1;

		// Token: 0x040002B2 RID: 690
		private ListViewDragger m_Dragger;

		// Token: 0x040002B3 RID: 691
		private SelectionType m_SelectionType;

		// Token: 0x040002B4 RID: 692
		[SerializeField]
		private AlternatingRowBackground m_ShowAlternatingRowBackgrounds = AlternatingRowBackground.None;

		// Token: 0x040002B6 RID: 694
		internal static readonly int s_DefaultItemHeight = 30;

		// Token: 0x040002B7 RID: 695
		internal static CustomStyleProperty<int> s_ItemHeightProperty = new CustomStyleProperty<int>("--unity-item-height");

		// Token: 0x040002B8 RID: 696
		private int m_FirstVisibleIndex;

		// Token: 0x040002B9 RID: 697
		private float m_LastHeight;

		// Token: 0x040002BA RID: 698
		private List<ListView.RecycledItem> m_Pool = new List<ListView.RecycledItem>();

		// Token: 0x040002BB RID: 699
		internal readonly ScrollView m_ScrollView;

		// Token: 0x040002BC RID: 700
		private readonly VisualElement m_EmptyRows;

		// Token: 0x040002BD RID: 701
		private int m_LastItemIndex;

		// Token: 0x040002BE RID: 702
		private List<ListView.RecycledItem> m_ScrollInsertionList = new List<ListView.RecycledItem>();

		// Token: 0x040002BF RID: 703
		private const int k_ExtraVisibleItems = 2;

		// Token: 0x040002C0 RID: 704
		private int m_VisibleItemCount;

		// Token: 0x040002C1 RID: 705
		public static readonly string ussClassName = "unity-list-view";

		// Token: 0x040002C2 RID: 706
		public static readonly string borderUssClassName = ListView.ussClassName + "--with-border";

		// Token: 0x040002C3 RID: 707
		public static readonly string itemUssClassName = ListView.ussClassName + "__item";

		// Token: 0x040002C4 RID: 708
		public static readonly string dragHoverBarUssClassName = ListView.ussClassName + "__drag-hover-bar";

		// Token: 0x040002C5 RID: 709
		public static readonly string itemDragHoverUssClassName = ListView.itemUssClassName + "--drag-hover";

		// Token: 0x040002C6 RID: 710
		public static readonly string itemSelectedVariantUssClassName = ListView.itemUssClassName + "--selected";

		// Token: 0x040002C7 RID: 711
		public static readonly string itemAlternativeBackgroundUssClassName = ListView.itemUssClassName + "--alternative-background";

		// Token: 0x040002C8 RID: 712
		internal static readonly string s_BackgroundFillUssClassName = ListView.ussClassName + "__background";

		// Token: 0x040002C9 RID: 713
		private long m_TouchDownTime = 0L;

		// Token: 0x040002CA RID: 714
		private Vector3 m_TouchDownPosition;

		// Token: 0x020000D7 RID: 215
		public new class UxmlFactory : UxmlFactory<ListView, ListView.UxmlTraits>
		{
		}

		// Token: 0x020000D8 RID: 216
		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			// Token: 0x17000168 RID: 360
			// (get) Token: 0x06000642 RID: 1602 RVA: 0x000194D0 File Offset: 0x000176D0
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}

			// Token: 0x06000643 RID: 1603 RVA: 0x000194F0 File Offset: 0x000176F0
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				int num = 0;
				ListView listView = (ListView)ve;
				listView.reorderable = this.m_Reorderable.GetValueFromBag(bag, cc);
				bool flag = this.m_ItemHeight.TryGetValueFromBag(bag, cc, ref num);
				if (flag)
				{
					listView.itemHeight = num;
				}
				listView.showBorder = this.m_ShowBorder.GetValueFromBag(bag, cc);
				listView.selectionType = this.m_SelectionType.GetValueFromBag(bag, cc);
				listView.showAlternatingRowBackgrounds = this.m_ShowAlternatingRowBackgrounds.GetValueFromBag(bag, cc);
				listView.showBoundCollectionSize = this.m_ShowBoundCollectionSize.GetValueFromBag(bag, cc);
			}

			// Token: 0x040002CB RID: 715
			private readonly UxmlIntAttributeDescription m_ItemHeight = new UxmlIntAttributeDescription
			{
				name = "item-height",
				obsoleteNames = new string[] { "itemHeight" },
				defaultValue = ListView.s_DefaultItemHeight
			};

			// Token: 0x040002CC RID: 716
			private readonly UxmlBoolAttributeDescription m_ShowBorder = new UxmlBoolAttributeDescription
			{
				name = "show-border",
				defaultValue = false
			};

			// Token: 0x040002CD RID: 717
			private readonly UxmlEnumAttributeDescription<SelectionType> m_SelectionType = new UxmlEnumAttributeDescription<SelectionType>
			{
				name = "selection-type",
				defaultValue = SelectionType.Single
			};

			// Token: 0x040002CE RID: 718
			private readonly UxmlEnumAttributeDescription<AlternatingRowBackground> m_ShowAlternatingRowBackgrounds = new UxmlEnumAttributeDescription<AlternatingRowBackground>
			{
				name = "show-alternating-row-backgrounds",
				defaultValue = AlternatingRowBackground.None
			};

			// Token: 0x040002CF RID: 719
			private readonly UxmlBoolAttributeDescription m_Reorderable = new UxmlBoolAttributeDescription
			{
				name = "reorderable",
				defaultValue = false
			};

			// Token: 0x040002D0 RID: 720
			private readonly UxmlBoolAttributeDescription m_ShowBoundCollectionSize = new UxmlBoolAttributeDescription
			{
				name = "show-bound-collection-size",
				defaultValue = true
			};
		}

		// Token: 0x020000DA RID: 218
		internal class RecycledItem
		{
			// Token: 0x1700016B RID: 363
			// (get) Token: 0x0600064D RID: 1613 RVA: 0x00019720 File Offset: 0x00017920
			// (set) Token: 0x0600064E RID: 1614 RVA: 0x00019728 File Offset: 0x00017928
			public VisualElement element { get; private set; }

			// Token: 0x0600064F RID: 1615 RVA: 0x00019734 File Offset: 0x00017934
			public RecycledItem(VisualElement element)
			{
				this.element = element;
				this.index = (this.id = -1);
				element.AddToClassList(ListView.itemUssClassName);
			}

			// Token: 0x06000650 RID: 1616 RVA: 0x0001976D File Offset: 0x0001796D
			public void DetachElement()
			{
				this.element.RemoveFromClassList(ListView.itemUssClassName);
				this.element = null;
			}

			// Token: 0x06000651 RID: 1617 RVA: 0x0001978C File Offset: 0x0001798C
			public void SetSelected(bool selected)
			{
				bool flag = this.element != null;
				if (flag)
				{
					if (selected)
					{
						this.element.AddToClassList(ListView.itemSelectedVariantUssClassName);
						this.element.pseudoStates |= PseudoStates.Checked;
					}
					else
					{
						this.element.RemoveFromClassList(ListView.itemSelectedVariantUssClassName);
						this.element.pseudoStates &= ~PseudoStates.Checked;
					}
				}
			}

			// Token: 0x040002D5 RID: 725
			public const int kUndefinedIndex = -1;

			// Token: 0x040002D7 RID: 727
			public int index;

			// Token: 0x040002D8 RID: 728
			public int id;
		}
	}
}
