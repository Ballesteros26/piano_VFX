using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.UIElements
{
	// Token: 0x0200011C RID: 284
	internal class ListViewDragger : DragEventsProcessor
	{
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x00021D6C File Offset: 0x0001FF6C
		private ListView targetListView
		{
			get
			{
				return this.m_Target as ListView;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x00021D8C File Offset: 0x0001FF8C
		private ScrollView targetScrollView
		{
			get
			{
				return this.targetListView.m_ScrollView;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000866 RID: 2150 RVA: 0x00021DA9 File Offset: 0x0001FFA9
		// (set) Token: 0x06000867 RID: 2151 RVA: 0x00021DB1 File Offset: 0x0001FFB1
		public IListViewDragAndDropController dragAndDropController { get; set; }

		// Token: 0x06000868 RID: 2152 RVA: 0x00021DBC File Offset: 0x0001FFBC
		public ListViewDragger(ListView listView)
			: base(listView)
		{
			this.m_DragHoverBar = new VisualElement();
			this.m_DragHoverBar.AddToClassList(ListView.dragHoverBarUssClassName);
			this.m_DragHoverBar.style.width = this.targetListView.localBound.width;
			this.m_DragHoverBar.style.visibility = Visibility.Hidden;
			this.m_DragHoverBar.pickingMode = PickingMode.Ignore;
			this.targetListView.RegisterCallback<GeometryChangedEvent>(delegate(GeometryChangedEvent e)
			{
				this.m_DragHoverBar.style.width = this.targetListView.localBound.width;
			}, TrickleDown.NoTrickleDown);
			this.targetListView.m_ScrollView.contentViewport.Add(this.m_DragHoverBar);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00021E7C File Offset: 0x0002007C
		protected override bool CanStartDrag(Vector3 pointerPosition)
		{
			bool flag = this.dragAndDropController == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = !Enumerable.Any<object>(this.targetListView.selectedItems);
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					bool flag4 = !this.targetScrollView.contentContainer.worldBound.Contains(pointerPosition);
					flag2 = !flag4 && this.dragAndDropController.CanStartDrag(this.targetListView.selectedItems);
				}
			}
			return flag2;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00021EF8 File Offset: 0x000200F8
		protected override StartDragArgs StartDrag(Vector3 pointerPosition)
		{
			return this.dragAndDropController.SetupDragAndDrop(this.targetListView.selectedItems);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00021F20 File Offset: 0x00020120
		protected override DragVisualMode UpdateDrag(Vector3 pointerPosition)
		{
			ListViewDragger.DragPosition dragPosition = default(ListViewDragger.DragPosition);
			DragVisualMode visualMode = this.GetVisualMode(pointerPosition, ref dragPosition);
			bool flag = visualMode == DragVisualMode.Rejected;
			if (flag)
			{
				this.ClearDragAndDropUI();
			}
			else
			{
				this.ApplyDragAndDropUI(dragPosition);
			}
			return visualMode;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00021F60 File Offset: 0x00020160
		private DragVisualMode GetVisualMode(Vector3 pointerPosition, ref ListViewDragger.DragPosition dragPosition)
		{
			bool flag = this.dragAndDropController == null;
			DragVisualMode dragVisualMode;
			if (flag)
			{
				dragVisualMode = DragVisualMode.Rejected;
			}
			else
			{
				this.HandleDragAndScroll(pointerPosition);
				bool flag2 = !this.TryGetDragPosition(pointerPosition, ref dragPosition);
				if (flag2)
				{
					dragVisualMode = DragVisualMode.Rejected;
				}
				else
				{
					ListDragAndDropArgs listDragAndDropArgs = this.MakeDragAndDropArgs(dragPosition);
					dragVisualMode = this.dragAndDropController.HandleDragAndDrop(listDragAndDropArgs);
				}
			}
			return dragVisualMode;
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00021FC8 File Offset: 0x000201C8
		protected override void OnDrop(Vector3 pointerPosition)
		{
			ListViewDragger.DragPosition dragPosition = default(ListViewDragger.DragPosition);
			bool flag = !this.TryGetDragPosition(pointerPosition, ref dragPosition);
			if (!flag)
			{
				ListDragAndDropArgs listDragAndDropArgs = this.MakeDragAndDropArgs(dragPosition);
				bool flag2 = this.dragAndDropController.HandleDragAndDrop(listDragAndDropArgs) != DragVisualMode.Rejected;
				if (flag2)
				{
					this.dragAndDropController.OnDrop(listDragAndDropArgs);
				}
			}
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0002202C File Offset: 0x0002022C
		protected void HandleDragAndScroll(Vector2 pointerPosition)
		{
			bool flag = pointerPosition.y < this.targetScrollView.worldBound.yMin + 5f;
			bool flag2 = pointerPosition.y > this.targetScrollView.worldBound.yMax - 5f;
			bool flag3 = flag || flag2;
			if (flag3)
			{
				this.targetScrollView.scrollOffset += (flag ? Vector2.down : Vector2.up) * 20f;
			}
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x000220B8 File Offset: 0x000202B8
		protected void ApplyDragAndDropUI(ListViewDragger.DragPosition dragPosition)
		{
			bool flag = this.m_LastDragPosition.Equals(dragPosition);
			if (!flag)
			{
				this.ClearDragAndDropUI();
				this.m_LastDragPosition = dragPosition;
				switch (dragPosition.dragAndDropPosition)
				{
				case DragAndDropPosition.OverItem:
					dragPosition.recycledItem.element.AddToClassList(ListView.itemDragHoverUssClassName);
					break;
				case DragAndDropPosition.BetweenItems:
				{
					bool flag2 = dragPosition.insertAtIndex == 0;
					if (flag2)
					{
						this.PlaceHoverBarAt(0f);
					}
					else
					{
						this.PlaceHoverBarAtElement(this.targetListView.GetRecycledItemFromIndex(dragPosition.insertAtIndex - 1).element);
					}
					break;
				}
				case DragAndDropPosition.OutsideItems:
				{
					ListView.RecycledItem recycledItemFromIndex = this.targetListView.GetRecycledItemFromIndex(this.targetListView.itemsSource.Count - 1);
					bool flag3 = recycledItemFromIndex != null;
					if (flag3)
					{
						this.PlaceHoverBarAtElement(recycledItemFromIndex.element);
					}
					else
					{
						this.PlaceHoverBarAt(0f);
					}
					break;
				}
				default:
					throw new ArgumentOutOfRangeException("dragAndDropPosition", dragPosition.dragAndDropPosition, "Unsupported dragAndDropPosition value");
				}
			}
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x000221C0 File Offset: 0x000203C0
		protected bool TryGetDragPosition(Vector2 pointerPosition, ref ListViewDragger.DragPosition dragPosition)
		{
			ListView.RecycledItem recycledItem = this.GetRecycledItem(pointerPosition);
			bool flag = recycledItem != null;
			bool flag3;
			if (flag)
			{
				bool flag2 = recycledItem.element.worldBound.yMax - pointerPosition.y < 5f;
				if (flag2)
				{
					dragPosition.insertAtIndex = recycledItem.index + 1;
					dragPosition.dragAndDropPosition = DragAndDropPosition.BetweenItems;
					flag3 = true;
				}
				else
				{
					bool flag4 = pointerPosition.y - recycledItem.element.worldBound.yMin > 5f;
					if (flag4)
					{
						Vector2 scrollOffset = this.targetScrollView.scrollOffset;
						this.targetScrollView.ScrollTo(recycledItem.element);
						bool flag5 = scrollOffset != this.targetScrollView.scrollOffset;
						if (flag5)
						{
							flag3 = this.TryGetDragPosition(pointerPosition, ref dragPosition);
						}
						else
						{
							dragPosition.recycledItem = recycledItem;
							dragPosition.insertAtIndex = -1;
							dragPosition.dragAndDropPosition = DragAndDropPosition.OverItem;
							flag3 = true;
						}
					}
					else
					{
						dragPosition.insertAtIndex = recycledItem.index;
						dragPosition.dragAndDropPosition = DragAndDropPosition.BetweenItems;
						flag3 = true;
					}
				}
			}
			else
			{
				bool flag6 = !this.targetListView.worldBound.Contains(pointerPosition);
				if (flag6)
				{
					flag3 = false;
				}
				else
				{
					dragPosition.dragAndDropPosition = DragAndDropPosition.OutsideItems;
					bool flag7 = pointerPosition.y >= this.targetScrollView.contentContainer.worldBound.yMax;
					if (flag7)
					{
						dragPosition.insertAtIndex = this.targetListView.itemsSource.Count;
					}
					else
					{
						dragPosition.insertAtIndex = 0;
					}
					flag3 = true;
				}
			}
			return flag3;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00022348 File Offset: 0x00020548
		private ListDragAndDropArgs MakeDragAndDropArgs(ListViewDragger.DragPosition dragPosition)
		{
			object obj = null;
			ListView.RecycledItem recycledItem = dragPosition.recycledItem;
			bool flag = recycledItem != null;
			if (flag)
			{
				obj = this.targetListView.itemsSource[recycledItem.index];
			}
			return new ListDragAndDropArgs
			{
				target = obj,
				insertAtIndex = dragPosition.insertAtIndex,
				dragAndDropPosition = dragPosition.dragAndDropPosition
			};
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x000223B4 File Offset: 0x000205B4
		private void PlaceHoverBarAtElement(VisualElement element)
		{
			VisualElement contentViewport = this.targetScrollView.contentViewport;
			this.PlaceHoverBarAt(Mathf.Min(contentViewport.WorldToLocal(element.worldBound).yMax, contentViewport.localBound.yMax - 2f));
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00022402 File Offset: 0x00020602
		private void PlaceHoverBarAt(float top)
		{
			this.m_DragHoverBar.style.top = top;
			this.m_DragHoverBar.style.visibility = Visibility.Visible;
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00022434 File Offset: 0x00020634
		protected override void ClearDragAndDropUI()
		{
			this.m_LastDragPosition = default(ListViewDragger.DragPosition);
			foreach (ListView.RecycledItem recycledItem in this.targetListView.Pool)
			{
				recycledItem.element.RemoveFromClassList(ListView.itemDragHoverUssClassName);
			}
			this.m_DragHoverBar.style.visibility = Visibility.Hidden;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x000224C0 File Offset: 0x000206C0
		private ListView.RecycledItem GetRecycledItem(Vector3 pointerPosition)
		{
			foreach (ListView.RecycledItem recycledItem in this.targetListView.Pool)
			{
				bool flag = recycledItem.element.worldBound.Contains(pointerPosition);
				if (flag)
				{
					return recycledItem;
				}
			}
			return null;
		}

		// Token: 0x040003CC RID: 972
		private ListViewDragger.DragPosition m_LastDragPosition;

		// Token: 0x040003CD RID: 973
		private readonly VisualElement m_DragHoverBar;

		// Token: 0x040003CE RID: 974
		private readonly List<VisualElement> m_PickedElements = new List<VisualElement>();

		// Token: 0x040003CF RID: 975
		public const int k_EmptyIndex = -1;

		// Token: 0x040003D0 RID: 976
		private const int k_AutoScrollAreaSize = 5;

		// Token: 0x040003D1 RID: 977
		private const int k_BetweenElementsAreaSize = 5;

		// Token: 0x040003D2 RID: 978
		private const int k_PanSpeed = 20;

		// Token: 0x040003D3 RID: 979
		private const int k_DragHoverBarHeight = 2;

		// Token: 0x0200011D RID: 285
		internal struct DragPosition : IEquatable<ListViewDragger.DragPosition>
		{
			// Token: 0x06000877 RID: 2167 RVA: 0x00022574 File Offset: 0x00020774
			public bool Equals(ListViewDragger.DragPosition other)
			{
				return this.insertAtIndex == other.insertAtIndex && object.Equals(this.recycledItem, other.recycledItem) && this.dragAndDropPosition == other.dragAndDropPosition;
			}

			// Token: 0x06000878 RID: 2168 RVA: 0x000225B8 File Offset: 0x000207B8
			public override bool Equals(object obj)
			{
				return obj is ListViewDragger.DragPosition && this.Equals((ListViewDragger.DragPosition)obj);
			}

			// Token: 0x06000879 RID: 2169 RVA: 0x000225E4 File Offset: 0x000207E4
			public override int GetHashCode()
			{
				int num = this.insertAtIndex;
				num = (num * 397) ^ ((this.recycledItem != null) ? this.recycledItem.GetHashCode() : 0);
				return (num * 397) ^ (int)this.dragAndDropPosition;
			}

			// Token: 0x040003D5 RID: 981
			public int insertAtIndex;

			// Token: 0x040003D6 RID: 982
			public ListView.RecycledItem recycledItem;

			// Token: 0x040003D7 RID: 983
			public DragAndDropPosition dragAndDropPosition;
		}
	}
}
