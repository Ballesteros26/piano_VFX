using System;

namespace System.ComponentModel
{
	/// <summary>Indicates whether the component associated with this attribute has been inherited from a base class. This class cannot be inherited.</summary>
	// Token: 0x020002F7 RID: 759
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event)]
	public sealed class InheritanceAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InheritanceAttribute" /> class.</summary>
		// Token: 0x06001884 RID: 6276 RVA: 0x00068A40 File Offset: 0x00066C40
		public InheritanceAttribute()
		{
			this.inheritanceLevel = InheritanceAttribute.Default.inheritanceLevel;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InheritanceAttribute" /> class with the specified inheritance level.</summary>
		/// <param name="inheritanceLevel">An <see cref="T:System.ComponentModel.InheritanceLevel" /> that indicates the level of inheritance to set this attribute to. </param>
		// Token: 0x06001885 RID: 6277 RVA: 0x00068A58 File Offset: 0x00066C58
		public InheritanceAttribute(InheritanceLevel inheritanceLevel)
		{
			this.inheritanceLevel = inheritanceLevel;
		}

		/// <summary>Gets or sets the current inheritance level stored in this attribute.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.InheritanceLevel" /> stored in this attribute.</returns>
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x00068A67 File Offset: 0x00066C67
		public InheritanceLevel InheritanceLevel
		{
			get
			{
				return this.inheritanceLevel;
			}
		}

		/// <summary>Override to test for equality.</summary>
		/// <returns>true if the object is the same; otherwise, false.</returns>
		/// <param name="value">The object to test. </param>
		// Token: 0x06001887 RID: 6279 RVA: 0x00068A6F File Offset: 0x00066C6F
		public override bool Equals(object value)
		{
			return value == this || (value is InheritanceAttribute && ((InheritanceAttribute)value).InheritanceLevel == this.inheritanceLevel);
		}

		/// <summary>Returns the hashcode for this object.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.InheritanceAttribute" />.</returns>
		// Token: 0x06001888 RID: 6280 RVA: 0x0004C98A File Offset: 0x0004AB8A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>true if the current value of the attribute is the default; otherwise, false.</returns>
		// Token: 0x06001889 RID: 6281 RVA: 0x00068A94 File Offset: 0x00066C94
		public override bool IsDefaultAttribute()
		{
			return this.Equals(InheritanceAttribute.Default);
		}

		/// <summary>Converts this attribute to a string.</summary>
		/// <returns>A string that represents this <see cref="T:System.ComponentModel.InheritanceAttribute" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600188A RID: 6282 RVA: 0x00068AA1 File Offset: 0x00066CA1
		public override string ToString()
		{
			return TypeDescriptor.GetConverter(typeof(InheritanceLevel)).ConvertToString(this.InheritanceLevel);
		}

		// Token: 0x04001426 RID: 5158
		private readonly InheritanceLevel inheritanceLevel;

		/// <summary>Specifies that the component is inherited. This field is read-only.</summary>
		// Token: 0x04001427 RID: 5159
		public static readonly InheritanceAttribute Inherited = new InheritanceAttribute(InheritanceLevel.Inherited);

		/// <summary>Specifies that the component is inherited and is read-only. This field is read-only.</summary>
		// Token: 0x04001428 RID: 5160
		public static readonly InheritanceAttribute InheritedReadOnly = new InheritanceAttribute(InheritanceLevel.InheritedReadOnly);

		/// <summary>Specifies that the component is not inherited. This field is read-only.</summary>
		// Token: 0x04001429 RID: 5161
		public static readonly InheritanceAttribute NotInherited = new InheritanceAttribute(InheritanceLevel.NotInherited);

		/// <summary>Specifies that the default value for <see cref="T:System.ComponentModel.InheritanceAttribute" /> is <see cref="F:System.ComponentModel.InheritanceAttribute.NotInherited" />. This field is read-only.</summary>
		// Token: 0x0400142A RID: 5162
		public static readonly InheritanceAttribute Default = InheritanceAttribute.NotInherited;
	}
}
