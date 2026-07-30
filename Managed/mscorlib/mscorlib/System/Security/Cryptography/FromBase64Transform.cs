using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Cryptography
{
	/// <summary>Converts a <see cref="T:System.Security.Cryptography.CryptoStream" /> from base 64.</summary>
	// Token: 0x0200064A RID: 1610
	[ComVisible(true)]
	public class FromBase64Transform : ICryptoTransform, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.FromBase64Transform" /> class.</summary>
		// Token: 0x060045C1 RID: 17857 RVA: 0x000F4DF8 File Offset: 0x000F2FF8
		public FromBase64Transform()
			: this(FromBase64TransformMode.IgnoreWhiteSpaces)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.FromBase64Transform" /> class with the specified transformation mode.</summary>
		/// <param name="whitespaces">One of the <see cref="T:System.Security.Cryptography.FromBase64Transform" /> values. </param>
		// Token: 0x060045C2 RID: 17858 RVA: 0x000F4E01 File Offset: 0x000F3001
		public FromBase64Transform(FromBase64TransformMode whitespaces)
		{
			this._whitespaces = whitespaces;
			this._inputIndex = 0;
		}

		/// <summary>Gets the input block size.</summary>
		/// <returns>The size of the input data blocks in bytes.</returns>
		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x060045C3 RID: 17859 RVA: 0x00003B29 File Offset: 0x00001D29
		public int InputBlockSize
		{
			get
			{
				return 1;
			}
		}

		/// <summary>Gets the output block size.</summary>
		/// <returns>The size of the output data blocks in bytes.</returns>
		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x060045C4 RID: 17860 RVA: 0x00028538 File Offset: 0x00026738
		public int OutputBlockSize
		{
			get
			{
				return 3;
			}
		}

		/// <summary>Gets a value that indicates whether multiple blocks can be transformed.</summary>
		/// <returns>Always false.</returns>
		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x060045C5 RID: 17861 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the current transform can be reused.</summary>
		/// <returns>Always true.</returns>
		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x060045C6 RID: 17862 RVA: 0x00003B29 File Offset: 0x00001D29
		public virtual bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		/// <summary>Converts the specified region of the input byte array from base 64 and copies the result to the specified region of the output byte array.</summary>
		/// <returns>The number of bytes written.</returns>
		/// <param name="inputBuffer">The input to compute from base 64. </param>
		/// <param name="inputOffset">The offset into the input byte array from which to begin using data. </param>
		/// <param name="inputCount">The number of bytes in the input byte array to use as data. </param>
		/// <param name="outputBuffer">The output to which to write the result. </param>
		/// <param name="outputOffset">The offset into the output byte array from which to begin writing data. </param>
		/// <exception cref="T:System.ObjectDisposedException">The current <see cref="T:System.Security.Cryptography.FromBase64Transform" /> object has already been disposed. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="inputCount" /> uses an invalid value.-or-<paramref name="inputBuffer" /> has an invalid offset length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="inputOffset" /> is out of range. This parameter requires a non-negative number.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inputBuffer" /> is null.</exception>
		// Token: 0x060045C7 RID: 17863 RVA: 0x000F4E24 File Offset: 0x000F3024
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
			if (this._inputBuffer == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a disposed object."));
			}
			byte[] array = new byte[inputCount];
			int num;
			if (this._whitespaces == FromBase64TransformMode.IgnoreWhiteSpaces)
			{
				array = this.DiscardWhiteSpaces(inputBuffer, inputOffset, inputCount);
				num = array.Length;
			}
			else
			{
				Buffer.InternalBlockCopy(inputBuffer, inputOffset, array, 0, inputCount);
				num = inputCount;
			}
			if (num + this._inputIndex < 4)
			{
				Buffer.InternalBlockCopy(array, 0, this._inputBuffer, this._inputIndex, num);
				this._inputIndex += num;
				return 0;
			}
			int num2 = (num + this._inputIndex) / 4;
			byte[] array2 = new byte[this._inputIndex + num];
			Buffer.InternalBlockCopy(this._inputBuffer, 0, array2, 0, this._inputIndex);
			Buffer.InternalBlockCopy(array, 0, array2, this._inputIndex, num);
			this._inputIndex = (num + this._inputIndex) % 4;
			Buffer.InternalBlockCopy(array, num - this._inputIndex, this._inputBuffer, 0, this._inputIndex);
			byte[] array3 = Convert.FromBase64CharArray(Encoding.ASCII.GetChars(array2, 0, 4 * num2), 0, 4 * num2);
			Buffer.BlockCopy(array3, 0, outputBuffer, outputOffset, array3.Length);
			return array3.Length;
		}

		/// <summary>Converts the specified region of the specified byte array from base 64.</summary>
		/// <returns>The computed conversion.</returns>
		/// <param name="inputBuffer">The input to convert from base 64. </param>
		/// <param name="inputOffset">The offset into the byte array from which to begin using data. </param>
		/// <param name="inputCount">The number of bytes in the byte array to use as data. </param>
		/// <exception cref="T:System.ObjectDisposedException">The current <see cref="T:System.Security.Cryptography.FromBase64Transform" /> object has already been disposed. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="inputBuffer" /> has an invalid offset length.-or-<paramref name="inputCount" /> has an invalid value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="inputOffset" /> is out of range. This parameter requires a non-negative number.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inputBuffer" /> is null.</exception>
		// Token: 0x060045C8 RID: 17864 RVA: 0x000F4F98 File Offset: 0x000F3198
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
			if (this._inputBuffer == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a disposed object."));
			}
			byte[] array = new byte[inputCount];
			int num;
			if (this._whitespaces == FromBase64TransformMode.IgnoreWhiteSpaces)
			{
				array = this.DiscardWhiteSpaces(inputBuffer, inputOffset, inputCount);
				num = array.Length;
			}
			else
			{
				Buffer.InternalBlockCopy(inputBuffer, inputOffset, array, 0, inputCount);
				num = inputCount;
			}
			if (num + this._inputIndex < 4)
			{
				this.Reset();
				return EmptyArray<byte>.Value;
			}
			int num2 = (num + this._inputIndex) / 4;
			byte[] array2 = new byte[this._inputIndex + num];
			Buffer.InternalBlockCopy(this._inputBuffer, 0, array2, 0, this._inputIndex);
			Buffer.InternalBlockCopy(array, 0, array2, this._inputIndex, num);
			this._inputIndex = (num + this._inputIndex) % 4;
			Buffer.InternalBlockCopy(array, num - this._inputIndex, this._inputBuffer, 0, this._inputIndex);
			byte[] array3 = Convert.FromBase64CharArray(Encoding.ASCII.GetChars(array2, 0, 4 * num2), 0, 4 * num2);
			this.Reset();
			return array3;
		}

		// Token: 0x060045C9 RID: 17865 RVA: 0x000F50E4 File Offset: 0x000F32E4
		private byte[] DiscardWhiteSpaces(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			int num = 0;
			for (int i = 0; i < inputCount; i++)
			{
				if (char.IsWhiteSpace((char)inputBuffer[inputOffset + i]))
				{
					num++;
				}
			}
			byte[] array = new byte[inputCount - num];
			num = 0;
			for (int i = 0; i < inputCount; i++)
			{
				if (!char.IsWhiteSpace((char)inputBuffer[inputOffset + i]))
				{
					array[num++] = inputBuffer[inputOffset + i];
				}
			}
			return array;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Security.Cryptography.FromBase64Transform" /> class.</summary>
		// Token: 0x060045CA RID: 17866 RVA: 0x000F513F File Offset: 0x000F333F
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060045CB RID: 17867 RVA: 0x000F514E File Offset: 0x000F334E
		private void Reset()
		{
			this._inputIndex = 0;
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Cryptography.FromBase64Transform" />.</summary>
		// Token: 0x060045CC RID: 17868 RVA: 0x000F5157 File Offset: 0x000F3357
		public void Clear()
		{
			this.Dispose();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.FromBase64Transform" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060045CD RID: 17869 RVA: 0x000F515F File Offset: 0x000F335F
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._inputBuffer != null)
				{
					Array.Clear(this._inputBuffer, 0, this._inputBuffer.Length);
				}
				this._inputBuffer = null;
				this._inputIndex = 0;
			}
		}

		// Token: 0x060045CE RID: 17870 RVA: 0x000F5190 File Offset: 0x000F3390
		~FromBase64Transform()
		{
			this.Dispose(false);
		}

		// Token: 0x040023D3 RID: 9171
		private byte[] _inputBuffer = new byte[4];

		// Token: 0x040023D4 RID: 9172
		private int _inputIndex;

		// Token: 0x040023D5 RID: 9173
		private FromBase64TransformMode _whitespaces;
	}
}
