using System;
using System.Reflection.Emit;

namespace System.Xml.Serialization
{
	// Token: 0x020002CD RID: 717
	internal class IfState
	{
		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001B0E RID: 6926 RVA: 0x00096897 File Offset: 0x00094A97
		// (set) Token: 0x06001B0F RID: 6927 RVA: 0x0009689F File Offset: 0x00094A9F
		internal Label EndIf
		{
			get
			{
				return this.endIf;
			}
			set
			{
				this.endIf = value;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001B10 RID: 6928 RVA: 0x000968A8 File Offset: 0x00094AA8
		// (set) Token: 0x06001B11 RID: 6929 RVA: 0x000968B0 File Offset: 0x00094AB0
		internal Label ElseBegin
		{
			get
			{
				return this.elseBegin;
			}
			set
			{
				this.elseBegin = value;
			}
		}

		// Token: 0x040015C1 RID: 5569
		private Label elseBegin;

		// Token: 0x040015C2 RID: 5570
		private Label endIf;
	}
}
