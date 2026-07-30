using System;

namespace System.Web.Hosting
{
	/// <summary>Provides the core implementation for the <see cref="T:System.Web.Hosting.VirtualFile" /> and <see cref="T:System.Web.Hosting.VirtualDirectory" /> objects. An abstract class, it cannot be instantiated.</summary>
	// Token: 0x02000559 RID: 1369
	public abstract class VirtualFileBase : MarshalByRefObject
	{
		// Token: 0x06003B34 RID: 15156 RVA: 0x0009EF30 File Offset: 0x0009D130
		internal void SetVirtualPath(string vpath)
		{
			this.vpath = vpath;
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the <see cref="T:System.Web.Hosting.VirtualFileBase" /> instance represents a virtual file or a virtual directory.</summary>
		/// <returns>true if the <see cref="T:System.Web.Hosting.VirtualFileBase" /> instance is a virtual directory; otherwise, false if the <see cref="T:System.Web.Hosting.VirtualFileBase" /> instance is a virtual file.</returns>
		// Token: 0x1700122D RID: 4653
		// (get) Token: 0x06003B35 RID: 15157
		public abstract bool IsDirectory { get; }

		/// <summary>Gets the display name of the virtual resource.</summary>
		/// <returns>The display name of the virtual file.</returns>
		// Token: 0x1700122E RID: 4654
		// (get) Token: 0x06003B36 RID: 15158 RVA: 0x0009EF39 File Offset: 0x0009D139
		public virtual string Name
		{
			get
			{
				return VirtualPathUtility.GetFileName(this.vpath);
			}
		}

		/// <summary>Gets the virtual file path.</summary>
		/// <returns>The path to the virtual file. </returns>
		// Token: 0x1700122F RID: 4655
		// (get) Token: 0x06003B37 RID: 15159 RVA: 0x0009EF46 File Offset: 0x0009D146
		public string VirtualPath
		{
			get
			{
				return this.vpath;
			}
		}

		/// <summary>Gives a <see cref="T:System.Web.Hosting.VirtualFileBase" /> instance an infinite lifetime by preventing a lease from being created.</summary>
		/// <returns>Always null.</returns>
		// Token: 0x06003B38 RID: 15160 RVA: 0x00003BEA File Offset: 0x00001DEA
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x04001FF6 RID: 8182
		private string vpath;
	}
}
