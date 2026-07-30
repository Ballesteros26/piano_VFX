using System;
using System.Text;
using System.Web.Configuration;

namespace System.Web.Util
{
	// Token: 0x02000150 RID: 336
	internal class WebEncoding
	{
		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x0002AF9C File Offset: 0x0002919C
		private static GlobalizationSection GlobalizationConfig
		{
			get
			{
				if (!WebEncoding.cached)
				{
					try
					{
						WebEncoding.sect = (GlobalizationSection)WebConfigurationManager.GetWebApplicationSection("system.web/globalization");
					}
					catch
					{
					}
					WebEncoding.cached = true;
				}
				return WebEncoding.sect;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x0002AFE4 File Offset: 0x000291E4
		public static Encoding FileEncoding
		{
			get
			{
				if (WebEncoding.GlobalizationConfig == null)
				{
					return Encoding.Default;
				}
				return WebEncoding.GlobalizationConfig.FileEncoding;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x0002AFFD File Offset: 0x000291FD
		public static Encoding ResponseEncoding
		{
			get
			{
				if (WebEncoding.GlobalizationConfig == null)
				{
					return Encoding.Default;
				}
				return WebEncoding.GlobalizationConfig.ResponseEncoding;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x0002B016 File Offset: 0x00029216
		public static Encoding RequestEncoding
		{
			get
			{
				if (WebEncoding.GlobalizationConfig == null)
				{
					return Encoding.Default;
				}
				return WebEncoding.GlobalizationConfig.RequestEncoding;
			}
		}

		// Token: 0x04001222 RID: 4642
		private static bool cached;

		// Token: 0x04001223 RID: 4643
		private static GlobalizationSection sect;
	}
}
