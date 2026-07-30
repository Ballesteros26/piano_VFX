using System;
using System.Collections;
using System.Xml;

namespace System.Web.Services.Description
{
	/// <summary>Represents the collection of extensibility elements used by the XML Web service. This class cannot be inherited.</summary>
	// Token: 0x020000FC RID: 252
	public sealed class ServiceDescriptionFormatExtensionCollection : ServiceDescriptionBaseCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> class.</summary>
		/// <param name="parent">The object of which this collection is a member.</param>
		// Token: 0x060006AC RID: 1708 RVA: 0x0001CB15 File Offset: 0x0001AD15
		public ServiceDescriptionFormatExtensionCollection(object parent)
			: base(parent)
		{
		}

		/// <summary>Gets or sets the value of a member of the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" />.</summary>
		/// <returns>The value of the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" />.</returns>
		/// <param name="index">The zero-based index of the member whose value is modified or returned.</param>
		// Token: 0x170001EF RID: 495
		public object this[int index]
		{
			get
			{
				return base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> to the end of the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" />.</summary>
		/// <returns>The zero-based index where the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> has been added.</returns>
		/// <param name="extension">The <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" />, passed by reference, to add to the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" />.</param>
		// Token: 0x060006AF RID: 1711 RVA: 0x0000CD4B File Offset: 0x0000AF4B
		public int Add(object extension)
		{
			return base.List.Add(extension);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> to the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> at the specified zero-based index.</summary>
		/// <param name="index">The zero-based index at which to insert the <paramref name="extension" /> parameter.</param>
		/// <param name="extension">The <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> to add to the collection.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is less than zero.- or - The <paramref name="index" /> parameter is greater than <see cref="P:System.Collections.CollectionBase.Count" />. </exception>
		// Token: 0x060006B0 RID: 1712 RVA: 0x0000CD59 File Offset: 0x0000AF59
		public void Insert(int index, object extension)
		{
			base.List.Insert(index, extension);
		}

		/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> and returns the zero-based index of the first instance with the collection.</summary>
		/// <returns>The zero-based index of the specified <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" />, or -1 if the element was not found in the collection.</returns>
		/// <param name="extension">The <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> for which to search in the collection.</param>
		// Token: 0x060006B1 RID: 1713 RVA: 0x0000CD68 File Offset: 0x0000AF68
		public int IndexOf(object extension)
		{
			return base.List.IndexOf(extension);
		}

		/// <summary>Returns a value indicating whether the specified <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> is a member of the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" />.</summary>
		/// <returns>true if the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> is a member of the collection; otherwise, false.</returns>
		/// <param name="extension">The <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> for which to check collection membership.</param>
		// Token: 0x060006B2 RID: 1714 RVA: 0x0000CD76 File Offset: 0x0000AF76
		public bool Contains(object extension)
		{
			return base.List.Contains(extension);
		}

		/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> from the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" />.</summary>
		/// <param name="extension">The <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> to remove from the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" />.</param>
		// Token: 0x060006B3 RID: 1715 RVA: 0x0000CD84 File Offset: 0x0000AF84
		public void Remove(object extension)
		{
			base.List.Remove(extension);
		}

		/// <summary>Copies the entire <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> into a one-dimensional array of type <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" />, starting at the specified zero-based index of the target array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> serving as the destination of the copy action.</param>
		/// <param name="index">The zero-based index at which to start placing the copied collection.</param>
		// Token: 0x060006B4 RID: 1716 RVA: 0x0000CD92 File Offset: 0x0000AF92
		public void CopyTo(object[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Searches the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> and returns the first element of the specified derived <see cref="T:System.Type" />.</summary>
		/// <returns>If the search is successful, an object of the specified <see cref="T:System.Type" />; otherwise, null.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> for which to search the collection.</param>
		// Token: 0x060006B5 RID: 1717 RVA: 0x0001CB2C File Offset: 0x0001AD2C
		public object Find(Type type)
		{
			for (int i = 0; i < base.List.Count; i++)
			{
				object obj = base.List[i];
				if (type.IsAssignableFrom(obj.GetType()))
				{
					((ServiceDescriptionFormatExtension)obj).Handled = true;
					return obj;
				}
			}
			return null;
		}

		/// <summary>Searches the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> and returns an array of all elements of the specified <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Object" /> instances representing all collection members of the specified type.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> for which to search the collection.</param>
		// Token: 0x060006B6 RID: 1718 RVA: 0x0001CB7C File Offset: 0x0001AD7C
		public object[] FindAll(Type type)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < base.List.Count; i++)
			{
				object obj = base.List[i];
				if (type.IsAssignableFrom(obj.GetType()))
				{
					((ServiceDescriptionFormatExtension)obj).Handled = true;
					arrayList.Add(obj);
				}
			}
			return (object[])arrayList.ToArray(type);
		}

		/// <summary>Searches the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> for a member with the specified name and namespace URI.</summary>
		/// <returns>If the search is successful, an <see cref="T:System.Xml.XmlElement" />; otherwise, null.</returns>
		/// <param name="name">The name of the <see cref="T:System.Xml.XmlElement" /> to be found.</param>
		/// <param name="ns">The XML namespace URI of the <see cref="T:System.Xml.XmlElement" /> to be found.</param>
		// Token: 0x060006B7 RID: 1719 RVA: 0x0001CBE0 File Offset: 0x0001ADE0
		public XmlElement Find(string name, string ns)
		{
			for (int i = 0; i < base.List.Count; i++)
			{
				XmlElement xmlElement = base.List[i] as XmlElement;
				if (xmlElement != null && xmlElement.LocalName == name && xmlElement.NamespaceURI == ns)
				{
					this.SetHandled(xmlElement);
					return xmlElement;
				}
			}
			return null;
		}

		/// <summary>Searches the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> and returns an array of all members with the specified name and namespace URI.</summary>
		/// <returns>An array of <see cref="T:System.Xml.XmlElement" /> instances.</returns>
		/// <param name="name">The XML name attribute of the <see cref="T:System.Xml.XmlElement" /> objects to be found.</param>
		/// <param name="ns">The XML namespace URI attribute of the <see cref="T:System.Xml.XmlElement" /> objects to be found.</param>
		// Token: 0x060006B8 RID: 1720 RVA: 0x0001CC40 File Offset: 0x0001AE40
		public XmlElement[] FindAll(string name, string ns)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < base.List.Count; i++)
			{
				XmlElement xmlElement = base.List[i] as XmlElement;
				if (xmlElement != null && xmlElement.LocalName == name && xmlElement.NamespaceURI == ns)
				{
					this.SetHandled(xmlElement);
					arrayList.Add(xmlElement);
				}
			}
			return (XmlElement[])arrayList.ToArray(typeof(XmlElement));
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0001CCBE File Offset: 0x0001AEBE
		private void SetHandled(XmlElement element)
		{
			if (this.handledElements == null)
			{
				this.handledElements = new ArrayList();
			}
			if (!this.handledElements.Contains(element))
			{
				this.handledElements.Add(element);
			}
		}

		/// <summary>Returns a value indicating whether the specified object is used by the import process when the extensibility element is imported into the XML Web service.</summary>
		/// <returns>true if the <paramref name="item" /> parameter is used; otherwise, false.</returns>
		/// <param name="item">An object, either of type <see cref="T:System.Xml.XmlElement" /> or <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> to check for use by the import process.</param>
		// Token: 0x060006BA RID: 1722 RVA: 0x0001CCEE File Offset: 0x0001AEEE
		public bool IsHandled(object item)
		{
			if (item is XmlElement)
			{
				return this.IsHandled((XmlElement)item);
			}
			return ((ServiceDescriptionFormatExtension)item).Handled;
		}

		/// <summary>Returns a value indicating whether the specified object is necessary for the operation of the XML Web service.</summary>
		/// <returns>true if the <paramref name="item" /> parameter is required; otherwise, false.</returns>
		/// <param name="item">An object, either of type <see cref="T:System.Xml.XmlElement" /> or <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" />, to check whether it is necessary.</param>
		// Token: 0x060006BB RID: 1723 RVA: 0x0001CD10 File Offset: 0x0001AF10
		public bool IsRequired(object item)
		{
			if (item is XmlElement)
			{
				return this.IsRequired((XmlElement)item);
			}
			return ((ServiceDescriptionFormatExtension)item).Required;
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001CD32 File Offset: 0x0001AF32
		private bool IsHandled(XmlElement element)
		{
			return this.handledElements != null && this.handledElements.Contains(element);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0001CD4C File Offset: 0x0001AF4C
		private bool IsRequired(XmlElement element)
		{
			XmlAttribute xmlAttribute = element.Attributes["required", "http://schemas.xmlsoap.org/wsdl/"];
			if (xmlAttribute == null || xmlAttribute.Value == null)
			{
				xmlAttribute = element.Attributes["required"];
				if (xmlAttribute == null || xmlAttribute.Value == null)
				{
					return false;
				}
			}
			return XmlConvert.ToBoolean(xmlAttribute.Value);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0001CDA3 File Offset: 0x0001AFA3
		protected override void SetParent(object value, object parent)
		{
			if (value is ServiceDescriptionFormatExtension)
			{
				((ServiceDescriptionFormatExtension)value).SetParent(parent);
			}
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001CDB9 File Offset: 0x0001AFB9
		protected override void OnValidate(object value)
		{
			if (!(value is XmlElement) && !(value is ServiceDescriptionFormatExtension))
			{
				throw new ArgumentException(Res.GetString("OnlyXmlElementsOrTypesDerivingFromServiceDescriptionFormatExtension0"), "value");
			}
			base.OnValidate(value);
		}

		// Token: 0x04000412 RID: 1042
		private ArrayList handledElements;
	}
}
