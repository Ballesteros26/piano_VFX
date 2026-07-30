using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Cryptography
{
	/// <summary>Derives a key from a password using an extension of the PBKDF1 algorithm.</summary>
	// Token: 0x0200066F RID: 1647
	[ComVisible(true)]
	public class PasswordDeriveBytes : DeriveBytes
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> class with the password and key salt to use to derive the key.</summary>
		/// <param name="strPassword">The password for which to derive the key. </param>
		/// <param name="rgbSalt">The key salt to use to derive the key. </param>
		// Token: 0x060046B2 RID: 18098 RVA: 0x000F8925 File Offset: 0x000F6B25
		public PasswordDeriveBytes(string strPassword, byte[] rgbSalt)
			: this(strPassword, rgbSalt, new CspParameters())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> class specifying the password and key salt to use to derive the key.</summary>
		/// <param name="password">The password to derive the key for.</param>
		/// <param name="salt">The key salt to use to derive the key.</param>
		// Token: 0x060046B3 RID: 18099 RVA: 0x000F8934 File Offset: 0x000F6B34
		public PasswordDeriveBytes(byte[] password, byte[] salt)
			: this(password, salt, new CspParameters())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> class with the password, key salt, hash name, and number of iterations to use to derive the key.</summary>
		/// <param name="strPassword">The password for which to derive the key. </param>
		/// <param name="rgbSalt">The key salt to use to derive the key. </param>
		/// <param name="strHashName">The name of the hash algorithm for the operation. </param>
		/// <param name="iterations">The number of iterations for the operation. </param>
		// Token: 0x060046B4 RID: 18100 RVA: 0x000F8943 File Offset: 0x000F6B43
		public PasswordDeriveBytes(string strPassword, byte[] rgbSalt, string strHashName, int iterations)
			: this(strPassword, rgbSalt, strHashName, iterations, new CspParameters())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> class specifying the password, key salt, hash name, and iterations to use to derive the key.</summary>
		/// <param name="password">The password to derive the key for.</param>
		/// <param name="salt">The key salt to use to derive the key.</param>
		/// <param name="hashName">The hash algorithm to use to derive the key.</param>
		/// <param name="iterations">The iteration count to use to derive the key.</param>
		// Token: 0x060046B5 RID: 18101 RVA: 0x000F8955 File Offset: 0x000F6B55
		public PasswordDeriveBytes(byte[] password, byte[] salt, string hashName, int iterations)
			: this(password, salt, hashName, iterations, new CspParameters())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> class with the password, key salt, and cryptographic service provider (CSP) parameters to use to derive the key.</summary>
		/// <param name="strPassword">The password for which to derive the key. </param>
		/// <param name="rgbSalt">The key salt to use to derive the key. </param>
		/// <param name="cspParams">The CSP parameters for the operation. </param>
		// Token: 0x060046B6 RID: 18102 RVA: 0x000F8967 File Offset: 0x000F6B67
		public PasswordDeriveBytes(string strPassword, byte[] rgbSalt, CspParameters cspParams)
			: this(strPassword, rgbSalt, "SHA1", 100, cspParams)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> class specifying the password, key salt, and cryptographic service provider (CSP) to use to derive the key.</summary>
		/// <param name="password">The password to derive the key for.</param>
		/// <param name="salt">The key salt to use to derive the key.</param>
		/// <param name="cspParams">The cryptographic service provider (CSP) parameters for the operation.</param>
		// Token: 0x060046B7 RID: 18103 RVA: 0x000F8979 File Offset: 0x000F6B79
		public PasswordDeriveBytes(byte[] password, byte[] salt, CspParameters cspParams)
			: this(password, salt, "SHA1", 100, cspParams)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> class with the password, key salt, hash name, number of iterations, and cryptographic service provider (CSP) parameters to use to derive the key.</summary>
		/// <param name="strPassword">The password for which to derive the key. </param>
		/// <param name="rgbSalt">The key salt to use to derive the key. </param>
		/// <param name="strHashName">The name of the hash algorithm for the operation. </param>
		/// <param name="iterations">The number of iterations for the operation. </param>
		/// <param name="cspParams">The CSP parameters for the operation. </param>
		// Token: 0x060046B8 RID: 18104 RVA: 0x000F898B File Offset: 0x000F6B8B
		public PasswordDeriveBytes(string strPassword, byte[] rgbSalt, string strHashName, int iterations, CspParameters cspParams)
			: this(new UTF8Encoding(false).GetBytes(strPassword), rgbSalt, strHashName, iterations, cspParams)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> class specifying the password, key salt, hash name, iterations, and cryptographic service provider (CSP) to use to derive the key.</summary>
		/// <param name="password">The password to derive the key for.</param>
		/// <param name="salt">The key salt to use to derive the key.</param>
		/// <param name="hashName">The hash algorithm to use to derive the key.</param>
		/// <param name="iterations">The iteration count to use to derive the key.</param>
		/// <param name="cspParams">The cryptographic service provider (CSP) parameters for the operation.</param>
		// Token: 0x060046B9 RID: 18105 RVA: 0x000F89A5 File Offset: 0x000F6BA5
		[SecuritySafeCritical]
		public PasswordDeriveBytes(byte[] password, byte[] salt, string hashName, int iterations, CspParameters cspParams)
		{
			this.IterationCount = iterations;
			this.Salt = salt;
			this.HashName = hashName;
			this._password = password;
		}

		/// <summary>Gets or sets the name of the hash algorithm for the operation.</summary>
		/// <returns>The name of the hash algorithm for the operation.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The name of the hash value is fixed and an attempt is made to change this value. </exception>
		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x060046BA RID: 18106 RVA: 0x000F89CA File Offset: 0x000F6BCA
		// (set) Token: 0x060046BB RID: 18107 RVA: 0x000F89D4 File Offset: 0x000F6BD4
		public string HashName
		{
			get
			{
				return this._hashName;
			}
			set
			{
				if (this._baseValue != null)
				{
					throw new CryptographicException(Environment.GetResourceString("Value of '{0}' cannot be changed after the bytes have been retrieved.", new object[] { "HashName" }));
				}
				this._hashName = value;
				this._hash = (HashAlgorithm)CryptoConfig.CreateFromName(this._hashName);
			}
		}

		/// <summary>Gets or sets the number of iterations for the operation.</summary>
		/// <returns>The number of iterations for the operation.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The number of iterations is fixed and an attempt is made to change this value. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property cannot be set because its value is out of range. This property requires a non-negative number.</exception>
		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x060046BC RID: 18108 RVA: 0x000F8A24 File Offset: 0x000F6C24
		// (set) Token: 0x060046BD RID: 18109 RVA: 0x000F8A2C File Offset: 0x000F6C2C
		public int IterationCount
		{
			get
			{
				return this._iterations;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("Positive number required."));
				}
				if (this._baseValue != null)
				{
					throw new CryptographicException(Environment.GetResourceString("Value of '{0}' cannot be changed after the bytes have been retrieved.", new object[] { "IterationCount" }));
				}
				this._iterations = value;
			}
		}

		/// <summary>Gets or sets the key salt value for the operation.</summary>
		/// <returns>The key salt value for the operation.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key salt value is fixed and an attempt is made to change this value. </exception>
		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x060046BE RID: 18110 RVA: 0x000F8A7F File Offset: 0x000F6C7F
		// (set) Token: 0x060046BF RID: 18111 RVA: 0x000F8A9C File Offset: 0x000F6C9C
		public byte[] Salt
		{
			get
			{
				if (this._salt == null)
				{
					return null;
				}
				return (byte[])this._salt.Clone();
			}
			set
			{
				if (this._baseValue != null)
				{
					throw new CryptographicException(Environment.GetResourceString("Value of '{0}' cannot be changed after the bytes have been retrieved.", new object[] { "Salt" }));
				}
				if (value == null)
				{
					this._salt = null;
					return;
				}
				this._salt = (byte[])value.Clone();
			}
		}

		/// <summary>Returns pseudo-random key bytes.</summary>
		/// <returns>A byte array filled with pseudo-random key bytes.</returns>
		/// <param name="cb">The number of pseudo-random key bytes to generate. </param>
		// Token: 0x060046C0 RID: 18112 RVA: 0x000F8AEC File Offset: 0x000F6CEC
		[SecuritySafeCritical]
		[Obsolete("Rfc2898DeriveBytes replaces PasswordDeriveBytes for deriving key material from a password and is preferred in new applications.")]
		public override byte[] GetBytes(int cb)
		{
			if (cb < 1)
			{
				throw new IndexOutOfRangeException("cb");
			}
			int num = 0;
			byte[] array = new byte[cb];
			if (this._baseValue == null)
			{
				this.ComputeBaseValue();
			}
			else if (this._extra != null)
			{
				num = this._extra.Length - this._extraCount;
				if (num >= cb)
				{
					Buffer.InternalBlockCopy(this._extra, this._extraCount, array, 0, cb);
					if (num > cb)
					{
						this._extraCount += cb;
					}
					else
					{
						this._extra = null;
					}
					return array;
				}
				Buffer.InternalBlockCopy(this._extra, num, array, 0, num);
				this._extra = null;
			}
			byte[] array2 = this.ComputeBytes(cb - num);
			Buffer.InternalBlockCopy(array2, 0, array, num, cb - num);
			if (array2.Length + num > cb)
			{
				this._extra = array2;
				this._extraCount = cb - num;
			}
			return array;
		}

		/// <summary>Resets the state of the operation.</summary>
		// Token: 0x060046C1 RID: 18113 RVA: 0x000F8BB7 File Offset: 0x000F6DB7
		public override void Reset()
		{
			this._prefix = 0;
			this._extra = null;
			this._baseValue = null;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060046C2 RID: 18114 RVA: 0x000F8BD0 File Offset: 0x000F6DD0
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				if (this._hash != null)
				{
					this._hash.Dispose();
				}
				if (this._baseValue != null)
				{
					Array.Clear(this._baseValue, 0, this._baseValue.Length);
				}
				if (this._extra != null)
				{
					Array.Clear(this._extra, 0, this._extra.Length);
				}
				if (this._password != null)
				{
					Array.Clear(this._password, 0, this._password.Length);
				}
				if (this._salt != null)
				{
					Array.Clear(this._salt, 0, this._salt.Length);
				}
			}
		}

		/// <summary>Derives a cryptographic key from the <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> object.</summary>
		/// <returns>The derived key.</returns>
		/// <param name="algname">The algorithm name for which to derive the key. </param>
		/// <param name="alghashname">The hash algorithm name to use to derive the key. </param>
		/// <param name="keySize">The size of the key, in bits, to derive. </param>
		/// <param name="rgbIV">The initialization vector (IV) to use to derive the key. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="keySize" /> parameter is incorrect.-or- The cryptographic service provider (CSP) cannot be acquired.-or- The <paramref name="algname" /> parameter is not a valid algorithm name.-or- The <paramref name="alghashname" /> parameter is not a valid hash algorithm name. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060046C3 RID: 18115 RVA: 0x000F8C6D File Offset: 0x000F6E6D
		[SecuritySafeCritical]
		public byte[] CryptDeriveKey(string algname, string alghashname, int keySize, byte[] rgbIV)
		{
			if (keySize < 0)
			{
				throw new CryptographicException(Environment.GetResourceString("Specified key is not a valid size for this algorithm."));
			}
			throw new NotSupportedException("CspParameters are not supported by Mono");
		}

		// Token: 0x060046C4 RID: 18116 RVA: 0x000F8C90 File Offset: 0x000F6E90
		private byte[] ComputeBaseValue()
		{
			this._hash.Initialize();
			this._hash.TransformBlock(this._password, 0, this._password.Length, this._password, 0);
			if (this._salt != null)
			{
				this._hash.TransformBlock(this._salt, 0, this._salt.Length, this._salt, 0);
			}
			this._hash.TransformFinalBlock(EmptyArray<byte>.Value, 0, 0);
			this._baseValue = this._hash.Hash;
			this._hash.Initialize();
			for (int i = 1; i < this._iterations - 1; i++)
			{
				this._hash.ComputeHash(this._baseValue);
				this._baseValue = this._hash.Hash;
			}
			return this._baseValue;
		}

		// Token: 0x060046C5 RID: 18117 RVA: 0x000F8D60 File Offset: 0x000F6F60
		[SecurityCritical]
		private byte[] ComputeBytes(int cb)
		{
			int num = 0;
			this._hash.Initialize();
			int num2 = this._hash.HashSize / 8;
			byte[] array = new byte[(cb + num2 - 1) / num2 * num2];
			using (CryptoStream cryptoStream = new CryptoStream(Stream.Null, this._hash, CryptoStreamMode.Write))
			{
				this.HashPrefix(cryptoStream);
				cryptoStream.Write(this._baseValue, 0, this._baseValue.Length);
				cryptoStream.Close();
			}
			Buffer.InternalBlockCopy(this._hash.Hash, 0, array, num, num2);
			num += num2;
			while (cb > num)
			{
				this._hash.Initialize();
				using (CryptoStream cryptoStream2 = new CryptoStream(Stream.Null, this._hash, CryptoStreamMode.Write))
				{
					this.HashPrefix(cryptoStream2);
					cryptoStream2.Write(this._baseValue, 0, this._baseValue.Length);
					cryptoStream2.Close();
				}
				Buffer.InternalBlockCopy(this._hash.Hash, 0, array, num, num2);
				num += num2;
			}
			return array;
		}

		// Token: 0x060046C6 RID: 18118 RVA: 0x000F8E80 File Offset: 0x000F7080
		private void HashPrefix(CryptoStream cs)
		{
			int num = 0;
			byte[] array = new byte[] { 48, 48, 48 };
			if (this._prefix > 999)
			{
				throw new CryptographicException(Environment.GetResourceString("Requested number of bytes exceeds the maximum."));
			}
			if (this._prefix >= 100)
			{
				byte[] array2 = array;
				int num2 = 0;
				array2[num2] += (byte)(this._prefix / 100);
				num++;
			}
			if (this._prefix >= 10)
			{
				byte[] array3 = array;
				int num3 = num;
				array3[num3] += (byte)(this._prefix % 100 / 10);
				num++;
			}
			if (this._prefix > 0)
			{
				byte[] array4 = array;
				int num4 = num;
				array4[num4] += (byte)(this._prefix % 10);
				num++;
				cs.Write(array, 0, num);
			}
			this._prefix++;
		}

		// Token: 0x0400245A RID: 9306
		private int _extraCount;

		// Token: 0x0400245B RID: 9307
		private int _prefix;

		// Token: 0x0400245C RID: 9308
		private int _iterations;

		// Token: 0x0400245D RID: 9309
		private byte[] _baseValue;

		// Token: 0x0400245E RID: 9310
		private byte[] _extra;

		// Token: 0x0400245F RID: 9311
		private byte[] _salt;

		// Token: 0x04002460 RID: 9312
		private string _hashName;

		// Token: 0x04002461 RID: 9313
		private byte[] _password;

		// Token: 0x04002462 RID: 9314
		private HashAlgorithm _hash;
	}
}
