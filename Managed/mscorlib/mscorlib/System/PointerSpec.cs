using System;
using System.Text;

namespace System
{
	// Token: 0x02000249 RID: 585
	internal class PointerSpec : ModifierSpec
	{
		// Token: 0x06001B57 RID: 6999 RVA: 0x0006719E File Offset: 0x0006539E
		internal PointerSpec(int pointer_level)
		{
			this.pointer_level = pointer_level;
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x000671B0 File Offset: 0x000653B0
		public Type Resolve(Type type)
		{
			for (int i = 0; i < this.pointer_level; i++)
			{
				type = type.MakePointerType();
			}
			return type;
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x000671D7 File Offset: 0x000653D7
		public StringBuilder Append(StringBuilder sb)
		{
			return sb.Append('*', this.pointer_level);
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x000671E7 File Offset: 0x000653E7
		public override string ToString()
		{
			return this.Append(new StringBuilder()).ToString();
		}

		// Token: 0x04000F5A RID: 3930
		private int pointer_level;
	}
}
