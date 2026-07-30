using System;

namespace System.Web.Services.Discovery
{
	// Token: 0x020000B6 RID: 182
	internal class InvalidDocumentContentsException : Exception
	{
		// Token: 0x060004BC RID: 1212 RVA: 0x00016225 File Offset: 0x00014425
		internal InvalidDocumentContentsException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
