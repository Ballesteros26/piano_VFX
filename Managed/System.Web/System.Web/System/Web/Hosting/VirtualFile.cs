using System;
using System.IO;

namespace System.Web.Hosting
{
	/// <summary>Represents a file object in a virtual file or resource space.</summary>
	// Token: 0x02000558 RID: 1368
	public abstract class VirtualFile : VirtualFileBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Hosting.VirtualFile" /> class. </summary>
		/// <param name="virtualPath">The virtual path to the resource represented by this instance. </param>
		// Token: 0x06003B30 RID: 15152 RVA: 0x0009EF21 File Offset: 0x0009D121
		protected VirtualFile(string virtualPath)
		{
			base.SetVirtualPath(virtualPath);
		}

		/// <summary>Gets a value that indicates that this is a virtual resource that should be treated as a file.</summary>
		/// <returns>Always false. </returns>
		// Token: 0x1700122C RID: 4652
		// (get) Token: 0x06003B31 RID: 15153 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool IsDirectory
		{
			get
			{
				return false;
			}
		}

		/// <summary>When overridden in a derived class, returns a read-only stream to the virtual resource.</summary>
		/// <returns>A read-only stream to the virtual file.</returns>
		// Token: 0x06003B32 RID: 15154
		public abstract Stream Open();
	}
}
