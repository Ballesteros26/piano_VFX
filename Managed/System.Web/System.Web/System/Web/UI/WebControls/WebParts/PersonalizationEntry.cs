using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Represents core pieces of custom personalization state information contained in a <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" /> object. This class cannot be inherited. </summary>
	// Token: 0x020006DC RID: 1756
	public sealed class PersonalizationEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationEntry" /> class using the specified value and scope.</summary>
		/// <param name="value">An object of personalization data associated with the personalization scope in the <paramref name="scope" /> parameter.</param>
		/// <param name="scope">The <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> that applies to the custom personalization information.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scope" /> is set to a value that is not a valid member of <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" />.</exception>
		// Token: 0x06004A73 RID: 19059 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public PersonalizationEntry(object value, PersonalizationScope scope)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationEntry" /> class using the provided parameters.</summary>
		/// <param name="value">An object of personalization data associated with the personalization scope in the <paramref name="scope" /> parameter.</param>
		/// <param name="scope">The <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> that applies to the custom personalization information.</param>
		/// <param name="isSensitive">A Boolean value indicating if the custom state information is sensitive and should not be exported.</param>
		// Token: 0x06004A74 RID: 19060 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public PersonalizationEntry(object value, PersonalizationScope scope, bool isSensitive)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the Boolean value that indicates if the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationEntry" /> contains sensitive information.</summary>
		/// <returns>true if the sensitive setting for the attribute is set; otherwise false.</returns>
		// Token: 0x170016FB RID: 5883
		// (get) Token: 0x06004A75 RID: 19061 RVA: 0x000CA818 File Offset: 0x000C8A18
		// (set) Token: 0x06004A76 RID: 19062 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public bool IsSensitive
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the personalization scope associated with this personalization data entry.</summary>
		/// <returns>The personalization scope associated with this personalization data entry.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scope" /> is set to a value that is not a valid member of <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" />.</exception>
		// Token: 0x170016FC RID: 5884
		// (get) Token: 0x06004A77 RID: 19063 RVA: 0x000CA834 File Offset: 0x000C8A34
		// (set) Token: 0x06004A78 RID: 19064 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public PersonalizationScope Scope
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return PersonalizationScope.User;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the personalization state information for this entry.</summary>
		/// <returns>An object representing personalization state information.</returns>
		// Token: 0x170016FD RID: 5885
		// (get) Token: 0x06004A79 RID: 19065 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004A7A RID: 19066 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public object Value
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
