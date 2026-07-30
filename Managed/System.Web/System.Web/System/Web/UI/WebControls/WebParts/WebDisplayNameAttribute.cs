using System;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Defines the friendly name for a property of a Web Parts control.</summary>
	// Token: 0x02000473 RID: 1139
	[AttributeUsage(AttributeTargets.Property)]
	public class WebDisplayNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebDisplayNameAttribute" /> class without a specified name. </summary>
		// Token: 0x0600341D RID: 13341 RVA: 0x0008A8A2 File Offset: 0x00088AA2
		public WebDisplayNameAttribute()
			: this(string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebDisplayNameAttribute" /> class with a specified display name.</summary>
		/// <param name="displayName">The friendly name to use in a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" />.  </param>
		// Token: 0x0600341E RID: 13342 RVA: 0x0008A8AF File Offset: 0x00088AAF
		public WebDisplayNameAttribute(string displayName)
		{
			this._displayName = displayName;
		}

		/// <summary>Gets the name of a property to display in a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" /> control.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the value to display in a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" />.</returns>
		// Token: 0x17001065 RID: 4197
		// (get) Token: 0x0600341F RID: 13343 RVA: 0x0008A8BE File Offset: 0x00088ABE
		public virtual string DisplayName
		{
			get
			{
				return this.DisplayNameValue;
			}
		}

		/// <summary>Gets or sets the name to display in the <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" /> control.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the value to display in a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" />.</returns>
		// Token: 0x17001066 RID: 4198
		// (get) Token: 0x06003420 RID: 13344 RVA: 0x0008A8C6 File Offset: 0x00088AC6
		// (set) Token: 0x06003421 RID: 13345 RVA: 0x0008A8CE File Offset: 0x00088ACE
		protected string DisplayNameValue
		{
			get
			{
				return this._displayName;
			}
			set
			{
				this._displayName = value;
			}
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance, or null. </param>
		// Token: 0x06003422 RID: 13346 RVA: 0x0008A8D8 File Offset: 0x00088AD8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			WebDisplayNameAttribute webDisplayNameAttribute = obj as WebDisplayNameAttribute;
			return webDisplayNameAttribute != null && webDisplayNameAttribute.DisplayName == this.DisplayName;
		}

		/// <summary>Returns the hash code for the display name value.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06003423 RID: 13347 RVA: 0x0008A908 File Offset: 0x00088B08
		public override int GetHashCode()
		{
			return this.DisplayName.GetHashCode();
		}

		/// <summary>Determines whether the current instance is set to the default value.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.WebParts.WebDisplayNameAttribute" /> equals <see cref="F:System.Web.UI.WebControls.WebParts.WebDisplayNameAttribute.Default" />; otherwise, false.</returns>
		// Token: 0x06003424 RID: 13348 RVA: 0x0008A915 File Offset: 0x00088B15
		public override bool IsDefaultAttribute()
		{
			return this.Equals(WebDisplayNameAttribute.Default);
		}

		/// <summary>Represents an instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebDisplayNameAttribute" /> class with the <see cref="P:System.Web.UI.WebControls.WebParts.WebDisplayNameAttribute.DisplayName" /> property set to an empty string ("").</summary>
		// Token: 0x04001CF2 RID: 7410
		public static readonly WebDisplayNameAttribute Default = new WebDisplayNameAttribute();

		// Token: 0x04001CF3 RID: 7411
		private string _displayName;
	}
}
