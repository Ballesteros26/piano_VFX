using System;
using System.Collections;

namespace System.Configuration
{
	/// <summary>Represents a collection of key/value pairs used to describe a configuration object as well as a <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
	// Token: 0x0200018B RID: 395
	[Serializable]
	public class SettingsAttributeDictionary : Hashtable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsAttributeDictionary" /> class. </summary>
		// Token: 0x06000BDB RID: 3035 RVA: 0x0003C4B5 File Offset: 0x0003A6B5
		public SettingsAttributeDictionary()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsAttributeDictionary" /> class. </summary>
		/// <param name="attributes">A collection of key/value pairs that are related to configuration settings.</param>
		// Token: 0x06000BDC RID: 3036 RVA: 0x0003C4BD File Offset: 0x0003A6BD
		public SettingsAttributeDictionary(SettingsAttributeDictionary attributes)
			: base(attributes)
		{
		}
	}
}
