using System;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics
{
	/// <summary>Provides unescaped XML data for the logging of user-provided trace data.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200037C RID: 892
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class UnescapedXmlDiagnosticData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.UnescapedXmlDiagnosticData" /> class by using the specified XML data string.</summary>
		/// <param name="xmlPayload">The XML data to be logged in the UserData node of the event schema.  </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A96 RID: 6806 RVA: 0x0000220F File Offset: 0x0000040F
		public UnescapedXmlDiagnosticData(string xmlPayload)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the unescaped XML data string.</summary>
		/// <returns>An unescaped XML string.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001A97 RID: 6807 RVA: 0x000560B4 File Offset: 0x000542B4
		// (set) Token: 0x06001A98 RID: 6808 RVA: 0x0000220F File Offset: 0x0000040F
		public string UnescapedXml
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
	}
}
