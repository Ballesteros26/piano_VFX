using System;
using System.ComponentModel;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Configures an ASP.NET application to use the <see cref="T:System.Web.Configuration.AuthenticationMode" /> forms modality. </summary>
	// Token: 0x0200059E RID: 1438
	public sealed class FormsAuthenticationConfiguration : ConfigurationElement
	{
		// Token: 0x06003CF3 RID: 15603 RVA: 0x000A1CAC File Offset: 0x0009FEAC
		static FormsAuthenticationConfiguration()
		{
			FormsAuthenticationConfiguration.properties.Add(FormsAuthenticationConfiguration.cookielessProp);
			FormsAuthenticationConfiguration.properties.Add(FormsAuthenticationConfiguration.credentialsProp);
			FormsAuthenticationConfiguration.properties.Add(FormsAuthenticationConfiguration.defaultUrlProp);
			FormsAuthenticationConfiguration.properties.Add(FormsAuthenticationConfiguration.domainProp);
			FormsAuthenticationConfiguration.properties.Add(FormsAuthenticationConfiguration.enableCrossAppRedirectsProp);
			FormsAuthenticationConfiguration.properties.Add(FormsAuthenticationConfiguration.loginUrlProp);
			FormsAuthenticationConfiguration.properties.Add(FormsAuthenticationConfiguration.nameProp);
			FormsAuthenticationConfiguration.properties.Add(FormsAuthenticationConfiguration.pathProp);
			FormsAuthenticationConfiguration.properties.Add(FormsAuthenticationConfiguration.protectionProp);
			FormsAuthenticationConfiguration.properties.Add(FormsAuthenticationConfiguration.requireSSLProp);
			FormsAuthenticationConfiguration.properties.Add(FormsAuthenticationConfiguration.slidingExpirationProp);
			FormsAuthenticationConfiguration.properties.Add(FormsAuthenticationConfiguration.timeoutProp);
			FormsAuthenticationConfiguration.elementProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(FormsAuthenticationConfiguration), new ValidatorCallback(FormsAuthenticationConfiguration.ValidateElement)));
		}

		// Token: 0x06003CF5 RID: 15605 RVA: 0x0000393A File Offset: 0x00001B3A
		private static void ValidateElement(object o)
		{
		}

		// Token: 0x170012C6 RID: 4806
		// (get) Token: 0x06003CF6 RID: 15606 RVA: 0x000A1FB0 File Offset: 0x000A01B0
		protected internal override ConfigurationElementProperty ElementProperty
		{
			get
			{
				return FormsAuthenticationConfiguration.elementProperty;
			}
		}

		/// <summary>Gets or sets a value indicating whether forms-based authentication should use cookies.</summary>
		/// <returns>One of the <see cref="T:System.Web.HttpCookieMode" /> values. The default value is <see cref="F:System.Web.HttpCookieMode.UseDeviceProfile" />.</returns>
		// Token: 0x170012C7 RID: 4807
		// (get) Token: 0x06003CF7 RID: 15607 RVA: 0x000A1FB7 File Offset: 0x000A01B7
		// (set) Token: 0x06003CF8 RID: 15608 RVA: 0x000A1FC9 File Offset: 0x000A01C9
		[ConfigurationProperty("cookieless", DefaultValue = "UseDeviceProfile")]
		public HttpCookieMode Cookieless
		{
			get
			{
				return (HttpCookieMode)base[FormsAuthenticationConfiguration.cookielessProp];
			}
			set
			{
				base[FormsAuthenticationConfiguration.cookielessProp] = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.FormsAuthenticationCredentials" /> collection of user names and passwords.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.FormsAuthenticationCredentials" /> collection that contains the user names and passwords.</returns>
		// Token: 0x170012C8 RID: 4808
		// (get) Token: 0x06003CF9 RID: 15609 RVA: 0x000A1FDC File Offset: 0x000A01DC
		[ConfigurationProperty("credentials")]
		public FormsAuthenticationCredentials Credentials
		{
			get
			{
				return (FormsAuthenticationCredentials)base[FormsAuthenticationConfiguration.credentialsProp];
			}
		}

		/// <summary>Gets or sets the default URL.</summary>
		/// <returns>The URL to which to redirect the request after authentication. The default value is default.aspx.</returns>
		// Token: 0x170012C9 RID: 4809
		// (get) Token: 0x06003CFA RID: 15610 RVA: 0x000A1FEE File Offset: 0x000A01EE
		// (set) Token: 0x06003CFB RID: 15611 RVA: 0x000A2000 File Offset: 0x000A0200
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("defaultUrl", DefaultValue = "default.aspx")]
		public string DefaultUrl
		{
			get
			{
				return (string)base[FormsAuthenticationConfiguration.defaultUrlProp];
			}
			set
			{
				base[FormsAuthenticationConfiguration.defaultUrlProp] = value;
			}
		}

		/// <summary>Gets or sets the domain name to be sent with forms authentication cookies.</summary>
		/// <returns>The name of the domain for the outgoing forms authentication cookies. Default is an empty string.</returns>
		// Token: 0x170012CA RID: 4810
		// (get) Token: 0x06003CFC RID: 15612 RVA: 0x000A200E File Offset: 0x000A020E
		// (set) Token: 0x06003CFD RID: 15613 RVA: 0x000A2020 File Offset: 0x000A0220
		[ConfigurationProperty("domain", DefaultValue = "")]
		public string Domain
		{
			get
			{
				return (string)base[FormsAuthenticationConfiguration.domainProp];
			}
			set
			{
				base[FormsAuthenticationConfiguration.domainProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether authenticated users can be redirected to URLS in other applications.</summary>
		/// <returns>true if authenticated users can be redirected to URLs in other applications; otherwise false. The default is false.</returns>
		// Token: 0x170012CB RID: 4811
		// (get) Token: 0x06003CFE RID: 15614 RVA: 0x000A202E File Offset: 0x000A022E
		// (set) Token: 0x06003CFF RID: 15615 RVA: 0x000A2040 File Offset: 0x000A0240
		[ConfigurationProperty("enableCrossAppRedirects", DefaultValue = "False")]
		public bool EnableCrossAppRedirects
		{
			get
			{
				return (bool)base[FormsAuthenticationConfiguration.enableCrossAppRedirectsProp];
			}
			set
			{
				base[FormsAuthenticationConfiguration.enableCrossAppRedirectsProp] = value;
			}
		}

		/// <summary>Gets or sets the redirection URL for the request.</summary>
		/// <returns>The URL the request is redirected to when the user is not authenticated. The default value is login.aspx.</returns>
		// Token: 0x170012CC RID: 4812
		// (get) Token: 0x06003D00 RID: 15616 RVA: 0x000A2053 File Offset: 0x000A0253
		// (set) Token: 0x06003D01 RID: 15617 RVA: 0x000A2065 File Offset: 0x000A0265
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("loginUrl", DefaultValue = "login.aspx")]
		public string LoginUrl
		{
			get
			{
				return (string)base[FormsAuthenticationConfiguration.loginUrlProp];
			}
			set
			{
				base[FormsAuthenticationConfiguration.loginUrlProp] = value;
			}
		}

		/// <summary>Gets or sets the cookie name.</summary>
		/// <returns>The name of the HTTP cookie to use for request authentication.</returns>
		// Token: 0x170012CD RID: 4813
		// (get) Token: 0x06003D02 RID: 15618 RVA: 0x000A2073 File Offset: 0x000A0273
		// (set) Token: 0x06003D03 RID: 15619 RVA: 0x000A2085 File Offset: 0x000A0285
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("name", DefaultValue = ".ASPXAUTH")]
		public string Name
		{
			get
			{
				return (string)base[FormsAuthenticationConfiguration.nameProp];
			}
			set
			{
				base[FormsAuthenticationConfiguration.nameProp] = value;
			}
		}

		/// <summary>Gets or sets the cookie path.</summary>
		/// <returns>The path of the HTTP cookie to use for authentication. The default value is a slash (/), which represents the Web-application root.</returns>
		// Token: 0x170012CE RID: 4814
		// (get) Token: 0x06003D04 RID: 15620 RVA: 0x000A2093 File Offset: 0x000A0293
		// (set) Token: 0x06003D05 RID: 15621 RVA: 0x000A20A5 File Offset: 0x000A02A5
		[ConfigurationProperty("path", DefaultValue = "/")]
		[StringValidator(MinLength = 1)]
		public string Path
		{
			get
			{
				return (string)base[FormsAuthenticationConfiguration.pathProp];
			}
			set
			{
				base[FormsAuthenticationConfiguration.pathProp] = value;
			}
		}

		/// <summary>Gets or sets the encryption type used to encrypt the cookie.</summary>
		/// <returns>One of the <see cref="T:System.Web.Configuration.FormsProtectionEnum" /> enumeration values. The default value is All.Note   Be sure to use the default value for this property if you want both data validation and encryption to help protect the cookie. This option uses the configured data-validation algorithm based on the machineKey. Triple-DES (3DES) is used for encryption, if available and if the key is long enough (48 bytes or more). To improve the protection of your cookie, you may also want to set the <see cref="P:System.Web.Configuration.FormsAuthenticationConfiguration.RequireSSL" /> to true.</returns>
		// Token: 0x170012CF RID: 4815
		// (get) Token: 0x06003D06 RID: 15622 RVA: 0x000A20B3 File Offset: 0x000A02B3
		// (set) Token: 0x06003D07 RID: 15623 RVA: 0x000A20C5 File Offset: 0x000A02C5
		[ConfigurationProperty("protection", DefaultValue = "All")]
		public FormsProtectionEnum Protection
		{
			get
			{
				return (FormsProtectionEnum)base[FormsAuthenticationConfiguration.protectionProp];
			}
			set
			{
				base[FormsAuthenticationConfiguration.protectionProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether a Secure Sockets Layer (SSL) connection is required when transmitting authentication information.</summary>
		/// <returns>true if an SSL connection is required; otherwise, false. The default is false.</returns>
		// Token: 0x170012D0 RID: 4816
		// (get) Token: 0x06003D08 RID: 15624 RVA: 0x000A20D8 File Offset: 0x000A02D8
		// (set) Token: 0x06003D09 RID: 15625 RVA: 0x000A20EA File Offset: 0x000A02EA
		[ConfigurationProperty("requireSSL", DefaultValue = "False")]
		public bool RequireSSL
		{
			get
			{
				return (bool)base[FormsAuthenticationConfiguration.requireSSLProp];
			}
			set
			{
				base[FormsAuthenticationConfiguration.requireSSLProp] = value;
			}
		}

		/// <summary>Gets or sets the authentication sliding expiration.</summary>
		/// <returns>true if the sliding expiration is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x170012D1 RID: 4817
		// (get) Token: 0x06003D0A RID: 15626 RVA: 0x000A20FD File Offset: 0x000A02FD
		// (set) Token: 0x06003D0B RID: 15627 RVA: 0x000A210F File Offset: 0x000A030F
		[ConfigurationProperty("slidingExpiration", DefaultValue = "True")]
		public bool SlidingExpiration
		{
			get
			{
				return (bool)base[FormsAuthenticationConfiguration.slidingExpirationProp];
			}
			set
			{
				base[FormsAuthenticationConfiguration.slidingExpirationProp] = value;
			}
		}

		/// <summary>Gets or sets the authentication time-out.</summary>
		/// <returns>The amount of time in minutes after which the authentication expires. The default value is 30 minutes.</returns>
		// Token: 0x170012D2 RID: 4818
		// (get) Token: 0x06003D0C RID: 15628 RVA: 0x000A2122 File Offset: 0x000A0322
		// (set) Token: 0x06003D0D RID: 15629 RVA: 0x000A2134 File Offset: 0x000A0334
		[TypeConverter(typeof(TimeSpanMinutesConverter))]
		[TimeSpanValidator(MinValueString = "00:01:00")]
		[ConfigurationProperty("timeout", DefaultValue = "00:30:00")]
		public TimeSpan Timeout
		{
			get
			{
				return (TimeSpan)base[FormsAuthenticationConfiguration.timeoutProp];
			}
			set
			{
				base[FormsAuthenticationConfiguration.timeoutProp] = value;
			}
		}

		// Token: 0x170012D3 RID: 4819
		// (get) Token: 0x06003D0E RID: 15630 RVA: 0x000A2147 File Offset: 0x000A0347
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return FormsAuthenticationConfiguration.properties;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to use Coordinated Universal Time (UTC) or local time for the ticket expiration date.</summary>
		/// <returns>The ticket expiration-date compatibility mode.</returns>
		// Token: 0x170012D4 RID: 4820
		// (get) Token: 0x06003D0F RID: 15631 RVA: 0x000A2150 File Offset: 0x000A0350
		// (set) Token: 0x06003D10 RID: 15632 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public TicketCompatibilityMode TicketCompatibilityMode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return TicketCompatibilityMode.Framework20;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x040020E5 RID: 8421
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040020E6 RID: 8422
		private static ConfigurationProperty cookielessProp = new ConfigurationProperty("cookieless", typeof(HttpCookieMode), HttpCookieMode.UseDeviceProfile, new GenericEnumConverter(typeof(HttpCookieMode)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020E7 RID: 8423
		private static ConfigurationProperty credentialsProp = new ConfigurationProperty("credentials", typeof(FormsAuthenticationCredentials), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020E8 RID: 8424
		private static ConfigurationProperty defaultUrlProp = new ConfigurationProperty("defaultUrl", typeof(string), "default.aspx", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020E9 RID: 8425
		private static ConfigurationProperty domainProp = new ConfigurationProperty("domain", typeof(string), "");

		// Token: 0x040020EA RID: 8426
		private static ConfigurationProperty enableCrossAppRedirectsProp = new ConfigurationProperty("enableCrossAppRedirects", typeof(bool), false);

		// Token: 0x040020EB RID: 8427
		private static ConfigurationProperty loginUrlProp = new ConfigurationProperty("loginUrl", typeof(string), "login.aspx", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020EC RID: 8428
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), ".ASPXAUTH", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020ED RID: 8429
		private static ConfigurationProperty pathProp = new ConfigurationProperty("path", typeof(string), "/", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020EE RID: 8430
		private static ConfigurationProperty protectionProp = new ConfigurationProperty("protection", typeof(FormsProtectionEnum), FormsProtectionEnum.All, new GenericEnumConverter(typeof(FormsProtectionEnum)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020EF RID: 8431
		private static ConfigurationProperty requireSSLProp = new ConfigurationProperty("requireSSL", typeof(bool), false);

		// Token: 0x040020F0 RID: 8432
		private static ConfigurationProperty slidingExpirationProp = new ConfigurationProperty("slidingExpiration", typeof(bool), true);

		// Token: 0x040020F1 RID: 8433
		private static ConfigurationProperty timeoutProp = new ConfigurationProperty("timeout", typeof(TimeSpan), TimeSpan.FromMinutes(30.0), PropertyHelper.TimeSpanMinutesConverter, new TimeSpanValidator(new TimeSpan(0, 1, 0), TimeSpan.MaxValue), ConfigurationPropertyOptions.None);

		// Token: 0x040020F2 RID: 8434
		private static ConfigurationElementProperty elementProperty;
	}
}
