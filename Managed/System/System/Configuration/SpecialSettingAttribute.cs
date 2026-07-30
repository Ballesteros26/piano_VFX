using System;

namespace System.Configuration
{
	/// <summary>Indicates that an application settings property has a special significance. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001A4 RID: 420
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class SpecialSettingAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SpecialSettingAttribute" /> class.</summary>
		/// <param name="specialSetting">A <see cref="T:System.Configuration.SpecialSetting" /> enumeration value defining the category of the application settings property.</param>
		// Token: 0x06000C5D RID: 3165 RVA: 0x0003D225 File Offset: 0x0003B425
		public SpecialSettingAttribute(SpecialSetting specialSetting)
		{
			this.setting = specialSetting;
		}

		/// <summary>Gets the value describing the special setting category of the application settings property.</summary>
		/// <returns>A <see cref="T:System.Configuration.SpecialSetting" /> enumeration value defining the category of the application settings property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x0003D234 File Offset: 0x0003B434
		public SpecialSetting SpecialSetting
		{
			get
			{
				return this.setting;
			}
		}

		// Token: 0x04001005 RID: 4101
		private SpecialSetting setting;
	}
}
