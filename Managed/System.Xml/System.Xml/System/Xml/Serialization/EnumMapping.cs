using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002EB RID: 747
	internal class EnumMapping : PrimitiveMapping
	{
		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001BE6 RID: 7142 RVA: 0x00099FF3 File Offset: 0x000981F3
		// (set) Token: 0x06001BE7 RID: 7143 RVA: 0x00099FFB File Offset: 0x000981FB
		internal bool IsFlags
		{
			get
			{
				return this.isFlags;
			}
			set
			{
				this.isFlags = value;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001BE8 RID: 7144 RVA: 0x0009A004 File Offset: 0x00098204
		// (set) Token: 0x06001BE9 RID: 7145 RVA: 0x0009A00C File Offset: 0x0009820C
		internal ConstantMapping[] Constants
		{
			get
			{
				return this.constants;
			}
			set
			{
				this.constants = value;
			}
		}

		// Token: 0x04001617 RID: 5655
		private ConstantMapping[] constants;

		// Token: 0x04001618 RID: 5656
		private bool isFlags;
	}
}
