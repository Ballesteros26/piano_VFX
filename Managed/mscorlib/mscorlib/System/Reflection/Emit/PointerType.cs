using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000350 RID: 848
	[StructLayout(LayoutKind.Sequential)]
	internal class PointerType : SymbolType
	{
		// Token: 0x060025B6 RID: 9654 RVA: 0x00087CF1 File Offset: 0x00085EF1
		internal PointerType(Type elementType)
			: base(elementType)
		{
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x00087D42 File Offset: 0x00085F42
		internal override Type InternalResolve()
		{
			return this.m_baseType.InternalResolve().MakePointerType();
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x00003B29 File Offset: 0x00001D29
		protected override bool IsPointerImpl()
		{
			return true;
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x00087D54 File Offset: 0x00085F54
		internal override string FormatName(string elementName)
		{
			if (elementName == null)
			{
				return null;
			}
			return elementName + "*";
		}
	}
}
