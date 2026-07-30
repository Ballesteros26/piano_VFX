using System;
using System.Text;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200013E RID: 318
	public abstract class BaseTextEvent : MetaEvent
	{
		// Token: 0x06000826 RID: 2086 RVA: 0x0001EBEA File Offset: 0x0001CDEA
		public BaseTextEvent(MidiEventType eventType)
			: base(eventType)
		{
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0001EBF3 File Offset: 0x0001CDF3
		public BaseTextEvent(MidiEventType eventType, string text)
			: this(eventType)
		{
			this.Text = text;
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x0001EC03 File Offset: 0x0001CE03
		// (set) Token: 0x06000829 RID: 2089 RVA: 0x0001EC0B File Offset: 0x0001CE0B
		public string Text { get; set; }

		// Token: 0x0600082A RID: 2090 RVA: 0x0001EC14 File Offset: 0x0001CE14
		protected sealed override void ReadContent(MidiReader reader, ReadingSettings settings, int size)
		{
			ThrowIfArgument.IsNegative("size", size, "Text event cannot be read since the size is negative number.");
			if (size == 0)
			{
				this.Text = string.Empty;
				return;
			}
			byte[] array = reader.ReadBytes(size);
			Encoding encoding = settings.TextEncoding ?? SmfConstants.DefaultTextEncoding;
			DecodeTextCallback decodeTextCallback = settings.DecodeTextCallback;
			this.Text = ((decodeTextCallback != null) ? decodeTextCallback(array, settings) : encoding.GetString(array));
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0001EC7C File Offset: 0x0001CE7C
		protected sealed override void WriteContent(MidiWriter writer, WritingSettings settings)
		{
			string text = this.Text;
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			byte[] bytes = (settings.TextEncoding ?? SmfConstants.DefaultTextEncoding).GetBytes(text);
			writer.WriteBytes(bytes);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0001ECB6 File Offset: 0x0001CEB6
		protected sealed override int GetContentSize(WritingSettings settings)
		{
			if (string.IsNullOrEmpty(this.Text))
			{
				return 0;
			}
			return (settings.TextEncoding ?? SmfConstants.DefaultTextEncoding).GetByteCount(this.Text);
		}
	}
}
