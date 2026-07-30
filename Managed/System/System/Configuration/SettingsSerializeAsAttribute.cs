using System;

namespace System.Configuration
{
	/// <summary>Specifies the serialization mechanism that the settings provider should use. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001A1 RID: 417
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class SettingsSerializeAsAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.SettingsSerializeAsAttribute" /> class.</summary>
		/// <param name="serializeAs">A <see cref="T:System.Configuration.SettingsSerializeAs" /> enumerated value that specifies the serialization scheme.</param>
		// Token: 0x06000C59 RID: 3161 RVA: 0x0003D1A2 File Offset: 0x0003B3A2
		public SettingsSerializeAsAttribute(SettingsSerializeAs serializeAs)
		{
			this.serializeAs = serializeAs;
		}

		/// <summary>Gets the <see cref="T:System.Configuration.SettingsSerializeAs" /> enumeration value that specifies the serialization scheme.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsSerializeAs" /> enumerated value that specifies the serialization scheme.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x0003D1B1 File Offset: 0x0003B3B1
		public SettingsSerializeAs SerializeAs
		{
			get
			{
				return this.serializeAs;
			}
		}

		// Token: 0x04001001 RID: 4097
		private SettingsSerializeAs serializeAs;
	}
}
