using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures an HTTP handler for a Web application. This class cannot be inherited.</summary>
	// Token: 0x020005AE RID: 1454
	public sealed class HttpHandlersSection : ConfigurationSection
	{
		// Token: 0x06003E3C RID: 15932 RVA: 0x000A4F79 File Offset: 0x000A3179
		static HttpHandlersSection()
		{
			HttpHandlersSection.properties.Add(HttpHandlersSection.handlersProp);
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.HttpHandlerActionCollection" /> collection of <see cref="T:System.Web.Configuration.HttpHandlerAction" /> objects contained by the <see cref="T:System.Web.Configuration.HttpHandlersSection" /> object.</summary>
		/// <returns>An <see cref="T:System.Web.Configuration.HttpHandlerActionCollection" /> that contains the <see cref="T:System.Web.Configuration.HttpHandlerAction" /> objects, or handlers.</returns>
		// Token: 0x17001381 RID: 4993
		// (get) Token: 0x06003E3E RID: 15934 RVA: 0x000A4FB5 File Offset: 0x000A31B5
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public HttpHandlerActionCollection Handlers
		{
			get
			{
				return (HttpHandlerActionCollection)base[HttpHandlersSection.handlersProp];
			}
		}

		// Token: 0x17001382 RID: 4994
		// (get) Token: 0x06003E3F RID: 15935 RVA: 0x000A4FC7 File Offset: 0x000A31C7
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpHandlersSection.properties;
			}
		}

		// Token: 0x06003E40 RID: 15936 RVA: 0x000A4FD0 File Offset: 0x000A31D0
		internal object LocateHandler(string verb, string filepath, out bool allowCache)
		{
			int count = this.Handlers.Count;
			for (int i = 0; i < count; i++)
			{
				HttpHandlerAction httpHandlerAction = this.Handlers[i];
				string[] verbs = httpHandlerAction.Verbs;
				if (verbs == null)
				{
					if (httpHandlerAction.PathMatches(filepath))
					{
						allowCache = httpHandlerAction.Path != "*";
						return httpHandlerAction.GetHandlerInstance();
					}
				}
				else
				{
					int j = verbs.Length;
					while (j > 0)
					{
						j--;
						if (!(verbs[j] != verb) && httpHandlerAction.PathMatches(filepath))
						{
							allowCache = httpHandlerAction.Path != "*";
							return httpHandlerAction.GetHandlerInstance();
						}
					}
				}
			}
			allowCache = false;
			return null;
		}

		// Token: 0x04002213 RID: 8723
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002214 RID: 8724
		private static ConfigurationProperty handlersProp = new ConfigurationProperty("", typeof(HttpHandlerActionCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
