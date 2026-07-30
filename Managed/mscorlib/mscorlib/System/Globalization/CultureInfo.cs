using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Globalization
{
	/// <summary>Provides information about a specific culture (called a locale for unmanaged code development). The information includes the names for the culture, the writing system, the calendar used, and formatting for dates and sort strings.</summary>
	// Token: 0x02000443 RID: 1091
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class CultureInfo : ICloneable, IFormatProvider
	{
		/// <summary>Gets the <see cref="T:System.Globalization.CultureInfo" /> object that is culture-independent (invariant).</summary>
		/// <returns>The object that is culture-independent (invariant).</returns>
		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x0600341B RID: 13339 RVA: 0x000BF0A1 File Offset: 0x000BD2A1
		public static CultureInfo InvariantCulture
		{
			get
			{
				return CultureInfo.invariant_culture_info;
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CultureInfo" /> object that represents the culture used by the current thread.</summary>
		/// <returns>An object that represents the culture used by the current thread.</returns>
		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x0600341C RID: 13340 RVA: 0x000BF0AA File Offset: 0x000BD2AA
		// (set) Token: 0x0600341D RID: 13341 RVA: 0x000BF0B6 File Offset: 0x000BD2B6
		public static CultureInfo CurrentCulture
		{
			get
			{
				return Thread.CurrentThread.CurrentCulture;
			}
			set
			{
				Thread.CurrentThread.CurrentCulture = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CultureInfo" /> object that represents the current user interface culture used by the Resource Manager to look up culture-specific resources at run time.</summary>
		/// <returns>The culture used by the Resource Manager to look up culture-specific resources at run time.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x0600341E RID: 13342 RVA: 0x000BF0C3 File Offset: 0x000BD2C3
		// (set) Token: 0x0600341F RID: 13343 RVA: 0x000BF0CF File Offset: 0x000BD2CF
		public static CultureInfo CurrentUICulture
		{
			get
			{
				return Thread.CurrentThread.CurrentUICulture;
			}
			set
			{
				Thread.CurrentThread.CurrentUICulture = value;
			}
		}

		// Token: 0x06003420 RID: 13344 RVA: 0x000BF0DC File Offset: 0x000BD2DC
		internal static CultureInfo ConstructCurrentCulture()
		{
			if (CultureInfo.default_current_culture != null)
			{
				return CultureInfo.default_current_culture;
			}
			string current_locale_name = CultureInfo.get_current_locale_name();
			CultureInfo cultureInfo = null;
			if (current_locale_name != null)
			{
				try
				{
					cultureInfo = CultureInfo.CreateSpecificCulture(current_locale_name);
				}
				catch
				{
				}
			}
			if (cultureInfo == null)
			{
				cultureInfo = CultureInfo.InvariantCulture;
			}
			else
			{
				cultureInfo.m_isReadOnly = true;
				cultureInfo.m_useUserOverride = true;
			}
			CultureInfo.default_current_culture = cultureInfo;
			return cultureInfo;
		}

		// Token: 0x06003421 RID: 13345 RVA: 0x000BF140 File Offset: 0x000BD340
		internal static CultureInfo ConstructCurrentUICulture()
		{
			return CultureInfo.ConstructCurrentCulture();
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06003422 RID: 13346 RVA: 0x000BF147 File Offset: 0x000BD347
		internal string Territory
		{
			get
			{
				return this.territory;
			}
		}

		/// <summary>Gets the culture types that pertain to the current <see cref="T:System.Globalization.CultureInfo" /> object.</summary>
		/// <returns>A bitwise combination of one or more <see cref="T:System.Globalization.CultureTypes" /> values. There is no default value.</returns>
		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06003423 RID: 13347 RVA: 0x000BF150 File Offset: 0x000BD350
		[ComVisible(false)]
		public CultureTypes CultureTypes
		{
			get
			{
				CultureTypes cultureTypes = (CultureTypes)0;
				foreach (object obj in Enum.GetValues(typeof(CultureTypes)))
				{
					CultureTypes cultureTypes2 = (CultureTypes)obj;
					if (Array.IndexOf<CultureInfo>(CultureInfo.GetCultures(cultureTypes2), this) >= 0)
					{
						cultureTypes |= cultureTypes2;
					}
				}
				return cultureTypes;
			}
		}

		/// <summary>Gets an alternate user interface culture suitable for console applications when the default graphic user interface culture is unsuitable.</summary>
		/// <returns>An alternate culture that is used to read and display text on the console.</returns>
		// Token: 0x06003424 RID: 13348 RVA: 0x000BF1C4 File Offset: 0x000BD3C4
		[ComVisible(false)]
		public CultureInfo GetConsoleFallbackUICulture()
		{
			string name = this.Name;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
			if (num <= 1260172255U)
			{
				if (num <= 939759947U)
				{
					if (num <= 249681006U)
					{
						if (num <= 198587497U)
						{
							if (num != 64366545U)
							{
								if (num != 77939050U)
								{
									if (num != 198587497U)
									{
										goto IL_06C2;
									}
									if (!(name == "ar-SA"))
									{
										goto IL_06C2;
									}
								}
								else if (!(name == "mr-IN"))
								{
									goto IL_06C2;
								}
							}
							else if (!(name == "ar-SY"))
							{
								goto IL_06C2;
							}
						}
						else if (num != 233820021U)
						{
							if (num != 236085687U)
							{
								if (num != 249681006U)
								{
									goto IL_06C2;
								}
								if (!(name == "hi-IN"))
								{
									goto IL_06C2;
								}
							}
							else if (!(name == "ar-KW"))
							{
								goto IL_06C2;
							}
						}
						else if (!(name == "ar-EG"))
						{
							goto IL_06C2;
						}
					}
					else if (num <= 469295067U)
					{
						if (num != 419506663U)
						{
							if (num != 434712723U)
							{
								if (num != 469295067U)
								{
									goto IL_06C2;
								}
								if (!(name == "ar-AE"))
								{
									goto IL_06C2;
								}
							}
							else if (!(name == "sa-IN"))
							{
								goto IL_06C2;
							}
						}
						else if (!(name == "ar-BH"))
						{
							goto IL_06C2;
						}
					}
					else if (num != 511763911U)
					{
						if (num != 907337542U)
						{
							if (num != 939759947U)
							{
								goto IL_06C2;
							}
							if (!(name == "ar-MA"))
							{
								goto IL_06C2;
							}
							goto IL_06B7;
						}
						else if (!(name == "ar-JO"))
						{
							goto IL_06C2;
						}
					}
					else if (!(name == "vi-VN"))
					{
						goto IL_06C2;
					}
				}
				else if (num <= 1074569279U)
				{
					if (num <= 1011170994U)
					{
						if (num != 944060518U)
						{
							if (num != 944899161U)
							{
								if (num != 1011170994U)
								{
									goto IL_06C2;
								}
								if (!(name == "te"))
								{
									goto IL_06C2;
								}
							}
							else if (!(name == "sa"))
							{
								goto IL_06C2;
							}
						}
						else if (!(name == "ta"))
						{
							goto IL_06C2;
						}
					}
					else if (num != 1011465184U)
					{
						if (num != 1070729495U)
						{
							if (num != 1074569279U)
							{
								goto IL_06C2;
							}
							if (!(name == "ar-IQ"))
							{
								goto IL_06C2;
							}
						}
						else if (!(name == "ar-QA"))
						{
							goto IL_06C2;
						}
					}
					else if (!(name == "vi"))
					{
						goto IL_06C2;
					}
				}
				else if (num <= 1123180923U)
				{
					if (num != 1094514636U)
					{
						if (num != 1095059089U)
						{
							if (num != 1123180923U)
							{
								goto IL_06C2;
							}
							if (!(name == "ar-DZ"))
							{
								goto IL_06C2;
							}
							goto IL_06B7;
						}
						else if (!(name == "th"))
						{
							goto IL_06C2;
						}
					}
					else if (!(name == "kn"))
					{
						goto IL_06C2;
					}
				}
				else if (num != 1141238470U)
				{
					if (num != 1162022470U)
					{
						if (num != 1260172255U)
						{
							goto IL_06C2;
						}
						if (!(name == "dv"))
						{
							goto IL_06C2;
						}
					}
					else if (!(name == "ur"))
					{
						goto IL_06C2;
					}
				}
				else if (!(name == "ar-LY"))
				{
					goto IL_06C2;
				}
			}
			else if (num <= 1756775346U)
			{
				if (num <= 1527123707U)
				{
					if (num <= 1429081278U)
					{
						if (num != 1277200137U)
						{
							if (num != 1347311754U)
							{
								if (num != 1429081278U)
								{
									goto IL_06C2;
								}
								if (!(name == "mr"))
								{
									goto IL_06C2;
								}
							}
							else if (!(name == "pa"))
							{
								goto IL_06C2;
							}
						}
						else if (!(name == "gu"))
						{
							goto IL_06C2;
						}
					}
					else if (num != 1456070279U)
					{
						if (num != 1458211363U)
						{
							if (num != 1527123707U)
							{
								goto IL_06C2;
							}
							if (!(name == "ar-LB"))
							{
								goto IL_06C2;
							}
						}
						else if (!(name == "gu-IN"))
						{
							goto IL_06C2;
						}
					}
					else
					{
						if (!(name == "ar-TN"))
						{
							goto IL_06C2;
						}
						goto IL_06B7;
					}
				}
				else if (num <= 1622153968U)
				{
					if (num != 1547363254U)
					{
						if (num != 1562713850U)
						{
							if (num != 1622153968U)
							{
								goto IL_06C2;
							}
							if (!(name == "kok-IN"))
							{
								goto IL_06C2;
							}
						}
						else if (!(name == "ar"))
						{
							goto IL_06C2;
						}
					}
					else if (!(name == "he"))
					{
						goto IL_06C2;
					}
				}
				else if (num != 1680010088U)
				{
					if (num != 1748694682U)
					{
						if (num != 1756775346U)
						{
							goto IL_06C2;
						}
						if (!(name == "ta-IN"))
						{
							goto IL_06C2;
						}
					}
					else if (!(name == "hi"))
					{
						goto IL_06C2;
					}
				}
				else if (!(name == "fa"))
				{
					goto IL_06C2;
				}
			}
			else if (num <= 3073845542U)
			{
				if (num <= 2153224060U)
				{
					if (num != 1846834581U)
					{
						if (num != 2046577884U)
						{
							if (num != 2153224060U)
							{
								goto IL_06C2;
							}
							if (!(name == "he-IL"))
							{
								goto IL_06C2;
							}
						}
						else if (!(name == "kok"))
						{
							goto IL_06C2;
						}
					}
					else if (!(name == "dv-MV"))
					{
						goto IL_06C2;
					}
				}
				else if (num != 2902799296U)
				{
					if (num != 3060605246U)
					{
						if (num != 3073845542U)
						{
							goto IL_06C2;
						}
						if (!(name == "te-IN"))
						{
							goto IL_06C2;
						}
					}
					else if (!(name == "pa-IN"))
					{
						goto IL_06C2;
					}
				}
				else if (!(name == "kn-IN"))
				{
					goto IL_06C2;
				}
			}
			else if (num <= 3477219856U)
			{
				if (num != 3294142633U)
				{
					if (num != 3311105148U)
					{
						if (num != 3477219856U)
						{
							goto IL_06C2;
						}
						if (!(name == "fa-IR"))
						{
							goto IL_06C2;
						}
					}
					else if (!(name == "syr-SY"))
					{
						goto IL_06C2;
					}
				}
				else if (!(name == "syr"))
				{
					goto IL_06C2;
				}
			}
			else if (num != 3957656723U)
			{
				if (num != 4027935912U)
				{
					if (num != 4091062904U)
					{
						goto IL_06C2;
					}
					if (!(name == "th-TH"))
					{
						goto IL_06C2;
					}
				}
				else if (!(name == "ur-PK"))
				{
					goto IL_06C2;
				}
			}
			else if (!(name == "ar-YE"))
			{
				goto IL_06C2;
			}
			return CultureInfo.GetCultureInfo("en");
			IL_06B7:
			return CultureInfo.GetCultureInfo("fr");
			IL_06C2:
			if ((this.CultureTypes & CultureTypes.WindowsOnlyCultures) == (CultureTypes)0)
			{
				return this;
			}
			return CultureInfo.InvariantCulture;
		}

		/// <summary>Deprecated. Gets the RFC 4646 standard identification for a language. </summary>
		/// <returns>A string that is the RFC 4646 standard identification for a language.</returns>
		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06003425 RID: 13349 RVA: 0x000BF8A8 File Offset: 0x000BDAA8
		[ComVisible(false)]
		public string IetfLanguageTag
		{
			get
			{
				string name = this.Name;
				if (name == "zh-CHS")
				{
					return "zh-Hans";
				}
				if (!(name == "zh-CHT"))
				{
					return this.Name;
				}
				return "zh-Hant";
			}
		}

		/// <summary>Gets the active input locale identifier.</summary>
		/// <returns>A 32-bit signed number that specifies an input locale identifier.</returns>
		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06003426 RID: 13350 RVA: 0x000BF8EC File Offset: 0x000BDAEC
		[ComVisible(false)]
		public virtual int KeyboardLayoutId
		{
			get
			{
				int lcid = this.LCID;
				if (lcid <= 1034)
				{
					if (lcid == 4)
					{
						return 2052;
					}
					if (lcid == 1034)
					{
						return 3082;
					}
				}
				else
				{
					if (lcid == 31748)
					{
						return 1028;
					}
					if (lcid == 31770)
					{
						return 2074;
					}
				}
				if (this.LCID >= 1024)
				{
					return this.LCID;
				}
				return this.LCID + 1024;
			}
		}

		/// <summary>Gets the culture identifier for the current <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>The culture identifier for the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06003427 RID: 13351 RVA: 0x000BF960 File Offset: 0x000BDB60
		public virtual int LCID
		{
			get
			{
				return this.cultureID;
			}
		}

		/// <summary>Gets the culture name in the format languagecode2-country/regioncode2.</summary>
		/// <returns>The culture name in the format languagecode2-country/regioncode2. languagecode2 is a lowercase two-letter code derived from ISO 639-1. country/regioncode2 is derived from ISO 3166 and usually consists of two uppercase letters.</returns>
		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06003428 RID: 13352 RVA: 0x000BF968 File Offset: 0x000BDB68
		public virtual string Name
		{
			get
			{
				return this.m_name;
			}
		}

		/// <summary>Gets the culture name, consisting of the language, the country/region, and the optional script, that the culture is set to display.</summary>
		/// <returns>The culture name. consisting of the full name of the language, the full name of the country/region, and the optional script. The format is discussed in the description of the <see cref="T:System.Globalization.CultureInfo" /> class.</returns>
		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06003429 RID: 13353 RVA: 0x000BF970 File Offset: 0x000BDB70
		public virtual string NativeName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.nativename;
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x0600342A RID: 13354 RVA: 0x000BF986 File Offset: 0x000BDB86
		internal string NativeCalendarName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.native_calendar_names[(this.default_calendar_type >> 8) - 1];
			}
		}

		/// <summary>Gets the default calendar used by the culture.</summary>
		/// <returns>A <see cref="T:System.Globalization.Calendar" /> that represents the default calendar used by the culture.</returns>
		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x0600342B RID: 13355 RVA: 0x000BF9A7 File Offset: 0x000BDBA7
		public virtual Calendar Calendar
		{
			get
			{
				if (this.calendar == null)
				{
					if (!this.constructed)
					{
						this.Construct();
					}
					this.calendar = CultureInfo.CreateCalendar(this.default_calendar_type);
				}
				return this.calendar;
			}
		}

		/// <summary>Gets the list of calendars that can be used by the culture.</summary>
		/// <returns>An array of type <see cref="T:System.Globalization.Calendar" /> that represents the calendars that can be used by the culture represented by the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x0600342C RID: 13356 RVA: 0x000BF9D6 File Offset: 0x000BDBD6
		[MonoLimitation("Optional calendars are not supported only default calendar is returned")]
		public virtual Calendar[] OptionalCalendars
		{
			get
			{
				return new Calendar[] { this.Calendar };
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CultureInfo" /> that represents the parent culture of the current <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>The <see cref="T:System.Globalization.CultureInfo" /> that represents the parent culture of the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x0600342D RID: 13357 RVA: 0x000BF9E8 File Offset: 0x000BDBE8
		public virtual CultureInfo Parent
		{
			get
			{
				if (this.parent_culture == null)
				{
					if (!this.constructed)
					{
						this.Construct();
					}
					if (this.parent_lcid == this.cultureID)
					{
						if (this.parent_lcid == 31748 && this.EnglishName[this.EnglishName.Length - 1] == 'y')
						{
							return this.parent_culture = new CultureInfo("zh-Hant");
						}
						if (this.parent_lcid == 4 && this.EnglishName[this.EnglishName.Length - 1] == 'y')
						{
							return this.parent_culture = new CultureInfo("zh-Hans");
						}
						return null;
					}
					else if (this.parent_lcid == 127)
					{
						this.parent_culture = CultureInfo.InvariantCulture;
					}
					else if (this.cultureID == 127)
					{
						this.parent_culture = this;
					}
					else if (this.cultureID == 1028)
					{
						this.parent_culture = new CultureInfo("zh-CHT");
					}
					else
					{
						this.parent_culture = new CultureInfo(this.parent_lcid);
					}
				}
				return this.parent_culture;
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.TextInfo" /> that defines the writing system associated with the culture.</summary>
		/// <returns>The <see cref="T:System.Globalization.TextInfo" /> that defines the writing system associated with the culture.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x0600342E RID: 13358 RVA: 0x000BFAF4 File Offset: 0x000BDCF4
		public virtual TextInfo TextInfo
		{
			get
			{
				if (this.textInfo == null)
				{
					if (!this.constructed)
					{
						this.Construct();
					}
					lock (this)
					{
						if (this.textInfo == null)
						{
							this.textInfo = this.CreateTextInfo(this.m_isReadOnly);
						}
					}
				}
				return this.textInfo;
			}
		}

		/// <summary>Gets the ISO 639-2 three-letter code for the language of the current <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>The ISO 639-2 three-letter code for the language of the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x0600342F RID: 13359 RVA: 0x000BFB68 File Offset: 0x000BDD68
		public virtual string ThreeLetterISOLanguageName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.iso3lang;
			}
		}

		/// <summary>Gets the three-letter code for the language as defined in the Windows API.</summary>
		/// <returns>The three-letter code for the language as defined in the Windows API.</returns>
		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06003430 RID: 13360 RVA: 0x000BFB7E File Offset: 0x000BDD7E
		public virtual string ThreeLetterWindowsLanguageName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.win3lang;
			}
		}

		/// <summary>Gets the ISO 639-1 two-letter code for the language of the current <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>The ISO 639-1 two-letter code for the language of the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06003431 RID: 13361 RVA: 0x000BFB94 File Offset: 0x000BDD94
		public virtual string TwoLetterISOLanguageName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.iso2lang;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Globalization.CultureInfo" /> uses the user-selected culture settings.</summary>
		/// <returns>true if the current <see cref="T:System.Globalization.CultureInfo" /> uses the user-selected culture settings; otherwise, false.</returns>
		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06003432 RID: 13362 RVA: 0x000BFBAA File Offset: 0x000BDDAA
		public bool UseUserOverride
		{
			get
			{
				return this.m_useUserOverride;
			}
		}

		/// <summary>Refreshes cached culture-related information.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003433 RID: 13363 RVA: 0x000BFBB4 File Offset: 0x000BDDB4
		public void ClearCachedData()
		{
			object obj = CultureInfo.shared_table_lock;
			lock (obj)
			{
				CultureInfo.shared_by_number = null;
				CultureInfo.shared_by_name = null;
			}
			CultureInfo.default_current_culture = null;
			RegionInfo.ClearCachedData();
			TimeZone.ClearCachedData();
			TimeZoneInfo.ClearCachedData();
		}

		/// <summary>Creates a copy of the current <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>A copy of the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x06003434 RID: 13364 RVA: 0x000BFC10 File Offset: 0x000BDE10
		public virtual object Clone()
		{
			if (!this.constructed)
			{
				this.Construct();
			}
			CultureInfo cultureInfo = (CultureInfo)base.MemberwiseClone();
			cultureInfo.m_isReadOnly = false;
			cultureInfo.cached_serialized_form = null;
			if (!this.IsNeutralCulture)
			{
				cultureInfo.NumberFormat = (NumberFormatInfo)this.NumberFormat.Clone();
				cultureInfo.DateTimeFormat = (DateTimeFormatInfo)this.DateTimeFormat.Clone();
			}
			return cultureInfo;
		}

		/// <summary>Determines whether the specified object is the same culture as the current <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>true if <paramref name="value" /> is the same culture as the current <see cref="T:System.Globalization.CultureInfo" />; otherwise, false.</returns>
		/// <param name="value">The object to compare with the current <see cref="T:System.Globalization.CultureInfo" />. </param>
		// Token: 0x06003435 RID: 13365 RVA: 0x000BFC7C File Offset: 0x000BDE7C
		public override bool Equals(object value)
		{
			CultureInfo cultureInfo = value as CultureInfo;
			return cultureInfo != null && cultureInfo.cultureID == this.cultureID && cultureInfo.m_name == this.m_name;
		}

		/// <summary>Gets the list of supported cultures filtered by the specified <see cref="T:System.Globalization.CultureTypes" /> parameter.</summary>
		/// <returns>An array that contains the cultures specified by the <paramref name="types" /> parameter. The array of cultures is unsorted.</returns>
		/// <param name="types">A bitwise combination of the enumeration values that filter the cultures to retrieve. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="types" /> specifies an invalid combination of <see cref="T:System.Globalization.CultureTypes" /> values.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003436 RID: 13366 RVA: 0x000BFCB4 File Offset: 0x000BDEB4
		public static CultureInfo[] GetCultures(CultureTypes types)
		{
			bool flag = (types & CultureTypes.NeutralCultures) > (CultureTypes)0;
			bool flag2 = (types & CultureTypes.SpecificCultures) > (CultureTypes)0;
			bool flag3 = (types & CultureTypes.InstalledWin32Cultures) > (CultureTypes)0;
			CultureInfo[] array = CultureInfo.internal_get_cultures(flag, flag2, flag3);
			int i = 0;
			if (flag && array.Length != 0 && array[0] == null)
			{
				array[i++] = (CultureInfo)CultureInfo.InvariantCulture.Clone();
			}
			while (i < array.Length)
			{
				CultureInfo cultureInfo = array[i];
				CultureInfo.Data textInfoData = cultureInfo.GetTextInfoData();
				CultureInfo cultureInfo2 = array[i];
				string name = cultureInfo.m_name;
				bool flag4 = false;
				int num = cultureInfo.datetime_index;
				int calendarType = cultureInfo.CalendarType;
				int num2 = cultureInfo.number_index;
				string text = cultureInfo.iso2lang;
				int ansi = textInfoData.ansi;
				int oem = textInfoData.oem;
				int mac = textInfoData.mac;
				int ebcdic = textInfoData.ebcdic;
				bool right_to_left = textInfoData.right_to_left;
				char list_sep = (char)textInfoData.list_sep;
				cultureInfo2.m_cultureData = CultureData.GetCultureData(name, flag4, num, calendarType, num2, text, ansi, oem, mac, ebcdic, right_to_left, list_sep.ToString());
				i++;
			}
			return array;
		}

		// Token: 0x06003437 RID: 13367 RVA: 0x000BFD89 File Offset: 0x000BDF89
		private unsafe CultureInfo.Data GetTextInfoData()
		{
			return *(CultureInfo.Data*)this.textinfo_data;
		}

		/// <summary>Serves as a hash function for the current <see cref="T:System.Globalization.CultureInfo" />, suitable for hashing algorithms and data structures, such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x06003438 RID: 13368 RVA: 0x000BFD96 File Offset: 0x000BDF96
		public override int GetHashCode()
		{
			return this.cultureID.GetHashCode();
		}

		/// <summary>Returns a read-only wrapper around the specified <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>A read-only <see cref="T:System.Globalization.CultureInfo" /> wrapper around <paramref name="ci" />.</returns>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> to wrap. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ci" /> is null. </exception>
		// Token: 0x06003439 RID: 13369 RVA: 0x000BFDA4 File Offset: 0x000BDFA4
		public static CultureInfo ReadOnly(CultureInfo ci)
		{
			if (ci == null)
			{
				throw new ArgumentNullException("ci");
			}
			if (ci.m_isReadOnly)
			{
				return ci;
			}
			CultureInfo cultureInfo = (CultureInfo)ci.Clone();
			cultureInfo.m_isReadOnly = true;
			if (cultureInfo.numInfo != null)
			{
				cultureInfo.numInfo = NumberFormatInfo.ReadOnly(cultureInfo.numInfo);
			}
			if (cultureInfo.dateTimeInfo != null)
			{
				cultureInfo.dateTimeInfo = DateTimeFormatInfo.ReadOnly(cultureInfo.dateTimeInfo);
			}
			if (cultureInfo.textInfo != null)
			{
				cultureInfo.textInfo = TextInfo.ReadOnly(cultureInfo.textInfo);
			}
			return cultureInfo;
		}

		/// <summary>Returns a string containing the name of the current <see cref="T:System.Globalization.CultureInfo" /> in the format languagecode2-country/regioncode2.</summary>
		/// <returns>A string containing the name of the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x0600343A RID: 13370 RVA: 0x000BF968 File Offset: 0x000BDB68
		public override string ToString()
		{
			return this.m_name;
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CompareInfo" /> that defines how to compare strings for the culture.</summary>
		/// <returns>The <see cref="T:System.Globalization.CompareInfo" /> that defines how to compare strings for the culture.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x0600343B RID: 13371 RVA: 0x000BFE3C File Offset: 0x000BE03C
		public virtual CompareInfo CompareInfo
		{
			get
			{
				if (this.compareInfo == null)
				{
					if (!this.constructed)
					{
						this.Construct();
					}
					lock (this)
					{
						if (this.compareInfo == null)
						{
							this.compareInfo = new CompareInfo(this);
						}
					}
				}
				return this.compareInfo;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Globalization.CultureInfo" /> represents a neutral culture.</summary>
		/// <returns>true if the current <see cref="T:System.Globalization.CultureInfo" /> represents a neutral culture; otherwise, false.</returns>
		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x0600343C RID: 13372 RVA: 0x000BFEAC File Offset: 0x000BE0AC
		public virtual bool IsNeutralCulture
		{
			get
			{
				if (this.cultureID == 127)
				{
					return false;
				}
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.territory == null;
			}
		}

		// Token: 0x0600343D RID: 13373 RVA: 0x00002194 File Offset: 0x00000394
		private void CheckNeutral()
		{
		}

		/// <summary>Gets or sets a <see cref="T:System.Globalization.NumberFormatInfo" /> that defines the culturally appropriate format of displaying numbers, currency, and percentage.</summary>
		/// <returns>A <see cref="T:System.Globalization.NumberFormatInfo" /> that defines the culturally appropriate format of displaying numbers, currency, and percentage.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Globalization.CultureInfo.NumberFormat" /> property or any of the <see cref="T:System.Globalization.NumberFormatInfo" /> properties is set, and the <see cref="T:System.Globalization.CultureInfo" /> is read-only. </exception>
		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x0600343E RID: 13374 RVA: 0x000BFED4 File Offset: 0x000BE0D4
		// (set) Token: 0x0600343F RID: 13375 RVA: 0x000BFF14 File Offset: 0x000BE114
		public virtual NumberFormatInfo NumberFormat
		{
			get
			{
				if (this.numInfo == null)
				{
					this.numInfo = new NumberFormatInfo(this.m_cultureData)
					{
						isReadOnly = this.m_isReadOnly
					};
				}
				return this.numInfo;
			}
			set
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				if (this.m_isReadOnly)
				{
					throw new InvalidOperationException("This instance is read only");
				}
				if (value == null)
				{
					throw new ArgumentNullException("NumberFormat");
				}
				this.numInfo = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Globalization.DateTimeFormatInfo" /> that defines the culturally appropriate format of displaying dates and times.</summary>
		/// <returns>A <see cref="T:System.Globalization.DateTimeFormatInfo" /> that defines the culturally appropriate format of displaying dates and times.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Globalization.CultureInfo.DateTimeFormat" /> property or any of the <see cref="T:System.Globalization.DateTimeFormatInfo" /> properties is set, and the <see cref="T:System.Globalization.CultureInfo" /> is read-only. </exception>
		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06003440 RID: 13376 RVA: 0x000BFF50 File Offset: 0x000BE150
		// (set) Token: 0x06003441 RID: 13377 RVA: 0x000BFFB8 File Offset: 0x000BE1B8
		public virtual DateTimeFormatInfo DateTimeFormat
		{
			get
			{
				if (this.dateTimeInfo != null)
				{
					return this.dateTimeInfo;
				}
				if (!this.constructed)
				{
					this.Construct();
				}
				this.CheckNeutral();
				DateTimeFormatInfo dateTimeFormatInfo = new DateTimeFormatInfo(this.m_cultureData, this.Calendar);
				dateTimeFormatInfo.m_isReadOnly = this.m_isReadOnly;
				Thread.MemoryBarrier();
				this.dateTimeInfo = dateTimeFormatInfo;
				return this.dateTimeInfo;
			}
			set
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				if (this.m_isReadOnly)
				{
					throw new InvalidOperationException("This instance is read only");
				}
				if (value == null)
				{
					throw new ArgumentNullException("DateTimeFormat");
				}
				this.dateTimeInfo = value;
			}
		}

		/// <summary>Gets the full localized culture name. </summary>
		/// <returns>The full localized culture name in the format languagefull [country/regionfull], where languagefull is the full name of the language and country/regionfull is the full name of the country/region.</returns>
		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06003442 RID: 13378 RVA: 0x000BFFF2 File Offset: 0x000BE1F2
		public virtual string DisplayName
		{
			get
			{
				return this.EnglishName;
			}
		}

		/// <summary>Gets the culture name in the format languagefull [country/regionfull] in English.</summary>
		/// <returns>The culture name in the format languagefull [country/regionfull] in English, where languagefull is the full name of the language and country/regionfull is the full name of the country/region.</returns>
		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06003443 RID: 13379 RVA: 0x000BFFFA File Offset: 0x000BE1FA
		public virtual string EnglishName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.englishname;
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CultureInfo" /> that represents the culture installed with the operating system.</summary>
		/// <returns>The <see cref="T:System.Globalization.CultureInfo" /> that represents the culture installed with the operating system.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06003444 RID: 13380 RVA: 0x000BF140 File Offset: 0x000BD340
		public static CultureInfo InstalledUICulture
		{
			get
			{
				return CultureInfo.ConstructCurrentCulture();
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Globalization.CultureInfo" /> is read-only.</summary>
		/// <returns>true if the current <see cref="T:System.Globalization.CultureInfo" /> is read-only; otherwise, false. The default is false.</returns>
		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06003445 RID: 13381 RVA: 0x000C0010 File Offset: 0x000BE210
		public bool IsReadOnly
		{
			get
			{
				return this.m_isReadOnly;
			}
		}

		/// <summary>Gets an object that defines how to format the specified type.</summary>
		/// <returns>The value of the <see cref="P:System.Globalization.CultureInfo.NumberFormat" /> property, which is a <see cref="T:System.Globalization.NumberFormatInfo" /> containing the default number format information for the current <see cref="T:System.Globalization.CultureInfo" />, if <paramref name="formatType" /> is the <see cref="T:System.Type" /> object for the <see cref="T:System.Globalization.NumberFormatInfo" /> class.-or- The value of the <see cref="P:System.Globalization.CultureInfo.DateTimeFormat" /> property, which is a <see cref="T:System.Globalization.DateTimeFormatInfo" /> containing the default date and time format information for the current <see cref="T:System.Globalization.CultureInfo" />, if <paramref name="formatType" /> is the <see cref="T:System.Type" /> object for the <see cref="T:System.Globalization.DateTimeFormatInfo" /> class.-or- null, if <paramref name="formatType" /> is any other object.</returns>
		/// <param name="formatType">The <see cref="T:System.Type" /> for which to get a formatting object. This method only supports the <see cref="T:System.Globalization.NumberFormatInfo" /> and <see cref="T:System.Globalization.DateTimeFormatInfo" /> types. </param>
		// Token: 0x06003446 RID: 13382 RVA: 0x000C0018 File Offset: 0x000BE218
		public virtual object GetFormat(Type formatType)
		{
			object obj = null;
			if (formatType == typeof(NumberFormatInfo))
			{
				obj = this.NumberFormat;
			}
			else if (formatType == typeof(DateTimeFormatInfo))
			{
				obj = this.DateTimeFormat;
			}
			return obj;
		}

		// Token: 0x06003447 RID: 13383 RVA: 0x000C005C File Offset: 0x000BE25C
		private void Construct()
		{
			this.construct_internal_locale_from_lcid(this.cultureID);
			this.constructed = true;
		}

		// Token: 0x06003448 RID: 13384
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool construct_internal_locale_from_lcid(int lcid);

		// Token: 0x06003449 RID: 13385
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool construct_internal_locale_from_name(string name);

		// Token: 0x0600344A RID: 13386
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string get_current_locale_name();

		// Token: 0x0600344B RID: 13387
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CultureInfo[] internal_get_cultures(bool neutral, bool specific, bool installed);

		// Token: 0x0600344C RID: 13388 RVA: 0x000C0074 File Offset: 0x000BE274
		private void ConstructInvariant(bool read_only)
		{
			this.cultureID = 127;
			this.numInfo = NumberFormatInfo.InvariantInfo;
			if (!read_only)
			{
				this.numInfo = (NumberFormatInfo)this.numInfo.Clone();
			}
			this.textInfo = TextInfo.Invariant;
			this.m_name = string.Empty;
			this.englishname = (this.nativename = "Invariant Language (Invariant Country)");
			this.iso3lang = "IVL";
			this.iso2lang = "iv";
			this.win3lang = "IVL";
			this.default_calendar_type = 257;
		}

		// Token: 0x0600344D RID: 13389 RVA: 0x000C010B File Offset: 0x000BE30B
		private TextInfo CreateTextInfo(bool readOnly)
		{
			TextInfo textInfo = new TextInfo(this.m_cultureData);
			textInfo.SetReadOnlyState(readOnly);
			return textInfo;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureInfo" /> class based on the culture specified by the culture identifier.</summary>
		/// <param name="culture">A predefined <see cref="T:System.Globalization.CultureInfo" /> identifier, <see cref="P:System.Globalization.CultureInfo.LCID" /> property of an existing <see cref="T:System.Globalization.CultureInfo" /> object, or Windows-only culture identifier. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="culture" /> is less than zero. </exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="culture" /> is not a valid culture identifier. </exception>
		// Token: 0x0600344E RID: 13390 RVA: 0x000C011F File Offset: 0x000BE31F
		public CultureInfo(int culture)
			: this(culture, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureInfo" /> class based on the culture specified by the culture identifier and on the Boolean that specifies whether to use the user-selected culture settings from the system.</summary>
		/// <param name="culture">A predefined <see cref="T:System.Globalization.CultureInfo" /> identifier, <see cref="P:System.Globalization.CultureInfo.LCID" /> property of an existing <see cref="T:System.Globalization.CultureInfo" /> object, or Windows-only culture identifier. </param>
		/// <param name="useUserOverride">A Boolean that denotes whether to use the user-selected culture settings (true) or the default culture settings (false). </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="culture" /> is less than zero. </exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="culture" /> is not a valid culture identifier.</exception>
		// Token: 0x0600344F RID: 13391 RVA: 0x000C0129 File Offset: 0x000BE329
		public CultureInfo(int culture, bool useUserOverride)
			: this(culture, useUserOverride, false)
		{
		}

		// Token: 0x06003450 RID: 13392 RVA: 0x000C0134 File Offset: 0x000BE334
		private CultureInfo(int culture, bool useUserOverride, bool read_only)
		{
			if (culture < 0)
			{
				throw new ArgumentOutOfRangeException("culture", "Positive number required.");
			}
			this.constructed = true;
			this.m_isReadOnly = read_only;
			this.m_useUserOverride = useUserOverride;
			if (culture == 127)
			{
				this.m_cultureData = CultureData.Invariant;
				this.ConstructInvariant(read_only);
				return;
			}
			if (!this.construct_internal_locale_from_lcid(culture))
			{
				string text = string.Format(CultureInfo.InvariantCulture, "Culture ID {0} (0x{1}) is not a supported culture.", culture.ToString(CultureInfo.InvariantCulture), culture.ToString("X4", CultureInfo.InvariantCulture));
				throw new CultureNotFoundException("culture", text);
			}
			CultureInfo.Data textInfoData = this.GetTextInfoData();
			string name = this.m_name;
			bool useUserOverride2 = this.m_useUserOverride;
			int num = this.datetime_index;
			int calendarType = this.CalendarType;
			int num2 = this.number_index;
			string text2 = this.iso2lang;
			int ansi = textInfoData.ansi;
			int oem = textInfoData.oem;
			int mac = textInfoData.mac;
			int ebcdic = textInfoData.ebcdic;
			bool right_to_left = textInfoData.right_to_left;
			char list_sep = (char)textInfoData.list_sep;
			this.m_cultureData = CultureData.GetCultureData(name, useUserOverride2, num, calendarType, num2, text2, ansi, oem, mac, ebcdic, right_to_left, list_sep.ToString());
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureInfo" /> class based on the culture specified by name.</summary>
		/// <param name="name">A predefined <see cref="T:System.Globalization.CultureInfo" /> name, <see cref="P:System.Globalization.CultureInfo.Name" /> of an existing <see cref="T:System.Globalization.CultureInfo" />, or Windows-only culture name. <paramref name="name" /> is not case-sensitive.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="name" /> is not a valid culture name.</exception>
		// Token: 0x06003451 RID: 13393 RVA: 0x000C022C File Offset: 0x000BE42C
		public CultureInfo(string name)
			: this(name, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureInfo" /> class based on the culture specified by name and on the Boolean that specifies whether to use the user-selected culture settings from the system.</summary>
		/// <param name="name">A predefined <see cref="T:System.Globalization.CultureInfo" /> name, <see cref="P:System.Globalization.CultureInfo.Name" /> of an existing <see cref="T:System.Globalization.CultureInfo" />, or Windows-only culture name. <paramref name="name" /> is not case-sensitive.</param>
		/// <param name="useUserOverride">A Boolean that denotes whether to use the user-selected culture settings (true) or the default culture settings (false). </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="name" /> is not a valid culture name.</exception>
		// Token: 0x06003452 RID: 13394 RVA: 0x000C0236 File Offset: 0x000BE436
		public CultureInfo(string name, bool useUserOverride)
			: this(name, useUserOverride, false)
		{
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x000C0244 File Offset: 0x000BE444
		private CultureInfo(string name, bool useUserOverride, bool read_only)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.constructed = true;
			this.m_isReadOnly = read_only;
			this.m_useUserOverride = useUserOverride;
			this.m_isInherited = base.GetType() != typeof(CultureInfo);
			if (name.Length == 0)
			{
				this.m_cultureData = CultureData.Invariant;
				this.ConstructInvariant(read_only);
				return;
			}
			if (!this.construct_internal_locale_from_name(name.ToLowerInvariant()))
			{
				throw CultureInfo.CreateNotFoundException(name);
			}
			CultureInfo.Data textInfoData = this.GetTextInfoData();
			string name2 = this.m_name;
			int num = this.datetime_index;
			int calendarType = this.CalendarType;
			int num2 = this.number_index;
			string text = this.iso2lang;
			int ansi = textInfoData.ansi;
			int oem = textInfoData.oem;
			int mac = textInfoData.mac;
			int ebcdic = textInfoData.ebcdic;
			bool right_to_left = textInfoData.right_to_left;
			char list_sep = (char)textInfoData.list_sep;
			this.m_cultureData = CultureData.GetCultureData(name2, useUserOverride, num, calendarType, num2, text, ansi, oem, mac, ebcdic, right_to_left, list_sep.ToString());
		}

		// Token: 0x06003454 RID: 13396 RVA: 0x000C0322 File Offset: 0x000BE522
		private CultureInfo()
		{
			this.constructed = true;
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x000C0331 File Offset: 0x000BE531
		private static void insert_into_shared_tables(CultureInfo c)
		{
			if (CultureInfo.shared_by_number == null)
			{
				CultureInfo.shared_by_number = new Dictionary<int, CultureInfo>();
				CultureInfo.shared_by_name = new Dictionary<string, CultureInfo>();
			}
			CultureInfo.shared_by_number[c.cultureID] = c;
			CultureInfo.shared_by_name[c.m_name] = c;
		}

		/// <summary>Retrieves a cached, read-only instance of a culture by using the specified culture identifier.</summary>
		/// <returns>A read-only <see cref="T:System.Globalization.CultureInfo" /> object.</returns>
		/// <param name="culture">A locale identifier (LCID).</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="culture" /> is less than zero.</exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="culture" /> specifies a culture that is not supported.</exception>
		// Token: 0x06003456 RID: 13398 RVA: 0x000C0370 File Offset: 0x000BE570
		public static CultureInfo GetCultureInfo(int culture)
		{
			if (culture < 1)
			{
				throw new ArgumentOutOfRangeException("culture", "Positive number required.");
			}
			object obj = CultureInfo.shared_table_lock;
			CultureInfo cultureInfo2;
			lock (obj)
			{
				CultureInfo cultureInfo;
				if (CultureInfo.shared_by_number != null && CultureInfo.shared_by_number.TryGetValue(culture, out cultureInfo))
				{
					cultureInfo2 = cultureInfo;
				}
				else
				{
					cultureInfo = new CultureInfo(culture, false, true);
					CultureInfo.insert_into_shared_tables(cultureInfo);
					cultureInfo2 = cultureInfo;
				}
			}
			return cultureInfo2;
		}

		/// <summary>Retrieves a cached, read-only instance of a culture using the specified culture name. </summary>
		/// <returns>A read-only <see cref="T:System.Globalization.CultureInfo" /> object.</returns>
		/// <param name="name">The name of a culture. <paramref name="name" /> is not case-sensitive.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="name" /> specifies a culture that is not supported.</exception>
		// Token: 0x06003457 RID: 13399 RVA: 0x000C03EC File Offset: 0x000BE5EC
		public static CultureInfo GetCultureInfo(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			object obj = CultureInfo.shared_table_lock;
			CultureInfo cultureInfo2;
			lock (obj)
			{
				CultureInfo cultureInfo;
				if (CultureInfo.shared_by_name != null && CultureInfo.shared_by_name.TryGetValue(name, out cultureInfo))
				{
					cultureInfo2 = cultureInfo;
				}
				else
				{
					cultureInfo = new CultureInfo(name, false, true);
					CultureInfo.insert_into_shared_tables(cultureInfo);
					cultureInfo2 = cultureInfo;
				}
			}
			return cultureInfo2;
		}

		/// <summary>Retrieves a cached, read-only instance of a culture. Parameters specify a culture that is initialized with the <see cref="T:System.Globalization.TextInfo" /> and <see cref="T:System.Globalization.CompareInfo" /> objects specified by another culture.</summary>
		/// <returns>A read-only <see cref="T:System.Globalization.CultureInfo" /> object.</returns>
		/// <param name="name">The name of a culture. <paramref name="name" /> is not case-sensitive.</param>
		/// <param name="altName">The name of a culture that supplies the <see cref="T:System.Globalization.TextInfo" /> and <see cref="T:System.Globalization.CompareInfo" /> objects used to initialize <paramref name="name" />. <paramref name="altName" /> is not case-sensitive.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="altName" /> is null.</exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="name" /> or <paramref name="altName" /> specifies a culture that is not supported.</exception>
		// Token: 0x06003458 RID: 13400 RVA: 0x000C0460 File Offset: 0x000BE660
		[MonoTODO("Currently it ignores the altName parameter")]
		public static CultureInfo GetCultureInfo(string name, string altName)
		{
			if (name == null)
			{
				throw new ArgumentNullException("null");
			}
			if (altName == null)
			{
				throw new ArgumentNullException("null");
			}
			return CultureInfo.GetCultureInfo(name);
		}

		/// <summary>Deprecated. Retrieves a read-only <see cref="T:System.Globalization.CultureInfo" /> object having linguistic characteristics that are identified by the specified RFC 4646 language tag.</summary>
		/// <returns>A read-only <see cref="T:System.Globalization.CultureInfo" /> object.</returns>
		/// <param name="name">The name of a language as specified by the RFC 4646 standard.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="name" /> does not correspond to a supported culture.</exception>
		// Token: 0x06003459 RID: 13401 RVA: 0x000C0484 File Offset: 0x000BE684
		public static CultureInfo GetCultureInfoByIetfLanguageTag(string name)
		{
			if (name == "zh-Hans")
			{
				return CultureInfo.GetCultureInfo("zh-CHS");
			}
			if (!(name == "zh-Hant"))
			{
				return CultureInfo.GetCultureInfo(name);
			}
			return CultureInfo.GetCultureInfo("zh-CHT");
		}

		// Token: 0x0600345A RID: 13402 RVA: 0x000C04C0 File Offset: 0x000BE6C0
		internal static CultureInfo CreateCulture(string name, bool reference)
		{
			bool flag = name.Length == 0;
			bool flag2;
			bool flag3;
			if (reference)
			{
				flag2 = !flag;
				flag3 = false;
			}
			else
			{
				flag3 = false;
				flag2 = !flag;
			}
			return new CultureInfo(name, flag2, flag3);
		}

		/// <summary>Creates a <see cref="T:System.Globalization.CultureInfo" /> that represents the specific culture that is associated with the specified name.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> object that represents:The invariant culture, if <paramref name="name" /> is an empty string ("").-or- The specific culture associated with <paramref name="name" />, if <paramref name="name" /> is a neutral culture.-or- The culture specified by <paramref name="name" />, if <paramref name="name" /> is already a specific culture.</returns>
		/// <param name="name">A predefined <see cref="T:System.Globalization.CultureInfo" /> name or the name of an existing <see cref="T:System.Globalization.CultureInfo" /> object. <paramref name="name" /> is not case-sensitive.</param>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="name" /> is not a valid culture name.-or- The culture specified by <paramref name="name" /> does not have a specific culture associated with it. </exception>
		/// <exception cref="T:System.NullReferenceException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x0600345B RID: 13403 RVA: 0x000C04F8 File Offset: 0x000BE6F8
		public static CultureInfo CreateSpecificCulture(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				return CultureInfo.InvariantCulture;
			}
			string text = name;
			name = name.ToLowerInvariant();
			CultureInfo cultureInfo = new CultureInfo();
			if (!cultureInfo.construct_internal_locale_from_name(name))
			{
				int num = name.Length - 1;
				if (num > 0)
				{
					while ((num = name.LastIndexOf('-', num - 1)) > 0 && !cultureInfo.construct_internal_locale_from_name(name.Substring(0, num)))
					{
					}
				}
				if (num <= 0)
				{
					throw CultureInfo.CreateNotFoundException(text);
				}
			}
			if (cultureInfo.IsNeutralCulture)
			{
				cultureInfo = CultureInfo.CreateSpecificCultureFromNeutral(cultureInfo.Name);
			}
			CultureInfo.Data textInfoData = cultureInfo.GetTextInfoData();
			CultureInfo cultureInfo2 = cultureInfo;
			string name2 = cultureInfo.m_name;
			bool flag = false;
			int num2 = cultureInfo.datetime_index;
			int calendarType = cultureInfo.CalendarType;
			int num3 = cultureInfo.number_index;
			string text2 = cultureInfo.iso2lang;
			int ansi = textInfoData.ansi;
			int oem = textInfoData.oem;
			int mac = textInfoData.mac;
			int ebcdic = textInfoData.ebcdic;
			bool right_to_left = textInfoData.right_to_left;
			char list_sep = (char)textInfoData.list_sep;
			cultureInfo2.m_cultureData = CultureData.GetCultureData(name2, flag, num2, calendarType, num3, text2, ansi, oem, mac, ebcdic, right_to_left, list_sep.ToString());
			return cultureInfo;
		}

		// Token: 0x0600345C RID: 13404 RVA: 0x000C05E8 File Offset: 0x000BE7E8
		private static CultureInfo CreateSpecificCultureFromNeutral(string name)
		{
			string text = name.ToLowerInvariant();
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			int num2;
			if (num <= 1344898993U)
			{
				if (num <= 1128614327U)
				{
					if (num <= 1025408520U)
					{
						if (num <= 975938470U)
						{
							if (num <= 926444256U)
							{
								if (num <= 896475900U)
								{
									if (num != 275533995U)
									{
										if (num == 896475900U)
										{
											if (text == "arn")
											{
												num2 = 1146;
												goto IL_1B49;
											}
										}
									}
									else if (text == "nso")
									{
										num2 = 1132;
										goto IL_1B49;
									}
								}
								else if (num != 925484199U)
								{
									if (num == 926444256U)
									{
										if (text == "id")
										{
											num2 = 1057;
											goto IL_1B49;
										}
									}
								}
								else if (text == "mn-cyrl")
								{
									num2 = 1104;
									goto IL_1B49;
								}
							}
							else if (num <= 944060518U)
							{
								if (num != 942383232U)
								{
									if (num == 944060518U)
									{
										if (text == "ta")
										{
											num2 = 1097;
											goto IL_1B49;
										}
									}
								}
								else if (text == "be")
								{
									num2 = 1059;
									goto IL_1B49;
								}
							}
							else if (num != 944899161U)
							{
								if (num == 975938470U)
								{
									if (text == "bg")
									{
										num2 = 1026;
										goto IL_1B49;
									}
								}
							}
							else if (text == "sa")
							{
								num2 = 1103;
								goto IL_1B49;
							}
						}
						else if (num <= 996684602U)
						{
							if (num <= 977615756U)
							{
								if (num != 976777113U)
								{
									if (num == 977615756U)
									{
										if (text == "tg")
										{
											num2 = 1064;
											goto IL_1B49;
										}
									}
								}
								else if (text == "ig")
								{
									num2 = 1136;
									goto IL_1B49;
								}
							}
							else if (num != 991980614U)
							{
								if (num == 996684602U)
								{
									if (text == "mn-mong")
									{
										num2 = 2128;
										goto IL_1B49;
									}
								}
							}
							else if (text == "gd")
							{
								num2 = 1169;
								goto IL_1B49;
							}
						}
						else if (num <= 1011170994U)
						{
							if (num != 1009493708U)
							{
								if (num == 1011170994U)
								{
									if (text == "te")
									{
										num2 = 1098;
										goto IL_1B49;
									}
								}
							}
							else if (text == "ba")
							{
								num2 = 1133;
								goto IL_1B49;
							}
						}
						else if (num != 1011465184U)
						{
							if (num != 1012009637U)
							{
								if (num == 1025408520U)
								{
									if (text == "tzm-latn")
									{
										num2 = 2143;
										goto IL_1B49;
									}
								}
							}
							else if (text == "se")
							{
								num2 = 1083;
								goto IL_1B49;
							}
						}
						else if (text == "vi")
						{
							num2 = 1066;
							goto IL_1B49;
						}
					}
					else if (num <= 1092248970U)
					{
						if (num <= 1058693732U)
						{
							if (num <= 1044726232U)
							{
								if (num != 1044181779U)
								{
									if (num == 1044726232U)
									{
										if (text == "tk")
										{
											num2 = 1090;
											goto IL_1B49;
										}
									}
								}
								else if (text == "kk")
								{
									num2 = 1087;
									goto IL_1B49;
								}
							}
							else if (num != 1045564875U)
							{
								if (num == 1058693732U)
								{
									if (text == "el")
									{
										num2 = 1032;
										goto IL_1B49;
									}
								}
							}
							else if (text == "sk")
							{
								num2 = 1051;
								goto IL_1B49;
							}
						}
						else if (num <= 1076162899U)
						{
							if (num != 1075868709U)
							{
								if (num == 1076162899U)
								{
									if (text == "am")
									{
										num2 = 1118;
										goto IL_1B49;
									}
								}
							}
							else if (text == "ga")
							{
								num2 = 2108;
								goto IL_1B49;
							}
						}
						else if (num != 1079120113U)
						{
							if (num != 1087741671U)
							{
								if (num == 1092248970U)
								{
									if (text == "en")
									{
										num2 = 1033;
										goto IL_1B49;
									}
								}
							}
							else if (text == "az-cyrl")
							{
								num2 = 2092;
								goto IL_1B49;
							}
						}
						else if (text == "si")
						{
							num2 = 1115;
							goto IL_1B49;
						}
					}
					else if (num <= 1110556780U)
					{
						if (num <= 1095059089U)
						{
							if (num != 1094514636U)
							{
								if (num == 1095059089U)
								{
									if (text == "th")
									{
										num2 = 1054;
										goto IL_1B49;
									}
								}
							}
							else if (text == "kn")
							{
								num2 = 1099;
								goto IL_1B49;
							}
						}
						else if (num != 1110159422U)
						{
							if (num == 1110556780U)
							{
								if (text == "lo")
								{
									num2 = 1108;
									goto IL_1B49;
								}
							}
						}
						else if (text == "bo")
						{
							num2 = 1105;
							goto IL_1B49;
						}
					}
					else if (num <= 1126201566U)
					{
						if (num != 1111292255U)
						{
							if (num == 1126201566U)
							{
								if (text == "gl")
								{
									num2 = 1110;
									goto IL_1B49;
								}
							}
						}
						else if (text == "ko")
						{
							num2 = 1042;
							goto IL_1B49;
						}
					}
					else if (num != 1126937041U)
					{
						if (num != 1128069874U)
						{
							if (num == 1128614327U)
							{
								if (text == "tn")
								{
									num2 = 1074;
									goto IL_1B49;
								}
							}
						}
						else if (text == "kl")
						{
							num2 = 1135;
							goto IL_1B49;
						}
					}
					else if (text == "bn")
					{
						num2 = 1093;
						goto IL_1B49;
					}
				}
				else if (num <= 1213341065U)
				{
					if (num <= 1177122803U)
					{
						if (num <= 1162022470U)
						{
							if (num <= 1144553303U)
							{
								if (num != 1129452970U)
								{
									if (num == 1144553303U)
									{
										if (text == "ii")
										{
											num2 = 1144;
											goto IL_1B49;
										}
									}
								}
								else if (text == "sl")
								{
									num2 = 1060;
									goto IL_1B49;
								}
							}
							else if (num != 1144847493U)
							{
								if (num == 1162022470U)
								{
									if (text == "ur")
									{
										num2 = 1056;
										goto IL_1B49;
									}
								}
							}
							else if (text == "km")
							{
								num2 = 1107;
								goto IL_1B49;
							}
						}
						else if (num <= 1163008208U)
						{
							if (num != 1162757945U)
							{
								if (num == 1163008208U)
								{
									if (text == "sr")
									{
										num2 = 9242;
										goto IL_1B49;
									}
								}
							}
							else if (text == "pl")
							{
								num2 = 1045;
								goto IL_1B49;
							}
						}
						else if (num != 1164435231U)
						{
							if (num != 1176137065U)
							{
								if (num == 1177122803U)
								{
									if (text == "cs")
									{
										num2 = 1029;
										goto IL_1B49;
									}
								}
							}
							else if (text == "es")
							{
								num2 = 3082;
								goto IL_1B49;
							}
						}
						else if (text == "zh")
						{
							num2 = 2052;
							goto IL_1B49;
						}
					}
					else if (num <= 1195724803U)
					{
						if (num <= 1194444875U)
						{
							if (num != 1192914684U)
							{
								if (num == 1194444875U)
								{
									if (text == "lb")
									{
										num2 = 1134;
										goto IL_1B49;
									}
								}
							}
							else if (text == "et")
							{
								num2 = 1061;
								goto IL_1B49;
							}
						}
						else if (num != 1194886160U)
						{
							if (num == 1195724803U)
							{
								if (text == "tr")
								{
									num2 = 1055;
									goto IL_1B49;
								}
							}
						}
						else if (text == "it")
						{
							num2 = 1040;
							goto IL_1B49;
						}
					}
					else if (num <= 1211324057U)
					{
						if (num != 1209692303U)
						{
							if (num == 1211324057U)
							{
								if (text == "iu-cans")
								{
									num2 = 1117;
									goto IL_1B49;
								}
							}
						}
						else if (text == "eu")
						{
							num2 = 1069;
							goto IL_1B49;
						}
					}
					else if (num != 1211663779U)
					{
						if (num != 1211957969U)
						{
							if (num == 1213341065U)
							{
								if (text == "sq")
								{
									num2 = 1052;
									goto IL_1B49;
								}
							}
						}
						else if (text == "ka")
						{
							num2 = 1079;
							goto IL_1B49;
						}
					}
					else if (text == "iu")
					{
						num2 = 2141;
						goto IL_1B49;
					}
				}
				else if (num <= 1277200137U)
				{
					if (num <= 1231251517U)
					{
						if (num <= 1227161470U)
						{
							if (num != 1213488160U)
							{
								if (num == 1227161470U)
								{
									if (text == "af")
									{
										num2 = 1078;
										goto IL_1B49;
									}
								}
							}
							else if (text == "ru")
							{
								num2 = 1049;
								goto IL_1B49;
							}
						}
						else if (num != 1230118684U)
						{
							if (num == 1231251517U)
							{
								if (text == "xh")
								{
									num2 = 1076;
									goto IL_1B49;
								}
							}
						}
						else if (text == "sv")
						{
							num2 = 1053;
							goto IL_1B49;
						}
					}
					else if (num <= 1246896303U)
					{
						if (num != 1237973804U)
						{
							if (num == 1246896303U)
							{
								if (text == "sw")
								{
									num2 = 1089;
									goto IL_1B49;
								}
							}
						}
						else if (text == "uz-latn")
						{
							num2 = 1091;
							goto IL_1B49;
						}
					}
					else if (num != 1247043398U)
					{
						if (num != 1260172255U)
						{
							if (num == 1277200137U)
							{
								if (text == "gu")
								{
									num2 = 1095;
									goto IL_1B49;
								}
							}
						}
						else if (text == "dv")
						{
							num2 = 1125;
							goto IL_1B49;
						}
					}
					else if (text == "rw")
					{
						num2 = 1159;
						goto IL_1B49;
					}
				}
				else if (num <= 1296390517U)
				{
					if (num <= 1278921350U)
					{
						if (num != 1277347232U)
						{
							if (num == 1278921350U)
							{
								if (text == "hu")
								{
									num2 = 1038;
									goto IL_1B49;
								}
							}
						}
						else if (text == "fy")
						{
							num2 = 1122;
							goto IL_1B49;
						}
					}
					else if (num != 1296243422U)
					{
						if (num == 1296390517U)
						{
							if (text == "tt")
							{
								num2 = 1092;
								goto IL_1B49;
							}
						}
					}
					else if (text == "uz")
					{
						num2 = 1091;
						goto IL_1B49;
					}
				}
				else if (num <= 1312329493U)
				{
					if (num != 1311490850U)
					{
						if (num == 1312329493U)
						{
							if (text == "is")
							{
								num2 = 1039;
								goto IL_1B49;
							}
						}
					}
					else if (text == "bs")
					{
						num2 = 5146;
						goto IL_1B49;
					}
				}
				else if (num != 1328268469U)
				{
					if (num != 1329254207U)
					{
						if (num == 1344898993U)
						{
							if (text == "cy")
							{
								num2 = 1106;
								goto IL_1B49;
							}
						}
					}
					else if (text == "hr")
					{
						num2 = 1050;
						goto IL_1B49;
					}
				}
				else if (text == "br")
				{
					num2 = 1150;
					goto IL_1B49;
				}
			}
			else if (num <= 1646454850U)
			{
				if (num <= 1545391778U)
				{
					if (num <= 1462636516U)
					{
						if (num <= 1428492898U)
						{
							if (num <= 1347311754U)
							{
								if (num != 1346178921U)
								{
									if (num == 1347311754U)
									{
										if (text == "pa")
										{
											num2 = 1094;
											goto IL_1B49;
										}
									}
								}
								else if (text == "ky")
								{
									num2 = 1088;
									goto IL_1B49;
								}
							}
							else if (num != 1424802581U)
							{
								if (num == 1428492898U)
								{
									if (text == "az")
									{
										num2 = 1068;
										goto IL_1B49;
									}
								}
							}
							else if (text == "tg-cyrl")
							{
								num2 = 1064;
								goto IL_1B49;
							}
						}
						else if (num <= 1429850248U)
						{
							if (num != 1429081278U)
							{
								if (num == 1429850248U)
								{
									if (text == "gsw")
									{
										num2 = 1156;
										goto IL_1B49;
									}
								}
							}
							else if (text == "mr")
							{
								num2 = 1102;
								goto IL_1B49;
							}
						}
						else if (num != 1445858897U)
						{
							if (num != 1461901041U)
							{
								if (num == 1462636516U)
								{
									if (text == "mt")
									{
										num2 = 1082;
										goto IL_1B49;
									}
								}
							}
							else if (text == "fr")
							{
								num2 = 1036;
								goto IL_1B49;
							}
						}
						else if (text == "ms")
						{
							num2 = 1086;
							goto IL_1B49;
						}
					}
					else if (num <= 1479958588U)
					{
						if (num <= 1478281302U)
						{
							if (num != 1463180969U)
							{
								if (num == 1478281302U)
								{
									if (text == "da")
									{
										num2 = 1030;
										goto IL_1B49;
									}
								}
							}
							else if (text == "nb")
							{
								num2 = 1044;
								goto IL_1B49;
							}
						}
						else if (num != 1479119945U)
						{
							if (num == 1479958588U)
							{
								if (text == "ne")
								{
									num2 = 1121;
									goto IL_1B49;
								}
							}
						}
						else if (text == "ca")
						{
							num2 = 1027;
							goto IL_1B49;
						}
					}
					else if (num <= 1483209992U)
					{
						if (num != 1480252778U)
						{
							if (num == 1483209992U)
							{
								if (text == "zu")
								{
									num2 = 1077;
									goto IL_1B49;
								}
							}
						}
						else if (text == "hy")
						{
							num2 = 1067;
							goto IL_1B49;
						}
					}
					else if (num != 1514352469U)
					{
						if (num != 1529997255U)
						{
							if (num == 1545391778U)
							{
								if (text == "de")
								{
									num2 = 1031;
									goto IL_1B49;
								}
							}
						}
						else if (text == "lv")
						{
							num2 = 1062;
							goto IL_1B49;
						}
					}
					else if (text == "ug")
					{
						num2 = 1152;
						goto IL_1B49;
					}
				}
				else if (num <= 1579491469U)
				{
					if (num <= 1551553596U)
					{
						if (num <= 1546524611U)
						{
							if (num != 1545789136U)
							{
								if (num == 1546524611U)
								{
									if (text == "mi")
									{
										num2 = 1153;
										goto IL_1B49;
									}
								}
							}
							else if (text == "fi")
							{
								num2 = 1035;
								goto IL_1B49;
							}
						}
						else if (num != 1547363254U)
						{
							if (num == 1551553596U)
							{
								if (text == "prs")
								{
									num2 = 1164;
									goto IL_1B49;
								}
							}
						}
						else if (text == "he")
						{
							num2 = 1037;
							goto IL_1B49;
						}
					}
					else if (num <= 1563552493U)
					{
						if (num != 1562713850U)
						{
							if (num == 1563552493U)
							{
								if (text == "lt")
								{
									num2 = 1063;
									goto IL_1B49;
								}
							}
						}
						else if (text == "ar")
						{
							num2 = 1025;
							goto IL_1B49;
						}
					}
					else if (num != 1563699588U)
					{
						if (num != 1565420801U)
						{
							if (num == 1579491469U)
							{
								if (text == "as")
								{
									num2 = 1101;
									goto IL_1B49;
								}
							}
						}
						else if (text == "pt")
						{
							num2 = 1046;
							goto IL_1B49;
						}
					}
					else if (text == "or")
					{
						num2 = 1096;
						goto IL_1B49;
					}
				}
				else if (num <= 1596857468U)
				{
					if (num <= 1581462945U)
					{
						if (num != 1580079849U)
						{
							if (num == 1581462945U)
							{
								if (text == "uk")
								{
									num2 = 1058;
									goto IL_1B49;
								}
							}
						}
						else if (text == "mk")
						{
							num2 = 1071;
							goto IL_1B49;
						}
					}
					else if (num != 1582198420U)
					{
						if (num == 1596857468U)
						{
							if (text == "ml")
							{
								num2 = 1100;
								goto IL_1B49;
							}
						}
					}
					else if (text == "ps")
					{
						num2 = 1123;
						goto IL_1B49;
					}
				}
				else if (num <= 1616151016U)
				{
					if (num != 1614473730U)
					{
						if (num == 1616151016U)
						{
							if (text == "rm")
							{
								num2 = 1047;
								goto IL_1B49;
							}
						}
					}
					else if (text == "ha")
					{
						num2 = 1128;
						goto IL_1B49;
					}
				}
				else if (num != 1630412706U)
				{
					if (num != 1630957159U)
					{
						if (num == 1646454850U)
						{
							if (text == "fo")
							{
								num2 = 1080;
								goto IL_1B49;
							}
						}
					}
					else if (text == "nl")
					{
						num2 = 1043;
						goto IL_1B49;
					}
				}
				else if (text == "mn")
				{
					num2 = 1104;
					goto IL_1B49;
				}
			}
			else if (num <= 3012500870U)
			{
				if (num <= 1748694682U)
				{
					if (num <= 1649706254U)
					{
						if (num <= 1647734778U)
						{
							if (num != 1646896135U)
							{
								if (num == 1647734778U)
								{
									if (text == "no")
									{
										num2 = 1044;
										goto IL_1B49;
									}
								}
							}
							else if (text == "co")
							{
								num2 = 1155;
								goto IL_1B49;
							}
						}
						else if (num != 1648867611U)
						{
							if (num == 1649706254U)
							{
								if (text == "ro")
								{
									num2 = 1048;
									goto IL_1B49;
								}
							}
						}
						else if (text == "wo")
						{
							num2 = 1160;
							goto IL_1B49;
						}
					}
					else if (num <= 1664512397U)
					{
						if (num != 1650441729U)
						{
							if (num == 1664512397U)
							{
								if (text == "nn")
								{
									num2 = 2068;
									goto IL_1B49;
								}
							}
						}
						else if (text == "yo")
						{
							num2 = 1130;
							goto IL_1B49;
						}
					}
					else if (num != 1680010088U)
					{
						if (num != 1680473867U)
						{
							if (num == 1748694682U)
							{
								if (text == "hi")
								{
									num2 = 1081;
									goto IL_1B49;
								}
							}
						}
						else if (text == "iu-latn")
						{
							num2 = 2141;
							goto IL_1B49;
						}
					}
					else if (text == "fa")
					{
						num2 = 1065;
						goto IL_1B49;
					}
				}
				else if (num <= 2046577884U)
				{
					if (num <= 1816099348U)
					{
						if (num != 1790977000U)
						{
							if (num == 1816099348U)
							{
								if (text == "ja")
								{
									num2 = 1041;
									goto IL_1B49;
								}
							}
						}
						else if (text == "bs-latn")
						{
							num2 = 5146;
							goto IL_1B49;
						}
					}
					else if (num != 1848919111U)
					{
						if (num == 2046577884U)
						{
							if (text == "kok")
							{
								num2 = 1111;
								goto IL_1B49;
							}
						}
					}
					else if (text == "oc")
					{
						num2 = 1154;
						goto IL_1B49;
					}
				}
				else
				{
					if (num <= 2197937899U)
					{
						if (num != 2180460995U)
						{
							if (num != 2197937899U)
							{
								goto IL_1B38;
							}
							if (!(text == "zh-hant"))
							{
								goto IL_1B38;
							}
						}
						else if (!(text == "zh-cht"))
						{
							goto IL_1B38;
						}
						num2 = 3076;
						goto IL_1B49;
					}
					if (num != 2264349090U)
					{
						if (num != 2281825994U)
						{
							if (num != 3012500870U)
							{
								goto IL_1B38;
							}
							if (!(text == "sr-latn"))
							{
								goto IL_1B38;
							}
							num2 = 9242;
							goto IL_1B49;
						}
						else if (!(text == "zh-hans"))
						{
							goto IL_1B38;
						}
					}
					else if (!(text == "zh-chs"))
					{
						goto IL_1B38;
					}
					num2 = 2052;
					goto IL_1B49;
				}
			}
			else if (num <= 3795602801U)
			{
				if (num <= 3294142633U)
				{
					if (num <= 3224459074U)
					{
						if (num != 3174420263U)
						{
							if (num == 3224459074U)
							{
								if (text == "tzm")
								{
									num2 = 2143;
									goto IL_1B49;
								}
							}
						}
						else if (text == "bs-cyrl")
						{
							num2 = 8218;
							goto IL_1B49;
						}
					}
					else if (num != 3240320582U)
					{
						if (num == 3294142633U)
						{
							if (text == "syr")
							{
								num2 = 1114;
								goto IL_1B49;
							}
						}
					}
					else if (text == "dsb")
					{
						num2 = 2094;
						goto IL_1B49;
					}
				}
				else if (num <= 3659307299U)
				{
					if (num != 3336872436U)
					{
						if (num == 3659307299U)
						{
							if (text == "sah")
							{
								num2 = 1157;
								goto IL_1B49;
							}
						}
					}
					else if (text == "fil")
					{
						num2 = 1124;
						goto IL_1B49;
					}
				}
				else if (num != 3678056394U)
				{
					if (num != 3761944489U)
					{
						if (num == 3795602801U)
						{
							if (text == "sr-cyrl")
							{
								num2 = 10266;
								goto IL_1B49;
							}
						}
					}
					else if (text == "smn")
					{
						num2 = 9275;
						goto IL_1B49;
					}
				}
				else if (text == "sms")
				{
					num2 = 8251;
					goto IL_1B49;
				}
			}
			else if (num <= 3953034599U)
			{
				if (num <= 3912943060U)
				{
					if (num != 3829054965U)
					{
						if (num == 3912943060U)
						{
							if (text == "sma")
							{
								num2 = 7227;
								goto IL_1B49;
							}
						}
					}
					else if (text == "smj")
					{
						num2 = 5179;
						goto IL_1B49;
					}
				}
				else if (num != 3918412059U)
				{
					if (num == 3953034599U)
					{
						if (text == "moh")
						{
							num2 = 1148;
							goto IL_1B49;
						}
					}
				}
				else if (text == "uz-cyrl")
				{
					num2 = 2115;
					goto IL_1B49;
				}
			}
			else if (num <= 4041297251U)
			{
				if (num != 3999162536U)
				{
					if (num == 4041297251U)
					{
						if (text == "quz")
						{
							num2 = 1131;
							goto IL_1B49;
						}
					}
				}
				else if (text == "az-latn")
				{
					num2 = 1068;
					goto IL_1B49;
				}
			}
			else if (num != 4103207754U)
			{
				if (num != 4276183917U)
				{
					if (num == 4280271688U)
					{
						if (text == "ha-latn")
						{
							num2 = 1128;
							goto IL_1B49;
						}
					}
				}
				else if (text == "qut")
				{
					num2 = 1158;
					goto IL_1B49;
				}
			}
			else if (text == "hsb")
			{
				num2 = 1070;
				goto IL_1B49;
			}
			IL_1B38:
			throw new NotImplementedException("Mapping for neutral culture " + name);
			IL_1B49:
			return new CultureInfo(num2);
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x0600345D RID: 13405 RVA: 0x000C2144 File Offset: 0x000C0344
		internal int CalendarType
		{
			get
			{
				switch (this.default_calendar_type >> 8)
				{
				case 1:
					return 1;
				case 2:
					return 7;
				case 3:
					return 23;
				case 4:
					return 6;
				default:
					throw new NotImplementedException("CalendarType");
				}
			}
		}

		// Token: 0x0600345E RID: 13406 RVA: 0x000C2188 File Offset: 0x000C0388
		private static Calendar CreateCalendar(int calendarType)
		{
			string text;
			switch (calendarType >> 8)
			{
			case 1:
				return new GregorianCalendar((GregorianCalendarTypes)(calendarType & 255));
			case 2:
				text = "System.Globalization.ThaiBuddhistCalendar";
				break;
			case 3:
				text = "System.Globalization.UmAlQuraCalendar";
				break;
			case 4:
				text = "System.Globalization.HijriCalendar";
				break;
			default:
				throw new NotImplementedException("Unknown calendar type: " + calendarType);
			}
			Type type = Type.GetType(text, false);
			if (type == null)
			{
				return new GregorianCalendar(GregorianCalendarTypes.Localized);
			}
			return (Calendar)Activator.CreateInstance(type);
		}

		// Token: 0x0600345F RID: 13407 RVA: 0x000C2213 File Offset: 0x000C0413
		private static Exception CreateNotFoundException(string name)
		{
			return new CultureNotFoundException("name", "Culture name " + name + " is not supported.");
		}

		/// <summary>Gets or sets the default culture for threads in the current application domain.</summary>
		/// <returns>The default culture for threads in the current application domain, or null if the current system culture is the default thread culture in the application domain.</returns>
		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06003460 RID: 13408 RVA: 0x000C222F File Offset: 0x000C042F
		// (set) Token: 0x06003461 RID: 13409 RVA: 0x000C2238 File Offset: 0x000C0438
		public static CultureInfo DefaultThreadCurrentCulture
		{
			get
			{
				return CultureInfo.s_DefaultThreadCurrentCulture;
			}
			set
			{
				CultureInfo.s_DefaultThreadCurrentCulture = value;
			}
		}

		/// <summary>Gets or sets the default UI culture for threads in the current application domain.</summary>
		/// <returns>The default UI culture for threads in the current application domain, or null if the current system UI culture is the default thread UI culture in the application domain.</returns>
		/// <exception cref="T:System.ArgumentException">In a set operation, the <see cref="P:System.Globalization.CultureInfo.Name" /> property value is invalid. </exception>
		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06003462 RID: 13410 RVA: 0x000C2242 File Offset: 0x000C0442
		// (set) Token: 0x06003463 RID: 13411 RVA: 0x000C224B File Offset: 0x000C044B
		public static CultureInfo DefaultThreadCurrentUICulture
		{
			get
			{
				return CultureInfo.s_DefaultThreadCurrentUICulture;
			}
			set
			{
				CultureInfo.s_DefaultThreadCurrentUICulture = value;
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06003464 RID: 13412 RVA: 0x000BF968 File Offset: 0x000BDB68
		internal string SortName
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06003465 RID: 13413 RVA: 0x000C2255 File Offset: 0x000C0455
		internal static CultureInfo UserDefaultUICulture
		{
			get
			{
				return CultureInfo.ConstructCurrentUICulture();
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06003466 RID: 13414 RVA: 0x000BF140 File Offset: 0x000BD340
		internal static CultureInfo UserDefaultCulture
		{
			get
			{
				return CultureInfo.ConstructCurrentCulture();
			}
		}

		// Token: 0x06003467 RID: 13415 RVA: 0x000C225C File Offset: 0x000C045C
		internal static void CheckDomainSafetyObject(object obj, object container)
		{
			if (obj.GetType().Assembly != typeof(CultureInfo).Assembly)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Cannot set sub-classed {0} object to {1} object."), obj.GetType(), container.GetType()));
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06003468 RID: 13416 RVA: 0x000C22B0 File Offset: 0x000C04B0
		internal bool HasInvariantCultureName
		{
			get
			{
				return this.Name == CultureInfo.InvariantCulture.Name;
			}
		}

		// Token: 0x06003469 RID: 13417 RVA: 0x000C22C8 File Offset: 0x000C04C8
		internal static bool VerifyCultureName(string cultureName, bool throwException)
		{
			int i = 0;
			while (i < cultureName.Length)
			{
				char c = cultureName[i];
				if (!char.IsLetterOrDigit(c) && c != '-' && c != '_')
				{
					if (throwException)
					{
						throw new ArgumentException(Environment.GetResourceString("The given culture name '{0}' cannot be used to locate a resource file. Resource filenames must consist of only letters, numbers, hyphens or underscores.", new object[] { cultureName }));
					}
					return false;
				}
				else
				{
					i++;
				}
			}
			return true;
		}

		// Token: 0x0600346A RID: 13418 RVA: 0x000C2320 File Offset: 0x000C0520
		internal static bool VerifyCultureName(CultureInfo culture, bool throwException)
		{
			return !culture.m_isInherited || CultureInfo.VerifyCultureName(culture.Name, throwException);
		}

		// Token: 0x04001BDE RID: 7134
		private static volatile CultureInfo invariant_culture_info = new CultureInfo(127, false, true);

		// Token: 0x04001BDF RID: 7135
		private static object shared_table_lock = new object();

		// Token: 0x04001BE0 RID: 7136
		private static CultureInfo default_current_culture;

		// Token: 0x04001BE1 RID: 7137
		private bool m_isReadOnly;

		// Token: 0x04001BE2 RID: 7138
		private int cultureID;

		// Token: 0x04001BE3 RID: 7139
		[NonSerialized]
		private int parent_lcid;

		// Token: 0x04001BE4 RID: 7140
		[NonSerialized]
		private int datetime_index;

		// Token: 0x04001BE5 RID: 7141
		[NonSerialized]
		private int number_index;

		// Token: 0x04001BE6 RID: 7142
		[NonSerialized]
		private int default_calendar_type;

		// Token: 0x04001BE7 RID: 7143
		private bool m_useUserOverride;

		// Token: 0x04001BE8 RID: 7144
		internal volatile NumberFormatInfo numInfo;

		// Token: 0x04001BE9 RID: 7145
		internal volatile DateTimeFormatInfo dateTimeInfo;

		// Token: 0x04001BEA RID: 7146
		private volatile TextInfo textInfo;

		// Token: 0x04001BEB RID: 7147
		internal string m_name;

		// Token: 0x04001BEC RID: 7148
		[NonSerialized]
		private string englishname;

		// Token: 0x04001BED RID: 7149
		[NonSerialized]
		private string nativename;

		// Token: 0x04001BEE RID: 7150
		[NonSerialized]
		private string iso3lang;

		// Token: 0x04001BEF RID: 7151
		[NonSerialized]
		private string iso2lang;

		// Token: 0x04001BF0 RID: 7152
		[NonSerialized]
		private string win3lang;

		// Token: 0x04001BF1 RID: 7153
		[NonSerialized]
		private string territory;

		// Token: 0x04001BF2 RID: 7154
		[NonSerialized]
		private string[] native_calendar_names;

		// Token: 0x04001BF3 RID: 7155
		private volatile CompareInfo compareInfo;

		// Token: 0x04001BF4 RID: 7156
		[NonSerialized]
		private unsafe readonly void* textinfo_data;

		// Token: 0x04001BF5 RID: 7157
		private int m_dataItem;

		// Token: 0x04001BF6 RID: 7158
		private Calendar calendar;

		// Token: 0x04001BF7 RID: 7159
		[NonSerialized]
		private CultureInfo parent_culture;

		// Token: 0x04001BF8 RID: 7160
		[NonSerialized]
		private bool constructed;

		// Token: 0x04001BF9 RID: 7161
		[NonSerialized]
		internal byte[] cached_serialized_form;

		// Token: 0x04001BFA RID: 7162
		[NonSerialized]
		internal CultureData m_cultureData;

		// Token: 0x04001BFB RID: 7163
		[NonSerialized]
		internal bool m_isInherited;

		// Token: 0x04001BFC RID: 7164
		internal const int InvariantCultureId = 127;

		// Token: 0x04001BFD RID: 7165
		private const int CalendarTypeBits = 8;

		// Token: 0x04001BFE RID: 7166
		private const string MSG_READONLY = "This instance is read only";

		// Token: 0x04001BFF RID: 7167
		private static volatile CultureInfo s_DefaultThreadCurrentUICulture;

		// Token: 0x04001C00 RID: 7168
		private static volatile CultureInfo s_DefaultThreadCurrentCulture;

		// Token: 0x04001C01 RID: 7169
		private static Dictionary<int, CultureInfo> shared_by_number;

		// Token: 0x04001C02 RID: 7170
		private static Dictionary<string, CultureInfo> shared_by_name;

		// Token: 0x04001C03 RID: 7171
		internal static readonly bool IsTaiwanSku;

		// Token: 0x02000444 RID: 1092
		private struct Data
		{
			// Token: 0x04001C04 RID: 7172
			public int ansi;

			// Token: 0x04001C05 RID: 7173
			public int ebcdic;

			// Token: 0x04001C06 RID: 7174
			public int mac;

			// Token: 0x04001C07 RID: 7175
			public int oem;

			// Token: 0x04001C08 RID: 7176
			public bool right_to_left;

			// Token: 0x04001C09 RID: 7177
			public byte list_sep;
		}
	}
}
