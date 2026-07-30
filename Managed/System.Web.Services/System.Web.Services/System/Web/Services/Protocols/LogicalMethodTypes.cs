using System;

namespace System.Web.Services.Protocols
{
	/// <summary>Specifies how the XML Web service method was invoked.</summary>
	// Token: 0x0200003E RID: 62
	public enum LogicalMethodTypes
	{
		/// <summary>The XML Web service method is invoked synchronously.</summary>
		// Token: 0x040001FE RID: 510
		Sync = 1,
		/// <summary>The XML Web service method is invoked asynchronously.</summary>
		// Token: 0x040001FF RID: 511
		Async
	}
}
