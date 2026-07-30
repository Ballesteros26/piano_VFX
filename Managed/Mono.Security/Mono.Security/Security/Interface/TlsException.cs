using System;

namespace Mono.Security.Interface
{
	// Token: 0x02000089 RID: 137
	public sealed class TlsException : Exception
	{
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x00017406 File Offset: 0x00015606
		public Alert Alert
		{
			get
			{
				return this.alert;
			}
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00017410 File Offset: 0x00015610
		public TlsException(Alert alert)
			: this(alert, alert.Description.ToString())
		{
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00017438 File Offset: 0x00015638
		public TlsException(Alert alert, string message)
			: base(message)
		{
			this.alert = alert;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00017448 File Offset: 0x00015648
		public TlsException(AlertLevel level, AlertDescription description)
			: this(new Alert(level, description))
		{
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00017457 File Offset: 0x00015657
		public TlsException(AlertDescription description)
			: this(new Alert(description))
		{
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00017465 File Offset: 0x00015665
		public TlsException(AlertDescription description, string message)
			: this(new Alert(description), message)
		{
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00017474 File Offset: 0x00015674
		public TlsException(AlertDescription description, string format, params object[] args)
			: this(new Alert(description), string.Format(format, args))
		{
		}

		// Token: 0x04000383 RID: 899
		private Alert alert;
	}
}
