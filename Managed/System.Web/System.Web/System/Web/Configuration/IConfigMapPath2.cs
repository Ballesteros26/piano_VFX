using System;

namespace System.Web.Configuration
{
	// Token: 0x02000568 RID: 1384
	internal interface IConfigMapPath2
	{
		// Token: 0x06003B57 RID: 15191
		void GetPathConfigFilename(string siteID, VirtualPath path, out string directory, out string baseName);

		// Token: 0x06003B58 RID: 15192
		string MapPath(string siteID, VirtualPath path);

		// Token: 0x06003B59 RID: 15193
		VirtualPath GetAppPathForPath(string siteID, VirtualPath path);
	}
}
