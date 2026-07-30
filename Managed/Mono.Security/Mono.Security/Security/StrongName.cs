using System;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace Mono.Security
{
	// Token: 0x0200000B RID: 11
	public sealed class StrongName
	{
		// Token: 0x0600004F RID: 79 RVA: 0x000038F3 File Offset: 0x00001AF3
		public StrongName()
		{
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000038FB File Offset: 0x00001AFB
		public StrongName(int keySize)
		{
			this.rsa = new RSAManaged(keySize);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003910 File Offset: 0x00001B10
		public StrongName(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (data.Length == 16)
			{
				int i = 0;
				int num = 0;
				while (i < data.Length)
				{
					num += (int)data[i++];
				}
				if (num == 4)
				{
					this.publicKey = (byte[])data.Clone();
					return;
				}
			}
			else
			{
				this.RSA = CryptoConvert.FromCapiKeyBlob(data);
				if (this.rsa == null)
				{
					throw new ArgumentException("data isn't a correctly encoded RSA public key");
				}
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003983 File Offset: 0x00001B83
		public StrongName(RSA rsa)
		{
			if (rsa == null)
			{
				throw new ArgumentNullException("rsa");
			}
			this.RSA = rsa;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000039A0 File Offset: 0x00001BA0
		private void InvalidateCache()
		{
			this.publicKey = null;
			this.keyToken = null;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000039B0 File Offset: 0x00001BB0
		public bool CanSign
		{
			get
			{
				if (this.rsa == null)
				{
					return false;
				}
				if (this.RSA is RSAManaged)
				{
					return !(this.rsa as RSAManaged).PublicOnly;
				}
				bool flag;
				try
				{
					RSAParameters rsaparameters = this.rsa.ExportParameters(true);
					flag = rsaparameters.D != null && rsaparameters.P != null && rsaparameters.Q != null;
				}
				catch (CryptographicException)
				{
					flag = false;
				}
				return flag;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003A2C File Offset: 0x00001C2C
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00003A47 File Offset: 0x00001C47
		public RSA RSA
		{
			get
			{
				if (this.rsa == null)
				{
					this.rsa = RSA.Create();
				}
				return this.rsa;
			}
			set
			{
				this.rsa = value;
				this.InvalidateCache();
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003A58 File Offset: 0x00001C58
		public byte[] PublicKey
		{
			get
			{
				if (this.publicKey == null)
				{
					byte[] array = CryptoConvert.ToCapiKeyBlob(this.rsa, false);
					this.publicKey = new byte[32 + (this.rsa.KeySize >> 3)];
					this.publicKey[0] = array[4];
					this.publicKey[1] = array[5];
					this.publicKey[2] = array[6];
					this.publicKey[3] = array[7];
					this.publicKey[4] = 4;
					this.publicKey[5] = 128;
					this.publicKey[6] = 0;
					this.publicKey[7] = 0;
					byte[] bytes = BitConverterLE.GetBytes(this.publicKey.Length - 12);
					this.publicKey[8] = bytes[0];
					this.publicKey[9] = bytes[1];
					this.publicKey[10] = bytes[2];
					this.publicKey[11] = bytes[3];
					this.publicKey[12] = 6;
					Buffer.BlockCopy(array, 1, this.publicKey, 13, this.publicKey.Length - 13);
					this.publicKey[23] = 49;
				}
				return (byte[])this.publicKey.Clone();
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003B6C File Offset: 0x00001D6C
		public byte[] PublicKeyToken
		{
			get
			{
				if (this.keyToken == null)
				{
					byte[] array = this.PublicKey;
					if (array == null)
					{
						return null;
					}
					byte[] array2 = HashAlgorithm.Create(this.TokenAlgorithm).ComputeHash(array);
					this.keyToken = new byte[8];
					Buffer.BlockCopy(array2, array2.Length - 8, this.keyToken, 0, 8);
					Array.Reverse<byte>(this.keyToken, 0, 8);
				}
				return (byte[])this.keyToken.Clone();
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003BDB File Offset: 0x00001DDB
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00003BF8 File Offset: 0x00001DF8
		public string TokenAlgorithm
		{
			get
			{
				if (this.tokenAlgorithm == null)
				{
					this.tokenAlgorithm = "SHA1";
				}
				return this.tokenAlgorithm;
			}
			set
			{
				string text = value.ToUpper(CultureInfo.InvariantCulture);
				if (text == "SHA1" || text == "MD5")
				{
					this.tokenAlgorithm = value;
					this.InvalidateCache();
					return;
				}
				throw new ArgumentException("Unsupported hash algorithm for token");
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003C43 File Offset: 0x00001E43
		public byte[] GetBytes()
		{
			return CryptoConvert.ToCapiPrivateKeyBlob(this.RSA);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003C50 File Offset: 0x00001E50
		private uint RVAtoPosition(uint r, int sections, byte[] headers)
		{
			for (int i = 0; i < sections; i++)
			{
				uint num = BitConverterLE.ToUInt32(headers, i * 40 + 20);
				uint num2 = BitConverterLE.ToUInt32(headers, i * 40 + 12);
				int num3 = (int)BitConverterLE.ToUInt32(headers, i * 40 + 8);
				if (num2 <= r && (ulong)r < (ulong)num2 + (ulong)((long)num3))
				{
					return num + r - num2;
				}
			}
			return 0U;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003CA8 File Offset: 0x00001EA8
		internal StrongName.StrongNameSignature StrongHash(Stream stream, StrongName.StrongNameOptions options)
		{
			StrongName.StrongNameSignature strongNameSignature = new StrongName.StrongNameSignature();
			HashAlgorithm hashAlgorithm = HashAlgorithm.Create(this.TokenAlgorithm);
			CryptoStream cryptoStream = new CryptoStream(Stream.Null, hashAlgorithm, CryptoStreamMode.Write);
			byte[] array = new byte[128];
			stream.Read(array, 0, 128);
			if (BitConverterLE.ToUInt16(array, 0) != 23117)
			{
				return null;
			}
			uint num = BitConverterLE.ToUInt32(array, 60);
			cryptoStream.Write(array, 0, 128);
			if (num != 128U)
			{
				byte[] array2 = new byte[num - 128U];
				stream.Read(array2, 0, array2.Length);
				cryptoStream.Write(array2, 0, array2.Length);
			}
			byte[] array3 = new byte[248];
			stream.Read(array3, 0, 248);
			if (BitConverterLE.ToUInt32(array3, 0) != 17744U)
			{
				return null;
			}
			if (BitConverterLE.ToUInt16(array3, 4) != 332)
			{
				return null;
			}
			byte[] array4 = new byte[8];
			Buffer.BlockCopy(array4, 0, array3, 88, 4);
			Buffer.BlockCopy(array4, 0, array3, 152, 8);
			cryptoStream.Write(array3, 0, 248);
			ushort num2 = BitConverterLE.ToUInt16(array3, 6);
			int num3 = (int)(num2 * 40);
			byte[] array5 = new byte[num3];
			stream.Read(array5, 0, num3);
			cryptoStream.Write(array5, 0, num3);
			uint num4 = BitConverterLE.ToUInt32(array3, 232);
			uint num5 = this.RVAtoPosition(num4, (int)num2, array5);
			int num6 = (int)BitConverterLE.ToUInt32(array3, 236);
			byte[] array6 = new byte[num6];
			stream.Position = (long)((ulong)num5);
			stream.Read(array6, 0, num6);
			uint num7 = BitConverterLE.ToUInt32(array6, 32);
			strongNameSignature.SignaturePosition = this.RVAtoPosition(num7, (int)num2, array5);
			strongNameSignature.SignatureLength = BitConverterLE.ToUInt32(array6, 36);
			uint num8 = BitConverterLE.ToUInt32(array6, 8);
			strongNameSignature.MetadataPosition = this.RVAtoPosition(num8, (int)num2, array5);
			strongNameSignature.MetadataLength = BitConverterLE.ToUInt32(array6, 12);
			if (options == StrongName.StrongNameOptions.Metadata)
			{
				cryptoStream.Close();
				hashAlgorithm.Initialize();
				byte[] array7 = new byte[strongNameSignature.MetadataLength];
				stream.Position = (long)((ulong)strongNameSignature.MetadataPosition);
				stream.Read(array7, 0, array7.Length);
				strongNameSignature.Hash = hashAlgorithm.ComputeHash(array7);
				return strongNameSignature;
			}
			for (int i = 0; i < (int)num2; i++)
			{
				uint num9 = BitConverterLE.ToUInt32(array5, i * 40 + 20);
				int num10 = (int)BitConverterLE.ToUInt32(array5, i * 40 + 16);
				byte[] array8 = new byte[num10];
				stream.Position = (long)((ulong)num9);
				stream.Read(array8, 0, num10);
				if (num9 <= strongNameSignature.SignaturePosition && (ulong)strongNameSignature.SignaturePosition < (ulong)num9 + (ulong)((long)num10))
				{
					int num11 = (int)(strongNameSignature.SignaturePosition - num9);
					if (num11 > 0)
					{
						cryptoStream.Write(array8, 0, num11);
					}
					strongNameSignature.Signature = new byte[strongNameSignature.SignatureLength];
					Buffer.BlockCopy(array8, num11, strongNameSignature.Signature, 0, (int)strongNameSignature.SignatureLength);
					Array.Reverse<byte>(strongNameSignature.Signature);
					int num12 = (int)((long)num11 + (long)((ulong)strongNameSignature.SignatureLength));
					int num13 = num10 - num12;
					if (num13 > 0)
					{
						cryptoStream.Write(array8, num12, num13);
					}
				}
				else
				{
					cryptoStream.Write(array8, 0, num10);
				}
			}
			cryptoStream.Close();
			strongNameSignature.Hash = hashAlgorithm.Hash;
			return strongNameSignature;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003FD0 File Offset: 0x000021D0
		public byte[] Hash(string fileName)
		{
			FileStream fileStream = File.OpenRead(fileName);
			StrongName.StrongNameSignature strongNameSignature = this.StrongHash(fileStream, StrongName.StrongNameOptions.Metadata);
			fileStream.Close();
			return strongNameSignature.Hash;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003FF8 File Offset: 0x000021F8
		public bool Sign(string fileName)
		{
			bool flag = false;
			StrongName.StrongNameSignature strongNameSignature;
			using (FileStream fileStream = File.OpenRead(fileName))
			{
				strongNameSignature = this.StrongHash(fileStream, StrongName.StrongNameOptions.Signature);
				fileStream.Close();
			}
			if (strongNameSignature.Hash == null)
			{
				return false;
			}
			byte[] array = null;
			try
			{
				RSAPKCS1SignatureFormatter rsapkcs1SignatureFormatter = new RSAPKCS1SignatureFormatter(this.rsa);
				rsapkcs1SignatureFormatter.SetHashAlgorithm(this.TokenAlgorithm);
				array = rsapkcs1SignatureFormatter.CreateSignature(strongNameSignature.Hash);
				Array.Reverse<byte>(array);
			}
			catch (CryptographicException)
			{
				return false;
			}
			using (FileStream fileStream2 = File.OpenWrite(fileName))
			{
				fileStream2.Position = (long)((ulong)strongNameSignature.SignaturePosition);
				fileStream2.Write(array, 0, array.Length);
				fileStream2.Close();
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000040CC File Offset: 0x000022CC
		public bool Verify(string fileName)
		{
			bool flag = false;
			using (FileStream fileStream = File.OpenRead(fileName))
			{
				flag = this.Verify(fileStream);
				fileStream.Close();
			}
			return flag;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004110 File Offset: 0x00002310
		public bool Verify(Stream stream)
		{
			StrongName.StrongNameSignature strongNameSignature = this.StrongHash(stream, StrongName.StrongNameOptions.Signature);
			if (strongNameSignature.Hash == null)
			{
				return false;
			}
			bool flag;
			try
			{
				AssemblyHashAlgorithm assemblyHashAlgorithm = AssemblyHashAlgorithm.SHA1;
				if (this.tokenAlgorithm == "MD5")
				{
					assemblyHashAlgorithm = AssemblyHashAlgorithm.MD5;
				}
				flag = StrongName.Verify(this.rsa, assemblyHashAlgorithm, strongNameSignature.Hash, strongNameSignature.Signature);
			}
			catch (CryptographicException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004180 File Offset: 0x00002380
		private static bool Verify(RSA rsa, AssemblyHashAlgorithm algorithm, byte[] hash, byte[] signature)
		{
			RSAPKCS1SignatureDeformatter rsapkcs1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
			if (algorithm != AssemblyHashAlgorithm.None)
			{
				if (algorithm == AssemblyHashAlgorithm.MD5)
				{
					rsapkcs1SignatureDeformatter.SetHashAlgorithm("MD5");
					goto IL_0034;
				}
				if (algorithm != AssemblyHashAlgorithm.SHA1)
				{
				}
			}
			rsapkcs1SignatureDeformatter.SetHashAlgorithm("SHA1");
			IL_0034:
			return rsapkcs1SignatureDeformatter.VerifySignature(hash, signature);
		}

		// Token: 0x04000047 RID: 71
		private RSA rsa;

		// Token: 0x04000048 RID: 72
		private byte[] publicKey;

		// Token: 0x04000049 RID: 73
		private byte[] keyToken;

		// Token: 0x0400004A RID: 74
		private string tokenAlgorithm;

		// Token: 0x020000C2 RID: 194
		internal class StrongNameSignature
		{
			// Token: 0x170001C2 RID: 450
			// (get) Token: 0x06000729 RID: 1833 RVA: 0x0002131D File Offset: 0x0001F51D
			// (set) Token: 0x0600072A RID: 1834 RVA: 0x00021325 File Offset: 0x0001F525
			public byte[] Hash
			{
				get
				{
					return this.hash;
				}
				set
				{
					this.hash = value;
				}
			}

			// Token: 0x170001C3 RID: 451
			// (get) Token: 0x0600072B RID: 1835 RVA: 0x0002132E File Offset: 0x0001F52E
			// (set) Token: 0x0600072C RID: 1836 RVA: 0x00021336 File Offset: 0x0001F536
			public byte[] Signature
			{
				get
				{
					return this.signature;
				}
				set
				{
					this.signature = value;
				}
			}

			// Token: 0x170001C4 RID: 452
			// (get) Token: 0x0600072D RID: 1837 RVA: 0x0002133F File Offset: 0x0001F53F
			// (set) Token: 0x0600072E RID: 1838 RVA: 0x00021347 File Offset: 0x0001F547
			public uint MetadataPosition
			{
				get
				{
					return this.metadataPosition;
				}
				set
				{
					this.metadataPosition = value;
				}
			}

			// Token: 0x170001C5 RID: 453
			// (get) Token: 0x0600072F RID: 1839 RVA: 0x00021350 File Offset: 0x0001F550
			// (set) Token: 0x06000730 RID: 1840 RVA: 0x00021358 File Offset: 0x0001F558
			public uint MetadataLength
			{
				get
				{
					return this.metadataLength;
				}
				set
				{
					this.metadataLength = value;
				}
			}

			// Token: 0x170001C6 RID: 454
			// (get) Token: 0x06000731 RID: 1841 RVA: 0x00021361 File Offset: 0x0001F561
			// (set) Token: 0x06000732 RID: 1842 RVA: 0x00021369 File Offset: 0x0001F569
			public uint SignaturePosition
			{
				get
				{
					return this.signaturePosition;
				}
				set
				{
					this.signaturePosition = value;
				}
			}

			// Token: 0x170001C7 RID: 455
			// (get) Token: 0x06000733 RID: 1843 RVA: 0x00021372 File Offset: 0x0001F572
			// (set) Token: 0x06000734 RID: 1844 RVA: 0x0002137A File Offset: 0x0001F57A
			public uint SignatureLength
			{
				get
				{
					return this.signatureLength;
				}
				set
				{
					this.signatureLength = value;
				}
			}

			// Token: 0x170001C8 RID: 456
			// (get) Token: 0x06000735 RID: 1845 RVA: 0x00021383 File Offset: 0x0001F583
			// (set) Token: 0x06000736 RID: 1846 RVA: 0x0002138B File Offset: 0x0001F58B
			public byte CliFlag
			{
				get
				{
					return this.cliFlag;
				}
				set
				{
					this.cliFlag = value;
				}
			}

			// Token: 0x170001C9 RID: 457
			// (get) Token: 0x06000737 RID: 1847 RVA: 0x00021394 File Offset: 0x0001F594
			// (set) Token: 0x06000738 RID: 1848 RVA: 0x0002139C File Offset: 0x0001F59C
			public uint CliFlagPosition
			{
				get
				{
					return this.cliFlagPosition;
				}
				set
				{
					this.cliFlagPosition = value;
				}
			}

			// Token: 0x040004B3 RID: 1203
			private byte[] hash;

			// Token: 0x040004B4 RID: 1204
			private byte[] signature;

			// Token: 0x040004B5 RID: 1205
			private uint signaturePosition;

			// Token: 0x040004B6 RID: 1206
			private uint signatureLength;

			// Token: 0x040004B7 RID: 1207
			private uint metadataPosition;

			// Token: 0x040004B8 RID: 1208
			private uint metadataLength;

			// Token: 0x040004B9 RID: 1209
			private byte cliFlag;

			// Token: 0x040004BA RID: 1210
			private uint cliFlagPosition;
		}

		// Token: 0x020000C3 RID: 195
		internal enum StrongNameOptions
		{
			// Token: 0x040004BC RID: 1212
			Metadata,
			// Token: 0x040004BD RID: 1213
			Signature
		}
	}
}
