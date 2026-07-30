using System;
using System.Linq;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200012D RID: 301
	public abstract class SysExEvent : MidiEvent
	{
		// Token: 0x060007D1 RID: 2001 RVA: 0x0001E39A File Offset: 0x0001C59A
		protected SysExEvent(MidiEventType eventType)
			: base(eventType)
		{
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x0001E44C File Offset: 0x0001C64C
		public bool Completed
		{
			get
			{
				byte[] data = this.Data;
				byte? b = ((data != null) ? new byte?(data.LastOrDefault<byte>()) : null);
				int? num = ((b != null) ? new int?((int)b.GetValueOrDefault()) : null);
				int num2 = 247;
				return (num.GetValueOrDefault() == num2) & (num != null);
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x0001E4B3 File Offset: 0x0001C6B3
		// (set) Token: 0x060007D4 RID: 2004 RVA: 0x0001E4BB File Offset: 0x0001C6BB
		public byte[] Data { get; set; }

		// Token: 0x060007D5 RID: 2005 RVA: 0x0001E4C4 File Offset: 0x0001C6C4
		internal sealed override void Read(MidiReader reader, ReadingSettings settings, int size)
		{
			ThrowIfArgument.IsNegative("size", size, "Non-negative size have to be specified in order to read SysEx event.");
			this.Data = reader.ReadBytes(size);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0001E4E4 File Offset: 0x0001C6E4
		internal sealed override void Write(MidiWriter writer, WritingSettings settings)
		{
			byte[] data = this.Data;
			if (data != null)
			{
				writer.WriteBytes(data);
			}
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0001E502 File Offset: 0x0001C702
		internal sealed override int GetSize(WritingSettings settings)
		{
			byte[] data = this.Data;
			if (data == null)
			{
				return 0;
			}
			return data.Length;
		}

		// Token: 0x0400087C RID: 2172
		public const byte EndOfEventByte = 247;
	}
}
