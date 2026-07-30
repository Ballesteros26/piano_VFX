using System;
using System.Runtime.Serialization;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000050 RID: 80
	[Serializable]
	internal sealed class TlsException : Exception
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00012D53 File Offset: 0x00010F53
		public Alert Alert
		{
			get
			{
				return this.alert;
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00012D5B File Offset: 0x00010F5B
		internal TlsException(string message)
			: base(message)
		{
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00012D64 File Offset: 0x00010F64
		internal TlsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00012D6E File Offset: 0x00010F6E
		internal TlsException(string message, Exception ex)
			: base(message, ex)
		{
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00012D78 File Offset: 0x00010F78
		internal TlsException(AlertLevel level, AlertDescription description)
			: this(level, description, Alert.GetAlertMessage(description))
		{
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00012D88 File Offset: 0x00010F88
		internal TlsException(AlertLevel level, AlertDescription description, string message)
			: base(message)
		{
			this.alert = new Alert(level, description);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00012D9E File Offset: 0x00010F9E
		internal TlsException(AlertDescription description)
			: this(description, Alert.GetAlertMessage(description))
		{
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00012DAD File Offset: 0x00010FAD
		internal TlsException(AlertDescription description, string message)
			: base(message)
		{
			this.alert = new Alert(description);
		}

		// Token: 0x040001B7 RID: 439
		private Alert alert;
	}
}
