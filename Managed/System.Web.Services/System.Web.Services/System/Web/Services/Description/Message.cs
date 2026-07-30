using System;
using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Provides an abstract definition of data passed by an XML Web service. This class cannot be inherited.</summary>
	// Token: 0x020000F9 RID: 249
	[XmlFormatExtensionPoint("Extensions")]
	public sealed class Message : NamedItem
	{
		// Token: 0x06000699 RID: 1689 RVA: 0x0001C93D File Offset: 0x0001AB3D
		internal void SetParent(ServiceDescription parent)
		{
			this.parent = parent;
		}

		/// <summary>Gets the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.Message" />.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.Message" />.</returns>
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x0001C946 File Offset: 0x0001AB46
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

		/// <summary>Gets the <see cref="T:System.Web.Services.Description.ServiceDescription" /> of which the current <see cref="T:System.Web.Services.Description.Message" /> is a member.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Description.ServiceDescription" />.</returns>
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x0001C962 File Offset: 0x0001AB62
		public ServiceDescription ServiceDescription
		{
			get
			{
				return this.parent;
			}
		}

		/// <summary>Gets the collection of the <see cref="T:System.Web.Services.Description.MessagePart" /> objects contained in the <see cref="T:System.Web.Services.Description.Message" />.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Description.MessagePartCollection" />.</returns>
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x0001C96A File Offset: 0x0001AB6A
		[XmlElement("part")]
		public MessagePartCollection Parts
		{
			get
			{
				if (this.parts == null)
				{
					this.parts = new MessagePartCollection(this);
				}
				return this.parts;
			}
		}

		/// <summary>Searches the <see cref="T:System.Web.Services.Description.MessagePartCollection" /> returned by the <see cref="P:System.Web.Services.Description.Message.Parts" /> property and returns an array of type <see cref="T:System.Web.Services.Description.MessagePart" /> that contains the named instances.</summary>
		/// <returns>An array of type <see cref="T:System.Web.Services.Description.MessagePart" />.</returns>
		/// <param name="partNames">An array of names of the <see cref="T:System.Web.Services.Description.MessagePart" /> instances to be returned. </param>
		/// <exception cref="T:System.ArgumentException">No <see cref="T:System.Web.Services.Description.MessagePart" /> instances with the specified names exist within the collection. </exception>
		// Token: 0x0600069D RID: 1693 RVA: 0x0001C988 File Offset: 0x0001AB88
		public MessagePart[] FindPartsByName(string[] partNames)
		{
			MessagePart[] array = new MessagePart[partNames.Length];
			for (int i = 0; i < partNames.Length; i++)
			{
				array[i] = this.FindPartByName(partNames[i]);
			}
			return array;
		}

		/// <summary>Searches the <see cref="T:System.Web.Services.Description.MessagePartCollection" /> returned by the <see cref="P:System.Web.Services.Description.Message.Parts" /> property, and returns the named <see cref="T:System.Web.Services.Description.MessagePart" />.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Description.MessagePart" />.</returns>
		/// <param name="partName">A string that names the <see cref="T:System.Web.Services.Description.MessagePart" /> to be returned.</param>
		/// <exception cref="T:System.ArgumentException">No <see cref="T:System.Web.Services.Description.MessagePart" /> with the specified name exists within the collection.</exception>
		// Token: 0x0600069E RID: 1694 RVA: 0x0001C9BC File Offset: 0x0001ABBC
		public MessagePart FindPartByName(string partName)
		{
			for (int i = 0; i < this.parts.Count; i++)
			{
				MessagePart messagePart = this.parts[i];
				if (messagePart.Name == partName)
				{
					return messagePart;
				}
			}
			throw new ArgumentException(Res.GetString("MissingMessagePartForMessageFromNamespace3", new object[]
			{
				partName,
				base.Name,
				this.ServiceDescription.TargetNamespace
			}), "partName");
		}

		// Token: 0x04000409 RID: 1033
		private MessagePartCollection parts;

		// Token: 0x0400040A RID: 1034
		private ServiceDescription parent;

		// Token: 0x0400040B RID: 1035
		private ServiceDescriptionFormatExtensionCollection extensions;
	}
}
