using System;
using System.Collections;

namespace System.Web.Hosting
{
	/// <summary>Represents a directory object in a virtual file or resource space.</summary>
	// Token: 0x02000557 RID: 1367
	public abstract class VirtualDirectory : VirtualFileBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Hosting.VirtualDirectory" /> class. </summary>
		/// <param name="virtualPath">The virtual path to the resource represented by this instance.</param>
		// Token: 0x06003B2B RID: 15147 RVA: 0x0009EF21 File Offset: 0x0009D121
		protected VirtualDirectory(string virtualPath)
		{
			base.SetVirtualPath(virtualPath);
		}

		/// <summary>Gets a list of the files and subdirectories contained in this virtual directory.</summary>
		/// <returns>An object implementing the <see cref="T:System.Collections.IEnumerable" /> interface containing <see cref="T:System.Web.Hosting.VirtualFile" /> and <see cref="T:System.Web.Hosting.VirtualDirectory" /> objects.</returns>
		// Token: 0x17001228 RID: 4648
		// (get) Token: 0x06003B2C RID: 15148
		public abstract IEnumerable Children { get; }

		/// <summary>Gets a list of all the subdirectories contained in this directory.</summary>
		/// <returns>An object implementing the <see cref="T:System.Collections.IEnumerable" /> interface containing <see cref="T:System.Web.Hosting.VirtualDirectory" /> objects.</returns>
		// Token: 0x17001229 RID: 4649
		// (get) Token: 0x06003B2D RID: 15149
		public abstract IEnumerable Directories { get; }

		/// <summary>Gets a list of all files contained in this directory.</summary>
		/// <returns>An object implementing the <see cref="T:System.Collections.IEnumerable" /> interface containing <see cref="T:System.Web.Hosting.VirtualFile" /> objects.</returns>
		// Token: 0x1700122A RID: 4650
		// (get) Token: 0x06003B2E RID: 15150
		public abstract IEnumerable Files { get; }

		/// <summary>Gets a value that indicates that this is a virtual resource that should be treated as a directory.</summary>
		/// <returns>Always true.</returns>
		// Token: 0x1700122B RID: 4651
		// (get) Token: 0x06003B2F RID: 15151 RVA: 0x00008B66 File Offset: 0x00006D66
		public override bool IsDirectory
		{
			get
			{
				return true;
			}
		}
	}
}
