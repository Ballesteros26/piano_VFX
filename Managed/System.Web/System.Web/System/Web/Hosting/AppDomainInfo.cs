using System;
using Unity;

namespace System.Web.Hosting
{
	/// <summary>Provides information about the application domain.</summary>
	// Token: 0x0200075D RID: 1885
	public class AppDomainInfo : IAppDomainInfo
	{
		// Token: 0x06004D07 RID: 19719 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal AppDomainInfo()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the application domain ID.</summary>
		/// <returns>The unique application domain ID.</returns>
		// Token: 0x06004D08 RID: 19720 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string GetId()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the physical path of the application domain.</summary>
		/// <returns>The physical path of the application domain.</returns>
		// Token: 0x06004D09 RID: 19721 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string GetPhysicalPath()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the site ID of the application domain.</summary>
		/// <returns>The site ID of the application domain.</returns>
		// Token: 0x06004D0A RID: 19722 RVA: 0x000CB1D4 File Offset: 0x000C93D4
		public int GetSiteId()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Gets the root virtual path of the application domain.</summary>
		/// <returns>The root virtual path of the application domain.</returns>
		// Token: 0x06004D0B RID: 19723 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string GetVirtualPath()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the idle state of the application domain.</summary>
		/// <returns>true if the application domain is idle; otherwise, false.</returns>
		// Token: 0x06004D0C RID: 19724 RVA: 0x000CB1F0 File Offset: 0x000C93F0
		public bool IsIdle()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
}
