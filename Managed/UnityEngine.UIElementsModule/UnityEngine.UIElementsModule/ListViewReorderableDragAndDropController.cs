using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.UIElements
{
	// Token: 0x0200011F RID: 287
	internal class ListViewReorderableDragAndDropController : IListViewDragAndDropController, IDragAndDropController<object, IListDragAndDropArgs>, IReorderable<object>
	{
		// Token: 0x0600087B RID: 2171 RVA: 0x0002269C File Offset: 0x0002089C
		public ListViewReorderableDragAndDropController(ListView listView)
		{
			this.m_ListView = listView;
			this.enableReordering = true;
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x000226B5 File Offset: 0x000208B5
		// (set) Token: 0x0600087D RID: 2173 RVA: 0x000226BD File Offset: 0x000208BD
		public bool enableReordering { get; set; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x000226C6 File Offset: 0x000208C6
		// (set) Token: 0x0600087F RID: 2175 RVA: 0x000226CE File Offset: 0x000208CE
		public Action<ItemMoveArgs<object>> onItemMoved { get; set; }

		// Token: 0x06000880 RID: 2176 RVA: 0x000226D8 File Offset: 0x000208D8
		public virtual bool CanStartDrag(IEnumerable<object> items)
		{
			return this.enableReordering;
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x000226F0 File Offset: 0x000208F0
		public virtual StartDragArgs SetupDragAndDrop(IEnumerable<object> items)
		{
			string text = string.Empty;
			foreach (object obj in items)
			{
				bool flag = string.IsNullOrEmpty(text);
				if (!flag)
				{
					text = "<Multiple>";
					break;
				}
				int selectedIndex = this.m_ListView.selectedIndex;
				ListView.RecycledItem recycledItemFromIndex = this.m_ListView.GetRecycledItemFromIndex(selectedIndex);
				Label label = ((recycledItemFromIndex != null) ? recycledItemFromIndex.element.Q(null, null) : null);
				text = ((label != null) ? label.text : string.Format("Item {0}", selectedIndex));
			}
			return new StartDragArgs(text, this.m_ListView);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x000227B4 File Offset: 0x000209B4
		public virtual DragVisualMode HandleDragAndDrop(IListDragAndDropArgs args)
		{
			bool flag = args.dragAndDropPosition == DragAndDropPosition.OverItem || !this.enableReordering;
			DragVisualMode dragVisualMode;
			if (flag)
			{
				dragVisualMode = DragVisualMode.Rejected;
			}
			else
			{
				dragVisualMode = ((args.dragAndDropData.userData == this.m_ListView) ? DragVisualMode.Move : DragVisualMode.Rejected);
			}
			return dragVisualMode;
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x000227FC File Offset: 0x000209FC
		public virtual void OnDrop(IListDragAndDropArgs args)
		{
			int num = 0;
			int[] array = Enumerable.ToArray<int>(Enumerable.OrderBy<int, int>(this.m_ListView.selectedIndices, (int i) => i));
			for (int j = array.Length - 1; j >= 0; j--)
			{
				int num2 = array[j];
				bool flag = num2 < args.insertAtIndex;
				if (flag)
				{
					num--;
				}
				this.m_ListView.itemsSource.RemoveAt(num2);
			}
			DragAndDropPosition dragAndDropPosition = args.dragAndDropPosition;
			if (dragAndDropPosition - DragAndDropPosition.BetweenItems > 1)
			{
				throw new ArgumentException(string.Format("{0} is not supported by {1}.", args.dragAndDropPosition, "ListViewReorderableDragAndDropController"));
			}
			this.InsertRange(args.insertAtIndex + num);
			this.m_ListView.Refresh();
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x000228D4 File Offset: 0x00020AD4
		private void InsertRange(int index)
		{
			List<int> list = new List<int>();
			object[] array = Enumerable.ToArray<object>(this.m_ListView.selectedItems);
			int[] array2 = Enumerable.ToArray<int>(this.m_ListView.selectedIndices);
			for (int i = 0; i < array.Length; i++)
			{
				object obj = array[i];
				this.m_ListView.itemsSource.Insert(index, obj);
				Action<ItemMoveArgs<object>> onItemMoved = this.onItemMoved;
				if (onItemMoved != null)
				{
					onItemMoved.Invoke(new ItemMoveArgs<object>
					{
						item = obj,
						newIndex = index,
						previousIndex = array2[i]
					});
				}
				list.Add(index);
				index++;
			}
			this.m_ListView.SetSelectionWithoutNotify(list);
		}

		// Token: 0x040003D8 RID: 984
		protected readonly ListView m_ListView;
	}
}
