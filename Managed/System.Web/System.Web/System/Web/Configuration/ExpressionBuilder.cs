using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Retrieves a dynamic resource during compilation.</summary>
	// Token: 0x0200059C RID: 1436
	public sealed class ExpressionBuilder : ConfigurationElement
	{
		// Token: 0x06003CDE RID: 15582 RVA: 0x000A1B60 File Offset: 0x0009FD60
		static ExpressionBuilder()
		{
			ExpressionBuilder.properties.Add(ExpressionBuilder.expressionPrefixProp);
			ExpressionBuilder.properties.Add(ExpressionBuilder.typeProp);
		}

		// Token: 0x06003CDF RID: 15583 RVA: 0x0009F629 File Offset: 0x0009D829
		internal ExpressionBuilder()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ExpressionBuilder" /> class. </summary>
		/// <param name="expressionPrefix">A string that identifies the type of expression to retrieve.</param>
		/// <param name="theType">A string that specifies the expression type.</param>
		// Token: 0x06003CE0 RID: 15584 RVA: 0x000A1BFB File Offset: 0x0009FDFB
		public ExpressionBuilder(string expressionPrefix, string theType)
		{
			this.ExpressionPrefix = expressionPrefix;
			this.Type = theType;
		}

		/// <summary>Gets or sets a string that identifies the type of expression to retrieve.</summary>
		/// <returns>A string that identifies the type of expression to retrieve.</returns>
		// Token: 0x170012BF RID: 4799
		// (get) Token: 0x06003CE1 RID: 15585 RVA: 0x000A1C11 File Offset: 0x0009FE11
		// (set) Token: 0x06003CE2 RID: 15586 RVA: 0x000A1C23 File Offset: 0x0009FE23
		[ConfigurationProperty("expressionPrefix", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		[StringValidator(MinLength = 1)]
		public string ExpressionPrefix
		{
			get
			{
				return (string)base[ExpressionBuilder.expressionPrefixProp];
			}
			set
			{
				base[ExpressionBuilder.expressionPrefixProp] = value;
			}
		}

		/// <summary>Gets or sets a string that specifies the expression type.</summary>
		/// <returns>A string that specifies the expression type.</returns>
		// Token: 0x170012C0 RID: 4800
		// (get) Token: 0x06003CE3 RID: 15587 RVA: 0x000A1C31 File Offset: 0x0009FE31
		// (set) Token: 0x06003CE4 RID: 15588 RVA: 0x000A1C43 File Offset: 0x0009FE43
		[ConfigurationProperty("type", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
		[StringValidator(MinLength = 1)]
		public string Type
		{
			get
			{
				return (string)base[ExpressionBuilder.typeProp];
			}
			set
			{
				base[ExpressionBuilder.typeProp] = value;
			}
		}

		// Token: 0x170012C1 RID: 4801
		// (get) Token: 0x06003CE5 RID: 15589 RVA: 0x000A1C51 File Offset: 0x0009FE51
		internal Type TypeInternal
		{
			get
			{
				return global::System.Type.GetType(this.Type, true);
			}
		}

		// Token: 0x170012C2 RID: 4802
		// (get) Token: 0x06003CE6 RID: 15590 RVA: 0x000A1C5F File Offset: 0x0009FE5F
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ExpressionBuilder.properties;
			}
		}

		// Token: 0x040020E1 RID: 8417
		private static ConfigurationProperty expressionPrefixProp = new ConfigurationProperty("expressionPrefix", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040020E2 RID: 8418
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040020E3 RID: 8419
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
