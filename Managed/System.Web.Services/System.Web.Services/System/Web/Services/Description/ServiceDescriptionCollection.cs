using System;
using System.Xml;

namespace System.Web.Services.Description
{
	/// <summary>Represents a collection of instances of the <see cref="T:System.Web.Services.Description.ServiceDescription" /> class. This class cannot be inherited.</summary>
	// Token: 0x02000113 RID: 275
	public sealed class ServiceDescriptionCollection : ServiceDescriptionBaseCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" /> class.</summary>
		// Token: 0x06000865 RID: 2149 RVA: 0x0003BBB8 File Offset: 0x00039DB8
		public ServiceDescriptionCollection()
			: base(null)
		{
		}

		/// <summary>Gets or sets the value of a <see cref="T:System.Web.Services.Description.ServiceDescription" /> at the specified zero-based index.</summary>
		/// <returns>The value of a <see cref="T:System.Web.Services.Description.ServiceDescription" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Description.ServiceDescription" /> whose value is modified or returned.</param>
		// Token: 0x17000225 RID: 549
		public ServiceDescription this[int index]
		{
			get
			{
				return (ServiceDescription)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.Services.Description.ServiceDescription" /> specified by its <see cref="P:System.Web.Services.Description.ServiceDescription.TargetNamespace" /> property.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Description.ServiceDescription" /> specified by its namespace property.</returns>
		/// <param name="ns">The namespace of the <see cref="T:System.Web.Services.Description.ServiceDescription" /> returned.</param>
		// Token: 0x17000226 RID: 550
		public ServiceDescription this[string ns]
		{
			get
			{
				return (ServiceDescription)this.Table[ns];
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.ServiceDescription" /> to the end of the <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" />.</summary>
		/// <returns>The zero-based index where the <see cref="T:System.Web.Services.Description.ServiceDescription" /> parameter has been added.</returns>
		/// <param name="serviceDescription">The <see cref="T:System.Web.Services.Description.ServiceDescription" /> to add to the collection.</param>
		// Token: 0x06000869 RID: 2153 RVA: 0x0000CD4B File Offset: 0x0000AF4B
		public int Add(ServiceDescription serviceDescription)
		{
			return base.List.Add(serviceDescription);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.ServiceDescription" /> instance to the <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" /> at the specified zero-based index.</summary>
		/// <param name="index">The zero-based index at which to insert the <paramref name="serviceDescription" /> parameter.</param>
		/// <param name="serviceDescription">The <see cref="T:System.Web.Services.Description.ServiceDescription" /> to add to the collection.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is less than zero.- or - The <paramref name="index" /> parameter is greater than <see cref="P:System.Collections.CollectionBase.Count" />.</exception>
		// Token: 0x0600086A RID: 2154 RVA: 0x0000CD59 File Offset: 0x0000AF59
		public void Insert(int index, ServiceDescription serviceDescription)
		{
			base.List.Insert(index, serviceDescription);
		}

		/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.ServiceDescription" /> and returns the zero-based index of the first occurrence within the collection.</summary>
		/// <returns>The zero-based index of the specified service description, or -1 if the element was not found in the collection.</returns>
		/// <param name="serviceDescription">The <see cref="T:System.Web.Services.Description.ServiceDescription" /> for which to search in the collection.</param>
		// Token: 0x0600086B RID: 2155 RVA: 0x0000CD68 File Offset: 0x0000AF68
		public int IndexOf(ServiceDescription serviceDescription)
		{
			return base.List.IndexOf(serviceDescription);
		}

		/// <summary>Returns a value indicating whether the specified <see cref="T:System.Web.Services.Description.ServiceDescription" /> is a member of the collection.</summary>
		/// <returns>true if the <paramref name="serviceDescription" /> parameter is a member of the <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" />; otherwise, false.</returns>
		/// <param name="serviceDescription">The <see cref="T:System.Web.Services.Description.ServiceDescription" /> for which to check collection membership.</param>
		// Token: 0x0600086C RID: 2156 RVA: 0x0000CD76 File Offset: 0x0000AF76
		public bool Contains(ServiceDescription serviceDescription)
		{
			return base.List.Contains(serviceDescription);
		}

		/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.Services.Description.ServiceDescription" /> from the collection.</summary>
		/// <param name="serviceDescription">The <see cref="T:System.Web.Services.Description.ServiceDescription" /> to remove from the collection.</param>
		// Token: 0x0600086D RID: 2157 RVA: 0x0000CD84 File Offset: 0x0000AF84
		public void Remove(ServiceDescription serviceDescription)
		{
			base.List.Remove(serviceDescription);
		}

		/// <summary>Copies the entire <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" /> to a one-dimensional array of type <see cref="T:System.Web.Services.Description.ServiceDescription" />, starting at the specified zero-based index of the target array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.ServiceDescription" /> serving as the destination of the copy action.</param>
		/// <param name="index">The zero-based index at which to start placing the copied collection.</param>
		// Token: 0x0600086E RID: 2158 RVA: 0x0000CD92 File Offset: 0x0000AF92
		public void CopyTo(ServiceDescription[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0003BBE8 File Offset: 0x00039DE8
		protected override string GetKey(object value)
		{
			string targetNamespace = ((ServiceDescription)value).TargetNamespace;
			if (targetNamespace == null)
			{
				return string.Empty;
			}
			return targetNamespace;
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0003BC0B File Offset: 0x00039E0B
		private Exception ItemNotFound(XmlQualifiedName name, string type)
		{
			return new Exception(Res.GetString("WebDescriptionMissingItem", new object[] { type, name.Name, name.Namespace }));
		}

		/// <summary>Searches the <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" /> and returns the <see cref="T:System.Web.Services.Description.Message" /> with the specified name that is a member of one of the <see cref="T:System.Web.Services.Description.ServiceDescription" /> instances contained in the collection.</summary>
		/// <returns>The message with the specified name.</returns>
		/// <param name="name">The <see cref="T:System.Xml.XmlQualifiedName" />, passed by reference, whose <see cref="P:System.Xml.XmlQualifiedName.Name" /> property is shared by the <see cref="T:System.Web.Services.Description.Message" /> returned.</param>
		/// <exception cref="T:System.Exception">The specified Message is not a member of any <see cref="T:System.Web.Services.Description.ServiceDescription" /> instances within the collection. </exception>
		// Token: 0x06000871 RID: 2161 RVA: 0x0003BC38 File Offset: 0x00039E38
		public Message GetMessage(XmlQualifiedName name)
		{
			ServiceDescription serviceDescription = this.GetServiceDescription(name);
			Message message = null;
			while (message == null && serviceDescription != null)
			{
				message = serviceDescription.Messages[name.Name];
				serviceDescription = serviceDescription.Next;
			}
			if (message == null)
			{
				throw this.ItemNotFound(name, "message");
			}
			return message;
		}

		/// <summary>Searches the <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" /> and returns the <see cref="T:System.Web.Services.Description.PortType" /> with the specified name that is a member of one of the <see cref="T:System.Web.Services.Description.ServiceDescription" /> instances contained in the collection.</summary>
		/// <returns>The PortType with the specified name.</returns>
		/// <param name="name">The <see cref="T:System.Xml.XmlQualifiedName" />, passed by reference, whose <see cref="P:System.Xml.XmlQualifiedName.Name" /> property is shared by the <see cref="T:System.Web.Services.Description.PortType" /> returned.</param>
		/// <exception cref="T:System.Exception">The specified PortType is not a member of any <see cref="T:System.Web.Services.Description.ServiceDescription" /> instances within the collection.</exception>
		// Token: 0x06000872 RID: 2162 RVA: 0x0003BC84 File Offset: 0x00039E84
		public PortType GetPortType(XmlQualifiedName name)
		{
			ServiceDescription serviceDescription = this.GetServiceDescription(name);
			PortType portType = null;
			while (portType == null && serviceDescription != null)
			{
				portType = serviceDescription.PortTypes[name.Name];
				serviceDescription = serviceDescription.Next;
			}
			if (portType == null)
			{
				throw this.ItemNotFound(name, "message");
			}
			return portType;
		}

		/// <summary>Searches the <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" /> and returns the <see cref="T:System.Web.Services.Description.Service" /> with the specified name that is a member of one of the <see cref="T:System.Web.Services.Description.ServiceDescription" /> instances contained in the collection.</summary>
		/// <returns>The service with the specified name.</returns>
		/// <param name="name">The <see cref="T:System.Xml.XmlQualifiedName" />, passed by reference, whose <see cref="P:System.Xml.XmlQualifiedName.Name" /> property is shared by the <see cref="T:System.Web.Services.Description.Service" /> returned.</param>
		/// <exception cref="T:System.Exception">The specified Service is not a member of any <see cref="T:System.Web.Services.Description.ServiceDescription" /> instances within the collection. </exception>
		// Token: 0x06000873 RID: 2163 RVA: 0x0003BCD0 File Offset: 0x00039ED0
		public Service GetService(XmlQualifiedName name)
		{
			ServiceDescription serviceDescription = this.GetServiceDescription(name);
			Service service = null;
			while (service == null && serviceDescription != null)
			{
				service = serviceDescription.Services[name.Name];
				serviceDescription = serviceDescription.Next;
			}
			if (service == null)
			{
				throw this.ItemNotFound(name, "service");
			}
			return service;
		}

		/// <summary>Searches the <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" /> and returns the <see cref="T:System.Web.Services.Description.Binding" /> with the specified name that is a member of one of the <see cref="T:System.Web.Services.Description.ServiceDescription" /> instances contained in the collection.</summary>
		/// <returns>The binding with the specified name.</returns>
		/// <param name="name">An <see cref="T:System.Xml.XmlQualifiedName" /> whose <see cref="P:System.Xml.XmlQualifiedName.Name" /> property is used to retrieve a <see cref="T:System.Web.Services.Description.Binding" /> instance.</param>
		/// <exception cref="T:System.Exception">The specified Binding is not a member of any <see cref="T:System.Web.Services.Description.ServiceDescription" /> instances within the collection.</exception>
		// Token: 0x06000874 RID: 2164 RVA: 0x0003BD1C File Offset: 0x00039F1C
		public Binding GetBinding(XmlQualifiedName name)
		{
			ServiceDescription serviceDescription = this.GetServiceDescription(name);
			Binding binding = null;
			while (binding == null && serviceDescription != null)
			{
				binding = serviceDescription.Bindings[name.Name];
				serviceDescription = serviceDescription.Next;
			}
			if (binding == null)
			{
				throw this.ItemNotFound(name, "binding");
			}
			return binding;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0003BD68 File Offset: 0x00039F68
		private ServiceDescription GetServiceDescription(XmlQualifiedName name)
		{
			ServiceDescription serviceDescription = this[name.Namespace];
			if (serviceDescription == null)
			{
				throw new ArgumentException(Res.GetString("WebDescriptionMissing", new object[]
				{
					name.ToString(),
					name.Namespace
				}), "name");
			}
			return serviceDescription;
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0003BDB3 File Offset: 0x00039FB3
		protected override void SetParent(object value, object parent)
		{
			((ServiceDescription)value).SetParent((ServiceDescriptionCollection)parent);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0003BDC8 File Offset: 0x00039FC8
		protected override void OnInsertComplete(int index, object value)
		{
			string key = this.GetKey(value);
			if (key != null)
			{
				ServiceDescription serviceDescription = (ServiceDescription)this.Table[key];
				((ServiceDescription)value).Next = (ServiceDescription)this.Table[key];
				this.Table[key] = value;
			}
			this.SetParent(value, this);
		}
	}
}
