using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the <see cref="T:System.Web.Configuration.HttpModulesSection" /> modules. This class cannot be inherited.</summary>
	// Token: 0x020005AF RID: 1455
	public sealed class HttpModuleAction : ConfigurationElement
	{
		// Token: 0x06003E41 RID: 15937 RVA: 0x000A5078 File Offset: 0x000A3278
		static HttpModuleAction()
		{
			HttpModuleAction.properties.Add(HttpModuleAction.nameProp);
			HttpModuleAction.properties.Add(HttpModuleAction.typeProp);
			HttpModuleAction.elementProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(HttpModuleAction), new ValidatorCallback(HttpModuleAction.ValidateElement)));
		}

		// Token: 0x06003E42 RID: 15938 RVA: 0x0009F629 File Offset: 0x0009D829
		internal HttpModuleAction()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.HttpModuleAction" /> class using the passed parameters.</summary>
		/// <param name="name">The module name.</param>
		/// <param name="type">A comma-separated list containing the module type name and the assembly information. </param>
		// Token: 0x06003E43 RID: 15939 RVA: 0x000A5120 File Offset: 0x000A3320
		public HttpModuleAction(string name, string type)
		{
			this.Name = name;
			this.Type = type;
		}

		// Token: 0x06003E44 RID: 15940 RVA: 0x0000393A File Offset: 0x00001B3A
		private static void ValidateElement(object o)
		{
		}

		// Token: 0x17001383 RID: 4995
		// (get) Token: 0x06003E45 RID: 15941 RVA: 0x000A5136 File Offset: 0x000A3336
		protected internal override ConfigurationElementProperty ElementProperty
		{
			get
			{
				return HttpModuleAction.elementProperty;
			}
		}

		/// <summary>Gets or sets the module name.</summary>
		/// <returns>The module name.</returns>
		// Token: 0x17001384 RID: 4996
		// (get) Token: 0x06003E46 RID: 15942 RVA: 0x000A513D File Offset: 0x000A333D
		// (set) Token: 0x06003E47 RID: 15943 RVA: 0x000A514F File Offset: 0x000A334F
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("name", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Name
		{
			get
			{
				return (string)base[HttpModuleAction.nameProp];
			}
			set
			{
				base[HttpModuleAction.nameProp] = value;
			}
		}

		/// <summary>Gets or sets the module type.</summary>
		/// <returns>A comma-separated list containing the module type name and the assembly information. </returns>
		// Token: 0x17001385 RID: 4997
		// (get) Token: 0x06003E48 RID: 15944 RVA: 0x000A515D File Offset: 0x000A335D
		// (set) Token: 0x06003E49 RID: 15945 RVA: 0x000A516F File Offset: 0x000A336F
		[ConfigurationProperty("type", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
		public string Type
		{
			get
			{
				return (string)base[HttpModuleAction.typeProp];
			}
			set
			{
				base[HttpModuleAction.typeProp] = value;
			}
		}

		// Token: 0x17001386 RID: 4998
		// (get) Token: 0x06003E4A RID: 15946 RVA: 0x000A517D File Offset: 0x000A337D
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpModuleAction.properties;
			}
		}

		// Token: 0x04002215 RID: 8725
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002216 RID: 8726
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002217 RID: 8727
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), "hoho", ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04002218 RID: 8728
		private static ConfigurationElementProperty elementProperty;
	}
}
