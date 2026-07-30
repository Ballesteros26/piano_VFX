using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Web.SessionState
{
	/// <summary>Defines the contract to implement a custom session-state container.</summary>
	// Token: 0x02000495 RID: 1173
	public interface IHttpSessionState
	{
		/// <summary>Ends the current session.</summary>
		// Token: 0x0600354B RID: 13643
		void Abandon();

		/// <summary>Adds a new item to the session-state collection.</summary>
		/// <param name="name">The name of the item to add to the session-state collection. </param>
		/// <param name="value">The value of the item to add to the session-state collection. </param>
		// Token: 0x0600354C RID: 13644
		void Add(string name, object value);

		/// <summary>Clears all values from the session-state item collection.</summary>
		// Token: 0x0600354D RID: 13645
		void Clear();

		/// <summary>Copies the collection of session-state item values to a one-dimensional array, starting at the specified index in the array.</summary>
		/// <param name="array">The <see cref="T:System.Array" /> that receives the session values. </param>
		/// <param name="index">The index in <paramref name="array" /> where copying starts. </param>
		// Token: 0x0600354E RID: 13646
		void CopyTo(Array array, int index);

		/// <summary>Returns an enumerator that can be used to read all the session-state item values in the current session.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can iterate through the values in the session-state item collection.</returns>
		// Token: 0x0600354F RID: 13647
		IEnumerator GetEnumerator();

		/// <summary>Deletes an item from the session-state item collection.</summary>
		/// <param name="name">The name of the item to delete from the session-state item collection. </param>
		// Token: 0x06003550 RID: 13648
		void Remove(string name);

		/// <summary>Clears all values from the session-state item collection.</summary>
		// Token: 0x06003551 RID: 13649
		void RemoveAll();

		/// <summary>Deletes an item at a specified index from the session-state item collection.</summary>
		/// <param name="index">The index of the item to remove from the session-state collection. </param>
		// Token: 0x06003552 RID: 13650
		void RemoveAt(int index);

		/// <summary>Gets or sets the code-page identifier for the current session.</summary>
		/// <returns>The code-page identifier for the current session.</returns>
		// Token: 0x170010DD RID: 4317
		// (get) Token: 0x06003553 RID: 13651
		// (set) Token: 0x06003554 RID: 13652
		int CodePage { get; set; }

		/// <summary>Gets a value that indicates whether the application is configured for cookieless sessions.</summary>
		/// <returns>One of the <see cref="T:System.Web.HttpCookieMode" /> values that indicate whether the application is configured for cookieless sessions. The default is <see cref="F:System.Web.HttpCookieMode.UseCookies" />.</returns>
		// Token: 0x170010DE RID: 4318
		// (get) Token: 0x06003555 RID: 13653
		HttpCookieMode CookieMode { get; }

		/// <summary>Gets the number of items in the session-state item collection.</summary>
		/// <returns>The number of items in the session-state item collection.</returns>
		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x06003556 RID: 13654
		int Count { get; }

		/// <summary>Gets a value indicating whether the session ID is embedded in the URL or stored in an HTTP cookie.</summary>
		/// <returns>true if the session is embedded in the URL; otherwise, false.</returns>
		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x06003557 RID: 13655
		bool IsCookieless { get; }

		/// <summary>Gets a value indicating whether the session was created with the current request.</summary>
		/// <returns>true if the session was created with the current request; otherwise, false.</returns>
		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x06003558 RID: 13656
		bool IsNewSession { get; }

		/// <summary>Gets a value indicating whether the session is read-only.</summary>
		/// <returns>true if the session is read-only; otherwise, false.</returns>
		// Token: 0x170010E2 RID: 4322
		// (get) Token: 0x06003559 RID: 13657
		bool IsReadOnly { get; }

		/// <summary>Gets a value indicating whether access to the collection of session-state values is synchronized (thread safe).</summary>
		/// <returns>true if access to the collection is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x0600355A RID: 13658
		bool IsSynchronized { get; }

		/// <summary>Gets or sets a session-state item value by numerical index.</summary>
		/// <returns>The session-state item value specified in the <paramref name="index" /> parameter.</returns>
		/// <param name="index">The numerical index of the session-state item value. </param>
		// Token: 0x170010E4 RID: 4324
		object this[int index] { get; set; }

		/// <summary>Gets or sets a session-state item value by name.</summary>
		/// <returns>The session-state item value specified in the <paramref name="name" /> parameter.</returns>
		/// <param name="name">The key name of the session-state item value. </param>
		// Token: 0x170010E5 RID: 4325
		object this[string name] { get; set; }

		/// <summary>Gets a collection of the keys for all values stored in the session-state item collection.</summary>
		/// <returns>The <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> that contains all the session-item keys.</returns>
		// Token: 0x170010E6 RID: 4326
		// (get) Token: 0x0600355F RID: 13663
		NameObjectCollectionBase.KeysCollection Keys { get; }

		/// <summary>Gets or sets the locale identifier (LCID) of the current session.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> instance that specifies the culture of the current session.</returns>
		// Token: 0x170010E7 RID: 4327
		// (get) Token: 0x06003560 RID: 13664
		// (set) Token: 0x06003561 RID: 13665
		int LCID { get; set; }

		/// <summary>Gets the current session-state mode.</summary>
		/// <returns>One of the <see cref="T:System.Web.SessionState.SessionStateMode" /> values.</returns>
		// Token: 0x170010E8 RID: 4328
		// (get) Token: 0x06003562 RID: 13666
		SessionStateMode Mode { get; }

		/// <summary>Gets the unique session identifier for the session.</summary>
		/// <returns>The session ID.</returns>
		// Token: 0x170010E9 RID: 4329
		// (get) Token: 0x06003563 RID: 13667
		string SessionID { get; }

		/// <summary>Gets a collection of objects declared by &lt;object Runat="Server" Scope="Session"/&gt; tags within the ASP.NET application file Global.asax.</summary>
		/// <returns>An <see cref="T:System.Web.HttpStaticObjectsCollection" /> containing objects declared in the Global.asax file.</returns>
		// Token: 0x170010EA RID: 4330
		// (get) Token: 0x06003564 RID: 13668
		HttpStaticObjectsCollection StaticObjects { get; }

		/// <summary>Gets an object that can be used to synchronize access to the collection of session-state values.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x170010EB RID: 4331
		// (get) Token: 0x06003565 RID: 13669
		object SyncRoot { get; }

		/// <summary>Gets and sets the time-out period (in minutes) allowed between requests before the session-state provider terminates the session.</summary>
		/// <returns>The time-out period, in minutes.</returns>
		// Token: 0x170010EC RID: 4332
		// (get) Token: 0x06003566 RID: 13670
		// (set) Token: 0x06003567 RID: 13671
		int Timeout { get; set; }
	}
}
