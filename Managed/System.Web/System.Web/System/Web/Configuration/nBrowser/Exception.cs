using System;
using System.Runtime.Serialization;

namespace System.Web.Configuration.nBrowser
{
	// Token: 0x020005FA RID: 1530
	internal class Exception : Exception
	{
		// Token: 0x0600425F RID: 16991 RVA: 0x000ADD15 File Offset: 0x000ABF15
		public Exception()
		{
		}

		// Token: 0x06004260 RID: 16992 RVA: 0x000ADD1D File Offset: 0x000ABF1D
		public Exception(string errorMessage)
			: base(errorMessage)
		{
		}

		// Token: 0x06004261 RID: 16993 RVA: 0x000ADD26 File Offset: 0x000ABF26
		public Exception(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06004262 RID: 16994 RVA: 0x00025DB5 File Offset: 0x00023FB5
		protected Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
