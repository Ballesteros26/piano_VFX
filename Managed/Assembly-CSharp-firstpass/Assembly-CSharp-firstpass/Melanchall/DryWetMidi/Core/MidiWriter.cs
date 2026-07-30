using System;
using System.IO;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000180 RID: 384
	public sealed class MidiWriter : IDisposable
	{
		// Token: 0x06000973 RID: 2419 RVA: 0x0002159A File Offset: 0x0001F79A
		public MidiWriter(Stream stream)
		{
			this._binaryWriter = new BinaryWriter(stream, SmfConstants.DefaultTextEncoding, true);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x000215C0 File Offset: 0x0001F7C0
		public void Flush()
		{
			this._binaryWriter.Flush();
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x000215CD File Offset: 0x0001F7CD
		public void WriteByte(byte value)
		{
			this._binaryWriter.Write(value);
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x000215DB File Offset: 0x0001F7DB
		public void WriteBytes(byte[] bytes)
		{
			ThrowIfArgument.IsNull("bytes", bytes);
			this._binaryWriter.Write(bytes);
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x000215F4 File Offset: 0x0001F7F4
		public void WriteSByte(sbyte value)
		{
			this._binaryWriter.Write(value);
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00021602 File Offset: 0x0001F802
		public void WriteWord(ushort value)
		{
			this._numberBuffer[0] = (byte)((value >> 8) & 255);
			this._numberBuffer[1] = (byte)(value & 255);
			this._binaryWriter.Write(this._numberBuffer, 0, 2);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0002163C File Offset: 0x0001F83C
		public void WriteDword(uint value)
		{
			this._numberBuffer[0] = (byte)((value >> 24) & 255U);
			this._numberBuffer[1] = (byte)((value >> 16) & 255U);
			this._numberBuffer[2] = (byte)((value >> 8) & 255U);
			this._numberBuffer[3] = (byte)(value & 255U);
			this._binaryWriter.Write(this._numberBuffer, 0, 4);
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00021602 File Offset: 0x0001F802
		public void WriteInt16(short value)
		{
			this._numberBuffer[0] = (byte)((value >> 8) & 255);
			this._numberBuffer[1] = (byte)(value & 255);
			this._binaryWriter.Write(this._numberBuffer, 0, 2);
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x000216A4 File Offset: 0x0001F8A4
		public void WriteString(string value)
		{
			char[] array = ((value != null) ? value.ToCharArray() : null);
			if (array != null && array.Length != 0)
			{
				this._binaryWriter.Write(array);
			}
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x000216D1 File Offset: 0x0001F8D1
		public void WriteVlqNumber(int value)
		{
			this.WriteVlqNumber((long)value);
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x000216DC File Offset: 0x0001F8DC
		public void WriteVlqNumber(long value)
		{
			byte[] vlqBytes = value.GetVlqBytes();
			this.WriteBytes(vlqBytes);
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x000216F8 File Offset: 0x0001F8F8
		public void Write3ByteDword(uint value)
		{
			byte[] array = new byte[3];
			int num = array.Length;
			while (--num >= 0)
			{
				array[num] = (byte)(value & 255U);
				value >>= 8;
			}
			this.WriteBytes(array);
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00021731 File Offset: 0x0001F931
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0002173A File Offset: 0x0001F93A
		private void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			if (disposing)
			{
				this._binaryWriter.Dispose();
			}
			this._disposed = true;
		}

		// Token: 0x040008F7 RID: 2295
		private readonly BinaryWriter _binaryWriter;

		// Token: 0x040008F8 RID: 2296
		private readonly byte[] _numberBuffer = new byte[4];

		// Token: 0x040008F9 RID: 2297
		private bool _disposed;
	}
}
