using System;
using System.ComponentModel;
using Unity;

namespace System.Configuration
{
	/// <summary>Contains meta-information on an individual property within the configuration. This type cannot be inherited.</summary>
	// Token: 0x02000058 RID: 88
	public sealed class PropertyInformation
	{
		// Token: 0x060002E4 RID: 740 RVA: 0x00008841 File Offset: 0x00006A41
		internal PropertyInformation(ConfigurationElement owner, ConfigurationProperty property)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			if (property == null)
			{
				throw new ArgumentNullException("property");
			}
			this.owner = owner;
			this.property = property;
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.TypeConverter" /> object related to the configuration attribute.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> object.</returns>
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00008873 File Offset: 0x00006A73
		public TypeConverter Converter
		{
			get
			{
				return this.property.Converter;
			}
		}

		/// <summary>Gets an object containing the default value related to a configuration attribute.</summary>
		/// <returns>An object containing the default value of the configuration attribute.</returns>
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00008880 File Offset: 0x00006A80
		public object DefaultValue
		{
			get
			{
				return this.property.DefaultValue;
			}
		}

		/// <summary>Gets the description of the object that corresponds to a configuration attribute.</summary>
		/// <returns>The description of the configuration attribute.</returns>
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000888D File Offset: 0x00006A8D
		public string Description
		{
			get
			{
				return this.property.Description;
			}
		}

		/// <summary>Gets a value specifying whether the configuration attribute is a key.</summary>
		/// <returns>true if the configuration attribute is a key; otherwise, false.</returns>
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000889A File Offset: 0x00006A9A
		public bool IsKey
		{
			get
			{
				return this.property.IsKey;
			}
		}

		/// <summary>Gets a value specifying whether the configuration attribute is locked.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.PropertyInformation" /> object is locked; otherwise, false.</returns>
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x000088A7 File Offset: 0x00006AA7
		// (set) Token: 0x060002EA RID: 746 RVA: 0x000088AF File Offset: 0x00006AAF
		[MonoTODO]
		public bool IsLocked
		{
			get
			{
				return this.isLocked;
			}
			internal set
			{
				this.isLocked = value;
			}
		}

		/// <summary>Gets a value specifying whether the configuration attribute has been modified.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.PropertyInformation" /> object has been modified; otherwise, false.</returns>
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002EB RID: 747 RVA: 0x000088B8 File Offset: 0x00006AB8
		// (set) Token: 0x060002EC RID: 748 RVA: 0x000088C0 File Offset: 0x00006AC0
		public bool IsModified
		{
			get
			{
				return this.isModified;
			}
			internal set
			{
				this.isModified = value;
			}
		}

		/// <summary>Gets a value specifying whether the configuration attribute is required.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.PropertyInformation" /> object is required; otherwise, false.</returns>
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002ED RID: 749 RVA: 0x000088C9 File Offset: 0x00006AC9
		public bool IsRequired
		{
			get
			{
				return this.property.IsRequired;
			}
		}

		/// <summary>Gets the line number in the configuration file related to the configuration attribute.</summary>
		/// <returns>A line number of the configuration file.</returns>
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002EE RID: 750 RVA: 0x000088D6 File Offset: 0x00006AD6
		// (set) Token: 0x060002EF RID: 751 RVA: 0x000088DE File Offset: 0x00006ADE
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
			internal set
			{
				this.lineNumber = value;
			}
		}

		/// <summary>Gets the name of the object that corresponds to a configuration attribute.</summary>
		/// <returns>The name of the <see cref="T:System.Configuration.PropertyInformation" /> object.</returns>
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x000088E7 File Offset: 0x00006AE7
		public string Name
		{
			get
			{
				return this.property.Name;
			}
		}

		/// <summary>Gets the source file that corresponds to a configuration attribute.</summary>
		/// <returns>The source file of the <see cref="T:System.Configuration.PropertyInformation" /> object.</returns>
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x000088F4 File Offset: 0x00006AF4
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x000088FC File Offset: 0x00006AFC
		public string Source
		{
			get
			{
				return this.source;
			}
			internal set
			{
				this.source = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the object that corresponds to a configuration attribute.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the <see cref="T:System.Configuration.PropertyInformation" /> object.</returns>
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00008905 File Offset: 0x00006B05
		public Type Type
		{
			get
			{
				return this.property.Type;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ConfigurationValidatorBase" /> object related to the configuration attribute.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationValidatorBase" /> object.</returns>
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00008912 File Offset: 0x00006B12
		public ConfigurationValidatorBase Validator
		{
			get
			{
				return this.property.Validator;
			}
		}

		/// <summary>Gets or sets an object containing the value related to a configuration attribute.</summary>
		/// <returns>An object containing the value for the <see cref="T:System.Configuration.PropertyInformation" /> object.</returns>
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00008920 File Offset: 0x00006B20
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x00008993 File Offset: 0x00006B93
		public object Value
		{
			get
			{
				if (this.origin == PropertyValueOrigin.Default)
				{
					if (!this.property.IsElement)
					{
						return this.DefaultValue;
					}
					ConfigurationElement configurationElement = (ConfigurationElement)Activator.CreateInstance(this.Type, true);
					configurationElement.InitFromProperty(this);
					if (this.owner != null && this.owner.IsReadOnly())
					{
						configurationElement.SetReadOnly();
					}
					this.val = configurationElement;
					this.origin = PropertyValueOrigin.Inherited;
				}
				return this.val;
			}
			set
			{
				this.val = value;
				this.isModified = true;
				this.origin = PropertyValueOrigin.SetHere;
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x000089AC File Offset: 0x00006BAC
		internal void Reset(PropertyInformation parentProperty)
		{
			if (parentProperty == null)
			{
				this.origin = PropertyValueOrigin.Default;
				return;
			}
			if (this.property.IsElement)
			{
				((ConfigurationElement)this.Value).Reset((ConfigurationElement)parentProperty.Value);
				return;
			}
			this.val = parentProperty.Value;
			this.origin = PropertyValueOrigin.Inherited;
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00008A00 File Offset: 0x00006C00
		internal bool IsElement
		{
			get
			{
				return this.property.IsElement;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.PropertyValueOrigin" /> object related to the configuration attribute. </summary>
		/// <returns>A <see cref="T:System.Configuration.PropertyValueOrigin" /> object.</returns>
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00008A0D File Offset: 0x00006C0D
		public PropertyValueOrigin ValueOrigin
		{
			get
			{
				return this.origin;
			}
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00008A15 File Offset: 0x00006C15
		internal string GetStringValue()
		{
			return this.property.ConvertToString(this.Value);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00008A28 File Offset: 0x00006C28
		internal void SetStringValue(string value)
		{
			this.val = this.property.ConvertFromString(value);
			if (!object.Equals(this.val, this.DefaultValue))
			{
				this.origin = PropertyValueOrigin.SetHere;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002FC RID: 764 RVA: 0x00008A56 File Offset: 0x00006C56
		internal ConfigurationProperty Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00003524 File Offset: 0x00001724
		internal PropertyInformation()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000110 RID: 272
		private bool isLocked;

		// Token: 0x04000111 RID: 273
		private bool isModified;

		// Token: 0x04000112 RID: 274
		private int lineNumber;

		// Token: 0x04000113 RID: 275
		private string source;

		// Token: 0x04000114 RID: 276
		private object val;

		// Token: 0x04000115 RID: 277
		private PropertyValueOrigin origin;

		// Token: 0x04000116 RID: 278
		private readonly ConfigurationElement owner;

		// Token: 0x04000117 RID: 279
		private readonly ConfigurationProperty property;
	}
}
