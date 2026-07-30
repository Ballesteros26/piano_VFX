using System;

namespace System.Web.SessionState
{
	/// <summary>Represents session-state data for a session store.</summary>
	// Token: 0x020004A6 RID: 1190
	public class SessionStateStoreData
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.SessionState.SessionStateStoreData" /> class.</summary>
		/// <param name="sessionItems">The session variables and values for the current session.</param>
		/// <param name="staticObjects">The <see cref="T:System.Web.HttpStaticObjectsCollection" /> for the current session.</param>
		/// <param name="timeout">The <see cref="P:System.Web.SessionState.SessionStateStoreData.Timeout" /> for the current session.</param>
		// Token: 0x060035FD RID: 13821 RVA: 0x0008E40A File Offset: 0x0008C60A
		public SessionStateStoreData(ISessionStateItemCollection sessionItems, HttpStaticObjectsCollection staticObjects, int timeout)
		{
			this.sessionItems = sessionItems;
			this.staticObjects = staticObjects;
			this.timeout = timeout;
		}

		/// <summary>The session variables and values for the current session.</summary>
		/// <returns>An <see cref="T:System.Web.SessionState.ISessionStateItemCollection" /> object that contains variables and values for the current session.</returns>
		// Token: 0x170010F8 RID: 4344
		// (get) Token: 0x060035FE RID: 13822 RVA: 0x0008E427 File Offset: 0x0008C627
		public virtual ISessionStateItemCollection Items
		{
			get
			{
				return this.sessionItems;
			}
		}

		/// <summary>Gets a collection of objects declared by &lt;object Runat="Server" Scope="Session"/&gt; tags within the ASP.NET application file Global.asax.</summary>
		/// <returns>An <see cref="T:System.Web.HttpStaticObjectsCollection" /> containing objects declared in the Global.asax file.</returns>
		// Token: 0x170010F9 RID: 4345
		// (get) Token: 0x060035FF RID: 13823 RVA: 0x0008E42F File Offset: 0x0008C62F
		public virtual HttpStaticObjectsCollection StaticObjects
		{
			get
			{
				return this.staticObjects;
			}
		}

		/// <summary>Gets and sets the amount of time, in minutes, allowed between requests before the session-state provider terminates the session.</summary>
		/// <returns>The time-out period in minutes.</returns>
		// Token: 0x170010FA RID: 4346
		// (get) Token: 0x06003600 RID: 13824 RVA: 0x0008E437 File Offset: 0x0008C637
		// (set) Token: 0x06003601 RID: 13825 RVA: 0x0008E43F File Offset: 0x0008C63F
		public virtual int Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				this.timeout = value;
			}
		}

		// Token: 0x04001D94 RID: 7572
		private ISessionStateItemCollection sessionItems;

		// Token: 0x04001D95 RID: 7573
		private HttpStaticObjectsCollection staticObjects;

		// Token: 0x04001D96 RID: 7574
		private int timeout;
	}
}
