using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x0200009B RID: 155
	internal sealed class DataViewListener
	{
		// Token: 0x06000999 RID: 2457 RVA: 0x0002C293 File Offset: 0x0002A493
		internal DataViewListener(DataView dv)
		{
			this._objectID = dv.ObjectID;
			this._dvWeak = new WeakReference(dv);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0002C2B4 File Offset: 0x0002A4B4
		private void ChildRelationCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataView dataView = (DataView)this._dvWeak.Target;
			if (dataView != null)
			{
				dataView.ChildRelationCollectionChanged(sender, e);
				return;
			}
			this.CleanUp(true);
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0002C2E8 File Offset: 0x0002A4E8
		private void ParentRelationCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataView dataView = (DataView)this._dvWeak.Target;
			if (dataView != null)
			{
				dataView.ParentRelationCollectionChanged(sender, e);
				return;
			}
			this.CleanUp(true);
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0002C31C File Offset: 0x0002A51C
		private void ColumnCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataView dataView = (DataView)this._dvWeak.Target;
			if (dataView != null)
			{
				dataView.ColumnCollectionChangedInternal(sender, e);
				return;
			}
			this.CleanUp(true);
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0002C350 File Offset: 0x0002A550
		internal void MaintainDataView(ListChangedType changedType, DataRow row, bool trackAddRemove)
		{
			DataView dataView = (DataView)this._dvWeak.Target;
			if (dataView != null)
			{
				dataView.MaintainDataView(changedType, row, trackAddRemove);
				return;
			}
			this.CleanUp(true);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0002C384 File Offset: 0x0002A584
		internal void IndexListChanged(ListChangedEventArgs e)
		{
			DataView dataView = (DataView)this._dvWeak.Target;
			if (dataView != null)
			{
				dataView.IndexListChangedInternal(e);
				return;
			}
			this.CleanUp(true);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0002C3B4 File Offset: 0x0002A5B4
		internal void RegisterMetaDataEvents(DataTable table)
		{
			this._table = table;
			if (table != null)
			{
				this.RegisterListener(table);
				CollectionChangeEventHandler collectionChangeEventHandler = new CollectionChangeEventHandler(this.ColumnCollectionChanged);
				table.Columns.ColumnPropertyChanged += collectionChangeEventHandler;
				table.Columns.CollectionChanged += collectionChangeEventHandler;
				CollectionChangeEventHandler collectionChangeEventHandler2 = new CollectionChangeEventHandler(this.ChildRelationCollectionChanged);
				((DataRelationCollection.DataTableRelationCollection)table.ChildRelations).RelationPropertyChanged += collectionChangeEventHandler2;
				table.ChildRelations.CollectionChanged += collectionChangeEventHandler2;
				CollectionChangeEventHandler collectionChangeEventHandler3 = new CollectionChangeEventHandler(this.ParentRelationCollectionChanged);
				((DataRelationCollection.DataTableRelationCollection)table.ParentRelations).RelationPropertyChanged += collectionChangeEventHandler3;
				table.ParentRelations.CollectionChanged += collectionChangeEventHandler3;
			}
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0002C44E File Offset: 0x0002A64E
		internal void UnregisterMetaDataEvents()
		{
			this.UnregisterMetaDataEvents(true);
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0002C458 File Offset: 0x0002A658
		private void UnregisterMetaDataEvents(bool updateListeners)
		{
			DataTable table = this._table;
			this._table = null;
			if (table != null)
			{
				CollectionChangeEventHandler collectionChangeEventHandler = new CollectionChangeEventHandler(this.ColumnCollectionChanged);
				table.Columns.ColumnPropertyChanged -= collectionChangeEventHandler;
				table.Columns.CollectionChanged -= collectionChangeEventHandler;
				CollectionChangeEventHandler collectionChangeEventHandler2 = new CollectionChangeEventHandler(this.ChildRelationCollectionChanged);
				((DataRelationCollection.DataTableRelationCollection)table.ChildRelations).RelationPropertyChanged -= collectionChangeEventHandler2;
				table.ChildRelations.CollectionChanged -= collectionChangeEventHandler2;
				CollectionChangeEventHandler collectionChangeEventHandler3 = new CollectionChangeEventHandler(this.ParentRelationCollectionChanged);
				((DataRelationCollection.DataTableRelationCollection)table.ParentRelations).RelationPropertyChanged -= collectionChangeEventHandler3;
				table.ParentRelations.CollectionChanged -= collectionChangeEventHandler3;
				if (updateListeners)
				{
					List<DataViewListener> listeners = table.GetListeners();
					List<DataViewListener> list = listeners;
					lock (list)
					{
						listeners.Remove(this);
					}
				}
			}
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0002C534 File Offset: 0x0002A734
		internal void RegisterListChangedEvent(Index index)
		{
			this._index = index;
			if (index != null)
			{
				lock (index)
				{
					index.AddRef();
					index.ListChangedAdd(this);
				}
			}
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0002C580 File Offset: 0x0002A780
		internal void UnregisterListChangedEvent()
		{
			Index index = this._index;
			this._index = null;
			if (index != null)
			{
				Index index2 = index;
				lock (index2)
				{
					index.ListChangedRemove(this);
					if (index.RemoveRef() <= 1)
					{
						index.RemoveRef();
					}
				}
			}
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0002C5E0 File Offset: 0x0002A7E0
		private void CleanUp(bool updateListeners)
		{
			this.UnregisterMetaDataEvents(updateListeners);
			this.UnregisterListChangedEvent();
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0002C5F0 File Offset: 0x0002A7F0
		private void RegisterListener(DataTable table)
		{
			List<DataViewListener> listeners = table.GetListeners();
			List<DataViewListener> list = listeners;
			lock (list)
			{
				int num = listeners.Count - 1;
				while (0 <= num)
				{
					DataViewListener dataViewListener = listeners[num];
					if (!dataViewListener._dvWeak.IsAlive)
					{
						listeners.RemoveAt(num);
						dataViewListener.CleanUp(false);
					}
					num--;
				}
				listeners.Add(this);
			}
		}

		// Token: 0x04000666 RID: 1638
		private readonly WeakReference _dvWeak;

		// Token: 0x04000667 RID: 1639
		private DataTable _table;

		// Token: 0x04000668 RID: 1640
		private Index _index;

		// Token: 0x04000669 RID: 1641
		internal readonly int _objectID;
	}
}
