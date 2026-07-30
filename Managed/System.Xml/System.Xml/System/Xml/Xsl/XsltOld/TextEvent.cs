using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200054A RID: 1354
	internal class TextEvent : Event
	{
		// Token: 0x060036AE RID: 13998 RVA: 0x00132185 File Offset: 0x00130385
		protected TextEvent()
		{
		}

		// Token: 0x060036AF RID: 13999 RVA: 0x0013218D File Offset: 0x0013038D
		public TextEvent(string text)
		{
			this.text = text;
		}

		// Token: 0x060036B0 RID: 14000 RVA: 0x0013219C File Offset: 0x0013039C
		public TextEvent(Compiler compiler)
		{
			NavigatorInput input = compiler.Input;
			this.text = input.Value;
		}

		// Token: 0x060036B1 RID: 14001 RVA: 0x001321C2 File Offset: 0x001303C2
		public override bool Output(Processor processor, ActionFrame frame)
		{
			return processor.TextEvent(this.text);
		}

		// Token: 0x060036B2 RID: 14002 RVA: 0x001321D0 File Offset: 0x001303D0
		public virtual string Evaluate(Processor processor, ActionFrame frame)
		{
			return this.text;
		}

		// Token: 0x0400230F RID: 8975
		private string text;
	}
}
