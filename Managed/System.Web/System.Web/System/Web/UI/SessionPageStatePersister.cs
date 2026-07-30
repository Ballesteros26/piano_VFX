using System;

namespace System.Web.UI
{
	/// <summary>Stores ASP.NET page view state on the Web server.</summary>
	// Token: 0x02000223 RID: 547
	public class SessionPageStatePersister : PageStatePersister
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.SessionPageStatePersister" /> class.</summary>
		/// <param name="page">The <see cref="T:System.Web.UI.Page" /> that the view state persistence mechanism is created for.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Web.SessionState.HttpSessionState" /> is null (Nothing in Visual Basic)</exception>
		// Token: 0x06001662 RID: 5730 RVA: 0x0003C0A5 File Offset: 0x0003A2A5
		public SessionPageStatePersister(Page page)
			: base(page)
		{
			throw new NotImplementedException();
		}

		/// <summary>Deserializes and loads persisted state from the server-side session object when a <see cref="T:System.Web.UI.Page" /> object initializes its control hierarchy.</summary>
		/// <exception cref="T:System.Web.HttpException">The <see cref="M:System.Web.UI.SessionPageStatePersister.Load" /> method could not successfully deserialize the state contained in the request to the Web server.</exception>
		// Token: 0x06001663 RID: 5731 RVA: 0x00003A1F File Offset: 0x00001C1F
		public override void Load()
		{
			throw new NotImplementedException();
		}

		/// <summary>Serializes any object state contained in the <see cref="P:System.Web.UI.PageStatePersister.ViewState" /> or the <see cref="P:System.Web.UI.PageStatePersister.ControlState" /> property and writes the state to the session object.</summary>
		// Token: 0x06001664 RID: 5732 RVA: 0x00003A1F File Offset: 0x00001C1F
		public override void Save()
		{
			throw new NotImplementedException();
		}
	}
}
