using System;
using System.Collections;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.Util;
using System.Xml;

namespace System.Web.Configuration
{
	/// <summary>Defines configuration settings that are used to support the globalization infrastructure of Web applications. This class cannot be inherited.</summary>
	// Token: 0x020005A2 RID: 1442
	public sealed class GlobalizationSection : ConfigurationSection
	{
		// Token: 0x06003D32 RID: 15666 RVA: 0x000A23FC File Offset: 0x000A05FC
		static GlobalizationSection()
		{
			GlobalizationSection.properties.Add(GlobalizationSection.cultureProp);
			GlobalizationSection.properties.Add(GlobalizationSection.enableBestFitResponseEncodingProp);
			GlobalizationSection.properties.Add(GlobalizationSection.enableClientBasedCultureProp);
			GlobalizationSection.properties.Add(GlobalizationSection.fileEncodingProp);
			GlobalizationSection.properties.Add(GlobalizationSection.requestEncodingProp);
			GlobalizationSection.properties.Add(GlobalizationSection.resourceProviderFactoryTypeProp);
			GlobalizationSection.properties.Add(GlobalizationSection.responseEncodingProp);
			GlobalizationSection.properties.Add(GlobalizationSection.responseHeaderEncodingProp);
			GlobalizationSection.properties.Add(GlobalizationSection.uiCultureProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.GlobalizationSection" /> class by using default settings.</summary>
		// Token: 0x06003D33 RID: 15667 RVA: 0x000A25A5 File Offset: 0x000A07A5
		public GlobalizationSection()
		{
			this.encodingHash = new Hashtable();
		}

		// Token: 0x06003D34 RID: 15668 RVA: 0x000A25B8 File Offset: 0x000A07B8
		private void VerifyData()
		{
			bool flag = false;
			try
			{
				this.GetSanitizedCulture(this.Culture, ref flag);
			}
			catch
			{
				throw new ConfigurationErrorsException("the <globalization> tag contains an invalid value for the 'culture' attribute");
			}
			try
			{
				this.GetSanitizedCulture(this.UICulture, ref flag);
			}
			catch
			{
				throw new ConfigurationErrorsException("the <globalization> tag contains an invalid value for the 'uiCulture' attribute");
			}
		}

		// Token: 0x06003D35 RID: 15669 RVA: 0x000A2620 File Offset: 0x000A0820
		protected override void PostDeserialize()
		{
			base.PostDeserialize();
			this.VerifyData();
		}

		// Token: 0x06003D36 RID: 15670 RVA: 0x000A262E File Offset: 0x000A082E
		protected override void PreSerialize(XmlWriter writer)
		{
			base.PreSerialize(writer);
			this.VerifyData();
		}

		/// <summary>Gets or sets a value specifying the default culture for processing incoming Web requests.</summary>
		/// <returns>The default culture for processing incoming Web requests.</returns>
		// Token: 0x170012E2 RID: 4834
		// (get) Token: 0x06003D37 RID: 15671 RVA: 0x000A263D File Offset: 0x000A083D
		// (set) Token: 0x06003D38 RID: 15672 RVA: 0x000A264F File Offset: 0x000A084F
		[ConfigurationProperty("culture", DefaultValue = "")]
		public string Culture
		{
			get
			{
				return (string)base[GlobalizationSection.cultureProp];
			}
			set
			{
				base[GlobalizationSection.cultureProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the best-fit character encoding for a response is enabled.</summary>
		/// <returns>true if the best-fit character encoding for a response is enabled; otherwise, false. The default is false.</returns>
		// Token: 0x170012E3 RID: 4835
		// (get) Token: 0x06003D39 RID: 15673 RVA: 0x000A265D File Offset: 0x000A085D
		// (set) Token: 0x06003D3A RID: 15674 RVA: 0x000A266F File Offset: 0x000A086F
		[ConfigurationProperty("enableBestFitResponseEncoding", DefaultValue = "False")]
		public bool EnableBestFitResponseEncoding
		{
			get
			{
				return (bool)base[GlobalizationSection.enableBestFitResponseEncodingProp];
			}
			set
			{
				base[GlobalizationSection.enableBestFitResponseEncodingProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="P:System.Web.Configuration.GlobalizationSection.Culture" /> and <see cref="P:System.Web.Configuration.GlobalizationSection.UICulture" /> properties should be based on the <see cref="F:System.Net.HttpRequestHeader.AcceptLanguage" /> header field value that is sent by the client browser.</summary>
		/// <returns>true if the <see cref="P:System.Web.Configuration.GlobalizationSection.Culture" /> and <see cref="P:System.Web.Configuration.GlobalizationSection.UICulture" /> should be based on the <see cref="F:System.Net.HttpRequestHeader.AcceptLanguage" /> header field value sent by the client browser; otherwise, false. The default is false.</returns>
		// Token: 0x170012E4 RID: 4836
		// (get) Token: 0x06003D3B RID: 15675 RVA: 0x000A2682 File Offset: 0x000A0882
		// (set) Token: 0x06003D3C RID: 15676 RVA: 0x000A2694 File Offset: 0x000A0894
		[ConfigurationProperty("enableClientBasedCulture", DefaultValue = "False")]
		public bool EnableClientBasedCulture
		{
			get
			{
				return (bool)base[GlobalizationSection.enableClientBasedCultureProp];
			}
			set
			{
				base[GlobalizationSection.enableClientBasedCultureProp] = value;
			}
		}

		/// <summary>Gets or sets a value specifying the default encoding for .aspx, .asmx, and .asax file parsing.</summary>
		/// <returns>The default encoding value.</returns>
		// Token: 0x170012E5 RID: 4837
		// (get) Token: 0x06003D3D RID: 15677 RVA: 0x000A26A7 File Offset: 0x000A08A7
		// (set) Token: 0x06003D3E RID: 15678 RVA: 0x000A26BA File Offset: 0x000A08BA
		[ConfigurationProperty("fileEncoding")]
		public Encoding FileEncoding
		{
			get
			{
				return this.GetEncoding(GlobalizationSection.fileEncodingProp, ref this.cached_fileencoding);
			}
			set
			{
				base[GlobalizationSection.fileEncodingProp] = value.WebName;
			}
		}

		/// <summary>Gets or sets a value specifying the content encoding of HTTP requests.</summary>
		/// <returns>The content encoding of HTTP requests. The default is UTF-8.</returns>
		// Token: 0x170012E6 RID: 4838
		// (get) Token: 0x06003D3F RID: 15679 RVA: 0x000A26CD File Offset: 0x000A08CD
		// (set) Token: 0x06003D40 RID: 15680 RVA: 0x000A26E0 File Offset: 0x000A08E0
		[ConfigurationProperty("requestEncoding", DefaultValue = "utf-8")]
		public Encoding RequestEncoding
		{
			get
			{
				return this.GetEncoding(GlobalizationSection.requestEncodingProp, ref this.cached_requestencoding);
			}
			set
			{
				base[GlobalizationSection.requestEncodingProp] = value.WebName;
			}
		}

		/// <summary>Gets or sets the factory type of the resource provider.</summary>
		/// <returns>The factory type of the resource provider.</returns>
		// Token: 0x170012E7 RID: 4839
		// (get) Token: 0x06003D41 RID: 15681 RVA: 0x000A26F3 File Offset: 0x000A08F3
		// (set) Token: 0x06003D42 RID: 15682 RVA: 0x000A2705 File Offset: 0x000A0905
		[ConfigurationProperty("resourceProviderFactoryType", DefaultValue = "")]
		public string ResourceProviderFactoryType
		{
			get
			{
				return (string)base[GlobalizationSection.resourceProviderFactoryTypeProp];
			}
			set
			{
				base[GlobalizationSection.resourceProviderFactoryTypeProp] = value;
			}
		}

		/// <summary>Gets or sets a value specifying the content encoding of HTTP responses.</summary>
		/// <returns>The content encoding of HTTP responses. The default is UTF-8.</returns>
		// Token: 0x170012E8 RID: 4840
		// (get) Token: 0x06003D43 RID: 15683 RVA: 0x000A2713 File Offset: 0x000A0913
		// (set) Token: 0x06003D44 RID: 15684 RVA: 0x000A2726 File Offset: 0x000A0926
		[ConfigurationProperty("responseEncoding", DefaultValue = "utf-8")]
		public Encoding ResponseEncoding
		{
			get
			{
				return this.GetEncoding(GlobalizationSection.responseEncodingProp, ref this.cached_responseencoding);
			}
			set
			{
				base[GlobalizationSection.responseEncodingProp] = value.WebName;
			}
		}

		/// <summary>Gets or sets a value specifying the header encoding of HTTP responses.</summary>
		/// <returns>The header encoding of HTTP responses. The default is UTF-8.</returns>
		// Token: 0x170012E9 RID: 4841
		// (get) Token: 0x06003D45 RID: 15685 RVA: 0x000A2739 File Offset: 0x000A0939
		// (set) Token: 0x06003D46 RID: 15686 RVA: 0x000A274C File Offset: 0x000A094C
		[ConfigurationProperty("responseHeaderEncoding", DefaultValue = "utf-8")]
		public Encoding ResponseHeaderEncoding
		{
			get
			{
				return this.GetEncoding(GlobalizationSection.responseHeaderEncodingProp, ref this.cached_responseheaderencoding);
			}
			set
			{
				base[GlobalizationSection.responseHeaderEncodingProp] = value.WebName;
			}
		}

		/// <summary>Gets or sets a value specifying the default culture for processing locale-dependent resource searches.</summary>
		/// <returns>The default culture for processing locale-dependent resource searches.</returns>
		// Token: 0x170012EA RID: 4842
		// (get) Token: 0x06003D47 RID: 15687 RVA: 0x000A275F File Offset: 0x000A095F
		// (set) Token: 0x06003D48 RID: 15688 RVA: 0x000A2771 File Offset: 0x000A0971
		[ConfigurationProperty("uiCulture", DefaultValue = "")]
		public string UICulture
		{
			get
			{
				return (string)base[GlobalizationSection.uiCultureProp];
			}
			set
			{
				base[GlobalizationSection.uiCultureProp] = value;
			}
		}

		// Token: 0x170012EB RID: 4843
		// (get) Token: 0x06003D49 RID: 15689 RVA: 0x000A277F File Offset: 0x000A097F
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return GlobalizationSection.properties;
			}
		}

		// Token: 0x170012EC RID: 4844
		// (get) Token: 0x06003D4A RID: 15690 RVA: 0x000A2786 File Offset: 0x000A0986
		internal bool IsAutoCulture
		{
			get
			{
				return this.autoCulture;
			}
		}

		// Token: 0x170012ED RID: 4845
		// (get) Token: 0x06003D4B RID: 15691 RVA: 0x000A278E File Offset: 0x000A098E
		internal bool IsAutoUICulture
		{
			get
			{
				return this.autoUICulture;
			}
		}

		// Token: 0x06003D4C RID: 15692 RVA: 0x000A2798 File Offset: 0x000A0998
		private CultureInfo GetSanitizedCulture(string culture, ref bool auto)
		{
			auto = false;
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			if (culture.Length <= 3)
			{
				return new CultureInfo(culture);
			}
			if (!culture.StartsWith("auto"))
			{
				return new CultureInfo(culture);
			}
			auto = true;
			if (culture.Length > 5 && culture[4] == ':')
			{
				return new CultureInfo(culture.Substring(5));
			}
			return Helpers.InvariantCulture;
		}

		// Token: 0x06003D4D RID: 15693 RVA: 0x000A2804 File Offset: 0x000A0A04
		internal CultureInfo GetUICulture()
		{
			string uiculture = this.UICulture;
			if (this.cached_uiculture != uiculture)
			{
				try
				{
					this.cached_uicultureinfo = this.GetSanitizedCulture(uiculture, ref this.autoUICulture);
					this.cached_uiculture = uiculture;
				}
				catch
				{
					GlobalizationSection.CultureFailed("UICulture", uiculture);
					this.cached_uicultureinfo = new CultureInfo(127);
					this.cached_uiculture = null;
				}
			}
			return this.cached_uicultureinfo;
		}

		// Token: 0x06003D4E RID: 15694 RVA: 0x000A287C File Offset: 0x000A0A7C
		internal CultureInfo GetCulture()
		{
			string culture = this.Culture;
			if (this.cached_culture != culture)
			{
				try
				{
					this.cached_cultureinfo = this.GetSanitizedCulture(culture, ref this.autoCulture);
					this.cached_culture = culture;
				}
				catch
				{
					GlobalizationSection.CultureFailed("Culture", culture);
					this.cached_cultureinfo = new CultureInfo(127);
					this.cached_culture = null;
				}
			}
			return this.cached_cultureinfo;
		}

		// Token: 0x06003D4F RID: 15695 RVA: 0x000A28F4 File Offset: 0x000A0AF4
		private Encoding GetEncoding(ConfigurationProperty prop, ref string cached_encoding_name)
		{
			string text = (string)base[prop];
			if (cached_encoding_name == null)
			{
				cached_encoding_name = ((text == null) ? "utf-8" : text);
			}
			Encoding encoding = (Encoding)this.encodingHash[prop];
			if (encoding == null || encoding.WebName != cached_encoding_name)
			{
				try
				{
					string text2 = cached_encoding_name.ToLower(Helpers.InvariantCulture);
					uint num = global::<PrivateImplementationDetails>.ComputeStringHash(text2);
					if (num <= 1769188890U)
					{
						if (num <= 662872037U)
						{
							if (num != 102844035U)
							{
								if (num != 103516576U)
								{
									if (num != 662872037U)
									{
										goto IL_01EC;
									}
									if (!(text2 == "utf-16le"))
									{
										goto IL_01EC;
									}
								}
								else if (!(text2 == "unicode"))
								{
									goto IL_01EC;
								}
							}
							else
							{
								if (!(text2 == "utf-8"))
								{
									goto IL_01EC;
								}
								goto IL_01E4;
							}
						}
						else if (num != 864497655U)
						{
							if (num != 998087601U)
							{
								if (num != 1769188890U)
								{
									goto IL_01EC;
								}
								if (!(text2 == "utf-16"))
								{
									goto IL_01EC;
								}
							}
							else if (!(text2 == "ucs-2"))
							{
								goto IL_01EC;
							}
						}
						else
						{
							if (!(text2 == "utf-16be"))
							{
								goto IL_01EC;
							}
							goto IL_01DA;
						}
					}
					else if (num <= 2579641873U)
					{
						if (num != 2342213115U)
						{
							if (num != 2421957552U)
							{
								if (num != 2579641873U)
								{
									goto IL_01EC;
								}
								if (!(text2 == "unicodefffe"))
								{
									goto IL_01EC;
								}
								goto IL_01DA;
							}
							else
							{
								if (!(text2 == "x-unicode-1-1-utf-8"))
								{
									goto IL_01EC;
								}
								goto IL_01E4;
							}
						}
						else
						{
							if (!(text2 == "unicode-2-0-utf-8"))
							{
								goto IL_01EC;
							}
							goto IL_01E4;
						}
					}
					else if (num != 2831744022U)
					{
						if (num != 3097952761U)
						{
							if (num != 3859930375U)
							{
								goto IL_01EC;
							}
							if (!(text2 == "iso-10646-ucs-2"))
							{
								goto IL_01EC;
							}
						}
						else
						{
							if (!(text2 == "unicode-1-1-utf-8"))
							{
								goto IL_01EC;
							}
							goto IL_01E4;
						}
					}
					else
					{
						if (!(text2 == "x-unicode-2-0-utf-8"))
						{
							goto IL_01EC;
						}
						goto IL_01E4;
					}
					encoding = new UnicodeEncoding(false, true);
					goto IL_01F4;
					IL_01DA:
					encoding = new UnicodeEncoding(true, true);
					goto IL_01F4;
					IL_01E4:
					encoding = Encoding.UTF8;
					goto IL_01F4;
					IL_01EC:
					encoding = Encoding.GetEncoding(cached_encoding_name);
					IL_01F4:;
				}
				catch
				{
					GlobalizationSection.EncodingFailed(prop.Name, cached_encoding_name);
					encoding = new UTF8Encoding(false, false);
				}
			}
			this.encodingHash[prop] = encoding;
			cached_encoding_name = encoding.WebName;
			return encoding;
		}

		// Token: 0x06003D50 RID: 15696 RVA: 0x000A2B44 File Offset: 0x000A0D44
		private static void EncodingFailed(string att, string enc)
		{
			if (GlobalizationSection.encoding_warning)
			{
				return;
			}
			GlobalizationSection.encoding_warning = true;
			Console.WriteLine("Encoding {1} cannot be loaded.\n{0}=\"{1}\"\n", att, enc);
		}

		// Token: 0x06003D51 RID: 15697 RVA: 0x000A2B60 File Offset: 0x000A0D60
		private static void CultureFailed(string att, string cul)
		{
			if (GlobalizationSection.culture_warning)
			{
				return;
			}
			GlobalizationSection.culture_warning = true;
			Console.WriteLine("Culture {1} cannot be loaded. Perhaps your runtime \ndon't have ICU support?\n{0}=\"{1}\"\n", att, cul);
		}

		// Token: 0x040020FA RID: 8442
		private static ConfigurationProperty cultureProp = new ConfigurationProperty("culture", typeof(string), "");

		// Token: 0x040020FB RID: 8443
		private static ConfigurationProperty enableBestFitResponseEncodingProp = new ConfigurationProperty("enableBestFitResponseEncoding", typeof(bool), false);

		// Token: 0x040020FC RID: 8444
		private static ConfigurationProperty enableClientBasedCultureProp = new ConfigurationProperty("enableClientBasedCulture", typeof(bool), false);

		// Token: 0x040020FD RID: 8445
		private static ConfigurationProperty fileEncodingProp = new ConfigurationProperty("fileEncoding", typeof(string));

		// Token: 0x040020FE RID: 8446
		private static ConfigurationProperty requestEncodingProp = new ConfigurationProperty("requestEncoding", typeof(string), "utf-8");

		// Token: 0x040020FF RID: 8447
		private static ConfigurationProperty resourceProviderFactoryTypeProp = new ConfigurationProperty("resourceProviderFactoryType", typeof(string), "");

		// Token: 0x04002100 RID: 8448
		private static ConfigurationProperty responseEncodingProp = new ConfigurationProperty("responseEncoding", typeof(string), "utf-8");

		// Token: 0x04002101 RID: 8449
		private static ConfigurationProperty responseHeaderEncodingProp = new ConfigurationProperty("responseHeaderEncoding", typeof(string), "utf-8");

		// Token: 0x04002102 RID: 8450
		private static ConfigurationProperty uiCultureProp = new ConfigurationProperty("uiCulture", typeof(string), "");

		// Token: 0x04002103 RID: 8451
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002104 RID: 8452
		private string cached_fileencoding;

		// Token: 0x04002105 RID: 8453
		private string cached_requestencoding;

		// Token: 0x04002106 RID: 8454
		private string cached_responseencoding;

		// Token: 0x04002107 RID: 8455
		private string cached_responseheaderencoding;

		// Token: 0x04002108 RID: 8456
		private Hashtable encodingHash;

		// Token: 0x04002109 RID: 8457
		private string cached_culture;

		// Token: 0x0400210A RID: 8458
		private CultureInfo cached_cultureinfo;

		// Token: 0x0400210B RID: 8459
		private string cached_uiculture;

		// Token: 0x0400210C RID: 8460
		private CultureInfo cached_uicultureinfo;

		// Token: 0x0400210D RID: 8461
		private static bool encoding_warning;

		// Token: 0x0400210E RID: 8462
		private static bool culture_warning;

		// Token: 0x0400210F RID: 8463
		private bool autoCulture;

		// Token: 0x04002110 RID: 8464
		private bool autoUICulture;
	}
}
