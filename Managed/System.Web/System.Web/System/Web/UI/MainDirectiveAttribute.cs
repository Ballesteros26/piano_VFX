using System;

namespace System.Web.UI
{
	// Token: 0x020001E7 RID: 487
	internal sealed class MainDirectiveAttribute<T>
	{
		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x00035599 File Offset: 0x00033799
		public string UnparsedValue
		{
			get
			{
				return this.unparsedValue;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x000355A1 File Offset: 0x000337A1
		public bool IsExpression
		{
			get
			{
				return this.isExpression;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x000355A9 File Offset: 0x000337A9
		public T Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x000355B1 File Offset: 0x000337B1
		public MainDirectiveAttribute(string value)
		{
			this.unparsedValue = value;
			if (value != null)
			{
				this.isExpression = BaseParser.IsExpression(value);
			}
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x000355CF File Offset: 0x000337CF
		public MainDirectiveAttribute(T value, bool unused)
		{
			this.value = value;
		}

		// Token: 0x04001476 RID: 5238
		private string unparsedValue;

		// Token: 0x04001477 RID: 5239
		private T value;

		// Token: 0x04001478 RID: 5240
		private bool isExpression;
	}
}
