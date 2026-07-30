using System;
using System.Collections;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020004FA RID: 1274
	internal class CopyCodeAction : Action
	{
		// Token: 0x0600340D RID: 13325 RVA: 0x00128F50 File Offset: 0x00127150
		internal CopyCodeAction()
		{
			this.copyEvents = new ArrayList();
		}

		// Token: 0x0600340E RID: 13326 RVA: 0x00128F63 File Offset: 0x00127163
		internal void AddEvent(Event copyEvent)
		{
			this.copyEvents.Add(copyEvent);
		}

		// Token: 0x0600340F RID: 13327 RVA: 0x00128F72 File Offset: 0x00127172
		internal void AddEvents(ArrayList copyEvents)
		{
			this.copyEvents.AddRange(copyEvents);
		}

		// Token: 0x06003410 RID: 13328 RVA: 0x00128F80 File Offset: 0x00127180
		internal override void ReplaceNamespaceAlias(Compiler compiler)
		{
			int count = this.copyEvents.Count;
			for (int i = 0; i < count; i++)
			{
				((Event)this.copyEvents[i]).ReplaceNamespaceAlias(compiler);
			}
		}

		// Token: 0x06003411 RID: 13329 RVA: 0x00128FBC File Offset: 0x001271BC
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			int state = frame.State;
			if (state != 0)
			{
				if (state != 2)
				{
					return;
				}
			}
			else
			{
				frame.Counter = 0;
				frame.State = 2;
			}
			while (processor.CanContinue && ((Event)this.copyEvents[frame.Counter]).Output(processor, frame))
			{
				if (frame.IncrementCounter() >= this.copyEvents.Count)
				{
					frame.Finished();
					return;
				}
			}
		}

		// Token: 0x06003412 RID: 13330 RVA: 0x00129029 File Offset: 0x00127229
		internal override DbgData GetDbgData(ActionFrame frame)
		{
			return ((Event)this.copyEvents[frame.Counter]).DbgData;
		}

		// Token: 0x04002174 RID: 8564
		private const int Outputting = 2;

		// Token: 0x04002175 RID: 8565
		private ArrayList copyEvents;
	}
}
