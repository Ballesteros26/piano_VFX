using System;
using System.IO;

namespace System.Web.Mail
{
	// Token: 0x020000F0 RID: 240
	internal interface IAttachmentEncoder
	{
		// Token: 0x06000CF0 RID: 3312
		void EncodeStream(Stream ins, Stream outs);
	}
}
