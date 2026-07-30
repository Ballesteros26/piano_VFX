using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Cryptography
{
	/// <summary>Implements password-based key derivation functionality, PBKDF2, by using a pseudo-random number generator based on <see cref="T:System.Security.Cryptography.HMACSHA1" />.</summary>
	// Token: 0x02000674 RID: 1652
	[ComVisible(true)]
	public class Rfc2898DeriveBytes : DeriveBytes
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class using the password and salt size to derive the key.</summary>
		/// <param name="password">The password used to derive the key. </param>
		/// <param name="saltSize">The size of the random salt that you want the class to generate. </param>
		/// <exception cref="T:System.ArgumentException">The specified salt size is smaller than 8 bytes. </exception>
		/// <exception cref="T:System.ArgumentNullException">The password or salt is null. </exception>
		// Token: 0x060046E5 RID: 18149 RVA: 0x000F9273 File Offset: 0x000F7473
		public Rfc2898DeriveBytes(string password, int saltSize)
			: this(password, saltSize, 1000)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class using a password, a salt size, and number of iterations to derive the key.</summary>
		/// <param name="password">The password used to derive the key. </param>
		/// <param name="saltSize">The size of the random salt that you want the class to generate. </param>
		/// <param name="iterations">The number of iterations for the operation. </param>
		/// <exception cref="T:System.ArgumentException">The specified salt size is smaller than 8 bytes or the iteration count is less than 1. </exception>
		/// <exception cref="T:System.ArgumentNullException">The password or salt is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="iterations " />is out of range. This parameter requires a non-negative number.</exception>
		// Token: 0x060046E6 RID: 18150 RVA: 0x000F9284 File Offset: 0x000F7484
		[SecuritySafeCritical]
		public Rfc2898DeriveBytes(string password, int saltSize, int iterations)
		{
			if (saltSize < 0)
			{
				throw new ArgumentOutOfRangeException("saltSize", Environment.GetResourceString("Non-negative number required."));
			}
			byte[] array = new byte[saltSize];
			Utils.StaticRandomNumberGenerator.GetBytes(array);
			this.Salt = array;
			this.IterationCount = iterations;
			this.m_password = new UTF8Encoding(false).GetBytes(password);
			this.m_hmacsha1 = new HMACSHA1(this.m_password);
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class using a password and salt to derive the key.</summary>
		/// <param name="password">The password used to derive the key. </param>
		/// <param name="salt">The key salt used to derive the key. </param>
		/// <exception cref="T:System.ArgumentException">The specified salt size is smaller than 8 bytes or the iteration count is less than 1. </exception>
		/// <exception cref="T:System.ArgumentNullException">The password or salt is null. </exception>
		// Token: 0x060046E7 RID: 18151 RVA: 0x000F92F9 File Offset: 0x000F74F9
		public Rfc2898DeriveBytes(string password, byte[] salt)
			: this(password, salt, 1000)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class using a password, a salt, and number of iterations to derive the key.</summary>
		/// <param name="password">The password used to derive the key. </param>
		/// <param name="salt">The key salt used to derive the key. </param>
		/// <param name="iterations">The number of iterations for the operation. </param>
		/// <exception cref="T:System.ArgumentException">The specified salt size is smaller than 8 bytes or the iteration count is less than 1. </exception>
		/// <exception cref="T:System.ArgumentNullException">The password or salt is null. </exception>
		// Token: 0x060046E8 RID: 18152 RVA: 0x000F9308 File Offset: 0x000F7508
		public Rfc2898DeriveBytes(string password, byte[] salt, int iterations)
			: this(new UTF8Encoding(false).GetBytes(password), salt, iterations)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class using a password, a salt, and number of iterations to derive the key.</summary>
		/// <param name="password">The password used to derive the key. </param>
		/// <param name="salt">The key salt used to derive the key.</param>
		/// <param name="iterations">The number of iterations for the operation. </param>
		/// <exception cref="T:System.ArgumentException">The specified salt size is smaller than 8 bytes or the iteration count is less than 1. </exception>
		/// <exception cref="T:System.ArgumentNullException">The password or salt is null. </exception>
		// Token: 0x060046E9 RID: 18153 RVA: 0x000F931E File Offset: 0x000F751E
		[SecuritySafeCritical]
		public Rfc2898DeriveBytes(byte[] password, byte[] salt, int iterations)
		{
			this.Salt = salt;
			this.IterationCount = iterations;
			this.m_password = password;
			this.m_hmacsha1 = new HMACSHA1(password);
			this.Initialize();
		}

		/// <summary>Gets or sets the number of iterations for the operation.</summary>
		/// <returns>The number of iterations for the operation.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of iterations is less than 1. </exception>
		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x060046EA RID: 18154 RVA: 0x000F934D File Offset: 0x000F754D
		// (set) Token: 0x060046EB RID: 18155 RVA: 0x000F9355 File Offset: 0x000F7555
		public int IterationCount
		{
			get
			{
				return (int)this.m_iterations;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("Positive number required."));
				}
				this.m_iterations = (uint)value;
				this.Initialize();
			}
		}

		/// <summary>Gets or sets the key salt value for the operation.</summary>
		/// <returns>The key salt value for the operation.</returns>
		/// <exception cref="T:System.ArgumentException">The specified salt size is smaller than 8 bytes. </exception>
		/// <exception cref="T:System.ArgumentNullException">The salt is null. </exception>
		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x060046EC RID: 18156 RVA: 0x000F937D File Offset: 0x000F757D
		// (set) Token: 0x060046ED RID: 18157 RVA: 0x000F938F File Offset: 0x000F758F
		public byte[] Salt
		{
			get
			{
				return (byte[])this.m_salt.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length < 8)
				{
					throw new ArgumentException(Environment.GetResourceString("Salt is not at least eight bytes."));
				}
				this.m_salt = (byte[])value.Clone();
				this.Initialize();
			}
		}

		/// <summary>Returns the pseudo-random key for this object.</summary>
		/// <returns>A byte array filled with pseudo-random key bytes.</returns>
		/// <param name="cb">The number of pseudo-random key bytes to generate. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="cb " />is out of range. This parameter requires a non-negative number.</exception>
		// Token: 0x060046EE RID: 18158 RVA: 0x000F93CC File Offset: 0x000F75CC
		public override byte[] GetBytes(int cb)
		{
			if (cb <= 0)
			{
				throw new ArgumentOutOfRangeException("cb", Environment.GetResourceString("Positive number required."));
			}
			byte[] array = new byte[cb];
			int i = 0;
			int num = this.m_endIndex - this.m_startIndex;
			if (num > 0)
			{
				if (cb < num)
				{
					Buffer.InternalBlockCopy(this.m_buffer, this.m_startIndex, array, 0, cb);
					this.m_startIndex += cb;
					return array;
				}
				Buffer.InternalBlockCopy(this.m_buffer, this.m_startIndex, array, 0, num);
				this.m_startIndex = (this.m_endIndex = 0);
				i += num;
			}
			while (i < cb)
			{
				byte[] array2 = this.Func();
				int num2 = cb - i;
				if (num2 <= 20)
				{
					Buffer.InternalBlockCopy(array2, 0, array, i, num2);
					i += num2;
					Buffer.InternalBlockCopy(array2, num2, this.m_buffer, this.m_startIndex, 20 - num2);
					this.m_endIndex += 20 - num2;
					return array;
				}
				Buffer.InternalBlockCopy(array2, 0, array, i, 20);
				i += 20;
			}
			return array;
		}

		/// <summary>Resets the state of the operation.</summary>
		// Token: 0x060046EF RID: 18159 RVA: 0x000F94D4 File Offset: 0x000F76D4
		public override void Reset()
		{
			this.Initialize();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060046F0 RID: 18160 RVA: 0x000F94DC File Offset: 0x000F76DC
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				if (this.m_hmacsha1 != null)
				{
					((IDisposable)this.m_hmacsha1).Dispose();
				}
				if (this.m_buffer != null)
				{
					Array.Clear(this.m_buffer, 0, this.m_buffer.Length);
				}
				if (this.m_salt != null)
				{
					Array.Clear(this.m_salt, 0, this.m_salt.Length);
				}
			}
		}

		// Token: 0x060046F1 RID: 18161 RVA: 0x000F9540 File Offset: 0x000F7740
		private void Initialize()
		{
			if (this.m_buffer != null)
			{
				Array.Clear(this.m_buffer, 0, this.m_buffer.Length);
			}
			this.m_buffer = new byte[20];
			this.m_block = 1U;
			this.m_startIndex = (this.m_endIndex = 0);
		}

		// Token: 0x060046F2 RID: 18162 RVA: 0x000F9590 File Offset: 0x000F7790
		private byte[] Func()
		{
			byte[] array = Utils.Int(this.m_block);
			this.m_hmacsha1.TransformBlock(this.m_salt, 0, this.m_salt.Length, null, 0);
			this.m_hmacsha1.TransformBlock(array, 0, array.Length, null, 0);
			this.m_hmacsha1.TransformFinalBlock(EmptyArray<byte>.Value, 0, 0);
			byte[] array2 = this.m_hmacsha1.HashValue;
			this.m_hmacsha1.Initialize();
			byte[] array3 = array2;
			int num = 2;
			while ((long)num <= (long)((ulong)this.m_iterations))
			{
				this.m_hmacsha1.TransformBlock(array2, 0, array2.Length, null, 0);
				this.m_hmacsha1.TransformFinalBlock(EmptyArray<byte>.Value, 0, 0);
				array2 = this.m_hmacsha1.HashValue;
				for (int i = 0; i < 20; i++)
				{
					byte[] array4 = array3;
					int num2 = i;
					array4[num2] ^= array2[i];
				}
				this.m_hmacsha1.Initialize();
				num++;
			}
			this.m_block += 1U;
			return array3;
		}

		// Token: 0x060046F3 RID: 18163 RVA: 0x000F8C6D File Offset: 0x000F6E6D
		[SecuritySafeCritical]
		public byte[] CryptDeriveKey(string algname, string alghashname, int keySize, byte[] rgbIV)
		{
			if (keySize < 0)
			{
				throw new CryptographicException(Environment.GetResourceString("Specified key is not a valid size for this algorithm."));
			}
			throw new NotSupportedException("CspParameters are not supported by Mono");
		}

		// Token: 0x04002469 RID: 9321
		private byte[] m_buffer;

		// Token: 0x0400246A RID: 9322
		private byte[] m_salt;

		// Token: 0x0400246B RID: 9323
		private HMACSHA1 m_hmacsha1;

		// Token: 0x0400246C RID: 9324
		private byte[] m_password;

		// Token: 0x0400246D RID: 9325
		private uint m_iterations;

		// Token: 0x0400246E RID: 9326
		private uint m_block;

		// Token: 0x0400246F RID: 9327
		private int m_startIndex;

		// Token: 0x04002470 RID: 9328
		private int m_endIndex;

		// Token: 0x04002471 RID: 9329
		private const int BlockSize = 20;
	}
}
