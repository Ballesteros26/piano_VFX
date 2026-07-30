using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Web.SessionState;

namespace System.Web
{
	/// <summary>Encapsulates the HTTP intrinsic object that provides access to session-state values, session-level settings, and lifetime management methods.</summary>
	// Token: 0x0200003F RID: 63
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class HttpSessionStateWrapper : HttpSessionStateBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpSessionStateWrapper" /> class.</summary>
		/// <param name="httpSessionState">The object that this wrapper class provides access to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="httpSessionState" /> is null.</exception>
		// Token: 0x06000381 RID: 897 RVA: 0x00006FE1 File Offset: 0x000051E1
		public HttpSessionStateWrapper(HttpSessionState httpSessionState)
		{
			if (httpSessionState == null)
			{
				throw new ArgumentNullException("httpSessionState");
			}
			this._session = httpSessionState;
		}

		/// <summary>Gets or sets the character-set identifier for the current session.</summary>
		/// <returns>The character-set identifier for the current session.</returns>
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000382 RID: 898 RVA: 0x00006FFE File Offset: 0x000051FE
		// (set) Token: 0x06000383 RID: 899 RVA: 0x0000700B File Offset: 0x0000520B
		public override int CodePage
		{
			get
			{
				return this._session.CodePage;
			}
			set
			{
				this._session.CodePage = value;
			}
		}

		/// <summary>Gets a reference to the current session-state object.</summary>
		/// <returns>The current session-state object.</returns>
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00002058 File Offset: 0x00000258
		public override HttpSessionStateBase Contents
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value that indicates whether the application is configured for cookieless sessions.</summary>
		/// <returns>One of the cookie-mode values that indicate whether the application is configured for cookieless sessions. The default is <see cref="F:System.Web.HttpCookieMode.UseCookies" />.</returns>
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000385 RID: 901 RVA: 0x00007019 File Offset: 0x00005219
		public override HttpCookieMode CookieMode
		{
			get
			{
				return this._session.CookieMode;
			}
		}

		/// <summary>Gets a value that indicates whether the session ID is embedded in the URL.</summary>
		/// <returns>true if the session ID is embedded in the URL; otherwise, false.</returns>
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00007026 File Offset: 0x00005226
		public override bool IsCookieless
		{
			get
			{
				return this._session.IsCookieless;
			}
		}

		/// <summary>Gets a value that indicates whether the session was created during the current request.</summary>
		/// <returns>true if the session was created during the current request; otherwise, false.</returns>
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000387 RID: 903 RVA: 0x00007033 File Offset: 0x00005233
		public override bool IsNewSession
		{
			get
			{
				return this._session.IsNewSession;
			}
		}

		/// <summary>Gets a value that indicates whether the session is read-only.</summary>
		/// <returns>true if the session is read-only; otherwise, false.</returns>
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000388 RID: 904 RVA: 0x00007040 File Offset: 0x00005240
		public override bool IsReadOnly
		{
			get
			{
				return this._session.IsReadOnly;
			}
		}

		/// <summary>Gets a collection of the keys for all values that are stored in the session-state collection.</summary>
		/// <returns>The session keys.</returns>
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000704D File Offset: 0x0000524D
		public override NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				return this._session.Keys;
			}
		}

		/// <summary>Gets or sets the locale identifier (LCID) of the current session.</summary>
		/// <returns>The LCID (culture) of the current session.</returns>
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000705A File Offset: 0x0000525A
		// (set) Token: 0x0600038B RID: 907 RVA: 0x00007067 File Offset: 0x00005267
		public override int LCID
		{
			get
			{
				return this._session.LCID;
			}
			set
			{
				this._session.LCID = value;
			}
		}

		/// <summary>Gets the current session-state mode.</summary>
		/// <returns>The session-state mode.</returns>
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00007075 File Offset: 0x00005275
		public override SessionStateMode Mode
		{
			get
			{
				return this._session.Mode;
			}
		}

		/// <summary>Gets the unique identifier for the session.</summary>
		/// <returns>The unique session identifier.</returns>
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00007082 File Offset: 0x00005282
		public override string SessionID
		{
			get
			{
				return this._session.SessionID;
			}
		}

		/// <summary>Gets a collection of objects that are declared by object elements that are marked as server controls and scoped to the current session in the application's Global.asax file.</summary>
		/// <returns>The objects that are declared in the Global.asax file.</returns>
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000708F File Offset: 0x0000528F
		public override HttpStaticObjectsCollectionBase StaticObjects
		{
			get
			{
				return new HttpStaticObjectsCollectionWrapper(this._session.StaticObjects);
			}
		}

		/// <summary>Gets or sets the time, in minutes, that can elapse between requests before the session-state provider ends the session.</summary>
		/// <returns>The time-out period, in minutes.</returns>
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600038F RID: 911 RVA: 0x000070A1 File Offset: 0x000052A1
		// (set) Token: 0x06000390 RID: 912 RVA: 0x000070AE File Offset: 0x000052AE
		public override int Timeout
		{
			get
			{
				return this._session.Timeout;
			}
			set
			{
				this._session.Timeout = value;
			}
		}

		/// <summary>Gets or sets a session value by using the specified index.</summary>
		/// <returns>The session-state value that is stored at the specified index.</returns>
		/// <param name="index">The index of the session value.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
		// Token: 0x17000198 RID: 408
		public override object this[int index]
		{
			get
			{
				return this._session[index];
			}
			set
			{
				this._session[index] = value;
			}
		}

		/// <summary>Gets or sets a session value by using the specified name.</summary>
		/// <returns>The session-state value that has the specified name, or null if the item does not exist.</returns>
		/// <param name="name">The key name of the session value.</param>
		// Token: 0x17000199 RID: 409
		public override object this[string name]
		{
			get
			{
				return this._session[name];
			}
			set
			{
				this._session[name] = value;
			}
		}

		/// <summary>Cancels the current session.</summary>
		// Token: 0x06000395 RID: 917 RVA: 0x000070F6 File Offset: 0x000052F6
		public override void Abandon()
		{
			this._session.Abandon();
		}

		/// <summary>Adds an item to the session-state collection.</summary>
		/// <param name="name">The name of the item to add to the session-state collection.</param>
		/// <param name="value">The value of the item to add to the session-state collection.</param>
		// Token: 0x06000396 RID: 918 RVA: 0x00007103 File Offset: 0x00005303
		public override void Add(string name, object value)
		{
			this._session.Add(name, value);
		}

		/// <summary>Removes all keys and values from the session-state collection.</summary>
		// Token: 0x06000397 RID: 919 RVA: 0x00007112 File Offset: 0x00005312
		public override void Clear()
		{
			this._session.Clear();
		}

		/// <summary>Deletes an item from the session-state collection.</summary>
		/// <param name="name">The name of the item to delete from the session-state collection.</param>
		// Token: 0x06000398 RID: 920 RVA: 0x0000711F File Offset: 0x0000531F
		public override void Remove(string name)
		{
			this._session.Remove(name);
		}

		/// <summary>Removes all keys and values from the session-state collection.</summary>
		// Token: 0x06000399 RID: 921 RVA: 0x0000712D File Offset: 0x0000532D
		public override void RemoveAll()
		{
			this._session.RemoveAll();
		}

		/// <summary>Deletes the item at the specified index from the session-state collection.</summary>
		/// <param name="index">The index of the item to remove from the session-state collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.- or -<paramref name="index" /> is equal to or greater than <see cref="P:System.Web.SessionState.HttpSessionState.Count" />.</exception>
		// Token: 0x0600039A RID: 922 RVA: 0x0000713A File Offset: 0x0000533A
		public override void RemoveAt(int index)
		{
			this._session.RemoveAt(index);
		}

		/// <summary>Copies the collection of session-state values to a one-dimensional array, starting at the specified index in the array.</summary>
		/// <param name="array">The array to copy session values to</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying starts.</param>
		// Token: 0x0600039B RID: 923 RVA: 0x00007148 File Offset: 0x00005348
		public override void CopyTo(Array array, int index)
		{
			this._session.CopyTo(array, index);
		}

		/// <summary>Gets the number of items in the session-state collection.</summary>
		/// <returns>The number of items in the collection.</returns>
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600039C RID: 924 RVA: 0x00007157 File Offset: 0x00005357
		public override int Count
		{
			get
			{
				return this._session.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to the collection of session-state values is synchronized (thread safe).</summary>
		/// <returns>true if access to the collection is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600039D RID: 925 RVA: 0x00007164 File Offset: 0x00005364
		public override bool IsSynchronized
		{
			get
			{
				return this._session.IsSynchronized;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection of session-state values.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00007171 File Offset: 0x00005371
		public override object SyncRoot
		{
			get
			{
				return this._session.SyncRoot;
			}
		}

		/// <summary>Returns an enumerator that can be used to read all the session-state variable names in the current session.</summary>
		/// <returns>An enumerator that can iterate through the variable names in the session-state collection.</returns>
		// Token: 0x0600039F RID: 927 RVA: 0x0000717E File Offset: 0x0000537E
		public override IEnumerator GetEnumerator()
		{
			return this._session.GetEnumerator();
		}

		// Token: 0x04000D9E RID: 3486
		private readonly HttpSessionState _session;
	}
}
