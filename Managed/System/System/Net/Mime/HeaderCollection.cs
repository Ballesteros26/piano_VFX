using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Net.Mail;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x0200059C RID: 1436
	internal class HeaderCollection : NameValueCollection
	{
		// Token: 0x06002CC6 RID: 11462 RVA: 0x000B087E File Offset: 0x000AEA7E
		internal HeaderCollection()
			: base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x06002CC7 RID: 11463 RVA: 0x000B088C File Offset: 0x000AEA8C
		public override void Remove(string name)
		{
			bool on = Logging.On;
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException(global::SR.GetString("The parameter '{0}' cannot be an empty string.", new object[] { "name" }), "name");
			}
			MailHeaderID id = MailHeaderInfo.GetID(name);
			if (id == MailHeaderID.ContentType && this.part != null)
			{
				this.part.ContentType = null;
			}
			else if (id == MailHeaderID.ContentDisposition && this.part is MimePart)
			{
				((MimePart)this.part).ContentDisposition = null;
			}
			base.Remove(name);
		}

		// Token: 0x06002CC8 RID: 11464 RVA: 0x000B0928 File Offset: 0x000AEB28
		public override string Get(string name)
		{
			bool on = Logging.On;
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException(global::SR.GetString("The parameter '{0}' cannot be an empty string.", new object[] { "name" }), "name");
			}
			MailHeaderID id = MailHeaderInfo.GetID(name);
			if (id == MailHeaderID.ContentType && this.part != null)
			{
				this.part.ContentType.PersistIfNeeded(this, false);
			}
			else if (id == MailHeaderID.ContentDisposition && this.part is MimePart)
			{
				((MimePart)this.part).ContentDisposition.PersistIfNeeded(this, false);
			}
			return base.Get(name);
		}

		// Token: 0x06002CC9 RID: 11465 RVA: 0x000B09D0 File Offset: 0x000AEBD0
		public override string[] GetValues(string name)
		{
			bool on = Logging.On;
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException(global::SR.GetString("The parameter '{0}' cannot be an empty string.", new object[] { "name" }), "name");
			}
			MailHeaderID id = MailHeaderInfo.GetID(name);
			if (id == MailHeaderID.ContentType && this.part != null)
			{
				this.part.ContentType.PersistIfNeeded(this, false);
			}
			else if (id == MailHeaderID.ContentDisposition && this.part is MimePart)
			{
				((MimePart)this.part).ContentDisposition.PersistIfNeeded(this, false);
			}
			return base.GetValues(name);
		}

		// Token: 0x06002CCA RID: 11466 RVA: 0x000B0A77 File Offset: 0x000AEC77
		internal void InternalRemove(string name)
		{
			base.Remove(name);
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x000B0A80 File Offset: 0x000AEC80
		internal void InternalSet(string name, string value)
		{
			base.Set(name, value);
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x000B0A8A File Offset: 0x000AEC8A
		internal void InternalAdd(string name, string value)
		{
			if (MailHeaderInfo.IsSingleton(name))
			{
				base.Set(name, value);
				return;
			}
			base.Add(name, value);
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x000B0AA8 File Offset: 0x000AECA8
		public override void Set(string name, string value)
		{
			bool on = Logging.On;
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException(global::SR.GetString("The parameter '{0}' cannot be an empty string.", new object[] { "name" }), "name");
			}
			if (value == string.Empty)
			{
				throw new ArgumentException(global::SR.GetString("The parameter '{0}' cannot be an empty string.", new object[] { "value" }), "name");
			}
			if (!MimeBasePart.IsAscii(name, false))
			{
				throw new FormatException(global::SR.GetString("An invalid character was found in header name."));
			}
			name = MailHeaderInfo.NormalizeCase(name);
			MailHeaderID id = MailHeaderInfo.GetID(name);
			value = value.Normalize(NormalizationForm.FormC);
			if (id == MailHeaderID.ContentType && this.part != null)
			{
				this.part.ContentType.Set(value.ToLower(CultureInfo.InvariantCulture), this);
				return;
			}
			if (id == MailHeaderID.ContentDisposition && this.part is MimePart)
			{
				((MimePart)this.part).ContentDisposition.Set(value.ToLower(CultureInfo.InvariantCulture), this);
				return;
			}
			base.Set(name, value);
		}

		// Token: 0x06002CCE RID: 11470 RVA: 0x000B0BCC File Offset: 0x000AEDCC
		public override void Add(string name, string value)
		{
			bool on = Logging.On;
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException(global::SR.GetString("The parameter '{0}' cannot be an empty string.", new object[] { "name" }), "name");
			}
			if (value == string.Empty)
			{
				throw new ArgumentException(global::SR.GetString("The parameter '{0}' cannot be an empty string.", new object[] { "value" }), "name");
			}
			MailBnfHelper.ValidateHeaderName(name);
			name = MailHeaderInfo.NormalizeCase(name);
			MailHeaderID id = MailHeaderInfo.GetID(name);
			value = value.Normalize(NormalizationForm.FormC);
			if (id == MailHeaderID.ContentType && this.part != null)
			{
				this.part.ContentType.Set(value.ToLower(CultureInfo.InvariantCulture), this);
				return;
			}
			if (id == MailHeaderID.ContentDisposition && this.part is MimePart)
			{
				((MimePart)this.part).ContentDisposition.Set(value.ToLower(CultureInfo.InvariantCulture), this);
				return;
			}
			this.InternalAdd(name, value);
		}

		// Token: 0x04002506 RID: 9478
		private MimeBasePart part;
	}
}
