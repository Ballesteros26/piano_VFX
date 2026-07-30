using System;

namespace System.Collections.Specialized
{
	/// <summary>Represents a collection of strings.</summary>
	// Token: 0x0200070A RID: 1802
	[Serializable]
	public class StringCollection : IList, ICollection, IEnumerable
	{
		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the entry to get or set. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.Specialized.StringCollection.Count" />. </exception>
		// Token: 0x17000DB7 RID: 3511
		public string this[int index]
		{
			get
			{
				return (string)this.data[index];
			}
			set
			{
				this.data[index] = value;
			}
		}

		/// <summary>Gets the number of strings contained in the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <returns>The number of strings contained in the <see cref="T:System.Collections.Specialized.StringCollection" />.</returns>
		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x060038B9 RID: 14521 RVA: 0x000D04A1 File Offset: 0x000CE6A1
		public int Count
		{
			get
			{
				return this.data.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.StringCollection" /> object is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Specialized.StringCollection" /> object is read-only; otherwise, false. The default is false.</returns>
		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x060038BA RID: 14522 RVA: 0x00004240 File Offset: 0x00002440
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.StringCollection" /> object has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Specialized.StringCollection" /> object has a fixed size; otherwise, false. The default is false.</returns>
		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x060038BB RID: 14523 RVA: 0x00004240 File Offset: 0x00002440
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Adds a string to the end of the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <returns>The zero-based index at which the new element is inserted.</returns>
		/// <param name="value">The string to add to the end of the <see cref="T:System.Collections.Specialized.StringCollection" />. The value can be null. </param>
		// Token: 0x060038BC RID: 14524 RVA: 0x000D04AE File Offset: 0x000CE6AE
		public int Add(string value)
		{
			return this.data.Add(value);
		}

		/// <summary>Copies the elements of a string array to the end of the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <param name="value">An array of strings to add to the end of the <see cref="T:System.Collections.Specialized.StringCollection" />. The array itself can not be null but it can contain elements that are null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		// Token: 0x060038BD RID: 14525 RVA: 0x000D04BC File Offset: 0x000CE6BC
		public void AddRange(string[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.data.AddRange(value);
		}

		/// <summary>Removes all the strings from the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		// Token: 0x060038BE RID: 14526 RVA: 0x000D04D8 File Offset: 0x000CE6D8
		public void Clear()
		{
			this.data.Clear();
		}

		/// <summary>Determines whether the specified string is in the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <returns>true if <paramref name="value" /> is found in the <see cref="T:System.Collections.Specialized.StringCollection" />; otherwise, false.</returns>
		/// <param name="value">The string to locate in the <see cref="T:System.Collections.Specialized.StringCollection" />. The value can be null. </param>
		// Token: 0x060038BF RID: 14527 RVA: 0x000D04E5 File Offset: 0x000CE6E5
		public bool Contains(string value)
		{
			return this.data.Contains(value);
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.Specialized.StringCollection" /> values to a one-dimensional array of strings, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional array of strings that is the destination of the elements copied from <see cref="T:System.Collections.Specialized.StringCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.Specialized.StringCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Specialized.StringCollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x060038C0 RID: 14528 RVA: 0x000D04F3 File Offset: 0x000CE6F3
		public void CopyTo(string[] array, int index)
		{
			this.data.CopyTo(array, index);
		}

		/// <summary>Returns a <see cref="T:System.Collections.Specialized.StringEnumerator" /> that iterates through the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringEnumerator" /> for the <see cref="T:System.Collections.Specialized.StringCollection" />.</returns>
		// Token: 0x060038C1 RID: 14529 RVA: 0x000D0502 File Offset: 0x000CE702
		public StringEnumerator GetEnumerator()
		{
			return new StringEnumerator(this);
		}

		/// <summary>Searches for the specified string and returns the zero-based index of the first occurrence within the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> in the <see cref="T:System.Collections.Specialized.StringCollection" />, if found; otherwise, -1.</returns>
		/// <param name="value">The string to locate. The value can be null. </param>
		// Token: 0x060038C2 RID: 14530 RVA: 0x000D050A File Offset: 0x000CE70A
		public int IndexOf(string value)
		{
			return this.data.IndexOf(value);
		}

		/// <summary>Inserts a string into the <see cref="T:System.Collections.Specialized.StringCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> is inserted. </param>
		/// <param name="value">The string to insert. The value can be null. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> greater than <see cref="P:System.Collections.Specialized.StringCollection.Count" />. </exception>
		// Token: 0x060038C3 RID: 14531 RVA: 0x000D0518 File Offset: 0x000CE718
		public void Insert(int index, string value)
		{
			this.data.Insert(index, value);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.StringCollection" /> is read-only.</summary>
		/// <returns>This property always returns false.</returns>
		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x060038C4 RID: 14532 RVA: 0x00004240 File Offset: 0x00002440
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Specialized.StringCollection" /> is synchronized (thread safe).</summary>
		/// <returns>This property always returns false.</returns>
		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x060038C5 RID: 14533 RVA: 0x00004240 File Offset: 0x00002440
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Removes the first occurrence of a specific string from the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <param name="value">The string to remove from the <see cref="T:System.Collections.Specialized.StringCollection" />. The value can be null. </param>
		// Token: 0x060038C6 RID: 14534 RVA: 0x000D0527 File Offset: 0x000CE727
		public void Remove(string value)
		{
			this.data.Remove(value);
		}

		/// <summary>Removes the string at the specified index of the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <param name="index">The zero-based index of the string to remove. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.Specialized.StringCollection.Count" />. </exception>
		// Token: 0x060038C7 RID: 14535 RVA: 0x000D0535 File Offset: 0x000CE735
		public void RemoveAt(int index)
		{
			this.data.RemoveAt(index);
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.StringCollection" />.</returns>
		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x060038C8 RID: 14536 RVA: 0x000D0543 File Offset: 0x000CE743
		public object SyncRoot
		{
			get
			{
				return this.data.SyncRoot;
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.Specialized.StringCollection.Count" />. </exception>
		// Token: 0x17000DBE RID: 3518
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (string)value;
			}
		}

		/// <summary>Adds an object to the end of the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <returns>The <see cref="T:System.Collections.Specialized.StringCollection" /> index at which the <paramref name="value" /> has been added.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be added to the end of the <see cref="T:System.Collections.Specialized.StringCollection" />. The value can be null. </param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.StringCollection" /> is read-only.-or- The <see cref="T:System.Collections.Specialized.StringCollection" /> has a fixed size. </exception>
		// Token: 0x060038CB RID: 14539 RVA: 0x000D0568 File Offset: 0x000CE768
		int IList.Add(object value)
		{
			return this.Add((string)value);
		}

		/// <summary>Determines whether an element is in the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <returns>true if <paramref name="value" /> is found in the <see cref="T:System.Collections.Specialized.StringCollection" />; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.Specialized.StringCollection" />. The value can be null. </param>
		// Token: 0x060038CC RID: 14540 RVA: 0x000D0576 File Offset: 0x000CE776
		bool IList.Contains(object value)
		{
			return this.Contains((string)value);
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the entire <see cref="T:System.Collections.Specialized.StringCollection" />, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.Specialized.StringCollection" />. The value can be null. </param>
		// Token: 0x060038CD RID: 14541 RVA: 0x000D0584 File Offset: 0x000CE784
		int IList.IndexOf(object value)
		{
			return this.IndexOf((string)value);
		}

		/// <summary>Inserts an element into the <see cref="T:System.Collections.Specialized.StringCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert. The value can be null. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is greater than <see cref="P:System.Collections.Specialized.StringCollection.Count" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.StringCollection" /> is read-only.-or- The <see cref="T:System.Collections.Specialized.StringCollection" /> has a fixed size. </exception>
		// Token: 0x060038CE RID: 14542 RVA: 0x000D0592 File Offset: 0x000CE792
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (string)value);
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.Specialized.StringCollection" />. The value can be null. </param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.StringCollection" /> is read-only.-or- The <see cref="T:System.Collections.Specialized.StringCollection" /> has a fixed size. </exception>
		// Token: 0x060038CF RID: 14543 RVA: 0x000D05A1 File Offset: 0x000CE7A1
		void IList.Remove(object value)
		{
			this.Remove((string)value);
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.Specialized.StringCollection" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Specialized.StringCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.Specialized.StringCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Specialized.StringCollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x060038D0 RID: 14544 RVA: 0x000D04F3 File Offset: 0x000CE6F3
		void ICollection.CopyTo(Array array, int index)
		{
			this.data.CopyTo(array, index);
		}

		/// <summary>Returns a <see cref="T:System.Collections.IEnumerator" /> that iterates through the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <returns>A <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Specialized.StringCollection" />.</returns>
		// Token: 0x060038D1 RID: 14545 RVA: 0x000D05AF File Offset: 0x000CE7AF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.data.GetEnumerator();
		}

		// Token: 0x04002C76 RID: 11382
		private ArrayList data = new ArrayList();
	}
}
