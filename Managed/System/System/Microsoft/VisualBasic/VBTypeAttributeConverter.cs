using System;
using System.Reflection;

namespace Microsoft.VisualBasic
{
	// Token: 0x020000E6 RID: 230
	internal sealed class VBTypeAttributeConverter : VBModifierAttributeConverter
	{
		// Token: 0x0600059E RID: 1438 RVA: 0x00013A70 File Offset: 0x00011C70
		private VBTypeAttributeConverter()
		{
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x00013ABD File Offset: 0x00011CBD
		public static VBTypeAttributeConverter Default { get; } = new VBTypeAttributeConverter();

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x00013AC4 File Offset: 0x00011CC4
		protected override string[] Names { get; } = new string[] { "Public", "Friend" };

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00013ACC File Offset: 0x00011CCC
		protected override object[] Values { get; } = new object[]
		{
			TypeAttributes.Public,
			TypeAttributes.NotPublic
		};

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x00013AD4 File Offset: 0x00011CD4
		protected override object DefaultValue
		{
			get
			{
				return TypeAttributes.Public;
			}
		}
	}
}
