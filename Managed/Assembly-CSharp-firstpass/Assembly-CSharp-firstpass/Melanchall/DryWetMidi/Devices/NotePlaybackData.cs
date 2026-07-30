using System;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x020000FC RID: 252
	public sealed class NotePlaybackData
	{
		// Token: 0x06000662 RID: 1634 RVA: 0x0001A2EA File Offset: 0x000184EA
		public NotePlaybackData(SevenBitNumber noteNumber, SevenBitNumber velocity, SevenBitNumber offVelocity, FourBitNumber channel)
			: this(true)
		{
			this.NoteNumber = noteNumber;
			this.Velocity = velocity;
			this.OffVelocity = offVelocity;
			this.Channel = channel;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001A310 File Offset: 0x00018510
		private NotePlaybackData(bool playNote)
		{
			this.PlayNote = playNote;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x0001A31F File Offset: 0x0001851F
		public SevenBitNumber NoteNumber { get; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x0001A327 File Offset: 0x00018527
		public SevenBitNumber Velocity { get; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x0001A32F File Offset: 0x0001852F
		public SevenBitNumber OffVelocity { get; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x0001A337 File Offset: 0x00018537
		public FourBitNumber Channel { get; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x0001A33F File Offset: 0x0001853F
		internal bool PlayNote { get; }

		// Token: 0x06000669 RID: 1641 RVA: 0x0001A347 File Offset: 0x00018547
		internal NoteOnEvent GetNoteOnEvent()
		{
			return new NoteOnEvent(this.NoteNumber, this.Velocity)
			{
				Channel = this.Channel
			};
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001A366 File Offset: 0x00018566
		internal NoteOffEvent GetNoteOffEvent()
		{
			return new NoteOffEvent(this.NoteNumber, this.OffVelocity)
			{
				Channel = this.Channel
			};
		}

		// Token: 0x040007D8 RID: 2008
		public static readonly NotePlaybackData SkipNote = new NotePlaybackData(false);
	}
}
