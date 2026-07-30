using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x02000107 RID: 263
	internal sealed class PlaybackEventsComparer : IComparer<PlaybackEvent>
	{
		// Token: 0x060006EF RID: 1775 RVA: 0x0001BA08 File Offset: 0x00019C08
		public int Compare(PlaybackEvent x, PlaybackEvent y)
		{
			long num = x.RawTime - y.RawTime;
			if (num != 0L)
			{
				return Math.Sign(num);
			}
			ChannelEvent channelEvent = x.Event as ChannelEvent;
			ChannelEvent channelEvent2 = y.Event as ChannelEvent;
			if (channelEvent == null || channelEvent2 == null)
			{
				return 0;
			}
			if (!(channelEvent is NoteEvent) && channelEvent2 is NoteEvent)
			{
				return -1;
			}
			if (channelEvent is NoteEvent && !(channelEvent2 is NoteEvent))
			{
				return 1;
			}
			return 0;
		}
	}
}
