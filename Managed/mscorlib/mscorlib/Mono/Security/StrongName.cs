using System;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace Mono.Security
{
	// Token: 0x0200004B RID: 75
	internal sealed class StrongName
	{
		// Token: 0x06000207 RID: 519 RVA: 0x00002111 File Offset: 0x00000311
		public StrongName()
		{
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000B3DD File Offset: 0x000095DD
		public StrongName(int keySize)
		{
			this.rsa = new RSAManaged(keySize);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000B3F4 File Offset: 0x000095F4
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

		// Token: 0x0600020A RID: 522 RVA: 0x0000B467 File Offset: 0x00009667
		public StrongName(RSA rsa)
		{
			if (rsa == null)
			{
				throw new ArgumentNullException("rsa");
			}
			this.RSA = rsa;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000B484 File Offset: 0x00009684
		private void InvalidateCache()
		{
			this.publicKey = null;
			this.keyToken = null;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000B494 File Offset: 0x00009694
		public bool CanSign
		{
			get
			{
				if (this.rsa == null)
				{
					return false;
				}
				if (this.RSA is RSACryptoServiceProvider)
				{
					return !(this.rsa as RSACryptoServiceProvider).PublicOnly;
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

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000B530 File Offset: 0x00009730
		// (set) Token: 0x0600020E RID: 526 RVA: 0x0000B54B File Offset: 0x0000974B
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

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000B55C File Offset: 0x0000975C
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

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000B670 File Offset: 0x00009870
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

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000B6DF File Offset: 0x000098DF
		// (set) Token: 0x06000212 RID: 530 RVA: 0x0000B6FC File Offset: 0x000098FC
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

		// Token: 0x06000213 RID: 531 RVA: 0x0000B747 File Offset: 0x00009947
		public byte[] GetBytes()
		{
			return CryptoConvert.ToCapiPrivateKeyBlob(this.RSA);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000B754 File Offset: 0x00009954
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

		// Token: 0x06000215 RID: 533 RVA: 0x0000B7AC File Offset: 0x000099AC
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

		// Token: 0x06000216 RID: 534 RVA: 0x0000BAD4 File Offset: 0x00009CD4
		public byte[] Hash(string fileName)
		{
			FileStream fileStream = File.OpenRead(fileName);
			StrongName.StrongNameSignature strongNameSignature = this.StrongHash(fileStream, StrongName.StrongNameOptions.Metadata);
			fileStream.Close();
			return strongNameSignature.Hash;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000BAFC File Offset: 0x00009CFC
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

		// Token: 0x06000218 RID: 536 RVA: 0x0000BBD0 File Offset: 0x00009DD0
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

		// Token: 0x06000219 RID: 537 RVA: 0x0000BC14 File Offset: 0x00009E14
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

		// Token: 0x0600021A RID: 538 RVA: 0x0000BC84 File Offset: 0x00009E84
		public static bool IsAssemblyStrongnamed(string assemblyName)
		{
			if (!StrongName.initialized)
			{
				object obj = StrongName.lockObject;
				lock (obj)
				{
					if (!StrongName.initialized)
					{
						StrongNameManager.LoadConfig(Environment.GetMachineConfigPath());
						StrongName.initialized = true;
					}
				}
			}
			bool flag;
			try
			{
				AssemblyName assemblyName2 = AssemblyName.GetAssemblyName(assemblyName);
				if (assemblyName2 == null)
				{
					flag = false;
				}
				else
				{
					byte[] mappedPublicKey = StrongNameManager.GetMappedPublicKey(assemblyName2.GetPublicKeyToken());
					if (mappedPublicKey == null || mappedPublicKey.Length < 12)
					{
						mappedPublicKey = assemblyName2.GetPublicKey();
						if (mappedPublicKey == null || mappedPublicKey.Length < 12)
						{
							return false;
						}
					}
					if (!StrongNameManager.MustVerify(assemblyName2))
					{
						flag = true;
					}
					else
					{
						flag = new StrongName(CryptoConvert.FromCapiPublicKeyBlob(mappedPublicKey, 12)).Verify(assemblyName);
					}
				}
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000BD4C File Offset: 0x00009F4C
		public static bool VerifySignature(byte[] publicKey, int algorithm, byte[] hash, byte[] signature)
		{
			bool flag;
			try
			{
				flag = StrongName.Verify(CryptoConvert.FromCapiPublicKeyBlob(publicKey), (AssemblyHashAlgorithm)algorithm, hash, signature);
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000BD80 File Offset: 0x00009F80
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

		// Token: 0x0400046D RID: 1133
		private RSA rsa;

		// Token: 0x0400046E RID: 1134
		private byte[] publicKey;

		// Token: 0x0400046F RID: 1135
		private byte[] keyToken;

		// Token: 0x04000470 RID: 1136
		private string tokenAlgorithm;

		// Token: 0x04000471 RID: 1137
		private static object lockObject = new object();

		// Token: 0x04000472 RID: 1138
		private static bool initialized;

		// Token: 0x0200004C RID: 76
		internal class StrongNameSignature
		{
			// Token: 0x1700004F RID: 79
			// (get) Token: 0x0600021E RID: 542 RVA: 0x0000BDD5 File Offset: 0x00009FD5
			// (set) Token: 0x0600021F RID: 543 RVA: 0x0000BDDD File Offset: 0x00009FDD
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

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x06000220 RID: 544 RVA: 0x0000BDE6 File Offset: 0x00009FE6
			// (set) Token: 0x06000221 RID: 545 RVA: 0x0000BDEE File Offset: 0x00009FEE
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

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x06000222 RID: 546 RVA: 0x0000BDF7 File Offset: 0x00009FF7
			// (set) Token: 0x06000223 RID: 547 RVA: 0x0000BDFF File Offset: 0x00009FFF
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

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x06000224 RID: 548 RVA: 0x0000BE08 File Offset: 0x0000A008
			// (set) Token: 0x06000225 RID: 549 RVA: 0x0000BE10 File Offset: 0x0000A010
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

			// Token: 0x17000053 RID: 83
			// (get) Token: 0x06000226 RID: 550 RVA: 0x0000BE19 File Offset: 0x0000A019
			// (set) Token: 0x06000227 RID: 551 RVA: 0x0000BE21 File Offset: 0x0000A021
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

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x06000228 RID: 552 RVA: 0x0000BE2A File Offset: 0x0000A02A
			// (set) Token: 0x06000229 RID: 553 RVA: 0x0000BE32 File Offset: 0x0000A032
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

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x0600022A RID: 554 RVA: 0x0000BE3B File Offset: 0x0000A03B
			// (set) Token: 0x0600022B RID: 555 RVA: 0x0000BE43 File Offset: 0x0000A043
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

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x0600022C RID: 556 RVA: 0x0000BE4C File Offset: 0x0000A04C
			// (set) Token: 0x0600022D RID: 557 RVA: 0x0000BE54 File Offset: 0x0000A054
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

			// Token: 0x04000473 RID: 1139
			private byte[] hash;

			// Token: 0x04000474 RID: 1140
			private byte[] signature;

			// Token: 0x04000475 RID: 1141
			private uint signaturePosition;

			// Token: 0x04000476 RID: 1142
			private uint signatureLength;

			// Token: 0x04000477 RID: 1143
			private uint metadataPosition;

			// Token: 0x04000478 RID: 1144
			private uint metadataLength;

			// Token: 0x04000479 RID: 1145
			private byte cliFlag;

			// Token: 0x0400047A RID: 1146
			private uint cliFlagPosition;
		}

		// Token: 0x0200004D RID: 77
		internal enum StrongNameOptions
		{
			// Token: 0x0400047C RID: 1148
			Metadata,
			// Token: 0x0400047D RID: 1149
			Signature
		}
	}
}
