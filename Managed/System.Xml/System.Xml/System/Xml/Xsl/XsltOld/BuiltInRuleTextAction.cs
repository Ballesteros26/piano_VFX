using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000550 RID: 1360
	internal class BuiltInRuleTextAction : Action
	{
		// Token: 0x060036CA RID: 14026 RVA: 0x0013257C File Offset: 0x0013077C
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			int state = frame.State;
			if (state != 0)
			{
				if (state != 2)
				{
					return;
				}
				processor.TextEvent(frame.StoredOutput);
				frame.Finished();
				return;
			}
			else
			{
				string text = processor.ValueOf(frame.NodeSet.Current);
				if (processor.TextEvent(text, false))
				{
					frame.Finished();
					return;
				}
				frame.StoredOutput = text;
				frame.State = 2;
				return;
			}
		}

		// Token: 0x0400231C RID: 8988
		private const int ResultStored = 2;
	}
}
