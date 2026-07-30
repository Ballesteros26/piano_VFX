using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Cryptography
{
	/// <summary>Converts a <see cref="T:System.Security.Cryptography.CryptoStream" /> to base 64.</summary>
	// Token: 0x02000649 RID: 1609
	[ComVisible(true)]
	public class ToBase64Transform : ICryptoTransform, IDisposable
	{
		/// <summary>Gets the input block size.</summary>
		/// <returns>The size of the input data blocks in bytes.</returns>
		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x060045B6 RID: 17846 RVA: 0x00028538 File Offset: 0x00026738
		public int InputBlockSize
		{
			get
			{
				return 3;
			}
		}

		/// <summary>Gets the output block size.</summary>
		/// <returns>The size of the output data blocks in bytes.</returns>
		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x060045B7 RID: 17847 RVA: 0x00028BDC File Offset: 0x00026DDC
		public int OutputBlockSize
		{
			get
			{
				return 4;
			}
		}

		/// <summary>Gets a value that indicates whether multiple blocks can be transformed.</summary>
		/// <returns>Always false.</returns>
		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x060045B8 RID: 17848 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the current transform can be reused.</summary>
		/// <returns>Always true.</returns>
		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x060045B9 RID: 17849 RVA: 0x00003B29 File Offset: 0x00001D29
		public virtual bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		/// <summary>Converts the specified region of the input byte array to base 64 and copies the result to the specified region of the output byte array.</summary>
		/// <returns>The number of bytes written.</returns>
		/// <param name="inputBuffer">The input to compute to base 64. </param>
		/// <param name="inputOffset">The offset into the input byte array from which to begin using data. </param>
		/// <param name="inputCount">The number of bytes in the input byte array to use as data. </param>
		/// <param name="outputBuffer">The output to which to write the result. </param>
		/// <param name="outputOffset">The offset into the output byte array from which to begin writing data. </param>
		/// <exception cref="T:System.ObjectDisposedException">The current <see cref="T:System.Security.Cryptography.ToBase64Transform" /> object has already been disposed. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The data size is not valid. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="inputBuffer" /> parameter contains an invalid offset length.-or-The <paramref name="inputCount" /> parameter contains an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputBuffer" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="inputBuffer" /> parameter requires a non-negative number.</exception>
		// Token: 0x060045BA RID: 17850 RVA: 0x000F4C78 File Offset: 0x000F2E78
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
			char[] array = new char[4];
			Convert.ToBase64CharArray(inputBuffer, inputOffset, 3, array, 0);
			byte[] bytes = Encoding.ASCII.GetBytes(array);
			if (bytes.Length != 4)
			{
				throw new CryptographicException(Environment.GetResourceString("Length of the data to encrypt is invalid."));
			}
			Buffer.BlockCopy(bytes, 0, outputBuffer, outputOffset, bytes.Length);
			return bytes.Length;
		}

		/// <summary>Converts the specified region of the specified byte array to base 64.</summary>
		/// <returns>The computed base 64 conversion.</returns>
		/// <param name="inputBuffer">The input to convert to base 64. </param>
		/// <param name="inputOffset">The offset into the byte array from which to begin using data. </param>
		/// <param name="inputCount">The number of bytes in the byte array to use as data. </param>
		/// <exception cref="T:System.ObjectDisposedException">The current <see cref="T:System.Security.Cryptography.ToBase64Transform" /> object has already been disposed. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="inputBuffer" /> parameter contains an invalid offset length.-or-The <paramref name="inputCount" /> parameter contains an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputBuffer" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="inputBuffer" /> parameter requires a non-negative number.</exception>
		// Token: 0x060045BB RID: 17851 RVA: 0x000F4D24 File Offset: 0x000F2F24
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
			if (inputCount == 0)
			{
				return EmptyArray<byte>.Value;
			}
			char[] array = new char[4];
			Convert.ToBase64CharArray(inputBuffer, inputOffset, inputCount, array, 0);
			return Encoding.ASCII.GetBytes(array);
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Security.Cryptography.ToBase64Transform" /> class.</summary>
		// Token: 0x060045BC RID: 17852 RVA: 0x000F4DB0 File Offset: 0x000F2FB0
		public void Dispose()
		{
			this.Clear();
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Cryptography.ToBase64Transform" />.</summary>
		// Token: 0x060045BD RID: 17853 RVA: 0x000F4DB8 File Offset: 0x000F2FB8
		public void Clear()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.ToBase64Transform" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060045BE RID: 17854 RVA: 0x00002194 File Offset: 0x00000394
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x060045BF RID: 17855 RVA: 0x000F4DC8 File Offset: 0x000F2FC8
		~ToBase64Transform()
		{
			this.Dispose(false);
		}
	}
}
