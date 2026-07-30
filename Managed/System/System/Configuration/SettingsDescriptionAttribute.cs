using System;

namespace System.Configuration
{
	/// <summary>Provides a string that describes an individual configuration property. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200018E RID: 398
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class SettingsDescriptionAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.SettingsDescriptionAttribute" /> class.</summary>
		/// <param name="description">The <see cref="T:System.String" /> used as descriptive text.</param>
		// Token: 0x06000BEE RID: 3054 RVA: 0x0003C878 File Offset: 0x0003AA78
		public SettingsDescriptionAttribute(string description)
		{
			this.desc = description;
		}

		/// <summary>Gets the descriptive text for the associated configuration property.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the descriptive text for the associated configuration property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x0003C887 File Offset: 0x0003AA87
		public string Description
		{
			get
			{
				return this.desc;
			}
		}

		// Token: 0x04000FDF RID: 4063
		private string desc;
	}
}
