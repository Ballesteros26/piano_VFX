using System;
using System.IO;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000139 RID: 313
	public sealed class MidiEventToBytesConverter : IDisposable
	{
		// Token: 0x06000815 RID: 2069 RVA: 0x0001E9BD File Offset: 0x0001CBBD
		public MidiEventToBytesConverter(int capacity)
		{
			ThrowIfArgument.IsNegative("capacity", capacity, "Capacity is negative.");
			this._dataBytesStream = new MemoryStream(capacity);
			this._midiWriter = new MidiWriter(this._dataBytesStream);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0001E9FD File Offset: 0x0001CBFD
		public MidiEventToBytesConverter()
			: this(0)
		{
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x0001EA06 File Offset: 0x0001CC06
		public WritingSettings WritingSettings { get; } = new WritingSettings();

		// Token: 0x06000818 RID: 2072 RVA: 0x0001EA0E File Offset: 0x0001CC0E
		public byte[] Convert(MidiEvent midiEvent)
		{
			ThrowIfArgument.IsNull("midiEvent", midiEvent);
			return this.Convert(midiEvent, 0);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0001EA24 File Offset: 0x0001CC24
		public byte[] Convert(MidiEvent midiEvent, int minSize)
		{
			ThrowIfArgument.IsNull("midiEvent", midiEvent);
			ThrowIfArgument.IsNegative("minSize", minSize, "Min size is negative.");
			this._dataBytesStream.Seek(0L, SeekOrigin.Begin);
			EventWriterFactory.GetWriter(midiEvent).Write(midiEvent, this._midiWriter, this.WritingSettings, true);
			Array buffer = this._dataBytesStream.GetBuffer();
			long position = this._dataBytesStream.Position;
			byte[] array = new byte[Math.Max(position, (long)minSize)];
			Array.Copy(buffer, 0L, array, 0L, position);
			return array;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0001EAA6 File Offset: 0x0001CCA6
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0001EAAF File Offset: 0x0001CCAF
		private void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			if (disposing)
			{
				this._dataBytesStream.Dispose();
				this._midiWriter.Dispose();
			}
			this._disposed = true;
		}

		// Token: 0x04000892 RID: 2194
		private readonly MemoryStream _dataBytesStream;

		// Token: 0x04000893 RID: 2195
		private readonly MidiWriter _midiWriter;

		// Token: 0x04000894 RID: 2196
		private bool _disposed;
	}
}
