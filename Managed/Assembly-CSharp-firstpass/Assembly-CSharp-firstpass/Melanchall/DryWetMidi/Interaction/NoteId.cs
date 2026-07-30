using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000D7 RID: 215
	public sealed class NoteId
	{
		// Token: 0x06000552 RID: 1362 RVA: 0x00017F0D File Offset: 0x0001610D
		public NoteId(FourBitNumber channel, SevenBitNumber noteNumber)
		{
			this.Channel = channel;
			this.NoteNumber = noteNumber;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x00017F23 File Offset: 0x00016123
		public FourBitNumber Channel { get; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x00017F2B File Offset: 0x0001612B
		public SevenBitNumber NoteNumber { get; }

		// Token: 0x06000555 RID: 1365 RVA: 0x00017F34 File Offset: 0x00016134
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj == this)
			{
				return true;
			}
			NoteId noteId = obj as NoteId;
			return noteId != null && this.Channel == noteId.Channel && this.NoteNumber == noteId.NoteNumber;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00017F8C File Offset: 0x0001618C
		public override int GetHashCode()
		{
			return (17 * 23 + this.Channel.GetHashCode()) * 23 + this.NoteNumber.GetHashCode();
		}
	}
}
