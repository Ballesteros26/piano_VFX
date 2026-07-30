using System;

namespace System.Web.Services.Discovery
{
	/// <summary>Obtains the file locations and descriptions of ASP.NET Web services. This class cannot be inherited.</summary>
	// Token: 0x0200009C RID: 156
	public sealed class ContractSearchPattern : DiscoverySearchPattern
	{
		/// <summary>Gets the file name pattern to use as a search target.</summary>
		/// <returns>The literal string "*.asmx".</returns>
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x00012A84 File Offset: 0x00010C84
		public override string Pattern
		{
			get
			{
				return "*.asmx";
			}
		}

		/// <summary>Creates the <see cref="T:System.Web.Services.Discovery.ContractReference" /> object for the specified .asmx file.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Discovery.ContractReference" /> object with the specified file name for its .asmx file.</returns>
		/// <param name="filename">The file-system path of the Web service's .asmx file.</param>
		// Token: 0x060003FF RID: 1023 RVA: 0x00012A8B File Offset: 0x00010C8B
		public override DiscoveryReference GetDiscoveryReference(string filename)
		{
			return new ContractReference(filename + "?wsdl", filename);
		}
	}
}
