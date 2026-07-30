using System;
using System.Collections;
using Unity;

namespace System.DirectoryServices
{
	/// <summary>Contains the values of a <see cref="T:System.DirectoryServices.DirectoryEntry" /> property.</summary>
	// Token: 0x02000028 RID: 40
	public class PropertyValueCollection : CollectionBase
	{
		// Token: 0x06000146 RID: 326 RVA: 0x0000445D File Offset: 0x0000265D
		internal PropertyValueCollection(DirectoryEntry parent)
		{
			this._Mbit = false;
			this._parent = parent;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00004473 File Offset: 0x00002673
		// (set) Token: 0x06000148 RID: 328 RVA: 0x0000447B File Offset: 0x0000267B
		internal bool Mbit
		{
			get
			{
				return this._Mbit;
			}
			set
			{
				this._Mbit = value;
			}
		}

		/// <summary>Gets or sets the property value that is located at a specified index of this collection.</summary>
		/// <returns>The property value at the specified index.</returns>
		/// <param name="index">The zero-based index of the property value.</param>
		/// <exception cref="T:System.ArgumentNullException">The property value is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">The index is less than zero (0) or greater than the size of the collection.</exception>
		// Token: 0x1700005A RID: 90
		public object this[int index]
		{
			get
			{
				return base.List[index];
			}
			set
			{
				base.List[index] = value;
				this._Mbit = true;
			}
		}

		/// <summary>Appends the specified <see cref="T:System.DirectoryServices.PropertyValueCollection" /> object to this collection.</summary>
		/// <returns>The zero-based index of the <see cref="T:System.DirectoryServices.PropertyValueCollection" /> object that is appended to this collection.</returns>
		/// <param name="value">The <see cref="T:System.DirectoryServices.PropertyValueCollection" />  object to append to this collection.</param>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">An error occurred during the call to the underlying interface.</exception>
		/// <exception cref="T:System.ArgumentNullException">The property value is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x0600014B RID: 331 RVA: 0x000044A8 File Offset: 0x000026A8
		public int Add(object value)
		{
			if (this.Contains(value))
			{
				return -1;
			}
			this._Mbit = true;
			return base.List.Add(value);
		}

		/// <summary>Appends the contents of the specified <see cref="T:System.DirectoryServices.PropertyValueCollection" /> object to this collection.</summary>
		/// <param name="value">The <see cref="T:System.DirectoryServices.PropertyValueCollection" /> array that contains the objects to append to this collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The property value is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x0600014C RID: 332 RVA: 0x000044C8 File Offset: 0x000026C8
		public void AddRange(object[] value)
		{
			foreach (object obj in value)
			{
				this.Add(obj);
			}
		}

		/// <summary>Appends the contents of the <see cref="T:System.DirectoryServices.PropertyValueCollection" /> object to this collection.</summary>
		/// <param name="value">A <see cref="T:System.DirectoryServices.PropertyValueCollection" /> object that contains the objects to append to this collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The property value is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x0600014D RID: 333 RVA: 0x000044F4 File Offset: 0x000026F4
		public void AddRange(PropertyValueCollection value)
		{
			foreach (object obj in value)
			{
				this.Add(obj);
			}
		}

		/// <summary>Retrieves the index of a specified property value in this collection.</summary>
		/// <returns>The zero-based index of the specified property value. If the object is not found, the return value is -1.</returns>
		/// <param name="value">The property value to find.</param>
		// Token: 0x0600014E RID: 334 RVA: 0x00003FF2 File Offset: 0x000021F2
		public int IndexOf(object value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts a property value into this collection at a specified index.</summary>
		/// <param name="index">The zero-based index at which to insert the property value.</param>
		/// <param name="value">The property value to insert.</param>
		/// <exception cref="T:System.ArgumentNullException">The property value is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">An error occurred during the call to the underlying interface.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">The index is less than 0 (zero) or greater than the size of the collection.</exception>
		// Token: 0x0600014F RID: 335 RVA: 0x00004544 File Offset: 0x00002744
		public void Insert(int index, object value)
		{
			base.List.Insert(index, value);
			this._Mbit = true;
		}

		/// <summary>Removes a specified property value from this collection.</summary>
		/// <exception cref="T:System.ArgumentNullException">The property value is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">An error occurred during the call to the underlying interface.</exception>
		// Token: 0x06000150 RID: 336 RVA: 0x0000455A File Offset: 0x0000275A
		public void Remove(object value)
		{
			base.List.Remove(value);
			this._Mbit = true;
		}

		/// <summary>Determines whether the specified <see cref="T:System.DirectoryServices.PropertyValueCollection" /> object is in this collection.</summary>
		/// <returns>true if the specified property belongs to this collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.DirectoryServices.PropertyValueCollection" /> object to search for in this collection.</param>
		// Token: 0x06000151 RID: 337 RVA: 0x00003FE4 File Offset: 0x000021E4
		public bool Contains(object value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00004570 File Offset: 0x00002770
		internal bool ContainsCaselessStringValue(string value)
		{
			for (int i = 0; i < base.Count; i++)
			{
				string text = (string)base.List[i];
				if (string.Compare(value, text, true) == 0)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies all <see cref="T:System.DirectoryServices.PropertyValueCollection" /> objects in this collection to the specified array, starting at the specified index in the target array.</summary>
		/// <param name="array">The array of <see cref="T:System.DirectoryServices.PropertyValueCollection" /> objects that receives the elements of this collection.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> where this method starts copying this collection.</param>
		// Token: 0x06000153 RID: 339 RVA: 0x000045B0 File Offset: 0x000027B0
		public void CopyTo(object[] array, int index)
		{
			foreach (object obj in base.List)
			{
				array[index++] = obj;
			}
		}

		/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnClearComplete" /> method.</summary>
		// Token: 0x06000154 RID: 340 RVA: 0x00004608 File Offset: 0x00002808
		[MonoTODO]
		protected override void OnClearComplete()
		{
			if (this._parent != null)
			{
				this._parent.CommitDeferred();
			}
		}

		/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnInsertComplete(System.Int32,System.Object)" /> method.</summary>
		/// <param name="index">The zero-based index at which to insert <paramref name="value" />.</param>
		/// <param name="value">The new value of the element at <paramref name="index" />.</param>
		// Token: 0x06000155 RID: 341 RVA: 0x00004608 File Offset: 0x00002808
		[MonoTODO]
		protected override void OnInsertComplete(int index, object value)
		{
			if (this._parent != null)
			{
				this._parent.CommitDeferred();
			}
		}

		/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnRemoveComplete(System.Int32,System.Object)" /> method.</summary>
		/// <param name="index">The zero-based index at which value can be found.</param>
		/// <param name="value">The value of the element to remove from <paramref name="index" />.</param>
		// Token: 0x06000156 RID: 342 RVA: 0x00004608 File Offset: 0x00002808
		[MonoTODO]
		protected override void OnRemoveComplete(int index, object value)
		{
			if (this._parent != null)
			{
				this._parent.CommitDeferred();
			}
		}

		/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnSetComplete(System.Int32,System.Object,System.Object)" /> method.</summary>
		/// <param name="index">The zero-based index at which <paramref name="oldValue" /> can be found.</param>
		/// <param name="oldValue">The value to replace with <paramref name="newValue" />. </param>
		/// <param name="newValue">The new value of the element at <paramref name="index" />. </param>
		// Token: 0x06000157 RID: 343 RVA: 0x00004608 File Offset: 0x00002808
		[MonoTODO]
		protected override void OnSetComplete(int index, object oldValue, object newValue)
		{
			if (this._parent != null)
			{
				this._parent.CommitDeferred();
			}
		}

		/// <summary>Gets the property name for the attributes in the value collection.</summary>
		/// <returns>A string that contains the name of the property with the values that are included in this <see cref="T:System.DirectoryServices.PropertyValueCollection" /> object.</returns>
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000461D File Offset: 0x0000281D
		[MonoTODO]
		public string PropertyName
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>Gets or sets the values of the collection.</summary>
		/// <returns>If the collection is empty, the property value is a null reference (Nothing in Visual Basic). If the collection contains one value, the property value is that value. If the collection contains multiple values, the property value equals a copy of an array of those values.If setting this property, the value or values are added to the <see cref="T:System.DirectoryServices.PropertyValueCollection" />. Setting this property to a null reference (Nothing) clears the collection.</returns>
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00004624 File Offset: 0x00002824
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00004688 File Offset: 0x00002888
		public object Value
		{
			get
			{
				int count = base.Count;
				if (count == 0)
				{
					return null;
				}
				if (count != 1)
				{
					Array array = new object[base.Count];
					for (int i = array.GetLowerBound(0); i <= array.GetUpperBound(0); i++)
					{
						array.SetValue(base.List[i], i);
					}
					return array;
				}
				return base.List[0];
			}
			set
			{
				if (value == null && base.List.Count == 0)
				{
					return;
				}
				base.List.Clear();
				if (value != null)
				{
					this.Add(value);
				}
				this._Mbit = true;
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00002644 File Offset: 0x00000844
		internal PropertyValueCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400009B RID: 155
		private bool _Mbit;

		// Token: 0x0400009C RID: 156
		private DirectoryEntry _parent;
	}
}
