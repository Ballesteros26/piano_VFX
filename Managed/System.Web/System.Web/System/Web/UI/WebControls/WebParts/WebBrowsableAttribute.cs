using System;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Indicates whether the designated property of a Web Parts control is displayed in a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" /> object.</summary>
	/// <exception cref="T:System.Web.AspNetHostingPermission">for operating in a hosted environment. Demand value: <see cref="F:System.Security.Permissions.SecurityAction.LinkDemand" />; Permission value: <see cref="F:System.Web.AspNetHostingPermissionLevel.Minimal" />.</exception>
	// Token: 0x02000471 RID: 1137
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class WebBrowsableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebBrowsableAttribute" /> class with the <see cref="P:System.Web.UI.WebControls.WebParts.WebBrowsableAttribute.Browsable" /> property set to true.</summary>
		// Token: 0x0600340D RID: 13325 RVA: 0x0008A78C File Offset: 0x0008898C
		public WebBrowsableAttribute()
			: this(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebBrowsableAttribute" /> class with the specified value for the <see cref="P:System.Web.UI.WebControls.WebParts.WebBrowsableAttribute.Browsable" /> property.</summary>
		/// <param name="browsable">A Boolean value indicating whether the property should be displayed in a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" />. </param>
		// Token: 0x0600340E RID: 13326 RVA: 0x0008A795 File Offset: 0x00088995
		public WebBrowsableAttribute(bool browsable)
		{
			this._browsable = browsable;
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" /> control should display a specific property of a Web Parts control.</summary>
		/// <returns>true if <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" /> will display the property; otherwise, false.</returns>
		// Token: 0x17001062 RID: 4194
		// (get) Token: 0x0600340F RID: 13327 RVA: 0x0008A7A4 File Offset: 0x000889A4
		public bool Browsable
		{
			get
			{
				return this._browsable;
			}
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance, or null. </param>
		// Token: 0x06003410 RID: 13328 RVA: 0x0008A7AC File Offset: 0x000889AC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			WebBrowsableAttribute webBrowsableAttribute = obj as WebBrowsableAttribute;
			return webBrowsableAttribute != null && webBrowsableAttribute.Browsable == this.Browsable;
		}

		/// <summary>Returns the hash code for the display name value.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06003411 RID: 13329 RVA: 0x0008A7D9 File Offset: 0x000889D9
		public override int GetHashCode()
		{
			return this._browsable.GetHashCode();
		}

		/// <summary>Determines whether the current instance is set to the default value.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.WebParts.WebBrowsableAttribute" /> equals <see cref="F:System.Web.UI.WebControls.WebParts.WebBrowsableAttribute.Default" />; otherwise, false.</returns>
		// Token: 0x06003412 RID: 13330 RVA: 0x0008A7E6 File Offset: 0x000889E6
		public override bool IsDefaultAttribute()
		{
			return this.Equals(WebBrowsableAttribute.Default);
		}

		/// <summary>Represents an instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebBrowsableAttribute" /> class with the <see cref="P:System.Web.UI.WebControls.WebParts.WebBrowsableAttribute.Browsable" /> property set to true.</summary>
		// Token: 0x04001CEC RID: 7404
		public static readonly WebBrowsableAttribute Yes = new WebBrowsableAttribute(true);

		/// <summary>Represents an instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebBrowsableAttribute" /> class with the <see cref="P:System.Web.UI.WebControls.WebParts.WebBrowsableAttribute.Browsable" /> property set to false.</summary>
		// Token: 0x04001CED RID: 7405
		public static readonly WebBrowsableAttribute No = new WebBrowsableAttribute(false);

		/// <summary>Represents an instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebBrowsableAttribute" /> class with the <see cref="P:System.Web.UI.WebControls.WebParts.WebBrowsableAttribute.Browsable" /> property set to the default value, which is false.</summary>
		// Token: 0x04001CEE RID: 7406
		public static readonly WebBrowsableAttribute Default = WebBrowsableAttribute.No;

		// Token: 0x04001CEF RID: 7407
		private bool _browsable;
	}
}
