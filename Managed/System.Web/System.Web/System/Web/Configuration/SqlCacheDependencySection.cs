using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the SQL cache dependencies for an ASP.NET application. This class cannot be inherited. </summary>
	// Token: 0x020005DC RID: 1500
	public sealed class SqlCacheDependencySection : ConfigurationSection
	{
		// Token: 0x060040E4 RID: 16612 RVA: 0x000AAA68 File Offset: 0x000A8C68
		static SqlCacheDependencySection()
		{
			SqlCacheDependencySection.properties.Add(SqlCacheDependencySection.databasesProp);
			SqlCacheDependencySection.properties.Add(SqlCacheDependencySection.enabledProp);
			SqlCacheDependencySection.properties.Add(SqlCacheDependencySection.pollTimeProp);
			SqlCacheDependencySection.elementProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(SqlCacheDependencySection), new ValidatorCallback(SqlCacheDependencySection.ValidateElement)));
		}

		// Token: 0x060040E5 RID: 16613 RVA: 0x0000393A File Offset: 0x00001B3A
		private static void ValidateElement(object o)
		{
		}

		// Token: 0x17001488 RID: 5256
		// (get) Token: 0x060040E6 RID: 16614 RVA: 0x000AAB30 File Offset: 0x000A8D30
		protected internal override ConfigurationElementProperty ElementProperty
		{
			get
			{
				return SqlCacheDependencySection.elementProperty;
			}
		}

		// Token: 0x060040E7 RID: 16615 RVA: 0x0009FE7D File Offset: 0x0009E07D
		protected override void PostDeserialize()
		{
			base.PostDeserialize();
		}

		/// <summary>Gets the collection of <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> objects stored within the <see cref="T:System.Web.Configuration.SqlCacheDependencySection" />.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> of <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> objects</returns>
		// Token: 0x17001489 RID: 5257
		// (get) Token: 0x060040E8 RID: 16616 RVA: 0x000AAB37 File Offset: 0x000A8D37
		[ConfigurationProperty("databases")]
		public SqlCacheDependencyDatabaseCollection Databases
		{
			get
			{
				return (SqlCacheDependencyDatabaseCollection)base[SqlCacheDependencySection.databasesProp];
			}
		}

		/// <summary>Gets or sets a value indicating whether the database table should be monitored for changes.</summary>
		/// <returns>true if SQL cache monitoring is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x1700148A RID: 5258
		// (get) Token: 0x060040E9 RID: 16617 RVA: 0x000AAB49 File Offset: 0x000A8D49
		// (set) Token: 0x060040EA RID: 16618 RVA: 0x000AAB5B File Offset: 0x000A8D5B
		[ConfigurationProperty("enabled", DefaultValue = "True")]
		public bool Enabled
		{
			get
			{
				return (bool)base[SqlCacheDependencySection.enabledProp];
			}
			set
			{
				base[SqlCacheDependencySection.enabledProp] = value;
			}
		}

		/// <summary>Gets or sets the frequency with which the <see cref="T:System.Web.Caching.SqlCacheDependency" /> polls the database table for changes.</summary>
		/// <returns>The SQL cache dependency polling time, in milliseconds. The default is 500.</returns>
		// Token: 0x1700148B RID: 5259
		// (get) Token: 0x060040EB RID: 16619 RVA: 0x000AAB6E File Offset: 0x000A8D6E
		// (set) Token: 0x060040EC RID: 16620 RVA: 0x000AAB80 File Offset: 0x000A8D80
		[ConfigurationProperty("pollTime", DefaultValue = "60000")]
		public int PollTime
		{
			get
			{
				return (int)base[SqlCacheDependencySection.pollTimeProp];
			}
			set
			{
				base[SqlCacheDependencySection.pollTimeProp] = value;
			}
		}

		// Token: 0x1700148C RID: 5260
		// (get) Token: 0x060040ED RID: 16621 RVA: 0x000AAB93 File Offset: 0x000A8D93
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SqlCacheDependencySection.properties;
			}
		}

		// Token: 0x04002319 RID: 8985
		private static ConfigurationProperty databasesProp = new ConfigurationProperty("databases", typeof(SqlCacheDependencyDatabaseCollection), null, null, null, ConfigurationPropertyOptions.None);

		// Token: 0x0400231A RID: 8986
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), true);

		// Token: 0x0400231B RID: 8987
		private static ConfigurationProperty pollTimeProp = new ConfigurationProperty("pollTime", typeof(int), 60000);

		// Token: 0x0400231C RID: 8988
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x0400231D RID: 8989
		private static ConfigurationElementProperty elementProperty;
	}
}
