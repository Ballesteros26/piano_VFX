using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020004EB RID: 1259
	internal sealed class AvtEvent : TextEvent
	{
		// Token: 0x06003347 RID: 13127 RVA: 0x001258CB File Offset: 0x00123ACB
		public AvtEvent(int key)
		{
			this.key = key;
		}

		// Token: 0x06003348 RID: 13128 RVA: 0x001258DA File Offset: 0x00123ADA
		public override bool Output(Processor processor, ActionFrame frame)
		{
			return processor.TextEvent(processor.EvaluateString(frame, this.key));
		}

		// Token: 0x06003349 RID: 13129 RVA: 0x001258EF File Offset: 0x00123AEF
		public override string Evaluate(Processor processor, ActionFrame frame)
		{
			return processor.EvaluateString(frame, this.key);
		}

		// Token: 0x04002125 RID: 8485
		private int key;
	}
}
