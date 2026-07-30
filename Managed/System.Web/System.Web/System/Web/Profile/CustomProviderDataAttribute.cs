using System;

namespace System.Web.Profile
{
	/// <summary>Provides a string of custom data to the provider for a profile property.</summary>
	// Token: 0x02000506 RID: 1286
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class CustomProviderDataAttribute : Attribute
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.Profile.CustomProviderDataAttribute" /> class and specifies a string of custom data.</summary>
		/// <param name="customProviderData">The string of custom data to supply to the provider.</param>
		// Token: 0x06003940 RID: 14656 RVA: 0x0009A029 File Offset: 0x00098229
		public CustomProviderDataAttribute(string customProviderData)
		{
			this.customProviderData = customProviderData;
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Web.Profile.CustomProviderDataAttribute.CustomProviderData" /> property is set to the default value.</summary>
		/// <returns>true if the <see cref="P:System.Web.Profile.CustomProviderDataAttribute.CustomProviderData" /> property is set to the default value; otherwise, false.</returns>
		// Token: 0x06003941 RID: 14657 RVA: 0x0009A038 File Offset: 0x00098238
		public override bool IsDefaultAttribute()
		{
			return string.IsNullOrEmpty(this.CustomProviderData);
		}

		/// <summary>Gets a string of custom data for the profile property provider.</summary>
		/// <returns>A string of custom data for the profile property provider. The default is null.</returns>
		// Token: 0x170011CB RID: 4555
		// (get) Token: 0x06003942 RID: 14658 RVA: 0x0009A045 File Offset: 0x00098245
		public string CustomProviderData
		{
			get
			{
				return this.customProviderData;
			}
		}

		// Token: 0x04001F20 RID: 7968
		private string customProviderData;
	}
}
