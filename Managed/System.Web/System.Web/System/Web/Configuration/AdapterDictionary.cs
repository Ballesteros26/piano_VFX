using System;
using System.Collections.Specialized;

namespace System.Web.Configuration
{
	/// <summary>Used internally at run time by the configuration system to contain the names of the available adapters used to render server controls on different browsers. </summary>
	// Token: 0x0200055B RID: 1371
	[Serializable]
	public class AdapterDictionary : OrderedDictionary
	{
		/// <summary>Used internally at run time by the configuration system to get or set a specified adapter name.</summary>
		/// <returns>The name of the specified adapter.</returns>
		/// <param name="key">Key of the specified adapter.</param>
		// Token: 0x17001231 RID: 4657
		public string this[string key]
		{
			get
			{
				return (string)base[key];
			}
			set
			{
				base[key] = value;
			}
		}
	}
}
