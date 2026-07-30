using System;
using System.Runtime.Serialization;

namespace System.Web.Util
{
	// Token: 0x0200010C RID: 268
	internal sealed class AppVerifierException : Exception
	{
		// Token: 0x06000DC2 RID: 3522 RVA: 0x00025DA5 File Offset: 0x00023FA5
		public AppVerifierException(AppVerifierErrorCode errorCode, string message)
			: base(message)
		{
			this._errorCode = errorCode;
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x00025DB5 File Offset: 0x00023FB5
		private AppVerifierException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x00025DBF File Offset: 0x00023FBF
		public AppVerifierErrorCode ErrorCode
		{
			get
			{
				return this._errorCode;
			}
		}

		// Token: 0x0400118D RID: 4493
		private readonly AppVerifierErrorCode _errorCode;
	}
}
