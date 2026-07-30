using System;
using System.Text;

namespace System.Web.Mail
{
	// Token: 0x020000F1 RID: 241
	internal class MailAddress
	{
		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x0002329E File Offset: 0x0002149E
		// (set) Token: 0x06000CF2 RID: 3314 RVA: 0x000232A6 File Offset: 0x000214A6
		public string User
		{
			get
			{
				return this.user;
			}
			set
			{
				this.user = value;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x000232AF File Offset: 0x000214AF
		// (set) Token: 0x06000CF4 RID: 3316 RVA: 0x000232B7 File Offset: 0x000214B7
		public string Host
		{
			get
			{
				return this.host;
			}
			set
			{
				this.host = value;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x000232C0 File Offset: 0x000214C0
		// (set) Token: 0x06000CF6 RID: 3318 RVA: 0x000232C8 File Offset: 0x000214C8
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x000232D1 File Offset: 0x000214D1
		// (set) Token: 0x06000CF8 RID: 3320 RVA: 0x000232EC File Offset: 0x000214EC
		public string Address
		{
			get
			{
				return this.user + "@" + this.host;
			}
			set
			{
				string[] array = value.Split(new char[] { '@' });
				if (array.Length != 2)
				{
					throw new FormatException("Invalid e-mail address: '" + value + "'.");
				}
				this.user = array[0];
				this.host = array[1];
			}
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0002333C File Offset: 0x0002153C
		public static MailAddress Parse(string str)
		{
			if (str == null || str.Trim() == "")
			{
				return null;
			}
			MailAddress mailAddress = new MailAddress();
			string text = null;
			string text2 = null;
			foreach (string text3 in str.Split(new char[] { ' ', '<' }))
			{
				if (text3.IndexOf('@') > 0)
				{
					text = text3;
					break;
				}
				text2 = text2 + text3 + " ";
			}
			if (text == null)
			{
				throw new FormatException("Invalid e-mail address: '" + str + "'.");
			}
			text = text.Trim(new char[] { '<', '>', '(', ')' });
			mailAddress.Address = text;
			if (text2 != null)
			{
				mailAddress.Name = text2.Trim(new char[] { ' ', '"' });
				mailAddress.Name = ((mailAddress.Name.Length == 0) ? null : mailAddress.Name);
			}
			return mailAddress;
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0002342C File Offset: 0x0002162C
		public override string ToString()
		{
			string text;
			if (this.name == null)
			{
				text = "<" + this.Address + ">";
			}
			else
			{
				string text2 = this.Name;
				if (MailUtil.NeedEncoding(text2))
				{
					text2 = string.Concat(new string[]
					{
						"=?",
						Encoding.Default.BodyName,
						"?B?",
						MailUtil.Base64Encode(text2),
						"?="
					});
				}
				text = string.Concat(new string[] { "\"", text2, "\" <", this.Address, ">" });
			}
			return text;
		}

		// Token: 0x04001123 RID: 4387
		protected string user;

		// Token: 0x04001124 RID: 4388
		protected string host;

		// Token: 0x04001125 RID: 4389
		protected string name;
	}
}
