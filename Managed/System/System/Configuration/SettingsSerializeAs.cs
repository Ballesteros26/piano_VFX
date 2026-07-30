using System;

namespace System.Configuration
{
	/// <summary>Determines the serialization scheme used to store application settings.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001A0 RID: 416
	public enum SettingsSerializeAs
	{
		/// <summary>The settings property is serialized as plain text.</summary>
		// Token: 0x04000FFD RID: 4093
		String,
		/// <summary>The settings property is serialized as XML using XML serialization.</summary>
		// Token: 0x04000FFE RID: 4094
		Xml,
		/// <summary>The settings property is serialized using binary object serialization.</summary>
		// Token: 0x04000FFF RID: 4095
		Binary,
		/// <summary>The settings provider has implicit knowledge of the property or its type and picks an appropriate serialization mechanism. Often used for custom serialization.</summary>
		// Token: 0x04001000 RID: 4096
		ProviderSpecific
	}
}
