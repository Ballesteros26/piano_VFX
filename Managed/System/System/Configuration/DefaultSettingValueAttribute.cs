using System;

namespace System.Configuration
{
	/// <summary>Specifies the default value for an application settings property.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000176 RID: 374
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class DefaultSettingValueAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.DefaultSettingValueAttribute" /> class.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that represents the default value for the property. </param>
		// Token: 0x06000B75 RID: 2933 RVA: 0x0003BC3A File Offset: 0x00039E3A
		public DefaultSettingValueAttribute(string value)
		{
			this.value = value;
		}

		/// <summary>Gets the default value for the application settings property.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the default value for the property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x0003BC49 File Offset: 0x00039E49
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04000FC8 RID: 4040
		private string value;
	}
}
