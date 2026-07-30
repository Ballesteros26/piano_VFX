using System;
using System.IO;
using System.Security.Cryptography;

namespace Mono.Security.Authenticode
{
	// Token: 0x02000095 RID: 149
	internal class AuthenticodeBase
	{
		// Token: 0x060004CB RID: 1227 RVA: 0x0001BE5E File Offset: 0x0001A05E
		public AuthenticodeBase()
		{
			this.fileblock = new byte[4096];
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x0001BE76 File Offset: 0x0001A076
		internal int PEOffset
		{
			get
			{
				if (this.blockNo < 1)
				{
					this.ReadFirstBlock();
				}
				return this.peOffset;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0001BE8D File Offset: 0x0001A08D
		internal int CoffSymbolTableOffset
		{
			get
			{
				if (this.blockNo < 1)
				{
					this.ReadFirstBlock();
				}
				return this.coffSymbolTableOffset;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0001BEA4 File Offset: 0x0001A0A4
		internal int SecurityOffset
		{
			get
			{
				if (this.blockNo < 1)
				{
					this.ReadFirstBlock();
				}
				return this.dirSecurityOffset;
			}
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001BEBB File Offset: 0x0001A0BB
		internal void Open(string filename)
		{
			if (this.fs != null)
			{
				this.Close();
			}
			this.fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
			this.blockNo = 0;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0001BEE1 File Offset: 0x0001A0E1
		internal void Close()
		{
			if (this.fs != null)
			{
				this.fs.Close();
				this.fs = null;
			}
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0001BF00 File Offset: 0x0001A100
		internal void ReadFirstBlock()
		{
			int num = this.ProcessFirstBlock();
			if (num != 0)
			{
				throw new NotSupportedException(Locale.GetText("Cannot sign non PE files, e.g. .CAB or .MSI files (error {0}).", new object[] { num }));
			}
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0001BF38 File Offset: 0x0001A138
		internal int ProcessFirstBlock()
		{
			if (this.fs == null)
			{
				return 1;
			}
			this.fs.Position = 0L;
			this.blockLength = this.fs.Read(this.fileblock, 0, this.fileblock.Length);
			this.blockNo = 1;
			if (this.blockLength < 64)
			{
				return 2;
			}
			if (BitConverterLE.ToUInt16(this.fileblock, 0) != 23117)
			{
				return 3;
			}
			this.peOffset = BitConverterLE.ToInt32(this.fileblock, 60);
			if (this.peOffset > this.fileblock.Length)
			{
				throw new NotSupportedException(string.Format(Locale.GetText("Header size too big (> {0} bytes)."), this.fileblock.Length));
			}
			if ((long)this.peOffset > this.fs.Length)
			{
				return 4;
			}
			if (BitConverterLE.ToUInt32(this.fileblock, this.peOffset) != 17744U)
			{
				return 5;
			}
			this.dirSecurityOffset = BitConverterLE.ToInt32(this.fileblock, this.peOffset + 152);
			this.dirSecuritySize = BitConverterLE.ToInt32(this.fileblock, this.peOffset + 156);
			this.coffSymbolTableOffset = BitConverterLE.ToInt32(this.fileblock, this.peOffset + 12);
			return 0;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0001C070 File Offset: 0x0001A270
		internal byte[] GetSecurityEntry()
		{
			if (this.blockNo < 1)
			{
				this.ReadFirstBlock();
			}
			if (this.dirSecuritySize > 8)
			{
				byte[] array = new byte[this.dirSecuritySize - 8];
				this.fs.Position = (long)(this.dirSecurityOffset + 8);
				this.fs.Read(array, 0, array.Length);
				return array;
			}
			return null;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0001C0CC File Offset: 0x0001A2CC
		internal byte[] GetHash(HashAlgorithm hash)
		{
			if (this.blockNo < 1)
			{
				this.ReadFirstBlock();
			}
			this.fs.Position = (long)this.blockLength;
			int num = 0;
			long num2;
			if (this.dirSecurityOffset > 0)
			{
				if (this.dirSecurityOffset < this.blockLength)
				{
					this.blockLength = this.dirSecurityOffset;
					num2 = 0L;
				}
				else
				{
					num2 = (long)(this.dirSecurityOffset - this.blockLength);
				}
			}
			else if (this.coffSymbolTableOffset > 0)
			{
				this.fileblock[this.PEOffset + 12] = 0;
				this.fileblock[this.PEOffset + 13] = 0;
				this.fileblock[this.PEOffset + 14] = 0;
				this.fileblock[this.PEOffset + 15] = 0;
				this.fileblock[this.PEOffset + 16] = 0;
				this.fileblock[this.PEOffset + 17] = 0;
				this.fileblock[this.PEOffset + 18] = 0;
				this.fileblock[this.PEOffset + 19] = 0;
				if (this.coffSymbolTableOffset < this.blockLength)
				{
					this.blockLength = this.coffSymbolTableOffset;
					num2 = 0L;
				}
				else
				{
					num2 = (long)(this.coffSymbolTableOffset - this.blockLength);
				}
			}
			else
			{
				num = (int)(this.fs.Length & 7L);
				if (num > 0)
				{
					num = 8 - num;
				}
				num2 = this.fs.Length - (long)this.blockLength;
			}
			int num3 = this.peOffset + 88;
			hash.TransformBlock(this.fileblock, 0, num3, this.fileblock, 0);
			num3 += 4;
			hash.TransformBlock(this.fileblock, num3, 60, this.fileblock, num3);
			num3 += 68;
			if (num2 == 0L)
			{
				hash.TransformFinalBlock(this.fileblock, num3, this.blockLength - num3);
			}
			else
			{
				hash.TransformBlock(this.fileblock, num3, this.blockLength - num3, this.fileblock, num3);
				long num4 = num2 >> 12;
				int num5 = (int)(num2 - (num4 << 12));
				if (num5 == 0)
				{
					num4 -= 1L;
					num5 = 4096;
				}
				for (;;)
				{
					long num6 = num4;
					num4 = num6 - 1L;
					if (num6 <= 0L)
					{
						break;
					}
					this.fs.Read(this.fileblock, 0, this.fileblock.Length);
					hash.TransformBlock(this.fileblock, 0, this.fileblock.Length, this.fileblock, 0);
				}
				if (this.fs.Read(this.fileblock, 0, num5) != num5)
				{
					return null;
				}
				if (num > 0)
				{
					hash.TransformBlock(this.fileblock, 0, num5, this.fileblock, 0);
					hash.TransformFinalBlock(new byte[num], 0, num);
				}
				else
				{
					hash.TransformFinalBlock(this.fileblock, 0, num5);
				}
			}
			return hash.Hash;
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0001C364 File Offset: 0x0001A564
		protected byte[] HashFile(string fileName, string hashName)
		{
			byte[] array;
			try
			{
				this.Open(fileName);
				HashAlgorithm hashAlgorithm = HashAlgorithm.Create(hashName);
				byte[] hash = this.GetHash(hashAlgorithm);
				this.Close();
				array = hash;
			}
			catch
			{
				array = null;
			}
			return array;
		}

		// Token: 0x040005BD RID: 1469
		public const string spcIndirectDataContext = "1.3.6.1.4.1.311.2.1.4";

		// Token: 0x040005BE RID: 1470
		private byte[] fileblock;

		// Token: 0x040005BF RID: 1471
		private FileStream fs;

		// Token: 0x040005C0 RID: 1472
		private int blockNo;

		// Token: 0x040005C1 RID: 1473
		private int blockLength;

		// Token: 0x040005C2 RID: 1474
		private int peOffset;

		// Token: 0x040005C3 RID: 1475
		private int dirSecurityOffset;

		// Token: 0x040005C4 RID: 1476
		private int dirSecuritySize;

		// Token: 0x040005C5 RID: 1477
		private int coffSymbolTableOffset;
	}
}
