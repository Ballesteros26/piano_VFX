using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Web.SessionState;

namespace System.Web
{
	/// <summary>Serves as the base class for classes that provides access to session-state values, session-level settings, and lifetime management methods.</summary>
	// Token: 0x0200003E RID: 62
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public abstract class HttpSessionStateBase : ICollection, IEnumerable
	{
		/// <summary>When overridden in a derived class, gets or sets the character-set identifier for the current session.</summary>
		/// <returns>The character-set identifier for the current session.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000362 RID: 866 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x06000363 RID: 867 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual int CodePage
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a reference to the current session-state object.</summary>
		/// <returns>The current session-state object.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000364 RID: 868 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual HttpSessionStateBase Contents
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the application is configured for cookieless sessions.</summary>
		/// <returns>One of the cookie-mode values that indicate whether the application is configured for cookieless sessions.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000365 RID: 869 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual HttpCookieMode CookieMode
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the session ID is embedded in the URL.</summary>
		/// <returns>true if the session ID is embedded in the URL; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool IsCookieless
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the session was created during the current request.</summary>
		/// <returns>true if the session was created during the current request; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000367 RID: 871 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool IsNewSession
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the session is read-only.</summary>
		/// <returns>true if the session is read-only; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool IsReadOnly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a collection of the keys for all values that are stored in the session-state collection.</summary>
		/// <returns>The session keys.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the locale identifier (LCID) of the current session.</summary>
		/// <returns>The LCID (culture) of the current session.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600036A RID: 874 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x0600036B RID: 875 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual int LCID
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the current session-state mode.</summary>
		/// <returns>The session-state mode.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600036C RID: 876 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual SessionStateMode Mode
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the unique identifier for the session.</summary>
		/// <returns>The unique session identifier.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600036D RID: 877 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string SessionID
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a collection of objects that are declared by object elements that are marked as server controls and scoped to the current session in the application's Global.asax file.</summary>
		/// <returns>The objects that are declared in the Global.asax file.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600036E RID: 878 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual HttpStaticObjectsCollectionBase StaticObjects
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the time, in minutes, that can elapse between requests before the session-state provider ends the session.</summary>
		/// <returns>The time-out period, in minutes.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600036F RID: 879 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x06000370 RID: 880 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual int Timeout
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets or sets a session value by using the specified index.</summary>
		/// <returns>The session-state value that is stored at the specified index.</returns>
		/// <param name="index">The index of the session value.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000187 RID: 391
		public virtual object this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets or sets a session value by using the specified name.</summary>
		/// <returns>The session-state value that has the specified name, or null if the item does not exist.</returns>
		/// <param name="name">The key name of the session value.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000188 RID: 392
		public virtual object this[string name]
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, cancels the current session.</summary>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000375 RID: 885 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void Abandon()
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, adds an item to the session-state collection.</summary>
		/// <param name="name">The name of the item to add to the session-state collection.</param>
		/// <param name="value">The value of the item to add to the session-state collection.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000376 RID: 886 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void Add(string name, object value)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, removes all keys and values from the session-state collection.</summary>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000377 RID: 887 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void Clear()
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, deletes an item from the session-state collection.</summary>
		/// <param name="name">The name of the item to delete from the session-state collection.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000378 RID: 888 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void Remove(string name)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, removes all keys and values from the session-state collection.</summary>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000379 RID: 889 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void RemoveAll()
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, deletes the item at the specified index from the session-state collection.</summary>
		/// <param name="index">The index of the item to remove from the session-state collection.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600037A RID: 890 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, copies the collection of session-state values to a one-dimensional array, starting at the specified index in the array.</summary>
		/// <param name="array">The array to copy session values to.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying starts.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600037B RID: 891 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, gets the number of items in the session-state collection.</summary>
		/// <returns>The number of items in the collection.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual int Count
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether access to the collection of session-state values is synchronized (thread safe).</summary>
		/// <returns>true if access to the collection is synchronized (thread safe); otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool IsSynchronized
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets an object that can be used to synchronize access to the collection of session-state values.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual object SyncRoot
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, returns an enumerator that can be used to read all the session-state variable names in the current session.</summary>
		/// <returns>An enumerator that can iterate through the variable names in the session-state collection.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600037F RID: 895 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}
