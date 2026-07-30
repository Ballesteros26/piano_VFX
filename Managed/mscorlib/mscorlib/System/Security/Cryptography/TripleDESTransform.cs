using System;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	// Token: 0x020006A6 RID: 1702
	internal class TripleDESTransform : SymmetricTransform
	{
		// Token: 0x060048C3 RID: 18627 RVA: 0x00106570 File Offset: 0x00104770
		public TripleDESTransform(TripleDES algo, bool encryption, byte[] key, byte[] iv)
			: base(algo, encryption, iv)
		{
			if (key == null)
			{
				key = TripleDESTransform.GetStrongKey();
			}
			if (TripleDES.IsWeakKey(key))
			{
				throw new CryptographicException(Locale.GetText("This is a known weak key."));
			}
			byte[] array = new byte[8];
			byte[] array2 = new byte[8];
			byte[] array3 = new byte[8];
			DES des = DES.Create();
			Buffer.BlockCopy(key, 0, array, 0, 8);
			Buffer.BlockCopy(key, 8, array2, 0, 8);
			if (key.Length == 16)
			{
				Buffer.BlockCopy(key, 0, array3, 0, 8);
			}
			else
			{
				Buffer.BlockCopy(key, 16, array3, 0, 8);
			}
			if (encryption || algo.Mode == CipherMode.CFB)
			{
				this.E1 = new DESTransform(des, true, array, iv);
				this.D2 = new DESTransform(des, false, array2, iv);
				this.E3 = new DESTransform(des, true, array3, iv);
				return;
			}
			this.D1 = new DESTransform(des, false, array3, iv);
			this.E2 = new DESTransform(des, true, array2, iv);
			this.D3 = new DESTransform(des, false, array, iv);
		}

		// Token: 0x060048C4 RID: 18628 RVA: 0x00106664 File Offset: 0x00104864
		protected override void ECB(byte[] input, byte[] output)
		{
			DESTransform.Permutation(input, output, DESTransform.ipTab, false);
			if (this.encrypt)
			{
				this.E1.ProcessBlock(output, output);
				this.D2.ProcessBlock(output, output);
				this.E3.ProcessBlock(output, output);
			}
			else
			{
				this.D1.ProcessBlock(output, output);
				this.E2.ProcessBlock(output, output);
				this.D3.ProcessBlock(output, output);
			}
			DESTransform.Permutation(output, output, DESTransform.fpTab, true);
		}

		// Token: 0x060048C5 RID: 18629 RVA: 0x001066E4 File Offset: 0x001048E4
		internal static byte[] GetStrongKey()
		{
			int num = DESTransform.BLOCK_BYTE_SIZE * 3;
			byte[] array = KeyBuilder.Key(num);
			while (TripleDES.IsWeakKey(array))
			{
				array = KeyBuilder.Key(num);
			}
			return array;
		}

		// Token: 0x0400260F RID: 9743
		private DESTransform E1;

		// Token: 0x04002610 RID: 9744
		private DESTransform D2;

		// Token: 0x04002611 RID: 9745
		private DESTransform E3;

		// Token: 0x04002612 RID: 9746
		private DESTransform D1;

		// Token: 0x04002613 RID: 9747
		private DESTransform E2;

		// Token: 0x04002614 RID: 9748
		private DESTransform D3;
	}
}
