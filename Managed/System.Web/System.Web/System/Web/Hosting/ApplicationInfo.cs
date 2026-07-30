using System;
using Unity;

namespace System.Web.Hosting
{
	/// <summary>Provides information about a running application. This class cannot be inherited.</summary>
	// Token: 0x02000549 RID: 1353
	[Serializable]
	public sealed class ApplicationInfo
	{
		// Token: 0x06003A97 RID: 14999 RVA: 0x0009DFD7 File Offset: 0x0009C1D7
		internal ApplicationInfo(string id, string phys, string virt)
		{
			this.id = id;
			this.physical_path = phys;
			this.virtual_path = virt;
		}

		/// <summary>Gets the unique identifier for the application.</summary>
		/// <returns>The unique identifier for the application specified when the application was created by using the <see cref="M:System.Web.Hosting.ApplicationManager.CreateObject(System.String,System.Type,System.String,System.String,System.Boolean)" /> method.</returns>
		// Token: 0x1700120C RID: 4620
		// (get) Token: 0x06003A98 RID: 15000 RVA: 0x0009DFF4 File Offset: 0x0009C1F4
		public string ID
		{
			get
			{
				return this.id;
			}
		}

		/// <summary>Gets the physical path corresponding to the application's root.</summary>
		/// <returns>The physical path corresponding to the application's root.</returns>
		// Token: 0x1700120D RID: 4621
		// (get) Token: 0x06003A99 RID: 15001 RVA: 0x0009DFFC File Offset: 0x0009C1FC
		public string PhysicalPath
		{
			get
			{
				return this.physical_path;
			}
		}

		/// <summary>Gets the virtual path corresponding to the application's root.</summary>
		/// <returns>The virtual path corresponding to the application's root.</returns>
		// Token: 0x1700120E RID: 4622
		// (get) Token: 0x06003A9A RID: 15002 RVA: 0x0009E004 File Offset: 0x0009C204
		public string VirtualPath
		{
			get
			{
				return this.virtual_path;
			}
		}

		// Token: 0x06003A9B RID: 15003 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal ApplicationInfo()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001FD5 RID: 8149
		private string id;

		// Token: 0x04001FD6 RID: 8150
		private string physical_path;

		// Token: 0x04001FD7 RID: 8151
		private string virtual_path;
	}
}
