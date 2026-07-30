using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008A0 RID: 2208
	public struct OSPlatform : IEquatable<OSPlatform>
	{
		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x060054BD RID: 21693 RVA: 0x00128154 File Offset: 0x00126354
		public static OSPlatform Linux { get; } = new OSPlatform("LINUX");

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x060054BE RID: 21694 RVA: 0x0012815B File Offset: 0x0012635B
		public static OSPlatform OSX { get; } = new OSPlatform("OSX");

		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x060054BF RID: 21695 RVA: 0x00128162 File Offset: 0x00126362
		public static OSPlatform Windows { get; } = new OSPlatform("WINDOWS");

		// Token: 0x060054C0 RID: 21696 RVA: 0x00128169 File Offset: 0x00126369
		private OSPlatform(string osPlatform)
		{
			if (osPlatform == null)
			{
				throw new ArgumentNullException("osPlatform");
			}
			if (osPlatform.Length == 0)
			{
				throw new ArgumentException("Value cannot be empty.", "osPlatform");
			}
			this._osPlatform = osPlatform;
		}

		// Token: 0x060054C1 RID: 21697 RVA: 0x00128198 File Offset: 0x00126398
		public static OSPlatform Create(string osPlatform)
		{
			return new OSPlatform(osPlatform);
		}

		// Token: 0x060054C2 RID: 21698 RVA: 0x001281A0 File Offset: 0x001263A0
		public bool Equals(OSPlatform other)
		{
			return this.Equals(other._osPlatform);
		}

		// Token: 0x060054C3 RID: 21699 RVA: 0x001281AE File Offset: 0x001263AE
		internal bool Equals(string other)
		{
			return string.Equals(this._osPlatform, other, StringComparison.Ordinal);
		}

		// Token: 0x060054C4 RID: 21700 RVA: 0x001281BD File Offset: 0x001263BD
		public override bool Equals(object obj)
		{
			return obj is OSPlatform && this.Equals((OSPlatform)obj);
		}

		// Token: 0x060054C5 RID: 21701 RVA: 0x001281D5 File Offset: 0x001263D5
		public override int GetHashCode()
		{
			if (this._osPlatform != null)
			{
				return this._osPlatform.GetHashCode();
			}
			return 0;
		}

		// Token: 0x060054C6 RID: 21702 RVA: 0x001281EC File Offset: 0x001263EC
		public override string ToString()
		{
			return this._osPlatform ?? string.Empty;
		}

		// Token: 0x060054C7 RID: 21703 RVA: 0x001281FD File Offset: 0x001263FD
		public static bool operator ==(OSPlatform left, OSPlatform right)
		{
			return left.Equals(right);
		}

		// Token: 0x060054C8 RID: 21704 RVA: 0x00128207 File Offset: 0x00126407
		public static bool operator !=(OSPlatform left, OSPlatform right)
		{
			return !(left == right);
		}

		// Token: 0x04002BE5 RID: 11237
		private readonly string _osPlatform;
	}
}
