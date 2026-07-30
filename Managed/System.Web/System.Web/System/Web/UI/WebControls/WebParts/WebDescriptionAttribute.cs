using System;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Defines the string value to use as a ToolTip for a property of a Web Parts control.</summary>
	// Token: 0x02000472 RID: 1138
	[AttributeUsage(AttributeTargets.Property)]
	public class WebDescriptionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebDescriptionAttribute" /> class. </summary>
		// Token: 0x06003414 RID: 13332 RVA: 0x0008A815 File Offset: 0x00088A15
		public WebDescriptionAttribute()
			: this(string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebDescriptionAttribute" /> class with the specified description.</summary>
		/// <param name="description">The ToolTip to use in a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" />. </param>
		// Token: 0x06003415 RID: 13333 RVA: 0x0008A822 File Offset: 0x00088A22
		public WebDescriptionAttribute(string description)
		{
			this._description = description;
		}

		/// <summary>Gets the ToolTip for a property to display in a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" /> control.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the value to display in a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" />.</returns>
		// Token: 0x17001063 RID: 4195
		// (get) Token: 0x06003416 RID: 13334 RVA: 0x0008A831 File Offset: 0x00088A31
		public virtual string Description
		{
			get
			{
				return this.DescriptionValue;
			}
		}

		/// <summary>Gets or sets the ToolTip to display in the <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" /> control.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the value to display in a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" />.</returns>
		// Token: 0x17001064 RID: 4196
		// (get) Token: 0x06003417 RID: 13335 RVA: 0x0008A839 File Offset: 0x00088A39
		// (set) Token: 0x06003418 RID: 13336 RVA: 0x0008A841 File Offset: 0x00088A41
		protected string DescriptionValue
		{
			get
			{
				return this._description;
			}
			set
			{
				this._description = value;
			}
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance, or null. </param>
		// Token: 0x06003419 RID: 13337 RVA: 0x0008A84C File Offset: 0x00088A4C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			WebDescriptionAttribute webDescriptionAttribute = obj as WebDescriptionAttribute;
			return webDescriptionAttribute != null && webDescriptionAttribute.Description == this.Description;
		}

		/// <summary>Returns the hash code for the display name value.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600341A RID: 13338 RVA: 0x0008A87C File Offset: 0x00088A7C
		public override int GetHashCode()
		{
			return this.Description.GetHashCode();
		}

		/// <summary>Determines whether the current instance is set to the default value.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.WebParts.WebDescriptionAttribute" /> equals <see cref="F:System.Web.UI.WebControls.WebParts.WebDescriptionAttribute.Default" />; otherwise, false.</returns>
		// Token: 0x0600341B RID: 13339 RVA: 0x0008A889 File Offset: 0x00088A89
		public override bool IsDefaultAttribute()
		{
			return this.Equals(WebDescriptionAttribute.Default);
		}

		/// <summary>Represents an instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebDescriptionAttribute" /> class with the <see cref="P:System.Web.UI.WebControls.WebParts.WebDescriptionAttribute.Description" /> property set to an empty string ("").</summary>
		// Token: 0x04001CF0 RID: 7408
		public static readonly WebDescriptionAttribute Default = new WebDescriptionAttribute();

		// Token: 0x04001CF1 RID: 7409
		private string _description;
	}
}
