using System;
using System.Security.Permissions;
using System.Web.Configuration;
using System.Web.UI;

namespace System.Web
{
	/// <summary>Enables the server to gather information on the capabilities of the browser that is running on the client.</summary>
	// Token: 0x02000080 RID: 128
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HttpBrowserCapabilities : HttpCapabilitiesBase, IFilterResolutionService
	{
		// Token: 0x060005A0 RID: 1440 RVA: 0x00003A1F File Offset: 0x00001C1F
		bool IFilterResolutionService.EvaluateFilter(string filterName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00003A1F File Offset: 0x00001C1F
		int IFilterResolutionService.CompareFilters(string filter1, string filter2)
		{
			throw new NotImplementedException();
		}
	}
}
