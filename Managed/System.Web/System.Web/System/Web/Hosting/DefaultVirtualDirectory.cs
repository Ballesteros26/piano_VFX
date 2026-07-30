using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace System.Web.Hosting
{
	// Token: 0x0200054D RID: 1357
	internal sealed class DefaultVirtualDirectory : VirtualDirectory
	{
		// Token: 0x06003ABF RID: 15039 RVA: 0x0009E5C8 File Offset: 0x0009C7C8
		internal DefaultVirtualDirectory(string virtualPath)
			: base(virtualPath)
		{
		}

		// Token: 0x06003AC0 RID: 15040 RVA: 0x0009E5D4 File Offset: 0x0009C7D4
		private void Init()
		{
			if (this.phys_dir == null)
			{
				string virtualPath = base.VirtualPath;
				string text = HostingEnvironment.MapPath(virtualPath);
				if (File.Exists(text))
				{
					this.virtual_dir = VirtualPathUtility.GetDirectory(virtualPath);
					this.phys_dir = HostingEnvironment.MapPath(this.virtual_dir);
					return;
				}
				this.virtual_dir = VirtualPathUtility.AppendTrailingSlash(virtualPath);
				this.phys_dir = text;
			}
		}

		// Token: 0x06003AC1 RID: 15041 RVA: 0x0009E630 File Offset: 0x0009C830
		private List<VirtualFileBase> AddDirectories(List<VirtualFileBase> list, string dir)
		{
			if (string.IsNullOrEmpty(dir) || !Directory.Exists(dir))
			{
				return list;
			}
			foreach (string text in Directory.GetDirectories(this.phys_dir))
			{
				list.Add(new DefaultVirtualDirectory(VirtualPathUtility.Combine(this.virtual_dir, Path.GetFileName(text))));
			}
			return list;
		}

		// Token: 0x06003AC2 RID: 15042 RVA: 0x0009E68C File Offset: 0x0009C88C
		private List<VirtualFileBase> AddFiles(List<VirtualFileBase> list, string dir)
		{
			if (string.IsNullOrEmpty(dir) || !Directory.Exists(dir))
			{
				return list;
			}
			foreach (string text in Directory.GetFiles(this.phys_dir))
			{
				list.Add(new DefaultVirtualFile(VirtualPathUtility.Combine(this.virtual_dir, Path.GetFileName(text))));
			}
			return list;
		}

		// Token: 0x17001212 RID: 4626
		// (get) Token: 0x06003AC3 RID: 15043 RVA: 0x0009E6E8 File Offset: 0x0009C8E8
		public override IEnumerable Children
		{
			get
			{
				this.Init();
				List<VirtualFileBase> list = new List<VirtualFileBase>();
				this.AddDirectories(list, this.phys_dir);
				return this.AddFiles(list, this.phys_dir);
			}
		}

		// Token: 0x17001213 RID: 4627
		// (get) Token: 0x06003AC4 RID: 15044 RVA: 0x0009E71C File Offset: 0x0009C91C
		public override IEnumerable Directories
		{
			get
			{
				this.Init();
				return this.AddDirectories(new List<VirtualFileBase>(), this.phys_dir);
			}
		}

		// Token: 0x17001214 RID: 4628
		// (get) Token: 0x06003AC5 RID: 15045 RVA: 0x0009E735 File Offset: 0x0009C935
		public override IEnumerable Files
		{
			get
			{
				this.Init();
				return this.AddFiles(new List<VirtualFileBase>(), this.phys_dir);
			}
		}

		// Token: 0x04001FE2 RID: 8162
		private string phys_dir;

		// Token: 0x04001FE3 RID: 8163
		private string virtual_dir;
	}
}
