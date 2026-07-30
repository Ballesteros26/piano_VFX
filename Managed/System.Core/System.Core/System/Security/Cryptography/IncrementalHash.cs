using System;
using Unity;

namespace System.Security.Cryptography
{
	// Token: 0x02000059 RID: 89
	public sealed class IncrementalHash : IDisposable
	{
		// Token: 0x060001CA RID: 458 RVA: 0x00004EB5 File Offset: 0x000030B5
		private IncrementalHash(HashAlgorithmName name, HashAlgorithm hash)
		{
			this._algorithmName = name;
			this._hash = hash;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00004ECB File Offset: 0x000030CB
		public HashAlgorithmName AlgorithmName
		{
			get
			{
				return this._algorithmName;
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00004ED3 File Offset: 0x000030D3
		public void AppendData(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this.AppendData(data, 0, data.Length);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00004EF0 File Offset: 0x000030F0
		public void AppendData(byte[] data, int offset, int count)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Non negative number is required.");
			}
			if (count < 0 || count > data.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (data.Length - count < offset)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (this._disposed)
			{
				throw new ObjectDisposedException(typeof(IncrementalHash).Name);
			}
			if (this._resetPending)
			{
				this._hash.Initialize();
				this._resetPending = false;
			}
			this._hash.TransformBlock(data, offset, count, null, 0);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00004F90 File Offset: 0x00003190
		public byte[] GetHashAndReset()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(typeof(IncrementalHash).Name);
			}
			if (this._resetPending)
			{
				this._hash.Initialize();
			}
			this._hash.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
			byte[] hash = this._hash.Hash;
			this._resetPending = true;
			return hash;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00004FF2 File Offset: 0x000031F2
		public void Dispose()
		{
			this._disposed = true;
			if (this._hash != null)
			{
				this._hash.Dispose();
				this._hash = null;
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00005015 File Offset: 0x00003215
		public static IncrementalHash CreateHash(HashAlgorithmName hashAlgorithm)
		{
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw new ArgumentException("The hash algorithm name cannot be null or empty.", "hashAlgorithm");
			}
			return new IncrementalHash(hashAlgorithm, IncrementalHash.GetHashAlgorithm(hashAlgorithm));
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00005041 File Offset: 0x00003241
		public static IncrementalHash CreateHMAC(HashAlgorithmName hashAlgorithm, byte[] key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw new ArgumentException("The hash algorithm name cannot be null or empty.", "hashAlgorithm");
			}
			return new IncrementalHash(hashAlgorithm, IncrementalHash.GetHMAC(hashAlgorithm, key));
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000507C File Offset: 0x0000327C
		private static HashAlgorithm GetHashAlgorithm(HashAlgorithmName hashAlgorithm)
		{
			if (hashAlgorithm == HashAlgorithmName.MD5)
			{
				return new MD5CryptoServiceProvider();
			}
			if (hashAlgorithm == HashAlgorithmName.SHA1)
			{
				return new SHA1CryptoServiceProvider();
			}
			if (hashAlgorithm == HashAlgorithmName.SHA256)
			{
				return new SHA256CryptoServiceProvider();
			}
			if (hashAlgorithm == HashAlgorithmName.SHA384)
			{
				return new SHA384CryptoServiceProvider();
			}
			if (hashAlgorithm == HashAlgorithmName.SHA512)
			{
				return new SHA512CryptoServiceProvider();
			}
			throw new CryptographicException(-2146893816);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000050F4 File Offset: 0x000032F4
		private static HashAlgorithm GetHMAC(HashAlgorithmName hashAlgorithm, byte[] key)
		{
			if (hashAlgorithm == HashAlgorithmName.MD5)
			{
				return new HMACMD5(key);
			}
			if (hashAlgorithm == HashAlgorithmName.SHA1)
			{
				return new HMACSHA1(key);
			}
			if (hashAlgorithm == HashAlgorithmName.SHA256)
			{
				return new HMACSHA256(key);
			}
			if (hashAlgorithm == HashAlgorithmName.SHA384)
			{
				return new HMACSHA384(key);
			}
			if (hashAlgorithm == HashAlgorithmName.SHA512)
			{
				return new HMACSHA512(key);
			}
			throw new CryptographicException(-2146893816);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000220F File Offset: 0x0000040F
		internal IncrementalHash()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000267 RID: 615
		private const int NTE_BAD_ALGID = -2146893816;

		// Token: 0x04000268 RID: 616
		private readonly HashAlgorithmName _algorithmName;

		// Token: 0x04000269 RID: 617
		private HashAlgorithm _hash;

		// Token: 0x0400026A RID: 618
		private bool _disposed;

		// Token: 0x0400026B RID: 619
		private bool _resetPending;
	}
}
