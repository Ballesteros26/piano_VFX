using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020004E3 RID: 1251
	internal abstract class Action
	{
		// Token: 0x060032FB RID: 13051
		internal abstract void Execute(Processor processor, ActionFrame frame);

		// Token: 0x060032FC RID: 13052 RVA: 0x00002F50 File Offset: 0x00001150
		internal virtual void ReplaceNamespaceAlias(Compiler compiler)
		{
		}

		// Token: 0x060032FD RID: 13053 RVA: 0x00124A9C File Offset: 0x00122C9C
		internal virtual DbgData GetDbgData(ActionFrame frame)
		{
			return DbgData.Empty;
		}

		// Token: 0x04002104 RID: 8452
		internal const int Initialized = 0;

		// Token: 0x04002105 RID: 8453
		internal const int Finished = -1;
	}
}
