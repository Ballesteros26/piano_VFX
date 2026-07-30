using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Defines the ASP.NET event mapping settings for event providers. This class cannot be inherited.</summary>
	// Token: 0x0200059A RID: 1434
	public sealed class EventMappingSettings : ConfigurationElement
	{
		// Token: 0x06003CC2 RID: 15554 RVA: 0x000A1900 File Offset: 0x0009FB00
		static EventMappingSettings()
		{
			EventMappingSettings.properties.Add(EventMappingSettings.endEventCodeProp);
			EventMappingSettings.properties.Add(EventMappingSettings.nameProp);
			EventMappingSettings.properties.Add(EventMappingSettings.startEventCodeProp);
			EventMappingSettings.properties.Add(EventMappingSettings.typeProp);
		}

		// Token: 0x06003CC3 RID: 15555 RVA: 0x0009F629 File Offset: 0x0009D829
		internal EventMappingSettings()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> class using the specified name and type.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object being created.</param>
		/// <param name="type">The fully qualified type of the event class to use.</param>
		// Token: 0x06003CC4 RID: 15556 RVA: 0x000A1A11 File Offset: 0x0009FC11
		public EventMappingSettings(string name, string type)
		{
			this.Name = name;
			this.Type = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.BufferModeSettings" /> class using the specified values.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object being created.</param>
		/// <param name="type">The fully qualified type of the event class to use.</param>
		/// <param name="startEventCode">The starting event code range.</param>
		/// <param name="endEventCode">The ending event code range.</param>
		// Token: 0x06003CC5 RID: 15557 RVA: 0x000A1A27 File Offset: 0x0009FC27
		public EventMappingSettings(string name, string type, int startEventCode, int endEventCode)
		{
			this.Name = name;
			this.Type = type;
			this.StartEventCode = startEventCode;
			this.EndEventCode = endEventCode;
		}

		/// <summary>Gets or sets the ending event code of the range.</summary>
		/// <returns>The ending event code of the range. The default is <see cref="F:System.Int32.MaxValue" />.</returns>
		// Token: 0x170012B7 RID: 4791
		// (get) Token: 0x06003CC6 RID: 15558 RVA: 0x000A1A4C File Offset: 0x0009FC4C
		// (set) Token: 0x06003CC7 RID: 15559 RVA: 0x000A1A5E File Offset: 0x0009FC5E
		[ConfigurationProperty("endEventCode", DefaultValue = "2147483647")]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		public int EndEventCode
		{
			get
			{
				return (int)base[EventMappingSettings.endEventCodeProp];
			}
			set
			{
				base[EventMappingSettings.endEventCodeProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object.</summary>
		/// <returns>The name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object. The default is an empty string ("").</returns>
		// Token: 0x170012B8 RID: 4792
		// (get) Token: 0x06003CC8 RID: 15560 RVA: 0x000A1A71 File Offset: 0x0009FC71
		// (set) Token: 0x06003CC9 RID: 15561 RVA: 0x000A1A83 File Offset: 0x0009FC83
		[ConfigurationProperty("name", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Name
		{
			get
			{
				return (string)base[EventMappingSettings.nameProp];
			}
			set
			{
				base[EventMappingSettings.nameProp] = value;
			}
		}

		/// <summary>Gets or sets the starting event code of the range.</summary>
		/// <returns>The starting event code of the range. The default is 0.</returns>
		// Token: 0x170012B9 RID: 4793
		// (get) Token: 0x06003CCA RID: 15562 RVA: 0x000A1A91 File Offset: 0x0009FC91
		// (set) Token: 0x06003CCB RID: 15563 RVA: 0x000A1AA3 File Offset: 0x0009FCA3
		[ConfigurationProperty("startEventCode", DefaultValue = "0")]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		public int StartEventCode
		{
			get
			{
				return (int)base[EventMappingSettings.startEventCodeProp];
			}
			set
			{
				base[EventMappingSettings.startEventCodeProp] = value;
			}
		}

		/// <summary>Gets or sets a custom event type.</summary>
		/// <returns>A valid type reference or an empty string (""). The default is an empty string.</returns>
		// Token: 0x170012BA RID: 4794
		// (get) Token: 0x06003CCC RID: 15564 RVA: 0x000A1AB6 File Offset: 0x0009FCB6
		// (set) Token: 0x06003CCD RID: 15565 RVA: 0x000A1AC8 File Offset: 0x0009FCC8
		[ConfigurationProperty("type", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
		public string Type
		{
			get
			{
				return (string)base[EventMappingSettings.typeProp];
			}
			set
			{
				base[EventMappingSettings.typeProp] = value;
			}
		}

		// Token: 0x170012BB RID: 4795
		// (get) Token: 0x06003CCE RID: 15566 RVA: 0x000A1AD6 File Offset: 0x0009FCD6
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return EventMappingSettings.properties;
			}
		}

		// Token: 0x040020DB RID: 8411
		private static ConfigurationProperty endEventCodeProp = new ConfigurationProperty("endEventCode", typeof(int), int.MaxValue, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020DC RID: 8412
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040020DD RID: 8413
		private static ConfigurationProperty startEventCodeProp = new ConfigurationProperty("startEventCode", typeof(int), 0, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020DE RID: 8414
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), "", ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040020DF RID: 8415
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
