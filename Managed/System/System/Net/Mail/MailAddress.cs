using System;
using System.Text;

namespace System.Net.Mail
{
	/// <summary>Represents the address of an electronic mail sender or recipient.</summary>
	// Token: 0x0200057E RID: 1406
	public class MailAddress
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailAddress" /> class using the specified address. </summary>
		/// <param name="address">A <see cref="T:System.String" /> that contains an e-mail address.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="address" /> is not in a recognized format.</exception>
		// Token: 0x06002BA4 RID: 11172 RVA: 0x000AC753 File Offset: 0x000AA953
		public MailAddress(string address)
			: this(address, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailAddress" /> class using the specified address and display name.</summary>
		/// <param name="address">A <see cref="T:System.String" /> that contains an e-mail address.</param>
		/// <param name="displayName">A <see cref="T:System.String" /> that contains the display name associated with <paramref name="address" />. This parameter can be null.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="address" /> is not in a recognized format.-or-<paramref name="address" /> contains non-ASCII characters.</exception>
		// Token: 0x06002BA5 RID: 11173 RVA: 0x000AC75D File Offset: 0x000AA95D
		public MailAddress(string address, string displayName)
			: this(address, displayName, Encoding.UTF8)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailAddress" /> class using the specified address, display name, and encoding.</summary>
		/// <param name="address">A <see cref="T:System.String" /> that contains an e-mail address.</param>
		/// <param name="displayName">A <see cref="T:System.String" /> that contains the display name associated with <paramref name="address" />.</param>
		/// <param name="displayNameEncoding">The <see cref="T:System.Text.Encoding" /> that defines the character set used for <paramref name="displayName" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.-or-<paramref name="displayName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> is <see cref="F:System.String.Empty" /> ("").-or-<paramref name="displayName" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="address" /> is not in a recognized format.-or-<paramref name="address" /> contains non-ASCII characters.</exception>
		// Token: 0x06002BA6 RID: 11174 RVA: 0x000AC76C File Offset: 0x000AA96C
		[MonoTODO("We don't do anything with displayNameEncoding")]
		public MailAddress(string address, string displayName, Encoding displayNameEncoding)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (address.Length == 0)
			{
				throw new ArgumentException("address");
			}
			if (displayName != null)
			{
				this.displayName = displayName.Trim();
			}
			this.ParseAddress(address);
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x000AC7AC File Offset: 0x000AA9AC
		private void ParseAddress(string address)
		{
			address = address.Trim();
			int num = address.IndexOf('"');
			if (num != -1)
			{
				if (num != 0 || address.Length == 1)
				{
					throw MailAddress.CreateFormatException();
				}
				int num2 = address.LastIndexOf('"');
				if (num2 == num)
				{
					throw MailAddress.CreateFormatException();
				}
				if (this.displayName == null)
				{
					this.displayName = address.Substring(num + 1, num2 - num - 1).Trim();
				}
				address = address.Substring(num2 + 1).Trim();
			}
			num = address.IndexOf('<');
			if (num >= 0)
			{
				if (this.displayName == null)
				{
					this.displayName = address.Substring(0, num).Trim();
				}
				if (address.Length - 1 == num)
				{
					throw MailAddress.CreateFormatException();
				}
				int num3 = address.IndexOf('>', num + 1);
				if (num3 == -1)
				{
					throw MailAddress.CreateFormatException();
				}
				address = address.Substring(num + 1, num3 - num - 1).Trim();
			}
			this.address = address;
			num = address.IndexOf('@');
			if (num <= 0)
			{
				throw MailAddress.CreateFormatException();
			}
			if (num != address.LastIndexOf('@'))
			{
				throw MailAddress.CreateFormatException();
			}
			this.user = address.Substring(0, num).Trim();
			if (this.user.Length == 0)
			{
				throw MailAddress.CreateFormatException();
			}
			this.host = address.Substring(num + 1).Trim();
			if (this.host.Length == 0)
			{
				throw MailAddress.CreateFormatException();
			}
		}

		/// <summary>Gets the e-mail address specified when this instance was created.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the e-mail address.</returns>
		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06002BA8 RID: 11176 RVA: 0x000AC900 File Offset: 0x000AAB00
		public string Address
		{
			get
			{
				return this.address;
			}
		}

		/// <summary>Gets the display name composed from the display name and address information specified when this instance was created.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the display name; otherwise, <see cref="F:System.String.Empty" /> ("") if no display name information was specified when this instance was created.</returns>
		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06002BA9 RID: 11177 RVA: 0x000AC908 File Offset: 0x000AAB08
		public string DisplayName
		{
			get
			{
				if (this.displayName == null)
				{
					return string.Empty;
				}
				return this.displayName;
			}
		}

		/// <summary>Gets the host portion of the address specified when this instance was created.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the host computer that accepts e-mail for the <see cref="P:System.Net.Mail.MailAddress.User" /> property.</returns>
		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06002BAA RID: 11178 RVA: 0x000AC91E File Offset: 0x000AAB1E
		public string Host
		{
			get
			{
				return this.host;
			}
		}

		/// <summary>Gets the user information from the address specified when this instance was created.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the user name portion of the <see cref="P:System.Net.Mail.MailAddress.Address" />.</returns>
		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06002BAB RID: 11179 RVA: 0x000AC926 File Offset: 0x000AAB26
		public string User
		{
			get
			{
				return this.user;
			}
		}

		/// <summary>Compares two mail addresses.</summary>
		/// <returns>true if the two mail addresses are equal; otherwise, false.</returns>
		/// <param name="value">A <see cref="T:System.Net.Mail.MailAddress" /> instance to compare to the current instance.</param>
		// Token: 0x06002BAC RID: 11180 RVA: 0x000AC92E File Offset: 0x000AAB2E
		public override bool Equals(object value)
		{
			return value != null && string.Compare(this.ToString(), value.ToString(), StringComparison.OrdinalIgnoreCase) == 0;
		}

		/// <summary>Returns a hash value for a mail address.</summary>
		/// <returns>An integer hash value.</returns>
		// Token: 0x06002BAD RID: 11181 RVA: 0x0009647E File Offset: 0x0009467E
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		/// <summary>Returns a string representation of this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the contents of this <see cref="T:System.Net.Mail.MailAddress" />.</returns>
		// Token: 0x06002BAE RID: 11182 RVA: 0x000AC94C File Offset: 0x000AAB4C
		public override string ToString()
		{
			if (this.to_string != null)
			{
				return this.to_string;
			}
			if (!string.IsNullOrEmpty(this.displayName))
			{
				this.to_string = string.Format("\"{0}\" <{1}>", this.DisplayName, this.Address);
			}
			else
			{
				this.to_string = this.address;
			}
			return this.to_string;
		}

		// Token: 0x06002BAF RID: 11183 RVA: 0x000AC9A5 File Offset: 0x000AABA5
		private static FormatException CreateFormatException()
		{
			return new FormatException("The specified string is not in the form required for an e-mail address.");
		}

		// Token: 0x04002467 RID: 9319
		private string address;

		// Token: 0x04002468 RID: 9320
		private string displayName;

		// Token: 0x04002469 RID: 9321
		private string host;

		// Token: 0x0400246A RID: 9322
		private string user;

		// Token: 0x0400246B RID: 9323
		private string to_string;
	}
}
