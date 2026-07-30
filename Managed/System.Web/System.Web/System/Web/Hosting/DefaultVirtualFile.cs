using System;
using System.IO;

namespace System.Web.Hosting
{
	// Token: 0x0200054E RID: 1358
	internal class DefaultVirtualFile : VirtualFile
	{
		// Token: 0x06003AC6 RID: 15046 RVA: 0x0009E74E File Offset: 0x0009C94E
		internal DefaultVirtualFile(string virtualPath)
			: base(virtualPath)
		{
		}

		// Token: 0x06003AC7 RID: 15047 RVA: 0x0009E757 File Offset: 0x0009C957
		public override Stream Open()
		{
			return File.OpenRead(HostingEnvironment.MapPath(base.VirtualPath));
		}
	}
}
