using System;
using System.Diagnostics;
using System.IO;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000709 RID: 1801
	internal sealed class SerializationHeaderRecord : IStreamable
	{
		// Token: 0x06004B68 RID: 19304 RVA: 0x0010CFAF File Offset: 0x0010B1AF
		internal SerializationHeaderRecord()
		{
		}

		// Token: 0x06004B69 RID: 19305 RVA: 0x0010CFBE File Offset: 0x0010B1BE
		internal SerializationHeaderRecord(BinaryHeaderEnum binaryHeaderEnum, int topId, int headerId, int majorVersion, int minorVersion)
		{
			this.binaryHeaderEnum = binaryHeaderEnum;
			this.topId = topId;
			this.headerId = headerId;
			this.majorVersion = majorVersion;
			this.minorVersion = minorVersion;
		}

		// Token: 0x06004B6A RID: 19306 RVA: 0x0010CFF4 File Offset: 0x0010B1F4
		public void Write(__BinaryWriter sout)
		{
			this.majorVersion = this.binaryFormatterMajorVersion;
			this.minorVersion = this.binaryFormatterMinorVersion;
			sout.WriteByte((byte)this.binaryHeaderEnum);
			sout.WriteInt32(this.topId);
			sout.WriteInt32(this.headerId);
			sout.WriteInt32(this.binaryFormatterMajorVersion);
			sout.WriteInt32(this.binaryFormatterMinorVersion);
		}

		// Token: 0x06004B6B RID: 19307 RVA: 0x000EB8C4 File Offset: 0x000E9AC4
		private static int GetInt32(byte[] buffer, int index)
		{
			return (int)buffer[index] | ((int)buffer[index + 1] << 8) | ((int)buffer[index + 2] << 16) | ((int)buffer[index + 3] << 24);
		}

		// Token: 0x06004B6C RID: 19308 RVA: 0x0010D058 File Offset: 0x0010B258
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			byte[] array = input.ReadBytes(17);
			if (array.Length < 17)
			{
				__Error.EndOfFile();
			}
			this.majorVersion = SerializationHeaderRecord.GetInt32(array, 9);
			if (this.majorVersion > this.binaryFormatterMajorVersion)
			{
				throw new SerializationException(Environment.GetResourceString("The input stream is not a valid binary format. The starting contents (in bytes) are: {0} ...", new object[] { BitConverter.ToString(array) }));
			}
			this.binaryHeaderEnum = (BinaryHeaderEnum)array[0];
			this.topId = SerializationHeaderRecord.GetInt32(array, 1);
			this.headerId = SerializationHeaderRecord.GetInt32(array, 5);
			this.minorVersion = SerializationHeaderRecord.GetInt32(array, 13);
		}

		// Token: 0x06004B6D RID: 19309 RVA: 0x00002194 File Offset: 0x00000394
		public void Dump()
		{
		}

		// Token: 0x06004B6E RID: 19310 RVA: 0x0010CF1D File Offset: 0x0010B11D
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x04002750 RID: 10064
		internal int binaryFormatterMajorVersion = 1;

		// Token: 0x04002751 RID: 10065
		internal int binaryFormatterMinorVersion;

		// Token: 0x04002752 RID: 10066
		internal BinaryHeaderEnum binaryHeaderEnum;

		// Token: 0x04002753 RID: 10067
		internal int topId;

		// Token: 0x04002754 RID: 10068
		internal int headerId;

		// Token: 0x04002755 RID: 10069
		internal int majorVersion;

		// Token: 0x04002756 RID: 10070
		internal int minorVersion;
	}
}
