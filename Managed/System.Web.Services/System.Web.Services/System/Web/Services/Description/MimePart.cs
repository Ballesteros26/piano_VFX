using System;
using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Represents an extensibility element added to a <see cref="T:System.Web.Services.Description.MimeMultipartRelatedBinding" />, specifying the concrete MIME type for the <see cref="T:System.Web.Services.Description.MessagePart" /> to which the MimePart applies. This class cannot be inherited.</summary>
	// Token: 0x020000CF RID: 207
	[XmlFormatExtensionPoint("Extensions")]
	public sealed class MimePart : ServiceDescriptionFormatExtension
	{
		/// <summary>Gets the collection of MIME extensibility elements for the part of the <see cref="T:System.Web.Services.Description.MimeMultipartRelatedBinding" /> of which the <see cref="T:System.Web.Services.Description.MimePart" /> is a member.</summary>
		/// <returns>A collection of service description format extension.</returns>
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x00018AB6 File Offset: 0x00016CB6
		[XmlIgnore]
		public ServiceDescriptionFormatExtensionCollection Extensions
		{
			get
			{
				if (this.extensions == null)
				{
					this.extensions = new ServiceDescriptionFormatExtensionCollection(this);
				}
				return this.extensions;
			}
		}

		// Token: 0x04000387 RID: 903
		private ServiceDescriptionFormatExtensionCollection extensions;
	}
}
