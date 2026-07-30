using System;
using System.Collections.Specialized;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Provides name/value-pair configuration information from a configuration section.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000183 RID: 387
	public class NameValueSectionHandler : IConfigurationSectionHandler
	{
		/// <summary>Creates a new configuration handler and adds it to the section-handler collection based on the specified parameters.</summary>
		/// <returns>A configuration object.</returns>
		/// <param name="parent">Parent object.</param>
		/// <param name="context">Configuration context object.</param>
		/// <param name="section">Section XML node.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BA6 RID: 2982 RVA: 0x0003BF30 File Offset: 0x0003A130
		public object Create(object parent, object context, XmlNode section)
		{
			return ConfigHelper.GetNameValueCollection(parent as NameValueCollection, section, this.KeyAttributeName, this.ValueAttributeName);
		}

		/// <summary>Gets the XML attribute name to use as the key in a key/value pair.</summary>
		/// <returns>A <see cref="T:System.String" /> value containing the name of the key attribute.</returns>
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x0003BC6B File Offset: 0x00039E6B
		protected virtual string KeyAttributeName
		{
			get
			{
				return "key";
			}
		}

		/// <summary>Gets the XML attribute name to use as the value in a key/value pair.</summary>
		/// <returns>A <see cref="T:System.String" /> value containing the name of the value attribute.</returns>
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x0003BC72 File Offset: 0x00039E72
		protected virtual string ValueAttributeName
		{
			get
			{
				return "value";
			}
		}
	}
}
