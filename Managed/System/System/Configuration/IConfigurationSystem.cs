using System;
using System.Runtime.InteropServices;

namespace System.Configuration
{
	/// <summary>Provides standard configuration methods.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200017B RID: 379
	[ComVisible(false)]
	public interface IConfigurationSystem
	{
		/// <summary>Gets the specified configuration.</summary>
		/// <returns>The object representing the configuration.</returns>
		/// <param name="configKey">The configuration key.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B81 RID: 2945
		object GetConfig(string configKey);

		/// <summary>Used for initialization.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B82 RID: 2946
		void Init();
	}
}
