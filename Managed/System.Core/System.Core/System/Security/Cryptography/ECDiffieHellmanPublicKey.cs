using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	/// <summary>Provides an abstract base class from which all <see cref="T:System.Security.Cryptography.ECDiffieHellmanCngPublicKey" /> implementations must inherit. </summary>
	// Token: 0x0200006B RID: 107
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public abstract class ECDiffieHellmanPublicKey : IDisposable
	{
		// Token: 0x06000280 RID: 640 RVA: 0x00005F32 File Offset: 0x00004132
		protected ECDiffieHellmanPublicKey()
		{
			this.m_keyBlob = new byte[0];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.ECDiffieHellmanPublicKey" /> class.</summary>
		/// <param name="keyBlob">A byte array that represents an <see cref="T:System.Security.Cryptography.ECDiffieHellmanPublicKey" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyBlob" /> is null.</exception>
		// Token: 0x06000281 RID: 641 RVA: 0x00005F46 File Offset: 0x00004146
		protected ECDiffieHellmanPublicKey(byte[] keyBlob)
		{
			if (keyBlob == null)
			{
				throw new ArgumentNullException("keyBlob");
			}
			this.m_keyBlob = keyBlob.Clone() as byte[];
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Security.Cryptography.ECDiffieHellman" /> class.</summary>
		// Token: 0x06000282 RID: 642 RVA: 0x00005F6D File Offset: 0x0000416D
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.ECDiffieHellman" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06000283 RID: 643 RVA: 0x00003C4C File Offset: 0x00001E4C
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Serializes the <see cref="T:System.Security.Cryptography.ECDiffieHellmanPublicKey" /> key BLOB to a byte array.</summary>
		/// <returns>A byte array that contains the serialized Elliptic Curve Diffie-Hellman (ECDH) public key.</returns>
		// Token: 0x06000284 RID: 644 RVA: 0x00005F76 File Offset: 0x00004176
		public virtual byte[] ToByteArray()
		{
			return this.m_keyBlob.Clone() as byte[];
		}

		/// <summary>Serializes the <see cref="T:System.Security.Cryptography.ECDiffieHellmanPublicKey" /> public key to an XML string.</summary>
		/// <returns>An XML string that contains the serialized Elliptic Curve Diffie-Hellman (ECDH) public key.</returns>
		// Token: 0x06000285 RID: 645 RVA: 0x00005F88 File Offset: 0x00004188
		public virtual string ToXmlString()
		{
			throw new NotImplementedException(global::SR.GetString("Method not supported. Derived class must override."));
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00005F19 File Offset: 0x00004119
		public virtual ECParameters ExportParameters()
		{
			throw new NotSupportedException(global::SR.GetString("Method not supported. Derived class must override."));
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00005F19 File Offset: 0x00004119
		public virtual ECParameters ExportExplicitParameters()
		{
			throw new NotSupportedException(global::SR.GetString("Method not supported. Derived class must override."));
		}

		// Token: 0x040002BB RID: 699
		private byte[] m_keyBlob;
	}
}
