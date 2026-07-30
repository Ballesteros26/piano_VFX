using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Defines a metadata attribute that you can use when developing ASP.NET server controls. Use the <see cref="T:System.Web.UI.ParseChildrenAttribute" /> class to indicate how the page parser should treat content nested inside a server control tag declared on a page. This class cannot be inherited.</summary>
	// Token: 0x02000218 RID: 536
	[AttributeUsage(AttributeTargets.Class)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class ParseChildrenAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ParseChildrenAttribute" /> class.</summary>
		// Token: 0x06001604 RID: 5636 RVA: 0x0003B5D8 File Offset: 0x000397D8
		public ParseChildrenAttribute()
		{
			this.childrenAsProperties = false;
			this.defaultProperty = "";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ParseChildrenAttribute" /> class using the <see cref="P:System.Web.UI.ParseChildrenAttribute.ChildrenAsProperties" /> property to determine if the elements that are contained within a server control are parsed as properties of the server control.</summary>
		/// <param name="childrenAsProperties">true to parse the elements as properties of the server control; otherwise, false. </param>
		// Token: 0x06001605 RID: 5637 RVA: 0x0003B602 File Offset: 0x00039802
		public ParseChildrenAttribute(bool childrenAsProperties)
		{
			this.childrenAsProperties = childrenAsProperties;
			this.defaultProperty = "";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ParseChildrenAttribute" /> class using the <paramref name="childrenAsProperties" /> and <paramref name="defaultProperty" /> parameters.</summary>
		/// <param name="childrenAsProperties">true to parse the elements as properties of the server control; otherwise, false. </param>
		/// <param name="defaultProperty">A string that defines a collection property of the server control into which nested content is parsed by default. </param>
		// Token: 0x06001606 RID: 5638 RVA: 0x0003B62C File Offset: 0x0003982C
		public ParseChildrenAttribute(bool childrenAsProperties, string defaultProperty)
		{
			this.childrenAsProperties = childrenAsProperties;
			if (childrenAsProperties)
			{
				this.defaultProperty = defaultProperty;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ParseChildrenAttribute" /> class using the <see cref="P:System.Web.UI.ParseChildrenAttribute.ChildControlType" /> property to determine which elements that are contained within a server control are parsed as controls.</summary>
		/// <param name="childControlType">The control type to parse as a property. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="childControlType" /> is null. </exception>
		// Token: 0x06001607 RID: 5639 RVA: 0x0003B655 File Offset: 0x00039855
		public ParseChildrenAttribute(Type childControlType)
		{
			this.childType = childControlType;
			this.defaultProperty = "";
		}

		/// <summary>Gets or sets a value indicating whether to parse the elements that are contained within a server control as properties.</summary>
		/// <returns>true to parse the elements as properties; otherwise, false. The default is true.</returns>
		/// <exception cref="T:System.NotSupportedException">The current <see cref="T:System.Web.UI.ParseChildrenAttribute" /> was invoked with <paramref name="childrenAsProperties" /> set to false.</exception>
		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x0003B67F File Offset: 0x0003987F
		// (set) Token: 0x06001609 RID: 5641 RVA: 0x0003B687 File Offset: 0x00039887
		public bool ChildrenAsProperties
		{
			get
			{
				return this.childrenAsProperties;
			}
			set
			{
				this.childrenAsProperties = value;
			}
		}

		/// <summary>Gets or sets the default property for the server control into which the elements are parsed.</summary>
		/// <returns>The name of the default collection property of the server control into which the elements are parsed.</returns>
		/// <exception cref="T:System.NotSupportedException">The current <see cref="T:System.Web.UI.ParseChildrenAttribute" /> was invoked with <paramref name="childrenAsProperties" /> set to false.</exception>
		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x0003B690 File Offset: 0x00039890
		// (set) Token: 0x0600160B RID: 5643 RVA: 0x0003B698 File Offset: 0x00039898
		public string DefaultProperty
		{
			get
			{
				return this.defaultProperty;
			}
			set
			{
				this.defaultProperty = value;
			}
		}

		/// <summary>Gets a value indicating the allowed type of a control. </summary>
		/// <returns>The control type. The default is <see cref="T:System.Web.UI.Control" />. </returns>
		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x0600160C RID: 5644 RVA: 0x0003B6A1 File Offset: 0x000398A1
		public Type ChildControlType
		{
			get
			{
				return this.childType;
			}
		}

		/// <summary>Determines whether the specified object is equal to the current object.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to the current object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current object.</param>
		// Token: 0x0600160D RID: 5645 RVA: 0x0003B6AC File Offset: 0x000398AC
		public override bool Equals(object obj)
		{
			ParseChildrenAttribute parseChildrenAttribute = obj as ParseChildrenAttribute;
			return parseChildrenAttribute != null && this.childrenAsProperties == parseChildrenAttribute.childrenAsProperties && (!this.childrenAsProperties || this.defaultProperty == parseChildrenAttribute.DefaultProperty);
		}

		/// <summary>Serves as a hash function for the <see cref="T:System.Web.UI.ParseChildrenAttribute" /> object.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Web.UI.ParseChildrenAttribute" /> object. </returns>
		// Token: 0x0600160E RID: 5646 RVA: 0x00031CB1 File Offset: 0x0002FEB1
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Returns a value indicating whether the value of the current instance of the <see cref="T:System.Web.UI.ParseChildrenAttribute" /> class is the default value of the derived class.</summary>
		/// <returns>true if the current <see cref="T:System.Web.UI.ParseChildrenAttribute" /> value is the default instance; otherwise, false.</returns>
		// Token: 0x0600160F RID: 5647 RVA: 0x0003B6F0 File Offset: 0x000398F0
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ParseChildrenAttribute.Default);
		}

		// Token: 0x04001540 RID: 5440
		private bool childrenAsProperties;

		// Token: 0x04001541 RID: 5441
		private string defaultProperty;

		/// <summary>Defines the default value for the <see cref="T:System.Web.UI.ParseChildrenAttribute" /> class. This field is read-only.</summary>
		// Token: 0x04001542 RID: 5442
		public static readonly ParseChildrenAttribute Default = new ParseChildrenAttribute();

		/// <summary>Indicates that the nested content that is contained within the server control is parsed as controls.</summary>
		// Token: 0x04001543 RID: 5443
		public static readonly ParseChildrenAttribute ParseAsChildren = new ParseChildrenAttribute(false);

		/// <summary>Indicates that the nested content that is contained within a server control is parsed as properties of the control. </summary>
		// Token: 0x04001544 RID: 5444
		public static readonly ParseChildrenAttribute ParseAsProperties = new ParseChildrenAttribute(true);

		// Token: 0x04001545 RID: 5445
		private Type childType = typeof(Control);
	}
}
