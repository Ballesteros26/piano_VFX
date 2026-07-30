using System;

namespace TMPro
{
	// Token: 0x02000011 RID: 17
	public static class TMP_Compatibility
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002A74 File Offset: 0x00000C74
		public static TextAlignmentOptions ConvertTextAlignmentEnumValues(TextAlignmentOptions oldValue)
		{
			switch (oldValue)
			{
			case (TextAlignmentOptions)0:
				return TextAlignmentOptions.TopLeft;
			case (TextAlignmentOptions)1:
				return TextAlignmentOptions.Top;
			case (TextAlignmentOptions)2:
				return TextAlignmentOptions.TopRight;
			case (TextAlignmentOptions)3:
				return TextAlignmentOptions.TopJustified;
			case (TextAlignmentOptions)4:
				return TextAlignmentOptions.Left;
			case (TextAlignmentOptions)5:
				return TextAlignmentOptions.Center;
			case (TextAlignmentOptions)6:
				return TextAlignmentOptions.Right;
			case (TextAlignmentOptions)7:
				return TextAlignmentOptions.Justified;
			case (TextAlignmentOptions)8:
				return TextAlignmentOptions.BottomLeft;
			case (TextAlignmentOptions)9:
				return TextAlignmentOptions.Bottom;
			case (TextAlignmentOptions)10:
				return TextAlignmentOptions.BottomRight;
			case (TextAlignmentOptions)11:
				return TextAlignmentOptions.BottomJustified;
			case (TextAlignmentOptions)12:
				return TextAlignmentOptions.BaselineLeft;
			case (TextAlignmentOptions)13:
				return TextAlignmentOptions.Baseline;
			case (TextAlignmentOptions)14:
				return TextAlignmentOptions.BaselineRight;
			case (TextAlignmentOptions)15:
				return TextAlignmentOptions.BaselineJustified;
			case (TextAlignmentOptions)16:
				return TextAlignmentOptions.MidlineLeft;
			case (TextAlignmentOptions)17:
				return TextAlignmentOptions.Midline;
			case (TextAlignmentOptions)18:
				return TextAlignmentOptions.MidlineRight;
			case (TextAlignmentOptions)19:
				return TextAlignmentOptions.MidlineJustified;
			case (TextAlignmentOptions)20:
				return TextAlignmentOptions.CaplineLeft;
			case (TextAlignmentOptions)21:
				return TextAlignmentOptions.Capline;
			case (TextAlignmentOptions)22:
				return TextAlignmentOptions.CaplineRight;
			case (TextAlignmentOptions)23:
				return TextAlignmentOptions.CaplineJustified;
			default:
				return TextAlignmentOptions.TopLeft;
			}
		}

		// Token: 0x02000075 RID: 117
		public enum AnchorPositions
		{
			// Token: 0x0400050D RID: 1293
			TopLeft,
			// Token: 0x0400050E RID: 1294
			Top,
			// Token: 0x0400050F RID: 1295
			TopRight,
			// Token: 0x04000510 RID: 1296
			Left,
			// Token: 0x04000511 RID: 1297
			Center,
			// Token: 0x04000512 RID: 1298
			Right,
			// Token: 0x04000513 RID: 1299
			BottomLeft,
			// Token: 0x04000514 RID: 1300
			Bottom,
			// Token: 0x04000515 RID: 1301
			BottomRight,
			// Token: 0x04000516 RID: 1302
			BaseLine,
			// Token: 0x04000517 RID: 1303
			None
		}
	}
}
