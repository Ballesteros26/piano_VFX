using System;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	// Token: 0x020000A1 RID: 161
	internal struct GdipPropertyItem
	{
		// Token: 0x06000A04 RID: 2564 RVA: 0x00015D3C File Offset: 0x00013F3C
		internal static void MarshalTo(GdipPropertyItem gdipProp, PropertyItem prop)
		{
			prop.Id = gdipProp.id;
			prop.Len = gdipProp.len;
			prop.Type = gdipProp.type;
			prop.Value = new byte[gdipProp.len];
			Marshal.Copy(gdipProp.value, prop.Value, 0, gdipProp.len);
		}

		// Token: 0x04000605 RID: 1541
		internal int id;

		// Token: 0x04000606 RID: 1542
		internal int len;

		// Token: 0x04000607 RID: 1543
		internal short type;

		// Token: 0x04000608 RID: 1544
		internal IntPtr value;
	}
}
