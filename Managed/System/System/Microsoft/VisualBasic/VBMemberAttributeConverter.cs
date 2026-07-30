using System;
using System.CodeDom;

namespace Microsoft.VisualBasic
{
	// Token: 0x020000E4 RID: 228
	internal sealed class VBMemberAttributeConverter : VBModifierAttributeConverter
	{
		// Token: 0x0600058E RID: 1422 RVA: 0x000138C0 File Offset: 0x00011AC0
		private VBMemberAttributeConverter()
		{
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x00013954 File Offset: 0x00011B54
		public static VBMemberAttributeConverter Default { get; } = new VBMemberAttributeConverter();

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x0001395B File Offset: 0x00011B5B
		protected override string[] Names { get; } = new string[] { "Public", "Protected", "Protected Friend", "Friend", "Private" };

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x00013963 File Offset: 0x00011B63
		protected override object[] Values { get; } = new object[]
		{
			MemberAttributes.Public,
			MemberAttributes.Family,
			MemberAttributes.FamilyOrAssembly,
			MemberAttributes.Assembly,
			MemberAttributes.Private
		};

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0001396B File Offset: 0x00011B6B
		protected override object DefaultValue
		{
			get
			{
				return MemberAttributes.Private;
			}
		}
	}
}
