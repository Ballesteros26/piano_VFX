using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Defines the basic operations of cryptographic transformations.</summary>
	// Token: 0x02000669 RID: 1641
	[ComVisible(true)]
	public interface ICryptoTransform : IDisposable
	{
		/// <summary>Gets the input block size.</summary>
		/// <returns>The size of the input data blocks in bytes.</returns>
		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06004689 RID: 18057
		int InputBlockSize { get; }

		/// <summary>Gets the output block size.</summary>
		/// <returns>The size of the output data blocks in bytes.</returns>
		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x0600468A RID: 18058
		int OutputBlockSize { get; }

		/// <summary>Gets a value indicating whether multiple blocks can be transformed.</summary>
		/// <returns>true if multiple blocks can be transformed; otherwise, false.</returns>
		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x0600468B RID: 18059
		bool CanTransformMultipleBlocks { get; }

		/// <summary>Gets a value indicating whether the current transform can be reused.</summary>
		/// <returns>true if the current transform can be reused; otherwise, false.</returns>
		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x0600468C RID: 18060
		bool CanReuseTransform { get; }

		/// <summary>Transforms the specified region of the input byte array and copies the resulting transform to the specified region of the output byte array.</summary>
		/// <returns>The number of bytes written.</returns>
		/// <param name="inputBuffer">The input for which to compute the transform. </param>
		/// <param name="inputOffset">The offset into the input byte array from which to begin using data. </param>
		/// <param name="inputCount">The number of bytes in the input byte array to use as data. </param>
		/// <param name="outputBuffer">The output to which to write the transform. </param>
		/// <param name="outputOffset">The offset into the output byte array from which to begin writing data. </param>
		// Token: 0x0600468D RID: 18061
		int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset);

		/// <summary>Transforms the specified region of the specified byte array.</summary>
		/// <returns>The computed transform.</returns>
		/// <param name="inputBuffer">The input for which to compute the transform. </param>
		/// <param name="inputOffset">The offset into the byte array from which to begin using data. </param>
		/// <param name="inputCount">The number of bytes in the byte array to use as data. </param>
		// Token: 0x0600468E RID: 18062
		byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount);
	}
}
