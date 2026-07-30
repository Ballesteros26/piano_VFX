using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection.Emit
{
	// Token: 0x0200034E RID: 846
	[StructLayout(LayoutKind.Sequential)]
	internal class ArrayType : SymbolType
	{
		// Token: 0x060025A7 RID: 9639 RVA: 0x00087BF2 File Offset: 0x00085DF2
		internal ArrayType(Type elementType, int rank)
			: base(elementType)
		{
			this.rank = rank;
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x00087C02 File Offset: 0x00085E02
		internal int GetEffectiveRank()
		{
			return this.rank;
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x00087C0C File Offset: 0x00085E0C
		internal override Type InternalResolve()
		{
			Type type = this.m_baseType.InternalResolve();
			if (this.rank == 0)
			{
				return type.MakeArrayType();
			}
			return type.MakeArrayType(this.rank);
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x00087C40 File Offset: 0x00085E40
		internal override Type RuntimeResolve()
		{
			Type type = this.m_baseType.RuntimeResolve();
			if (this.rank == 0)
			{
				return type.MakeArrayType();
			}
			return type.MakeArrayType(this.rank);
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x00003B29 File Offset: 0x00001D29
		protected override bool IsArrayImpl()
		{
			return true;
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x00087C74 File Offset: 0x00085E74
		public override int GetArrayRank()
		{
			if (this.rank != 0)
			{
				return this.rank;
			}
			return 1;
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x00087C88 File Offset: 0x00085E88
		internal override string FormatName(string elementName)
		{
			if (elementName == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(elementName);
			stringBuilder.Append("[");
			for (int i = 1; i < this.rank; i++)
			{
				stringBuilder.Append(",");
			}
			if (this.rank == 1)
			{
				stringBuilder.Append("*");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x040013DE RID: 5086
		private int rank;
	}
}
