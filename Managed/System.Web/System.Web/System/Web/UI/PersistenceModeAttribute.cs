using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Defines the metadata attribute that specifies how an ASP.NET server control property or event is persisted to an ASP.NET page at design time. This class cannot be inherited.</summary>
	// Token: 0x0200021B RID: 539
	[AttributeUsage(AttributeTargets.All)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class PersistenceModeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PersistenceModeAttribute" /> class. </summary>
		/// <param name="mode">The <see cref="T:System.Web.UI.PersistenceMode" /> value to assign to <see cref="P:System.Web.UI.PersistenceModeAttribute.Mode" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="mode" /> is not one of the <see cref="T:System.Web.UI.PersistenceMode" /> values.</exception>
		// Token: 0x06001627 RID: 5671 RVA: 0x0003B866 File Offset: 0x00039A66
		public PersistenceModeAttribute(PersistenceMode mode)
		{
			this.mode = mode;
		}

		/// <summary>Gets the current value of the <see cref="T:System.Web.UI.PersistenceMode" /> enumeration.</summary>
		/// <returns>A <see cref="T:System.Web.UI.PersistenceMode" /> that represents the current value of the enumeration. This value can be Attribute, InnerProperty, InnerDefaultProperty, or EncodedInnerDefaultProperty. The default is Attribute.</returns>
		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001628 RID: 5672 RVA: 0x0003B875 File Offset: 0x00039A75
		public PersistenceMode Mode
		{
			get
			{
				return this.mode;
			}
		}

		/// <summary>Compares the <see cref="T:System.Web.UI.PersistenceModeAttribute" /> object against another object.</summary>
		/// <returns>true if the objects are considered equal; otherwise, false.</returns>
		/// <param name="obj">The object to compare to.</param>
		// Token: 0x06001629 RID: 5673 RVA: 0x0003B880 File Offset: 0x00039A80
		public override bool Equals(object obj)
		{
			PersistenceModeAttribute persistenceModeAttribute = obj as PersistenceModeAttribute;
			return persistenceModeAttribute != null && persistenceModeAttribute.mode == this.mode;
		}

		/// <summary>Provides a hash value for a <see cref="T:System.Web.UI.PersistenceModeAttribute" /> attribute.</summary>
		/// <returns>The hash value to be assigned to the <see cref="T:System.Web.UI.PersistenceModeAttribute" />.</returns>
		// Token: 0x0600162A RID: 5674 RVA: 0x0003B875 File Offset: 0x00039A75
		public override int GetHashCode()
		{
			return (int)this.mode;
		}

		/// <summary>Indicates whether the <see cref="T:System.Web.UI.PersistenceModeAttribute" /> object is of the default type.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.PersistenceModeAttribute" /> is of the default type; otherwise, false.</returns>
		// Token: 0x0600162B RID: 5675 RVA: 0x0003B8A7 File Offset: 0x00039AA7
		public override bool IsDefaultAttribute()
		{
			return this.mode == PersistenceMode.Attribute;
		}

		// Token: 0x04001550 RID: 5456
		private PersistenceMode mode;

		/// <summary>Specifies that the property or event persists in the opening tag of the server control as an attribute. This field is read-only.</summary>
		// Token: 0x04001551 RID: 5457
		public static readonly PersistenceModeAttribute Attribute = new PersistenceModeAttribute(PersistenceMode.Attribute);

		/// <summary>Specifies the default type for the <see cref="T:System.Web.UI.PersistenceModeAttribute" /> class. The default is PersistenceMode.Attribute. This field is read-only.</summary>
		// Token: 0x04001552 RID: 5458
		public static readonly PersistenceModeAttribute Default = new PersistenceModeAttribute(PersistenceMode.Attribute);

		/// <summary>Specifies that a property is HTML-encoded and persists as the only inner content of the ASP.NET server control. This field is read-only.</summary>
		// Token: 0x04001553 RID: 5459
		public static readonly PersistenceModeAttribute EncodedInnerDefaultProperty = new PersistenceModeAttribute(PersistenceMode.EncodedInnerDefaultProperty);

		/// <summary>Specifies that a property persists as the only inner content of the ASP.NET server control. This field is read-only.</summary>
		// Token: 0x04001554 RID: 5460
		public static readonly PersistenceModeAttribute InnerDefaultProperty = new PersistenceModeAttribute(PersistenceMode.InnerDefaultProperty);

		/// <summary>Specifies that the property persists as a nested tag within the opening and closing tags of the server control. This field is read-only.</summary>
		// Token: 0x04001555 RID: 5461
		public static readonly PersistenceModeAttribute InnerProperty = new PersistenceModeAttribute(PersistenceMode.InnerProperty);
	}
}
