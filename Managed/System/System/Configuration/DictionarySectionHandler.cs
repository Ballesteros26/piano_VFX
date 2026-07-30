using System;
using System.Collections;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Provides key/value pair configuration information from a configuration section.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000177 RID: 375
	public class DictionarySectionHandler : IConfigurationSectionHandler
	{
		/// <summary>Creates a new configuration handler and adds it to the section-handler collection based on the specified parameters.</summary>
		/// <returns>A configuration object.</returns>
		/// <param name="parent">Parent object.</param>
		/// <param name="context">Configuration context object.</param>
		/// <param name="section">Section XML node.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B77 RID: 2935 RVA: 0x0003BC51 File Offset: 0x00039E51
		public virtual object Create(object parent, object context, XmlNode section)
		{
			return ConfigHelper.GetDictionary(parent as IDictionary, section, this.KeyAttributeName, this.ValueAttributeName);
		}

		/// <summary>Gets the XML attribute name to use as the key in a key/value pair.</summary>
		/// <returns>A string value containing the name of the key attribute.</returns>
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x0003BC6B File Offset: 0x00039E6B
		protected virtual string KeyAttributeName
		{
			get
			{
				return "key";
			}
		}

		/// <summary>Gets the XML attribute name to use as the value in a key/value pair.</summary>
		/// <returns>A string value containing the name of the value attribute.</returns>
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x0003BC72 File Offset: 0x00039E72
		protected virtual string ValueAttributeName
		{
			get
			{
				return "value";
			}
		}
	}
}
