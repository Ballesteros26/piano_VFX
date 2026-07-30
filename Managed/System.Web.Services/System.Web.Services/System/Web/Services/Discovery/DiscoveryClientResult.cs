using System;
using System.Xml.Serialization;

namespace System.Web.Services.Discovery
{
	/// <summary>Represents the details of a discovery reference without the contents of the referenced document. This class cannot be inherited.</summary>
	// Token: 0x020000A1 RID: 161
	public sealed class DiscoveryClientResult
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResult" /> class.</summary>
		// Token: 0x06000426 RID: 1062 RVA: 0x0000210F File Offset: 0x0000030F
		public DiscoveryClientResult()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResult" /> class and sets the <see cref="P:System.Web.Services.Discovery.DiscoveryClientResult.ReferenceTypeName" /> property to <paramref name="referenceType" />, the <see cref="P:System.Web.Services.Discovery.DiscoveryClientResult.Url" /> property to <paramref name="url" /> and the <see cref="P:System.Web.Services.Discovery.DiscoveryClientResult.Filename" /> property to <paramref name="filename" />.</summary>
		/// <param name="referenceType">Name of the class representing the type of reference in the discovery document. Sets the <see cref="P:System.Web.Services.Discovery.DiscoveryClientResult.ReferenceTypeName" /> property. </param>
		/// <param name="url">URL for the reference. Sets the <see cref="P:System.Web.Services.Discovery.DiscoveryClientResult.Url" /> property. </param>
		/// <param name="filename">Name of the file in which the reference was saved. Sets the <see cref="P:System.Web.Services.Discovery.DiscoveryClientResult.Filename" /> property. </param>
		// Token: 0x06000427 RID: 1063 RVA: 0x00013A0A File Offset: 0x00011C0A
		public DiscoveryClientResult(Type referenceType, string url, string filename)
		{
			this.referenceTypeName = ((referenceType == null) ? string.Empty : referenceType.FullName);
			this.url = url;
			this.filename = filename;
		}

		/// <summary>Name of the class representing the type of reference in the discovery document.</summary>
		/// <returns>Name of the class representing the type of a reference. Default value is null.</returns>
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x00013A3C File Offset: 0x00011C3C
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x00013A44 File Offset: 0x00011C44
		[XmlAttribute("referenceType")]
		public string ReferenceTypeName
		{
			get
			{
				return this.referenceTypeName;
			}
			set
			{
				this.referenceTypeName = value;
			}
		}

		/// <summary>Gets or sets the URL for the reference.</summary>
		/// <returns>The URL of the reference.</returns>
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x00013A4D File Offset: 0x00011C4D
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x00013A55 File Offset: 0x00011C55
		[XmlAttribute("url")]
		public string Url
		{
			get
			{
				return this.url;
			}
			set
			{
				this.url = value;
			}
		}

		/// <summary>Gets or sets the name of the file in which the reference is saved.</summary>
		/// <returns>Name of the file in which the reference is saved.</returns>
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x00013A5E File Offset: 0x00011C5E
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x00013A66 File Offset: 0x00011C66
		[XmlAttribute("filename")]
		public string Filename
		{
			get
			{
				return this.filename;
			}
			set
			{
				this.filename = value;
			}
		}

		// Token: 0x0400032C RID: 812
		private string referenceTypeName;

		// Token: 0x0400032D RID: 813
		private string url;

		// Token: 0x0400032E RID: 814
		private string filename;
	}
}
