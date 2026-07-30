using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System
{
	/// <summary>Provides methods for creating, manipulating, searching, and sorting arrays, thereby serving as the base class for all arrays in the common language runtime.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000E1 RID: 225
	public abstract class Array : ICollection, IEnumerable, IList, IStructuralComparable, IStructuralEquatable, ICloneable
	{
		/// <summary>Creates a multidimensional <see cref="T:System.Array" /> of the specified <see cref="T:System.Type" /> and dimension lengths, with zero-based indexing. The dimension lengths are specified in an array of 64-bit integers.</summary>
		/// <returns>A new multidimensional <see cref="T:System.Array" /> of the specified <see cref="T:System.Type" /> with the specified length for each dimension, using zero-based indexing.</returns>
		/// <param name="elementType">The <see cref="T:System.Type" /> of the <see cref="T:System.Array" /> to create.</param>
		/// <param name="lengths">An array of 64-bit integers that represent the size of each dimension of the <see cref="T:System.Array" /> to create. Each integer in the array must be between zero and <see cref="F:System.Int32.MaxValue" />, inclusive.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="elementType" /> is null.-or-<paramref name="lengths" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="elementType" /> is not a valid <see cref="T:System.Type" />.-or-The <paramref name="lengths" /> array contains less than one element.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="elementType" /> is not supported. For example, <see cref="T:System.Void" /> is not supported. -or-<paramref name="elementType" /> is an open generic type.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Any value in <paramref name="lengths" /> is less than zero or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060007E6 RID: 2022 RVA: 0x0002C240 File Offset: 0x0002A440
		public static Array CreateInstance(Type elementType, params long[] lengths)
		{
			if (lengths == null)
			{
				throw new ArgumentNullException("lengths");
			}
			if (lengths.Length == 0)
			{
				throw new ArgumentException("Must provide at least one rank.");
			}
			int[] array = new int[lengths.Length];
			for (int i = 0; i < lengths.Length; i++)
			{
				long num = lengths[i];
				if (num > 2147483647L || num < -2147483648L)
				{
					throw new ArgumentOutOfRangeException("len", "Arrays larger than 2GB are not supported.");
				}
				array[i] = (int)num;
			}
			return Array.CreateInstance(elementType, array);
		}

		/// <summary>Returns a read-only wrapper for the specified array.</summary>
		/// <returns>A read-only <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> wrapper for the specified array.</returns>
		/// <param name="array">The one-dimensional, zero-based array to wrap in a read-only <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />  wrapper. </param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		// Token: 0x060007E7 RID: 2023 RVA: 0x0002C2B3 File Offset: 0x0002A4B3
		public static ReadOnlyCollection<T> AsReadOnly<T>(T[] array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return new ReadOnlyCollection<T>(array);
		}

		/// <summary>Changes the number of elements of a one-dimensional array to the specified new size.</summary>
		/// <param name="array">The one-dimensional, zero-based array to resize, or null to create a new array with the specified size.</param>
		/// <param name="newSize">The size of the new array.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="newSize" /> is less than zero.</exception>
		// Token: 0x060007E8 RID: 2024 RVA: 0x0002C2CC File Offset: 0x0002A4CC
		public static void Resize<T>(ref T[] array, int newSize)
		{
			if (newSize < 0)
			{
				throw new ArgumentOutOfRangeException("newSize", "Non-negative number required.");
			}
			T[] array2 = array;
			if (array2 == null)
			{
				array = new T[newSize];
				return;
			}
			if (array2.Length != newSize)
			{
				T[] array3 = new T[newSize];
				Array.Copy(array2, 0, array3, 0, (array2.Length > newSize) ? newSize : array2.Length);
				array = array3;
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Array" />.</summary>
		/// <returns>The number of elements contained in the T:System.Collections.ICollection.</returns>
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x0002C321 File Offset: 0x0002A521
		int ICollection.Count
		{
			get
			{
				return this.Length;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x00015ED5 File Offset: 0x000140D5
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The index of the element to get or set.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ICollection.Count" />.</exception>
		/// <exception cref="T:System.ArgumentException">The current <see cref="T:System.Array" /> does not have exactly one dimension.</exception>
		// Token: 0x17000140 RID: 320
		object IList.this[int index]
		{
			get
			{
				return this.GetValue(index);
			}
			set
			{
				this.SetValue(value, index);
			}
		}

		/// <summary>Adds an item to <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The position into which the new element was inserted, or -1 to indicate that the item was not inserted into the collection.</returns>
		/// <param name="value">The object to be added to the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IList" /> is read-only.-or- The <see cref="T:System.Collections.IList" /> has a fixed size.</exception>
		// Token: 0x060007ED RID: 2029 RVA: 0x0002C33C File Offset: 0x0002A53C
		int IList.Add(object value)
		{
			throw new NotSupportedException("Collection was of a fixed size.");
		}

		/// <summary>Determines whether an element is in the <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>true if <paramref name="value" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, false.</returns>
		/// <param name="value">The object to locate in the current list. The element to locate can be null for reference types.</param>
		// Token: 0x060007EE RID: 2030 RVA: 0x0002C348 File Offset: 0x0002A548
		bool IList.Contains(object value)
		{
			return Array.IndexOf(this, value) >= 0;
		}

		/// <summary>Removes all items from the <see cref="T:System.Collections.IList" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IList" /> is read-only.</exception>
		// Token: 0x060007EF RID: 2031 RVA: 0x0002C357 File Offset: 0x0002A557
		void IList.Clear()
		{
			Array.Clear(this, this.GetLowerBound(0), this.Length);
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The index of value if found in the list; otherwise, -1.</returns>
		/// <param name="value">The object to locate in the current list.</param>
		// Token: 0x060007F0 RID: 2032 RVA: 0x0002C36C File Offset: 0x0002A56C
		int IList.IndexOf(object value)
		{
			return Array.IndexOf(this, value);
		}

		/// <summary>Inserts an item to the <see cref="T:System.Collections.IList" /> at the specified index.</summary>
		/// <param name="index">The index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The object to insert.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.IList" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IList" /> is read-only.-or- The <see cref="T:System.Collections.IList" /> has a fixed size. </exception>
		/// <exception cref="T:System.NullReferenceException">
		///   <paramref name="value" /> is null reference in the <see cref="T:System.Collections.IList" />.</exception>
		// Token: 0x060007F1 RID: 2033 RVA: 0x0002C33C File Offset: 0x0002A53C
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException("Collection was of a fixed size.");
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="value">The object to remove from the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IList" /> is read-only.-or- The <see cref="T:System.Collections.IList" /> has a fixed size. </exception>
		// Token: 0x060007F2 RID: 2034 RVA: 0x0002C33C File Offset: 0x0002A53C
		void IList.Remove(object value)
		{
			throw new NotSupportedException("Collection was of a fixed size.");
		}

		/// <summary>Removes the <see cref="T:System.Collections.IList" /> item at the specified index.</summary>
		/// <param name="index">The index of the element to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="T:System.Collections.IList" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IList" /> is read-only.-or- The <see cref="T:System.Collections.IList" /> has a fixed size.</exception>
		// Token: 0x060007F3 RID: 2035 RVA: 0x0002C33C File Offset: 0x0002A53C
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException("Collection was of a fixed size.");
		}

		/// <summary>Copies all the elements of the current one-dimensional <see cref="T:System.Array" /> to the specified one-dimensional <see cref="T:System.Array" /> starting at the specified destination <see cref="T:System.Array" /> index. The index is specified as a 32-bit integer.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the current <see cref="T:System.Array" />.</param>
		/// <param name="index">A 32-bit integer that represents the index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-The number of elements in the source <see cref="T:System.Array" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.ArrayTypeMismatchException">The type of the source <see cref="T:System.Array" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.RankException">The source <see cref="T:System.Array" /> is multidimensional.</exception>
		/// <exception cref="T:System.InvalidCastException">At least one element in the source <see cref="T:System.Array" /> cannot be cast to the type of destination <paramref name="array" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060007F4 RID: 2036 RVA: 0x0002C375 File Offset: 0x0002A575
		public void CopyTo(Array array, int index)
		{
			if (array != null && array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.");
			}
			Array.Copy(this, this.GetLowerBound(0), array, index, this.Length);
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Array" />.</summary>
		/// <returns>A shallow copy of the <see cref="T:System.Array" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060007F5 RID: 2037 RVA: 0x0002C3A3 File Offset: 0x0002A5A3
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Determines whether the current collection object precedes, occurs in the same position as, or follows another object in the sort order.</summary>
		/// <returns>An integer that indicates the relationship of the current collection object to other, as shown in the following table.Return valueDescription-1The current instance precedes <paramref name="other" />.0The current instance and <paramref name="other" /> are equal.1The current instance follows <paramref name="other" />.</returns>
		/// <param name="other">The object to compare with the current instance.</param>
		/// <param name="comparer">An object that compares the current object and <paramref name="other" />.</param>
		// Token: 0x060007F6 RID: 2038 RVA: 0x0002C3AC File Offset: 0x0002A5AC
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Array array = other as Array;
			if (array == null || this.Length != array.Length)
			{
				throw new ArgumentException("Object is not a array with the same number of elements as the array to compare it to.", "other");
			}
			int num = 0;
			int num2 = 0;
			while (num < array.Length && num2 == 0)
			{
				object value = this.GetValue(num);
				object value2 = array.GetValue(num);
				num2 = comparer.Compare(value, value2);
				num++;
			}
			return num2;
		}

		/// <summary>Determines whether an object is equal to the current instance.</summary>
		/// <returns>true if the two objects are equal; otherwise, false.</returns>
		/// <param name="other">The object to compare with the current instance.</param>
		/// <param name="comparer">An object that determines whether the current instance and <paramref name="other" /> are equal. </param>
		// Token: 0x060007F7 RID: 2039 RVA: 0x0002C418 File Offset: 0x0002A618
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			Array array = other as Array;
			if (array == null || array.Length != this.Length)
			{
				return false;
			}
			for (int i = 0; i < array.Length; i++)
			{
				object value = this.GetValue(i);
				object value2 = array.GetValue(i);
				if (!comparer.Equals(value, value2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x000224D3 File Offset: 0x000206D3
		internal static int CombineHashCodes(int h1, int h2)
		{
			return ((h1 << 5) + h1) ^ h2;
		}

		/// <summary>Returns a hash code for the current instance.</summary>
		/// <returns>The hash code for the current instance.</returns>
		/// <param name="comparer">An object that computes the hash code of the current object.</param>
		// Token: 0x060007F9 RID: 2041 RVA: 0x0002C478 File Offset: 0x0002A678
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			int num = 0;
			for (int i = ((this.Length >= 8) ? (this.Length - 8) : 0); i < this.Length; i++)
			{
				num = Array.CombineHashCodes(num, comparer.GetHashCode(this.GetValue(i)));
			}
			return num;
		}

		/// <summary>Searches an entire one-dimensional sorted <see cref="T:System.Array" /> for a specific element, using the <see cref="T:System.IComparable" /> interface implemented by each element of the <see cref="T:System.Array" /> and by the specified object.</summary>
		/// <returns>The index of the specified <paramref name="value" /> in the specified <paramref name="array" />, if <paramref name="value" /> is found. If <paramref name="value" /> is not found and <paramref name="value" /> is less than one or more elements in <paramref name="array" />, a negative number which is the bitwise complement of the index of the first element that is larger than <paramref name="value" />. If <paramref name="value" /> is not found and <paramref name="value" /> is greater than any of the elements in <paramref name="array" />, a negative number which is the bitwise complement of (the index of the last element plus 1).</returns>
		/// <param name="array">The sorted one-dimensional <see cref="T:System.Array" /> to search.</param>
		/// <param name="value">The object to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is of a type that is not compatible with the elements of <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IComparable" /> interface, and the search encounters an element that does not implement the <see cref="T:System.IComparable" /> interface.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060007FA RID: 2042 RVA: 0x0002C4CE File Offset: 0x0002A6CE
		public static int BinarySearch(Array array, object value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.BinarySearch(array, array.GetLowerBound(0), array.Length, value, null);
		}

		/// <summary>Converts an array of one type to an array of another type.</summary>
		/// <returns>An array of the target type containing the converted elements from the source array.</returns>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to convert to a target type.</param>
		/// <param name="converter">A <see cref="T:System.Converter`2" /> that converts each element from one type to another type.</param>
		/// <typeparam name="TInput">The type of the elements of the source array.</typeparam>
		/// <typeparam name="TOutput">The type of the elements of the target array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.-or-<paramref name="converter" /> is null.</exception>
		// Token: 0x060007FB RID: 2043 RVA: 0x0002C4F4 File Offset: 0x0002A6F4
		public static TOutput[] ConvertAll<TInput, TOutput>(TInput[] array, Converter<TInput, TOutput> converter)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (converter == null)
			{
				throw new ArgumentNullException("converter");
			}
			TOutput[] array2 = new TOutput[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = converter(array[i]);
			}
			return array2;
		}

		/// <summary>Copies a range of elements from an <see cref="T:System.Array" /> starting at the first element and pastes them into another <see cref="T:System.Array" /> starting at the first element. The length is specified as a 64-bit integer.</summary>
		/// <param name="sourceArray">The <see cref="T:System.Array" /> that contains the data to copy.</param>
		/// <param name="destinationArray">The <see cref="T:System.Array" /> that receives the data.</param>
		/// <param name="length">A 64-bit integer that represents the number of elements to copy. The integer must be between zero and <see cref="F:System.Int32.MaxValue" />, inclusive.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceArray" /> is null.-or-<paramref name="destinationArray" /> is null.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="sourceArray" /> and <paramref name="destinationArray" /> have different ranks.</exception>
		/// <exception cref="T:System.ArrayTypeMismatchException">
		///   <paramref name="sourceArray" /> and <paramref name="destinationArray" /> are of incompatible types.</exception>
		/// <exception cref="T:System.InvalidCastException">At least one element in <paramref name="sourceArray" /> cannot be cast to the type of <paramref name="destinationArray" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="length" /> is less than 0 or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="length" /> is greater than the number of elements in <paramref name="sourceArray" />.-or-<paramref name="length" /> is greater than the number of elements in <paramref name="destinationArray" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060007FC RID: 2044 RVA: 0x0002C549 File Offset: 0x0002A749
		public static void Copy(Array sourceArray, Array destinationArray, long length)
		{
			if (length > 2147483647L || length < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("length", "Arrays larger than 2GB are not supported.");
			}
			Array.Copy(sourceArray, destinationArray, (int)length);
		}

		/// <summary>Copies a range of elements from an <see cref="T:System.Array" /> starting at the specified source index and pastes them to another <see cref="T:System.Array" /> starting at the specified destination index. The length and the indexes are specified as 64-bit integers.</summary>
		/// <param name="sourceArray">The <see cref="T:System.Array" /> that contains the data to copy.</param>
		/// <param name="sourceIndex">A 64-bit integer that represents the index in the <paramref name="sourceArray" /> at which copying begins.</param>
		/// <param name="destinationArray">The <see cref="T:System.Array" /> that receives the data.</param>
		/// <param name="destinationIndex">A 64-bit integer that represents the index in the <paramref name="destinationArray" /> at which storing begins.</param>
		/// <param name="length">A 64-bit integer that represents the number of elements to copy. The integer must be between zero and <see cref="F:System.Int32.MaxValue" />, inclusive.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceArray" /> is null.-or-<paramref name="destinationArray" /> is null.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="sourceArray" /> and <paramref name="destinationArray" /> have different ranks.</exception>
		/// <exception cref="T:System.ArrayTypeMismatchException">
		///   <paramref name="sourceArray" /> and <paramref name="destinationArray" /> are of incompatible types.</exception>
		/// <exception cref="T:System.InvalidCastException">At least one element in <paramref name="sourceArray" /> cannot be cast to the type of <paramref name="destinationArray" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="sourceIndex" /> is outside the range of valid indexes for the <paramref name="sourceArray" />.-or-<paramref name="destinationIndex" /> is outside the range of valid indexes for the <paramref name="destinationArray" />.-or-<paramref name="length" /> is less than 0 or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="length" /> is greater than the number of elements from <paramref name="sourceIndex" /> to the end of <paramref name="sourceArray" />.-or-<paramref name="length" /> is greater than the number of elements from <paramref name="destinationIndex" /> to the end of <paramref name="destinationArray" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060007FD RID: 2045 RVA: 0x0002C578 File Offset: 0x0002A778
		public static void Copy(Array sourceArray, long sourceIndex, Array destinationArray, long destinationIndex, long length)
		{
			if (sourceIndex > 2147483647L || sourceIndex < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", "Arrays larger than 2GB are not supported.");
			}
			if (destinationIndex > 2147483647L || destinationIndex < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("destinationIndex", "Arrays larger than 2GB are not supported.");
			}
			if (length > 2147483647L || length < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("length", "Arrays larger than 2GB are not supported.");
			}
			Array.Copy(sourceArray, (int)sourceIndex, destinationArray, (int)destinationIndex, (int)length);
		}

		/// <summary>Copies all the elements of the current one-dimensional <see cref="T:System.Array" /> to the specified one-dimensional <see cref="T:System.Array" /> starting at the specified destination <see cref="T:System.Array" /> index. The index is specified as a 64-bit integer.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the current <see cref="T:System.Array" />.</param>
		/// <param name="index">A 64-bit integer that represents the index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for <paramref name="array" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-The number of elements in the source <see cref="T:System.Array" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.ArrayTypeMismatchException">The type of the source <see cref="T:System.Array" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.RankException">The source <see cref="T:System.Array" /> is multidimensional.</exception>
		/// <exception cref="T:System.InvalidCastException">At least one element in the source <see cref="T:System.Array" /> cannot be cast to the type of destination <paramref name="array" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060007FE RID: 2046 RVA: 0x0002C5FB File Offset: 0x0002A7FB
		public void CopyTo(Array array, long index)
		{
			if (index > 2147483647L || index < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index", "Arrays larger than 2GB are not supported.");
			}
			this.CopyTo(array, (int)index);
		}

		/// <summary>Performs the specified action on each element of the specified array.</summary>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> on whose elements the action is to be performed.</param>
		/// <param name="action">The <see cref="T:System.Action`1" /> to perform on each element of <paramref name="array" />.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.-or-<paramref name="action" /> is null.</exception>
		// Token: 0x060007FF RID: 2047 RVA: 0x0002C628 File Offset: 0x0002A828
		public static void ForEach<T>(T[] array, Action<T> action)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			for (int i = 0; i < array.Length; i++)
			{
				action(array[i]);
			}
		}

		/// <summary>Gets a 64-bit integer that represents the total number of elements in all the dimensions of the <see cref="T:System.Array" />.</summary>
		/// <returns>A 64-bit integer that represents the total number of elements in all the dimensions of the <see cref="T:System.Array" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x0002C66C File Offset: 0x0002A86C
		public long LongLength
		{
			get
			{
				long num = (long)this.GetLength(0);
				for (int i = 1; i < this.Rank; i++)
				{
					num *= (long)this.GetLength(i);
				}
				return num;
			}
		}

		/// <summary>Gets a 64-bit integer that represents the number of elements in the specified dimension of the <see cref="T:System.Array" />.</summary>
		/// <returns>A 64-bit integer that represents the number of elements in the specified dimension.</returns>
		/// <param name="dimension">A zero-based dimension of the <see cref="T:System.Array" /> whose length needs to be determined.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="dimension" /> is less than zero.-or-<paramref name="dimension" /> is equal to or greater than <see cref="P:System.Array.Rank" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000801 RID: 2049 RVA: 0x0002C69F File Offset: 0x0002A89F
		public long GetLongLength(int dimension)
		{
			return (long)this.GetLength(dimension);
		}

		/// <summary>Gets the value at the specified position in the one-dimensional <see cref="T:System.Array" />. The index is specified as a 64-bit integer.</summary>
		/// <returns>The value at the specified position in the one-dimensional <see cref="T:System.Array" />.</returns>
		/// <param name="index">A 64-bit integer that represents the position of the <see cref="T:System.Array" /> element to get.</param>
		/// <exception cref="T:System.ArgumentException">The current <see cref="T:System.Array" /> does not have exactly one dimension.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the current <see cref="T:System.Array" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000802 RID: 2050 RVA: 0x0002C6A9 File Offset: 0x0002A8A9
		public object GetValue(long index)
		{
			if (index > 2147483647L || index < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index", "Arrays larger than 2GB are not supported.");
			}
			return this.GetValue((int)index);
		}

		/// <summary>Gets the value at the specified position in the two-dimensional <see cref="T:System.Array" />. The indexes are specified as 64-bit integers.</summary>
		/// <returns>The value at the specified position in the two-dimensional <see cref="T:System.Array" />.</returns>
		/// <param name="index1">A 64-bit integer that represents the first-dimension index of the <see cref="T:System.Array" /> element to get.</param>
		/// <param name="index2">A 64-bit integer that represents the second-dimension index of the <see cref="T:System.Array" /> element to get.</param>
		/// <exception cref="T:System.ArgumentException">The current <see cref="T:System.Array" /> does not have exactly two dimensions.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Either <paramref name="index1" /> or <paramref name="index2" /> is outside the range of valid indexes for the corresponding dimension of the current <see cref="T:System.Array" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000803 RID: 2051 RVA: 0x0002C6D8 File Offset: 0x0002A8D8
		public object GetValue(long index1, long index2)
		{
			if (index1 > 2147483647L || index1 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index1", "Arrays larger than 2GB are not supported.");
			}
			if (index2 > 2147483647L || index2 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index2", "Arrays larger than 2GB are not supported.");
			}
			return this.GetValue((int)index1, (int)index2);
		}

		/// <summary>Gets the value at the specified position in the three-dimensional <see cref="T:System.Array" />. The indexes are specified as 64-bit integers.</summary>
		/// <returns>The value at the specified position in the three-dimensional <see cref="T:System.Array" />.</returns>
		/// <param name="index1">A 64-bit integer that represents the first-dimension index of the <see cref="T:System.Array" /> element to get.</param>
		/// <param name="index2">A 64-bit integer that represents the second-dimension index of the <see cref="T:System.Array" /> element to get.</param>
		/// <param name="index3">A 64-bit integer that represents the third-dimension index of the <see cref="T:System.Array" /> element to get.</param>
		/// <exception cref="T:System.ArgumentException">The current <see cref="T:System.Array" /> does not have exactly three dimensions.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index1" /> or <paramref name="index2" /> or <paramref name="index3" /> is outside the range of valid indexes for the corresponding dimension of the current <see cref="T:System.Array" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000804 RID: 2052 RVA: 0x0002C734 File Offset: 0x0002A934
		public object GetValue(long index1, long index2, long index3)
		{
			if (index1 > 2147483647L || index1 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index1", "Arrays larger than 2GB are not supported.");
			}
			if (index2 > 2147483647L || index2 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index2", "Arrays larger than 2GB are not supported.");
			}
			if (index3 > 2147483647L || index3 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index3", "Arrays larger than 2GB are not supported.");
			}
			return this.GetValue((int)index1, (int)index2, (int)index3);
		}

		/// <summary>Gets the value at the specified position in the multidimensional <see cref="T:System.Array" />. The indexes are specified as an array of 64-bit integers.</summary>
		/// <returns>The value at the specified position in the multidimensional <see cref="T:System.Array" />.</returns>
		/// <param name="indices">A one-dimensional array of 64-bit integers that represent the indexes specifying the position of the <see cref="T:System.Array" /> element to get.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="indices" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The number of dimensions in the current <see cref="T:System.Array" /> is not equal to the number of elements in <paramref name="indices" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Any element in <paramref name="indices" /> is outside the range of valid indexes for the corresponding dimension of the current <see cref="T:System.Array" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000805 RID: 2053 RVA: 0x0002C7B4 File Offset: 0x0002A9B4
		public object GetValue(params long[] indices)
		{
			if (indices == null)
			{
				throw new ArgumentNullException("indices");
			}
			if (this.Rank != indices.Length)
			{
				throw new ArgumentException("Indices length does not match the array rank.");
			}
			int[] array = new int[indices.Length];
			for (int i = 0; i < indices.Length; i++)
			{
				long num = indices[i];
				if (num > 2147483647L || num < -2147483648L)
				{
					throw new ArgumentOutOfRangeException("index", "Arrays larger than 2GB are not supported.");
				}
				array[i] = (int)num;
			}
			return this.GetValue(array);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Array" /> has a fixed size.</summary>
		/// <returns>This property is always true for all arrays.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x00003B29 File Offset: 0x00001D29
		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Array" /> is read-only.</summary>
		/// <returns>This property is always false for all arrays.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Array" /> is synchronized (thread safe).</summary>
		/// <returns>This property is always false for all arrays.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Array" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Array" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x00002119 File Offset: 0x00000319
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Searches a range of elements in a one-dimensional sorted <see cref="T:System.Array" /> for a value, using the <see cref="T:System.IComparable" /> interface implemented by each element of the <see cref="T:System.Array" /> and by the specified value.</summary>
		/// <returns>The index of the specified <paramref name="value" /> in the specified <paramref name="array" />, if <paramref name="value" /> is found. If <paramref name="value" /> is not found and <paramref name="value" /> is less than one or more elements in <paramref name="array" />, a negative number which is the bitwise complement of the index of the first element that is larger than <paramref name="value" />. If <paramref name="value" /> is not found and <paramref name="value" /> is greater than any of the elements in <paramref name="array" />, a negative number which is the bitwise complement of (the index of the last element plus 1).</returns>
		/// <param name="array">The sorted one-dimensional <see cref="T:System.Array" /> to search.</param>
		/// <param name="index">The starting index of the range to search.</param>
		/// <param name="length">The length of the range to search.</param>
		/// <param name="value">The object to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />.-or-<paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in <paramref name="array" />.-or-<paramref name="value" /> is of a type that is not compatible with the elements of <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IComparable" /> interface, and the search encounters an element that does not implement the <see cref="T:System.IComparable" /> interface.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600080A RID: 2058 RVA: 0x0002C82E File Offset: 0x0002AA2E
		public static int BinarySearch(Array array, int index, int length, object value)
		{
			return Array.BinarySearch(array, index, length, value, null);
		}

		/// <summary>Searches an entire one-dimensional sorted <see cref="T:System.Array" /> for a value using the specified <see cref="T:System.Collections.IComparer" /> interface.</summary>
		/// <returns>The index of the specified <paramref name="value" /> in the specified <paramref name="array" />, if <paramref name="value" /> is found. If <paramref name="value" /> is not found and <paramref name="value" /> is less than one or more elements in <paramref name="array" />, a negative number which is the bitwise complement of the index of the first element that is larger than <paramref name="value" />. If <paramref name="value" /> is not found and <paramref name="value" /> is greater than any of the elements in <paramref name="array" />, a negative number which is the bitwise complement of (the index of the last element plus 1).</returns>
		/// <param name="array">The sorted one-dimensional <see cref="T:System.Array" /> to search.</param>
		/// <param name="value">The object to search for.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing elements.-or- null to use the <see cref="T:System.IComparable" /> implementation of each element.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparer" /> is null, and <paramref name="value" /> is of a type that is not compatible with the elements of <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is null, <paramref name="value" /> does not implement the <see cref="T:System.IComparable" /> interface, and the search encounters an element that does not implement the <see cref="T:System.IComparable" /> interface.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600080B RID: 2059 RVA: 0x0002C83A File Offset: 0x0002AA3A
		public static int BinarySearch(Array array, object value, IComparer comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.BinarySearch(array, array.GetLowerBound(0), array.Length, value, comparer);
		}

		/// <summary>Searches a range of elements in a one-dimensional sorted <see cref="T:System.Array" /> for a value, using the specified <see cref="T:System.Collections.IComparer" /> interface.</summary>
		/// <returns>The index of the specified <paramref name="value" /> in the specified <paramref name="array" />, if <paramref name="value" /> is found. If <paramref name="value" /> is not found and <paramref name="value" /> is less than one or more elements in <paramref name="array" />, a negative number which is the bitwise complement of the index of the first element that is larger than <paramref name="value" />. If <paramref name="value" /> is not found and <paramref name="value" /> is greater than any of the elements in <paramref name="array" />, a negative number which is the bitwise complement of (the index of the last element plus 1).</returns>
		/// <param name="array">The sorted one-dimensional <see cref="T:System.Array" /> to search.</param>
		/// <param name="index">The starting index of the range to search.</param>
		/// <param name="length">The length of the range to search.</param>
		/// <param name="value">The object to search for.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing elements.-or- null to use the <see cref="T:System.IComparable" /> implementation of each element.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />.-or-<paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in <paramref name="array" />.-or-<paramref name="comparer" /> is null, and <paramref name="value" /> is of a type that is not compatible with the elements of <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is null, <paramref name="value" /> does not implement the <see cref="T:System.IComparable" /> interface, and the search encounters an element that does not implement the <see cref="T:System.IComparable" /> interface.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600080C RID: 2060 RVA: 0x0002C860 File Offset: 0x0002AA60
		public static int BinarySearch(Array array, int index, int length, object value, IComparer comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || length < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "length", "Non-negative number required.");
			}
			if (array.Length - index < length)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (array.Rank != 1)
			{
				throw new RankException("Only single dimension arrays are supported here.");
			}
			if (comparer == null)
			{
				comparer = LowLevelComparer.Default;
			}
			int i = index;
			int num = index + length - 1;
			object[] array2 = array as object[];
			if (array2 != null)
			{
				while (i <= num)
				{
					int median = Array.GetMedian(i, num);
					int num2;
					try
					{
						num2 = comparer.Compare(array2[median], value);
					}
					catch (Exception ex)
					{
						throw new InvalidOperationException("Failed to compare two elements in the array.", ex);
					}
					if (num2 == 0)
					{
						return median;
					}
					if (num2 < 0)
					{
						i = median + 1;
					}
					else
					{
						num = median - 1;
					}
				}
			}
			else
			{
				while (i <= num)
				{
					int median2 = Array.GetMedian(i, num);
					int num3;
					try
					{
						num3 = comparer.Compare(array.GetValue(median2), value);
					}
					catch (Exception ex2)
					{
						throw new InvalidOperationException("Failed to compare two elements in the array.", ex2);
					}
					if (num3 == 0)
					{
						return median2;
					}
					if (num3 < 0)
					{
						i = median2 + 1;
					}
					else
					{
						num = median2 - 1;
					}
				}
			}
			return ~i;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0002C994 File Offset: 0x0002AB94
		private static int GetMedian(int low, int hi)
		{
			return low + (hi - low >> 1);
		}

		/// <summary>Searches an entire one-dimensional sorted <see cref="T:System.Array" /> for a specific element, using the <see cref="T:System.IComparable`1" /> generic interface implemented by each element of the <see cref="T:System.Array" /> and by the specified object.</summary>
		/// <returns>The index of the specified <paramref name="value" /> in the specified <paramref name="array" />, if <paramref name="value" /> is found. If <paramref name="value" /> is not found and <paramref name="value" /> is less than one or more elements in <paramref name="array" />, a negative number which is the bitwise complement of the index of the first element that is larger than <paramref name="value" />. If <paramref name="value" /> is not found and <paramref name="value" /> is greater than any of the elements in <paramref name="array" />, a negative number which is the bitwise complement of (the index of the last element plus 1).</returns>
		/// <param name="array">The sorted one-dimensional, zero-based <see cref="T:System.Array" /> to search. </param>
		/// <param name="value">The object to search for.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IComparable`1" /> generic interface, and the search encounters an element that does not implement the <see cref="T:System.IComparable`1" /> generic interface.</exception>
		// Token: 0x0600080E RID: 2062 RVA: 0x0002C99D File Offset: 0x0002AB9D
		public static int BinarySearch<T>(T[] array, T value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.BinarySearch<T>(array, 0, array.Length, value, null);
		}

		/// <summary>Searches an entire one-dimensional sorted <see cref="T:System.Array" /> for a value using the specified <see cref="T:System.Collections.Generic.IComparer`1" /> generic interface.</summary>
		/// <returns>The index of the specified <paramref name="value" /> in the specified <paramref name="array" />, if <paramref name="value" /> is found. If <paramref name="value" /> is not found and <paramref name="value" /> is less than one or more elements in <paramref name="array" />, a negative number which is the bitwise complement of the index of the first element that is larger than <paramref name="value" />. If <paramref name="value" /> is not found and <paramref name="value" /> is greater than any of the elements in <paramref name="array" />, a negative number which is the bitwise complement of (the index of the last element plus 1).</returns>
		/// <param name="array">The sorted one-dimensional, zero-based <see cref="T:System.Array" /> to search.  </param>
		/// <param name="value">The object to search for.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing elements.-or- null to use the <see cref="T:System.IComparable`1" /> implementation of each element.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparer" /> is null, and <paramref name="value" /> is of a type that is not compatible with the elements of <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is null, <paramref name="value" /> does not implement the <see cref="T:System.IComparable`1" /> generic interface, and the search encounters an element that does not implement the <see cref="T:System.IComparable`1" /> generic interface.</exception>
		// Token: 0x0600080F RID: 2063 RVA: 0x0002C9B9 File Offset: 0x0002ABB9
		public static int BinarySearch<T>(T[] array, T value, IComparer<T> comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.BinarySearch<T>(array, 0, array.Length, value, comparer);
		}

		/// <summary>Searches a range of elements in a one-dimensional sorted <see cref="T:System.Array" /> for a value, using the <see cref="T:System.IComparable`1" /> generic interface implemented by each element of the <see cref="T:System.Array" /> and by the specified value.</summary>
		/// <returns>The index of the specified <paramref name="value" /> in the specified <paramref name="array" />, if <paramref name="value" /> is found. If <paramref name="value" /> is not found and <paramref name="value" /> is less than one or more elements in <paramref name="array" />, a negative number which is the bitwise complement of the index of the first element that is larger than <paramref name="value" />. If <paramref name="value" /> is not found and <paramref name="value" /> is greater than any of the elements in <paramref name="array" />, a negative number which is the bitwise complement of (the index of the last element plus 1).</returns>
		/// <param name="array">The sorted one-dimensional, zero-based <see cref="T:System.Array" /> to search. </param>
		/// <param name="index">The starting index of the range to search.</param>
		/// <param name="length">The length of the range to search.</param>
		/// <param name="value">The object to search for.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />.-or-<paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in <paramref name="array" />.-or-<paramref name="value" /> is of a type that is not compatible with the elements of <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IComparable`1" /> generic interface, and the search encounters an element that does not implement the <see cref="T:System.IComparable`1" /> generic interface.</exception>
		// Token: 0x06000810 RID: 2064 RVA: 0x0002C9D5 File Offset: 0x0002ABD5
		public static int BinarySearch<T>(T[] array, int index, int length, T value)
		{
			return Array.BinarySearch<T>(array, index, length, value, null);
		}

		/// <summary>Searches a range of elements in a one-dimensional sorted <see cref="T:System.Array" /> for a value, using the specified <see cref="T:System.Collections.Generic.IComparer`1" /> generic interface.</summary>
		/// <returns>The index of the specified <paramref name="value" /> in the specified <paramref name="array" />, if <paramref name="value" /> is found. If <paramref name="value" /> is not found and <paramref name="value" /> is less than one or more elements in <paramref name="array" />, a negative number which is the bitwise complement of the index of the first element that is larger than <paramref name="value" />. If <paramref name="value" /> is not found and <paramref name="value" /> is greater than any of the elements in <paramref name="array" />, a negative number which is the bitwise complement of (the index of the last element plus 1).</returns>
		/// <param name="array">The sorted one-dimensional, zero-based <see cref="T:System.Array" /> to search. </param>
		/// <param name="index">The starting index of the range to search.</param>
		/// <param name="length">The length of the range to search.</param>
		/// <param name="value">The object to search for.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing elements.-or- null to use the <see cref="T:System.IComparable`1" /> implementation of each element.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />.-or-<paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in <paramref name="array" />.-or-<paramref name="comparer" /> is null, and <paramref name="value" /> is of a type that is not compatible with the elements of <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is null, <paramref name="value" /> does not implement the <see cref="T:System.IComparable`1" /> generic interface, and the search encounters an element that does not implement the <see cref="T:System.IComparable`1" /> generic interface.</exception>
		// Token: 0x06000811 RID: 2065 RVA: 0x0002C9E4 File Offset: 0x0002ABE4
		public static int BinarySearch<T>(T[] array, int index, int length, T value, IComparer<T> comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || length < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "length", "Non-negative number required.");
			}
			if (array.Length - index < length)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			return ArraySortHelper<T>.BinarySearch(array, index, length, value, comparer);
		}

		/// <summary>Searches for the specified object and returns the index of the first occurrence within the entire one-dimensional <see cref="T:System.Array" />.</summary>
		/// <returns>The index of the first occurrence of <paramref name="value" /> within the entire <paramref name="array" />, if found; otherwise, the lower bound of the array minus 1.</returns>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> to search.</param>
		/// <param name="value">The object to locate in <paramref name="array" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000812 RID: 2066 RVA: 0x0002CA40 File Offset: 0x0002AC40
		public static int IndexOf(Array array, object value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.IndexOf(array, value, array.GetLowerBound(0), array.Length);
		}

		/// <summary>Searches for the specified object and returns the index of the first occurrence within the range of elements in the one-dimensional <see cref="T:System.Array" /> that extends from the specified index to the last element.</summary>
		/// <returns>The index of the first occurrence of <paramref name="value" /> within the range of elements in <paramref name="array" /> that extends from <paramref name="startIndex" /> to the last element, if found; otherwise, the lower bound of the array minus 1.</returns>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> to search.</param>
		/// <param name="value">The object to locate in <paramref name="array" />.</param>
		/// <param name="startIndex">The starting index of the search. 0 (zero) is valid in an empty array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="array" />.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000813 RID: 2067 RVA: 0x0002CA64 File Offset: 0x0002AC64
		public static int IndexOf(Array array, object value, int startIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int lowerBound = array.GetLowerBound(0);
			return Array.IndexOf(array, value, startIndex, array.Length - startIndex + lowerBound);
		}

		/// <summary>Searches for the specified object and returns the index of the first occurrence within the range of elements in the one-dimensional <see cref="T:System.Array" /> that starts at the specified index and contains the specified number of elements.</summary>
		/// <returns>The index of the first occurrence of <paramref name="value" /> within the range of elements in <paramref name="array" /> that starts at <paramref name="startIndex" /> and contains the number of elements specified in <paramref name="count" />, if found; otherwise, the lower bound of the array minus 1.</returns>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> to search.</param>
		/// <param name="value">The object to locate in <paramref name="array" />.</param>
		/// <param name="startIndex">The starting index of the search. 0 (zero) is valid in an empty array.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="array" />.-or-<paramref name="count" /> is less than zero.-or-<paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="array" />.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000814 RID: 2068 RVA: 0x0002CA9C File Offset: 0x0002AC9C
		public static int IndexOf(Array array, object value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new RankException("Only single dimension arrays are supported here.");
			}
			int lowerBound = array.GetLowerBound(0);
			if (startIndex < lowerBound || startIndex > array.Length + lowerBound)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0 || count > array.Length - startIndex + lowerBound)
			{
				throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
			}
			object[] array2 = array as object[];
			int num = startIndex + count;
			if (array2 != null)
			{
				if (value == null)
				{
					for (int i = startIndex; i < num; i++)
					{
						if (array2[i] == null)
						{
							return i;
						}
					}
				}
				else
				{
					for (int j = startIndex; j < num; j++)
					{
						object obj = array2[j];
						if (obj != null && obj.Equals(value))
						{
							return j;
						}
					}
				}
			}
			else
			{
				for (int k = startIndex; k < num; k++)
				{
					object value2 = array.GetValue(k);
					if (value2 == null)
					{
						if (value == null)
						{
							return k;
						}
					}
					else if (value2.Equals(value))
					{
						return k;
					}
				}
			}
			return lowerBound - 1;
		}

		/// <summary>Searches for the specified object and returns the index of the first occurrence within the entire <see cref="T:System.Array" />.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the entire <paramref name="array" />, if found; otherwise, –1.</returns>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="value">The object to locate in <paramref name="array" />.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		// Token: 0x06000815 RID: 2069 RVA: 0x0002CB96 File Offset: 0x0002AD96
		public static int IndexOf<T>(T[] array, T value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.IndexOfImpl<T>(array, value, 0, array.Length);
		}

		/// <summary>Searches for the specified object and returns the index of the first occurrence within the range of elements in the <see cref="T:System.Array" /> that extends from the specified index to the last element.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the range of elements in <paramref name="array" /> that extends from <paramref name="startIndex" /> to the last element, if found; otherwise, –1.</returns>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="value">The object to locate in <paramref name="array" />.</param>
		/// <param name="startIndex">The zero-based starting index of the search. 0 (zero) is valid in an empty array.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="array" />.</exception>
		// Token: 0x06000816 RID: 2070 RVA: 0x0002CBB1 File Offset: 0x0002ADB1
		public static int IndexOf<T>(T[] array, T value, int startIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.IndexOf<T>(array, value, startIndex, array.Length - startIndex);
		}

		/// <summary>Searches for the specified object and returns the index of the first occurrence within the range of elements in the <see cref="T:System.Array" /> that starts at the specified index and contains the specified number of elements.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the range of elements in <paramref name="array" /> that starts at <paramref name="startIndex" /> and contains the number of elements specified in <paramref name="count" />, if found; otherwise, –1.</returns>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="value">The object to locate in <paramref name="array" />.</param>
		/// <param name="startIndex">The zero-based starting index of the search. 0 (zero) is valid in an empty array.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="array" />.-or-<paramref name="count" /> is less than zero.-or-<paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="array" />.</exception>
		// Token: 0x06000817 RID: 2071 RVA: 0x0002CBD0 File Offset: 0x0002ADD0
		public static int IndexOf<T>(T[] array, T value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (startIndex < 0 || startIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0 || count > array.Length - startIndex)
			{
				throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
			}
			return Array.IndexOfImpl<T>(array, value, startIndex, count);
		}

		/// <summary>Searches for the specified object and returns the index of the last occurrence within the entire one-dimensional <see cref="T:System.Array" />.</summary>
		/// <returns>The index of the last occurrence of <paramref name="value" /> within the entire <paramref name="array" />, if found; otherwise, the lower bound of the array minus 1.</returns>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> to search.</param>
		/// <param name="value">The object to locate in <paramref name="array" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000818 RID: 2072 RVA: 0x0002CC2A File Offset: 0x0002AE2A
		public static int LastIndexOf(Array array, object value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.LastIndexOf(array, value, array.Length - 1, array.Length);
		}

		/// <summary>Searches for the specified object and returns the index of the last occurrence within the range of elements in the one-dimensional <see cref="T:System.Array" /> that extends from the first element to the specified index.</summary>
		/// <returns>The index of the last occurrence of <paramref name="value" /> within the range of elements in <paramref name="array" /> that extends from the first element to <paramref name="startIndex" />, if found; otherwise, the lower bound of the array minus 1.</returns>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> to search.</param>
		/// <param name="value">The object to locate in <paramref name="array" />.</param>
		/// <param name="startIndex">The starting index of the backward search.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="array" />.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000819 RID: 2073 RVA: 0x0002CC4F File Offset: 0x0002AE4F
		public static int LastIndexOf(Array array, object value, int startIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.LastIndexOf(array, value, startIndex, startIndex + 1);
		}

		/// <summary>Searches for the specified object and returns the index of the last occurrence within the range of elements in the one-dimensional <see cref="T:System.Array" /> that contains the specified number of elements and ends at the specified index.</summary>
		/// <returns>The index of the last occurrence of <paramref name="value" /> within the range of elements in <paramref name="array" /> that contains the number of elements specified in <paramref name="count" /> and ends at <paramref name="startIndex" />, if found; otherwise, the lower bound of the array minus 1.</returns>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> to search.</param>
		/// <param name="value">The object to locate in <paramref name="array" />.</param>
		/// <param name="startIndex">The starting index of the backward search.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="array" />.-or-<paramref name="count" /> is less than zero.-or-<paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="array" />.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600081A RID: 2074 RVA: 0x0002CC6C File Offset: 0x0002AE6C
		public static int LastIndexOf(Array array, object value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Length == 0)
			{
				return -1;
			}
			if (startIndex < 0 || startIndex >= array.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
			}
			if (count > startIndex + 1)
			{
				throw new ArgumentOutOfRangeException("endIndex", "endIndex cannot be greater than startIndex.");
			}
			if (array.Rank != 1)
			{
				throw new RankException("Only single dimension arrays are supported here.");
			}
			object[] array2 = array as object[];
			int num = startIndex - count + 1;
			if (array2 != null)
			{
				if (value == null)
				{
					for (int i = startIndex; i >= num; i--)
					{
						if (array2[i] == null)
						{
							return i;
						}
					}
				}
				else
				{
					for (int j = startIndex; j >= num; j--)
					{
						object obj = array2[j];
						if (obj != null && obj.Equals(value))
						{
							return j;
						}
					}
				}
			}
			else
			{
				for (int k = startIndex; k >= num; k--)
				{
					object value2 = array.GetValue(k);
					if (value2 == null)
					{
						if (value == null)
						{
							return k;
						}
					}
					else if (value2.Equals(value))
					{
						return k;
					}
				}
			}
			return -1;
		}

		/// <summary>Searches for the specified object and returns the index of the last occurrence within the entire <see cref="T:System.Array" />.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the entire <paramref name="array" />, if found; otherwise, –1.</returns>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="value">The object to locate in <paramref name="array" />.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		// Token: 0x0600081B RID: 2075 RVA: 0x0002CD69 File Offset: 0x0002AF69
		public static int LastIndexOf<T>(T[] array, T value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.LastIndexOf<T>(array, value, array.Length - 1, array.Length);
		}

		/// <summary>Searches for the specified object and returns the index of the last occurrence within the range of elements in the <see cref="T:System.Array" /> that extends from the first element to the specified index.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the range of elements in <paramref name="array" /> that extends from the first element to <paramref name="startIndex" />, if found; otherwise, –1.</returns>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="value">The object to locate in <paramref name="array" />.</param>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="array" />.</exception>
		// Token: 0x0600081C RID: 2076 RVA: 0x0002CD88 File Offset: 0x0002AF88
		public static int LastIndexOf<T>(T[] array, T value, int startIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.LastIndexOf<T>(array, value, startIndex, (array.Length == 0) ? 0 : (startIndex + 1));
		}

		/// <summary>Searches for the specified object and returns the index of the last occurrence within the range of elements in the <see cref="T:System.Array" /> that contains the specified number of elements and ends at the specified index.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the range of elements in <paramref name="array" /> that contains the number of elements specified in <paramref name="count" /> and ends at <paramref name="startIndex" />, if found; otherwise, –1.</returns>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="value">The object to locate in <paramref name="array" />.</param>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="array" />.-or-<paramref name="count" /> is less than zero.-or-<paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="array" />.</exception>
		// Token: 0x0600081D RID: 2077 RVA: 0x0002CDAC File Offset: 0x0002AFAC
		public static int LastIndexOf<T>(T[] array, T value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Length == 0)
			{
				if (startIndex != -1 && startIndex != 0)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (count != 0)
				{
					throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
				}
				return -1;
			}
			else
			{
				if (startIndex < 0 || startIndex >= array.Length)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (count < 0 || startIndex - count + 1 < 0)
				{
					throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
				}
				return Array.LastIndexOfImpl<T>(array, value, startIndex, count);
			}
		}

		/// <summary>Reverses the sequence of the elements in the entire one-dimensional <see cref="T:System.Array" />.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> to reverse.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600081E RID: 2078 RVA: 0x0002CE36 File Offset: 0x0002B036
		public static void Reverse(Array array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Reverse(array, array.GetLowerBound(0), array.Length);
		}

		/// <summary>Reverses the sequence of the elements in a range of elements in the one-dimensional <see cref="T:System.Array" />.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> to reverse.</param>
		/// <param name="index">The starting index of the section to reverse.</param>
		/// <param name="length">The number of elements in the section to reverse.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />.-or-<paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in <paramref name="array" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600081F RID: 2079 RVA: 0x0002CE5C File Offset: 0x0002B05C
		public static void Reverse(Array array, int index, int length)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int lowerBound = array.GetLowerBound(0);
			if (index < lowerBound || length < 0)
			{
				throw new ArgumentOutOfRangeException((index < lowerBound) ? "index" : "length", "Non-negative number required.");
			}
			if (array.Length - (index - lowerBound) < length)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (array.Rank != 1)
			{
				throw new RankException("Only single dimension arrays are supported here.");
			}
			int i = index;
			int num = index + length - 1;
			object[] array2 = array as object[];
			if (array2 != null)
			{
				while (i < num)
				{
					object obj = array2[i];
					array2[i] = array2[num];
					array2[num] = obj;
					i++;
					num--;
				}
				return;
			}
			while (i < num)
			{
				object value = array.GetValue(i);
				array.SetValue(array.GetValue(num), i);
				array.SetValue(value, num);
				i++;
				num--;
			}
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0002CF2B File Offset: 0x0002B12B
		public static void Reverse<T>(T[] array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Reverse<T>(array, 0, array.Length);
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0002CF48 File Offset: 0x0002B148
		public static void Reverse<T>(T[] array, int index, int length)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || length < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "length", "Non-negative number required.");
			}
			if (array.Length - index < length)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			int i = index;
			int num = index + length - 1;
			while (i < num)
			{
				T t = array[i];
				array[i] = array[num];
				array[num] = t;
				i++;
				num--;
			}
		}

		/// <summary>Sets a value to the element at the specified position in the one-dimensional <see cref="T:System.Array" />. The index is specified as a 64-bit integer.</summary>
		/// <param name="value">The new value for the specified element.</param>
		/// <param name="index">A 64-bit integer that represents the position of the <see cref="T:System.Array" /> element to set.</param>
		/// <exception cref="T:System.ArgumentException">The current <see cref="T:System.Array" /> does not have exactly one dimension.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> cannot be cast to the element type of the current <see cref="T:System.Array" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the current <see cref="T:System.Array" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000822 RID: 2082 RVA: 0x0002CFCD File Offset: 0x0002B1CD
		public void SetValue(object value, long index)
		{
			if (index > 2147483647L || index < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index", "Arrays larger than 2GB are not supported.");
			}
			this.SetValue(value, (int)index);
		}

		/// <summary>Sets a value to the element at the specified position in the two-dimensional <see cref="T:System.Array" />. The indexes are specified as 64-bit integers.</summary>
		/// <param name="value">The new value for the specified element.</param>
		/// <param name="index1">A 64-bit integer that represents the first-dimension index of the <see cref="T:System.Array" /> element to set.</param>
		/// <param name="index2">A 64-bit integer that represents the second-dimension index of the <see cref="T:System.Array" /> element to set.</param>
		/// <exception cref="T:System.ArgumentException">The current <see cref="T:System.Array" /> does not have exactly two dimensions.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> cannot be cast to the element type of the current <see cref="T:System.Array" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Either <paramref name="index1" /> or <paramref name="index2" /> is outside the range of valid indexes for the corresponding dimension of the current <see cref="T:System.Array" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000823 RID: 2083 RVA: 0x0002CFFC File Offset: 0x0002B1FC
		public void SetValue(object value, long index1, long index2)
		{
			if (index1 > 2147483647L || index1 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index1", "Arrays larger than 2GB are not supported.");
			}
			if (index2 > 2147483647L || index2 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index2", "Arrays larger than 2GB are not supported.");
			}
			this.SetValue(value, (int)index1, (int)index2);
		}

		/// <summary>Sets a value to the element at the specified position in the three-dimensional <see cref="T:System.Array" />. The indexes are specified as 64-bit integers.</summary>
		/// <param name="value">The new value for the specified element.</param>
		/// <param name="index1">A 64-bit integer that represents the first-dimension index of the <see cref="T:System.Array" /> element to set.</param>
		/// <param name="index2">A 64-bit integer that represents the second-dimension index of the <see cref="T:System.Array" /> element to set.</param>
		/// <param name="index3">A 64-bit integer that represents the third-dimension index of the <see cref="T:System.Array" /> element to set.</param>
		/// <exception cref="T:System.ArgumentException">The current <see cref="T:System.Array" /> does not have exactly three dimensions.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> cannot be cast to the element type of the current <see cref="T:System.Array" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index1" /> or <paramref name="index2" /> or <paramref name="index3" /> is outside the range of valid indexes for the corresponding dimension of the current <see cref="T:System.Array" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000824 RID: 2084 RVA: 0x0002D058 File Offset: 0x0002B258
		public void SetValue(object value, long index1, long index2, long index3)
		{
			if (index1 > 2147483647L || index1 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index1", "Arrays larger than 2GB are not supported.");
			}
			if (index2 > 2147483647L || index2 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index2", "Arrays larger than 2GB are not supported.");
			}
			if (index3 > 2147483647L || index3 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index3", "Arrays larger than 2GB are not supported.");
			}
			this.SetValue(value, (int)index1, (int)index2, (int)index3);
		}

		/// <summary>Sets a value to the element at the specified position in the multidimensional <see cref="T:System.Array" />. The indexes are specified as an array of 64-bit integers.</summary>
		/// <param name="value">The new value for the specified element.</param>
		/// <param name="indices">A one-dimensional array of 64-bit integers that represent the indexes specifying the position of the element to set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="indices" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The number of dimensions in the current <see cref="T:System.Array" /> is not equal to the number of elements in <paramref name="indices" />.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> cannot be cast to the element type of the current <see cref="T:System.Array" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Any element in <paramref name="indices" /> is outside the range of valid indexes for the corresponding dimension of the current <see cref="T:System.Array" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000825 RID: 2085 RVA: 0x0002D0DC File Offset: 0x0002B2DC
		public void SetValue(object value, params long[] indices)
		{
			if (indices == null)
			{
				throw new ArgumentNullException("indices");
			}
			if (this.Rank != indices.Length)
			{
				throw new ArgumentException("Indices length does not match the array rank.");
			}
			int[] array = new int[indices.Length];
			for (int i = 0; i < indices.Length; i++)
			{
				long num = indices[i];
				if (num > 2147483647L || num < -2147483648L)
				{
					throw new ArgumentOutOfRangeException("index", "Arrays larger than 2GB are not supported.");
				}
				array[i] = (int)num;
			}
			this.SetValue(value, array);
		}

		/// <summary>Sorts the elements in an entire one-dimensional <see cref="T:System.Array" /> using the <see cref="T:System.IComparable" /> implementation of each element of the <see cref="T:System.Array" />.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> to sort.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		/// <exception cref="T:System.InvalidOperationException">One or more elements in <paramref name="array" /> do not implement the <see cref="T:System.IComparable" /> interface.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000826 RID: 2086 RVA: 0x0002D157 File Offset: 0x0002B357
		public static void Sort(Array array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Sort(array, null, array.GetLowerBound(0), array.Length, null);
		}

		/// <summary>Sorts the elements in a range of elements in a one-dimensional <see cref="T:System.Array" /> using the <see cref="T:System.IComparable" /> implementation of each element of the <see cref="T:System.Array" />.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> to sort.</param>
		/// <param name="index">The starting index of the range to sort.</param>
		/// <param name="length">The number of elements in the range to sort.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />.-or-<paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">One or more elements in <paramref name="array" /> do not implement the <see cref="T:System.IComparable" /> interface.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000827 RID: 2087 RVA: 0x0002D17C File Offset: 0x0002B37C
		public static void Sort(Array array, int index, int length)
		{
			Array.Sort(array, null, index, length, null);
		}

		/// <summary>Sorts the elements in a one-dimensional <see cref="T:System.Array" /> using the specified <see cref="T:System.Collections.IComparer" />.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> to sort.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing elements.-or-null to use the <see cref="T:System.IComparable" /> implementation of each element.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is null, and one or more elements in <paramref name="array" /> do not implement the <see cref="T:System.IComparable" /> interface.</exception>
		/// <exception cref="T:System.ArgumentException">The implementation of <paramref name="comparer" /> caused an error during the sort. For example, <paramref name="comparer" /> might not return 0 when comparing an item with itself.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000828 RID: 2088 RVA: 0x0002D188 File Offset: 0x0002B388
		public static void Sort(Array array, IComparer comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Sort(array, null, array.GetLowerBound(0), array.Length, comparer);
		}

		/// <summary>Sorts the elements in a range of elements in a one-dimensional <see cref="T:System.Array" /> using the specified <see cref="T:System.Collections.IComparer" />.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> to sort.</param>
		/// <param name="index">The starting index of the range to sort.</param>
		/// <param name="length">The number of elements in the range to sort.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing elements.-or-null to use the <see cref="T:System.IComparable" /> implementation of each element.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />.-or-<paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in <paramref name="array" />. -or-The implementation of <paramref name="comparer" /> caused an error during the sort. For example, <paramref name="comparer" /> might not return 0 when comparing an item with itself.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is null, and one or more elements in <paramref name="array" /> do not implement the <see cref="T:System.IComparable" /> interface.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000829 RID: 2089 RVA: 0x0002D1AD File Offset: 0x0002B3AD
		public static void Sort(Array array, int index, int length, IComparer comparer)
		{
			Array.Sort(array, null, index, length, comparer);
		}

		/// <summary>Sorts a pair of one-dimensional <see cref="T:System.Array" /> objects (one contains the keys and the other contains the corresponding items) based on the keys in the first <see cref="T:System.Array" /> using the <see cref="T:System.IComparable" /> implementation of each key.</summary>
		/// <param name="keys">The one-dimensional <see cref="T:System.Array" /> that contains the keys to sort.</param>
		/// <param name="items">The one-dimensional <see cref="T:System.Array" /> that contains the items that correspond to each of the keys in the <paramref name="keys" /><see cref="T:System.Array" />.-or-null to sort only the <paramref name="keys" /><see cref="T:System.Array" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keys" /> is null.</exception>
		/// <exception cref="T:System.RankException">The <paramref name="keys" /><see cref="T:System.Array" /> is multidimensional.-or-The <paramref name="items" /><see cref="T:System.Array" /> is multidimensional.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="items" /> is not null, and the length of <paramref name="keys" /> is greater than the length of <paramref name="items" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">One or more elements in the <paramref name="keys" /><see cref="T:System.Array" /> do not implement the <see cref="T:System.IComparable" /> interface.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600082A RID: 2090 RVA: 0x0002D1B9 File Offset: 0x0002B3B9
		public static void Sort(Array keys, Array items)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			Array.Sort(keys, items, keys.GetLowerBound(0), keys.Length, null);
		}

		/// <summary>Sorts a pair of one-dimensional <see cref="T:System.Array" /> objects (one contains the keys and the other contains the corresponding items) based on the keys in the first <see cref="T:System.Array" /> using the specified <see cref="T:System.Collections.IComparer" />.</summary>
		/// <param name="keys">The one-dimensional <see cref="T:System.Array" /> that contains the keys to sort.</param>
		/// <param name="items">The one-dimensional <see cref="T:System.Array" /> that contains the items that correspond to each of the keys in the <paramref name="keys" /><see cref="T:System.Array" />.-or-null to sort only the <paramref name="keys" /><see cref="T:System.Array" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing elements.-or-null to use the <see cref="T:System.IComparable" /> implementation of each element.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keys" /> is null.</exception>
		/// <exception cref="T:System.RankException">The <paramref name="keys" /><see cref="T:System.Array" /> is multidimensional.-or-The <paramref name="items" /><see cref="T:System.Array" /> is multidimensional.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="items" /> is not null, and the length of <paramref name="keys" /> is greater than the length of <paramref name="items" />. -or-The implementation of <paramref name="comparer" /> caused an error during the sort. For example, <paramref name="comparer" /> might not return 0 when comparing an item with itself.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is null, and one or more elements in the <paramref name="keys" /><see cref="T:System.Array" /> do not implement the <see cref="T:System.IComparable" /> interface.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600082B RID: 2091 RVA: 0x0002D1DE File Offset: 0x0002B3DE
		public static void Sort(Array keys, Array items, IComparer comparer)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			Array.Sort(keys, items, keys.GetLowerBound(0), keys.Length, comparer);
		}

		/// <summary>Sorts a range of elements in a pair of one-dimensional <see cref="T:System.Array" /> objects (one contains the keys and the other contains the corresponding items) based on the keys in the first <see cref="T:System.Array" /> using the <see cref="T:System.IComparable" /> implementation of each key.</summary>
		/// <param name="keys">The one-dimensional <see cref="T:System.Array" /> that contains the keys to sort.</param>
		/// <param name="items">The one-dimensional <see cref="T:System.Array" /> that contains the items that correspond to each of the keys in the <paramref name="keys" /><see cref="T:System.Array" />.-or-null to sort only the <paramref name="keys" /><see cref="T:System.Array" />.</param>
		/// <param name="index">The starting index of the range to sort.</param>
		/// <param name="length">The number of elements in the range to sort.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keys" /> is null.</exception>
		/// <exception cref="T:System.RankException">The <paramref name="keys" /><see cref="T:System.Array" /> is multidimensional.-or-The <paramref name="items" /><see cref="T:System.Array" /> is multidimensional.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="keys" />.-or-<paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="items" /> is not null, and the length of <paramref name="keys" /> is greater than the length of <paramref name="items" />.-or-<paramref name="index" /> and <paramref name="length" /> do not specify a valid range in the <paramref name="keys" /><see cref="T:System.Array" />.-or-<paramref name="items" /> is not null, and <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in the <paramref name="items" /><see cref="T:System.Array" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">One or more elements in the <paramref name="keys" /><see cref="T:System.Array" /> do not implement the <see cref="T:System.IComparable" /> interface.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600082C RID: 2092 RVA: 0x0002D203 File Offset: 0x0002B403
		public static void Sort(Array keys, Array items, int index, int length)
		{
			Array.Sort(keys, items, index, length, null);
		}

		/// <summary>Sorts a range of elements in a pair of one-dimensional <see cref="T:System.Array" /> objects (one contains the keys and the other contains the corresponding items) based on the keys in the first <see cref="T:System.Array" /> using the specified <see cref="T:System.Collections.IComparer" />.</summary>
		/// <param name="keys">The one-dimensional <see cref="T:System.Array" /> that contains the keys to sort.</param>
		/// <param name="items">The one-dimensional <see cref="T:System.Array" /> that contains the items that correspond to each of the keys in the <paramref name="keys" /><see cref="T:System.Array" />.-or-null to sort only the <paramref name="keys" /><see cref="T:System.Array" />.</param>
		/// <param name="index">The starting index of the range to sort.</param>
		/// <param name="length">The number of elements in the range to sort.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing elements.-or-null to use the <see cref="T:System.IComparable" /> implementation of each element.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keys" /> is null.</exception>
		/// <exception cref="T:System.RankException">The <paramref name="keys" /><see cref="T:System.Array" /> is multidimensional.-or-The <paramref name="items" /><see cref="T:System.Array" /> is multidimensional.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="keys" />.-or-<paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="items" /> is not null, and the lower bound of <paramref name="keys" /> does not match the lower bound of <paramref name="items" />.-or-<paramref name="items" /> is not null, and the length of <paramref name="keys" /> is greater than the length of <paramref name="items" />.-or-<paramref name="index" /> and <paramref name="length" /> do not specify a valid range in the <paramref name="keys" /><see cref="T:System.Array" />.-or-<paramref name="items" /> is not null, and <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in the <paramref name="items" /><see cref="T:System.Array" />. -or-The implementation of <paramref name="comparer" /> caused an error during the sort. For example, <paramref name="comparer" /> might not return 0 when comparing an item with itself.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is null, and one or more elements in the <paramref name="keys" /><see cref="T:System.Array" /> do not implement the <see cref="T:System.IComparable" /> interface.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600082D RID: 2093 RVA: 0x0002D210 File Offset: 0x0002B410
		public static void Sort(Array keys, Array items, int index, int length, IComparer comparer)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			if (keys.Rank != 1 || (items != null && items.Rank != 1))
			{
				throw new RankException("Only single dimension arrays are supported here.");
			}
			int lowerBound = keys.GetLowerBound(0);
			if (items != null && lowerBound != items.GetLowerBound(0))
			{
				throw new ArgumentException("The arrays' lower bounds must be identical.");
			}
			if (index < lowerBound || length < 0)
			{
				throw new ArgumentOutOfRangeException((length < 0) ? "length" : "index", "Non-negative number required.");
			}
			if (keys.Length - (index - lowerBound) < length || (items != null && index - lowerBound > items.Length - length))
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (length > 1)
			{
				Array.SortImpl(keys, items, index, length, comparer);
			}
		}

		/// <summary>Sorts the elements in an entire <see cref="T:System.Array" /> using the <see cref="T:System.IComparable`1" /> generic interface implementation of each element of the <see cref="T:System.Array" />.</summary>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to sort.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">One or more elements in <paramref name="array" /> do not implement the <see cref="T:System.IComparable`1" /> generic interface.</exception>
		// Token: 0x0600082E RID: 2094 RVA: 0x0002D2C5 File Offset: 0x0002B4C5
		public static void Sort<T>(T[] array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Sort<T>(array, 0, array.Length, null);
		}

		/// <summary>Sorts the elements in a range of elements in an <see cref="T:System.Array" /> using the <see cref="T:System.IComparable`1" /> generic interface implementation of each element of the <see cref="T:System.Array" />.</summary>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to sort</param>
		/// <param name="index">The starting index of the range to sort.</param>
		/// <param name="length">The number of elements in the range to sort.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />.-or-<paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">One or more elements in <paramref name="array" /> do not implement the <see cref="T:System.IComparable`1" /> generic interface.</exception>
		// Token: 0x0600082F RID: 2095 RVA: 0x0002D2E0 File Offset: 0x0002B4E0
		public static void Sort<T>(T[] array, int index, int length)
		{
			Array.Sort<T>(array, index, length, null);
		}

		/// <summary>Sorts the elements in an <see cref="T:System.Array" /> using the specified <see cref="T:System.Collections.Generic.IComparer`1" /> generic interface.</summary>
		/// <param name="array">The one-dimensional, zero-base <see cref="T:System.Array" /> to sort</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> generic interface implementation to use when comparing elements, or null to use the <see cref="T:System.IComparable`1" /> generic interface implementation of each element.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is null, and one or more elements in <paramref name="array" /> do not implement the <see cref="T:System.IComparable`1" /> generic interface.</exception>
		/// <exception cref="T:System.ArgumentException">The implementation of <paramref name="comparer" /> caused an error during the sort. For example, <paramref name="comparer" /> might not return 0 when comparing an item with itself.</exception>
		// Token: 0x06000830 RID: 2096 RVA: 0x0002D2EB File Offset: 0x0002B4EB
		public static void Sort<T>(T[] array, IComparer<T> comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Sort<T>(array, 0, array.Length, comparer);
		}

		/// <summary>Sorts the elements in a range of elements in an <see cref="T:System.Array" /> using the specified <see cref="T:System.Collections.Generic.IComparer`1" /> generic interface.</summary>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to sort.</param>
		/// <param name="index">The starting index of the range to sort.</param>
		/// <param name="length">The number of elements in the range to sort.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> generic interface implementation to use when comparing elements, or null to use the <see cref="T:System.IComparable`1" /> generic interface implementation of each element.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />.-or-<paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in <paramref name="array" />. -or-The implementation of <paramref name="comparer" /> caused an error during the sort. For example, <paramref name="comparer" /> might not return 0 when comparing an item with itself.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is null, and one or more elements in <paramref name="array" /> do not implement the <see cref="T:System.IComparable`1" /> generic interface.</exception>
		// Token: 0x06000831 RID: 2097 RVA: 0x0002D308 File Offset: 0x0002B508
		public static void Sort<T>(T[] array, int index, int length, IComparer<T> comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || length < 0)
			{
				throw new ArgumentOutOfRangeException((length < 0) ? "length" : "index", "Non-negative number required.");
			}
			if (array.Length - index < length)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (length > 1)
			{
				ArraySortHelper<T>.Sort(array, index, length, comparer);
			}
		}

		/// <summary>Sorts the elements in an <see cref="T:System.Array" /> using the specified <see cref="T:System.Comparison`1" />.</summary>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to sort</param>
		/// <param name="comparison">The <see cref="T:System.Comparison`1" /> to use when comparing elements.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.-or-<paramref name="comparison" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The implementation of <paramref name="comparison" /> caused an error during the sort. For example, <paramref name="comparison" /> might not return 0 when comparing an item with itself.</exception>
		// Token: 0x06000832 RID: 2098 RVA: 0x0002D366 File Offset: 0x0002B566
		public static void Sort<T>(T[] array, Comparison<T> comparison)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (comparison == null)
			{
				throw new ArgumentNullException("comparison");
			}
			ArraySortHelper<T>.Sort(array, 0, array.Length, comparison);
		}

		/// <summary>Sorts a pair of <see cref="T:System.Array" /> objects (one contains the keys and the other contains the corresponding items) based on the keys in the first <see cref="T:System.Array" /> using the <see cref="T:System.IComparable`1" /> generic interface implementation of each key.</summary>
		/// <param name="keys">The one-dimensional, zero-based <see cref="T:System.Array" /> that contains the keys to sort. </param>
		/// <param name="items">The one-dimensional, zero-based <see cref="T:System.Array" /> that contains the items that correspond to the keys in <paramref name="keys" />, or null to sort only <paramref name="keys" />.</param>
		/// <typeparam name="TKey">The type of the elements of the key array.</typeparam>
		/// <typeparam name="TValue">The type of the elements of the items array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keys" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="items" /> is not null, and the lower bound of <paramref name="keys" /> does not match the lower bound of <paramref name="items" />.-or-<paramref name="items" /> is not null, and the length of <paramref name="keys" /> is greater than the length of <paramref name="items" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">One or more elements in the <paramref name="keys" /><see cref="T:System.Array" /> do not implement the <see cref="T:System.IComparable`1" /> generic interface.</exception>
		// Token: 0x06000833 RID: 2099 RVA: 0x0002D38F File Offset: 0x0002B58F
		public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			Array.Sort<TKey, TValue>(keys, items, 0, keys.Length, null);
		}

		/// <summary>Sorts a range of elements in a pair of <see cref="T:System.Array" /> objects (one contains the keys and the other contains the corresponding items) based on the keys in the first <see cref="T:System.Array" /> using the <see cref="T:System.IComparable`1" /> generic interface implementation of each key.</summary>
		/// <param name="keys">The one-dimensional, zero-based <see cref="T:System.Array" /> that contains the keys to sort. </param>
		/// <param name="items">The one-dimensional, zero-based <see cref="T:System.Array" /> that contains the items that correspond to the keys in <paramref name="keys" />, or null to sort only <paramref name="keys" />.</param>
		/// <param name="index">The starting index of the range to sort.</param>
		/// <param name="length">The number of elements in the range to sort.</param>
		/// <typeparam name="TKey">The type of the elements of the key array.</typeparam>
		/// <typeparam name="TValue">The type of the elements of the items array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keys" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="keys" />.-or-<paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="items" /> is not null, and the lower bound of <paramref name="keys" /> does not match the lower bound of <paramref name="items" />.-or-<paramref name="items" /> is not null, and the length of <paramref name="keys" /> is greater than the length of <paramref name="items" />.-or-<paramref name="index" /> and <paramref name="length" /> do not specify a valid range in the <paramref name="keys" /><see cref="T:System.Array" />.-or-<paramref name="items" /> is not null, and <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in the <paramref name="items" /><see cref="T:System.Array" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">One or more elements in the <paramref name="keys" /><see cref="T:System.Array" /> do not implement the <see cref="T:System.IComparable`1" /> generic interface.</exception>
		// Token: 0x06000834 RID: 2100 RVA: 0x0002D3AB File Offset: 0x0002B5AB
		public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items, int index, int length)
		{
			Array.Sort<TKey, TValue>(keys, items, index, length, null);
		}

		/// <summary>Sorts a pair of <see cref="T:System.Array" /> objects (one contains the keys and the other contains the corresponding items) based on the keys in the first <see cref="T:System.Array" /> using the specified <see cref="T:System.Collections.Generic.IComparer`1" /> generic interface.</summary>
		/// <param name="keys">The one-dimensional, zero-based <see cref="T:System.Array" /> that contains the keys to sort. </param>
		/// <param name="items">The one-dimensional, zero-based <see cref="T:System.Array" /> that contains the items that correspond to the keys in <paramref name="keys" />, or null to sort only <paramref name="keys" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> generic interface implementation to use when comparing elements, or null to use the <see cref="T:System.IComparable`1" /> generic interface implementation of each element.</param>
		/// <typeparam name="TKey">The type of the elements of the key array.</typeparam>
		/// <typeparam name="TValue">The type of the elements of the items array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keys" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="items" /> is not null, and the lower bound of <paramref name="keys" /> does not match the lower bound of <paramref name="items" />.-or-<paramref name="items" /> is not null, and the length of <paramref name="keys" /> is greater than the length of <paramref name="items" />.-or-The implementation of <paramref name="comparer" /> caused an error during the sort. For example, <paramref name="comparer" /> might not return 0 when comparing an item with itself.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is null, and one or more elements in the <paramref name="keys" /><see cref="T:System.Array" /> do not implement the <see cref="T:System.IComparable`1" /> generic interface.</exception>
		// Token: 0x06000835 RID: 2101 RVA: 0x0002D3B7 File Offset: 0x0002B5B7
		public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items, IComparer<TKey> comparer)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			Array.Sort<TKey, TValue>(keys, items, 0, keys.Length, comparer);
		}

		/// <summary>Sorts a range of elements in a pair of <see cref="T:System.Array" /> objects (one contains the keys and the other contains the corresponding items) based on the keys in the first <see cref="T:System.Array" /> using the specified <see cref="T:System.Collections.Generic.IComparer`1" /> generic interface.</summary>
		/// <param name="keys">The one-dimensional, zero-based <see cref="T:System.Array" /> that contains the keys to sort. </param>
		/// <param name="items">The one-dimensional, zero-based <see cref="T:System.Array" /> that contains the items that correspond to the keys in <paramref name="keys" />, or null to sort only <paramref name="keys" />.</param>
		/// <param name="index">The starting index of the range to sort.</param>
		/// <param name="length">The number of elements in the range to sort.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> generic interface implementation to use when comparing elements, or null to use the <see cref="T:System.IComparable`1" /> generic interface implementation of each element.</param>
		/// <typeparam name="TKey">The type of the elements of the key array.</typeparam>
		/// <typeparam name="TValue">The type of the elements of the items array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keys" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="keys" />.-or-<paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="items" /> is not null, and the lower bound of <paramref name="keys" /> does not match the lower bound of <paramref name="items" />.-or-<paramref name="items" /> is not null, and the length of <paramref name="keys" /> is greater than the length of <paramref name="items" />.-or-<paramref name="index" /> and <paramref name="length" /> do not specify a valid range in the <paramref name="keys" /><see cref="T:System.Array" />.-or-<paramref name="items" /> is not null, and <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in the <paramref name="items" /><see cref="T:System.Array" />. -or-The implementation of <paramref name="comparer" /> caused an error during the sort. For example, <paramref name="comparer" /> might not return 0 when comparing an item with itself.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is null, and one or more elements in the <paramref name="keys" /><see cref="T:System.Array" /> do not implement the <see cref="T:System.IComparable`1" /> generic interface.</exception>
		// Token: 0x06000836 RID: 2102 RVA: 0x0002D3D4 File Offset: 0x0002B5D4
		public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items, int index, int length, IComparer<TKey> comparer)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			if (index < 0 || length < 0)
			{
				throw new ArgumentOutOfRangeException((length < 0) ? "length" : "index", "Non-negative number required.");
			}
			if (keys.Length - index < length || (items != null && index > items.Length - length))
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (length > 1)
			{
				if (items == null)
				{
					Array.Sort<TKey>(keys, index, length, comparer);
					return;
				}
				ArraySortHelper<TKey, TValue>.Default.Sort(keys, items, index, length, comparer);
			}
		}

		/// <summary>Determines whether the specified array contains elements that match the conditions defined by the specified predicate.</summary>
		/// <returns>true if <paramref name="array" /> contains one or more elements that match the conditions defined by the specified predicate; otherwise, false.</returns>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> that defines the conditions of the elements to search for.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.-or-<paramref name="match" /> is null.</exception>
		// Token: 0x06000837 RID: 2103 RVA: 0x0002D452 File Offset: 0x0002B652
		public static bool Exists<T>(T[] array, Predicate<T> match)
		{
			return Array.FindIndex<T>(array, match) != -1;
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0002D464 File Offset: 0x0002B664
		public static void Fill<T>(T[] array, T value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = value;
			}
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0002D498 File Offset: 0x0002B698
		public static void Fill<T>(T[] array, T value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (startIndex < 0 || startIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0 || startIndex > array.Length - count)
			{
				throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
			}
			for (int i = startIndex; i < startIndex + count; i++)
			{
				array[i] = value;
			}
		}

		/// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the first occurrence within the entire <see cref="T:System.Array" />.</summary>
		/// <returns>The first element that matches the conditions defined by the specified predicate, if found; otherwise, the default value for type <paramref name="T" />.</returns>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> that defines the conditions of the element to search for.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.-or-<paramref name="match" /> is null.</exception>
		// Token: 0x0600083A RID: 2106 RVA: 0x0002D500 File Offset: 0x0002B700
		public static T Find<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (match(array[i]))
				{
					return array[i];
				}
			}
			return default(T);
		}

		/// <summary>Retrieves all the elements that match the conditions defined by the specified predicate.</summary>
		/// <returns>An <see cref="T:System.Array" /> containing all the elements that match the conditions defined by the specified predicate, if found; otherwise, an empty <see cref="T:System.Array" />.</returns>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> that defines the conditions of the elements to search for.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.-or-<paramref name="match" /> is null.</exception>
		// Token: 0x0600083B RID: 2107 RVA: 0x0002D558 File Offset: 0x0002B758
		public static T[] FindAll<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			int num = 0;
			T[] array2 = Array.Empty<T>();
			for (int i = 0; i < array.Length; i++)
			{
				if (match(array[i]))
				{
					if (num == array2.Length)
					{
						Array.Resize<T>(ref array2, Math.Min((num == 0) ? 4 : (num * 2), array.Length));
					}
					array2[num++] = array[i];
				}
			}
			if (num != array2.Length)
			{
				Array.Resize<T>(ref array2, num);
			}
			return array2;
		}

		/// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Array" />.</summary>
		/// <returns>The zero-based index of the first occurrence of an element that matches the conditions defined by <paramref name="match" />, if found; otherwise, –1.</returns>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> that defines the conditions of the element to search for.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.-or-<paramref name="match" /> is null.</exception>
		// Token: 0x0600083C RID: 2108 RVA: 0x0002D5E5 File Offset: 0x0002B7E5
		public static int FindIndex<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.FindIndex<T>(array, 0, array.Length, match);
		}

		/// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the range of elements in the <see cref="T:System.Array" /> that extends from the specified index to the last element.</summary>
		/// <returns>The zero-based index of the first occurrence of an element that matches the conditions defined by <paramref name="match" />, if found; otherwise, –1.</returns>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="startIndex">The zero-based starting index of the search.</param>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> that defines the conditions of the element to search for.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.-or-<paramref name="match" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="array" />.</exception>
		// Token: 0x0600083D RID: 2109 RVA: 0x0002D600 File Offset: 0x0002B800
		public static int FindIndex<T>(T[] array, int startIndex, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.FindIndex<T>(array, startIndex, array.Length - startIndex, match);
		}

		/// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the range of elements in the <see cref="T:System.Array" /> that starts at the specified index and contains the specified number of elements.</summary>
		/// <returns>The zero-based index of the first occurrence of an element that matches the conditions defined by <paramref name="match" />, if found; otherwise, –1.</returns>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="startIndex">The zero-based starting index of the search.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> that defines the conditions of the element to search for.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.-or-<paramref name="match" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="array" />.-or-<paramref name="count" /> is less than zero.-or-<paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="array" />.</exception>
		// Token: 0x0600083E RID: 2110 RVA: 0x0002D620 File Offset: 0x0002B820
		public static int FindIndex<T>(T[] array, int startIndex, int count, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (startIndex < 0 || startIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0 || startIndex > array.Length - count)
			{
				throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			int num = startIndex + count;
			for (int i = startIndex; i < num; i++)
			{
				if (match(array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the last occurrence within the entire <see cref="T:System.Array" />.</summary>
		/// <returns>The last element that matches the conditions defined by the specified predicate, if found; otherwise, the default value for type <paramref name="T" />.</returns>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> that defines the conditions of the element to search for.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.-or-<paramref name="match" /> is null.</exception>
		// Token: 0x0600083F RID: 2111 RVA: 0x0002D6A4 File Offset: 0x0002B8A4
		public static T FindLast<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			for (int i = array.Length - 1; i >= 0; i--)
			{
				if (match(array[i]))
				{
					return array[i];
				}
			}
			return default(T);
		}

		/// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the last occurrence within the entire <see cref="T:System.Array" />.</summary>
		/// <returns>The zero-based index of the last occurrence of an element that matches the conditions defined by <paramref name="match" />, if found; otherwise, –1.</returns>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> that defines the conditions of the element to search for.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.-or-<paramref name="match" /> is null.</exception>
		// Token: 0x06000840 RID: 2112 RVA: 0x0002D6FD File Offset: 0x0002B8FD
		public static int FindLastIndex<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.FindLastIndex<T>(array, array.Length - 1, array.Length, match);
		}

		/// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the last occurrence within the range of elements in the <see cref="T:System.Array" /> that extends from the first element to the specified index.</summary>
		/// <returns>The zero-based index of the last occurrence of an element that matches the conditions defined by <paramref name="match" />, if found; otherwise, –1.</returns>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> that defines the conditions of the element to search for.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.-or-<paramref name="match" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="array" />.</exception>
		// Token: 0x06000841 RID: 2113 RVA: 0x0002D71C File Offset: 0x0002B91C
		public static int FindLastIndex<T>(T[] array, int startIndex, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.FindLastIndex<T>(array, startIndex, startIndex + 1, match);
		}

		/// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the last occurrence within the range of elements in the <see cref="T:System.Array" /> that contains the specified number of elements and ends at the specified index.</summary>
		/// <returns>The zero-based index of the last occurrence of an element that matches the conditions defined by <paramref name="match" />, if found; otherwise, –1.</returns>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> that defines the conditions of the element to search for.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.-or-<paramref name="match" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="array" />.-or-<paramref name="count" /> is less than zero.-or-<paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="array" />.</exception>
		// Token: 0x06000842 RID: 2114 RVA: 0x0002D738 File Offset: 0x0002B938
		public static int FindLastIndex<T>(T[] array, int startIndex, int count, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			if (array.Length == 0)
			{
				if (startIndex != -1)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
			}
			else if (startIndex < 0 || startIndex >= array.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0 || startIndex - count + 1 < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
			}
			int num = startIndex - count;
			for (int i = startIndex; i > num; i--)
			{
				if (match(array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Determines whether every element in the array matches the conditions defined by the specified predicate.</summary>
		/// <returns>true if every element in <paramref name="array" /> matches the conditions defined by the specified predicate; otherwise, false. If there are no elements in the array, the return value is true.</returns>
		/// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to check against the conditions</param>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> that defines the conditions to check against the elements.</param>
		/// <typeparam name="T">The type of the elements of the array.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.-or-<paramref name="match" /> is null.</exception>
		// Token: 0x06000843 RID: 2115 RVA: 0x0002D7D4 File Offset: 0x0002B9D4
		public static bool TrueForAll<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (!match(array[i]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Array" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Array" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000844 RID: 2116 RVA: 0x0002D81D File Offset: 0x0002BA1D
		public IEnumerator GetEnumerator()
		{
			return new Array.ArrayEnumerator(this);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00002111 File Offset: 0x00000311
		private Array()
		{
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0002C321 File Offset: 0x0002A521
		internal int InternalArray__ICollection_get_Count()
		{
			return this.Length;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00003B29 File Offset: 0x00001D29
		internal bool InternalArray__ICollection_get_IsReadOnly()
		{
			return true;
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0002D825 File Offset: 0x0002BA25
		internal IEnumerator<T> InternalArray__IEnumerable_GetEnumerator<T>()
		{
			if (this.Length == 0)
			{
				return Array.EmptyInternalEnumerator<T>.Value;
			}
			return new Array.InternalEnumerator<T>(this);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0002D840 File Offset: 0x0002BA40
		internal void InternalArray__ICollection_Clear()
		{
			throw new NotSupportedException("Collection is read-only");
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0002D84C File Offset: 0x0002BA4C
		internal void InternalArray__ICollection_Add<T>(T item)
		{
			throw new NotSupportedException("Collection is of a fixed size");
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0002D84C File Offset: 0x0002BA4C
		internal bool InternalArray__ICollection_Remove<T>(T item)
		{
			throw new NotSupportedException("Collection is of a fixed size");
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0002D858 File Offset: 0x0002BA58
		internal bool InternalArray__ICollection_Contains<T>(T item)
		{
			if (this.Rank > 1)
			{
				throw new RankException(Locale.GetText("Only single dimension arrays are supported."));
			}
			int length = this.Length;
			for (int i = 0; i < length; i++)
			{
				T t;
				this.GetGenericValueImpl<T>(i, out t);
				if (item == null)
				{
					if (t == null)
					{
						return true;
					}
				}
				else if (item.Equals(t))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0002D8C4 File Offset: 0x0002BAC4
		internal void InternalArray__ICollection_CopyTo<T>(T[] array, int arrayIndex)
		{
			Array.Copy(this, this.GetLowerBound(0), array, arrayIndex, this.Length);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0002D8DC File Offset: 0x0002BADC
		internal T InternalArray__IReadOnlyList_get_Item<T>(int index)
		{
			if (index >= this.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			T t;
			this.GetGenericValueImpl<T>(index, out t);
			return t;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0002C321 File Offset: 0x0002A521
		internal int InternalArray__IReadOnlyCollection_get_Count()
		{
			return this.Length;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0002D84C File Offset: 0x0002BA4C
		internal void InternalArray__Insert<T>(int index, T item)
		{
			throw new NotSupportedException("Collection is of a fixed size");
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0002D84C File Offset: 0x0002BA4C
		internal void InternalArray__RemoveAt(int index)
		{
			throw new NotSupportedException("Collection is of a fixed size");
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0002D908 File Offset: 0x0002BB08
		internal int InternalArray__IndexOf<T>(T item)
		{
			if (this.Rank > 1)
			{
				throw new RankException(Locale.GetText("Only single dimension arrays are supported."));
			}
			int length = this.Length;
			for (int i = 0; i < length; i++)
			{
				T t;
				this.GetGenericValueImpl<T>(i, out t);
				if (item == null)
				{
					if (t == null)
					{
						return i + this.GetLowerBound(0);
					}
				}
				else if (t.Equals(item))
				{
					return i + this.GetLowerBound(0);
				}
			}
			return this.GetLowerBound(0) - 1;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0002D98C File Offset: 0x0002BB8C
		internal T InternalArray__get_Item<T>(int index)
		{
			if (index >= this.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			T t;
			this.GetGenericValueImpl<T>(index, out t);
			return t;
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0002D9B8 File Offset: 0x0002BBB8
		internal void InternalArray__set_Item<T>(int index, T item)
		{
			if (index >= this.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			object[] array = this as object[];
			if (array != null)
			{
				array[index] = item;
				return;
			}
			this.SetGenericValueImpl<T>(index, ref item);
		}

		// Token: 0x06000855 RID: 2133
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void GetGenericValueImpl<T>(int pos, out T value);

		// Token: 0x06000856 RID: 2134
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetGenericValueImpl<T>(int pos, ref T value);

		/// <summary>Gets a 32-bit integer that represents the total number of elements in all the dimensions of the <see cref="T:System.Array" />.</summary>
		/// <returns>A 32-bit integer that represents the total number of elements in all the dimensions of the <see cref="T:System.Array" />; zero if there are no elements in the array.</returns>
		/// <exception cref="T:System.OverflowException">The array is multidimensional and contains more than <see cref="F:System.Int32.MaxValue" /> elements.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x0002D9F8 File Offset: 0x0002BBF8
		public int Length
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				int num = this.GetLength(0);
				for (int i = 1; i < this.Rank; i++)
				{
					num *= this.GetLength(i);
				}
				return num;
			}
		}

		/// <summary>Gets the rank (number of dimensions) of the <see cref="T:System.Array" />. For example, a one-dimensional array returns 1, a two-dimensional array returns 2, and so on. </summary>
		/// <returns>The rank (number of dimensions) of the <see cref="T:System.Array" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x0002DA29 File Offset: 0x0002BC29
		public int Rank
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.GetRank();
			}
		}

		// Token: 0x06000859 RID: 2137
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetRank();

		/// <summary>Gets a 32-bit integer that represents the number of elements in the specified dimension of the <see cref="T:System.Array" />.</summary>
		/// <returns>A 32-bit integer that represents the number of elements in the specified dimension.</returns>
		/// <param name="dimension">A zero-based dimension of the <see cref="T:System.Array" /> whose length needs to be determined.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="dimension" /> is less than zero.-or-<paramref name="dimension" /> is equal to or greater than <see cref="P:System.Array.Rank" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600085A RID: 2138
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetLength(int dimension);

		/// <summary>Gets the index of the first element of the specified dimension in the array.</summary>
		/// <returns>The index of the first element of the specified dimension in the array.</returns>
		/// <param name="dimension">A zero-based dimension of the array whose starting index needs to be determined.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="dimension" /> is less than zero.-or-<paramref name="dimension" /> is equal to or greater than <see cref="P:System.Array.Rank" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600085B RID: 2139
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetLowerBound(int dimension);

		/// <summary>Gets the value at the specified position in the multidimensional <see cref="T:System.Array" />. The indexes are specified as an array of 32-bit integers.</summary>
		/// <returns>The value at the specified position in the multidimensional <see cref="T:System.Array" />.</returns>
		/// <param name="indices">A one-dimensional array of 32-bit integers that represent the indexes specifying the position of the <see cref="T:System.Array" /> element to get.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="indices" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The number of dimensions in the current <see cref="T:System.Array" /> is not equal to the number of elements in <paramref name="indices" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Any element in <paramref name="indices" /> is outside the range of valid indexes for the corresponding dimension of the current <see cref="T:System.Array" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600085C RID: 2140
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern object GetValue(params int[] indices);

		/// <summary>Sets a value to the element at the specified position in the multidimensional <see cref="T:System.Array" />. The indexes are specified as an array of 32-bit integers.</summary>
		/// <param name="value">The new value for the specified element.</param>
		/// <param name="indices">A one-dimensional array of 32-bit integers that represent the indexes specifying the position of the element to set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="indices" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The number of dimensions in the current <see cref="T:System.Array" /> is not equal to the number of elements in <paramref name="indices" />.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> cannot be cast to the element type of the current <see cref="T:System.Array" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Any element in <paramref name="indices" /> is outside the range of valid indexes for the corresponding dimension of the current <see cref="T:System.Array" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600085D RID: 2141
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetValue(object value, params int[] indices);

		// Token: 0x0600085E RID: 2142
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern object GetValueImpl(int pos);

		// Token: 0x0600085F RID: 2143
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetValueImpl(object value, int pos);

		// Token: 0x06000860 RID: 2144
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool FastCopy(Array source, int source_idx, Array dest, int dest_idx, int length);

		// Token: 0x06000861 RID: 2145
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Array CreateInstanceImpl(Type elementType, int[] lengths, int[] bounds);

		/// <summary>Gets the index of the last element of the specified dimension in the array.</summary>
		/// <returns>The index of the last element of the specified dimension in the array, or -1 if the specified dimension is empty. </returns>
		/// <param name="dimension">A zero-based dimension of the array whose upper bound needs to be determined.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="dimension" /> is less than zero.-or-<paramref name="dimension" /> is equal to or greater than <see cref="P:System.Array.Rank" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000862 RID: 2146 RVA: 0x0002DA31 File Offset: 0x0002BC31
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public int GetUpperBound(int dimension)
		{
			return this.GetLowerBound(dimension) + this.GetLength(dimension) - 1;
		}

		/// <summary>Gets the value at the specified position in the one-dimensional <see cref="T:System.Array" />. The index is specified as a 32-bit integer.</summary>
		/// <returns>The value at the specified position in the one-dimensional <see cref="T:System.Array" />.</returns>
		/// <param name="index">A 32-bit integer that represents the position of the <see cref="T:System.Array" /> element to get.</param>
		/// <exception cref="T:System.ArgumentException">The current <see cref="T:System.Array" /> does not have exactly one dimension.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the current <see cref="T:System.Array" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000863 RID: 2147 RVA: 0x0002DA44 File Offset: 0x0002BC44
		public object GetValue(int index)
		{
			if (this.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.");
			}
			int lowerBound = this.GetLowerBound(0);
			if (index < lowerBound || index > this.GetUpperBound(0))
			{
				throw new IndexOutOfRangeException(Locale.GetText("Index has to be between upper and lower bound of the array."));
			}
			if (base.GetType().GetElementType().IsPointer)
			{
				throw new NotSupportedException("Type is not supported.");
			}
			return this.GetValueImpl(index - lowerBound);
		}

		/// <summary>Gets the value at the specified position in the two-dimensional <see cref="T:System.Array" />. The indexes are specified as 32-bit integers.</summary>
		/// <returns>The value at the specified position in the two-dimensional <see cref="T:System.Array" />.</returns>
		/// <param name="index1">A 32-bit integer that represents the first-dimension index of the <see cref="T:System.Array" /> element to get.</param>
		/// <param name="index2">A 32-bit integer that represents the second-dimension index of the <see cref="T:System.Array" /> element to get.</param>
		/// <exception cref="T:System.ArgumentException">The current <see cref="T:System.Array" /> does not have exactly two dimensions.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Either <paramref name="index1" /> or <paramref name="index2" /> is outside the range of valid indexes for the corresponding dimension of the current <see cref="T:System.Array" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000864 RID: 2148 RVA: 0x0002DAB4 File Offset: 0x0002BCB4
		public object GetValue(int index1, int index2)
		{
			int[] array = new int[] { index1, index2 };
			return this.GetValue(array);
		}

		/// <summary>Gets the value at the specified position in the three-dimensional <see cref="T:System.Array" />. The indexes are specified as 32-bit integers.</summary>
		/// <returns>The value at the specified position in the three-dimensional <see cref="T:System.Array" />.</returns>
		/// <param name="index1">A 32-bit integer that represents the first-dimension index of the <see cref="T:System.Array" /> element to get.</param>
		/// <param name="index2">A 32-bit integer that represents the second-dimension index of the <see cref="T:System.Array" /> element to get.</param>
		/// <param name="index3">A 32-bit integer that represents the third-dimension index of the <see cref="T:System.Array" /> element to get.</param>
		/// <exception cref="T:System.ArgumentException">The current <see cref="T:System.Array" /> does not have exactly three dimensions.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index1" /> or <paramref name="index2" /> or <paramref name="index3" /> is outside the range of valid indexes for the corresponding dimension of the current <see cref="T:System.Array" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000865 RID: 2149 RVA: 0x0002DAD8 File Offset: 0x0002BCD8
		public object GetValue(int index1, int index2, int index3)
		{
			int[] array = new int[] { index1, index2, index3 };
			return this.GetValue(array);
		}

		/// <summary>Sets a value to the element at the specified position in the one-dimensional <see cref="T:System.Array" />. The index is specified as a 32-bit integer.</summary>
		/// <param name="value">The new value for the specified element.</param>
		/// <param name="index">A 32-bit integer that represents the position of the <see cref="T:System.Array" /> element to set.</param>
		/// <exception cref="T:System.ArgumentException">The current <see cref="T:System.Array" /> does not have exactly one dimension.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> cannot be cast to the element type of the current <see cref="T:System.Array" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the current <see cref="T:System.Array" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000866 RID: 2150 RVA: 0x0002DB00 File Offset: 0x0002BD00
		public void SetValue(object value, int index)
		{
			if (this.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.");
			}
			int lowerBound = this.GetLowerBound(0);
			if (index < lowerBound || index > this.GetUpperBound(0))
			{
				throw new IndexOutOfRangeException(Locale.GetText("Index has to be >= lower bound and <= upper bound of the array."));
			}
			if (base.GetType().GetElementType().IsPointer)
			{
				throw new NotSupportedException("Type is not supported.");
			}
			this.SetValueImpl(value, index - lowerBound);
		}

		/// <summary>Sets a value to the element at the specified position in the two-dimensional <see cref="T:System.Array" />. The indexes are specified as 32-bit integers.</summary>
		/// <param name="value">The new value for the specified element.</param>
		/// <param name="index1">A 32-bit integer that represents the first-dimension index of the <see cref="T:System.Array" /> element to set.</param>
		/// <param name="index2">A 32-bit integer that represents the second-dimension index of the <see cref="T:System.Array" /> element to set.</param>
		/// <exception cref="T:System.ArgumentException">The current <see cref="T:System.Array" /> does not have exactly two dimensions.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> cannot be cast to the element type of the current <see cref="T:System.Array" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Either <paramref name="index1" /> or <paramref name="index2" /> is outside the range of valid indexes for the corresponding dimension of the current <see cref="T:System.Array" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000867 RID: 2151 RVA: 0x0002DB70 File Offset: 0x0002BD70
		public void SetValue(object value, int index1, int index2)
		{
			int[] array = new int[] { index1, index2 };
			this.SetValue(value, array);
		}

		/// <summary>Sets a value to the element at the specified position in the three-dimensional <see cref="T:System.Array" />. The indexes are specified as 32-bit integers.</summary>
		/// <param name="value">The new value for the specified element.</param>
		/// <param name="index1">A 32-bit integer that represents the first-dimension index of the <see cref="T:System.Array" /> element to set.</param>
		/// <param name="index2">A 32-bit integer that represents the second-dimension index of the <see cref="T:System.Array" /> element to set.</param>
		/// <param name="index3">A 32-bit integer that represents the third-dimension index of the <see cref="T:System.Array" /> element to set.</param>
		/// <exception cref="T:System.ArgumentException">The current <see cref="T:System.Array" /> does not have exactly three dimensions.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> cannot be cast to the element type of the current <see cref="T:System.Array" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index1" /> or <paramref name="index2" /> or <paramref name="index3" /> is outside the range of valid indexes for the corresponding dimension of the current <see cref="T:System.Array" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000868 RID: 2152 RVA: 0x0002DB94 File Offset: 0x0002BD94
		public void SetValue(object value, int index1, int index2, int index3)
		{
			int[] array = new int[] { index1, index2, index3 };
			this.SetValue(value, array);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0002DBBD File Offset: 0x0002BDBD
		internal static Array UnsafeCreateInstance(Type elementType, int[] lengths, int[] lowerBounds)
		{
			return Array.CreateInstance(elementType, lengths, lowerBounds);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0002DBC7 File Offset: 0x0002BDC7
		internal static Array UnsafeCreateInstance(Type elementType, int length1, int length2)
		{
			return Array.CreateInstance(elementType, length1, length2);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0002DBD1 File Offset: 0x0002BDD1
		internal static Array UnsafeCreateInstance(Type elementType, params int[] lengths)
		{
			return Array.CreateInstance(elementType, lengths);
		}

		/// <summary>Creates a one-dimensional <see cref="T:System.Array" /> of the specified <see cref="T:System.Type" /> and length, with zero-based indexing.</summary>
		/// <returns>A new one-dimensional <see cref="T:System.Array" /> of the specified <see cref="T:System.Type" /> with the specified length, using zero-based indexing.</returns>
		/// <param name="elementType">The <see cref="T:System.Type" /> of the <see cref="T:System.Array" /> to create.</param>
		/// <param name="length">The size of the <see cref="T:System.Array" /> to create.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="elementType" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="elementType" /> is not a valid <see cref="T:System.Type" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="elementType" /> is not supported. For example, <see cref="T:System.Void" /> is not supported.-or-<paramref name="elementType" /> is an open generic type.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="length" /> is less than zero.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600086C RID: 2156 RVA: 0x0002DBDC File Offset: 0x0002BDDC
		public static Array CreateInstance(Type elementType, int length)
		{
			int[] array = new int[] { length };
			return Array.CreateInstance(elementType, array);
		}

		/// <summary>Creates a two-dimensional <see cref="T:System.Array" /> of the specified <see cref="T:System.Type" /> and dimension lengths, with zero-based indexing.</summary>
		/// <returns>A new two-dimensional <see cref="T:System.Array" /> of the specified <see cref="T:System.Type" /> with the specified length for each dimension, using zero-based indexing.</returns>
		/// <param name="elementType">The <see cref="T:System.Type" /> of the <see cref="T:System.Array" /> to create.</param>
		/// <param name="length1">The size of the first dimension of the <see cref="T:System.Array" /> to create.</param>
		/// <param name="length2">The size of the second dimension of the <see cref="T:System.Array" /> to create.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="elementType" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="elementType" /> is not a valid <see cref="T:System.Type" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="elementType" /> is not supported. For example, <see cref="T:System.Void" /> is not supported. -or-<paramref name="elementType" /> is an open generic type.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="length1" /> is less than zero.-or-<paramref name="length2" /> is less than zero.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600086D RID: 2157 RVA: 0x0002DBFC File Offset: 0x0002BDFC
		public static Array CreateInstance(Type elementType, int length1, int length2)
		{
			int[] array = new int[] { length1, length2 };
			return Array.CreateInstance(elementType, array);
		}

		/// <summary>Creates a three-dimensional <see cref="T:System.Array" /> of the specified <see cref="T:System.Type" /> and dimension lengths, with zero-based indexing.</summary>
		/// <returns>A new three-dimensional <see cref="T:System.Array" /> of the specified <see cref="T:System.Type" /> with the specified length for each dimension, using zero-based indexing.</returns>
		/// <param name="elementType">The <see cref="T:System.Type" /> of the <see cref="T:System.Array" /> to create.</param>
		/// <param name="length1">The size of the first dimension of the <see cref="T:System.Array" /> to create.</param>
		/// <param name="length2">The size of the second dimension of the <see cref="T:System.Array" /> to create.</param>
		/// <param name="length3">The size of the third dimension of the <see cref="T:System.Array" /> to create.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="elementType" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="elementType" /> is not a valid <see cref="T:System.Type" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="elementType" /> is not supported. For example, <see cref="T:System.Void" /> is not supported. -or-<paramref name="elementType" /> is an open generic type.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="length1" /> is less than zero.-or-<paramref name="length2" /> is less than zero.-or-<paramref name="length3" /> is less than zero.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600086E RID: 2158 RVA: 0x0002DC20 File Offset: 0x0002BE20
		public static Array CreateInstance(Type elementType, int length1, int length2, int length3)
		{
			int[] array = new int[] { length1, length2, length3 };
			return Array.CreateInstance(elementType, array);
		}

		/// <summary>Creates a multidimensional <see cref="T:System.Array" /> of the specified <see cref="T:System.Type" /> and dimension lengths, with zero-based indexing. The dimension lengths are specified in an array of 32-bit integers.</summary>
		/// <returns>A new multidimensional <see cref="T:System.Array" /> of the specified <see cref="T:System.Type" /> with the specified length for each dimension, using zero-based indexing.</returns>
		/// <param name="elementType">The <see cref="T:System.Type" /> of the <see cref="T:System.Array" /> to create.</param>
		/// <param name="lengths">An array of 32-bit integers that represent the size of each dimension of the <see cref="T:System.Array" /> to create.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="elementType" /> is null.-or-<paramref name="lengths" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="elementType" /> is not a valid <see cref="T:System.Type" />.-or-The <paramref name="lengths" /> array contains less than one element.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="elementType" /> is not supported. For example, <see cref="T:System.Void" /> is not supported. -or-<paramref name="elementType" /> is an open generic type.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Any value in <paramref name="lengths" /> is less than zero.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600086F RID: 2159 RVA: 0x0002DC48 File Offset: 0x0002BE48
		public static Array CreateInstance(Type elementType, params int[] lengths)
		{
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			if (lengths == null)
			{
				throw new ArgumentNullException("lengths");
			}
			if (lengths.Length > 255)
			{
				throw new TypeLoadException();
			}
			int[] array = null;
			elementType = elementType.UnderlyingSystemType as RuntimeType;
			if (elementType == null)
			{
				throw new ArgumentException("Type must be a type provided by the runtime.", "elementType");
			}
			if (elementType.Equals(typeof(void)))
			{
				throw new NotSupportedException("Array type can not be void");
			}
			if (elementType.ContainsGenericParameters)
			{
				throw new NotSupportedException("Array type can not be an open generic type");
			}
			return Array.CreateInstanceImpl(elementType, lengths, array);
		}

		/// <summary>Creates a multidimensional <see cref="T:System.Array" /> of the specified <see cref="T:System.Type" /> and dimension lengths, with the specified lower bounds.</summary>
		/// <returns>A new multidimensional <see cref="T:System.Array" /> of the specified <see cref="T:System.Type" /> with the specified length and lower bound for each dimension.</returns>
		/// <param name="elementType">The <see cref="T:System.Type" /> of the <see cref="T:System.Array" /> to create.</param>
		/// <param name="lengths">A one-dimensional array that contains the size of each dimension of the <see cref="T:System.Array" /> to create.</param>
		/// <param name="lowerBounds">A one-dimensional array that contains the lower bound (starting index) of each dimension of the <see cref="T:System.Array" /> to create.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="elementType" /> is null.-or-<paramref name="lengths" /> is null.-or-<paramref name="lowerBounds" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="elementType" /> is not a valid <see cref="T:System.Type" />.-or-The <paramref name="lengths" /> array contains less than one element.-or-The <paramref name="lengths" /> and <paramref name="lowerBounds" /> arrays do not contain the same number of elements.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="elementType" /> is not supported. For example, <see cref="T:System.Void" /> is not supported. -or-<paramref name="elementType" /> is an open generic type.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Any value in <paramref name="lengths" /> is less than zero.-or-Any value in <paramref name="lowerBounds" /> is very large, such that the sum of a dimension's lower bound and length is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000870 RID: 2160 RVA: 0x0002DCE8 File Offset: 0x0002BEE8
		public static Array CreateInstance(Type elementType, int[] lengths, int[] lowerBounds)
		{
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			if (lengths == null)
			{
				throw new ArgumentNullException("lengths");
			}
			if (lowerBounds == null)
			{
				throw new ArgumentNullException("lowerBounds");
			}
			elementType = elementType.UnderlyingSystemType as RuntimeType;
			if (elementType == null)
			{
				throw new ArgumentException("Type must be a type provided by the runtime.", "elementType");
			}
			if (elementType.Equals(typeof(void)))
			{
				throw new NotSupportedException("Array type can not be void");
			}
			if (elementType.ContainsGenericParameters)
			{
				throw new NotSupportedException("Array type can not be an open generic type");
			}
			if (lengths.Length < 1)
			{
				throw new ArgumentException(Locale.GetText("Arrays must contain >= 1 elements."));
			}
			if (lengths.Length != lowerBounds.Length)
			{
				throw new ArgumentException(Locale.GetText("Arrays must be of same size."));
			}
			for (int i = 0; i < lowerBounds.Length; i++)
			{
				if (lengths[i] < 0)
				{
					throw new ArgumentOutOfRangeException("lengths", Locale.GetText("Each value has to be >= 0."));
				}
				if ((long)lowerBounds[i] + (long)lengths[i] > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("lengths", Locale.GetText("Length + bound must not exceed Int32.MaxValue."));
				}
			}
			if (lengths.Length > 255)
			{
				throw new TypeLoadException();
			}
			return Array.CreateInstanceImpl(elementType, lengths, lowerBounds);
		}

		/// <summary>Sets a range of elements in the <see cref="T:System.Array" /> to zero, to false, or to null, depending on the element type.</summary>
		/// <param name="array">The <see cref="T:System.Array" /> whose elements need to be cleared.</param>
		/// <param name="index">The starting index of the range of elements to clear.</param>
		/// <param name="length">The number of elements to clear.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />.-or-<paramref name="length" /> is less than zero.-or-The sum of <paramref name="index" /> and <paramref name="length" /> is greater than the size of the <see cref="T:System.Array" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000871 RID: 2161 RVA: 0x0002DE10 File Offset: 0x0002C010
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void Clear(Array array, int index, int length)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (length < 0)
			{
				throw new IndexOutOfRangeException("length < 0");
			}
			int lowerBound = array.GetLowerBound(0);
			if (index < lowerBound)
			{
				throw new IndexOutOfRangeException("index < lower bound");
			}
			index -= lowerBound;
			if (index > array.Length - length)
			{
				throw new IndexOutOfRangeException("index + length > size");
			}
			Array.ClearInternal(array, index, length);
		}

		// Token: 0x06000872 RID: 2162
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearInternal(Array a, int index, int count);

		/// <summary>Copies a range of elements from an <see cref="T:System.Array" /> starting at the first element and pastes them into another <see cref="T:System.Array" /> starting at the first element. The length is specified as a 32-bit integer.</summary>
		/// <param name="sourceArray">The <see cref="T:System.Array" /> that contains the data to copy.</param>
		/// <param name="destinationArray">The <see cref="T:System.Array" /> that receives the data.</param>
		/// <param name="length">A 32-bit integer that represents the number of elements to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceArray" /> is null.-or-<paramref name="destinationArray" /> is null.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="sourceArray" /> and <paramref name="destinationArray" /> have different ranks.</exception>
		/// <exception cref="T:System.ArrayTypeMismatchException">
		///   <paramref name="sourceArray" /> and <paramref name="destinationArray" /> are of incompatible types.</exception>
		/// <exception cref="T:System.InvalidCastException">At least one element in <paramref name="sourceArray" /> cannot be cast to the type of <paramref name="destinationArray" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="length" /> is greater than the number of elements in <paramref name="sourceArray" />.-or-<paramref name="length" /> is greater than the number of elements in <paramref name="destinationArray" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000873 RID: 2163 RVA: 0x0002DE74 File Offset: 0x0002C074
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Copy(Array sourceArray, Array destinationArray, int length)
		{
			if (sourceArray == null)
			{
				throw new ArgumentNullException("sourceArray");
			}
			if (destinationArray == null)
			{
				throw new ArgumentNullException("destinationArray");
			}
			Array.Copy(sourceArray, sourceArray.GetLowerBound(0), destinationArray, destinationArray.GetLowerBound(0), length);
		}

		/// <summary>Copies a range of elements from an <see cref="T:System.Array" /> starting at the specified source index and pastes them to another <see cref="T:System.Array" /> starting at the specified destination index. The length and the indexes are specified as 32-bit integers.</summary>
		/// <param name="sourceArray">The <see cref="T:System.Array" /> that contains the data to copy.</param>
		/// <param name="sourceIndex">A 32-bit integer that represents the index in the <paramref name="sourceArray" /> at which copying begins.</param>
		/// <param name="destinationArray">The <see cref="T:System.Array" /> that receives the data.</param>
		/// <param name="destinationIndex">A 32-bit integer that represents the index in the <paramref name="destinationArray" /> at which storing begins.</param>
		/// <param name="length">A 32-bit integer that represents the number of elements to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceArray" /> is null.-or-<paramref name="destinationArray" /> is null.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="sourceArray" /> and <paramref name="destinationArray" /> have different ranks.</exception>
		/// <exception cref="T:System.ArrayTypeMismatchException">
		///   <paramref name="sourceArray" /> and <paramref name="destinationArray" /> are of incompatible types.</exception>
		/// <exception cref="T:System.InvalidCastException">At least one element in <paramref name="sourceArray" /> cannot be cast to the type of <paramref name="destinationArray" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="sourceIndex" /> is less than the lower bound of the first dimension of <paramref name="sourceArray" />.-or-<paramref name="destinationIndex" /> is less than the lower bound of the first dimension of <paramref name="destinationArray" />.-or-<paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="length" /> is greater than the number of elements from <paramref name="sourceIndex" /> to the end of <paramref name="sourceArray" />.-or-<paramref name="length" /> is greater than the number of elements from <paramref name="destinationIndex" /> to the end of <paramref name="destinationArray" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000874 RID: 2164 RVA: 0x0002DEA8 File Offset: 0x0002C0A8
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Copy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
		{
			if (sourceArray == null)
			{
				throw new ArgumentNullException("sourceArray");
			}
			if (destinationArray == null)
			{
				throw new ArgumentNullException("destinationArray");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Locale.GetText("Value has to be >= 0."));
			}
			if (sourceArray.Rank != destinationArray.Rank)
			{
				throw new RankException("Only single dimension arrays are supported here.");
			}
			if (sourceIndex < 0)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", Locale.GetText("Value has to be >= 0."));
			}
			if (destinationIndex < 0)
			{
				throw new ArgumentOutOfRangeException("destinationIndex", Locale.GetText("Value has to be >= 0."));
			}
			if (Array.FastCopy(sourceArray, sourceIndex, destinationArray, destinationIndex, length))
			{
				return;
			}
			int num = sourceIndex - sourceArray.GetLowerBound(0);
			int num2 = destinationIndex - destinationArray.GetLowerBound(0);
			if (num2 < 0)
			{
				throw new ArgumentOutOfRangeException("destinationIndex", "Index was less than the array's lower bound in the first dimension.");
			}
			if (num > sourceArray.Length - length)
			{
				throw new ArgumentException("length");
			}
			if (num2 > destinationArray.Length - length)
			{
				throw new ArgumentException("Destination array was not long enough. Check destIndex and length, and the array's lower bounds", string.Empty);
			}
			Type elementType = sourceArray.GetType().GetElementType();
			Type elementType2 = destinationArray.GetType().GetElementType();
			if (sourceArray != destinationArray || num > num2)
			{
				for (int i = 0; i < length; i++)
				{
					object valueImpl = sourceArray.GetValueImpl(num + i);
					try
					{
						destinationArray.SetValueImpl(valueImpl, num2 + i);
					}
					catch (ArgumentException)
					{
						throw Array.CreateArrayTypeMismatchException();
					}
					catch
					{
						if (Array.CanAssignArrayElement(elementType, elementType2))
						{
							throw;
						}
						throw Array.CreateArrayTypeMismatchException();
					}
				}
				return;
			}
			for (int j = length - 1; j >= 0; j--)
			{
				object valueImpl2 = sourceArray.GetValueImpl(num + j);
				try
				{
					destinationArray.SetValueImpl(valueImpl2, num2 + j);
				}
				catch (ArgumentException)
				{
					throw Array.CreateArrayTypeMismatchException();
				}
				catch
				{
					if (Array.CanAssignArrayElement(elementType, elementType2))
					{
						throw;
					}
					throw Array.CreateArrayTypeMismatchException();
				}
			}
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0002E080 File Offset: 0x0002C280
		private static Exception CreateArrayTypeMismatchException()
		{
			return new ArrayTypeMismatchException();
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0002E088 File Offset: 0x0002C288
		private static bool CanAssignArrayElement(Type source, Type target)
		{
			if (source.IsValueType)
			{
				return source.IsAssignableFrom(target);
			}
			if (source.IsInterface)
			{
				return !target.IsValueType;
			}
			if (target.IsInterface)
			{
				return !source.IsValueType;
			}
			return source.IsAssignableFrom(target) || target.IsAssignableFrom(source);
		}

		/// <summary>Copies a range of elements from an <see cref="T:System.Array" /> starting at the specified source index and pastes them to another <see cref="T:System.Array" /> starting at the specified destination index.  Guarantees that all changes are undone if the copy does not succeed completely.</summary>
		/// <param name="sourceArray">The <see cref="T:System.Array" /> that contains the data to copy.</param>
		/// <param name="sourceIndex">A 32-bit integer that represents the index in the <paramref name="sourceArray" /> at which copying begins.</param>
		/// <param name="destinationArray">The <see cref="T:System.Array" /> that receives the data.</param>
		/// <param name="destinationIndex">A 32-bit integer that represents the index in the <paramref name="destinationArray" /> at which storing begins.</param>
		/// <param name="length">A 32-bit integer that represents the number of elements to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceArray" /> is null.-or-<paramref name="destinationArray" /> is null.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="sourceArray" /> and <paramref name="destinationArray" /> have different ranks.</exception>
		/// <exception cref="T:System.ArrayTypeMismatchException">The <paramref name="sourceArray" /> type is neither the same as nor derived from the <paramref name="destinationArray" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">At least one element in <paramref name="sourceArray" /> cannot be cast to the type of <paramref name="destinationArray" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="sourceIndex" /> is less than the lower bound of the first dimension of <paramref name="sourceArray" />.-or-<paramref name="destinationIndex" /> is less than the lower bound of the first dimension of <paramref name="destinationArray" />.-or-<paramref name="length" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="length" /> is greater than the number of elements from <paramref name="sourceIndex" /> to the end of <paramref name="sourceArray" />.-or-<paramref name="length" /> is greater than the number of elements from <paramref name="destinationIndex" /> to the end of <paramref name="destinationArray" />.</exception>
		// Token: 0x06000877 RID: 2167 RVA: 0x0002E0DB File Offset: 0x0002C2DB
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void ConstrainedCopy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
		{
			Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length);
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0002E0E8 File Offset: 0x0002C2E8
		public static T[] Empty<T>()
		{
			return EmptyArray<T>.Value;
		}

		/// <summary>Initializes every element of the value-type <see cref="T:System.Array" /> by calling the default constructor of the value type.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000879 RID: 2169 RVA: 0x00002194 File Offset: 0x00000394
		public void Initialize()
		{
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0002E0EF File Offset: 0x0002C2EF
		private static int IndexOfImpl<T>(T[] array, T value, int startIndex, int count)
		{
			return EqualityComparer<T>.Default.IndexOf(array, value, startIndex, count);
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0002E0FF File Offset: 0x0002C2FF
		private static int LastIndexOfImpl<T>(T[] array, T value, int startIndex, int count)
		{
			return EqualityComparer<T>.Default.LastIndexOf(array, value, startIndex, count);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0002E110 File Offset: 0x0002C310
		private static void SortImpl(Array keys, Array items, int index, int length, IComparer comparer)
		{
			object[] array = keys as object[];
			object[] array2 = null;
			if (array != null)
			{
				array2 = items as object[];
			}
			if (array != null && (items == null || array2 != null))
			{
				Array.SorterObjectArray sorterObjectArray = new Array.SorterObjectArray(array, array2, comparer);
				sorterObjectArray.Sort(index, length);
				return;
			}
			Array.SorterGenericArray sorterGenericArray = new Array.SorterGenericArray(keys, items, comparer);
			sorterGenericArray.Sort(index, length);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0002E162 File Offset: 0x0002C362
		internal static T UnsafeLoad<T>(T[] array, int index)
		{
			return array[index];
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0002E16B File Offset: 0x0002C36B
		internal static void UnsafeStore<T>(T[] array, int index, T value)
		{
			array[index] = value;
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0002E175 File Offset: 0x0002C375
		internal static R UnsafeMov<S, R>(S instance)
		{
			return (R)((object)instance);
		}

		// Token: 0x020000E2 RID: 226
		private sealed class ArrayEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x06000880 RID: 2176 RVA: 0x0002E182 File Offset: 0x0002C382
			internal ArrayEnumerator(Array array)
			{
				this._array = array;
				this._index = -1;
				this._endIndex = array.Length;
			}

			// Token: 0x06000881 RID: 2177 RVA: 0x0002E1A4 File Offset: 0x0002C3A4
			public bool MoveNext()
			{
				if (this._index < this._endIndex)
				{
					this._index++;
					return this._index < this._endIndex;
				}
				return false;
			}

			// Token: 0x06000882 RID: 2178 RVA: 0x0002E1D2 File Offset: 0x0002C3D2
			public void Reset()
			{
				this._index = -1;
			}

			// Token: 0x06000883 RID: 2179 RVA: 0x0002C3A3 File Offset: 0x0002A5A3
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x17000148 RID: 328
			// (get) Token: 0x06000884 RID: 2180 RVA: 0x0002E1DC File Offset: 0x0002C3DC
			public object Current
			{
				get
				{
					if (this._index < 0)
					{
						throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
					}
					if (this._index >= this._endIndex)
					{
						throw new InvalidOperationException("Enumeration already finished.");
					}
					if (this._index == 0 && this._array.GetType().GetElementType().IsPointer)
					{
						throw new NotSupportedException("Type is not supported.");
					}
					return this._array.GetValueImpl(this._index);
				}
			}

			// Token: 0x040006CA RID: 1738
			private Array _array;

			// Token: 0x040006CB RID: 1739
			private int _index;

			// Token: 0x040006CC RID: 1740
			private int _endIndex;
		}

		// Token: 0x020000E3 RID: 227
		internal struct InternalEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06000885 RID: 2181 RVA: 0x0002E251 File Offset: 0x0002C451
			internal InternalEnumerator(Array array)
			{
				this.array = array;
				this.idx = -2;
			}

			// Token: 0x06000886 RID: 2182 RVA: 0x00002194 File Offset: 0x00000394
			public void Dispose()
			{
			}

			// Token: 0x06000887 RID: 2183 RVA: 0x0002E264 File Offset: 0x0002C464
			public bool MoveNext()
			{
				if (this.idx == -2)
				{
					this.idx = this.array.Length;
				}
				if (this.idx != -1)
				{
					int num = this.idx - 1;
					this.idx = num;
					return num != -1;
				}
				return false;
			}

			// Token: 0x17000149 RID: 329
			// (get) Token: 0x06000888 RID: 2184 RVA: 0x0002E2B0 File Offset: 0x0002C4B0
			public T Current
			{
				get
				{
					if (this.idx == -2)
					{
						throw new InvalidOperationException("Enumeration has not started. Call MoveNext");
					}
					if (this.idx == -1)
					{
						throw new InvalidOperationException("Enumeration already finished");
					}
					return this.array.InternalArray__get_Item<T>(this.array.Length - 1 - this.idx);
				}
			}

			// Token: 0x06000889 RID: 2185 RVA: 0x0002E305 File Offset: 0x0002C505
			void IEnumerator.Reset()
			{
				this.idx = -2;
			}

			// Token: 0x1700014A RID: 330
			// (get) Token: 0x0600088A RID: 2186 RVA: 0x0002E30F File Offset: 0x0002C50F
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040006CD RID: 1741
			private const int NOT_STARTED = -2;

			// Token: 0x040006CE RID: 1742
			private const int FINISHED = -1;

			// Token: 0x040006CF RID: 1743
			private readonly Array array;

			// Token: 0x040006D0 RID: 1744
			private int idx;
		}

		// Token: 0x020000E4 RID: 228
		internal class EmptyInternalEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x0600088B RID: 2187 RVA: 0x00002194 File Offset: 0x00000394
			public void Dispose()
			{
			}

			// Token: 0x0600088C RID: 2188 RVA: 0x00015ED5 File Offset: 0x000140D5
			public bool MoveNext()
			{
				return false;
			}

			// Token: 0x1700014B RID: 331
			// (get) Token: 0x0600088D RID: 2189 RVA: 0x0002E31C File Offset: 0x0002C51C
			public T Current
			{
				get
				{
					throw new InvalidOperationException("Enumeration has not started. Call MoveNext");
				}
			}

			// Token: 0x1700014C RID: 332
			// (get) Token: 0x0600088E RID: 2190 RVA: 0x0002E328 File Offset: 0x0002C528
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600088F RID: 2191 RVA: 0x00002194 File Offset: 0x00000394
			void IEnumerator.Reset()
			{
			}

			// Token: 0x040006D1 RID: 1745
			public static readonly Array.EmptyInternalEnumerator<T> Value = new Array.EmptyInternalEnumerator<T>();
		}

		// Token: 0x020000E5 RID: 229
		internal sealed class FunctorComparer<T> : IComparer<T>
		{
			// Token: 0x06000892 RID: 2194 RVA: 0x0002E341 File Offset: 0x0002C541
			public FunctorComparer(Comparison<T> comparison)
			{
				this.comparison = comparison;
			}

			// Token: 0x06000893 RID: 2195 RVA: 0x0002E350 File Offset: 0x0002C550
			public int Compare(T x, T y)
			{
				return this.comparison(x, y);
			}

			// Token: 0x040006D2 RID: 1746
			private Comparison<T> comparison;
		}

		// Token: 0x020000E6 RID: 230
		private struct SorterObjectArray
		{
			// Token: 0x06000894 RID: 2196 RVA: 0x0002E35F File Offset: 0x0002C55F
			internal SorterObjectArray(object[] keys, object[] items, IComparer comparer)
			{
				if (comparer == null)
				{
					comparer = Comparer.Default;
				}
				this.keys = keys;
				this.items = items;
				this.comparer = comparer;
			}

			// Token: 0x06000895 RID: 2197 RVA: 0x0002E380 File Offset: 0x0002C580
			internal void SwapIfGreaterWithItems(int a, int b)
			{
				if (a != b && this.comparer.Compare(this.keys[a], this.keys[b]) > 0)
				{
					object obj = this.keys[a];
					this.keys[a] = this.keys[b];
					this.keys[b] = obj;
					if (this.items != null)
					{
						object obj2 = this.items[a];
						this.items[a] = this.items[b];
						this.items[b] = obj2;
					}
				}
			}

			// Token: 0x06000896 RID: 2198 RVA: 0x0002E3FC File Offset: 0x0002C5FC
			private void Swap(int i, int j)
			{
				object obj = this.keys[i];
				this.keys[i] = this.keys[j];
				this.keys[j] = obj;
				if (this.items != null)
				{
					object obj2 = this.items[i];
					this.items[i] = this.items[j];
					this.items[j] = obj2;
				}
			}

			// Token: 0x06000897 RID: 2199 RVA: 0x0002E455 File Offset: 0x0002C655
			internal void Sort(int left, int length)
			{
				this.IntrospectiveSort(left, length);
			}

			// Token: 0x06000898 RID: 2200 RVA: 0x0002E460 File Offset: 0x0002C660
			private void IntrospectiveSort(int left, int length)
			{
				if (length < 2)
				{
					return;
				}
				try
				{
					this.IntroSort(left, length + left - 1, 2 * IntrospectiveSortUtilities.FloorLog2(this.keys.Length));
				}
				catch (IndexOutOfRangeException)
				{
					IntrospectiveSortUtilities.ThrowOrIgnoreBadComparer(this.comparer);
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException("Failed to compare two elements in the array.", ex);
				}
			}

			// Token: 0x06000899 RID: 2201 RVA: 0x0002E4C8 File Offset: 0x0002C6C8
			private void IntroSort(int lo, int hi, int depthLimit)
			{
				while (hi > lo)
				{
					int num = hi - lo + 1;
					if (num <= 16)
					{
						if (num == 1)
						{
							return;
						}
						if (num == 2)
						{
							this.SwapIfGreaterWithItems(lo, hi);
							return;
						}
						if (num == 3)
						{
							this.SwapIfGreaterWithItems(lo, hi - 1);
							this.SwapIfGreaterWithItems(lo, hi);
							this.SwapIfGreaterWithItems(hi - 1, hi);
							return;
						}
						this.InsertionSort(lo, hi);
						return;
					}
					else
					{
						if (depthLimit == 0)
						{
							this.Heapsort(lo, hi);
							return;
						}
						depthLimit--;
						int num2 = this.PickPivotAndPartition(lo, hi);
						this.IntroSort(num2 + 1, hi, depthLimit);
						hi = num2 - 1;
					}
				}
			}

			// Token: 0x0600089A RID: 2202 RVA: 0x0002E54C File Offset: 0x0002C74C
			private int PickPivotAndPartition(int lo, int hi)
			{
				int num = lo + (hi - lo) / 2;
				this.SwapIfGreaterWithItems(lo, num);
				this.SwapIfGreaterWithItems(lo, hi);
				this.SwapIfGreaterWithItems(num, hi);
				object obj = this.keys[num];
				this.Swap(num, hi - 1);
				int i = lo;
				int num2 = hi - 1;
				while (i < num2)
				{
					while (this.comparer.Compare(this.keys[++i], obj) < 0)
					{
					}
					while (this.comparer.Compare(obj, this.keys[--num2]) < 0)
					{
					}
					if (i >= num2)
					{
						break;
					}
					this.Swap(i, num2);
				}
				this.Swap(i, hi - 1);
				return i;
			}

			// Token: 0x0600089B RID: 2203 RVA: 0x0002E5E8 File Offset: 0x0002C7E8
			private void Heapsort(int lo, int hi)
			{
				int num = hi - lo + 1;
				for (int i = num / 2; i >= 1; i--)
				{
					this.DownHeap(i, num, lo);
				}
				for (int j = num; j > 1; j--)
				{
					this.Swap(lo, lo + j - 1);
					this.DownHeap(1, j - 1, lo);
				}
			}

			// Token: 0x0600089C RID: 2204 RVA: 0x0002E638 File Offset: 0x0002C838
			private void DownHeap(int i, int n, int lo)
			{
				object obj = this.keys[lo + i - 1];
				object obj2 = ((this.items != null) ? this.items[lo + i - 1] : null);
				while (i <= n / 2)
				{
					int num = 2 * i;
					if (num < n && this.comparer.Compare(this.keys[lo + num - 1], this.keys[lo + num]) < 0)
					{
						num++;
					}
					if (this.comparer.Compare(obj, this.keys[lo + num - 1]) >= 0)
					{
						break;
					}
					this.keys[lo + i - 1] = this.keys[lo + num - 1];
					if (this.items != null)
					{
						this.items[lo + i - 1] = this.items[lo + num - 1];
					}
					i = num;
				}
				this.keys[lo + i - 1] = obj;
				if (this.items != null)
				{
					this.items[lo + i - 1] = obj2;
				}
			}

			// Token: 0x0600089D RID: 2205 RVA: 0x0002E720 File Offset: 0x0002C920
			private void InsertionSort(int lo, int hi)
			{
				for (int i = lo; i < hi; i++)
				{
					int num = i;
					object obj = this.keys[i + 1];
					object obj2 = ((this.items != null) ? this.items[i + 1] : null);
					while (num >= lo && this.comparer.Compare(obj, this.keys[num]) < 0)
					{
						this.keys[num + 1] = this.keys[num];
						if (this.items != null)
						{
							this.items[num + 1] = this.items[num];
						}
						num--;
					}
					this.keys[num + 1] = obj;
					if (this.items != null)
					{
						this.items[num + 1] = obj2;
					}
				}
			}

			// Token: 0x040006D3 RID: 1747
			private object[] keys;

			// Token: 0x040006D4 RID: 1748
			private object[] items;

			// Token: 0x040006D5 RID: 1749
			private IComparer comparer;
		}

		// Token: 0x020000E7 RID: 231
		private struct SorterGenericArray
		{
			// Token: 0x0600089E RID: 2206 RVA: 0x0002E7CD File Offset: 0x0002C9CD
			internal SorterGenericArray(Array keys, Array items, IComparer comparer)
			{
				if (comparer == null)
				{
					comparer = Comparer.Default;
				}
				this.keys = keys;
				this.items = items;
				this.comparer = comparer;
			}

			// Token: 0x0600089F RID: 2207 RVA: 0x0002E7F0 File Offset: 0x0002C9F0
			internal void SwapIfGreaterWithItems(int a, int b)
			{
				if (a != b && this.comparer.Compare(this.keys.GetValue(a), this.keys.GetValue(b)) > 0)
				{
					object value = this.keys.GetValue(a);
					this.keys.SetValue(this.keys.GetValue(b), a);
					this.keys.SetValue(value, b);
					if (this.items != null)
					{
						object value2 = this.items.GetValue(a);
						this.items.SetValue(this.items.GetValue(b), a);
						this.items.SetValue(value2, b);
					}
				}
			}

			// Token: 0x060008A0 RID: 2208 RVA: 0x0002E898 File Offset: 0x0002CA98
			private void Swap(int i, int j)
			{
				object value = this.keys.GetValue(i);
				this.keys.SetValue(this.keys.GetValue(j), i);
				this.keys.SetValue(value, j);
				if (this.items != null)
				{
					object value2 = this.items.GetValue(i);
					this.items.SetValue(this.items.GetValue(j), i);
					this.items.SetValue(value2, j);
				}
			}

			// Token: 0x060008A1 RID: 2209 RVA: 0x0002E911 File Offset: 0x0002CB11
			internal void Sort(int left, int length)
			{
				this.IntrospectiveSort(left, length);
			}

			// Token: 0x060008A2 RID: 2210 RVA: 0x0002E91C File Offset: 0x0002CB1C
			private void IntrospectiveSort(int left, int length)
			{
				if (length < 2)
				{
					return;
				}
				try
				{
					this.IntroSort(left, length + left - 1, 2 * IntrospectiveSortUtilities.FloorLog2(this.keys.Length));
				}
				catch (IndexOutOfRangeException)
				{
					IntrospectiveSortUtilities.ThrowOrIgnoreBadComparer(this.comparer);
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException("Failed to compare two elements in the array.", ex);
				}
			}

			// Token: 0x060008A3 RID: 2211 RVA: 0x0002E988 File Offset: 0x0002CB88
			private void IntroSort(int lo, int hi, int depthLimit)
			{
				while (hi > lo)
				{
					int num = hi - lo + 1;
					if (num <= 16)
					{
						if (num == 1)
						{
							return;
						}
						if (num == 2)
						{
							this.SwapIfGreaterWithItems(lo, hi);
							return;
						}
						if (num == 3)
						{
							this.SwapIfGreaterWithItems(lo, hi - 1);
							this.SwapIfGreaterWithItems(lo, hi);
							this.SwapIfGreaterWithItems(hi - 1, hi);
							return;
						}
						this.InsertionSort(lo, hi);
						return;
					}
					else
					{
						if (depthLimit == 0)
						{
							this.Heapsort(lo, hi);
							return;
						}
						depthLimit--;
						int num2 = this.PickPivotAndPartition(lo, hi);
						this.IntroSort(num2 + 1, hi, depthLimit);
						hi = num2 - 1;
					}
				}
			}

			// Token: 0x060008A4 RID: 2212 RVA: 0x0002EA0C File Offset: 0x0002CC0C
			private int PickPivotAndPartition(int lo, int hi)
			{
				int num = lo + (hi - lo) / 2;
				this.SwapIfGreaterWithItems(lo, num);
				this.SwapIfGreaterWithItems(lo, hi);
				this.SwapIfGreaterWithItems(num, hi);
				object value = this.keys.GetValue(num);
				this.Swap(num, hi - 1);
				int i = lo;
				int num2 = hi - 1;
				while (i < num2)
				{
					while (this.comparer.Compare(this.keys.GetValue(++i), value) < 0)
					{
					}
					while (this.comparer.Compare(value, this.keys.GetValue(--num2)) < 0)
					{
					}
					if (i >= num2)
					{
						break;
					}
					this.Swap(i, num2);
				}
				this.Swap(i, hi - 1);
				return i;
			}

			// Token: 0x060008A5 RID: 2213 RVA: 0x0002EAB4 File Offset: 0x0002CCB4
			private void Heapsort(int lo, int hi)
			{
				int num = hi - lo + 1;
				for (int i = num / 2; i >= 1; i--)
				{
					this.DownHeap(i, num, lo);
				}
				for (int j = num; j > 1; j--)
				{
					this.Swap(lo, lo + j - 1);
					this.DownHeap(1, j - 1, lo);
				}
			}

			// Token: 0x060008A6 RID: 2214 RVA: 0x0002EB04 File Offset: 0x0002CD04
			private void DownHeap(int i, int n, int lo)
			{
				object value = this.keys.GetValue(lo + i - 1);
				object obj = ((this.items != null) ? this.items.GetValue(lo + i - 1) : null);
				while (i <= n / 2)
				{
					int num = 2 * i;
					if (num < n && this.comparer.Compare(this.keys.GetValue(lo + num - 1), this.keys.GetValue(lo + num)) < 0)
					{
						num++;
					}
					if (this.comparer.Compare(value, this.keys.GetValue(lo + num - 1)) >= 0)
					{
						break;
					}
					this.keys.SetValue(this.keys.GetValue(lo + num - 1), lo + i - 1);
					if (this.items != null)
					{
						this.items.SetValue(this.items.GetValue(lo + num - 1), lo + i - 1);
					}
					i = num;
				}
				this.keys.SetValue(value, lo + i - 1);
				if (this.items != null)
				{
					this.items.SetValue(obj, lo + i - 1);
				}
			}

			// Token: 0x060008A7 RID: 2215 RVA: 0x0002EC18 File Offset: 0x0002CE18
			private void InsertionSort(int lo, int hi)
			{
				for (int i = lo; i < hi; i++)
				{
					int num = i;
					object value = this.keys.GetValue(i + 1);
					object obj = ((this.items != null) ? this.items.GetValue(i + 1) : null);
					while (num >= lo && this.comparer.Compare(value, this.keys.GetValue(num)) < 0)
					{
						this.keys.SetValue(this.keys.GetValue(num), num + 1);
						if (this.items != null)
						{
							this.items.SetValue(this.items.GetValue(num), num + 1);
						}
						num--;
					}
					this.keys.SetValue(value, num + 1);
					if (this.items != null)
					{
						this.items.SetValue(obj, num + 1);
					}
				}
			}

			// Token: 0x040006D6 RID: 1750
			private Array keys;

			// Token: 0x040006D7 RID: 1751
			private Array items;

			// Token: 0x040006D8 RID: 1752
			private IComparer comparer;
		}
	}
}
