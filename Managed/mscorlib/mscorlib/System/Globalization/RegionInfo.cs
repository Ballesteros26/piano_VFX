using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Globalization
{
	/// <summary>Contains information about the country/region.</summary>
	// Token: 0x02000448 RID: 1096
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class RegionInfo
	{
		/// <summary>Gets the <see cref="T:System.Globalization.RegionInfo" /> that represents the country/region used by the current thread.</summary>
		/// <returns>The <see cref="T:System.Globalization.RegionInfo" /> that represents the country/region used by the current thread.</returns>
		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06003487 RID: 13447 RVA: 0x000C2E54 File Offset: 0x000C1054
		public static RegionInfo CurrentRegion
		{
			get
			{
				RegionInfo regionInfo = RegionInfo.currentRegion;
				if (regionInfo == null)
				{
					CultureInfo currentCulture = CultureInfo.CurrentCulture;
					if (currentCulture != null)
					{
						regionInfo = new RegionInfo(currentCulture);
					}
					if (Interlocked.CompareExchange<RegionInfo>(ref RegionInfo.currentRegion, regionInfo, null) != null)
					{
						regionInfo = RegionInfo.currentRegion;
					}
				}
				return regionInfo;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.RegionInfo" /> class based on the country/region associated with the specified culture identifier.</summary>
		/// <param name="culture">A culture identifier. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="culture" /> specifies either an invariant, custom, or neutral culture.</exception>
		// Token: 0x06003488 RID: 13448 RVA: 0x000C2E8F File Offset: 0x000C108F
		public RegionInfo(int culture)
		{
			if (!this.GetByTerritory(CultureInfo.GetCultureInfo(culture)))
			{
				throw new ArgumentException(string.Format("Region ID {0} (0x{0:X4}) is not a supported region.", culture), "culture");
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.RegionInfo" /> class based on the country/region or specific culture, specified by name.</summary>
		/// <param name="name">A string that contains a two-letter code defined in ISO 3166 for country/region.-or-A string that contains the culture name for a specific culture, custom culture, or Windows-only culture. If the culture name is not in RFC 4646 format, your application should specify the entire culture name instead of just the country/region. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid country/region name or specific culture name.</exception>
		// Token: 0x06003489 RID: 13449 RVA: 0x000C2EC0 File Offset: 0x000C10C0
		public RegionInfo(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException();
			}
			if (this.construct_internal_region_from_name(name.ToUpperInvariant()))
			{
				return;
			}
			if (!this.GetByTerritory(CultureInfo.GetCultureInfo(name)))
			{
				throw new ArgumentException(string.Format("Region name {0} is not supported.", name), "name");
			}
		}

		// Token: 0x0600348A RID: 13450 RVA: 0x000C2F10 File Offset: 0x000C1110
		private RegionInfo(CultureInfo ci)
		{
			if (ci.LCID == 127)
			{
				this.regionId = 244;
				this.iso2Name = "IV";
				this.iso3Name = "ivc";
				this.win3Name = "IVC";
				this.nativeName = (this.englishName = "Invariant Country");
				this.currencySymbol = "¤";
				this.isoCurrencySymbol = "XDR";
				this.currencyEnglishName = (this.currencyNativeName = "International Monetary Fund");
				return;
			}
			if (ci.Territory == null)
			{
				throw new NotImplementedException("Neutral region info");
			}
			this.construct_internal_region_from_name(ci.Territory.ToUpperInvariant());
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x000C2FBD File Offset: 0x000C11BD
		private bool GetByTerritory(CultureInfo ci)
		{
			if (ci == null)
			{
				throw new Exception("INTERNAL ERROR: should not happen.");
			}
			return !ci.IsNeutralCulture && ci.Territory != null && this.construct_internal_region_from_name(ci.Territory.ToUpperInvariant());
		}

		// Token: 0x0600348C RID: 13452
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool construct_internal_region_from_name(string name);

		/// <summary>Gets the name, in English, of the currency used in the country/region.</summary>
		/// <returns>The name, in English, of the currency used in the country/region.</returns>
		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x0600348D RID: 13453 RVA: 0x000C2FF0 File Offset: 0x000C11F0
		[ComVisible(false)]
		public virtual string CurrencyEnglishName
		{
			get
			{
				return this.currencyEnglishName;
			}
		}

		/// <summary>Gets the currency symbol associated with the country/region.</summary>
		/// <returns>The currency symbol associated with the country/region.</returns>
		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x0600348E RID: 13454 RVA: 0x000C2FF8 File Offset: 0x000C11F8
		public virtual string CurrencySymbol
		{
			get
			{
				return this.currencySymbol;
			}
		}

		/// <summary>Gets the full name of the country/region in the language of the localized version of .NET Framework.</summary>
		/// <returns>The full name of the country/region in the language of the localized version of .NET Framework.</returns>
		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x0600348F RID: 13455 RVA: 0x000C3000 File Offset: 0x000C1200
		[MonoTODO("DisplayName currently only returns the EnglishName")]
		public virtual string DisplayName
		{
			get
			{
				return this.englishName;
			}
		}

		/// <summary>Gets the full name of the country/region in English.</summary>
		/// <returns>The full name of the country/region in English.</returns>
		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06003490 RID: 13456 RVA: 0x000C3000 File Offset: 0x000C1200
		public virtual string EnglishName
		{
			get
			{
				return this.englishName;
			}
		}

		/// <summary>Gets a unique identification number for a geographical region, country, city, or location.</summary>
		/// <returns>A 32-bit signed number that uniquely identifies a geographical location.</returns>
		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06003491 RID: 13457 RVA: 0x000C3008 File Offset: 0x000C1208
		[ComVisible(false)]
		public virtual int GeoId
		{
			get
			{
				return this.regionId;
			}
		}

		/// <summary>Gets a value indicating whether the country/region uses the metric system for measurements.</summary>
		/// <returns>true if the country/region uses the metric system for measurements; otherwise, false.</returns>
		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06003492 RID: 13458 RVA: 0x000C3010 File Offset: 0x000C1210
		public virtual bool IsMetric
		{
			get
			{
				string text = this.iso2Name;
				return !(text == "US") && !(text == "UK");
			}
		}

		/// <summary>Gets the three-character ISO 4217 currency symbol associated with the country/region.</summary>
		/// <returns>The three-character ISO 4217 currency symbol associated with the country/region.</returns>
		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06003493 RID: 13459 RVA: 0x000C3041 File Offset: 0x000C1241
		public virtual string ISOCurrencySymbol
		{
			get
			{
				return this.isoCurrencySymbol;
			}
		}

		/// <summary>Gets the name of a country/region formatted in the native language of the country/region.</summary>
		/// <returns>The native name of the country/region formatted in the language associated with the ISO 3166 country/region code. </returns>
		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06003494 RID: 13460 RVA: 0x000C3049 File Offset: 0x000C1249
		[ComVisible(false)]
		public virtual string NativeName
		{
			get
			{
				return this.nativeName;
			}
		}

		/// <summary>Gets the name of the currency used in the country/region, formatted in the native language of the country/region. </summary>
		/// <returns>The native name of the currency used in the country/region, formatted in the language associated with the ISO 3166 country/region code. </returns>
		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06003495 RID: 13461 RVA: 0x000C3051 File Offset: 0x000C1251
		[ComVisible(false)]
		public virtual string CurrencyNativeName
		{
			get
			{
				return this.currencyNativeName;
			}
		}

		/// <summary>Gets the name or ISO 3166 two-letter country/region code for the current <see cref="T:System.Globalization.RegionInfo" /> object.</summary>
		/// <returns>The value specified by the <paramref name="name" /> parameter of the <see cref="M:System.Globalization.RegionInfo.#ctor(System.String)" /> constructor. The return value is in uppercase.-or-The two-letter code defined in ISO 3166 for the country/region specified by the <paramref name="culture" /> parameter of the <see cref="M:System.Globalization.RegionInfo.#ctor(System.Int32)" /> constructor. The return value is in uppercase.</returns>
		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06003496 RID: 13462 RVA: 0x000C3059 File Offset: 0x000C1259
		public virtual string Name
		{
			get
			{
				return this.iso2Name;
			}
		}

		/// <summary>Gets the three-letter code defined in ISO 3166 for the country/region.</summary>
		/// <returns>The three-letter code defined in ISO 3166 for the country/region.</returns>
		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06003497 RID: 13463 RVA: 0x000C3061 File Offset: 0x000C1261
		public virtual string ThreeLetterISORegionName
		{
			get
			{
				return this.iso3Name;
			}
		}

		/// <summary>Gets the three-letter code assigned by Windows to the country/region represented by this <see cref="T:System.Globalization.RegionInfo" />.</summary>
		/// <returns>The three-letter code assigned by Windows to the country/region represented by this <see cref="T:System.Globalization.RegionInfo" />.</returns>
		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06003498 RID: 13464 RVA: 0x000C3069 File Offset: 0x000C1269
		public virtual string ThreeLetterWindowsRegionName
		{
			get
			{
				return this.win3Name;
			}
		}

		/// <summary>Gets the two-letter code defined in ISO 3166 for the country/region.</summary>
		/// <returns>The two-letter code defined in ISO 3166 for the country/region.</returns>
		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06003499 RID: 13465 RVA: 0x000C3059 File Offset: 0x000C1259
		public virtual string TwoLetterISORegionName
		{
			get
			{
				return this.iso2Name;
			}
		}

		/// <summary>Determines whether the specified object is the same instance as the current <see cref="T:System.Globalization.RegionInfo" />.</summary>
		/// <returns>true if the <paramref name="value" /> parameter is a <see cref="T:System.Globalization.RegionInfo" /> object and its <see cref="P:System.Globalization.RegionInfo.Name" /> property is the same as the <see cref="P:System.Globalization.RegionInfo.Name" /> property of the current <see cref="T:System.Globalization.RegionInfo" /> object; otherwise, false.</returns>
		/// <param name="value">The object to compare with the current <see cref="T:System.Globalization.RegionInfo" />. </param>
		// Token: 0x0600349A RID: 13466 RVA: 0x000C3074 File Offset: 0x000C1274
		public override bool Equals(object value)
		{
			RegionInfo regionInfo = value as RegionInfo;
			return regionInfo != null && this.Name == regionInfo.Name;
		}

		/// <summary>Serves as a hash function for the current <see cref="T:System.Globalization.RegionInfo" />, suitable for hashing algorithms and data structures, such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Globalization.RegionInfo" />.</returns>
		// Token: 0x0600349B RID: 13467 RVA: 0x000C309E File Offset: 0x000C129E
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		/// <summary>Returns a string containing the culture name or ISO 3166 two-letter country/region codes specified for the current <see cref="T:System.Globalization.RegionInfo" />.</summary>
		/// <returns>A string containing the culture name or ISO 3166 two-letter country/region codes defined for the current <see cref="T:System.Globalization.RegionInfo" />.</returns>
		// Token: 0x0600349C RID: 13468 RVA: 0x000C30AB File Offset: 0x000C12AB
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x0600349D RID: 13469 RVA: 0x000C30B3 File Offset: 0x000C12B3
		internal static void ClearCachedData()
		{
			RegionInfo.currentRegion = null;
		}

		// Token: 0x04001C15 RID: 7189
		private static RegionInfo currentRegion;

		// Token: 0x04001C16 RID: 7190
		private int regionId;

		// Token: 0x04001C17 RID: 7191
		private string iso2Name;

		// Token: 0x04001C18 RID: 7192
		private string iso3Name;

		// Token: 0x04001C19 RID: 7193
		private string win3Name;

		// Token: 0x04001C1A RID: 7194
		private string englishName;

		// Token: 0x04001C1B RID: 7195
		private string nativeName;

		// Token: 0x04001C1C RID: 7196
		private string currencySymbol;

		// Token: 0x04001C1D RID: 7197
		private string isoCurrencySymbol;

		// Token: 0x04001C1E RID: 7198
		private string currencyEnglishName;

		// Token: 0x04001C1F RID: 7199
		private string currencyNativeName;
	}
}
