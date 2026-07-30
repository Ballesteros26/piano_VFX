using System;
using System.CodeDom;

namespace Microsoft.CSharp
{
	// Token: 0x020000EA RID: 234
	internal sealed class CSharpMemberAttributeConverter : CSharpModifierAttributeConverter
	{
		// Token: 0x0600064E RID: 1614 RVA: 0x00019AE0 File Offset: 0x00017CE0
		private CSharpMemberAttributeConverter()
		{
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x00019B74 File Offset: 0x00017D74
		public static CSharpMemberAttributeConverter Default { get; } = new CSharpMemberAttributeConverter();

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x00019B7B File Offset: 0x00017D7B
		protected override string[] Names { get; } = new string[] { "Public", "Protected", "Protected Internal", "Internal", "Private" };

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x00019B83 File Offset: 0x00017D83
		protected override object[] Values { get; } = new object[]
		{
			MemberAttributes.Public,
			MemberAttributes.Family,
			MemberAttributes.FamilyOrAssembly,
			MemberAttributes.Assembly,
			MemberAttributes.Private
		};

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x0001396B File Offset: 0x00011B6B
		protected override object DefaultValue
		{
			get
			{
				return MemberAttributes.Private;
			}
		}
	}
}
