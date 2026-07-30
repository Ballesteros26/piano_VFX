using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Web.UI
{
	/// <summary>Provides a base class for all strongly typed collections that manage <see cref="T:System.Web.UI.IStateManager" /> objects.</summary>
	// Token: 0x0200022A RID: 554
	public abstract class StateManagedCollection : IList, ICollection, IEnumerable, IStateManager
	{
		/// <summary>When overridden in a derived class, creates an instance of a class that implements <see cref="T:System.Web.UI.IStateManager" />. The type of object created is based on the specified member of the collection returned by the <see cref="M:System.Web.UI.StateManagedCollection.GetKnownTypes" /> method.</summary>
		/// <returns>An instance of a class derived from <see cref="T:System.Web.UI.IStateManager" />, according to the <paramref name="index" /> provided.</returns>
		/// <param name="index">The index, from the ordered list of types returned by <see cref="M:System.Web.UI.StateManagedCollection.GetKnownTypes" />, of the type of <see cref="T:System.Web.UI.IStateManager" /> to create.</param>
		/// <exception cref="T:System.InvalidOperationException">In all cases when not overridden in a derived class.</exception>
		// Token: 0x060016BE RID: 5822 RVA: 0x00003BEA File Offset: 0x00001DEA
		protected virtual object CreateKnownType(int index)
		{
			return null;
		}

		/// <summary>Forces the entire <see cref="T:System.Web.UI.StateManagedCollection" /> collection to be serialized into view state. </summary>
		// Token: 0x060016BF RID: 5823 RVA: 0x0003D1F0 File Offset: 0x0003B3F0
		public void SetDirty()
		{
			this.saveEverything = true;
			for (int i = 0; i < this.items.Count; i++)
			{
				this.SetDirtyObject(this.items[i]);
			}
		}

		/// <summary>When overridden in a derived class, instructs an object contained by the collection to record its entire state to view state, rather than recording only change information.</summary>
		/// <param name="o">The <see cref="T:System.Web.UI.IStateManager" /> that should serialize itself completely.</param>
		// Token: 0x060016C0 RID: 5824
		protected abstract void SetDirtyObject(object o);

		/// <summary>When overridden in a derived class, gets an array of <see cref="T:System.Web.UI.IStateManager" /> types that the <see cref="T:System.Web.UI.StateManagedCollection" /> collection can contain.</summary>
		/// <returns>An ordered array of <see cref="T:System.Type" /> objects that identify the types of <see cref="T:System.Web.UI.IStateManager" /> objects the collection can contain. The default implementation returns null.</returns>
		// Token: 0x060016C1 RID: 5825 RVA: 0x00003BEA File Offset: 0x00001DEA
		protected virtual Type[] GetKnownTypes()
		{
			return null;
		}

		/// <summary>When overridden in a derived class, performs additional work before the <see cref="M:System.Web.UI.StateManagedCollection.Clear" /> method removes all items from the collection.</summary>
		// Token: 0x060016C2 RID: 5826 RVA: 0x0000393A File Offset: 0x00001B3A
		protected virtual void OnClear()
		{
		}

		/// <summary>When overridden in a derived class, performs additional work after the <see cref="M:System.Web.UI.StateManagedCollection.Clear" /> method finishes removing all items from the collection.</summary>
		// Token: 0x060016C3 RID: 5827 RVA: 0x0000393A File Offset: 0x00001B3A
		protected virtual void OnClearComplete()
		{
		}

		/// <summary>When overridden in a derived class, performs additional work before the <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.Insert(System.Int32,System.Object)" /> or <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.Add(System.Object)" /> method adds an item to the collection.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted by the <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.Insert(System.Int32,System.Object)" /> method.</param>
		/// <param name="value">The object to insert into the <see cref="T:System.Web.UI.StateManagedCollection" />.</param>
		// Token: 0x060016C4 RID: 5828 RVA: 0x0000393A File Offset: 0x00001B3A
		protected virtual void OnInsert(int index, object value)
		{
		}

		/// <summary>When overridden in a derived class, performs additional work after the <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.Insert(System.Int32,System.Object)" /> or <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.Add(System.Object)" /> method adds an item to the collection.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> is inserted by the <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.Insert(System.Int32,System.Object)" /> method.</param>
		/// <param name="value">The object inserted into the <see cref="T:System.Web.UI.StateManagedCollection" />.</param>
		// Token: 0x060016C5 RID: 5829 RVA: 0x0000393A File Offset: 0x00001B3A
		protected virtual void OnInsertComplete(int index, object value)
		{
		}

		/// <summary>When overridden in a derived class, performs additional work before the <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.Remove(System.Object)" /> or <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.RemoveAt(System.Int32)" /> method removes the specified item from the collection.</summary>
		/// <param name="index">The zero-based index of the item to remove, which is used when <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.RemoveAt(System.Int32)" /> is called.</param>
		/// <param name="value">The object to remove from the <see cref="T:System.Web.UI.StateManagedCollection" />, which is used when <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.Remove(System.Object)" /> is called.</param>
		// Token: 0x060016C6 RID: 5830 RVA: 0x0000393A File Offset: 0x00001B3A
		protected virtual void OnRemove(int index, object value)
		{
		}

		/// <summary>When overridden in a derived class, performs additional work after the <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.Remove(System.Object)" /> or <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.RemoveAt(System.Int32)" /> method removes the specified item from the collection.</summary>
		/// <param name="index">The zero-based index of the item to remove, which is used when <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.RemoveAt(System.Int32)" /> is called.</param>
		/// <param name="value">The object removed from the <see cref="T:System.Web.UI.StateManagedCollection" />, which is used when <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.Remove(System.Object)" /> is called.</param>
		// Token: 0x060016C7 RID: 5831 RVA: 0x0000393A File Offset: 0x00001B3A
		protected virtual void OnRemoveComplete(int index, object value)
		{
		}

		/// <summary>When overridden in a derived class, validates an element of the <see cref="T:System.Web.UI.StateManagedCollection" /> collection.</summary>
		/// <param name="value">The <see cref="T:System.Web.UI.IStateManager" /> to validate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x060016C8 RID: 5832 RVA: 0x0003D22C File Offset: 0x0003B42C
		protected virtual void OnValidate(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
		}

		/// <summary>Restores the previously saved view state of the <see cref="T:System.Web.UI.StateManagedCollection" /> collection and the <see cref="T:System.Web.UI.IStateManager" /> items it contains.</summary>
		/// <param name="savedState">An object that represents the collection and collection elements' state to restore.</param>
		// Token: 0x060016C9 RID: 5833 RVA: 0x0003D23C File Offset: 0x0003B43C
		void IStateManager.LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				foreach (object obj in this.items)
				{
					((IStateManager)obj).LoadViewState(null);
				}
				return;
			}
			Triplet triplet = savedState as Triplet;
			if (triplet == null)
			{
				throw new InvalidOperationException("Internal error.");
			}
			List<int> list = triplet.First as List<int>;
			List<object> list2 = triplet.Second as List<object>;
			List<object> list3 = triplet.Third as List<object>;
			this.saveEverything = list == null;
			if (this.saveEverything)
			{
				this.Clear();
				int i = 0;
				while (i < list2.Count)
				{
					object obj2 = list3[i];
					IStateManager stateManager;
					if (obj2 is Type)
					{
						stateManager = (IStateManager)Activator.CreateInstance((Type)obj2);
						goto IL_00E3;
					}
					if (obj2 is int)
					{
						stateManager = (IStateManager)this.CreateKnownType((int)obj2);
						goto IL_00E3;
					}
					IL_0102:
					i++;
					continue;
					IL_00E3:
					stateManager.TrackViewState();
					stateManager.LoadViewState(list2[i]);
					((IList)this).Add(stateManager);
					goto IL_0102;
				}
				return;
			}
			for (int j = 0; j < list.Count; j++)
			{
				int num = list[j];
				if (num < this.Count)
				{
					IStateManager stateManager = ((IList)this)[num] as IStateManager;
					stateManager.TrackViewState();
					stateManager.LoadViewState(list2[j]);
				}
				else
				{
					object obj2 = list3[j];
					IStateManager stateManager;
					if (obj2 is Type)
					{
						stateManager = (IStateManager)Activator.CreateInstance((Type)obj2);
					}
					else
					{
						if (!(obj2 is int))
						{
							goto IL_01BA;
						}
						stateManager = (IStateManager)this.CreateKnownType((int)obj2);
					}
					stateManager.TrackViewState();
					stateManager.LoadViewState(list2[j]);
					((IList)this).Add(stateManager);
				}
				IL_01BA:;
			}
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x0003D428 File Offset: 0x0003B628
		private void AddListItem<T>(ref List<T> list, T item)
		{
			if (list == null)
			{
				list = new List<T>();
			}
			list.Add(item);
		}

		/// <summary>Saves the changes to the <see cref="T:System.Web.UI.StateManagedCollection" /> collection and each <see cref="T:System.Web.UI.IStateManager" /> object it contains since the time the page was posted back to the server.</summary>
		/// <returns>The object that contains the changes to the view state of the <see cref="T:System.Web.UI.StateManagedCollection" /> and the items it contains. If no view state is associated with the collection and its elements, this method returns null.</returns>
		// Token: 0x060016CB RID: 5835 RVA: 0x0003D440 File Offset: 0x0003B640
		object IStateManager.SaveViewState()
		{
			Type[] knownTypes = this.GetKnownTypes();
			bool flag = false;
			bool flag2 = knownTypes != null && knownTypes.Length != 0;
			int count = this.items.Count;
			List<int> list = null;
			List<object> list2 = null;
			List<object> list3 = null;
			for (int i = 0; i < count; i++)
			{
				IStateManager stateManager = this.items[i] as IStateManager;
				if (stateManager != null)
				{
					stateManager.TrackViewState();
					object obj = stateManager.SaveViewState();
					if (this.saveEverything || obj != null)
					{
						flag = true;
						Type type = stateManager.GetType();
						int num = (flag2 ? Array.IndexOf<Type>(knownTypes, type) : (-1));
						if (!this.saveEverything)
						{
							this.AddListItem<int>(ref list, i);
						}
						this.AddListItem<object>(ref list2, obj);
						if (num == -1)
						{
							this.AddListItem<object>(ref list3, type);
						}
						else
						{
							this.AddListItem<object>(ref list3, num);
						}
					}
				}
			}
			if (!flag)
			{
				return null;
			}
			return new Triplet(list, list2, list3);
		}

		/// <summary>Causes the <see cref="T:System.Web.UI.StateManagedCollection" /> collection and each of the <see cref="T:System.Web.UI.IStateManager" /> objects it contains to track changes to their view state so they can be persisted across requests for the same page.</summary>
		// Token: 0x060016CC RID: 5836 RVA: 0x0003D528 File Offset: 0x0003B728
		void IStateManager.TrackViewState()
		{
			this.isTrackingViewState = true;
			if (this.items != null && this.items.Count > 0)
			{
				foreach (object obj in this.items)
				{
					IStateManager stateManager = obj as IStateManager;
					if (stateManager != null)
					{
						stateManager.TrackViewState();
					}
				}
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.StateManagedCollection" /> collection is saving changes to its view state.</summary>
		/// <returns>true if the collection is marked to save its own state and the state of all the <see cref="T:System.Web.UI.IStateManager" /> items it contains; otherwise, false.</returns>
		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x0003D5A0 File Offset: 0x0003B7A0
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.isTrackingViewState;
			}
		}

		/// <summary>Removes all items from the <see cref="T:System.Web.UI.StateManagedCollection" /> collection.</summary>
		// Token: 0x060016CE RID: 5838 RVA: 0x0003D5A8 File Offset: 0x0003B7A8
		public void Clear()
		{
			this.OnClear();
			this.items.Clear();
			this.OnClearComplete();
			if (this.isTrackingViewState)
			{
				this.SetDirty();
			}
		}

		/// <summary>Returns an iterator that iterates through the <see cref="T:System.Web.UI.StateManagedCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the <see cref="T:System.Web.UI.StateManagedCollection" />.</returns>
		// Token: 0x060016CF RID: 5839 RVA: 0x0003D5CF File Offset: 0x0003B7CF
		public IEnumerator GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		/// <summary>Copies the elements of the <see cref="T:System.Web.UI.StateManagedCollection" /> collection to an array, starting at a particular array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Web.UI.StateManagedCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.- or -<paramref name="index" /> is greater than or equal to the length of <paramref name="array" />.- or -The number of elements in the source <see cref="T:System.Web.UI.StateManagedCollection" /> is greater than the available space from the <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x060016D0 RID: 5840 RVA: 0x0003D5DC File Offset: 0x0003B7DC
		public void CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		/// <summary>Returns an iterator that iterates through the <see cref="T:System.Web.UI.StateManagedCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the <see cref="T:System.Web.UI.StateManagedCollection" />.</returns>
		// Token: 0x060016D1 RID: 5841 RVA: 0x0003D5EB File Offset: 0x0003B7EB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Adds an item to the <see cref="T:System.Web.UI.StateManagedCollection" /> collection.</summary>
		/// <returns>The position at which the new element was inserted.</returns>
		/// <param name="value">The object to add to the <see cref="T:System.Web.UI.StateManagedCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The specified <paramref name="value" /> is null.</exception>
		// Token: 0x060016D2 RID: 5842 RVA: 0x0003D5F4 File Offset: 0x0003B7F4
		int IList.Add(object value)
		{
			this.OnValidate(value);
			if (this.isTrackingViewState)
			{
				((IStateManager)value).TrackViewState();
				this.SetDirtyObject(value);
			}
			this.OnInsert(-1, value);
			this.items.Add(value);
			this.OnInsertComplete(-1, value);
			return this.Count - 1;
		}

		/// <summary>Inserts an item into the <see cref="T:System.Web.UI.StateManagedCollection" /> collection at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The object to insert into the <see cref="T:System.Web.UI.StateManagedCollection" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="index" /> is out of range of the collection.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.UI.StateManagedCollection" /> is read-only.</exception>
		/// <exception cref="T:System.ArgumentNullException">The specified <paramref name="value" /> is null.</exception>
		// Token: 0x060016D3 RID: 5843 RVA: 0x0003D647 File Offset: 0x0003B847
		void IList.Insert(int index, object value)
		{
			this.OnValidate(value);
			if (this.isTrackingViewState)
			{
				((IStateManager)value).TrackViewState();
				this.SetDirty();
			}
			this.OnInsert(index, value);
			this.items.Insert(index, value);
			this.OnInsertComplete(index, value);
		}

		/// <summary>Removes the first occurrence of the specified object from the <see cref="T:System.Web.UI.StateManagedCollection" /> collection.</summary>
		/// <param name="value">The object to remove from the <see cref="T:System.Web.UI.StateManagedCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.UI.StateManagedCollection" /> is read-only.</exception>
		// Token: 0x060016D4 RID: 5844 RVA: 0x0003D688 File Offset: 0x0003B888
		void IList.Remove(object value)
		{
			if (value == null)
			{
				return;
			}
			this.OnValidate(value);
			int num = ((IList)this).IndexOf(value);
			if (num >= 0)
			{
				((IList)this).RemoveAt(num);
			}
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.IStateManager" /> element at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.UI.StateManagedCollection" /> is read-only.</exception>
		// Token: 0x060016D5 RID: 5845 RVA: 0x0003D6B8 File Offset: 0x0003B8B8
		void IList.RemoveAt(int index)
		{
			object obj = this.items[index];
			this.OnRemove(index, obj);
			this.items.RemoveAt(index);
			this.OnRemoveComplete(index, obj);
			if (this.isTrackingViewState)
			{
				this.SetDirty();
			}
		}

		/// <summary>Removes all items from the <see cref="T:System.Web.UI.StateManagedCollection" /> collection.</summary>
		// Token: 0x060016D6 RID: 5846 RVA: 0x0003D6FC File Offset: 0x0003B8FC
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Determines whether the <see cref="T:System.Web.UI.StateManagedCollection" /> collection contains a specific value.</summary>
		/// <returns>true if the object is found in the <see cref="T:System.Web.UI.StateManagedCollection" />; otherwise, false. If null is passed for the value parameter, false is returned.</returns>
		/// <param name="value">The object to locate in the <see cref="T:System.Web.UI.StateManagedCollection" />.</param>
		// Token: 0x060016D7 RID: 5847 RVA: 0x0003D704 File Offset: 0x0003B904
		bool IList.Contains(object value)
		{
			if (value == null)
			{
				return false;
			}
			this.OnValidate(value);
			return this.items.Contains(value);
		}

		/// <summary>Determines the index of a specified item in the <see cref="T:System.Web.UI.StateManagedCollection" /> collection.</summary>
		/// <returns>The index of <paramref name="value" />, if it is found in the list; otherwise, -1.</returns>
		/// <param name="value">The object to locate in the <see cref="T:System.Web.UI.StateManagedCollection" />.</param>
		// Token: 0x060016D8 RID: 5848 RVA: 0x0003D71E File Offset: 0x0003B91E
		int IList.IndexOf(object value)
		{
			if (value == null)
			{
				return -1;
			}
			this.OnValidate(value);
			return this.items.IndexOf(value);
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Web.UI.StateManagedCollection" /> collection.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Web.UI.StateManagedCollection" />.</returns>
		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x060016D9 RID: 5849 RVA: 0x0003D738 File Offset: 0x0003B938
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Web.UI.StateManagedCollection" /> collection.</summary>
		/// <returns>The number of elements in the <see cref="T:System.Web.UI.StateManagedCollection" />.</returns>
		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x060016DA RID: 5850 RVA: 0x0003D738 File Offset: 0x0003B938
		int ICollection.Count
		{
			get
			{
				return this.items.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.StateManagedCollection" /> collection is synchronized (thread safe). This method returns false in all cases.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x060016DB RID: 5851 RVA: 0x00008A69 File Offset: 0x00006C69
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Web.UI.StateManagedCollection" /> collection. This method returns null in all cases.</summary>
		/// <returns>null in all cases.</returns>
		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x00002058 File Offset: 0x00000258
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.StateManagedCollection" /> collection has a fixed size. This method returns false in all cases.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x060016DD RID: 5853 RVA: 0x00008A69 File Offset: 0x00006C69
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.StateManagedCollection" /> collection is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.StateManagedCollection" /> is read-only; otherwise, false.</returns>
		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x060016DE RID: 5854 RVA: 0x00008A69 File Offset: 0x00006C69
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.IStateManager" /> element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="index" /> is out of range of the collection.</exception>
		// Token: 0x1700073E RID: 1854
		object IList.this[int index]
		{
			get
			{
				return this.items[index];
			}
			set
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.OnValidate(value);
				if (this.isTrackingViewState)
				{
					((IStateManager)value).TrackViewState();
					this.SetDirty();
				}
				this.items[index] = value;
			}
		}

		// Token: 0x04001581 RID: 5505
		private ArrayList items = new ArrayList();

		// Token: 0x04001582 RID: 5506
		private bool saveEverything;

		// Token: 0x04001583 RID: 5507
		private bool isTrackingViewState;
	}
}
