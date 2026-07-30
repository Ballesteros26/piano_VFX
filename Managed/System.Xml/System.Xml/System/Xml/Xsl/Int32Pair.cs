using System;

namespace System.Xml.Xsl
{
	// Token: 0x020004BD RID: 1213
	internal struct Int32Pair
	{
		// Token: 0x06003143 RID: 12611 RVA: 0x0011C965 File Offset: 0x0011AB65
		public Int32Pair(int left, int right)
		{
			this.left = left;
			this.right = right;
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06003144 RID: 12612 RVA: 0x0011C975 File Offset: 0x0011AB75
		public int Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06003145 RID: 12613 RVA: 0x0011C97D File Offset: 0x0011AB7D
		public int Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x06003146 RID: 12614 RVA: 0x0011C988 File Offset: 0x0011AB88
		public override bool Equals(object other)
		{
			if (other is Int32Pair)
			{
				Int32Pair int32Pair = (Int32Pair)other;
				return this.left == int32Pair.left && this.right == int32Pair.right;
			}
			return false;
		}

		// Token: 0x06003147 RID: 12615 RVA: 0x0011C9C4 File Offset: 0x0011ABC4
		public override int GetHashCode()
		{
			return this.left.GetHashCode() ^ this.right.GetHashCode();
		}

		// Token: 0x04002030 RID: 8240
		private int left;

		// Token: 0x04002031 RID: 8241
		private int right;
	}
}
