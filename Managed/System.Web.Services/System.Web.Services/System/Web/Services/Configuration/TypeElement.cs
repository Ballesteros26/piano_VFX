using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Services.Configuration
{
	/// <summary>Represents the type element in the Web services configuration file.</summary>
	// Token: 0x02000146 RID: 326
	public sealed class TypeElement : ConfigurationElement
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Web.Services.Configuration.TypeElement" /> class.</summary>
		// Token: 0x060009F3 RID: 2547 RVA: 0x00043CCC File Offset: 0x00041ECC
		public TypeElement()
		{
			this.properties.Add(this.type);
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Web.Services.Configuration.TypeElement" /> class.</summary>
		/// <param name="type">The type of the configuration attribute.</param>
		// Token: 0x060009F4 RID: 2548 RVA: 0x00043D1D File Offset: 0x00041F1D
		public TypeElement(string type)
			: this()
		{
			base[this.type] = new TypeAndName(type);
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Web.Services.Configuration.TypeElement" /> class.</summary>
		/// <param name="type">The type of the configuration attribute.</param>
		// Token: 0x060009F5 RID: 2549 RVA: 0x00043D37 File Offset: 0x00041F37
		public TypeElement(Type type)
			: this(type.AssemblyQualifiedName)
		{
		}

		/// <summary>Gets or sets the type of the configuration attribute.</summary>
		/// <returns>The type of the configuration attribute.</returns>
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x00043D45 File Offset: 0x00041F45
		// (set) Token: 0x060009F7 RID: 2551 RVA: 0x00043D5D File Offset: 0x00041F5D
		[TypeConverter(typeof(TypeAndNameConverter))]
		[ConfigurationProperty("type", IsKey = true)]
		public Type Type
		{
			get
			{
				return ((TypeAndName)base[this.type]).type;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base[this.type] = new TypeAndName(value);
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x00043D85 File Offset: 0x00041F85
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x040005AF RID: 1455
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040005B0 RID: 1456
		private readonly ConfigurationProperty type = new ConfigurationProperty("type", typeof(TypeAndName), null, new TypeAndNameConverter(), null, ConfigurationPropertyOptions.IsKey);
	}
}
