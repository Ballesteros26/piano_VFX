using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.DataKey" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000380 RID: 896
	public sealed class DataKeyArray : ICollection, IEnumerable, IStateManager
	{
		// Token: 0x06002243 RID: 8771 RVA: 0x0005845E File Offset: 0x0005665E
		internal DataKeyArray(IList keys)
		{
			this.keys = keys;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataKeyArray" /> class.</summary>
		/// <param name="keys">An <see cref="T:System.Collections.ArrayList" /> of <see cref="T:System.Web.UI.WebControls.DataKey" /> objects with which to populate the collection.</param>
		// Token: 0x06002244 RID: 8772 RVA: 0x0005845E File Offset: 0x0005665E
		public DataKeyArray(ArrayList keys)
		{
			this.keys = keys;
		}

		/// <summary>Gets the number of items in the collection.</summary>
		/// <returns>The number of items in the collection.</returns>
		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06002245 RID: 8773 RVA: 0x0005846D File Offset: 0x0005666D
		public int Count
		{
			get
			{
				return this.keys.Count;
			}
		}

		/// <summary>Gets a value indicating whether the items in the collection can be modified.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06002246 RID: 8774 RVA: 0x00008A69 File Offset: 0x00006C69
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.DataKeyArray" /> collection is synchronized (thread safe).</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06002247 RID: 8775 RVA: 0x00008A69 File Offset: 0x00006C69
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.DataKey" /> object from the collection at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.DataKey" /> at the specified index in the collection.</returns>
		/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.DataKey" /> to retrieve from the collection.</param>
		// Token: 0x17000ACC RID: 2764
		public DataKey this[int index]
		{
			get
			{
				return (DataKey)this.keys[index];
			}
		}

		/// <summary>Gets the object used to synchronize access to the collection.</summary>
		/// <returns>An <see cref="T:System.Object" /> that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06002249 RID: 8777 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies all the items from this collection to the specified array of <see cref="T:System.Web.UI.WebControls.DataKey" /> objects, starting at the specified index in the array.</summary>
		/// <param name="array">A zero-based array of <see cref="T:System.Web.UI.WebControls.DataKey" /> objects that receives the copied items from the collection.</param>
		/// <param name="index">The first index in the specified array to receive the copied contents.</param>
		// Token: 0x0600224A RID: 8778 RVA: 0x00058490 File Offset: 0x00056690
		public void CopyTo(DataKey[] array, int index)
		{
			foreach (object obj in this)
			{
				DataKey dataKey = (DataKey)obj;
				array[index++] = dataKey;
			}
		}

		/// <summary>Copies all the items from this collection to the specified <see cref="T:System.Array" />, starting at the specified index in the <see cref="T:System.Array" />.</summary>
		/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the collection.</param>
		/// <param name="index">The first index in the specified <see cref="T:System.Array" /> to receive the copied contents.</param>
		// Token: 0x0600224B RID: 8779 RVA: 0x000584E8 File Offset: 0x000566E8
		void ICollection.CopyTo(Array array, int index)
		{
			foreach (object obj in this)
			{
				array.SetValue(obj, index++);
			}
		}

		/// <summary>Returns an enumerator that contains all <see cref="T:System.Web.UI.WebControls.DataKey" /> objects in the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" />-implemented object that contains all <see cref="T:System.Web.UI.WebControls.DataKey" /> objects in the collection.</returns>
		// Token: 0x0600224C RID: 8780 RVA: 0x00058540 File Offset: 0x00056740
		public IEnumerator GetEnumerator()
		{
			return this.keys.GetEnumerator();
		}

		/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.DataKeyArray" /> object.</summary>
		/// <param name="state">An <see cref="T:System.Object" /> that represents the state of the <see cref="T:System.Web.UI.WebControls.DataKeyArray" />.</param>
		// Token: 0x0600224D RID: 8781 RVA: 0x00058550 File Offset: 0x00056750
		void IStateManager.LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				return;
			}
			object[] array = (object[])savedState;
			int num = 0;
			while (num < array.Length && num < this.keys.Count)
			{
				((IStateManager)this.keys[num]).LoadViewState(array[num]);
				num++;
			}
		}

		/// <summary>Saves the current view state of the <see cref="T:System.Web.UI.WebControls.DataKeyArray" /> object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the saved state of the <see cref="T:System.Web.UI.WebControls.DataKeyArray" />.</returns>
		// Token: 0x0600224E RID: 8782 RVA: 0x000585A0 File Offset: 0x000567A0
		object IStateManager.SaveViewState()
		{
			if (this.keys.Count == 0)
			{
				return null;
			}
			object[] array = new object[this.keys.Count];
			for (int i = 0; i < this.keys.Count; i++)
			{
				array[i] = ((IStateManager)this.keys[i]).SaveViewState();
			}
			return array;
		}

		/// <summary>Marks the starting point at which to begin tracking and saving view-state changes to the <see cref="T:System.Web.UI.WebControls.DataKeyArray" /> object.</summary>
		// Token: 0x0600224F RID: 8783 RVA: 0x00058600 File Offset: 0x00056800
		void IStateManager.TrackViewState()
		{
			this.trackViewState = true;
			foreach (object obj in this.keys)
			{
				((IStateManager)obj).TrackViewState();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.DataKeyArray" /> object is tracking its view-state changes.</summary>
		/// <returns>true to indicate that the <see cref="T:System.Web.UI.WebControls.DataKeyArray" /> is tracking its view-state changes; otherwise, false.</returns>
		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06002250 RID: 8784 RVA: 0x00058660 File Offset: 0x00056860
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.trackViewState;
			}
		}

		// Token: 0x04001913 RID: 6419
		private IList keys;

		// Token: 0x04001914 RID: 6420
		private bool trackViewState;
	}
}
