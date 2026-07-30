using System;
using System.IO;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000138 RID: 312
	public sealed class BytesToMidiEventConverter : IDisposable
	{
		// Token: 0x0600080D RID: 2061 RVA: 0x0001E82C File Offset: 0x0001CA2C
		public BytesToMidiEventConverter(int capacity)
		{
			ThrowIfArgument.IsNegative("capacity", capacity, "Capacity is negative.");
			this._dataBytesStream = new MemoryStream(capacity);
			this._midiReader = new MidiReader(this._dataBytesStream, new ReaderSettings());
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0001E87C File Offset: 0x0001CA7C
		public BytesToMidiEventConverter()
			: this(0)
		{
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x0001E885 File Offset: 0x0001CA85
		public ReadingSettings ReadingSettings { get; } = new ReadingSettings();

		// Token: 0x06000810 RID: 2064 RVA: 0x0001E890 File Offset: 0x0001CA90
		public MidiEvent Convert(byte statusByte, byte[] dataBytes)
		{
			this._dataBytesStream.Seek(0L, SeekOrigin.Begin);
			if (dataBytes != null)
			{
				this._dataBytesStream.Write(dataBytes, 0, dataBytes.Length);
			}
			this._dataBytesStream.Seek(0L, SeekOrigin.Begin);
			return EventReaderFactory.GetReader(statusByte, false).Read(this._midiReader, this.ReadingSettings, statusByte);
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0001E8E7 File Offset: 0x0001CAE7
		public MidiEvent Convert(byte[] bytes)
		{
			ThrowIfArgument.IsNull("bytes", bytes);
			ThrowIfArgument.IsEmptyCollection<byte>("bytes", bytes, "Bytes is empty array.");
			return this.Convert(bytes, 0, bytes.Length);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0001E910 File Offset: 0x0001CB10
		public MidiEvent Convert(byte[] bytes, int offset, int length)
		{
			ThrowIfArgument.IsNull("bytes", bytes);
			ThrowIfArgument.IsEmptyCollection<byte>("bytes", bytes, "Bytes is empty array.");
			ThrowIfArgument.IsOutOfRange("offset", offset, 0, bytes.Length - 1, "Offset is out of range.");
			ThrowIfArgument.IsOutOfRange("length", length, 0, bytes.Length - offset, "Length is out of range.");
			byte[] array = new byte[bytes.Length - 1 - offset];
			Array.Copy(bytes, offset + 1, array, 0, array.Length);
			return this.Convert(bytes[offset], array);
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0001E989 File Offset: 0x0001CB89
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0001E992 File Offset: 0x0001CB92
		private void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			if (disposing)
			{
				this._dataBytesStream.Dispose();
				this._midiReader.Dispose();
			}
			this._disposed = true;
		}

		// Token: 0x0400088E RID: 2190
		private readonly MemoryStream _dataBytesStream;

		// Token: 0x0400088F RID: 2191
		private readonly MidiReader _midiReader;

		// Token: 0x04000890 RID: 2192
		private bool _disposed;
	}
}
