using System;
using System.Reflection;

namespace Microsoft.CSharp
{
	// Token: 0x020000EC RID: 236
	internal sealed class CSharpTypeAttributeConverter : CSharpModifierAttributeConverter
	{
		// Token: 0x0600065E RID: 1630 RVA: 0x00019C5C File Offset: 0x00017E5C
		private CSharpTypeAttributeConverter()
		{
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x00019CA9 File Offset: 0x00017EA9
		public static CSharpTypeAttributeConverter Default { get; } = new CSharpTypeAttributeConverter();

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x00019CB0 File Offset: 0x00017EB0
		protected override string[] Names { get; } = new string[] { "Public", "Internal" };

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x00019CB8 File Offset: 0x00017EB8
		protected override object[] Values { get; } = new object[]
		{
			TypeAttributes.Public,
			TypeAttributes.NotPublic
		};

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x00019CC0 File Offset: 0x00017EC0
		protected override object DefaultValue
		{
			get
			{
				return TypeAttributes.NotPublic;
			}
		}
	}
}
