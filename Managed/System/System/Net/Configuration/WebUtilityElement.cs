using System;
using System.Configuration;
using Unity;

namespace System.Net.Configuration
{
	/// <summary>Represents the WebUtility element in the configuration file.</summary>
	// Token: 0x020007D1 RID: 2001
	public sealed class WebUtilityElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.WebUtilityElement" /> class.</summary>
		// Token: 0x06004010 RID: 16400 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		public WebUtilityElement()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the default Unicode decoding conformance behavior used for an <see cref="T:System.Net.WebUtility" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Net.Configuration.UnicodeDecodingConformance" />.The default Unicode decoding behavior.</returns>
		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x06004011 RID: 16401 RVA: 0x000E0D2C File Offset: 0x000DEF2C
		// (set) Token: 0x06004012 RID: 16402 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		public UnicodeDecodingConformance UnicodeDecodingConformance
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return UnicodeDecodingConformance.Auto;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets the default Unicode encoding conformance behavior used for an <see cref="T:System.Net.WebUtility" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Net.Configuration.UnicodeEncodingConformance" />.The default Unicode encoding behavior.</returns>
		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06004013 RID: 16403 RVA: 0x000E0D48 File Offset: 0x000DEF48
		// (set) Token: 0x06004014 RID: 16404 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		public UnicodeEncodingConformance UnicodeEncodingConformance
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return UnicodeEncodingConformance.Auto;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
