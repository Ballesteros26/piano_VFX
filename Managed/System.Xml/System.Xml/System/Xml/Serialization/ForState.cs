using System;
using System.Reflection.Emit;

namespace System.Xml.Serialization
{
	// Token: 0x020002CB RID: 715
	internal class ForState
	{
		// Token: 0x06001B09 RID: 6921 RVA: 0x00096852 File Offset: 0x00094A52
		internal ForState(LocalBuilder indexVar, Label beginLabel, Label testLabel, object end)
		{
			this.indexVar = indexVar;
			this.beginLabel = beginLabel;
			this.testLabel = testLabel;
			this.end = end;
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001B0A RID: 6922 RVA: 0x00096877 File Offset: 0x00094A77
		internal LocalBuilder Index
		{
			get
			{
				return this.indexVar;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001B0B RID: 6923 RVA: 0x0009687F File Offset: 0x00094A7F
		internal Label BeginLabel
		{
			get
			{
				return this.beginLabel;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001B0C RID: 6924 RVA: 0x00096887 File Offset: 0x00094A87
		internal Label TestLabel
		{
			get
			{
				return this.testLabel;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x0009688F File Offset: 0x00094A8F
		internal object End
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x040015B6 RID: 5558
		private LocalBuilder indexVar;

		// Token: 0x040015B7 RID: 5559
		private Label beginLabel;

		// Token: 0x040015B8 RID: 5560
		private Label testLabel;

		// Token: 0x040015B9 RID: 5561
		private object end;
	}
}
