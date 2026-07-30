using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the SQL cache dependencies databases for an ASP.NET application. This class cannot be inherited. </summary>
	// Token: 0x020005DA RID: 1498
	public sealed class SqlCacheDependencyDatabase : ConfigurationElement
	{
		// Token: 0x060040C8 RID: 16584 RVA: 0x000AA810 File Offset: 0x000A8A10
		static SqlCacheDependencyDatabase()
		{
			SqlCacheDependencyDatabase.properties.Add(SqlCacheDependencyDatabase.connectionStringNameProp);
			SqlCacheDependencyDatabase.properties.Add(SqlCacheDependencyDatabase.nameProp);
			SqlCacheDependencyDatabase.properties.Add(SqlCacheDependencyDatabase.pollTimeProp);
			SqlCacheDependencyDatabase.elementProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(SqlCacheDependencyDatabase), new ValidatorCallback(SqlCacheDependencyDatabase.ValidateElement)));
		}

		// Token: 0x060040C9 RID: 16585 RVA: 0x0009F629 File Offset: 0x0009D829
		internal SqlCacheDependencyDatabase()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> class with the passed parameters.</summary>
		/// <param name="name">A string that specifies the name used by <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> to identify the database.</param>
		/// <param name="connectionStringName">A string that specifies the name of the connection string in the connectionStrings section to use to connect to this database.</param>
		// Token: 0x060040CA RID: 16586 RVA: 0x000AA8FA File Offset: 0x000A8AFA
		public SqlCacheDependencyDatabase(string name, string connectionStringName)
		{
			this.Name = name;
			this.ConnectionStringName = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> class. </summary>
		/// <param name="name">A string that specifies the name used by <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> to identify the database.</param>
		/// <param name="connectionStringName">A string that specifies the name of the connection string in the connectionStrings section to use to connect to this database.</param>
		/// <param name="pollTime">The database polling time, in milliseconds. </param>
		// Token: 0x060040CB RID: 16587 RVA: 0x000AA910 File Offset: 0x000A8B10
		public SqlCacheDependencyDatabase(string name, string connectionStringName, int pollTime)
		{
			this.Name = name;
			this.ConnectionStringName = name;
			this.PollTime = pollTime;
		}

		// Token: 0x060040CC RID: 16588 RVA: 0x0000393A File Offset: 0x00001B3A
		private static void ValidateElement(object o)
		{
		}

		// Token: 0x17001480 RID: 5248
		// (get) Token: 0x060040CD RID: 16589 RVA: 0x000AA92D File Offset: 0x000A8B2D
		protected internal override ConfigurationElementProperty ElementProperty
		{
			get
			{
				return SqlCacheDependencyDatabase.elementProperty;
			}
		}

		/// <summary>Gets or sets the connection name for the database.</summary>
		/// <returns>A string that specifies the name of a database connection string within the connectionStrings configuration section.</returns>
		// Token: 0x17001481 RID: 5249
		// (get) Token: 0x060040CE RID: 16590 RVA: 0x000AA934 File Offset: 0x000A8B34
		// (set) Token: 0x060040CF RID: 16591 RVA: 0x000AA946 File Offset: 0x000A8B46
		[ConfigurationProperty("connectionStringName", Options = ConfigurationPropertyOptions.IsRequired)]
		[StringValidator(MinLength = 1)]
		public string ConnectionStringName
		{
			get
			{
				return (string)base[SqlCacheDependencyDatabase.connectionStringNameProp];
			}
			set
			{
				base[SqlCacheDependencyDatabase.connectionStringNameProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the database. </summary>
		/// <returns>A string that specifies the name used by <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> to identify the database.</returns>
		// Token: 0x17001482 RID: 5250
		// (get) Token: 0x060040D0 RID: 16592 RVA: 0x000AA954 File Offset: 0x000A8B54
		// (set) Token: 0x060040D1 RID: 16593 RVA: 0x000AA966 File Offset: 0x000A8B66
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("name", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Name
		{
			get
			{
				return (string)base[SqlCacheDependencyDatabase.nameProp];
			}
			set
			{
				base[SqlCacheDependencyDatabase.nameProp] = value;
			}
		}

		/// <summary>Gets or sets the frequency with which the <see cref="T:System.Web.Caching.SqlCacheDependency" /> polls the database table for changes.</summary>
		/// <returns>The database polling time, in milliseconds. </returns>
		// Token: 0x17001483 RID: 5251
		// (get) Token: 0x060040D2 RID: 16594 RVA: 0x000AA974 File Offset: 0x000A8B74
		// (set) Token: 0x060040D3 RID: 16595 RVA: 0x000AA986 File Offset: 0x000A8B86
		[ConfigurationProperty("pollTime", DefaultValue = "60000")]
		public int PollTime
		{
			get
			{
				return (int)base[SqlCacheDependencyDatabase.pollTimeProp];
			}
			set
			{
				base[SqlCacheDependencyDatabase.pollTimeProp] = value;
			}
		}

		// Token: 0x17001484 RID: 5252
		// (get) Token: 0x060040D4 RID: 16596 RVA: 0x000AA999 File Offset: 0x000A8B99
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SqlCacheDependencyDatabase.properties;
			}
		}

		// Token: 0x04002314 RID: 8980
		private static ConfigurationProperty connectionStringNameProp = new ConfigurationProperty("connectionStringName", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04002315 RID: 8981
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002316 RID: 8982
		private static ConfigurationProperty pollTimeProp = new ConfigurationProperty("pollTime", typeof(int), 60000);

		// Token: 0x04002317 RID: 8983
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002318 RID: 8984
		private static ConfigurationElementProperty elementProperty;
	}
}
