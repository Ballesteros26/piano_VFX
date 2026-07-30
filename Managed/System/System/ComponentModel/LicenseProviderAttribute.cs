using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the <see cref="T:System.ComponentModel.LicenseProvider" /> to use with a class. This class cannot be inherited.</summary>
	// Token: 0x020002A3 RID: 675
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class LicenseProviderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseProviderAttribute" /> class without a license provider.</summary>
		// Token: 0x060014F0 RID: 5360 RVA: 0x00053910 File Offset: 0x00051B10
		public LicenseProviderAttribute()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseProviderAttribute" /> class with the specified type.</summary>
		/// <param name="typeName">The fully qualified name of the license provider class. </param>
		// Token: 0x060014F1 RID: 5361 RVA: 0x00053919 File Offset: 0x00051B19
		public LicenseProviderAttribute(string typeName)
		{
			this.licenseProviderName = typeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseProviderAttribute" /> class with the specified type of license provider.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of the license provider class. </param>
		// Token: 0x060014F2 RID: 5362 RVA: 0x00053928 File Offset: 0x00051B28
		public LicenseProviderAttribute(Type type)
		{
			this.licenseProviderType = type;
		}

		/// <summary>Gets the license provider that must be used with the associated class.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of the license provider. The default value is null.</returns>
		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x00053937 File Offset: 0x00051B37
		public Type LicenseProvider
		{
			get
			{
				if (this.licenseProviderType == null && this.licenseProviderName != null)
				{
					this.licenseProviderType = Type.GetType(this.licenseProviderName);
				}
				return this.licenseProviderType;
			}
		}

		/// <summary>Indicates a unique ID for this attribute type.</summary>
		/// <returns>A unique ID for this attribute type.</returns>
		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x00053968 File Offset: 0x00051B68
		public override object TypeId
		{
			get
			{
				string fullName = this.licenseProviderName;
				if (fullName == null && this.licenseProviderType != null)
				{
					fullName = this.licenseProviderType.FullName;
				}
				return base.GetType().FullName + fullName;
			}
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <returns>true if <paramref name="value" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="value">Another object to compare to. </param>
		// Token: 0x060014F5 RID: 5365 RVA: 0x000539AC File Offset: 0x00051BAC
		public override bool Equals(object value)
		{
			if (value is LicenseProviderAttribute && value != null)
			{
				Type licenseProvider = ((LicenseProviderAttribute)value).LicenseProvider;
				if (licenseProvider == this.LicenseProvider)
				{
					return true;
				}
				if (licenseProvider != null && licenseProvider.Equals(this.LicenseProvider))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.LicenseProviderAttribute" />.</returns>
		// Token: 0x060014F6 RID: 5366 RVA: 0x0004C98A File Offset: 0x0004AB8A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Specifies the default value, which is no provider. This static field is read-only.</summary>
		// Token: 0x04001304 RID: 4868
		public static readonly LicenseProviderAttribute Default = new LicenseProviderAttribute();

		// Token: 0x04001305 RID: 4869
		private Type licenseProviderType;

		// Token: 0x04001306 RID: 4870
		private string licenseProviderName;
	}
}
