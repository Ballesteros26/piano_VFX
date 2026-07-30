using System;

namespace System.Configuration
{
	/// <summary>Specifies the settings provider used to provide storage for the current application settings class or property. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200019D RID: 413
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class SettingsProviderAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.SettingsProviderAttribute" /> class.</summary>
		/// <param name="providerTypeName">A <see cref="T:System.String" /> containing the name of the settings provider.</param>
		// Token: 0x06000C4F RID: 3151 RVA: 0x0003D10B File Offset: 0x0003B30B
		public SettingsProviderAttribute(string providerTypeName)
		{
			if (providerTypeName == null)
			{
				throw new ArgumentNullException("providerTypeName");
			}
			this.providerTypeName = providerTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsProviderAttribute" /> class. </summary>
		/// <param name="providerType">A <see cref="T:System.Type" /> containing the settings provider type.</param>
		// Token: 0x06000C50 RID: 3152 RVA: 0x0003D128 File Offset: 0x0003B328
		public SettingsProviderAttribute(Type providerType)
		{
			if (providerType == null)
			{
				throw new ArgumentNullException("providerType");
			}
			this.providerTypeName = providerType.AssemblyQualifiedName;
		}

		/// <summary>Gets the type name of the settings provider.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the settings provider.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x0003D150 File Offset: 0x0003B350
		public string ProviderTypeName
		{
			get
			{
				return this.providerTypeName;
			}
		}

		// Token: 0x04000FFB RID: 4091
		private string providerTypeName;
	}
}
