using System;
using System.IO;
using System.Security.Cryptography;

namespace Mono.Security.Authenticode
{
	// Token: 0x020000A4 RID: 164
	public class AuthenticodeBase
	{
		// Token: 0x060005FF RID: 1535 RVA: 0x0001CF0E File Offset: 0x0001B10E
		public AuthenticodeBase()
		{
			this.fileblock = new byte[4096];
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x0001CF26 File Offset: 0x0001B126
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

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x0001CF3D File Offset: 0x0001B13D
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

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x0001CF54 File Offset: 0x0001B154
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

		// Token: 0x06000603 RID: 1539 RVA: 0x0001CF6B File Offset: 0x0001B16B
		internal void Open(string filename)
		{
			if (this.fs != null)
			{
				this.Close();
			}
			this.fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
			this.blockNo = 0;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001CF91 File Offset: 0x0001B191
		internal void Close()
		{
			if (this.fs != null)
			{
				this.fs.Close();
				this.fs = null;
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0001CFB0 File Offset: 0x0001B1B0
		internal void ReadFirstBlock()
		{
			int num = this.ProcessFirstBlock();
			if (num != 0)
			{
				throw new NotSupportedException(Locale.GetText("Cannot sign non PE files, e.g. .CAB or .MSI files (error {0}).", new object[] { num }));
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0001CFE8 File Offset: 0x0001B1E8
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

		// Token: 0x06000607 RID: 1543 RVA: 0x0001D120 File Offset: 0x0001B320
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

		// Token: 0x06000608 RID: 1544 RVA: 0x0001D17C File Offset: 0x0001B37C
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

		// Token: 0x06000609 RID: 1545 RVA: 0x0001D414 File Offset: 0x0001B614
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

		// Token: 0x040003FE RID: 1022
		public const string spcIndirectDataContext = "1.3.6.1.4.1.311.2.1.4";

		// Token: 0x040003FF RID: 1023
		private byte[] fileblock;

		// Token: 0x04000400 RID: 1024
		private FileStream fs;

		// Token: 0x04000401 RID: 1025
		private int blockNo;

		// Token: 0x04000402 RID: 1026
		private int blockLength;

		// Token: 0x04000403 RID: 1027
		private int peOffset;

		// Token: 0x04000404 RID: 1028
		private int dirSecurityOffset;

		// Token: 0x04000405 RID: 1029
		private int dirSecuritySize;

		// Token: 0x04000406 RID: 1030
		private int coffSymbolTableOffset;
	}
}
