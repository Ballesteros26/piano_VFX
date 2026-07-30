using System;
using System.ComponentModel;
using System.Configuration;
using System.Web.UI;
using System.Xml;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Provides programmatic access to the pages section of the configuration file. This class cannot be inherited.</summary>
	// Token: 0x020005C3 RID: 1475
	public sealed class PagesSection : ConfigurationSection
	{
		// Token: 0x06003F46 RID: 16198 RVA: 0x000A74E0 File Offset: 0x000A56E0
		static PagesSection()
		{
			PagesSection.properties.Add(PagesSection.asyncTimeoutProp);
			PagesSection.properties.Add(PagesSection.autoEventWireupProp);
			PagesSection.properties.Add(PagesSection.bufferProp);
			PagesSection.properties.Add(PagesSection.controlsProp);
			PagesSection.properties.Add(PagesSection.enableEventValidationProp);
			PagesSection.properties.Add(PagesSection.enableSessionStateProp);
			PagesSection.properties.Add(PagesSection.enableViewStateProp);
			PagesSection.properties.Add(PagesSection.enableViewStateMacProp);
			PagesSection.properties.Add(PagesSection.maintainScrollPositionOnPostBackProp);
			PagesSection.properties.Add(PagesSection.masterPageFileProp);
			PagesSection.properties.Add(PagesSection.maxPageStateFieldLengthProp);
			PagesSection.properties.Add(PagesSection.modeProp);
			PagesSection.properties.Add(PagesSection.namespacesProp);
			PagesSection.properties.Add(PagesSection.pageBaseTypeProp);
			PagesSection.properties.Add(PagesSection.pageParserFilterTypeProp);
			PagesSection.properties.Add(PagesSection.smartNavigationProp);
			PagesSection.properties.Add(PagesSection.styleSheetThemeProp);
			PagesSection.properties.Add(PagesSection.tagMappingProp);
			PagesSection.properties.Add(PagesSection.themeProp);
			PagesSection.properties.Add(PagesSection.userControlBaseTypeProp);
			PagesSection.properties.Add(PagesSection.validateRequestProp);
			PagesSection.properties.Add(PagesSection.viewStateEncryptionModeProp);
			PagesSection.properties.Add(PagesSection.clientIDModeProp);
			PagesSection.properties.Add(PagesSection.controlRenderingCompatibilityVersionProp);
		}

		/// <summary>Gets or sets a value indicating the number of seconds to wait for an asynchronous handler to complete during asynchronous page processing.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value indicating the amount of time in seconds to wait for an asynchronous handler to complete during asynchronous page processing.</returns>
		// Token: 0x170013E3 RID: 5091
		// (get) Token: 0x06003F48 RID: 16200 RVA: 0x000A79A4 File Offset: 0x000A5BA4
		// (set) Token: 0x06003F49 RID: 16201 RVA: 0x000A79B6 File Offset: 0x000A5BB6
		[ConfigurationProperty("asyncTimeout", DefaultValue = "00:00:45")]
		[TypeConverter(typeof(TimeSpanSecondsConverter))]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		public TimeSpan AsyncTimeout
		{
			get
			{
				return (TimeSpan)base[PagesSection.asyncTimeoutProp];
			}
			set
			{
				base[PagesSection.asyncTimeoutProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether events for ASP.NET pages are automatically connected to event-handling functions.</summary>
		/// <returns>true if events for ASP.NET pages are automatically connected to event-handling functions; otherwise, false. The default is true.</returns>
		// Token: 0x170013E4 RID: 5092
		// (get) Token: 0x06003F4A RID: 16202 RVA: 0x000A79C9 File Offset: 0x000A5BC9
		// (set) Token: 0x06003F4B RID: 16203 RVA: 0x000A79DB File Offset: 0x000A5BDB
		[ConfigurationProperty("autoEventWireup", DefaultValue = true)]
		public bool AutoEventWireup
		{
			get
			{
				return (bool)base[PagesSection.autoEventWireupProp];
			}
			set
			{
				base[PagesSection.autoEventWireupProp] = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether .aspx pages and .ascx controls use response buffering.</summary>
		/// <returns>true if .aspx pages and .ascx controls use response buffering; otherwise, false. The default is true.</returns>
		// Token: 0x170013E5 RID: 5093
		// (get) Token: 0x06003F4C RID: 16204 RVA: 0x000A79EE File Offset: 0x000A5BEE
		// (set) Token: 0x06003F4D RID: 16205 RVA: 0x000A7A00 File Offset: 0x000A5C00
		[ConfigurationProperty("buffer", DefaultValue = true)]
		public bool Buffer
		{
			get
			{
				return (bool)base[PagesSection.bufferProp];
			}
			set
			{
				base[PagesSection.bufferProp] = value;
			}
		}

		/// <summary>Gets or sets a value that determines how .aspx pages and .ascx controls are compiled.</summary>
		/// <returns>One of the values for the <see cref="P:System.Web.Configuration.PagesSection.CompilationMode" /> property, which specifies how .aspx pages and .ascx controls are compiled.</returns>
		// Token: 0x170013E6 RID: 5094
		// (get) Token: 0x06003F4E RID: 16206 RVA: 0x000A7A13 File Offset: 0x000A5C13
		// (set) Token: 0x06003F4F RID: 16207 RVA: 0x000A7A25 File Offset: 0x000A5C25
		[ConfigurationProperty("compilationMode", DefaultValue = CompilationMode.Always)]
		public CompilationMode CompilationMode
		{
			get
			{
				return (CompilationMode)base[PagesSection.modeProp];
			}
			set
			{
				base[PagesSection.modeProp] = value;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.Configuration.TagPrefixInfo" /> objects.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.TagPrefixCollection" /> of <see cref="T:System.Web.Configuration.TagPrefixInfo" /> objects.</returns>
		// Token: 0x170013E7 RID: 5095
		// (get) Token: 0x06003F50 RID: 16208 RVA: 0x000A7A38 File Offset: 0x000A5C38
		[ConfigurationProperty("controls")]
		public TagPrefixCollection Controls
		{
			get
			{
				return (TagPrefixCollection)base[PagesSection.controlsProp];
			}
		}

		/// <summary>Gets or sets a value that specifies whether event validation is enabled.</summary>
		/// <returns>true if event validation is enabled; otherwise, false.</returns>
		// Token: 0x170013E8 RID: 5096
		// (get) Token: 0x06003F51 RID: 16209 RVA: 0x000A7A4A File Offset: 0x000A5C4A
		// (set) Token: 0x06003F52 RID: 16210 RVA: 0x000A7A5C File Offset: 0x000A5C5C
		[ConfigurationProperty("enableEventValidation", DefaultValue = true)]
		public bool EnableEventValidation
		{
			get
			{
				return (bool)base[PagesSection.enableEventValidationProp];
			}
			set
			{
				base[PagesSection.enableEventValidationProp] = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether the session state is enabled, disabled, or read-only.</summary>
		/// <returns>One of the values for the <see cref="P:System.Web.Configuration.PagesSection.EnableSessionState" /> property, which specifies whether the session state is enabled, disabled, or read-only. The default is <see cref="F:System.Web.Configuration.PagesEnableSessionState.True" />, which indicates that session state is enabled.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The value is not a valid <see cref="T:System.Web.Configuration.PagesEnableSessionState" /> enumeration value.</exception>
		// Token: 0x170013E9 RID: 5097
		// (get) Token: 0x06003F53 RID: 16211 RVA: 0x000A7A70 File Offset: 0x000A5C70
		// (set) Token: 0x06003F54 RID: 16212 RVA: 0x000A7AC7 File Offset: 0x000A5CC7
		[ConfigurationProperty("enableSessionState", DefaultValue = "true")]
		public PagesEnableSessionState EnableSessionState
		{
			get
			{
				string text = (string)base[PagesSection.enableSessionStateProp];
				if (text == "true")
				{
					return PagesEnableSessionState.True;
				}
				if (text == "false")
				{
					return PagesEnableSessionState.False;
				}
				if (!(text == "ReadOnly"))
				{
					throw new ConfigurationErrorsException("The 'enableSessionState' attribute must be one of the following values: true,false, ReadOnly.");
				}
				return PagesEnableSessionState.ReadOnly;
			}
			set
			{
				if (value == PagesEnableSessionState.False)
				{
					base[PagesSection.enableSessionStateProp] = "false";
					return;
				}
				if (value != PagesEnableSessionState.ReadOnly)
				{
					base[PagesSection.enableSessionStateProp] = "true";
					return;
				}
				base[PagesSection.enableSessionStateProp] = "ReadOnly";
			}
		}

		/// <summary>Gets or sets a value indicating whether view state is enabled or disabled.</summary>
		/// <returns>true if view state is enabled; false if view state is disabled. The default is true.</returns>
		// Token: 0x170013EA RID: 5098
		// (get) Token: 0x06003F55 RID: 16213 RVA: 0x000A7B04 File Offset: 0x000A5D04
		// (set) Token: 0x06003F56 RID: 16214 RVA: 0x000A7B16 File Offset: 0x000A5D16
		[ConfigurationProperty("enableViewState", DefaultValue = true)]
		public bool EnableViewState
		{
			get
			{
				return (bool)base[PagesSection.enableViewStateProp];
			}
			set
			{
				base[PagesSection.enableViewStateProp] = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether ASP.NET should run a message authentication code (MAC) on the page's view state when the page is posted back from the client.</summary>
		/// <returns>true if ASP.NET should run a message authentication code (MAC) on the page's view state when the page is posted back from the client; otherwise, false. The default is false.</returns>
		// Token: 0x170013EB RID: 5099
		// (get) Token: 0x06003F57 RID: 16215 RVA: 0x000A7B29 File Offset: 0x000A5D29
		// (set) Token: 0x06003F58 RID: 16216 RVA: 0x000A7B3B File Offset: 0x000A5D3B
		[ConfigurationProperty("enableViewStateMac", DefaultValue = true)]
		public bool EnableViewStateMac
		{
			get
			{
				return (bool)base[PagesSection.enableViewStateMacProp];
			}
			set
			{
				base[PagesSection.enableViewStateMacProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the page scroll position should be maintained upon returning from a postback from the server.</summary>
		/// <returns>true if the page-scroll position should be maintained after postback; otherwise, false. The default value is false.</returns>
		// Token: 0x170013EC RID: 5100
		// (get) Token: 0x06003F59 RID: 16217 RVA: 0x000A7B4E File Offset: 0x000A5D4E
		// (set) Token: 0x06003F5A RID: 16218 RVA: 0x000A7B60 File Offset: 0x000A5D60
		[ConfigurationProperty("maintainScrollPositionOnPostBack", DefaultValue = false)]
		public bool MaintainScrollPositionOnPostBack
		{
			get
			{
				return (bool)base[PagesSection.maintainScrollPositionOnPostBackProp];
			}
			set
			{
				base[PagesSection.maintainScrollPositionOnPostBackProp] = value;
			}
		}

		/// <summary>Gets or sets a reference to the master page for the application. </summary>
		/// <returns>A reference to the master page for the application.</returns>
		// Token: 0x170013ED RID: 5101
		// (get) Token: 0x06003F5B RID: 16219 RVA: 0x000A7B73 File Offset: 0x000A5D73
		// (set) Token: 0x06003F5C RID: 16220 RVA: 0x000A7B85 File Offset: 0x000A5D85
		[ConfigurationProperty("masterPageFile", DefaultValue = "")]
		public string MasterPageFile
		{
			get
			{
				return (string)base[PagesSection.masterPageFileProp];
			}
			set
			{
				base[PagesSection.masterPageFileProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum number of characters that a single view-state field can contain.</summary>
		/// <returns>The maximum number of characters that a single view-state field can contain.</returns>
		// Token: 0x170013EE RID: 5102
		// (get) Token: 0x06003F5D RID: 16221 RVA: 0x000A7B93 File Offset: 0x000A5D93
		// (set) Token: 0x06003F5E RID: 16222 RVA: 0x000A7BA5 File Offset: 0x000A5DA5
		[ConfigurationProperty("maxPageStateFieldLength", DefaultValue = -1)]
		public int MaxPageStateFieldLength
		{
			get
			{
				return (int)base[PagesSection.maxPageStateFieldLengthProp];
			}
			set
			{
				base[PagesSection.maxPageStateFieldLengthProp] = value;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.Configuration.NamespaceInfo" /> objects.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.NamespaceCollection" /> of <see cref="T:System.Web.Configuration.NamespaceInfo" /> objects.</returns>
		// Token: 0x170013EF RID: 5103
		// (get) Token: 0x06003F5F RID: 16223 RVA: 0x000A7BB8 File Offset: 0x000A5DB8
		[ConfigurationProperty("namespaces")]
		public NamespaceCollection Namespaces
		{
			get
			{
				return (NamespaceCollection)base[PagesSection.namespacesProp];
			}
		}

		/// <summary>Gets or sets a value that specifies a code-behind class that .aspx pages inherit by default.</summary>
		/// <returns>A string that specifies a code-behind class that .aspx pages inherit by default.</returns>
		// Token: 0x170013F0 RID: 5104
		// (get) Token: 0x06003F60 RID: 16224 RVA: 0x000A7BCA File Offset: 0x000A5DCA
		// (set) Token: 0x06003F61 RID: 16225 RVA: 0x000A7BDC File Offset: 0x000A5DDC
		[ConfigurationProperty("pageBaseType", DefaultValue = "System.Web.UI.Page")]
		public string PageBaseType
		{
			get
			{
				return (string)base[PagesSection.pageBaseTypeProp];
			}
			set
			{
				base[PagesSection.pageBaseTypeProp] = value;
			}
		}

		/// <summary>Gets or sets a value that specifies the parser filter type.</summary>
		/// <returns>A string that specifies the parser filter type.</returns>
		// Token: 0x170013F1 RID: 5105
		// (get) Token: 0x06003F62 RID: 16226 RVA: 0x000A7BEA File Offset: 0x000A5DEA
		// (set) Token: 0x06003F63 RID: 16227 RVA: 0x000A7BFC File Offset: 0x000A5DFC
		[ConfigurationProperty("pageParserFilterType", DefaultValue = "")]
		public string PageParserFilterType
		{
			get
			{
				return (string)base[PagesSection.pageParserFilterTypeProp];
			}
			set
			{
				base[PagesSection.pageParserFilterTypeProp] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether smart navigation is enabled.</summary>
		/// <returns>true if smart navigation is enabled; otherwise, false. The default value is false.</returns>
		// Token: 0x170013F2 RID: 5106
		// (get) Token: 0x06003F64 RID: 16228 RVA: 0x000A7C0A File Offset: 0x000A5E0A
		// (set) Token: 0x06003F65 RID: 16229 RVA: 0x000A7C1C File Offset: 0x000A5E1C
		[ConfigurationProperty("smartNavigation", DefaultValue = false)]
		public bool SmartNavigation
		{
			get
			{
				return (bool)base[PagesSection.smartNavigationProp];
			}
			set
			{
				base[PagesSection.smartNavigationProp] = value;
			}
		}

		/// <summary>Gets or sets the name of an ASP.NET style sheet theme.</summary>
		/// <returns>The name of an ASP.NET style sheet theme.</returns>
		// Token: 0x170013F3 RID: 5107
		// (get) Token: 0x06003F66 RID: 16230 RVA: 0x000A7C2F File Offset: 0x000A5E2F
		// (set) Token: 0x06003F67 RID: 16231 RVA: 0x000A7C41 File Offset: 0x000A5E41
		[ConfigurationProperty("styleSheetTheme", DefaultValue = "")]
		public string StyleSheetTheme
		{
			get
			{
				return (string)base[PagesSection.styleSheetThemeProp];
			}
			set
			{
				base[PagesSection.styleSheetThemeProp] = value;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.Configuration.TagMapInfo" /> objects.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.TagMapCollection" /> of <see cref="T:System.Web.Configuration.TagMapInfo" /> objects.</returns>
		// Token: 0x170013F4 RID: 5108
		// (get) Token: 0x06003F68 RID: 16232 RVA: 0x000A7C4F File Offset: 0x000A5E4F
		[ConfigurationProperty("tagMapping")]
		public TagMapCollection TagMapping
		{
			get
			{
				return (TagMapCollection)base[PagesSection.tagMappingProp];
			}
		}

		/// <summary>Gets or sets the name of an ASP.NET page theme.</summary>
		/// <returns>The name of an ASP.NET page theme.</returns>
		// Token: 0x170013F5 RID: 5109
		// (get) Token: 0x06003F69 RID: 16233 RVA: 0x000A7C61 File Offset: 0x000A5E61
		// (set) Token: 0x06003F6A RID: 16234 RVA: 0x000A7C73 File Offset: 0x000A5E73
		[ConfigurationProperty("theme", DefaultValue = "")]
		public string Theme
		{
			get
			{
				return (string)base[PagesSection.themeProp];
			}
			set
			{
				base[PagesSection.themeProp] = value;
			}
		}

		/// <summary>Gets or sets a value that specifies a code-behind class that user controls inherit by default.</summary>
		/// <returns>A string that specifies a code-behind file that user controls inherit by default.</returns>
		// Token: 0x170013F6 RID: 5110
		// (get) Token: 0x06003F6B RID: 16235 RVA: 0x000A7C81 File Offset: 0x000A5E81
		// (set) Token: 0x06003F6C RID: 16236 RVA: 0x000A7C93 File Offset: 0x000A5E93
		[ConfigurationProperty("userControlBaseType", DefaultValue = "System.Web.UI.UserControl")]
		public string UserControlBaseType
		{
			get
			{
				return (string)base[PagesSection.userControlBaseTypeProp];
			}
			set
			{
				base[PagesSection.userControlBaseTypeProp] = value;
			}
		}

		/// <summary>Gets or sets a value that determines whether ASP.NET examines input from the browser for dangerous values. For more information, see Script Exploits Overview.</summary>
		/// <returns>true if ASP.NET examines input from the browser for dangerous values; otherwise, false. The default value is true.</returns>
		// Token: 0x170013F7 RID: 5111
		// (get) Token: 0x06003F6D RID: 16237 RVA: 0x000A7CA1 File Offset: 0x000A5EA1
		// (set) Token: 0x06003F6E RID: 16238 RVA: 0x000A7CB3 File Offset: 0x000A5EB3
		[ConfigurationProperty("validateRequest", DefaultValue = true)]
		public bool ValidateRequest
		{
			get
			{
				return (bool)base[PagesSection.validateRequestProp];
			}
			set
			{
				base[PagesSection.validateRequestProp] = value;
			}
		}

		/// <summary>Gets or sets the encryption mode that ASP.NET uses when maintaining ViewState values.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ViewStateEncryptionMode" /> enumeration value indicating when the ViewState values are encrypted.</returns>
		// Token: 0x170013F8 RID: 5112
		// (get) Token: 0x06003F6F RID: 16239 RVA: 0x000A7CC6 File Offset: 0x000A5EC6
		// (set) Token: 0x06003F70 RID: 16240 RVA: 0x000A7CD8 File Offset: 0x000A5ED8
		[ConfigurationProperty("viewStateEncryptionMode", DefaultValue = ViewStateEncryptionMode.Auto)]
		public ViewStateEncryptionMode ViewStateEncryptionMode
		{
			get
			{
				return (ViewStateEncryptionMode)base[PagesSection.viewStateEncryptionModeProp];
			}
			set
			{
				base[PagesSection.viewStateEncryptionModeProp] = value;
			}
		}

		/// <summary>Gets or sets the default algorithm that is used to generate a control's identifier.</summary>
		/// <returns>A value that indicates how the value in the <see cref="P:System.Web.UI.Control.ClientID" /> property is generated. The default value is <see cref="F:System.Web.UI.ClientIDMode.Predictable" />.</returns>
		// Token: 0x170013F9 RID: 5113
		// (get) Token: 0x06003F71 RID: 16241 RVA: 0x000A7CEB File Offset: 0x000A5EEB
		// (set) Token: 0x06003F72 RID: 16242 RVA: 0x000A7CFD File Offset: 0x000A5EFD
		[ConfigurationProperty("clientIDMode", DefaultValue = ClientIDMode.Predictable)]
		public ClientIDMode ClientIDMode
		{
			get
			{
				return (ClientIDMode)base[PagesSection.clientIDModeProp];
			}
			set
			{
				base[PagesSection.clientIDModeProp] = value;
			}
		}

		/// <summary>Gets or sets a value that specifies the ASP.NET version that any rendered HTML will be compatible with.</summary>
		/// <returns>The ASP.NET version that any rendered HTML will be compatible with.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt was made to set this property to null.</exception>
		// Token: 0x170013FA RID: 5114
		// (get) Token: 0x06003F73 RID: 16243 RVA: 0x000A7D10 File Offset: 0x000A5F10
		// (set) Token: 0x06003F74 RID: 16244 RVA: 0x000A7D22 File Offset: 0x000A5F22
		[ConfigurationProperty("controlRenderingCompatibilityVersion", DefaultValue = "4.0")]
		public Version ControlRenderingCompatibilityVersion
		{
			get
			{
				return (Version)base[PagesSection.controlRenderingCompatibilityVersionProp];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base[PagesSection.controlRenderingCompatibilityVersionProp] = value;
			}
		}

		// Token: 0x170013FB RID: 5115
		// (get) Token: 0x06003F75 RID: 16245 RVA: 0x000A7D44 File Offset: 0x000A5F44
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return PagesSection.properties;
			}
		}

		// Token: 0x06003F76 RID: 16246 RVA: 0x000A17EE File Offset: 0x0009F9EE
		protected internal override void DeserializeSection(XmlReader reader)
		{
			base.DeserializeSection(reader);
		}

		/// <summary>Gets the collection of device tags that ASP.NET should ignore when it renders a page.</summary>
		/// <returns>The collection of device tags that ASP.NET should ignore.</returns>
		// Token: 0x170013FC RID: 5116
		// (get) Token: 0x06003F77 RID: 16247 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public IgnoreDeviceFilterElementCollection IgnoreDeviceFilters
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets a value that indicates whether all system-generated hidden fields are rendered at the top of the form.</summary>
		/// <returns>true if system-generated hidden fields are rendered at the top of the form; otherwise, false. The default is true.</returns>
		// Token: 0x170013FD RID: 5117
		// (get) Token: 0x06003F78 RID: 16248 RVA: 0x000A7D4C File Offset: 0x000A5F4C
		// (set) Token: 0x06003F79 RID: 16249 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public bool RenderAllHiddenFieldsAtTopOfForm
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04002279 RID: 8825
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x0400227A RID: 8826
		private static ConfigurationProperty asyncTimeoutProp = new ConfigurationProperty("asyncTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(45.0), PropertyHelper.TimeSpanSecondsConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400227B RID: 8827
		private static ConfigurationProperty autoEventWireupProp = new ConfigurationProperty("autoEventWireup", typeof(bool), true);

		// Token: 0x0400227C RID: 8828
		private static ConfigurationProperty bufferProp = new ConfigurationProperty("buffer", typeof(bool), true);

		// Token: 0x0400227D RID: 8829
		private static ConfigurationProperty controlsProp = new ConfigurationProperty("controls", typeof(TagPrefixCollection), null, null, null, ConfigurationPropertyOptions.None);

		// Token: 0x0400227E RID: 8830
		private static ConfigurationProperty enableEventValidationProp = new ConfigurationProperty("enableEventValidation", typeof(bool), true);

		// Token: 0x0400227F RID: 8831
		private static ConfigurationProperty enableSessionStateProp = new ConfigurationProperty("enableSessionState", typeof(string), "true");

		// Token: 0x04002280 RID: 8832
		private static ConfigurationProperty enableViewStateProp = new ConfigurationProperty("enableViewState", typeof(bool), true);

		// Token: 0x04002281 RID: 8833
		private static ConfigurationProperty enableViewStateMacProp = new ConfigurationProperty("enableViewStateMac", typeof(bool), true);

		// Token: 0x04002282 RID: 8834
		private static ConfigurationProperty maintainScrollPositionOnPostBackProp = new ConfigurationProperty("maintainScrollPositionOnPostBack", typeof(bool), false);

		// Token: 0x04002283 RID: 8835
		private static ConfigurationProperty masterPageFileProp = new ConfigurationProperty("masterPageFile", typeof(string), "");

		// Token: 0x04002284 RID: 8836
		private static ConfigurationProperty maxPageStateFieldLengthProp = new ConfigurationProperty("maxPageStateFieldLength", typeof(int), -1);

		// Token: 0x04002285 RID: 8837
		private static ConfigurationProperty modeProp = new ConfigurationProperty("compilationMode", typeof(CompilationMode), CompilationMode.Always, new GenericEnumConverter(typeof(CompilationMode)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002286 RID: 8838
		private static ConfigurationProperty namespacesProp = new ConfigurationProperty("namespaces", typeof(NamespaceCollection), null, null, null, ConfigurationPropertyOptions.None);

		// Token: 0x04002287 RID: 8839
		private static ConfigurationProperty pageBaseTypeProp = new ConfigurationProperty("pageBaseType", typeof(string), "System.Web.UI.Page");

		// Token: 0x04002288 RID: 8840
		private static ConfigurationProperty pageParserFilterTypeProp = new ConfigurationProperty("pageParserFilterType", typeof(string), "");

		// Token: 0x04002289 RID: 8841
		private static ConfigurationProperty smartNavigationProp = new ConfigurationProperty("smartNavigation", typeof(bool), false);

		// Token: 0x0400228A RID: 8842
		private static ConfigurationProperty styleSheetThemeProp = new ConfigurationProperty("styleSheetTheme", typeof(string), "");

		// Token: 0x0400228B RID: 8843
		private static ConfigurationProperty tagMappingProp = new ConfigurationProperty("tagMapping", typeof(TagMapCollection), null, null, null, ConfigurationPropertyOptions.None);

		// Token: 0x0400228C RID: 8844
		private static ConfigurationProperty themeProp = new ConfigurationProperty("theme", typeof(string), "");

		// Token: 0x0400228D RID: 8845
		private static ConfigurationProperty userControlBaseTypeProp = new ConfigurationProperty("userControlBaseType", typeof(string), "System.Web.UI.UserControl");

		// Token: 0x0400228E RID: 8846
		private static ConfigurationProperty validateRequestProp = new ConfigurationProperty("validateRequest", typeof(bool), true);

		// Token: 0x0400228F RID: 8847
		private static ConfigurationProperty viewStateEncryptionModeProp = new ConfigurationProperty("viewStateEncryptionMode", typeof(ViewStateEncryptionMode), ViewStateEncryptionMode.Auto, new GenericEnumConverter(typeof(ViewStateEncryptionMode)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002290 RID: 8848
		private static ConfigurationProperty clientIDModeProp = new ConfigurationProperty("clientIDMode", typeof(ClientIDMode), ClientIDMode.Predictable, new GenericEnumConverter(typeof(ClientIDMode)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002291 RID: 8849
		private static ConfigurationProperty controlRenderingCompatibilityVersionProp = new ConfigurationProperty("controlRenderingCompatibilityVersion", typeof(Version), new Version(4, 0), new VersionConverter(3, 5, "The value for the property 'controlRenderingCompatibilityVersion' is not valid. The error is: The control rendering compatibility version must not be less than {1}."), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
	}
}
