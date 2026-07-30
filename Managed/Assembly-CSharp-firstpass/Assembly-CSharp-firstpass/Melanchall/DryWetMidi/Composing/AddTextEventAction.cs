using System;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Composing
{
	// Token: 0x020001A7 RID: 423
	internal sealed class AddTextEventAction<TEvent> : PatternAction where TEvent : BaseTextEvent
	{
		// Token: 0x06000A2E RID: 2606 RVA: 0x000226F9 File Offset: 0x000208F9
		public AddTextEventAction(string text)
		{
			this.Text = text;
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x00022708 File Offset: 0x00020908
		public string Text { get; }

		// Token: 0x06000A30 RID: 2608 RVA: 0x00022710 File Offset: 0x00020910
		public override PatternActionResult Invoke(long time, PatternContext context)
		{
			if (base.State != PatternActionState.Enabled)
			{
				return PatternActionResult.DoNothing;
			}
			TimedEvent timedEvent = new TimedEvent((BaseTextEvent)Activator.CreateInstance(typeof(TEvent), new object[] { this.Text }), time);
			return new PatternActionResult(new long?(time), new TimedEvent[] { timedEvent });
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0002276A File Offset: 0x0002096A
		public override PatternAction Clone()
		{
			return new AddTextEventAction<TEvent>(this.Text);
		}
	}
}
