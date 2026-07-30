using System;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Permissions;
using Unity;

namespace System.Web.SessionState
{
	/// <summary>Provides access to session-state values as well as session-level settings and lifetime management methods.</summary>
	// Token: 0x02000493 RID: 1171
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HttpSessionState : ICollection, IEnumerable
	{
		// Token: 0x06003505 RID: 13573 RVA: 0x0008B4EF File Offset: 0x000896EF
		internal HttpSessionState(IHttpSessionState container)
		{
			this.container = container;
		}

		// Token: 0x170010B8 RID: 4280
		// (get) Token: 0x06003506 RID: 13574 RVA: 0x0008B4FE File Offset: 0x000896FE
		internal IHttpSessionState Container
		{
			get
			{
				return this.container;
			}
		}

		/// <summary>Gets or sets the character-set identifier for the current session.</summary>
		/// <returns>The character-set identifier for the current session.</returns>
		// Token: 0x170010B9 RID: 4281
		// (get) Token: 0x06003507 RID: 13575 RVA: 0x0008B506 File Offset: 0x00089706
		// (set) Token: 0x06003508 RID: 13576 RVA: 0x0008B513 File Offset: 0x00089713
		public int CodePage
		{
			get
			{
				return this.container.CodePage;
			}
			set
			{
				this.container.CodePage = value;
			}
		}

		/// <summary>Gets a reference to the current session-state object.</summary>
		/// <returns>The current <see cref="T:System.Web.SessionState.HttpSessionState" />.</returns>
		// Token: 0x170010BA RID: 4282
		// (get) Token: 0x06003509 RID: 13577 RVA: 0x00002058 File Offset: 0x00000258
		public HttpSessionState Contents
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value that indicates whether the application is configured for cookieless sessions.</summary>
		/// <returns>One of the <see cref="T:System.Web.HttpCookieMode" /> values that indicate whether the application is configured for cookieless sessions. The default is <see cref="F:System.Web.HttpCookieMode.UseCookies" />.</returns>
		// Token: 0x170010BB RID: 4283
		// (get) Token: 0x0600350A RID: 13578 RVA: 0x0008B521 File Offset: 0x00089721
		public HttpCookieMode CookieMode
		{
			get
			{
				if (this.IsCookieless)
				{
					return HttpCookieMode.UseUri;
				}
				return HttpCookieMode.UseCookies;
			}
		}

		/// <summary>Gets the number of items in the session-state collection.</summary>
		/// <returns>The number of items in the collection.</returns>
		// Token: 0x170010BC RID: 4284
		// (get) Token: 0x0600350B RID: 13579 RVA: 0x0008B52E File Offset: 0x0008972E
		public int Count
		{
			get
			{
				return this.container.Count;
			}
		}

		/// <summary>Gets a value indicating whether the session ID is embedded in the URL or stored in an HTTP cookie.</summary>
		/// <returns>true if the session is embedded in the URL; otherwise, false.</returns>
		// Token: 0x170010BD RID: 4285
		// (get) Token: 0x0600350C RID: 13580 RVA: 0x0008B53B File Offset: 0x0008973B
		public bool IsCookieless
		{
			get
			{
				return this.container.IsCookieless;
			}
		}

		/// <summary>Gets a value indicating whether the session was created with the current request.</summary>
		/// <returns>true if the session was created with the current request; otherwise, false.</returns>
		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x0600350D RID: 13581 RVA: 0x0008B548 File Offset: 0x00089748
		public bool IsNewSession
		{
			get
			{
				return this.container.IsNewSession;
			}
		}

		/// <summary>Gets a value indicating whether the session is read-only.</summary>
		/// <returns>true if the session is read-only; otherwise, false.</returns>
		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x0600350E RID: 13582 RVA: 0x0008B555 File Offset: 0x00089755
		public bool IsReadOnly
		{
			get
			{
				return this.container.IsReadOnly;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection of session-state values is synchronized (thread safe).</summary>
		/// <returns>true if access to the collection is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x0600350F RID: 13583 RVA: 0x0008B562 File Offset: 0x00089762
		public bool IsSynchronized
		{
			get
			{
				return this.container.IsSynchronized;
			}
		}

		/// <summary>Gets or sets a session value by name.</summary>
		/// <returns>The session-state value with the specified name, or null if the item does not exist.</returns>
		/// <param name="name">The key name of the session value. </param>
		// Token: 0x170010C1 RID: 4289
		public object this[string name]
		{
			get
			{
				return this.container[name];
			}
			set
			{
				this.container[name] = value;
			}
		}

		/// <summary>Gets or sets a session value by numerical index.</summary>
		/// <returns>The session-state value stored at the specified index, or null if the item does not exist.</returns>
		/// <param name="index">The numerical index of the session value. </param>
		// Token: 0x170010C2 RID: 4290
		public object this[int index]
		{
			get
			{
				return this.container[index];
			}
			set
			{
				this.container[index] = value;
			}
		}

		/// <summary>Gets a collection of the keys for all values stored in the session-state collection.</summary>
		/// <returns>The <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> that contains all the session keys.</returns>
		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x06003514 RID: 13588 RVA: 0x0008B5A9 File Offset: 0x000897A9
		public NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				return this.container.Keys;
			}
		}

		/// <summary>Gets or sets the locale identifier (LCID) of the current session.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> instance that specifies the culture of the current session.</returns>
		// Token: 0x170010C4 RID: 4292
		// (get) Token: 0x06003515 RID: 13589 RVA: 0x0008B5B6 File Offset: 0x000897B6
		// (set) Token: 0x06003516 RID: 13590 RVA: 0x0008B5C3 File Offset: 0x000897C3
		public int LCID
		{
			get
			{
				return this.container.LCID;
			}
			set
			{
				this.container.LCID = value;
			}
		}

		/// <summary>Gets the current session-state mode.</summary>
		/// <returns>One of the <see cref="T:System.Web.SessionState.SessionStateMode" /> values.</returns>
		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x06003517 RID: 13591 RVA: 0x0008B5D1 File Offset: 0x000897D1
		public SessionStateMode Mode
		{
			get
			{
				return this.container.Mode;
			}
		}

		/// <summary>Gets the unique identifier for the session.</summary>
		/// <returns>The unique session identifier.</returns>
		// Token: 0x170010C6 RID: 4294
		// (get) Token: 0x06003518 RID: 13592 RVA: 0x0008B5DE File Offset: 0x000897DE
		public string SessionID
		{
			get
			{
				return this.container.SessionID;
			}
		}

		/// <summary>Gets a collection of objects declared by &lt;object Runat="Server" Scope="Session"/&gt; tags within the ASP.NET application file Global.asax.</summary>
		/// <returns>An <see cref="T:System.Web.HttpStaticObjectsCollection" /> containing objects declared in the Global.asax file.</returns>
		// Token: 0x170010C7 RID: 4295
		// (get) Token: 0x06003519 RID: 13593 RVA: 0x0008B5EB File Offset: 0x000897EB
		public HttpStaticObjectsCollection StaticObjects
		{
			get
			{
				return this.container.StaticObjects;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection of session-state values.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x170010C8 RID: 4296
		// (get) Token: 0x0600351A RID: 13594 RVA: 0x0008B5F8 File Offset: 0x000897F8
		public object SyncRoot
		{
			get
			{
				return this.container.SyncRoot;
			}
		}

		/// <summary>Gets and sets the amount of time, in minutes, allowed between requests before the session-state provider terminates the session.</summary>
		/// <returns>The time-out period, in minutes.</returns>
		// Token: 0x170010C9 RID: 4297
		// (get) Token: 0x0600351B RID: 13595 RVA: 0x0008B605 File Offset: 0x00089805
		// (set) Token: 0x0600351C RID: 13596 RVA: 0x0008B612 File Offset: 0x00089812
		public int Timeout
		{
			get
			{
				return this.container.Timeout;
			}
			set
			{
				this.container.Timeout = value;
			}
		}

		/// <summary>Cancels the current session.</summary>
		// Token: 0x0600351D RID: 13597 RVA: 0x0008B620 File Offset: 0x00089820
		public void Abandon()
		{
			this.container.Abandon();
		}

		/// <summary>Adds a new item to the session-state collection.</summary>
		/// <param name="name">The name of the item to add to the session-state collection. </param>
		/// <param name="value">The value of the item to add to the session-state collection. </param>
		// Token: 0x0600351E RID: 13598 RVA: 0x0008B62D File Offset: 0x0008982D
		public void Add(string name, object value)
		{
			this.container.Add(name, value);
		}

		/// <summary>Removes all keys and values from the session-state collection.</summary>
		// Token: 0x0600351F RID: 13599 RVA: 0x0008B63C File Offset: 0x0008983C
		public void Clear()
		{
			this.container.Clear();
		}

		/// <summary>Copies the collection of session-state values to a one-dimensional array, starting at the specified index in the array.</summary>
		/// <param name="array">The <see cref="T:System.Array" /> that receives the session values. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> from which copying starts. </param>
		// Token: 0x06003520 RID: 13600 RVA: 0x0008B649 File Offset: 0x00089849
		public void CopyTo(Array array, int index)
		{
			this.container.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that can be used to read all the session-state variable names in the current session.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can iterate through the variable names in the session-state collection.</returns>
		// Token: 0x06003521 RID: 13601 RVA: 0x0008B658 File Offset: 0x00089858
		public IEnumerator GetEnumerator()
		{
			return this.container.GetEnumerator();
		}

		/// <summary>Deletes an item from the session-state collection.</summary>
		/// <param name="name">The name of the item to delete from the session-state collection. </param>
		// Token: 0x06003522 RID: 13602 RVA: 0x0008B665 File Offset: 0x00089865
		public void Remove(string name)
		{
			this.container.Remove(name);
		}

		/// <summary>Removes all keys and values from the session-state collection.</summary>
		// Token: 0x06003523 RID: 13603 RVA: 0x0008B63C File Offset: 0x0008983C
		public void RemoveAll()
		{
			this.container.Clear();
		}

		/// <summary>Deletes an item at a specified index from the session-state collection.</summary>
		/// <param name="index">The index of the item to remove from the session-state collection. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.- or -<paramref name="index" /> is equal to or greater than <see cref="P:System.Web.SessionState.HttpSessionState.Count" />.</exception>
		// Token: 0x06003524 RID: 13604 RVA: 0x0008B673 File Offset: 0x00089873
		public void RemoveAt(int index)
		{
			this.container.RemoveAt(index);
		}

		// Token: 0x06003525 RID: 13605 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal HttpSessionState()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001D4B RID: 7499
		private IHttpSessionState container;
	}
}
