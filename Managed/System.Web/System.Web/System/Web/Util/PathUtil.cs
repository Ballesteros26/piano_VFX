using System;
using System.IO;
using System.Security.Permissions;

namespace System.Web.Util
{
	// Token: 0x0200012B RID: 299
	internal static class PathUtil
	{
		// Token: 0x06000E3F RID: 3647 RVA: 0x00026AFD File Offset: 0x00024CFD
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private static string GetSystem32Path()
		{
			return Environment.GetFolderPath(Environment.SpecialFolder.System);
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x00026B06 File Offset: 0x00024D06
		internal static string GetSystemDllFullPath(string filename)
		{
			return Path.Combine(PathUtil._system32Path, filename);
		}

		// Token: 0x040011C3 RID: 4547
		private static string _system32Path = PathUtil.GetSystem32Path();
	}
}
