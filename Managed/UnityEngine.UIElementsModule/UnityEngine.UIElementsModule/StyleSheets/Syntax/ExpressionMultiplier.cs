using System;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	// Token: 0x0200027D RID: 637
	internal struct ExpressionMultiplier
	{
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001299 RID: 4761 RVA: 0x000535D0 File Offset: 0x000517D0
		// (set) Token: 0x0600129A RID: 4762 RVA: 0x000535E8 File Offset: 0x000517E8
		public ExpressionMultiplierType type
		{
			get
			{
				return this.m_Type;
			}
			set
			{
				this.SetType(value);
			}
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x000535F4 File Offset: 0x000517F4
		public ExpressionMultiplier(ExpressionMultiplierType type = ExpressionMultiplierType.None)
		{
			this.m_Type = type;
			this.min = (this.max = 1);
			this.SetType(type);
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00053624 File Offset: 0x00051824
		private void SetType(ExpressionMultiplierType value)
		{
			this.m_Type = value;
			switch (value)
			{
			case ExpressionMultiplierType.ZeroOrMore:
				this.min = 0;
				this.max = 10;
				return;
			case ExpressionMultiplierType.OneOrMore:
			case ExpressionMultiplierType.OneOrMoreComma:
			case ExpressionMultiplierType.GroupAtLeastOne:
				this.min = 1;
				this.max = 10;
				return;
			case ExpressionMultiplierType.ZeroOrOne:
				this.min = 0;
				this.max = 1;
				return;
			}
			this.min = (this.max = 1);
		}

		// Token: 0x04000969 RID: 2409
		public const int Infinity = 10;

		// Token: 0x0400096A RID: 2410
		private ExpressionMultiplierType m_Type;

		// Token: 0x0400096B RID: 2411
		public int min;

		// Token: 0x0400096C RID: 2412
		public int max;
	}
}
