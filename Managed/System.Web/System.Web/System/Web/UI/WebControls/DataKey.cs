using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the primary key field or fields of a record in a data-bound control.</summary>
	// Token: 0x0200037F RID: 895
	public class DataKey : IStateManager, IEquatable<DataKey>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataKey" /> class using the specified dictionary of key field values.</summary>
		/// <param name="keyTable">An <see cref="T:System.Collections.Specialized.IOrderedDictionary" />  that contains the key field values.</param>
		// Token: 0x06002234 RID: 8756 RVA: 0x0005809B File Offset: 0x0005629B
		public DataKey(IOrderedDictionary keyTable)
			: this(keyTable, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataKey" /> class using the specified dictionary of key field values and array of field names.</summary>
		/// <param name="keyTable">The key field values.</param>
		/// <param name="keyNames">An array of strings that contain the names of the key fields.</param>
		// Token: 0x06002235 RID: 8757 RVA: 0x000580A5 File Offset: 0x000562A5
		public DataKey(IOrderedDictionary keyTable, string[] keyNames)
		{
			this.keyTable = keyTable;
			this.keyNames = keyNames;
		}

		/// <summary>Gets the value of the key field at the specified index from a <see cref="T:System.Web.UI.WebControls.DataKey" /> object.</summary>
		/// <returns>The value of the key field at the specified index.</returns>
		/// <param name="index">The zero-based index at which to retrieve the key field value.</param>
		// Token: 0x17000AC3 RID: 2755
		public virtual object this[int index]
		{
			get
			{
				return this.keyTable[index];
			}
		}

		/// <summary>Gets the value of the key field with the specified field name from a <see cref="T:System.Web.UI.WebControls.DataKey" /> object.</summary>
		/// <returns>The value of the key field with the specified field name.</returns>
		/// <param name="name">The name of the key field for which to retrieve the key field value.</param>
		// Token: 0x17000AC4 RID: 2756
		public virtual object this[string name]
		{
			get
			{
				return this.keyTable[name];
			}
		}

		/// <summary>Gets the value of the key field at index 0 in the <see cref="T:System.Web.UI.WebControls.DataKey" /> object.</summary>
		/// <returns>The value of the key field at index 0 in the <see cref="T:System.Web.UI.WebControls.DataKey" />.</returns>
		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06002238 RID: 8760 RVA: 0x000580D7 File Offset: 0x000562D7
		public virtual object Value
		{
			get
			{
				if (this.keyTable.Count == 0)
				{
					return null;
				}
				return this.keyTable[0];
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> object that contains every key field in the <see cref="T:System.Web.UI.WebControls.DataKey" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains every key field in the <see cref="T:System.Web.UI.WebControls.DataKey" />.</returns>
		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06002239 RID: 8761 RVA: 0x000580F4 File Offset: 0x000562F4
		public virtual IOrderedDictionary Values
		{
			get
			{
				if (this.readonlyKeyTable == null)
				{
					if (this.keyTable is OrderedDictionary)
					{
						this.readonlyKeyTable = ((OrderedDictionary)this.keyTable).AsReadOnly();
					}
					else
					{
						this.readonlyKeyTable = this.keyTable;
					}
				}
				return this.readonlyKeyTable;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Web.UI.WebControls.DataKey" /> array is equal to the current data key.</summary>
		/// <param name="other">The <see cref="T:System.Web.UI.WebControls.DataKey" /> object to compare to the current <see cref="T:System.Web.UI.WebControls.DataKey" />. object.</param>
		// Token: 0x0600223A RID: 8762 RVA: 0x00058140 File Offset: 0x00056340
		public bool Equals(DataKey other)
		{
			if (other == null)
			{
				return false;
			}
			IOrderedDictionary orderedDictionary = other.keyTable;
			if (this.keyTable != null && orderedDictionary != null)
			{
				if (this.keyTable.Count != orderedDictionary.Count)
				{
					return false;
				}
				foreach (object obj in this.keyTable.Keys)
				{
					if (!orderedDictionary.Contains(obj))
					{
						return false;
					}
					object obj2 = this.keyTable[obj];
					object obj3 = orderedDictionary[obj];
					if ((obj2 == null) ^ (obj3 == null))
					{
						return false;
					}
					if (!obj2.Equals(obj3))
					{
						return false;
					}
				}
			}
			string[] array = other.keyNames;
			if (this.keyNames != null && array != null)
			{
				int num = this.keyNames.Length;
				if (num != array.Length)
				{
					return false;
				}
				for (int i = 0; i < num; i++)
				{
					if (string.Compare(this.keyNames[i], array[i], StringComparison.Ordinal) != 0)
					{
						return false;
					}
				}
			}
			else if ((this.keyNames == null) ^ (array == null))
			{
				return false;
			}
			return true;
		}

		/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.DataKey" /> object.</summary>
		/// <param name="state">An object that represents the state of the <see cref="T:System.Web.UI.WebControls.DataKey" />.</param>
		/// <exception cref="T:System.Web.HttpException">
		///   <paramref name="state" /> is not null and cannot be resolved to a valid <see cref="P:System.Web.UI.Control.ViewState" />.</exception>
		// Token: 0x0600223B RID: 8763 RVA: 0x00058280 File Offset: 0x00056480
		protected virtual void LoadViewState(object state)
		{
			if (state is Pair)
			{
				Pair pair = (Pair)state;
				object[] array = (object[])pair.First;
				object[] array2 = (object[])pair.Second;
				for (int i = 0; i < array.Length; i++)
				{
					this.keyTable[array[i]] = array2[i];
				}
				return;
			}
			if (state is object[])
			{
				object[] array3 = (object[])state;
				for (int j = 0; j < array3.Length; j++)
				{
					this.keyTable[this.keyNames[j]] = array3[j];
				}
			}
		}

		/// <summary>Saves the current view state of the <see cref="T:System.Web.UI.WebControls.DataKey" /> object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the saved state of the <see cref="T:System.Web.UI.WebControls.DataKey" /> object.</returns>
		// Token: 0x0600223C RID: 8764 RVA: 0x0005830C File Offset: 0x0005650C
		protected virtual object SaveViewState()
		{
			if (this.keyTable.Count == 0)
			{
				return null;
			}
			if (this.keyNames != null)
			{
				object[] array = new object[this.keyTable.Count];
				int num = 0;
				foreach (object obj in this.keyTable.Values)
				{
					array[num++] = obj;
				}
				return array;
			}
			object[] array2 = new object[this.keyTable.Count];
			object[] array3 = new object[this.keyTable.Count];
			int num2 = 0;
			foreach (object obj2 in this.keyTable)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
				array3[num2] = dictionaryEntry.Key;
				array2[num2++] = dictionaryEntry.Value;
			}
			return new Pair(array3, array2);
		}

		/// <summary>Marks the starting point at which to begin tracking and saving view-state changes to the <see cref="T:System.Web.UI.WebControls.DataKey" /> object.</summary>
		// Token: 0x0600223D RID: 8765 RVA: 0x0005842C File Offset: 0x0005662C
		protected virtual void TrackViewState()
		{
			this.trackViewState = true;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.DataKey" /> object is tracking its view-state changes.</summary>
		/// <returns>true to indicate that the <see cref="T:System.Web.UI.WebControls.DataKey" /> is tracking its view-state changes; otherwise, false.</returns>
		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x0600223E RID: 8766 RVA: 0x00058435 File Offset: 0x00056635
		protected virtual bool IsTrackingViewState
		{
			get
			{
				return this.trackViewState;
			}
		}

		/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.DataKey" /> object.</summary>
		/// <param name="state">An <see cref="T:System.Object" /> that represents the state of the <see cref="T:System.Web.UI.WebControls.DataKey" />.</param>
		// Token: 0x0600223F RID: 8767 RVA: 0x0005843D File Offset: 0x0005663D
		void IStateManager.LoadViewState(object savedState)
		{
			this.LoadViewState(savedState);
		}

		/// <summary>Saves the current view state of the <see cref="T:System.Web.UI.WebControls.DataKey" /> object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the saved state of the <see cref="T:System.Web.UI.WebControls.DataKey" /> object.</returns>
		// Token: 0x06002240 RID: 8768 RVA: 0x00058446 File Offset: 0x00056646
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		/// <summary>Marks the starting point at which to begin tracking and saving view-state changes to the <see cref="T:System.Web.UI.WebControls.DataKey" /> object.</summary>
		// Token: 0x06002241 RID: 8769 RVA: 0x0005844E File Offset: 0x0005664E
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.DataKey" /> object is tracking its view-state changes.</summary>
		/// <returns>true to indicate that the <see cref="T:System.Web.UI.WebControls.DataKey" /> is tracking its view-state changes; otherwise, false.</returns>
		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06002242 RID: 8770 RVA: 0x00058456 File Offset: 0x00056656
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x0400190F RID: 6415
		private IOrderedDictionary keyTable;

		// Token: 0x04001910 RID: 6416
		private string[] keyNames;

		// Token: 0x04001911 RID: 6417
		private bool trackViewState;

		// Token: 0x04001912 RID: 6418
		private IOrderedDictionary readonlyKeyTable;
	}
}
