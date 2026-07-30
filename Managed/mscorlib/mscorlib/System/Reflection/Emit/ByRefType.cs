using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200034F RID: 847
	[StructLayout(LayoutKind.Sequential)]
	internal class ByRefType : SymbolType
	{
		// Token: 0x060025AE RID: 9646 RVA: 0x00087CF1 File Offset: 0x00085EF1
		internal ByRefType(Type elementType)
			: base(elementType)
		{
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x00087CFA File Offset: 0x00085EFA
		internal override Type InternalResolve()
		{
			return this.m_baseType.InternalResolve().MakeByRefType();
		}

		// Token: 0x060025B0 RID: 9648 RVA: 0x00003B29 File Offset: 0x00001D29
		protected override bool IsByRefImpl()
		{
			return true;
		}

		// Token: 0x060025B1 RID: 9649 RVA: 0x00087D0C File Offset: 0x00085F0C
		internal override string FormatName(string elementName)
		{
			if (elementName == null)
			{
				return null;
			}
			return elementName + "&";
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x00087D1E File Offset: 0x00085F1E
		public override Type MakeArrayType()
		{
			throw new ArgumentException("Cannot create an array type of a byref type");
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x00087D1E File Offset: 0x00085F1E
		public override Type MakeArrayType(int rank)
		{
			throw new ArgumentException("Cannot create an array type of a byref type");
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x00087D2A File Offset: 0x00085F2A
		public override Type MakeByRefType()
		{
			throw new ArgumentException("Cannot create a byref type of an already byref type");
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x00087D36 File Offset: 0x00085F36
		public override Type MakePointerType()
		{
			throw new ArgumentException("Cannot create a pointer type of a byref type");
		}
	}
}
