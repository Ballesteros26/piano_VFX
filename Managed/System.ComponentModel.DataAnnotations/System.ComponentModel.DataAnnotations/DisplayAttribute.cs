using System;
using System.Globalization;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Provides a general-purpose attribute that lets you specify localizable strings for types and members of entity partial classes.</summary>
	// Token: 0x02000011 RID: 17
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public sealed class DisplayAttribute : Attribute
	{
		/// <summary>Gets or sets a value that is used for the grid column label.</summary>
		/// <returns>A value that is for the grid column label.</returns>
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002D03 File Offset: 0x00000F03
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002D10 File Offset: 0x00000F10
		public string ShortName
		{
			get
			{
				return this._shortName.Value;
			}
			set
			{
				if (this._shortName.Value != value)
				{
					this._shortName.Value = value;
				}
			}
		}

		/// <summary>Gets or sets a value that is used for display in the UI.</summary>
		/// <returns>A value that is used for display in the UI.</returns>
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002D31 File Offset: 0x00000F31
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002D3E File Offset: 0x00000F3E
		public string Name
		{
			get
			{
				return this._name.Value;
			}
			set
			{
				if (this._name.Value != value)
				{
					this._name.Value = value;
				}
			}
		}

		/// <summary>Gets or sets a value that is used to display a description in the UI.</summary>
		/// <returns>The value that is used to display a description in the UI.</returns>
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002D5F File Offset: 0x00000F5F
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002D6C File Offset: 0x00000F6C
		public string Description
		{
			get
			{
				return this._description.Value;
			}
			set
			{
				if (this._description.Value != value)
				{
					this._description.Value = value;
				}
			}
		}

		/// <summary>Gets or sets a value that will be used to set the watermark for prompts in the UI.</summary>
		/// <returns>A value that will be used to display a watermark in the UI.</returns>
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002D8D File Offset: 0x00000F8D
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002D9A File Offset: 0x00000F9A
		public string Prompt
		{
			get
			{
				return this._prompt.Value;
			}
			set
			{
				if (this._prompt.Value != value)
				{
					this._prompt.Value = value;
				}
			}
		}

		/// <summary>Gets or sets a value that is used to group fields in the UI.</summary>
		/// <returns>A value that is used to group fields in the UI.</returns>
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002DBB File Offset: 0x00000FBB
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002DC8 File Offset: 0x00000FC8
		public string GroupName
		{
			get
			{
				return this._groupName.Value;
			}
			set
			{
				if (this._groupName.Value != value)
				{
					this._groupName.Value = value;
				}
			}
		}

		/// <summary>Gets or sets the type that contains the resources for the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.ShortName" />, <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Name" />, <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Prompt" />, and <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Description" /> properties.</summary>
		/// <returns>The type of the resource that contains the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.ShortName" />, <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Name" />, <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Prompt" />, and <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Description" /> properties.</returns>
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002DE9 File Offset: 0x00000FE9
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002DF4 File Offset: 0x00000FF4
		public Type ResourceType
		{
			get
			{
				return this._resourceType;
			}
			set
			{
				if (this._resourceType != value)
				{
					this._resourceType = value;
					this._shortName.ResourceType = value;
					this._name.ResourceType = value;
					this._description.ResourceType = value;
					this._prompt.ResourceType = value;
					this._groupName.ResourceType = value;
				}
			}
		}

		/// <summary>Gets or sets a value that indicates whether UI should be generated automatically in order to display this field.</summary>
		/// <returns>true if UI should be generated automatically to display this field; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to get the property value before it was set.</exception>
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002E52 File Offset: 0x00001052
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002E8B File Offset: 0x0000108B
		public bool AutoGenerateField
		{
			get
			{
				if (this._autoGenerateField == null)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The {0} property has not been set.  Use the {1} method to get the value.", "AutoGenerateField", "GetAutoGenerateField"));
				}
				return this._autoGenerateField.Value;
			}
			set
			{
				this._autoGenerateField = new bool?(value);
			}
		}

		/// <summary>Gets or sets a value that indicates whether filtering UI is automatically displayed for this field. </summary>
		/// <returns>true if UI should be generated automatically to display filtering for this field; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to get the property value before it was set.</exception>
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002E99 File Offset: 0x00001099
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002ED2 File Offset: 0x000010D2
		public bool AutoGenerateFilter
		{
			get
			{
				if (this._autoGenerateFilter == null)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The {0} property has not been set.  Use the {1} method to get the value.", "AutoGenerateFilter", "GetAutoGenerateFilter"));
				}
				return this._autoGenerateFilter.Value;
			}
			set
			{
				this._autoGenerateFilter = new bool?(value);
			}
		}

		/// <summary>Gets or sets the order weight of the column.</summary>
		/// <returns>The order weight of the column.</returns>
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002EE0 File Offset: 0x000010E0
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002F19 File Offset: 0x00001119
		public int Order
		{
			get
			{
				if (this._order == null)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The {0} property has not been set.  Use the {1} method to get the value.", "Order", "GetOrder"));
				}
				return this._order.Value;
			}
			set
			{
				this._order = new int?(value);
			}
		}

		/// <summary>Returns the value of the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.ShortName" /> property.</summary>
		/// <returns>The localized string for the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.ShortName" /> property if the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.ResourceType" /> property has been specified and if the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.ShortName" /> property represents a resource key; otherwise, the non-localized value of the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.ShortName" /> value property.</returns>
		// Token: 0x0600005E RID: 94 RVA: 0x00002F27 File Offset: 0x00001127
		public string GetShortName()
		{
			return this._shortName.GetLocalizableValue() ?? this.GetName();
		}

		/// <summary>Returns a value that is used for field display in the UI.</summary>
		/// <returns>The localized string for the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Name" /> property, if the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.ResourceType" /> property has been specified and the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Name" /> property represents a resource key; otherwise, the non-localized value of the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Name" /> property.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.ResourceType" /> property and the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Name" /> property are initialized, but a public static property that has a name that matches the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Name" /> value could not be found for the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.ResourceType" /> property.</exception>
		// Token: 0x0600005F RID: 95 RVA: 0x00002F3E File Offset: 0x0000113E
		public string GetName()
		{
			return this._name.GetLocalizableValue();
		}

		/// <summary>Returns the value of the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Description" /> property.</summary>
		/// <returns>The localized description, if the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.ResourceType" /> has been specified and the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Description" /> property represents a resource key; otherwise, the non-localized value of the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Description" /> property.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.ResourceType" /> property and the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Description" /> property are initialized, but a public static property that has a name that matches the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Description" /> value could not be found for the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.ResourceType" /> property.</exception>
		// Token: 0x06000060 RID: 96 RVA: 0x00002F4B File Offset: 0x0000114B
		public string GetDescription()
		{
			return this._description.GetLocalizableValue();
		}

		/// <summary>Returns the value of the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Prompt" /> property.</summary>
		/// <returns>Gets the localized string for the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Prompt" /> property if the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.ResourceType" /> property has been specified and if the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Prompt" /> property represents a resource key; otherwise, the non-localized value of the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Prompt" /> property.</returns>
		// Token: 0x06000061 RID: 97 RVA: 0x00002F58 File Offset: 0x00001158
		public string GetPrompt()
		{
			return this._prompt.GetLocalizableValue();
		}

		/// <summary>Returns the value of the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.GroupName" /> property.</summary>
		/// <returns>A value that will be used for grouping fields in the UI, if <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.GroupName" /> has been initialized; otherwise, null. If the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.ResourceType" /> property has been specified and the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.GroupName" /> property represents a resource key, a localized string is returned; otherwise, a non-localized string is returned.</returns>
		// Token: 0x06000062 RID: 98 RVA: 0x00002F65 File Offset: 0x00001165
		public string GetGroupName()
		{
			return this._groupName.GetLocalizableValue();
		}

		/// <summary>Returns the value of the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.AutoGenerateField" /> property.</summary>
		/// <returns>The value of <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.AutoGenerateField" /> if the property has been initialized; otherwise, null.</returns>
		// Token: 0x06000063 RID: 99 RVA: 0x00002F72 File Offset: 0x00001172
		public bool? GetAutoGenerateField()
		{
			return this._autoGenerateField;
		}

		/// <summary>Returns a value that indicates whether UI should be generated automatically in order to display filtering for this field. </summary>
		/// <returns>The value of <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.AutoGenerateFilter" /> if the property has been initialized; otherwise, null.</returns>
		// Token: 0x06000064 RID: 100 RVA: 0x00002F7A File Offset: 0x0000117A
		public bool? GetAutoGenerateFilter()
		{
			return this._autoGenerateFilter;
		}

		/// <summary>Returns the value of the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Order" /> property.</summary>
		/// <returns>The value of the <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Order" /> property, if it has been set; otherwise, null.</returns>
		// Token: 0x06000065 RID: 101 RVA: 0x00002F82 File Offset: 0x00001182
		public int? GetOrder()
		{
			return this._order;
		}

		// Token: 0x0400005D RID: 93
		private Type _resourceType;

		// Token: 0x0400005E RID: 94
		private LocalizableString _shortName = new LocalizableString("ShortName");

		// Token: 0x0400005F RID: 95
		private LocalizableString _name = new LocalizableString("Name");

		// Token: 0x04000060 RID: 96
		private LocalizableString _description = new LocalizableString("Description");

		// Token: 0x04000061 RID: 97
		private LocalizableString _prompt = new LocalizableString("Prompt");

		// Token: 0x04000062 RID: 98
		private LocalizableString _groupName = new LocalizableString("GroupName");

		// Token: 0x04000063 RID: 99
		private bool? _autoGenerateField;

		// Token: 0x04000064 RID: 100
		private bool? _autoGenerateFilter;

		// Token: 0x04000065 RID: 101
		private int? _order;
	}
}
