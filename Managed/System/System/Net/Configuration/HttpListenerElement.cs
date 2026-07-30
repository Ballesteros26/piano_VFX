using System;
using System.Configuration;
using Unity;

namespace System.Net.Configuration
{
	/// <summary>Represents the HttpListener element in the configuration file. This class cannot be inherited.</summary>
	// Token: 0x020007CF RID: 1999
	public sealed class HttpListenerElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.HttpListenerElement" /> class.</summary>
		// Token: 0x06004006 RID: 16390 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		public HttpListenerElement()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the default timeout elements used for an <see cref="T:System.Net.HttpListener" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Net.Configuration.HttpListenerTimeoutsElement" />.The timeout elements used for an <see cref="T:System.Net.HttpListener" /> object.</returns>
		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x06004007 RID: 16391 RVA: 0x0003D2D0 File Offset: 0x0003B4D0
		public HttpListenerTimeoutsElement Timeouts
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a value that indicates if <see cref="T:System.Net.HttpListener" /> uses the raw unescaped URI instead of the converted URI.</summary>
		/// <returns>A Boolean value that indicates if <see cref="T:System.Net.HttpListener" /> uses the raw unescaped URI, rather than the converted URI.</returns>
		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06004008 RID: 16392 RVA: 0x000E0C68 File Offset: 0x000DEE68
		public bool UnescapeRequestUrl
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}
	}
}
