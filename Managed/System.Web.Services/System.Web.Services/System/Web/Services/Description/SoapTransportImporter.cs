using System;
using System.Security.Permissions;

namespace System.Web.Services.Description
{
	/// <summary>Serves as a base class for derived classes that import SOAP transmission protocols into XML Web services.</summary>
	// Token: 0x02000130 RID: 304
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class SoapTransportImporter
	{
		/// <summary>When overridden in a derived class, this method determines whether the specified transport protocol is supported by the XML Web service.</summary>
		/// <returns>true if the transport protocol is supported; otherwise, false.</returns>
		/// <param name="transport">A URI representing the transport protocol to be checked. </param>
		// Token: 0x06000933 RID: 2355
		public abstract bool IsSupportedTransport(string transport);

		/// <summary>When overridden in a derived class, this method uses information contained in the <see cref="T:System.Web.Services.Description.ServiceDescription" /> object model (available through the <see cref="P:System.Web.Services.Description.SoapTransportImporter.ImportContext" /> property) to add transport-specific code to the class being generated.</summary>
		// Token: 0x06000934 RID: 2356
		public abstract void ImportClass();

		/// <summary>Gets or sets a reference to the <see cref="T:System.Web.Services.Description.SoapProtocolImporter" /> performing the import action.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.Services.Description.SoapProtocolImporter" /> performing the import action.</returns>
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x0004008E File Offset: 0x0003E28E
		// (set) Token: 0x06000936 RID: 2358 RVA: 0x00040096 File Offset: 0x0003E296
		public SoapProtocolImporter ImportContext
		{
			get
			{
				return this.protocolImporter;
			}
			set
			{
				this.protocolImporter = value;
			}
		}

		// Token: 0x04000570 RID: 1392
		private SoapProtocolImporter protocolImporter;
	}
}
