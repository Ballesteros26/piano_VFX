using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200000C RID: 12
	public struct Cursor : IEquatable<Cursor>
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002EC2 File Offset: 0x000010C2
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002ECA File Offset: 0x000010CA
		public Texture2D texture { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002ED3 File Offset: 0x000010D3
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002EDB File Offset: 0x000010DB
		public Vector2 hotspot { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002EE4 File Offset: 0x000010E4
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002EEC File Offset: 0x000010EC
		internal int defaultCursorId { get; set; }

		// Token: 0x06000045 RID: 69 RVA: 0x00002EF8 File Offset: 0x000010F8
		public override bool Equals(object obj)
		{
			return obj is Cursor && this.Equals((Cursor)obj);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002F24 File Offset: 0x00001124
		public bool Equals(Cursor other)
		{
			return EqualityComparer<Texture2D>.Default.Equals(this.texture, other.texture) && this.hotspot.Equals(other.hotspot) && this.defaultCursorId == other.defaultCursorId;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002F78 File Offset: 0x00001178
		public override int GetHashCode()
		{
			int num = 1500536833;
			num = num * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(this.texture);
			num = num * -1521134295 + EqualityComparer<Vector2>.Default.GetHashCode(this.hotspot);
			return num * -1521134295 + this.defaultCursorId.GetHashCode();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002FDC File Offset: 0x000011DC
		public static bool operator ==(Cursor style1, Cursor style2)
		{
			return style1.Equals(style2);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002FF8 File Offset: 0x000011F8
		public static bool operator !=(Cursor style1, Cursor style2)
		{
			return !(style1 == style2);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003014 File Offset: 0x00001214
		public override string ToString()
		{
			return string.Format("texture={0}, hotspot={1}", this.texture, this.hotspot);
		}
	}
}
