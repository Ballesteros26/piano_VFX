using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000129 RID: 297
	public abstract class ChannelEvent : MidiEvent
	{
		// Token: 0x060007B4 RID: 1972 RVA: 0x0001E2B0 File Offset: 0x0001C4B0
		protected ChannelEvent(MidiEventType eventType, int parametersCount)
			: base(eventType)
		{
			this._parameters = new byte[parametersCount];
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x0001E2C5 File Offset: 0x0001C4C5
		// (set) Token: 0x060007B6 RID: 1974 RVA: 0x0001E2CD File Offset: 0x0001C4CD
		public FourBitNumber Channel { get; set; }

		// Token: 0x1700011F RID: 287
		protected SevenBitNumber this[int index]
		{
			get
			{
				return (SevenBitNumber)this._parameters[index];
			}
			set
			{
				this._parameters[index] = value;
			}
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0001E2F8 File Offset: 0x0001C4F8
		internal sealed override void Read(MidiReader reader, ReadingSettings settings, int size)
		{
			for (int i = 0; i < this._parameters.Length; i++)
			{
				byte b = reader.ReadByte();
				if (b > SevenBitNumber.MaxValue)
				{
					switch (settings.InvalidChannelEventParameterValuePolicy)
					{
					case InvalidChannelEventParameterValuePolicy.Abort:
						throw new InvalidChannelEventParameterValueException(base.GetType(), b);
					case InvalidChannelEventParameterValuePolicy.ReadValid:
						b &= SevenBitNumber.MaxValue;
						break;
					case InvalidChannelEventParameterValuePolicy.SnapToLimits:
						b = SevenBitNumber.MaxValue;
						break;
					}
				}
				this._parameters[i] = b;
			}
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0001E378 File Offset: 0x0001C578
		internal sealed override void Write(MidiWriter writer, WritingSettings settings)
		{
			writer.WriteBytes(this._parameters);
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0001E386 File Offset: 0x0001C586
		internal sealed override int GetSize(WritingSettings settings)
		{
			return this._parameters.Length;
		}

		// Token: 0x0400084F RID: 2127
		internal readonly byte[] _parameters;
	}
}
