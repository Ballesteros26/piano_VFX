using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the base class from which all implementations of cryptographic hash algorithms must derive.</summary>
	// Token: 0x0200065E RID: 1630
	[ComVisible(true)]
	public abstract class HashAlgorithm : IDisposable, ICryptoTransform
	{
		/// <summary>Gets the size, in bits, of the computed hash code.</summary>
		/// <returns>The size, in bits, of the computed hash code.</returns>
		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06004641 RID: 17985 RVA: 0x000F7877 File Offset: 0x000F5A77
		public virtual int HashSize
		{
			get
			{
				return this.HashSizeValue;
			}
		}

		/// <summary>Gets the value of the computed hash code.</summary>
		/// <returns>The current value of the computed hash code.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">
		///   <see cref="F:System.Security.Cryptography.HashAlgorithm.HashValue" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06004642 RID: 17986 RVA: 0x000F787F File Offset: 0x000F5A7F
		public virtual byte[] Hash
		{
			get
			{
				if (this.m_bDisposed)
				{
					throw new ObjectDisposedException(null);
				}
				if (this.State != 0)
				{
					throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Hash must be finalized before the hash value is retrieved."));
				}
				return (byte[])this.HashValue.Clone();
			}
		}

		/// <summary>Creates an instance of the default implementation of a hash algorithm.</summary>
		/// <returns>A new <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" /> instance, unless the default settings have been changed using the &lt;cryptoClass&gt; Element.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06004643 RID: 17987 RVA: 0x000F78B8 File Offset: 0x000F5AB8
		public static HashAlgorithm Create()
		{
			return HashAlgorithm.Create("System.Security.Cryptography.HashAlgorithm");
		}

		/// <summary>Creates an instance of the specified implementation of a hash algorithm.</summary>
		/// <returns>A new instance of the specified hash algorithm, or null if <paramref name="hashName" /> is not a valid hash algorithm.</returns>
		/// <param name="hashName">The hash algorithm implementation to use. The following table shows the valid values for the <paramref name="hashName" /> parameter and the algorithms they map to. Parameter value Implements SHA <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" />SHA1 <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" />System.Security.Cryptography.SHA1 <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" />System.Security.Cryptography.HashAlgorithm <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" />MD5 <see cref="T:System.Security.Cryptography.MD5CryptoServiceProvider" />System.Security.Cryptography.MD5 <see cref="T:System.Security.Cryptography.MD5CryptoServiceProvider" />SHA256 <see cref="T:System.Security.Cryptography.SHA256Managed" />SHA-256 <see cref="T:System.Security.Cryptography.SHA256Managed" />System.Security.Cryptography.SHA256 <see cref="T:System.Security.Cryptography.SHA256Managed" />SHA384 <see cref="T:System.Security.Cryptography.SHA384Managed" />SHA-384 <see cref="T:System.Security.Cryptography.SHA384Managed" />System.Security.Cryptography.SHA384 <see cref="T:System.Security.Cryptography.SHA384Managed" />SHA512 <see cref="T:System.Security.Cryptography.SHA512Managed" />SHA-512 <see cref="T:System.Security.Cryptography.SHA512Managed" />System.Security.Cryptography.SHA512 <see cref="T:System.Security.Cryptography.SHA512Managed" /></param>
		// Token: 0x06004644 RID: 17988 RVA: 0x000F78C4 File Offset: 0x000F5AC4
		public static HashAlgorithm Create(string hashName)
		{
			return (HashAlgorithm)CryptoConfig.CreateFromName(hashName);
		}

		/// <summary>Computes the hash value for the specified <see cref="T:System.IO.Stream" /> object.</summary>
		/// <returns>The computed hash code.</returns>
		/// <param name="inputStream">The input to compute the hash code for. </param>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x06004645 RID: 17989 RVA: 0x000F78D4 File Offset: 0x000F5AD4
		public byte[] ComputeHash(Stream inputStream)
		{
			if (this.m_bDisposed)
			{
				throw new ObjectDisposedException(null);
			}
			byte[] array = new byte[4096];
			int num;
			do
			{
				num = inputStream.Read(array, 0, 4096);
				if (num > 0)
				{
					this.HashCore(array, 0, num);
				}
			}
			while (num > 0);
			this.HashValue = this.HashFinal();
			byte[] array2 = (byte[])this.HashValue.Clone();
			this.Initialize();
			return array2;
		}

		/// <summary>Computes the hash value for the specified byte array.</summary>
		/// <returns>The computed hash code.</returns>
		/// <param name="buffer">The input to compute the hash code for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x06004646 RID: 17990 RVA: 0x000F793C File Offset: 0x000F5B3C
		public byte[] ComputeHash(byte[] buffer)
		{
			if (this.m_bDisposed)
			{
				throw new ObjectDisposedException(null);
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.HashCore(buffer, 0, buffer.Length);
			this.HashValue = this.HashFinal();
			byte[] array = (byte[])this.HashValue.Clone();
			this.Initialize();
			return array;
		}

		/// <summary>Computes the hash value for the specified region of the specified byte array.</summary>
		/// <returns>The computed hash code.</returns>
		/// <param name="buffer">The input to compute the hash code for. </param>
		/// <param name="offset">The offset into the byte array from which to begin using data. </param>
		/// <param name="count">The number of bytes in the array to use as data. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="count" /> is an invalid value.-or-<paramref name="buffer" /> length is invalid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is out of range. This parameter requires a non-negative number.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x06004647 RID: 17991 RVA: 0x000F7994 File Offset: 0x000F5B94
		public byte[] ComputeHash(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("Non-negative number required."));
			}
			if (count < 0 || count > buffer.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Value was invalid."));
			}
			if (buffer.Length - count < offset)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (this.m_bDisposed)
			{
				throw new ObjectDisposedException(null);
			}
			this.HashCore(buffer, offset, count);
			this.HashValue = this.HashFinal();
			byte[] array = (byte[])this.HashValue.Clone();
			this.Initialize();
			return array;
		}

		/// <summary>When overridden in a derived class, gets the input block size.</summary>
		/// <returns>The input block size.</returns>
		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06004648 RID: 17992 RVA: 0x00003B29 File Offset: 0x00001D29
		public virtual int InputBlockSize
		{
			get
			{
				return 1;
			}
		}

		/// <summary>When overridden in a derived class, gets the output block size.</summary>
		/// <returns>The output block size.</returns>
		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06004649 RID: 17993 RVA: 0x00003B29 File Offset: 0x00001D29
		public virtual int OutputBlockSize
		{
			get
			{
				return 1;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether multiple blocks can be transformed.</summary>
		/// <returns>true if multiple blocks can be transformed; otherwise, false.</returns>
		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x0600464A RID: 17994 RVA: 0x00003B29 File Offset: 0x00001D29
		public virtual bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the current transform can be reused.</summary>
		/// <returns>Always true.</returns>
		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x0600464B RID: 17995 RVA: 0x00003B29 File Offset: 0x00001D29
		public virtual bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		/// <summary>Computes the hash value for the specified region of the input byte array and copies the specified region of the input byte array to the specified region of the output byte array.</summary>
		/// <returns>The number of bytes written.</returns>
		/// <param name="inputBuffer">The input to compute the hash code for. </param>
		/// <param name="inputOffset">The offset into the input byte array from which to begin using data. </param>
		/// <param name="inputCount">The number of bytes in the input byte array to use as data. </param>
		/// <param name="outputBuffer">A copy of the part of the input array used to compute the hash code. </param>
		/// <param name="outputOffset">The offset into the output byte array from which to begin writing data. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="inputCount" /> uses an invalid value.-or-<paramref name="inputBuffer" /> has an invalid length.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inputBuffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="inputOffset" /> is out of range. This parameter requires a non-negative number.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x0600464C RID: 17996 RVA: 0x000F7A34 File Offset: 0x000F5C34
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (inputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("inputOffset", Environment.GetResourceString("Non-negative number required."));
			}
			if (inputCount < 0 || inputCount > inputBuffer.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Value was invalid."));
			}
			if (inputBuffer.Length - inputCount < inputOffset)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (this.m_bDisposed)
			{
				throw new ObjectDisposedException(null);
			}
			this.State = 1;
			this.HashCore(inputBuffer, inputOffset, inputCount);
			if (outputBuffer != null && (inputBuffer != outputBuffer || inputOffset != outputOffset))
			{
				Buffer.BlockCopy(inputBuffer, inputOffset, outputBuffer, outputOffset, inputCount);
			}
			return inputCount;
		}

		/// <summary>Computes the hash value for the specified region of the specified byte array.</summary>
		/// <returns>An array that is a copy of the part of the input that is hashed.</returns>
		/// <param name="inputBuffer">The input to compute the hash code for. </param>
		/// <param name="inputOffset">The offset into the byte array from which to begin using data. </param>
		/// <param name="inputCount">The number of bytes in the byte array to use as data. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="inputCount" /> uses an invalid value.-or-<paramref name="inputBuffer" /> has an invalid offset length.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inputBuffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="inputOffset" /> is out of range. This parameter requires a non-negative number.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x0600464D RID: 17997 RVA: 0x000F7AD4 File Offset: 0x000F5CD4
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (inputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("inputOffset", Environment.GetResourceString("Non-negative number required."));
			}
			if (inputCount < 0 || inputCount > inputBuffer.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Value was invalid."));
			}
			if (inputBuffer.Length - inputCount < inputOffset)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (this.m_bDisposed)
			{
				throw new ObjectDisposedException(null);
			}
			this.HashCore(inputBuffer, inputOffset, inputCount);
			this.HashValue = this.HashFinal();
			byte[] array;
			if (inputCount != 0)
			{
				array = new byte[inputCount];
				Buffer.InternalBlockCopy(inputBuffer, inputOffset, array, 0, inputCount);
			}
			else
			{
				array = EmptyArray<byte>.Value;
			}
			this.State = 0;
			return array;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Security.Cryptography.HashAlgorithm" /> class.</summary>
		// Token: 0x0600464E RID: 17998 RVA: 0x000F7B83 File Offset: 0x000F5D83
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Cryptography.HashAlgorithm" /> class.</summary>
		// Token: 0x0600464F RID: 17999 RVA: 0x000F7B92 File Offset: 0x000F5D92
		public void Clear()
		{
			((IDisposable)this).Dispose();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.HashAlgorithm" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06004650 RID: 18000 RVA: 0x000F7B9A File Offset: 0x000F5D9A
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.HashValue != null)
				{
					Array.Clear(this.HashValue, 0, this.HashValue.Length);
				}
				this.HashValue = null;
				this.m_bDisposed = true;
			}
		}

		/// <summary>Initializes an implementation of the <see cref="T:System.Security.Cryptography.HashAlgorithm" /> class.</summary>
		// Token: 0x06004651 RID: 18001
		public abstract void Initialize();

		/// <summary>When overridden in a derived class, routes data written to the object into the hash algorithm for computing the hash.</summary>
		/// <param name="array">The input to compute the hash code for. </param>
		/// <param name="ibStart">The offset into the byte array from which to begin using data. </param>
		/// <param name="cbSize">The number of bytes in the byte array to use as data. </param>
		// Token: 0x06004652 RID: 18002
		protected abstract void HashCore(byte[] array, int ibStart, int cbSize);

		/// <summary>When overridden in a derived class, finalizes the hash computation after the last data is processed by the cryptographic stream object.</summary>
		/// <returns>The computed hash code.</returns>
		// Token: 0x06004653 RID: 18003
		protected abstract byte[] HashFinal();

		/// <summary>Represents the size, in bits, of the computed hash code.</summary>
		// Token: 0x04002433 RID: 9267
		protected int HashSizeValue;

		/// <summary>Represents the value of the computed hash code.</summary>
		// Token: 0x04002434 RID: 9268
		protected internal byte[] HashValue;

		/// <summary>Represents the state of the hash computation.</summary>
		// Token: 0x04002435 RID: 9269
		protected int State;

		// Token: 0x04002436 RID: 9270
		private bool m_bDisposed;
	}
}
