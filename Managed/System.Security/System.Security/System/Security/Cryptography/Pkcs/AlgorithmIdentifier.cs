using System;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> class defines the algorithm used for a cryptographic operation.</summary>
	// Token: 0x0200001C RID: 28
	public sealed class AlgorithmIdentifier
	{
		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.AlgorithmIdentifier.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> class by using a set of default parameters. </summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x0600005F RID: 95 RVA: 0x00003407 File Offset: 0x00001607
		public AlgorithmIdentifier()
		{
			this._oid = new Oid("1.2.840.113549.3.7", "3des");
			this._params = new byte[0];
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.AlgorithmIdentifier.#ctor(System.Security.Cryptography.Oid)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> class with the specified algorithm identifier.</summary>
		/// <param name="oid">An object identifier for the algorithm.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x06000060 RID: 96 RVA: 0x00003430 File Offset: 0x00001630
		public AlgorithmIdentifier(Oid oid)
		{
			this._oid = oid;
			this._params = new byte[0];
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.AlgorithmIdentifier.#ctor(System.Security.Cryptography.Oid,System.Int32)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> class with the specified algorithm identifier and key length.</summary>
		/// <param name="oid">An object identifier for the algorithm.</param>
		/// <param name="keyLength">The length, in bits, of the key.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x06000061 RID: 97 RVA: 0x0000344B File Offset: 0x0000164B
		public AlgorithmIdentifier(Oid oid, int keyLength)
		{
			this._oid = oid;
			this._length = keyLength;
			this._params = new byte[0];
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.AlgorithmIdentifier.KeyLength" /> property sets or retrieves the key length, in bits. This property is not used for algorithms that use a fixed key length.</summary>
		/// <returns>An int value that represents the key length, in bits.</returns>
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000062 RID: 98 RVA: 0x0000346D File Offset: 0x0000166D
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00003475 File Offset: 0x00001675
		public int KeyLength
		{
			get
			{
				return this._length;
			}
			set
			{
				this._length = value;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.AlgorithmIdentifier.Oid" /> property sets or retrieves the <see cref="T:System.Security.Cryptography.Oid" />  object that specifies the object identifier for the algorithm.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Oid" /> object that represents the algorithm.</returns>
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000064 RID: 100 RVA: 0x0000347E File Offset: 0x0000167E
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00003486 File Offset: 0x00001686
		public Oid Oid
		{
			get
			{
				return this._oid;
			}
			set
			{
				this._oid = value;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.AlgorithmIdentifier.Parameters" /> property sets or retrieves any parameters required by the algorithm.</summary>
		/// <returns>An array of byte values that specifies any parameters required by the algorithm.</returns>
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000066 RID: 102 RVA: 0x0000348F File Offset: 0x0000168F
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00003497 File Offset: 0x00001697
		public byte[] Parameters
		{
			get
			{
				return this._params;
			}
			set
			{
				this._params = value;
			}
		}

		// Token: 0x040000B0 RID: 176
		private Oid _oid;

		// Token: 0x040000B1 RID: 177
		private int _length;

		// Token: 0x040000B2 RID: 178
		private byte[] _params;
	}
}
