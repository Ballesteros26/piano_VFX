using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System.Collections.ObjectModel
{
	/// <summary>Represents a dynamic data collection that provides notifications when items get added, removed, or when the whole list is refreshed.</summary>
	/// <typeparam name="T">The type of elements in the collection.</typeparam>
	// Token: 0x020006E6 RID: 1766
	[TypeForwardedFrom("WindowsBase, Version=3.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	[Serializable]
	public class ObservableCollection<T> : Collection<T>, INotifyCollectionChanged, INotifyPropertyChanged
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> class.</summary>
		// Token: 0x0600370C RID: 14092 RVA: 0x000CB0E8 File Offset: 0x000C92E8
		public ObservableCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> class that contains elements copied from the specified list.</summary>
		/// <param name="list">The list from which the elements are copied.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="list" /> parameter cannot be null.</exception>
		// Token: 0x0600370D RID: 14093 RVA: 0x000CB0FB File Offset: 0x000C92FB
		public ObservableCollection(List<T> list)
			: base((list != null) ? new List<T>(list.Count) : list)
		{
			this.CopyFrom(list);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> class that contains elements copied from the specified collection.</summary>
		/// <param name="collection">The collection from which the elements are copied.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="collection" /> parameter cannot be null.</exception>
		// Token: 0x0600370E RID: 14094 RVA: 0x000CB126 File Offset: 0x000C9326
		public ObservableCollection(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.CopyFrom(collection);
		}

		// Token: 0x0600370F RID: 14095 RVA: 0x000CB150 File Offset: 0x000C9350
		private void CopyFrom(IEnumerable<T> collection)
		{
			IList<T> items = base.Items;
			if (collection != null && items != null)
			{
				foreach (T t in collection)
				{
					items.Add(t);
				}
			}
		}

		/// <summary>Moves the item at the specified index to a new location in the collection.</summary>
		/// <param name="oldIndex">The zero-based index specifying the location of the item to be moved.</param>
		/// <param name="newIndex">The zero-based index specifying the new location of the item.</param>
		// Token: 0x06003710 RID: 14096 RVA: 0x000CB1A4 File Offset: 0x000C93A4
		public void Move(int oldIndex, int newIndex)
		{
			this.MoveItem(oldIndex, newIndex);
		}

		/// <summary>Occurs when a property value changes.</summary>
		// Token: 0x14000063 RID: 99
		// (add) Token: 0x06003711 RID: 14097 RVA: 0x000CB1AE File Offset: 0x000C93AE
		// (remove) Token: 0x06003712 RID: 14098 RVA: 0x000CB1B7 File Offset: 0x000C93B7
		event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
		{
			add
			{
				this.PropertyChanged += value;
			}
			remove
			{
				this.PropertyChanged -= value;
			}
		}

		/// <summary>Occurs when an item is added, removed, changed, moved, or the entire list is refreshed.</summary>
		// Token: 0x14000064 RID: 100
		// (add) Token: 0x06003713 RID: 14099 RVA: 0x000CB1C0 File Offset: 0x000C93C0
		// (remove) Token: 0x06003714 RID: 14100 RVA: 0x000CB1F8 File Offset: 0x000C93F8
		[field: NonSerialized]
		public virtual event NotifyCollectionChangedEventHandler CollectionChanged;

		/// <summary>Removes all items from the collection.</summary>
		// Token: 0x06003715 RID: 14101 RVA: 0x000CB22D File Offset: 0x000C942D
		protected override void ClearItems()
		{
			this.CheckReentrancy();
			base.ClearItems();
			this.OnPropertyChanged("Count");
			this.OnPropertyChanged("Item[]");
			this.OnCollectionReset();
		}

		/// <summary>Removes the item at the specified index of the collection.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		// Token: 0x06003716 RID: 14102 RVA: 0x000CB258 File Offset: 0x000C9458
		protected override void RemoveItem(int index)
		{
			this.CheckReentrancy();
			T t = base[index];
			base.RemoveItem(index);
			this.OnPropertyChanged("Count");
			this.OnPropertyChanged("Item[]");
			this.OnCollectionChanged(NotifyCollectionChangedAction.Remove, t, index);
		}

		/// <summary>Inserts an item into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
		/// <param name="item">The object to insert.</param>
		// Token: 0x06003717 RID: 14103 RVA: 0x000CB29E File Offset: 0x000C949E
		protected override void InsertItem(int index, T item)
		{
			this.CheckReentrancy();
			base.InsertItem(index, item);
			this.OnPropertyChanged("Count");
			this.OnPropertyChanged("Item[]");
			this.OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
		}

		/// <summary>Replaces the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to replace.</param>
		/// <param name="item">The new value for the element at the specified index.</param>
		// Token: 0x06003718 RID: 14104 RVA: 0x000CB2D4 File Offset: 0x000C94D4
		protected override void SetItem(int index, T item)
		{
			this.CheckReentrancy();
			T t = base[index];
			base.SetItem(index, item);
			this.OnPropertyChanged("Item[]");
			this.OnCollectionChanged(NotifyCollectionChangedAction.Replace, t, item, index);
		}

		/// <summary>Moves the item at the specified index to a new location in the collection.</summary>
		/// <param name="oldIndex">The zero-based index specifying the location of the item to be moved.</param>
		/// <param name="newIndex">The zero-based index specifying the new location of the item.</param>
		// Token: 0x06003719 RID: 14105 RVA: 0x000CB318 File Offset: 0x000C9518
		protected virtual void MoveItem(int oldIndex, int newIndex)
		{
			this.CheckReentrancy();
			T t = base[oldIndex];
			base.RemoveItem(oldIndex);
			base.InsertItem(newIndex, t);
			this.OnPropertyChanged("Item[]");
			this.OnCollectionChanged(NotifyCollectionChangedAction.Move, t, newIndex, oldIndex);
		}

		/// <summary>Raises the <see cref="E:System.Collections.ObjectModel.ObservableCollection`1.PropertyChanged" /> event with the provided arguments.</summary>
		/// <param name="e">Arguments of the event being raised.</param>
		// Token: 0x0600371A RID: 14106 RVA: 0x000CB35C File Offset: 0x000C955C
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, e);
			}
		}

		/// <summary>Occurs when a property value changes.</summary>
		// Token: 0x14000065 RID: 101
		// (add) Token: 0x0600371B RID: 14107 RVA: 0x000CB374 File Offset: 0x000C9574
		// (remove) Token: 0x0600371C RID: 14108 RVA: 0x000CB3AC File Offset: 0x000C95AC
		[field: NonSerialized]
		protected virtual event PropertyChangedEventHandler PropertyChanged;

		/// <summary>Raises the <see cref="E:System.Collections.ObjectModel.ObservableCollection`1.CollectionChanged" /> event with the provided arguments.</summary>
		/// <param name="e">Arguments of the event being raised.</param>
		// Token: 0x0600371D RID: 14109 RVA: 0x000CB3E4 File Offset: 0x000C95E4
		protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			if (this.CollectionChanged != null)
			{
				using (this.BlockReentrancy())
				{
					this.CollectionChanged(this, e);
				}
			}
		}

		/// <summary>Disallows reentrant attempts to change this collection.</summary>
		/// <returns>An <see cref="T:System.IDisposable" /> object that can be used to dispose of the object.</returns>
		// Token: 0x0600371E RID: 14110 RVA: 0x000CB42C File Offset: 0x000C962C
		protected IDisposable BlockReentrancy()
		{
			this._monitor.Enter();
			return this._monitor;
		}

		/// <summary>Checks for reentrant attempts to change this collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">If there was a call to <see cref="M:System.Collections.ObjectModel.ObservableCollection`1.BlockReentrancy" /> of which the <see cref="T:System.IDisposable" /> return value has not yet been disposed of. Typically, this means when there are additional attempts to change this collection during a <see cref="E:System.Collections.ObjectModel.ObservableCollection`1.CollectionChanged" /> event. However, it depends on when derived classes choose to call <see cref="M:System.Collections.ObjectModel.ObservableCollection`1.BlockReentrancy" />.</exception>
		// Token: 0x0600371F RID: 14111 RVA: 0x000CB43F File Offset: 0x000C963F
		protected void CheckReentrancy()
		{
			if (this._monitor.Busy && this.CollectionChanged != null && this.CollectionChanged.GetInvocationList().Length > 1)
			{
				throw new InvalidOperationException(global::SR.GetString("Cannot change ObservableCollection during a CollectionChanged event."));
			}
		}

		// Token: 0x06003720 RID: 14112 RVA: 0x000CB476 File Offset: 0x000C9676
		private void OnPropertyChanged(string propertyName)
		{
			this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}

		// Token: 0x06003721 RID: 14113 RVA: 0x000CB484 File Offset: 0x000C9684
		private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index)
		{
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
		}

		// Token: 0x06003722 RID: 14114 RVA: 0x000CB494 File Offset: 0x000C9694
		private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index, int oldIndex)
		{
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index, oldIndex));
		}

		// Token: 0x06003723 RID: 14115 RVA: 0x000CB4A6 File Offset: 0x000C96A6
		private void OnCollectionChanged(NotifyCollectionChangedAction action, object oldItem, object newItem, int index)
		{
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));
		}

		// Token: 0x06003724 RID: 14116 RVA: 0x000CB4B8 File Offset: 0x000C96B8
		private void OnCollectionReset()
		{
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		// Token: 0x04002BEB RID: 11243
		private const string CountString = "Count";

		// Token: 0x04002BEC RID: 11244
		private const string IndexerName = "Item[]";

		// Token: 0x04002BED RID: 11245
		private ObservableCollection<T>.SimpleMonitor _monitor = new ObservableCollection<T>.SimpleMonitor();

		// Token: 0x020006E7 RID: 1767
		[TypeForwardedFrom("WindowsBase, Version=3.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
		[Serializable]
		private class SimpleMonitor : IDisposable
		{
			// Token: 0x06003725 RID: 14117 RVA: 0x000CB4C6 File Offset: 0x000C96C6
			public void Enter()
			{
				this._busyCount++;
			}

			// Token: 0x06003726 RID: 14118 RVA: 0x000CB4D6 File Offset: 0x000C96D6
			public void Dispose()
			{
				this._busyCount--;
			}

			// Token: 0x17000D59 RID: 3417
			// (get) Token: 0x06003727 RID: 14119 RVA: 0x000CB4E6 File Offset: 0x000C96E6
			public bool Busy
			{
				get
				{
					return this._busyCount > 0;
				}
			}

			// Token: 0x04002BEE RID: 11246
			private int _busyCount;
		}
	}
}
