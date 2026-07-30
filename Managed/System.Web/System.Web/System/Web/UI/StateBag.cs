using System;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Manages the view state of ASP.NET server controls, including pages. This class cannot be inherited.</summary>
	// Token: 0x02000228 RID: 552
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class StateBag : IDictionary, ICollection, IEnumerable, IStateManager
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.StateBag" /> class that allows stored state values to be case-insensitive.</summary>
		/// <param name="ignoreCase">true to ignore case; otherwise, false. </param>
		// Token: 0x06001692 RID: 5778 RVA: 0x0003CDC4 File Offset: 0x0003AFC4
		public StateBag(bool ignoreCase)
		{
			this.ht = new HybridDictionary(ignoreCase);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.StateBag" /> class. This is the default constructor for this class.</summary>
		// Token: 0x06001693 RID: 5779 RVA: 0x0003CDD8 File Offset: 0x0003AFD8
		public StateBag()
			: this(false)
		{
		}

		/// <summary>Restores the previously saved view state of the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		/// <param name="state">An object that represents the <see cref="T:System.Web.UI.StateBag" /> state to restore. </param>
		// Token: 0x06001694 RID: 5780 RVA: 0x0003CDE1 File Offset: 0x0003AFE1
		void IStateManager.LoadViewState(object savedState)
		{
			this.LoadViewState(savedState);
		}

		/// <summary>Saves the changes to the <see cref="T:System.Web.UI.StateBag" /> object since the time the page was posted back to the server.</summary>
		/// <returns>The object that contains the changes to the view state of the <see cref="T:System.Web.UI.StateBag" />. If there are no changes, or there are no <see cref="T:System.Web.UI.StateItem" /> elements in the <see cref="T:System.Web.UI.StateBag" />, this method returns null.</returns>
		// Token: 0x06001695 RID: 5781 RVA: 0x0003CDEA File Offset: 0x0003AFEA
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		/// <summary>Causes the <see cref="T:System.Web.UI.StateBag" /> object to track changes to its state so that it can be persisted across requests.</summary>
		// Token: 0x06001696 RID: 5782 RVA: 0x0003CDF2 File Offset: 0x0003AFF2
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		/// <summary>Gets a value indicating whether state changes are being tracked.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.StateBag" /> is marked to save changes to its state; otherwise, false.</returns>
		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x0003CDFA File Offset: 0x0003AFFA
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.track;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x0003CDFA File Offset: 0x0003AFFA
		internal bool IsTrackingViewState
		{
			get
			{
				return this.track;
			}
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x0003CE04 File Offset: 0x0003B004
		internal void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				return;
			}
			foreach (object obj in ((Hashtable)savedState))
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this.Add((string)dictionaryEntry.Key, dictionaryEntry.Value);
			}
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x0003CE74 File Offset: 0x0003B074
		internal object SaveViewState()
		{
			Hashtable hashtable = null;
			foreach (object obj in this.ht)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				StateItem stateItem = (StateItem)dictionaryEntry.Value;
				if (stateItem.IsDirty)
				{
					if (hashtable == null)
					{
						hashtable = new Hashtable();
					}
					hashtable.Add(dictionaryEntry.Key, stateItem.Value);
				}
			}
			return hashtable;
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x0003CF00 File Offset: 0x0003B100
		internal void TrackViewState()
		{
			this.track = true;
		}

		/// <summary>Adds a new <see cref="T:System.Web.UI.StateItem" /> object to the <see cref="T:System.Web.UI.StateBag" /> object. If the item already exists in the <see cref="T:System.Web.UI.StateBag" /> object, this method updates the value of the item.</summary>
		/// <returns>Returns a <see cref="T:System.Web.UI.StateItem" /> that represents the object added to view state.</returns>
		/// <param name="key">The attribute name for the <see cref="T:System.Web.UI.StateItem" />. </param>
		/// <param name="value">The value of the item to add to the <see cref="T:System.Web.UI.StateBag" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is null.- or -The number of characters in <paramref name="key" /> is 0. </exception>
		// Token: 0x0600169C RID: 5788 RVA: 0x0003CF0C File Offset: 0x0003B10C
		public StateItem Add(string key, object value)
		{
			StateItem stateItem = this.ht[key] as StateItem;
			if (stateItem == null)
			{
				stateItem = (this.ht[key] = new StateItem(value));
			}
			stateItem.Value = value;
			stateItem.IsDirty |= this.track;
			return stateItem;
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x0003CF60 File Offset: 0x0003B160
		internal string GetString(string key, string def)
		{
			string text = (string)this[key];
			if (text != null)
			{
				return text;
			}
			return def;
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x0003CF80 File Offset: 0x0003B180
		internal bool GetBool(string key, bool def)
		{
			object obj = this[key];
			if (obj != null)
			{
				return (bool)obj;
			}
			return def;
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x0003CFA0 File Offset: 0x0003B1A0
		internal char GetChar(string key, char def)
		{
			object obj = this[key];
			if (obj != null)
			{
				return (char)obj;
			}
			return def;
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x0003CFC0 File Offset: 0x0003B1C0
		internal int GetInt(string key, int def)
		{
			object obj = this[key];
			if (obj != null)
			{
				return (int)obj;
			}
			return def;
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x0003CFE0 File Offset: 0x0003B1E0
		internal short GetShort(string key, short def)
		{
			object obj = this[key];
			if (obj != null)
			{
				return (short)obj;
			}
			return def;
		}

		/// <summary>Removes all items from the current <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		// Token: 0x060016A2 RID: 5794 RVA: 0x0003D000 File Offset: 0x0003B200
		public void Clear()
		{
			this.ht.Clear();
		}

		/// <summary>Returns an enumerator that iterates over all the key/value pairs of the <see cref="T:System.Web.UI.StateItem" /> objects stored in the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		/// <returns>The enumerator to iterate through the state bag.</returns>
		// Token: 0x060016A3 RID: 5795 RVA: 0x0003D00D File Offset: 0x0003B20D
		public IDictionaryEnumerator GetEnumerator()
		{
			return this.ht.GetEnumerator();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x060016A4 RID: 5796 RVA: 0x0003D01A File Offset: 0x0003B21A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Checks a <see cref="T:System.Web.UI.StateItem" /> object stored in the <see cref="T:System.Web.UI.StateBag" /> object to evaluate whether it has been modified since the call to <see cref="M:System.Web.UI.Control.TrackViewState" />.</summary>
		/// <returns>true if the item has been modified; otherwise, false.</returns>
		/// <param name="key">The key of the item to check. </param>
		// Token: 0x060016A5 RID: 5797 RVA: 0x0003D024 File Offset: 0x0003B224
		public bool IsItemDirty(string key)
		{
			StateItem stateItem = this.ht[key] as StateItem;
			return stateItem != null && stateItem.IsDirty;
		}

		/// <summary>Removes the specified key/value pair from the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		/// <param name="key">The item to remove. </param>
		// Token: 0x060016A6 RID: 5798 RVA: 0x0003D04E File Offset: 0x0003B24E
		public void Remove(string key)
		{
			this.ht.Remove(key);
		}

		/// <summary>Sets the <see cref="P:System.Web.SessionState.ISessionStateItemCollection.Dirty" /> property for the specified <see cref="T:System.Web.UI.StateItem" /> object in the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		/// <param name="key">The key that identifies which <see cref="T:System.Web.UI.StateItem" /> in the <see cref="T:System.Web.UI.StateBag" /> to set. </param>
		/// <param name="dirty">true to mark the state of the item as modified; otherwise, false.</param>
		// Token: 0x060016A7 RID: 5799 RVA: 0x0003D05C File Offset: 0x0003B25C
		public void SetItemDirty(string key, bool dirty)
		{
			StateItem stateItem = (StateItem)this.ht[key];
			if (stateItem != null)
			{
				stateItem.IsDirty = dirty;
			}
		}

		/// <summary>Gets the number of <see cref="T:System.Web.UI.StateItem" /> objects in the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		/// <returns>The number of items in the <see cref="T:System.Web.UI.StateBag" />.</returns>
		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x060016A8 RID: 5800 RVA: 0x0003D085 File Offset: 0x0003B285
		public int Count
		{
			get
			{
				return this.ht.Count;
			}
		}

		/// <summary>Gets or sets the value of an item stored in the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		/// <returns>The specified item in the <see cref="T:System.Web.UI.StateBag" /> object.</returns>
		/// <param name="key">The key for the item. </param>
		// Token: 0x1700072D RID: 1837
		public object this[string key]
		{
			get
			{
				StateItem stateItem = this.ht[key] as StateItem;
				if (stateItem != null)
				{
					return stateItem.Value;
				}
				return null;
			}
			set
			{
				if (value == null && !this.IsTrackingViewState)
				{
					this.Remove(key);
					return;
				}
				this.Add(key, value);
			}
		}

		/// <summary>Gets a collection of keys representing the items in the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		/// <returns>The collection of keys.</returns>
		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x060016AB RID: 5803 RVA: 0x0003D0DC File Offset: 0x0003B2DC
		public ICollection Keys
		{
			get
			{
				return this.ht.Keys;
			}
		}

		/// <summary>Gets a collection of the view-state values stored in the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		/// <returns>The collection of view-state values.</returns>
		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x0003D0E9 File Offset: 0x0003B2E9
		public ICollection Values
		{
			get
			{
				return this.ht.Values;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IDictionary.Add(System.Object,System.Object)" />.</summary>
		/// <param name="key">The <see cref="T:System.Object" /> to use as the key of the element to add.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to use as the value of the element to add. </param>
		// Token: 0x060016AD RID: 5805 RVA: 0x0003D0F6 File Offset: 0x0003B2F6
		void IDictionary.Add(object key, object value)
		{
			this.Add((string)key, value);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IDictionary.Remove(System.Object)" />.</summary>
		/// <param name="key">The key of the element to remove. </param>
		// Token: 0x060016AE RID: 5806 RVA: 0x0003D106 File Offset: 0x0003B306
		void IDictionary.Remove(object key)
		{
			this.Remove((string)key);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x060016AF RID: 5807 RVA: 0x0003D114 File Offset: 0x0003B314
		void ICollection.CopyTo(Array array, int index)
		{
			this.ht.CopyTo(array, index);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IDictionary.Contains(System.Object)" />.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> contains an element with the key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.IDictionary" /> object.</param>
		// Token: 0x060016B0 RID: 5808 RVA: 0x0003D123 File Offset: 0x0003B323
		bool IDictionary.Contains(object key)
		{
			return this.ht.Contains(key);
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x00008A69 File Offset: 0x00006C69
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x060016B2 RID: 5810 RVA: 0x0003D131 File Offset: 0x0003B331
		object ICollection.SyncRoot
		{
			get
			{
				return this.ht;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IDictionary.Item(System.Object)" />.</summary>
		/// <returns>The element with the specified <paramref name="key" />.</returns>
		/// <param name="key">The key of the element to get.</param>
		// Token: 0x17000732 RID: 1842
		object IDictionary.this[object key]
		{
			get
			{
				return this[(string)key];
			}
			set
			{
				this[(string)key] = value;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IDictionary.IsFixedSize" />.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> object has a fixed size; otherwise, false.</returns>
		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x060016B5 RID: 5813 RVA: 0x00008A69 File Offset: 0x00006C69
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IDictionary.IsReadOnly" />.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> object is read-only; otherwise, false.</returns>
		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x00008A69 File Offset: 0x00006C69
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Sets the state of the <see cref="T:System.Web.UI.StateBag" /> object as well as the <see cref="P:System.Web.SessionState.ISessionStateItemCollection.Dirty" /> property of each of the <see cref="T:System.Web.UI.StateItem" /> objects contained by it.</summary>
		/// <param name="dirty">true to mark the state of the collection and its items as modified; otherwise, false.</param>
		// Token: 0x060016B7 RID: 5815 RVA: 0x0003D158 File Offset: 0x0003B358
		public void SetDirty(bool dirty)
		{
			foreach (object obj in this.ht)
			{
				((StateItem)((DictionaryEntry)obj).Value).IsDirty = dirty;
			}
		}

		// Token: 0x0400157D RID: 5501
		private HybridDictionary ht;

		// Token: 0x0400157E RID: 5502
		private bool track;
	}
}
