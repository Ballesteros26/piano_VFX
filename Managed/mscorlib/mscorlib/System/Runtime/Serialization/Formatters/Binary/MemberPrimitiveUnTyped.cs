using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000716 RID: 1814
	internal sealed class MemberPrimitiveUnTyped : IStreamable
	{
		// Token: 0x06004BB6 RID: 19382 RVA: 0x00002111 File Offset: 0x00000311
		internal MemberPrimitiveUnTyped()
		{
		}

		// Token: 0x06004BB7 RID: 19383 RVA: 0x0010E613 File Offset: 0x0010C813
		internal void Set(InternalPrimitiveTypeE typeInformation, object value)
		{
			this.typeInformation = typeInformation;
			this.value = value;
		}

		// Token: 0x06004BB8 RID: 19384 RVA: 0x0010E623 File Offset: 0x0010C823
		internal void Set(InternalPrimitiveTypeE typeInformation)
		{
			this.typeInformation = typeInformation;
		}

		// Token: 0x06004BB9 RID: 19385 RVA: 0x0010E62C File Offset: 0x0010C82C
		public void Write(__BinaryWriter sout)
		{
			sout.WriteValue(this.typeInformation, this.value);
		}

		// Token: 0x06004BBA RID: 19386 RVA: 0x0010E640 File Offset: 0x0010C840
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.value = input.ReadValue(this.typeInformation);
		}

		// Token: 0x06004BBB RID: 19387 RVA: 0x00002194 File Offset: 0x00000394
		public void Dump()
		{
		}

		// Token: 0x06004BBC RID: 19388 RVA: 0x0010E654 File Offset: 0x0010C854
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			if (BCLDebug.CheckEnabled("BINARY"))
			{
				Converter.ToComType(this.typeInformation);
			}
		}

		// Token: 0x04002795 RID: 10133
		internal InternalPrimitiveTypeE typeInformation;

		// Token: 0x04002796 RID: 10134
		internal object value;
	}
}
