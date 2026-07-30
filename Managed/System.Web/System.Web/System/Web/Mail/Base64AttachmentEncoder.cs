using System;
using System.IO;
using System.Security.Cryptography;

namespace System.Web.Mail
{
	// Token: 0x020000EF RID: 239
	internal class Base64AttachmentEncoder : IAttachmentEncoder
	{
		// Token: 0x06000CEE RID: 3310 RVA: 0x000231D8 File Offset: 0x000213D8
		public void EncodeStream(Stream ins, Stream outs)
		{
			if (ins == null || outs == null)
			{
				throw new ArgumentNullException("The input and output streams may not be null.");
			}
			ICryptoTransform cryptoTransform = new ToBase64Transform();
			byte[] array = new byte[cryptoTransform.InputBlockSize];
			byte[] array2 = new byte[cryptoTransform.OutputBlockSize];
			int num = 0;
			byte[] array3 = new byte[] { 13, 10 };
			for (;;)
			{
				int num2 = ins.Read(array, 0, array.Length);
				if (num2 < 1)
				{
					break;
				}
				if (num2 == array.Length)
				{
					cryptoTransform.TransformBlock(array, 0, array.Length, array2, 0);
					outs.Write(array2, 0, array2.Length);
					num += array2.Length;
					if (num == 60)
					{
						outs.Write(array3, 0, array3.Length);
						num = 0;
					}
				}
				else
				{
					array2 = cryptoTransform.TransformFinalBlock(array, 0, num2);
					outs.Write(array2, 0, array2.Length);
				}
			}
			outs.Write(array3, 0, array3.Length);
		}
	}
}
