using System;

namespace System.Web.Services.Discovery
{
	/// <summary>Obtains the file locations of XML Schema documents for use in populating a Web services discovery document. This class cannot be inherited.</summary>
	// Token: 0x020000BA RID: 186
	public sealed class XmlSchemaSearchPattern : DiscoverySearchPattern
	{
		/// <summary>Gets the file name pattern to use as a search target.</summary>
		/// <returns>The literal string "*.xsd".</returns>
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00016A18 File Offset: 0x00014C18
		public override string Pattern
		{
			get
			{
				return "*.xsd";
			}
		}

		/// <summary>Returns the <see cref="T:System.Web.Services.Discovery.SchemaReference" /> object for a given discovery document.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Discovery.SchemaReference" /> object that specifies the file name for an XML Schema document.</returns>
		/// <param name="filename">The file system path of the XML Schema document.</param>
		// Token: 0x060004D7 RID: 1239 RVA: 0x00016A1F File Offset: 0x00014C1F
		public override DiscoveryReference GetDiscoveryReference(string filename)
		{
			return new SchemaReference(filename);
		}
	}
}
