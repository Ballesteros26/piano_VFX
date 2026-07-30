using System;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Web
{
	/// <summary>Serves as the base class for classes that provide a collection of application-scoped objects for the <see cref="P:System.Web.HttpApplicationState.StaticObjects" /> property.</summary>
	// Token: 0x02000040 RID: 64
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public abstract class HttpStaticObjectsCollectionBase : ICollection, IEnumerable
	{
		/// <summary>When overridden in a derived class, gets the number of objects in the collection.</summary>
		/// <returns>The number of objects in the collection.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual int Count
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the collection is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool IsReadOnly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the collection is thread-safe.</summary>
		/// <returns>true if the collection is thread-safe; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool IsSynchronized
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the object that has the specified name from the collection.</summary>
		/// <returns>The object that is specified by <paramref name="name" />.</returns>
		/// <param name="name">The case-insensitive name of the object to get.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x170001A0 RID: 416
		public virtual object this[string name]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the collection has been accessed.</summary>
		/// <returns>true if the collection has never been accessed; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool NeverAccessed
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual object SyncRoot
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, copies the elements of the collection to an array, starting at the specified index in the array.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements that are copied from the collection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which to begin copying.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x060003A6 RID: 934 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, returns an enumerator that can be used to iterate through the collection.</summary>
		/// <returns>An object that can be used to iterate through the collection.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x060003A7 RID: 935 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, returns the object that has the specified name from the collection.</summary>
		/// <returns>The object that is specified by <paramref name="name" />.</returns>
		/// <param name="name">The case-insensitive name of the object to return.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x060003A8 RID: 936 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual object GetObject(string name)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, writes the contents of the collection to a <see cref="T:System.IO.BinaryWriter" /> object.</summary>
		/// <param name="writer">The object to use to write the serialized collection to a stream or encoded string.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x060003A9 RID: 937 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void Serialize(BinaryWriter writer)
		{
			throw new NotImplementedException();
		}
	}
}
