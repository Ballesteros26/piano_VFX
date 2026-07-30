using System;
using System.IO;

namespace System.Resources
{
	// Token: 0x020002B9 RID: 697
	internal class Win32GroupIconResource : Win32Resource
	{
		// Token: 0x06001FC1 RID: 8129 RVA: 0x0007CC69 File Offset: 0x0007AE69
		public Win32GroupIconResource(int id, int language, Win32IconResource[] icons)
			: base(Win32ResourceType.RT_GROUP_ICON, id, language)
		{
			this.icons = icons;
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x0007CC7C File Offset: 0x0007AE7C
		public override void WriteTo(Stream s)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(s))
			{
				binaryWriter.Write(0);
				binaryWriter.Write(1);
				binaryWriter.Write((short)this.icons.Length);
				for (int i = 0; i < this.icons.Length; i++)
				{
					Win32IconResource win32IconResource = this.icons[i];
					ICONDIRENTRY icon = win32IconResource.Icon;
					binaryWriter.Write(icon.bWidth);
					binaryWriter.Write(icon.bHeight);
					binaryWriter.Write(icon.bColorCount);
					binaryWriter.Write(0);
					binaryWriter.Write(icon.wPlanes);
					binaryWriter.Write(icon.wBitCount);
					binaryWriter.Write(icon.image.Length);
					binaryWriter.Write((short)win32IconResource.Name.Id);
				}
			}
		}

		// Token: 0x04001146 RID: 4422
		private Win32IconResource[] icons;
	}
}
