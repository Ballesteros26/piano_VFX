using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200000D RID: 13
	public struct HighlightState
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002930 File Offset: 0x00000B30
		public HighlightState(Color32 color, TMP_Offset padding)
		{
			this.color = color;
			this.padding = padding;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002940 File Offset: 0x00000B40
		public static bool operator ==(HighlightState lhs, HighlightState rhs)
		{
			return lhs.color.Compare(rhs.color) && lhs.padding == rhs.padding;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002968 File Offset: 0x00000B68
		public static bool operator !=(HighlightState lhs, HighlightState rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002974 File Offset: 0x00000B74
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002986 File Offset: 0x00000B86
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002999 File Offset: 0x00000B99
		public bool Equals(HighlightState other)
		{
			return base.Equals(other);
		}

		// Token: 0x04000028 RID: 40
		public Color32 color;

		// Token: 0x04000029 RID: 41
		public TMP_Offset padding;
	}
}
