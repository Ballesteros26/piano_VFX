using System;

namespace System.Configuration
{
	/// <summary>Provides a string that describes an application settings property group. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200018F RID: 399
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class SettingsGroupDescriptionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsGroupDescriptionAttribute" /> class.</summary>
		/// <param name="description">A <see cref="T:System.String" /> containing the descriptive text for the application settings group.</param>
		// Token: 0x06000BF0 RID: 3056 RVA: 0x0003C88F File Offset: 0x0003AA8F
		public SettingsGroupDescriptionAttribute(string description)
		{
			this.desc = description;
		}

		/// <summary>The descriptive text for the application settings properties group.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the descriptive text for the application settings group.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x0003C89E File Offset: 0x0003AA9E
		public string Description
		{
			get
			{
				return this.desc;
			}
		}

		// Token: 0x04000FE0 RID: 4064
		private string desc;
	}
}
