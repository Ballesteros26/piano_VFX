using System;
using Unity;

namespace System.Text
{
	/// <summary>Provides basic information about an encoding.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000281 RID: 641
	[Serializable]
	public sealed class EncodingInfo
	{
		// Token: 0x06001D7D RID: 7549 RVA: 0x0006E5CD File Offset: 0x0006C7CD
		internal EncodingInfo(int codePage, string name, string displayName)
		{
			this.iCodePage = codePage;
			this.strEncodingName = name;
			this.strDisplayName = displayName;
		}

		/// <summary>Gets the code page identifier of the encoding.</summary>
		/// <returns>The code page identifier of the encoding.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001D7E RID: 7550 RVA: 0x0006E5EA File Offset: 0x0006C7EA
		public int CodePage
		{
			get
			{
				return this.iCodePage;
			}
		}

		/// <summary>Gets the name registered with the Internet Assigned Numbers Authority (IANA) for the encoding.</summary>
		/// <returns>The IANA name for the encoding. For more information about the IANA, see www.iana.org.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001D7F RID: 7551 RVA: 0x0006E5F2 File Offset: 0x0006C7F2
		public string Name
		{
			get
			{
				return this.strEncodingName;
			}
		}

		/// <summary>Gets the human-readable description of the encoding.</summary>
		/// <returns>The human-readable description of the encoding.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001D80 RID: 7552 RVA: 0x0006E5FA File Offset: 0x0006C7FA
		public string DisplayName
		{
			get
			{
				return this.strDisplayName;
			}
		}

		/// <summary>Returns a <see cref="T:System.Text.Encoding" /> object that corresponds to the current <see cref="T:System.Text.EncodingInfo" /> object.</summary>
		/// <returns>A <see cref="T:System.Text.Encoding" /> object that corresponds to the current <see cref="T:System.Text.EncodingInfo" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D81 RID: 7553 RVA: 0x0006E602 File Offset: 0x0006C802
		public Encoding GetEncoding()
		{
			return Encoding.GetEncoding(this.iCodePage);
		}

		/// <summary>Gets a value indicating whether the specified object is equal to the current <see cref="T:System.Text.EncodingInfo" /> object.</summary>
		/// <returns>true if <paramref name="value" /> is a <see cref="T:System.Text.EncodingInfo" /> object and is equal to the current <see cref="T:System.Text.EncodingInfo" /> object; otherwise, false.</returns>
		/// <param name="value">An object to compare to the current <see cref="T:System.Text.EncodingInfo" /> object.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D82 RID: 7554 RVA: 0x0006E610 File Offset: 0x0006C810
		public override bool Equals(object value)
		{
			EncodingInfo encodingInfo = value as EncodingInfo;
			return encodingInfo != null && this.CodePage == encodingInfo.CodePage;
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.Text.EncodingInfo" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D83 RID: 7555 RVA: 0x0006E637 File Offset: 0x0006C837
		public override int GetHashCode()
		{
			return this.CodePage;
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal EncodingInfo()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001050 RID: 4176
		private int iCodePage;

		// Token: 0x04001051 RID: 4177
		private string strEncodingName;

		// Token: 0x04001052 RID: 4178
		private string strDisplayName;
	}
}
