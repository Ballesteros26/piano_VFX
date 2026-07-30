using System;
using System.Security.Cryptography;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x0200004E RID: 78
	internal class TlsCipherSuite : CipherSuite
	{
		// Token: 0x06000358 RID: 856 RVA: 0x0001291C File Offset: 0x00010B1C
		public TlsCipherSuite(short code, string name, CipherAlgorithmType cipherAlgorithmType, HashAlgorithmType hashAlgorithmType, ExchangeAlgorithmType exchangeAlgorithmType, bool exportable, bool blockMode, byte keyMaterialSize, byte expandedKeyMaterialSize, short effectiveKeyBytes, byte ivSize, byte blockSize)
			: base(code, name, cipherAlgorithmType, hashAlgorithmType, exchangeAlgorithmType, exportable, blockMode, keyMaterialSize, expandedKeyMaterialSize, effectiveKeyBytes, ivSize, blockSize)
		{
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00012950 File Offset: 0x00010B50
		public override byte[] ComputeServerRecordMAC(ContentType contentType, byte[] fragment)
		{
			object obj = this.headerLock;
			byte[] hash;
			lock (obj)
			{
				if (this.header == null)
				{
					this.header = new byte[13];
				}
				ulong num = ((base.Context is ClientContext) ? base.Context.ReadSequenceNumber : base.Context.WriteSequenceNumber);
				base.Write(this.header, 0, num);
				this.header[8] = (byte)contentType;
				base.Write(this.header, 9, base.Context.Protocol);
				base.Write(this.header, 11, (short)fragment.Length);
				KeyedHashAlgorithm serverHMAC = base.ServerHMAC;
				serverHMAC.TransformBlock(this.header, 0, this.header.Length, this.header, 0);
				serverHMAC.TransformBlock(fragment, 0, fragment.Length, fragment, 0);
				serverHMAC.TransformFinalBlock(CipherSuite.EmptyArray, 0, 0);
				hash = serverHMAC.Hash;
			}
			return hash;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00012A50 File Offset: 0x00010C50
		public override byte[] ComputeClientRecordMAC(ContentType contentType, byte[] fragment)
		{
			object obj = this.headerLock;
			byte[] hash;
			lock (obj)
			{
				if (this.header == null)
				{
					this.header = new byte[13];
				}
				ulong num = ((base.Context is ClientContext) ? base.Context.WriteSequenceNumber : base.Context.ReadSequenceNumber);
				base.Write(this.header, 0, num);
				this.header[8] = (byte)contentType;
				base.Write(this.header, 9, base.Context.Protocol);
				base.Write(this.header, 11, (short)fragment.Length);
				KeyedHashAlgorithm clientHMAC = base.ClientHMAC;
				clientHMAC.TransformBlock(this.header, 0, this.header.Length, this.header, 0);
				clientHMAC.TransformBlock(fragment, 0, fragment.Length, fragment, 0);
				clientHMAC.TransformFinalBlock(CipherSuite.EmptyArray, 0, 0);
				hash = clientHMAC.Hash;
			}
			return hash;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00012B50 File Offset: 0x00010D50
		public override void ComputeMasterSecret(byte[] preMasterSecret)
		{
			base.Context.MasterSecret = new byte[preMasterSecret.Length];
			base.Context.MasterSecret = base.PRF(preMasterSecret, "master secret", base.Context.RandomCS, 48);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00012B8C File Offset: 0x00010D8C
		public override void ComputeKeys()
		{
			TlsStream tlsStream = new TlsStream(base.PRF(base.Context.MasterSecret, "key expansion", base.Context.RandomSC, base.KeyBlockSize));
			base.Context.Negotiating.ClientWriteMAC = tlsStream.ReadBytes(base.HashSize);
			base.Context.Negotiating.ServerWriteMAC = tlsStream.ReadBytes(base.HashSize);
			base.Context.ClientWriteKey = tlsStream.ReadBytes((int)base.KeyMaterialSize);
			base.Context.ServerWriteKey = tlsStream.ReadBytes((int)base.KeyMaterialSize);
			if (base.IvSize != 0)
			{
				base.Context.ClientWriteIV = tlsStream.ReadBytes((int)base.IvSize);
				base.Context.ServerWriteIV = tlsStream.ReadBytes((int)base.IvSize);
			}
			else
			{
				base.Context.ClientWriteIV = CipherSuite.EmptyArray;
				base.Context.ServerWriteIV = CipherSuite.EmptyArray;
			}
			ClientSessionCache.SetContextInCache(base.Context);
			tlsStream.Reset();
		}

		// Token: 0x040001B0 RID: 432
		private const int MacHeaderLength = 13;

		// Token: 0x040001B1 RID: 433
		private byte[] header;

		// Token: 0x040001B2 RID: 434
		private object headerLock = new object();
	}
}
