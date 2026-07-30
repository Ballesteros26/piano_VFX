using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200051C RID: 1308
	internal abstract class Event
	{
		// Token: 0x060034B9 RID: 13497 RVA: 0x00002F50 File Offset: 0x00001150
		public virtual void ReplaceNamespaceAlias(Compiler compiler)
		{
		}

		// Token: 0x060034BA RID: 13498
		public abstract bool Output(Processor processor, ActionFrame frame);

		// Token: 0x060034BB RID: 13499 RVA: 0x0012A11F File Offset: 0x0012831F
		internal void OnInstructionExecute(Processor processor)
		{
			processor.OnInstructionExecute();
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x060034BC RID: 13500 RVA: 0x00124A9C File Offset: 0x00122C9C
		internal virtual DbgData DbgData
		{
			get
			{
				return DbgData.Empty;
			}
		}
	}
}
