using System;
using System.IO;

namespace Mono.Security.X509
{
	// Token: 0x02000065 RID: 101
	internal class X509Stores
	{
		// Token: 0x0600037F RID: 895 RVA: 0x0001521B File Offset: 0x0001341B
		internal X509Stores(string path, bool newFormat)
		{
			this._storePath = path;
			this._newFormat = newFormat;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00015234 File Offset: 0x00013434
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

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000381 RID: 897 RVA: 0x00015270 File Offset: 0x00013470
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

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000382 RID: 898 RVA: 0x000152AC File Offset: 0x000134AC
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

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000383 RID: 899 RVA: 0x000152EC File Offset: 0x000134EC
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

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0001532C File Offset: 0x0001352C
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

		// Token: 0x06000385 RID: 901 RVA: 0x0001536C File Offset: 0x0001356C
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

		// Token: 0x06000386 RID: 902 RVA: 0x000153FC File Offset: 0x000135FC
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

		// Token: 0x0400052B RID: 1323
		private string _storePath;

		// Token: 0x0400052C RID: 1324
		private bool _newFormat;

		// Token: 0x0400052D RID: 1325
		private X509Store _personal;

		// Token: 0x0400052E RID: 1326
		private X509Store _other;

		// Token: 0x0400052F RID: 1327
		private X509Store _intermediate;

		// Token: 0x04000530 RID: 1328
		private X509Store _trusted;

		// Token: 0x04000531 RID: 1329
		private X509Store _untrusted;

		// Token: 0x02000066 RID: 102
		public class Names
		{
			// Token: 0x04000532 RID: 1330
			public const string Personal = "My";

			// Token: 0x04000533 RID: 1331
			public const string OtherPeople = "AddressBook";

			// Token: 0x04000534 RID: 1332
			public const string IntermediateCA = "CA";

			// Token: 0x04000535 RID: 1333
			public const string TrustedRoot = "Trust";

			// Token: 0x04000536 RID: 1334
			public const string Untrusted = "Disallowed";
		}
	}
}
