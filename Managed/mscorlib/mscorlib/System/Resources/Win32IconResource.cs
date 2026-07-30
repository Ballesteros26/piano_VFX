using System;
using System.IO;

namespace System.Resources
{
	// Token: 0x020002B8 RID: 696
	internal class Win32IconResource : Win32Resource
	{
		// Token: 0x06001FBE RID: 8126 RVA: 0x0007CC2E File Offset: 0x0007AE2E
		public Win32IconResource(int id, int language, ICONDIRENTRY icon)
			: base(Win32ResourceType.RT_ICON, id, language)
		{
			this.icon = icon;
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001FBF RID: 8127 RVA: 0x0007CC40 File Offset: 0x0007AE40
		public ICONDIRENTRY Icon
		{
			get
			{
				return this.icon;
			}
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x0007CC48 File Offset: 0x0007AE48
		public override void WriteTo(Stream s)
		{
			s.Write(this.icon.image, 0, this.icon.image.Length);
		}

		// Token: 0x04001145 RID: 4421
		private ICONDIRENTRY icon;
	}
}
