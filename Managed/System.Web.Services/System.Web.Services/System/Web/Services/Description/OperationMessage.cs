using System;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Represents a message type passed by the action of an XML Web service.</summary>
	// Token: 0x020000F3 RID: 243
	public abstract class OperationMessage : NamedItem
	{
		// Token: 0x0600067D RID: 1661 RVA: 0x0001C630 File Offset: 0x0001A830
		internal void SetParent(Operation parent)
		{
			this.parent = parent;
		}

		/// <summary>Gets the <see cref="T:System.Web.Services.Description.Operation" /> of which the <see cref="T:System.Web.Services.Description.OperationMessage" /> is a member.</summary>
		/// <returns>The operation of which the <see cref="T:System.Web.Services.Description.OperationMessage" /> is a member.</returns>
		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x0001C639 File Offset: 0x0001A839
		public Operation Operation
		{
			get
			{
				return this.parent;
			}
		}

		/// <summary>Gets or sets an abstract, typed definition of the data being communicated.</summary>
		/// <returns>An XML qualified name.</returns>
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x0001C641 File Offset: 0x0001A841
		// (set) Token: 0x06000680 RID: 1664 RVA: 0x0001C649 File Offset: 0x0001A849
		[XmlAttribute("message")]
		public XmlQualifiedName Message
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
			}
		}

		// Token: 0x040003FC RID: 1020
		private XmlQualifiedName message = XmlQualifiedName.Empty;

		// Token: 0x040003FD RID: 1021
		private Operation parent;
	}
}
