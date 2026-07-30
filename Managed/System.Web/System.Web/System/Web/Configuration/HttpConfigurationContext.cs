using System;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Supplies current context information to configuration-section handlers in ASP.NET applications.</summary>
	// Token: 0x020005A9 RID: 1449
	public class HttpConfigurationContext
	{
		// Token: 0x06003E0A RID: 15882 RVA: 0x000A484B File Offset: 0x000A2A4B
		internal HttpConfigurationContext(string virtualPath)
		{
			this.virtualPath = virtualPath;
		}

		/// <summary>Gets the virtual path to the Web.config configuration file.</summary>
		/// <returns>The virtual path to the Web.config file. Null when evaluating Machine.config; an empty string ("") when evaluating the root Web.config file for the site.</returns>
		// Token: 0x17001371 RID: 4977
		// (get) Token: 0x06003E0B RID: 15883 RVA: 0x000A485A File Offset: 0x000A2A5A
		public string VirtualPath
		{
			get
			{
				return this.virtualPath;
			}
		}

		// Token: 0x06003E0C RID: 15884 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal HttpConfigurationContext()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002204 RID: 8708
		private string virtualPath;
	}
}
