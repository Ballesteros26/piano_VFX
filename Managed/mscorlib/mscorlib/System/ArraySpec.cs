using System;
using System.Text;

namespace System
{
	// Token: 0x02000248 RID: 584
	internal class ArraySpec : ModifierSpec
	{
		// Token: 0x06001B51 RID: 6993 RVA: 0x00067104 File Offset: 0x00065304
		internal ArraySpec(int dimensions, bool bound)
		{
			this.dimensions = dimensions;
			this.bound = bound;
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x0006711A File Offset: 0x0006531A
		public Type Resolve(Type type)
		{
			if (this.bound)
			{
				return type.MakeArrayType(1);
			}
			if (this.dimensions == 1)
			{
				return type.MakeArrayType();
			}
			return type.MakeArrayType(this.dimensions);
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x00067148 File Offset: 0x00065348
		public StringBuilder Append(StringBuilder sb)
		{
			if (this.bound)
			{
				return sb.Append("[*]");
			}
			return sb.Append('[').Append(',', this.dimensions - 1).Append(']');
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x0006717C File Offset: 0x0006537C
		public override string ToString()
		{
			return this.Append(new StringBuilder()).ToString();
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001B55 RID: 6997 RVA: 0x0006718E File Offset: 0x0006538E
		public int Rank
		{
			get
			{
				return this.dimensions;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06001B56 RID: 6998 RVA: 0x00067196 File Offset: 0x00065396
		public bool IsBound
		{
			get
			{
				return this.bound;
			}
		}

		// Token: 0x04000F58 RID: 3928
		private int dimensions;

		// Token: 0x04000F59 RID: 3929
		private bool bound;
	}
}
