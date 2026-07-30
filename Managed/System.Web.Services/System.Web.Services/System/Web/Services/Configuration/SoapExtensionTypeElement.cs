using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Services.Configuration
{
	/// <summary>Represents the SoapExtensionElement in the Web Services configuration file. This element adds a SOAP extension to run with all XML Web services within the scope of the configuration file. The class cannot be inherited.</summary>
	// Token: 0x02000143 RID: 323
	public sealed class SoapExtensionTypeElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> class.</summary>
		// Token: 0x060009D6 RID: 2518 RVA: 0x000439C0 File Offset: 0x00041BC0
		public SoapExtensionTypeElement()
		{
			this.properties.Add(this.group);
			this.properties.Add(this.priority);
			this.properties.Add(this.type);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> class.</summary>
		/// <param name="type">Specifies the SOAP extension class to add.</param>
		/// <param name="priority">Indicates the relative order in which a SOAP extension runs when multiple SOAP extensions are specified. Within each group, the priority attribute distinguishes the overall relative priority of the SOAP extension. A lower priority number indicates a higher priority for the SOAP extension. The lowest possible value for the priority attribute is 1.</param>
		/// <param name="group">Along with priority, specifies the relative order in which a SOAP extension runs when multiple SOAP extensions are configured to run.</param>
		// Token: 0x060009D7 RID: 2519 RVA: 0x00043A91 File Offset: 0x00041C91
		public SoapExtensionTypeElement(string type, int priority, PriorityGroup group)
			: this()
		{
			this.Type = Type.GetType(type, true, true);
			this.Priority = priority;
			this.Group = group;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> class.</summary>
		/// <param name="type">Specifies the SOAP extension class to add.</param>
		/// <param name="priority">Indicates the relative order in which a SOAP extension runs when multiple SOAP extensions are specified.</param>
		/// <param name="group">Along with priority, specifies the relative order in which a SOAP extension runs when multiple SOAP extensions are configured to run.</param>
		// Token: 0x060009D8 RID: 2520 RVA: 0x00043AB5 File Offset: 0x00041CB5
		public SoapExtensionTypeElement(Type type, int priority, PriorityGroup group)
			: this(type.AssemblyQualifiedName, priority, group)
		{
		}

		/// <summary>Gets or sets the relative order in which a SOAP extension runs when multiple SOAP extensions are configured to run.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Configuration.PriorityGroup" /> object whose value determines relative order in which a SOAP extension runs.</returns>
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x00043AC5 File Offset: 0x00041CC5
		// (set) Token: 0x060009DA RID: 2522 RVA: 0x00043AD8 File Offset: 0x00041CD8
		[ConfigurationProperty("group", IsKey = true, DefaultValue = PriorityGroup.Low)]
		public PriorityGroup Group
		{
			get
			{
				return (PriorityGroup)base[this.group];
			}
			set
			{
				if (Enum.IsDefined(typeof(PriorityGroup), value))
				{
					base[this.group] = value;
					return;
				}
				throw new ArgumentException(Res.GetString("Invalid_priority_group_value"), "value");
			}
		}

		/// <summary>Gets or sets the value that indicates the relative order in which a SOAP extension runs when multiple SOAP extensions are specified.</summary>
		/// <returns>A <see cref="T:System.Int32" /> whose value determines relative order in which a SOAP extension runs.</returns>
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x00043B18 File Offset: 0x00041D18
		// (set) Token: 0x060009DC RID: 2524 RVA: 0x00043B2B File Offset: 0x00041D2B
		[ConfigurationProperty("priority", IsKey = true, DefaultValue = 0)]
		[IntegerValidator(MinValue = 0)]
		public int Priority
		{
			get
			{
				return (int)base[this.priority];
			}
			set
			{
				base[this.priority] = value;
			}
		}

		/// <summary>Gets or sets the SOAP extension class to add to the SoapExtensionType element of the Web Services configuration file.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the type name of the SoapExtensionType element.</returns>
		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x00043B3F File Offset: 0x00041D3F
		// (set) Token: 0x060009DE RID: 2526 RVA: 0x00043B52 File Offset: 0x00041D52
		[ConfigurationProperty("type", IsKey = true)]
		[TypeConverter(typeof(TypeTypeConverter))]
		public Type Type
		{
			get
			{
				return (Type)base[this.type];
			}
			set
			{
				base[this.type] = value;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x00043B61 File Offset: 0x00041D61
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x040005AB RID: 1451
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040005AC RID: 1452
		private readonly ConfigurationProperty group = new ConfigurationProperty("group", typeof(PriorityGroup), PriorityGroup.Low, new EnumConverter(typeof(PriorityGroup)), null, ConfigurationPropertyOptions.IsKey);

		// Token: 0x040005AD RID: 1453
		private readonly ConfigurationProperty priority = new ConfigurationProperty("priority", typeof(int), 0, null, new IntegerValidator(0, int.MaxValue), ConfigurationPropertyOptions.IsKey);

		// Token: 0x040005AE RID: 1454
		private readonly ConfigurationProperty type = new ConfigurationProperty("type", typeof(Type), null, new TypeTypeConverter(), null, ConfigurationPropertyOptions.IsKey);
	}
}
