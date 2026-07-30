using System;

namespace System.Web.Services.Discovery
{
	/// <summary>Obtains the file locations of Web services discovery documents for use in populating another Web services discovery document.</summary>
	// Token: 0x020000A5 RID: 165
	public class DiscoveryDocumentLinksPattern : DiscoverySearchPattern
	{
		/// <summary>Gets the file-name pattern to use as a search target.</summary>
		/// <returns>The literal string "*.disco".</returns>
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x00013BD4 File Offset: 0x00011DD4
		public override string Pattern
		{
			get
			{
				return "*.disco";
			}
		}

		/// <summary>Returns the <see cref="T:System.Web.Services.Discovery.DiscoveryDocumentReference" /> object for a given discovery document.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Discovery.DiscoveryDocumentReference" /> object that specifies the location of a .vsdisco file.</returns>
		/// <param name="filename">The file-system path of the discovery document.</param>
		// Token: 0x06000447 RID: 1095 RVA: 0x00013BDB File Offset: 0x00011DDB
		public override DiscoveryReference GetDiscoveryReference(string filename)
		{
			return new DiscoveryDocumentReference(filename);
		}
	}
}
