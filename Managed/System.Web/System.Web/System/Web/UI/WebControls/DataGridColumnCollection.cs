using System;
using System.Collections;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>A collection of <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column objects that represent the columns in a <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. This class cannot be inherited. </summary>
	// Token: 0x0200037A RID: 890
	public sealed class DataGridColumnCollection : ICollection, IEnumerable, IStateManager
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> class.</summary>
		/// <param name="owner">The <see cref="T:System.Web.UI.WebControls.DataGrid" /> control that corresponds with this collection. </param>
		/// <param name="columns">A <see cref="T:System.Collections.ArrayList" /> that stores the collection of columns. </param>
		// Token: 0x060021FE RID: 8702 RVA: 0x00057820 File Offset: 0x00055A20
		public DataGridColumnCollection(DataGrid owner, ArrayList columns)
		{
			this.owner = owner;
			this.columns = columns;
		}

		/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column object to the end of the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection.</summary>
		/// <param name="column">The <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column object to append to the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" />. </param>
		// Token: 0x060021FF RID: 8703 RVA: 0x00057836 File Offset: 0x00055A36
		public void Add(DataGridColumn column)
		{
			this.columns.Add(column);
			column.Set_Owner(this.owner);
			if (this.track)
			{
				((IStateManager)column).TrackViewState();
			}
		}

		/// <summary>Inserts a <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column object in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection at the specified index.</summary>
		/// <param name="index">The index location in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> at which to insert the <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column. </param>
		/// <param name="column">The <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column to insert in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="column" /> is null.</exception>
		// Token: 0x06002200 RID: 8704 RVA: 0x0005785F File Offset: 0x00055A5F
		public void AddAt(int index, DataGridColumn column)
		{
			this.columns.Insert(index, column);
			column.Set_Owner(this.owner);
			if (this.track)
			{
				((IStateManager)column).TrackViewState();
			}
		}

		/// <summary>Removes all <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column objects from the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection.</summary>
		// Token: 0x06002201 RID: 8705 RVA: 0x00057888 File Offset: 0x00055A88
		public void Clear()
		{
			this.columns.Clear();
		}

		/// <summary>Copies the items from the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection to the specified <see cref="T:System.Array" />, starting at the specified index in the <see cref="T:System.Array" />.</summary>
		/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" />. </param>
		/// <param name="index">The first position in the specified <see cref="T:System.Array" /> to receive the copied contents. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		// Token: 0x06002202 RID: 8706 RVA: 0x00057895 File Offset: 0x00055A95
		public void CopyTo(Array array, int index)
		{
			this.columns.CopyTo(array, index);
		}

		/// <summary>Returns a <see cref="T:System.Collections.IEnumerator" /> interface that contains all the <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column objects in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Collections.IEnumerator" /> interface that contains all <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column objects in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" />.</returns>
		// Token: 0x06002203 RID: 8707 RVA: 0x000578A4 File Offset: 0x00055AA4
		public IEnumerator GetEnumerator()
		{
			return this.columns.GetEnumerator();
		}

		/// <summary>Returns the index of the specified <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column object from the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection.</summary>
		/// <returns>The index position of the specified <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" />. The default value is -1, which indicates that the specified <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived object is not found.</returns>
		/// <param name="column">The <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column to search for in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" />. </param>
		// Token: 0x06002204 RID: 8708 RVA: 0x000578B1 File Offset: 0x00055AB1
		public int IndexOf(DataGridColumn column)
		{
			return this.columns.IndexOf(column);
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x0000393A File Offset: 0x00001B3A
		[Obsolete("figure out what you need with me")]
		internal void OnColumnsChanged()
		{
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column object from the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection.</summary>
		/// <param name="column">The <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column to remove from the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" />. </param>
		// Token: 0x06002206 RID: 8710 RVA: 0x000578BF File Offset: 0x00055ABF
		public void Remove(DataGridColumn column)
		{
			this.columns.Remove(column);
		}

		/// <summary>Removes a <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column object from the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection at the specified index.</summary>
		/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> to remove. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or greater than <see cref="P:System.Web.UI.WebControls.DataGridColumnCollection.Count" />.</exception>
		// Token: 0x06002207 RID: 8711 RVA: 0x000578CD File Offset: 0x00055ACD
		public void RemoveAt(int index)
		{
			this.columns.RemoveAt(index);
		}

		/// <summary>Loads the previously saved state.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that contains the saved view state values for the control.</param>
		// Token: 0x06002208 RID: 8712 RVA: 0x000578DC File Offset: 0x00055ADC
		void IStateManager.LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array == null)
			{
				return;
			}
			int num = 0;
			foreach (object obj in this)
			{
				((IStateManager)obj).LoadViewState(array[num++]);
			}
		}

		/// <summary>Returns an object containing state changes.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the saved view state values for the control.</returns>
		// Token: 0x06002209 RID: 8713 RVA: 0x00057944 File Offset: 0x00055B44
		object IStateManager.SaveViewState()
		{
			object[] array = new object[this.Count];
			int num = 0;
			foreach (object obj in this)
			{
				IStateManager stateManager = (IStateManager)obj;
				array[num++] = stateManager.SaveViewState();
			}
			object[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				if (array2[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		/// <summary>Starts tracking state changes.</summary>
		// Token: 0x0600220A RID: 8714 RVA: 0x000579D0 File Offset: 0x00055BD0
		void IStateManager.TrackViewState()
		{
			this.track = true;
			foreach (object obj in this)
			{
				((IStateManager)obj).TrackViewState();
			}
		}

		/// <summary>Gets the number of columns in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection.</summary>
		/// <returns>The number of columns in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" />.</returns>
		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x0600220B RID: 8715 RVA: 0x00057A28 File Offset: 0x00055C28
		[Browsable(false)]
		public int Count
		{
			get
			{
				return this.columns.Count;
			}
		}

		/// <summary>Gets a value indicating whether the collection is tracking its view-state changes.</summary>
		/// <returns>true if a <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> object is tracking its view-state changes; otherwise, false.</returns>
		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x0600220C RID: 8716 RVA: 0x00057A35 File Offset: 0x00055C35
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.track;
			}
		}

		/// <summary>Gets a value that indicates whether the columns in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection can be modified.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x0600220D RID: 8717 RVA: 0x00057A3D File Offset: 0x00055C3D
		[Browsable(false)]
		public bool IsReadOnly
		{
			get
			{
				return this.columns.IsReadOnly;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection is synchronized (thread safe).</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x0600220E RID: 8718 RVA: 0x00057A4A File Offset: 0x00055C4A
		[Browsable(false)]
		public bool IsSynchronized
		{
			get
			{
				return this.columns.IsSynchronized;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column object from the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection at the specified index.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> to retrieve. </param>
		// Token: 0x17000AAF RID: 2735
		[Browsable(false)]
		public DataGridColumn this[int index]
		{
			get
			{
				return (DataGridColumn)this.columns[index];
			}
		}

		/// <summary>Gets the object that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Object" /> that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06002210 RID: 8720 RVA: 0x00057A6A File Offset: 0x00055C6A
		[Browsable(false)]
		public object SyncRoot
		{
			get
			{
				return this.columns.SyncRoot;
			}
		}

		// Token: 0x04001900 RID: 6400
		private DataGrid owner;

		// Token: 0x04001901 RID: 6401
		private ArrayList columns;

		// Token: 0x04001902 RID: 6402
		private bool track;
	}
}
