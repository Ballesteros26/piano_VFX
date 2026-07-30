using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Specifies a custom class that extends the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> class for use by Web Part connections.</summary>
	// Token: 0x020005E3 RID: 1507
	public sealed class TransformerInfo : ConfigurationElement
	{
		// Token: 0x0600415A RID: 16730 RVA: 0x000AB620 File Offset: 0x000A9820
		static TransformerInfo()
		{
			TransformerInfo.properties.Add(TransformerInfo.nameProp);
			TransformerInfo.properties.Add(TransformerInfo.typeProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.TransformerInfo" /> class with the specified name and type reference.</summary>
		/// <param name="name">The name of this transformer type.</param>
		/// <param name="type">A reference to a type that extends the transformer <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> class.</param>
		// Token: 0x0600415B RID: 16731 RVA: 0x000AB6BB File Offset: 0x000A98BB
		public TransformerInfo(string name, string type)
		{
			this.Name = name;
			this.Type = type;
		}

		/// <summary>Compares the current <see cref="T:System.Web.Configuration.TransformerInfo" /> object to another <see cref="T:System.Web.Configuration.TransformerInfo" /> object.</summary>
		/// <returns>true if the passed object is equal to the current object; otherwise, false.</returns>
		/// <param name="o">The object to compare to the current object.</param>
		// Token: 0x0600415C RID: 16732 RVA: 0x000AB6D4 File Offset: 0x000A98D4
		public override bool Equals(object o)
		{
			TransformerInfo transformerInfo = o as TransformerInfo;
			return this.Name == transformerInfo.Name && this.Type == transformerInfo.Type;
		}

		/// <summary>Generates a hash code for the collection.</summary>
		/// <returns>Unique integer hash code for the current object.</returns>
		// Token: 0x0600415D RID: 16733 RVA: 0x000AB70E File Offset: 0x000A990E
		public override int GetHashCode()
		{
			return this.Name.GetHashCode() + this.Type.GetHashCode();
		}

		/// <summary>Gets or sets a friendly name for a type that that extends the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> class.</summary>
		/// <returns>A friendly name for a type that that extends the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> class.</returns>
		// Token: 0x170014CB RID: 5323
		// (get) Token: 0x0600415E RID: 16734 RVA: 0x000AB727 File Offset: 0x000A9927
		// (set) Token: 0x0600415F RID: 16735 RVA: 0x000AB739 File Offset: 0x000A9939
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("name", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Name
		{
			get
			{
				return (string)base[TransformerInfo.nameProp];
			}
			set
			{
				base[TransformerInfo.nameProp] = value;
			}
		}

		/// <summary>Gets or sets the type reference for a class that extends the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> class.</summary>
		/// <returns>A type reference for a class that extends the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> class.</returns>
		// Token: 0x170014CC RID: 5324
		// (get) Token: 0x06004160 RID: 16736 RVA: 0x000AB747 File Offset: 0x000A9947
		// (set) Token: 0x06004161 RID: 16737 RVA: 0x000AB759 File Offset: 0x000A9959
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("type", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
		public string Type
		{
			get
			{
				return (string)base[TransformerInfo.typeProp];
			}
			set
			{
				base[TransformerInfo.typeProp] = value;
			}
		}

		// Token: 0x170014CD RID: 5325
		// (get) Token: 0x06004162 RID: 16738 RVA: 0x000AB767 File Offset: 0x000A9967
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TransformerInfo.properties;
			}
		}

		// Token: 0x04002332 RID: 9010
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002333 RID: 9011
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04002334 RID: 9012
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
