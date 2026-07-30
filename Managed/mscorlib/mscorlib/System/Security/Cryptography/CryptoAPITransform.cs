using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	/// <summary>Performs a cryptographic transformation of data. This class cannot be inherited.</summary>
	// Token: 0x02000697 RID: 1687
	[ComVisible(true)]
	public sealed class CryptoAPITransform : ICryptoTransform, IDisposable
	{
		// Token: 0x06004833 RID: 18483 RVA: 0x00101C5F File Offset: 0x000FFE5F
		internal CryptoAPITransform()
		{
			this.m_disposed = false;
		}

		/// <summary>Gets a value indicating whether the current transform can be reused.</summary>
		/// <returns>Always true.</returns>
		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x06004834 RID: 18484 RVA: 0x00003B29 File Offset: 0x00001D29
		public bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether multiple blocks can be transformed.</summary>
		/// <returns>true if multiple blocks can be transformed; otherwise, false.</returns>
		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x06004835 RID: 18485 RVA: 0x00003B29 File Offset: 0x00001D29
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the input block size.</summary>
		/// <returns>The input block size in bytes.</returns>
		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x06004836 RID: 18486 RVA: 0x00015ED5 File Offset: 0x000140D5
		public int InputBlockSize
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets the key handle.</summary>
		/// <returns>The key handle.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06004837 RID: 18487 RVA: 0x00101C6E File Offset: 0x000FFE6E
		public IntPtr KeyHandle
		{
			[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
			get
			{
				return IntPtr.Zero;
			}
		}

		/// <summary>Gets the output block size.</summary>
		/// <returns>The output block size in bytes.</returns>
		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06004838 RID: 18488 RVA: 0x00015ED5 File Offset: 0x000140D5
		public int OutputBlockSize
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Security.Cryptography.CryptoAPITransform" /> class.</summary>
		// Token: 0x06004839 RID: 18489 RVA: 0x00101C75 File Offset: 0x000FFE75
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Cryptography.CryptoAPITransform" /> method.</summary>
		// Token: 0x0600483A RID: 18490 RVA: 0x00101C84 File Offset: 0x000FFE84
		public void Clear()
		{
			this.Dispose(false);
		}

		// Token: 0x0600483B RID: 18491 RVA: 0x00101C8D File Offset: 0x000FFE8D
		private void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				this.m_disposed = true;
			}
		}

		/// <summary>Computes the transformation for the specified region of the input byte array and copies the resulting transformation to the specified region of the output byte array.</summary>
		/// <returns>The number of bytes written.</returns>
		/// <param name="inputBuffer">The input on which to perform the operation on. </param>
		/// <param name="inputOffset">The offset into the input byte array from which to begin using data from. </param>
		/// <param name="inputCount">The number of bytes in the input byte array to use as data. </param>
		/// <param name="outputBuffer">The output to which to write the data to. </param>
		/// <param name="outputOffset">The offset into the output byte array from which to begin writing data from. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputBuffer" /> parameter is null.-or- The <paramref name="outputBuffer" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The length of the input buffer is less than the sum of the input offset and the input count. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="inputOffset" /> is out of range. This parameter requires a non-negative number.</exception>
		// Token: 0x0600483C RID: 18492 RVA: 0x00015ED5 File Offset: 0x000140D5
		[SecuritySafeCritical]
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			return 0;
		}

		/// <summary>Computes the transformation for the specified region of the specified byte array.</summary>
		/// <returns>The computed transformation.</returns>
		/// <param name="inputBuffer">The input on which to perform the operation on. </param>
		/// <param name="inputOffset">The offset into the byte array from which to begin using data from. </param>
		/// <param name="inputCount">The number of bytes in the byte array to use as data. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputBuffer" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="inputOffset" /> parameter is less than zero.-or- The <paramref name="inputCount" /> parameter is less than zero.-or- The length of the input buffer is less than the sum of the input offset and the input count. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="F:System.Security.Cryptography.PaddingMode.PKCS7" /> padding is invalid. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="inputOffset" /> parameter is out of range. This parameter requires a non-negative number.</exception>
		// Token: 0x0600483D RID: 18493 RVA: 0x0000A42E File Offset: 0x0000862E
		[SecuritySafeCritical]
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			return null;
		}

		/// <summary>Resets the internal state of <see cref="T:System.Security.Cryptography.CryptoAPITransform" /> so that it can be used again to do a different encryption or decryption.</summary>
		// Token: 0x0600483E RID: 18494 RVA: 0x00002194 File Offset: 0x00000394
		[ComVisible(false)]
		public void Reset()
		{
		}

		// Token: 0x0400251E RID: 9502
		private bool m_disposed;
	}
}
