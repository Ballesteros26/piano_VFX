using System;
using System.Configuration;
using System.Reflection;
using System.Web.Security;

namespace System.Web.Configuration
{
	/// <summary>Configures an HTTP module for a Web application. This class cannot be inherited.</summary>
	// Token: 0x020005B1 RID: 1457
	public sealed class HttpModulesSection : ConfigurationSection
	{
		// Token: 0x06003E59 RID: 15961 RVA: 0x000A51D0 File Offset: 0x000A33D0
		static HttpModulesSection()
		{
			HttpModulesSection.properties.Add(HttpModulesSection.modulesProp);
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.HttpModuleActionCollection" /> of <see cref="T:System.Web.Configuration.HttpModuleAction" /> modules contained by the <see cref="T:System.Web.Configuration.HttpModulesSection" />.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.HttpModuleActionCollection" /> that contains the <see cref="T:System.Web.Configuration.HttpModuleAction" /> objects, or modules, defined by the <see cref="T:System.Web.Configuration.HttpModulesSection" />. </returns>
		// Token: 0x17001389 RID: 5001
		// (get) Token: 0x06003E5A RID: 15962 RVA: 0x000A520C File Offset: 0x000A340C
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public HttpModuleActionCollection Modules
		{
			get
			{
				return (HttpModuleActionCollection)base[HttpModulesSection.modulesProp];
			}
		}

		// Token: 0x1700138A RID: 5002
		// (get) Token: 0x06003E5B RID: 15963 RVA: 0x000A521E File Offset: 0x000A341E
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpModulesSection.properties;
			}
		}

		// Token: 0x06003E5C RID: 15964 RVA: 0x000A5228 File Offset: 0x000A3428
		internal HttpModuleCollection LoadModules(HttpApplication app)
		{
			HttpModuleCollection httpModuleCollection = new HttpModuleCollection();
			foreach (object obj in this.Modules)
			{
				HttpModuleAction httpModuleAction = (HttpModuleAction)obj;
				Type type = HttpApplication.LoadType(httpModuleAction.Type);
				if (!(type == null))
				{
					IHttpModule httpModule = (IHttpModule)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, null, null);
					httpModule.Init(app);
					httpModuleCollection.AddModule(httpModuleAction.Name, httpModule);
				}
			}
			IHttpModule httpModule2 = new DefaultAuthenticationModule();
			httpModule2.Init(app);
			httpModuleCollection.AddModule("DefaultAuthentication", httpModule2);
			return httpModuleCollection;
		}

		// Token: 0x0400221A RID: 8730
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x0400221B RID: 8731
		private static ConfigurationProperty modulesProp = new ConfigurationProperty("", typeof(HttpModuleActionCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
