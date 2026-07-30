using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200017F RID: 383
	public sealed class MidiReader : IDisposable
	{
		// Token: 0x06000961 RID: 2401 RVA: 0x000211EC File Offset: 0x0001F3EC
		public MidiReader(Stream stream, ReaderSettings settings)
		{
			ThrowIfArgument.IsNull("stream", stream);
			ThrowIfArgument.IsNull("settings", settings);
			this._settings = settings;
			if (!stream.CanSeek)
			{
				stream = new StreamWrapper(stream, settings.NonSeekableStreamBufferSize);
				this._isStreamWrapped = true;
			}
			this.Length = stream.Length;
			if (settings.ReadFromMemory && !(stream is MemoryStream))
			{
				this._allDataBuffer = new MemoryStream();
				stream.CopyTo(this._allDataBuffer);
				this._allDataBuffer.Position = 0L;
				stream = this._allDataBuffer;
			}
			this._binaryReader = new BinaryReader(stream, SmfConstants.DefaultTextEncoding, true);
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x00021293 File Offset: 0x0001F493
		// (set) Token: 0x06000963 RID: 2403 RVA: 0x000212A5 File Offset: 0x0001F4A5
		public long Position
		{
			get
			{
				return this._binaryReader.BaseStream.Position;
			}
			set
			{
				this._binaryReader.BaseStream.Position = value;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x000212B8 File Offset: 0x0001F4B8
		public long Length { get; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x000212C0 File Offset: 0x0001F4C0
		public bool EndReached
		{
			get
			{
				return this.Position >= this.Length || (this._isStreamWrapped && ((StreamWrapper)this._binaryReader.BaseStream).IsEndReached());
			}
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x000212F4 File Offset: 0x0001F4F4
		public byte[] ReadAllBytes()
		{
			byte[] array2;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				byte[] array = new byte[512];
				int num;
				while ((num = this._binaryReader.Read(array, 0, array.Length)) != 0)
				{
					memoryStream.Write(array, 0, num);
				}
				array2 = memoryStream.ToArray();
			}
			return array2;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00021358 File Offset: 0x0001F558
		public byte ReadByte()
		{
			return this._binaryReader.ReadByte();
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00021365 File Offset: 0x0001F565
		public sbyte ReadSByte()
		{
			return this._binaryReader.ReadSByte();
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00021374 File Offset: 0x0001F574
		public byte[] ReadBytes(int count)
		{
			if (this._isStreamWrapped && count > this._settings.NonSeekableStreamIncrementalBytesReadingThreshold)
			{
				List<byte[]> list = new List<byte[]>();
				while (count > 0)
				{
					byte[] array = this._binaryReader.ReadBytes(Math.Min(count, this._settings.NonSeekableStreamIncrementalBytesReadingStep));
					if (array.Length == 0)
					{
						break;
					}
					count -= array.Length;
					list.Add(array);
				}
				return list.SelectMany((byte[] bytes) => bytes).ToArray<byte>();
			}
			return this._binaryReader.ReadBytes(count);
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0002140C File Offset: 0x0001F60C
		public ushort ReadWord()
		{
			byte[] array = this.ReadBytes(2);
			if (array.Length < 2)
			{
				throw new NotEnoughBytesException("Not enough bytes in the stream to read a WORD.", 2L, (long)array.Length);
			}
			return (ushort)(((int)array[0] << 8) + (int)array[1]);
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00021444 File Offset: 0x0001F644
		public uint ReadDword()
		{
			byte[] array = this.ReadBytes(4);
			if (array.Length < 4)
			{
				throw new NotEnoughBytesException("Not enough bytes in the stream to read a DWORD.", 4L, (long)array.Length);
			}
			return (uint)(((int)array[0] << 24) + ((int)array[1] << 16) + ((int)array[2] << 8) + (int)array[3]);
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00021488 File Offset: 0x0001F688
		public short ReadInt16()
		{
			byte[] array = this.ReadBytes(2);
			if (array.Length < 2)
			{
				throw new NotEnoughBytesException("Not enough bytes in the stream to read a INT16.", 2L, (long)array.Length);
			}
			return (short)(((int)array[0] << 8) + (int)array[1]);
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x000214BE File Offset: 0x0001F6BE
		public string ReadString(int count)
		{
			return new string(this._binaryReader.ReadChars(count));
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x000214D1 File Offset: 0x0001F6D1
		public int ReadVlqNumber()
		{
			return (int)this.ReadVlqLongNumber();
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x000214DC File Offset: 0x0001F6DC
		public long ReadVlqLongNumber()
		{
			long num = 0L;
			try
			{
				byte b;
				do
				{
					b = this.ReadByte();
					num = (num << 7) + (long)(b & 127);
				}
				while (b >> 7 != 0);
			}
			catch (EndOfStreamException ex)
			{
				throw new NotEnoughBytesException("Not enough bytes in the stream to read a variable-length quantity number.", ex);
			}
			return num;
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00021524 File Offset: 0x0001F724
		public uint Read3ByteDword()
		{
			byte[] array = this.ReadBytes(3);
			if (array.Length < 3)
			{
				throw new NotEnoughBytesException("Not enough bytes in the stream to read a 3-byte DWORD.", 3L, (long)array.Length);
			}
			return (uint)(((int)array[0] << 16) + ((int)array[1] << 8) + (int)array[2]);
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00021560 File Offset: 0x0001F760
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00021569 File Offset: 0x0001F769
		private void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			if (disposing)
			{
				MemoryStream allDataBuffer = this._allDataBuffer;
				if (allDataBuffer != null)
				{
					allDataBuffer.Dispose();
				}
				this._binaryReader.Dispose();
			}
			this._disposed = true;
		}

		// Token: 0x040008F1 RID: 2289
		private readonly ReaderSettings _settings;

		// Token: 0x040008F2 RID: 2290
		private readonly BinaryReader _binaryReader;

		// Token: 0x040008F3 RID: 2291
		private readonly bool _isStreamWrapped;

		// Token: 0x040008F4 RID: 2292
		private readonly MemoryStream _allDataBuffer;

		// Token: 0x040008F5 RID: 2293
		private bool _disposed;
	}
}
