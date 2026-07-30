using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web
{
	/// <summary>Encapsulates the HTTP intrinsic object that enables information to be shared across multiple requests and sessions within an ASP.NET application.</summary>
	// Token: 0x02000033 RID: 51
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class HttpApplicationStateWrapper : HttpApplicationStateBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpApplicationStateWrapper" /> class. </summary>
		/// <param name="httpApplicationState">The object that this wrapper class provides access to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="httpApplicationState" /> is null.</exception>
		// Token: 0x0600013D RID: 317 RVA: 0x00006665 File Offset: 0x00004865
		public HttpApplicationStateWrapper(HttpApplicationState httpApplicationState)
		{
			if (httpApplicationState == null)
			{
				throw new ArgumentNullException("httpApplicationState");
			}
			this._application = httpApplicationState;
		}

		/// <summary>Gets the keys for the objects in the collection.</summary>
		/// <returns>An array of state object keys.</returns>
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00006682 File Offset: 0x00004882
		public override string[] AllKeys
		{
			get
			{
				return this._application.AllKeys;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.HttpApplicationStateBase" /> object.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.HttpApplicationState" /> object.</returns>
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00002058 File Offset: 0x00000258
		public override HttpApplicationStateBase Contents
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets the number of objects in the collection.</summary>
		/// <returns>The number of objects in the collection.</returns>
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0000668F File Offset: 0x0000488F
		public override int Count
		{
			get
			{
				return this._application.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to the collection is thread-safe.</summary>
		/// <returns>true if access is synchronized (thread-safe); otherwise, false. The default is false.</returns>
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000669C File Offset: 0x0000489C
		public override bool IsSynchronized
		{
			get
			{
				return ((ICollection)this._application).IsSynchronized;
			}
		}

		/// <summary>Gets a <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> instance that contains all the keys in the <see cref="T:System.Web.HttpApplicationStateWrapper" /> instance.</summary>
		/// <returns>A collection of all the keys in the collection.</returns>
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000066A9 File Offset: 0x000048A9
		public override NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				return this._application.Keys;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000066B6 File Offset: 0x000048B6
		public override object SyncRoot
		{
			get
			{
				return ((ICollection)this._application).SyncRoot;
			}
		}

		/// <summary>Gets a state object by index.</summary>
		/// <returns>The object referenced by <paramref name="index" />.</returns>
		/// <param name="index">The index of the object in the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
		// Token: 0x17000035 RID: 53
		public override object this[int index]
		{
			get
			{
				return this._application[index];
			}
		}

		/// <summary>Gets a state object by name.</summary>
		/// <returns>The object referenced by <paramref name="name" />, if found; otherwise, null.</returns>
		/// <param name="name">The name of the object in the collection.</param>
		// Token: 0x17000036 RID: 54
		public override object this[string name]
		{
			get
			{
				return this._application[name];
			}
			set
			{
				this._application[name] = value;
			}
		}

		/// <summary>Gets all objects that are declared by an object element where the scope is set to "Application" in the ASP.NET application.</summary>
		/// <returns>A collection of objects in the application.</returns>
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000066EE File Offset: 0x000048EE
		public override HttpStaticObjectsCollectionBase StaticObjects
		{
			get
			{
				return new HttpStaticObjectsCollectionWrapper(this._application.StaticObjects);
			}
		}

		/// <summary>Adds an object to the collection.</summary>
		/// <param name="name">The name of the object to add to the collection.</param>
		/// <param name="value">The value of the object.</param>
		// Token: 0x06000148 RID: 328 RVA: 0x00006700 File Offset: 0x00004900
		public override void Add(string name, object value)
		{
			this._application.Add(name, value);
		}

		/// <summary>Removes all objects from the collection.</summary>
		// Token: 0x06000149 RID: 329 RVA: 0x0000670F File Offset: 0x0000490F
		public override void Clear()
		{
			this._application.Clear();
		}

		/// <summary>Copies the elements of the collection to an array, starting at the specified index in the array.</summary>
		/// <param name="array">The one-dimensional array that is the destination for the elements that are copied from the collection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which to begin copying.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-The number of elements in the source <see cref="T:System.Web.HttpApplicationStateWrapper" /> object is greater than the available space from <paramref name="index" /> to the end of the destination array.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Web.HttpApplicationStateWrapper" /> object cannot be cast to the type of the destination array.</exception>
		// Token: 0x0600014A RID: 330 RVA: 0x0000671C File Offset: 0x0000491C
		public override void CopyTo(Array array, int index)
		{
			((ICollection)this._application).CopyTo(array, index);
		}

		/// <summary>Returns a state object by index.</summary>
		/// <returns>The object referenced by <paramref name="index" />.</returns>
		/// <param name="index">The index of the application state object to get.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
		// Token: 0x0600014B RID: 331 RVA: 0x0000672B File Offset: 0x0000492B
		public override object Get(int index)
		{
			return this._application.Get(index);
		}

		/// <summary>Returns a state object by name.</summary>
		/// <returns>The object referenced by <paramref name="name" />, if found; otherwise, null.</returns>
		/// <param name="name">The name of the object to get.</param>
		// Token: 0x0600014C RID: 332 RVA: 0x00006739 File Offset: 0x00004939
		public override object Get(string name)
		{
			return this._application.Get(name);
		}

		/// <summary>Returns an enumerator that can be used to iterate through a collection.</summary>
		/// <returns>An object that can be used to iterate through the collection.</returns>
		// Token: 0x0600014D RID: 333 RVA: 0x00006747 File Offset: 0x00004947
		public override IEnumerator GetEnumerator()
		{
			return ((IEnumerable)this._application).GetEnumerator();
		}

		/// <summary>Returns the name of a state object by index.</summary>
		/// <returns>The name of the application state object.</returns>
		/// <param name="index">The index of the application state object to get.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
		// Token: 0x0600014E RID: 334 RVA: 0x00006754 File Offset: 0x00004954
		public override string GetKey(int index)
		{
			return this._application.GetKey(index);
		}

		/// <summary>Returns the data that is necessary to serialize the <see cref="T:System.Web.HttpApplicationStateWrapper" /> object.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information that is required to serialize the <see cref="T:System.Web.HttpApplicationStateWrapper" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream that is associated with the <see cref="T:System.Web.HttpApplicationStateWrapper" /> object.</param>
		// Token: 0x0600014F RID: 335 RVA: 0x00006762 File Offset: 0x00004962
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this._application.GetObjectData(info, context);
		}

		/// <summary>Locks access to objects in the collection in order to enable synchronized access.</summary>
		// Token: 0x06000150 RID: 336 RVA: 0x00006771 File Offset: 0x00004971
		public override void Lock()
		{
			this._application.Lock();
		}

		/// <summary>Raises the deserialization event when deserialization is finished.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that is associated with the current <see cref="T:System.Web.HttpApplicationStateWrapper" /> instance is invalid.</exception>
		// Token: 0x06000151 RID: 337 RVA: 0x0000677E File Offset: 0x0000497E
		public override void OnDeserialization(object sender)
		{
			this._application.OnDeserialization(sender);
		}

		/// <summary>Removes the object specified by name from the collection.</summary>
		/// <param name="name">The name of the object to remove from the collection.</param>
		// Token: 0x06000152 RID: 338 RVA: 0x0000678C File Offset: 0x0000498C
		public override void Remove(string name)
		{
			this._application.Remove(name);
		}

		/// <summary>Removes all objects from the collection.</summary>
		// Token: 0x06000153 RID: 339 RVA: 0x0000679A File Offset: 0x0000499A
		public override void RemoveAll()
		{
			this._application.RemoveAll();
		}

		/// <summary>Removes the object specified by index from the collection.</summary>
		/// <param name="index">The position in the collection of the item to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
		// Token: 0x06000154 RID: 340 RVA: 0x000067A7 File Offset: 0x000049A7
		public override void RemoveAt(int index)
		{
			this._application.RemoveAt(index);
		}

		/// <summary>Updates the value of an object in the collection.</summary>
		/// <param name="name">The name of the object to update.</param>
		/// <param name="value">The updated value of the object.</param>
		// Token: 0x06000155 RID: 341 RVA: 0x000067B5 File Offset: 0x000049B5
		public override void Set(string name, object value)
		{
			this._application.Set(name, value);
		}

		/// <summary>Unlocks access to objects in the collection to enable synchronized access.</summary>
		// Token: 0x06000156 RID: 342 RVA: 0x000067C4 File Offset: 0x000049C4
		public override void UnLock()
		{
			this._application.UnLock();
		}

		// Token: 0x04000D9A RID: 3482
		private HttpApplicationState _application;
	}
}
