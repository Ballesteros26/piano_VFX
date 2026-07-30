using System;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Web
{
	/// <summary>Encapsulates the HTTP intrinsic object that provides a collection of application-scoped objects for the <see cref="P:System.Web.HttpApplicationState.StaticObjects" /> property.</summary>
	// Token: 0x02000041 RID: 65
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class HttpStaticObjectsCollectionWrapper : HttpStaticObjectsCollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpStaticObjectsCollectionWrapper" /> class. </summary>
		/// <param name="httpStaticObjectsCollection">The object that this wrapper class provides access to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="httpStaticObjectsCollection" /> is null.</exception>
		// Token: 0x060003AB RID: 939 RVA: 0x0000718B File Offset: 0x0000538B
		public HttpStaticObjectsCollectionWrapper(HttpStaticObjectsCollection httpStaticObjectsCollection)
		{
			if (httpStaticObjectsCollection == null)
			{
				throw new ArgumentNullException("httpStaticObjectsCollection");
			}
			this._collection = httpStaticObjectsCollection;
		}

		/// <summary>Gets the number of objects in the collection.</summary>
		/// <returns>The number of objects in the collection.</returns>
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060003AC RID: 940 RVA: 0x000071A8 File Offset: 0x000053A8
		public override int Count
		{
			get
			{
				return this._collection.Count;
			}
		}

		/// <summary>Gets a value that indicates whether the collection is read-only.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060003AD RID: 941 RVA: 0x000071B5 File Offset: 0x000053B5
		public override bool IsReadOnly
		{
			get
			{
				return this._collection.IsReadOnly;
			}
		}

		/// <summary>Gets a value that indicates whether the collection is thread-safe.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060003AE RID: 942 RVA: 0x000071C2 File Offset: 0x000053C2
		public override bool IsSynchronized
		{
			get
			{
				return this._collection.IsSynchronized;
			}
		}

		/// <summary>Gets the object that has the specified name from the collection.</summary>
		/// <returns>The object that is specified by <paramref name="name" />, if found; otherwise, null.</returns>
		/// <param name="name">The case-insensitive name of the object to get.</param>
		// Token: 0x170001A6 RID: 422
		public override object this[string name]
		{
			get
			{
				return this._collection[name];
			}
		}

		/// <summary>Gets a value that indicates whether the collection has been accessed.</summary>
		/// <returns>true if the collection has never been accessed; otherwise, false.</returns>
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x000071DD File Offset: 0x000053DD
		public override bool NeverAccessed
		{
			get
			{
				return this._collection.NeverAccessed;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>The current instance of the <see cref="T:System.Web.HttpStaticObjectsCollection" /> class that is wrapped by this class.</returns>
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x000071EA File Offset: 0x000053EA
		public override object SyncRoot
		{
			get
			{
				return this._collection.SyncRoot;
			}
		}

		/// <summary>Copies the elements of the collection to an array, starting at the specified index in the array.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements that are copied from the collection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which to begin copying.</param>
		// Token: 0x060003B2 RID: 946 RVA: 0x000071F7 File Offset: 0x000053F7
		public override void CopyTo(Array array, int index)
		{
			this._collection.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that can be used to iterate through the collection.</summary>
		/// <returns>An object that can be used to iterate through the collection.</returns>
		// Token: 0x060003B3 RID: 947 RVA: 0x00007206 File Offset: 0x00005406
		public override IEnumerator GetEnumerator()
		{
			return this._collection.GetEnumerator();
		}

		/// <summary>Returns the object that has the specified name from the collection.</summary>
		/// <returns>The object that is specified by <paramref name="name" />, if found; otherwise, null.</returns>
		/// <param name="name">The case-insensitive name of the object to return.</param>
		// Token: 0x060003B4 RID: 948 RVA: 0x00007213 File Offset: 0x00005413
		public override object GetObject(string name)
		{
			return this._collection.GetObject(name);
		}

		/// <summary>Writes the contents of the collection to a <see cref="T:System.IO.BinaryWriter" /> object.</summary>
		/// <param name="writer">The object to use to write the serialized collection to a stream or encoded string.</param>
		// Token: 0x060003B5 RID: 949 RVA: 0x00007221 File Offset: 0x00005421
		public override void Serialize(BinaryWriter writer)
		{
			this._collection.Serialize(writer);
		}

		// Token: 0x04000D9F RID: 3487
		private HttpStaticObjectsCollection _collection;
	}
}
