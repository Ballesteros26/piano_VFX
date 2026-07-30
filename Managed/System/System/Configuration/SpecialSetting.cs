using System;

namespace System.Configuration
{
	/// <summary>Specifies the special setting category of a application settings property.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001A3 RID: 419
	public enum SpecialSetting
	{
		/// <summary>The configuration property represents a connection string, typically for a data store or network resource. </summary>
		// Token: 0x04001003 RID: 4099
		ConnectionString,
		/// <summary>The configuration property represents a Uniform Resource Locator (URL) to a Web service.</summary>
		// Token: 0x04001004 RID: 4100
		WebServiceUrl
	}
}
