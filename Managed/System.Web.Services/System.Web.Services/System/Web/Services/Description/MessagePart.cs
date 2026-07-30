using System;
using System.Web.Services.Configuration;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Represents the messages to be broken up into their logical units with specific abstract information for each part. This class cannot be inherited.</summary>
	// Token: 0x020000FA RID: 250
	[XmlFormatExtensionPoint("Extensions")]
	public sealed class MessagePart : NamedItem
	{
		// Token: 0x060006A0 RID: 1696 RVA: 0x0001CA31 File Offset: 0x0001AC31
		internal void SetParent(Message parent)
		{
			this.parent = parent;
		}

		/// <summary>Gets the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.MessagePart" />.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.MessagePart" />.</returns>
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x0001CA3A File Offset: 0x0001AC3A
		[XmlIgnore]
		public override ServiceDescriptionFormatExtensionCollection Extensions
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

		/// <summary>Gets the <see cref="T:System.Web.Services.Description.Message" /> of which the <see cref="T:System.Web.Services.Description.MessagePart" /> is a member.</summary>
		/// <returns>The message of which the <see cref="T:System.Web.Services.Description.MessagePart" /> is a member.</returns>
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x0001CA56 File Offset: 0x0001AC56
		public Message Message
		{
			get
			{
				return this.parent;
			}
		}

		/// <summary>Gets or sets the name of the XML element that corresponds to the current <see cref="T:System.Web.Services.Description.MessagePart" />.</summary>
		/// <returns>The name of the XML element that corresponds to the current <see cref="T:System.Web.Services.Description.MessagePart" />.</returns>
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x0001CA5E File Offset: 0x0001AC5E
		// (set) Token: 0x060006A4 RID: 1700 RVA: 0x0001CA66 File Offset: 0x0001AC66
		[XmlAttribute("element")]
		public XmlQualifiedName Element
		{
			get
			{
				return this.element;
			}
			set
			{
				this.element = value;
			}
		}

		/// <summary>Gets or sets the XML data type of the <see cref="T:System.Web.Services.Description.MessagePart" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlQualifiedName" />.</returns>
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0001CA6F File Offset: 0x0001AC6F
		// (set) Token: 0x060006A6 RID: 1702 RVA: 0x0001CA85 File Offset: 0x0001AC85
		[XmlAttribute("type")]
		public XmlQualifiedName Type
		{
			get
			{
				if (this.type == null)
				{
					return XmlQualifiedName.Empty;
				}
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x0400040C RID: 1036
		private XmlQualifiedName type = XmlQualifiedName.Empty;

		// Token: 0x0400040D RID: 1037
		private XmlQualifiedName element = XmlQualifiedName.Empty;

		// Token: 0x0400040E RID: 1038
		private Message parent;

		// Token: 0x0400040F RID: 1039
		private ServiceDescriptionFormatExtensionCollection extensions;
	}
}
