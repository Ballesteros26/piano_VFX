using System;

namespace System.Web.Services.Protocols
{
	/// <summary>When overridden in a derived class, specifies a SOAP extension should run with an XML Web service method.</summary>
	// Token: 0x02000063 RID: 99
	public abstract class SoapExtensionAttribute : Attribute
	{
		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Type" /> of the SOAP extension.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the SOAP extension.</returns>
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000275 RID: 629
		public abstract Type ExtensionType { get; }

		/// <summary>When overridden in a derived class, gets or set the priority of the SOAP extension.</summary>
		/// <returns>The priority of the SOAP extension.</returns>
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000276 RID: 630
		// (set) Token: 0x06000277 RID: 631
		public abstract int Priority { get; set; }
	}
}
