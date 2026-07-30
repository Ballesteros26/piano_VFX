using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace System.Web
{
	/// <summary>Serves as the base class for classes that provide access to files that were uploaded by a client.</summary>
	// Token: 0x02000038 RID: 56
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public abstract class HttpFileCollectionBase : NameObjectCollectionBase, ICollection, IEnumerable
	{
		/// <summary>When overridden in a derived class, gets an array that contains the keys (names) of all posted file objects in the collection.</summary>
		/// <returns>An array of file names.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600027D RID: 637 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string[] AllKeys
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the number of posted file objects in the collection.</summary>
		/// <returns>The number of objects in the collection.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00003A1F File Offset: 0x00001C1F
		public override int Count
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether access to the collection is thread-safe.</summary>
		/// <returns>true if access is synchronized (thread-safe); otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600027F RID: 639 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool IsSynchronized
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual object SyncRoot
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the posted file object that has the specified name from the collection.</summary>
		/// <returns>The posted file object that is specified by <paramref name="name" />.</returns>
		/// <param name="name">The name of the object to return.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000124 RID: 292
		public virtual HttpPostedFileBase this[string name]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the posted file object at the specified index.</summary>
		/// <returns>The posted file object specified by <paramref name="index" />.</returns>
		/// <param name="index">The index of the object to get.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000125 RID: 293
		public virtual HttpPostedFileBase this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, copies the elements of the collection to an array, starting at the specified index in the array.</summary>
		/// <param name="dest">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying starts.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000283 RID: 643 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void CopyTo(Array dest, int index)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, returns the posted file object at the specified index.</summary>
		/// <returns>The posted file object specified by <paramref name="index" />.</returns>
		/// <param name="index">The index of the object to return.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000284 RID: 644 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual HttpPostedFileBase Get(int index)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, returns the posted file object that has the specified name from the collection.</summary>
		/// <returns>The posted file object that is specified by <paramref name="name" />.</returns>
		/// <param name="name">The name of the object to return.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000285 RID: 645 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual HttpPostedFileBase Get(string name)
		{
			throw new NotImplementedException();
		}

		/// <summary>When implemented in a derived class, returns all files that match the specified name.</summary>
		/// <returns>The collection of files.</returns>
		/// <param name="name">The name to match.</param>
		// Token: 0x06000286 RID: 646 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual IList<HttpPostedFileBase> GetMultiple(string name)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, returns an enumerator that can be used to iterate through the collection.</summary>
		/// <returns>An object that can be used to iterate through the collection.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000287 RID: 647 RVA: 0x00003A1F File Offset: 0x00001C1F
		public override IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, returns the name of the posted file object at the specified index.</summary>
		/// <returns>The name of the posted file object that is specified by <paramref name="index" />.</returns>
		/// <param name="index">The index of the object name to return.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000288 RID: 648 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string GetKey(int index)
		{
			throw new NotImplementedException();
		}
	}
}
