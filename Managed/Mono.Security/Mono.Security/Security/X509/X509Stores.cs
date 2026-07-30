using System;
using System.IO;

namespace Mono.Security.X509
{
	// Token: 0x0200001C RID: 28
	public class X509Stores
	{
		// Token: 0x06000177 RID: 375 RVA: 0x0000B42F File Offset: 0x0000962F
		internal X509Stores(string path, bool newFormat)
		{
			this._storePath = path;
			this._newFormat = newFormat;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000B448 File Offset: 0x00009648
		public X509Store Personal
		{
			get
			{
				if (this._personal == null)
				{
					string text = Path.Combine(this._storePath, "My");
					this._personal = new X509Store(text, false, false);
				}
				return this._personal;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000B484 File Offset: 0x00009684
		public X509Store OtherPeople
		{
			get
			{
				if (this._other == null)
				{
					string text = Path.Combine(this._storePath, "AddressBook");
					this._other = new X509Store(text, false, false);
				}
				return this._other;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000B4C0 File Offset: 0x000096C0
		public X509Store IntermediateCA
		{
			get
			{
				if (this._intermediate == null)
				{
					string text = Path.Combine(this._storePath, "CA");
					this._intermediate = new X509Store(text, true, this._newFormat);
				}
				return this._intermediate;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000B500 File Offset: 0x00009700
		public X509Store TrustedRoot
		{
			get
			{
				if (this._trusted == null)
				{
					string text = Path.Combine(this._storePath, "Trust");
					this._trusted = new X509Store(text, true, this._newFormat);
				}
				return this._trusted;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600017C RID: 380 RVA: 0x0000B540 File Offset: 0x00009740
		public X509Store Untrusted
		{
			get
			{
				if (this._untrusted == null)
				{
					string text = Path.Combine(this._storePath, "Disallowed");
					this._untrusted = new X509Store(text, false, this._newFormat);
				}
				return this._untrusted;
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000B580 File Offset: 0x00009780
		public void Clear()
		{
			if (this._personal != null)
			{
				this._personal.Clear();
			}
			this._personal = null;
			if (this._other != null)
			{
				this._other.Clear();
			}
			this._other = null;
			if (this._intermediate != null)
			{
				this._intermediate.Clear();
			}
			this._intermediate = null;
			if (this._trusted != null)
			{
				this._trusted.Clear();
			}
			this._trusted = null;
			if (this._untrusted != null)
			{
				this._untrusted.Clear();
			}
			this._untrusted = null;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000B610 File Offset: 0x00009810
		public X509Store Open(string storeName, bool create)
		{
			if (storeName == null)
			{
				throw new ArgumentNullException("storeName");
			}
			string text = Path.Combine(this._storePath, storeName);
			if (!create && !Directory.Exists(text))
			{
				return null;
			}
			return new X509Store(text, true, false);
		}

		// Token: 0x040000CE RID: 206
		private string _storePath;

		// Token: 0x040000CF RID: 207
		private bool _newFormat;

		// Token: 0x040000D0 RID: 208
		private X509Store _personal;

		// Token: 0x040000D1 RID: 209
		private X509Store _other;

		// Token: 0x040000D2 RID: 210
		private X509Store _intermediate;

		// Token: 0x040000D3 RID: 211
		private X509Store _trusted;

		// Token: 0x040000D4 RID: 212
		private X509Store _untrusted;

		// Token: 0x020000C7 RID: 199
		public class Names
		{
			// Token: 0x040004C9 RID: 1225
			public const string Personal = "My";

			// Token: 0x040004CA RID: 1226
			public const string OtherPeople = "AddressBook";

			// Token: 0x040004CB RID: 1227
			public const string IntermediateCA = "CA";

			// Token: 0x040004CC RID: 1228
			public const string TrustedRoot = "Trust";

			// Token: 0x040004CD RID: 1229
			public const string Untrusted = "Disallowed";
		}
	}
}
