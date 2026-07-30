using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using System.Threading;
using Unity;

namespace System.Web.SessionState
{
	/// <summary>Contains session-state values as well as session-level settings for the current request.</summary>
	// Token: 0x02000494 RID: 1172
	public class HttpSessionStateContainer : IHttpSessionState
	{
		/// <summary>Creates a new <see cref="T:System.Web.SessionState.HttpSessionStateContainer" /> object and initializes it with the specified settings and values.</summary>
		/// <param name="id">A session identifier for the new session. If null, an <see cref="T:System.ArgumentException" /> is thrown.</param>
		/// <param name="sessionItems">An <see cref="T:System.Web.SessionState.ISessionStateItemCollection" /> that contains the session values for the new session-state provider.</param>
		/// <param name="staticObjects">An <see cref="T:System.Web.HttpStaticObjectsCollection" /> that specifies the objects declared by &lt;object Runat="Server" Scope="Session"/&gt; tags within the ASP.NET application file Global.asax.</param>
		/// <param name="timeout">The amount of time, in minutes, allowed between requests before the session-state provider terminates the session.</param>
		/// <param name="newSession">true to indicate the session was created with the current request; otherwise, false. </param>
		/// <param name="cookieMode">The <see cref="P:System.Web.SessionState.HttpSessionStateContainer.CookieMode" /> for the new session-state provider.</param>
		/// <param name="mode">One of the <see cref="T:System.Web.SessionState.SessionStateMode" /> values that specifies the current session-state mode. </param>
		/// <param name="isReadonly">true to indicate the session is read-only; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="id" /> is null.</exception>
		// Token: 0x06003526 RID: 13606 RVA: 0x0008B684 File Offset: 0x00089884
		public HttpSessionStateContainer(string id, ISessionStateItemCollection sessionItems, HttpStaticObjectsCollection staticObjects, int timeout, bool newSession, HttpCookieMode cookieMode, SessionStateMode mode, bool isReadonly)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			this.sessionItems = sessionItems;
			this.id = id;
			this.staticObjects = staticObjects;
			this.timeout = timeout;
			this.newSession = newSession;
			this.cookieMode = cookieMode;
			this.mode = mode;
			this.isReadOnly = isReadonly;
			this.isCookieless = cookieMode == HttpCookieMode.UseUri;
		}

		/// <summary>Gets or sets the character-set identifier for the current session.</summary>
		/// <returns>The character-set identifier for the current session.</returns>
		// Token: 0x170010CA RID: 4298
		// (get) Token: 0x06003527 RID: 13607 RVA: 0x0008B6F0 File Offset: 0x000898F0
		// (set) Token: 0x06003528 RID: 13608 RVA: 0x0008B724 File Offset: 0x00089924
		public int CodePage
		{
			get
			{
				HttpContext httpContext = HttpContext.Current;
				if (httpContext == null)
				{
					return Encoding.Default.CodePage;
				}
				return httpContext.Response.ContentEncoding.CodePage;
			}
			set
			{
				HttpContext httpContext = HttpContext.Current;
				if (httpContext != null)
				{
					httpContext.Response.ContentEncoding = Encoding.GetEncoding(value);
				}
			}
		}

		/// <summary>Gets a value that indicates whether the application is configured for cookieless sessions.</summary>
		/// <returns>One of the <see cref="T:System.Web.HttpCookieMode" /> values that indicates whether the application is configured for cookieless sessions. The default is <see cref="F:System.Web.HttpCookieMode.UseCookies" />.</returns>
		// Token: 0x170010CB RID: 4299
		// (get) Token: 0x06003529 RID: 13609 RVA: 0x0008B74B File Offset: 0x0008994B
		public HttpCookieMode CookieMode
		{
			get
			{
				return this.cookieMode;
			}
		}

		/// <summary>Gets the number of items in the session-state collection.</summary>
		/// <returns>The number of items in the collection.</returns>
		// Token: 0x170010CC RID: 4300
		// (get) Token: 0x0600352A RID: 13610 RVA: 0x0008B753 File Offset: 0x00089953
		public int Count
		{
			get
			{
				if (this.sessionItems != null)
				{
					return this.sessionItems.Count;
				}
				return 0;
			}
		}

		/// <summary>Gets a value indicating whether the current session has been abandoned.</summary>
		/// <returns>true if the current session has been abandoned; otherwise, false.</returns>
		// Token: 0x170010CD RID: 4301
		// (get) Token: 0x0600352B RID: 13611 RVA: 0x0008B76A File Offset: 0x0008996A
		public bool IsAbandoned
		{
			get
			{
				return this.abandoned;
			}
		}

		/// <summary>Gets a value indicating whether the session ID is embedded in the URL or stored in an HTTP cookie.</summary>
		/// <returns>true if the session is embedded in the URL; otherwise, false.</returns>
		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x0600352C RID: 13612 RVA: 0x0008B772 File Offset: 0x00089972
		public bool IsCookieless
		{
			get
			{
				return this.isCookieless;
			}
		}

		/// <summary>Gets a value indicating whether the session was created with the current request.</summary>
		/// <returns>true if the session was created with the current request; otherwise, false.</returns>
		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x0600352D RID: 13613 RVA: 0x0008B77A File Offset: 0x0008997A
		public bool IsNewSession
		{
			get
			{
				return this.newSession;
			}
		}

		/// <summary>Gets a value indicating whether the session is read-only.</summary>
		/// <returns>true if the session is read-only; otherwise, false.</returns>
		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x0600352E RID: 13614 RVA: 0x0008B782 File Offset: 0x00089982
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection of session-state values is synchronized (thread safe).</summary>
		/// <returns>Always false, because thread-safe <see cref="T:System.Web.SessionState.HttpSessionStateContainer" /> objects are not supported.</returns>
		// Token: 0x170010D1 RID: 4305
		// (get) Token: 0x0600352F RID: 13615 RVA: 0x00008A69 File Offset: 0x00006C69
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010D2 RID: 4306
		object IHttpSessionState.this[int index]
		{
			get
			{
				if (this.sessionItems == null || this.sessionItems.Count == 0)
				{
					return null;
				}
				return this.sessionItems[index];
			}
			set
			{
				if (this.sessionItems != null)
				{
					this.sessionItems[index] = value;
				}
			}
		}

		// Token: 0x170010D3 RID: 4307
		object IHttpSessionState.this[string name]
		{
			get
			{
				if (this.sessionItems == null || this.sessionItems.Count == 0)
				{
					return null;
				}
				return this.sessionItems[name];
			}
			set
			{
				if (this.sessionItems != null)
				{
					this.sessionItems[name] = value;
				}
			}
		}

		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x06003534 RID: 13620 RVA: 0x0008B802 File Offset: 0x00089A02
		NameObjectCollectionBase.KeysCollection IHttpSessionState.Keys
		{
			get
			{
				if (this.sessionItems != null)
				{
					return this.sessionItems.Keys;
				}
				return null;
			}
		}

		/// <summary>Gets or sets the locale identifier (LCID) of the current session.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> instance that specifies the culture of the current session.</returns>
		// Token: 0x170010D5 RID: 4309
		// (get) Token: 0x06003535 RID: 13621 RVA: 0x000372DF File Offset: 0x000354DF
		// (set) Token: 0x06003536 RID: 13622 RVA: 0x000372F0 File Offset: 0x000354F0
		public int LCID
		{
			get
			{
				return Thread.CurrentThread.CurrentCulture.LCID;
			}
			set
			{
				Thread.CurrentThread.CurrentCulture = new CultureInfo(value);
			}
		}

		/// <summary>Gets the current session-state mode.</summary>
		/// <returns>One of the <see cref="T:System.Web.SessionState.SessionStateMode" /> values.</returns>
		// Token: 0x170010D6 RID: 4310
		// (get) Token: 0x06003537 RID: 13623 RVA: 0x0008B819 File Offset: 0x00089A19
		public SessionStateMode Mode
		{
			get
			{
				return this.mode;
			}
		}

		/// <summary>Gets the unique identifier for the session.</summary>
		/// <returns>The unique session identifier.</returns>
		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x06003538 RID: 13624 RVA: 0x0008B821 File Offset: 0x00089A21
		public string SessionID
		{
			get
			{
				return this.id;
			}
		}

		/// <summary>Gets a collection of objects declared by &lt;object Runat="Server" Scope="Session"/&gt; tags within the ASP.NET application file Global.asax.</summary>
		/// <returns>An <see cref="T:System.Web.HttpStaticObjectsCollection" /> containing objects declared in the Global.asax file.</returns>
		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x06003539 RID: 13625 RVA: 0x0008B829 File Offset: 0x00089A29
		public HttpStaticObjectsCollection StaticObjects
		{
			get
			{
				return this.staticObjects;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection of session-state values.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x0600353A RID: 13626 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets and sets the amount of time, in minutes, allowed between requests before the session-state provider terminates the session.</summary>
		/// <returns>The time-out period, in minutes.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt was made to set the <see cref="P:System.Web.SessionState.HttpSessionStateContainer.Timeout" /> value to an integer value less than 1.- or -An attempt was made to set the <see cref="P:System.Web.SessionState.HttpSessionStateContainer.Timeout" /> value to an integer value greater than the maximum allowed when <see cref="P:System.Web.SessionState.HttpSessionState.Mode" /> is set to <see cref="F:System.Web.SessionState.SessionStateMode.InProc" /> or <see cref="F:System.Web.SessionState.SessionStateMode.StateServer" />. The maximum allowed is 525,600 (one year). </exception>
		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x0600353B RID: 13627 RVA: 0x0008B831 File Offset: 0x00089A31
		// (set) Token: 0x0600353C RID: 13628 RVA: 0x0008B839 File Offset: 0x00089A39
		public int Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentException("The argument to SetTimeout must be greater than 0.");
				}
				this.timeout = value;
			}
		}

		// Token: 0x0600353D RID: 13629 RVA: 0x0008B851 File Offset: 0x00089A51
		internal void SetNewSession(bool value)
		{
			this.newSession = value;
		}

		/// <summary>Marks the current session as abandoned.</summary>
		// Token: 0x0600353E RID: 13630 RVA: 0x0008B85A File Offset: 0x00089A5A
		public void Abandon()
		{
			this.abandoned = true;
		}

		/// <summary>Adds a new item to the session-state collection.</summary>
		/// <param name="name">The name of the item to add to the session-state collection. </param>
		/// <param name="value">The value of the item to add to the session-state collection. </param>
		// Token: 0x0600353F RID: 13631 RVA: 0x0008B863 File Offset: 0x00089A63
		public void Add(string name, object value)
		{
			if (this.sessionItems == null)
			{
				return;
			}
			this.sessionItems[name] = value;
		}

		/// <summary>Removes all values and keys from the session-state collection.</summary>
		// Token: 0x06003540 RID: 13632 RVA: 0x0008B87B File Offset: 0x00089A7B
		public void Clear()
		{
			if (this.sessionItems == null)
			{
				return;
			}
			this.sessionItems.Clear();
		}

		/// <summary>Copies the collection of session-state values to a one-dimensional array, starting at the specified index in the array.</summary>
		/// <param name="array">The <see cref="T:System.Array" /> that receives the session values. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> from which copying starts. </param>
		// Token: 0x06003541 RID: 13633 RVA: 0x0008B894 File Offset: 0x00089A94
		public void CopyTo(Array array, int index)
		{
			if (this.sessionItems == null)
			{
				return;
			}
			NameObjectCollectionBase.KeysCollection keys = this.sessionItems.Keys;
			for (int i = 0; i < keys.Count; i++)
			{
				array.SetValue(keys.Get(i), i + index);
			}
		}

		/// <summary>Returns an enumerator that can be used to read all the session-state variable names in the current session.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can iterate through the variable names in the session-state collection.</returns>
		// Token: 0x06003542 RID: 13634 RVA: 0x0008B8D7 File Offset: 0x00089AD7
		public IEnumerator GetEnumerator()
		{
			if (this.sessionItems == null)
			{
				return null;
			}
			return this.sessionItems.GetEnumerator();
		}

		/// <summary>Deletes an item from the session-state collection.</summary>
		/// <param name="name">The name of the item to delete from the session-state collection. </param>
		// Token: 0x06003543 RID: 13635 RVA: 0x0008B8EE File Offset: 0x00089AEE
		public void Remove(string name)
		{
			if (this.sessionItems == null)
			{
				return;
			}
			this.sessionItems.Remove(name);
		}

		/// <summary>Clears all session-state values.</summary>
		// Token: 0x06003544 RID: 13636 RVA: 0x0008B87B File Offset: 0x00089A7B
		public void RemoveAll()
		{
			if (this.sessionItems == null)
			{
				return;
			}
			this.sessionItems.Clear();
		}

		/// <summary>Deletes an item at a specified index from the session-state collection.</summary>
		/// <param name="index">The index of the item to remove from the session-state collection. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.- or -<paramref name="index" /> is equal to or greater than <see cref="P:System.Web.SessionState.HttpSessionStateContainer.Count" />.</exception>
		// Token: 0x06003545 RID: 13637 RVA: 0x0008B905 File Offset: 0x00089B05
		public void RemoveAt(int index)
		{
			if (this.sessionItems == null)
			{
				return;
			}
			this.sessionItems.RemoveAt(index);
		}

		// Token: 0x06003546 RID: 13638 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public object get_Item(int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06003547 RID: 13639 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void set_Item(int index, object value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets a session value by name.</summary>
		/// <returns>The session-state value with the specified name.</returns>
		/// <param name="name">The key name of the session value. </param>
		// Token: 0x170010DB RID: 4315
		// (get) Token: 0x06003548 RID: 13640 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06003549 RID: 13641 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public object Item
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a collection of the keys for all values stored in the session-state collection.</summary>
		/// <returns>The <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> that contains all the session keys.</returns>
		// Token: 0x170010DC RID: 4316
		// (get) Token: 0x0600354A RID: 13642 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x04001D4C RID: 7500
		private string id;

		// Token: 0x04001D4D RID: 7501
		private HttpStaticObjectsCollection staticObjects;

		// Token: 0x04001D4E RID: 7502
		private int timeout;

		// Token: 0x04001D4F RID: 7503
		private bool newSession;

		// Token: 0x04001D50 RID: 7504
		private bool isCookieless;

		// Token: 0x04001D51 RID: 7505
		private SessionStateMode mode;

		// Token: 0x04001D52 RID: 7506
		private bool isReadOnly;

		// Token: 0x04001D53 RID: 7507
		internal bool abandoned;

		// Token: 0x04001D54 RID: 7508
		private ISessionStateItemCollection sessionItems;

		// Token: 0x04001D55 RID: 7509
		private HttpCookieMode cookieMode;
	}
}
