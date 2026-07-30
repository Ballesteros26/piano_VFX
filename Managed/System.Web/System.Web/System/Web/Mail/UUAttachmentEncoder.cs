using System;
using System.IO;
using System.Text;

namespace System.Web.Mail
{
	// Token: 0x02000102 RID: 258
	internal class UUAttachmentEncoder : IAttachmentEncoder
	{
		// Token: 0x06000D92 RID: 3474 RVA: 0x00024E90 File Offset: 0x00023090
		public UUAttachmentEncoder(int mode, string fileName)
		{
			string text = "\r\n";
			this.beginTag = Encoding.ASCII.GetBytes(string.Concat(new object[] { "begin ", mode, " ", fileName, text }));
			this.endTag = Encoding.ASCII.GetBytes("`" + text + "end" + text);
			this.endl = Encoding.ASCII.GetBytes(text);
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00024F18 File Offset: 0x00023118
		public void EncodeStream(Stream ins, Stream outs)
		{
			outs.Write(this.beginTag, 0, this.beginTag.Length);
			ToUUEncodingTransform toUUEncodingTransform = new ToUUEncodingTransform();
			byte[] array = new byte[toUUEncodingTransform.InputBlockSize];
			byte[] array2 = new byte[toUUEncodingTransform.OutputBlockSize];
			int num;
			for (;;)
			{
				num = ins.Read(array, 0, array.Length);
				if (num < 1)
				{
					goto IL_00A7;
				}
				if (num != toUUEncodingTransform.InputBlockSize)
				{
					break;
				}
				toUUEncodingTransform.TransformBlock(array, 0, num, array2, 0);
				outs.Write(array2, 0, array2.Length);
				outs.Write(this.endl, 0, this.endl.Length);
			}
			byte[] array3 = toUUEncodingTransform.TransformFinalBlock(array, 0, num);
			outs.Write(array3, 0, array3.Length);
			outs.Write(this.endl, 0, this.endl.Length);
			IL_00A7:
			outs.Write(this.endTag, 0, this.endTag.Length);
		}

		// Token: 0x0400115B RID: 4443
		protected byte[] beginTag;

		// Token: 0x0400115C RID: 4444
		protected byte[] endTag;

		// Token: 0x0400115D RID: 4445
		protected byte[] endl;
	}
}
