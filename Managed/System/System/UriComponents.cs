using System;

namespace System
{
	/// <summary>Specifies the parts of a <see cref="T:System.Uri" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000FF RID: 255
	[Flags]
	public enum UriComponents
	{
		/// <summary>The <see cref="P:System.Uri.Scheme" /> data.</summary>
		// Token: 0x04000CC0 RID: 3264
		Scheme = 1,
		/// <summary>The <see cref="P:System.Uri.UserInfo" /> data.</summary>
		// Token: 0x04000CC1 RID: 3265
		UserInfo = 2,
		/// <summary>The <see cref="P:System.Uri.Host" /> data.</summary>
		// Token: 0x04000CC2 RID: 3266
		Host = 4,
		/// <summary>The <see cref="P:System.Uri.Port" /> data.</summary>
		// Token: 0x04000CC3 RID: 3267
		Port = 8,
		/// <summary>The <see cref="P:System.Uri.LocalPath" /> data.</summary>
		// Token: 0x04000CC4 RID: 3268
		Path = 16,
		/// <summary>The <see cref="P:System.Uri.Query" /> data.</summary>
		// Token: 0x04000CC5 RID: 3269
		Query = 32,
		/// <summary>The <see cref="P:System.Uri.Fragment" /> data.</summary>
		// Token: 0x04000CC6 RID: 3270
		Fragment = 64,
		/// <summary>The <see cref="P:System.Uri.Port" /> data. If no port data is in the <see cref="T:System.Uri" /> and a default port has been assigned to the <see cref="P:System.Uri.Scheme" />, the default port is returned. If there is no default port, -1 is returned.</summary>
		// Token: 0x04000CC7 RID: 3271
		StrongPort = 128,
		/// <summary>The normalized form of the <see cref="P:System.Uri.Host" />.</summary>
		// Token: 0x04000CC8 RID: 3272
		NormalizedHost = 256,
		/// <summary>Specifies that the delimiter should be included.</summary>
		// Token: 0x04000CC9 RID: 3273
		KeepDelimiter = 1073741824,
		/// <summary>The complete <see cref="T:System.Uri" /> context that is needed for Uri Serializers. The context includes the IPv6 scope.</summary>
		// Token: 0x04000CCA RID: 3274
		SerializationInfoString = -2147483648,
		/// <summary>The <see cref="P:System.Uri.Scheme" />, <see cref="P:System.Uri.UserInfo" />, <see cref="P:System.Uri.Host" />, <see cref="P:System.Uri.Port" />, <see cref="P:System.Uri.LocalPath" />, <see cref="P:System.Uri.Query" />, and <see cref="P:System.Uri.Fragment" /> data.</summary>
		// Token: 0x04000CCB RID: 3275
		AbsoluteUri = 127,
		/// <summary>The <see cref="P:System.Uri.Host" /> and <see cref="P:System.Uri.Port" /> data. If no port data is in the Uri and a default port has been assigned to the <see cref="P:System.Uri.Scheme" />, the default port is returned. If there is no default port, -1 is returned.</summary>
		// Token: 0x04000CCC RID: 3276
		HostAndPort = 132,
		/// <summary>The <see cref="P:System.Uri.UserInfo" />, <see cref="P:System.Uri.Host" />, and <see cref="P:System.Uri.Port" /> data. If no port data is in the <see cref="T:System.Uri" /> and a default port has been assigned to the <see cref="P:System.Uri.Scheme" />, the default port is returned. If there is no default port, -1 is returned.</summary>
		// Token: 0x04000CCD RID: 3277
		StrongAuthority = 134,
		/// <summary>The <see cref="P:System.Uri.Scheme" />, <see cref="P:System.Uri.Host" />, and <see cref="P:System.Uri.Port" /> data.</summary>
		// Token: 0x04000CCE RID: 3278
		SchemeAndServer = 13,
		/// <summary>The <see cref="P:System.Uri.Scheme" />, <see cref="P:System.Uri.Host" />, <see cref="P:System.Uri.Port" />, <see cref="P:System.Uri.LocalPath" />, and <see cref="P:System.Uri.Query" /> data.</summary>
		// Token: 0x04000CCF RID: 3279
		HttpRequestUrl = 61,
		/// <summary>The <see cref="P:System.Uri.LocalPath" /> and <see cref="P:System.Uri.Query" /> data. Also see <see cref="P:System.Uri.PathAndQuery" />. </summary>
		// Token: 0x04000CD0 RID: 3280
		PathAndQuery = 48
	}
}
