using System;

namespace System.Web.Services.Configuration
{
	/// <summary>Specifies that a service description format extension runs at one or more extension points. This class cannot be inherited.</summary>
	// Token: 0x0200014E RID: 334
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class XmlFormatExtensionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.XmlFormatExtensionAttribute" /> class.</summary>
		// Token: 0x06000A5B RID: 2651 RVA: 0x000028A3 File Offset: 0x00000AA3
		public XmlFormatExtensionAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.XmlFormatExtensionAttribute" /> class that specifies the XML element and namespace to add when running at the specified extension point.</summary>
		/// <param name="elementName">The XML element added to the service description by the service description format extension.</param>
		/// <param name="ns">The XML namespace for the XML element added to the service description by the service description format extension.</param>
		/// <param name="extensionPoint1">The extension point at which to run the service description format extension.</param>
		// Token: 0x06000A5C RID: 2652 RVA: 0x000455BD File Offset: 0x000437BD
		public XmlFormatExtensionAttribute(string elementName, string ns, Type extensionPoint1)
			: this(elementName, ns, new Type[] { extensionPoint1 })
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.XmlFormatExtensionAttribute" /> class that specifies the XML element and namespace to add when running at the specified extension points.</summary>
		/// <param name="elementName">The XML element added to the service description by the service description format extension.</param>
		/// <param name="ns">The XML namespace for the XML element added to the service description by the service description format extension.</param>
		/// <param name="extensionPoint1">An extension point at which to run the service description format extension.</param>
		/// <param name="extensionPoint2">An extension point at which to run the service description format extension.</param>
		// Token: 0x06000A5D RID: 2653 RVA: 0x000455D1 File Offset: 0x000437D1
		public XmlFormatExtensionAttribute(string elementName, string ns, Type extensionPoint1, Type extensionPoint2)
			: this(elementName, ns, new Type[] { extensionPoint1, extensionPoint2 })
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.XmlFormatExtensionAttribute" /> class that specifies the XML element and namespace to add when running at the specified extension points.</summary>
		/// <param name="elementName">The XML element added to the service description by the service description format extension.</param>
		/// <param name="ns">The XML namespace for the XML element added to the service description by the service description format extension.</param>
		/// <param name="extensionPoint1">An extension point at which to run the service description format extension.</param>
		/// <param name="extensionPoint2">An extension point at which to run the service description format extension.</param>
		/// <param name="extensionPoint3">An extension point at which to run the service description format extension.</param>
		// Token: 0x06000A5E RID: 2654 RVA: 0x000455EA File Offset: 0x000437EA
		public XmlFormatExtensionAttribute(string elementName, string ns, Type extensionPoint1, Type extensionPoint2, Type extensionPoint3)
			: this(elementName, ns, new Type[] { extensionPoint1, extensionPoint2, extensionPoint3 })
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.XmlFormatExtensionAttribute" /> class that specifies the XML element and namespace to add when running at the specified extension points.</summary>
		/// <param name="elementName">The XML element added to the service description by the service description format extension.</param>
		/// <param name="ns">The XML namespace for the XML element added to the service description by the service description format extension.</param>
		/// <param name="extensionPoint1">An extension point at which to run the service description format extension.</param>
		/// <param name="extensionPoint2">An extension point at which to run the service description format extension.</param>
		/// <param name="extensionPoint3">An extension point at which to run the service description format extension.</param>
		/// <param name="extensionPoint4">An extension point at which to run the service description format extension. </param>
		// Token: 0x06000A5F RID: 2655 RVA: 0x00045608 File Offset: 0x00043808
		public XmlFormatExtensionAttribute(string elementName, string ns, Type extensionPoint1, Type extensionPoint2, Type extensionPoint3, Type extensionPoint4)
			: this(elementName, ns, new Type[] { extensionPoint1, extensionPoint2, extensionPoint3, extensionPoint4 })
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.XmlFormatExtensionAttribute" /> class that specifies the XML element and namespace to add when running at the specified extension points.</summary>
		/// <param name="elementName">The XML element added to the service description by the service description format extension. </param>
		/// <param name="ns">The XML namespace for the XML element added to the service description by the service description format extension. </param>
		/// <param name="extensionPoints">An array of extension points at which to run the service description format extension. </param>
		// Token: 0x06000A60 RID: 2656 RVA: 0x0004562B File Offset: 0x0004382B
		public XmlFormatExtensionAttribute(string elementName, string ns, Type[] extensionPoints)
		{
			this.name = elementName;
			this.ns = ns;
			this.types = extensionPoints;
		}

		/// <summary>The stages at which the service description format extension is to run.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> that specifies the stage at which the service description format extension is to run.</returns>
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x00045648 File Offset: 0x00043848
		// (set) Token: 0x06000A62 RID: 2658 RVA: 0x0004565F File Offset: 0x0004385F
		public Type[] ExtensionPoints
		{
			get
			{
				if (this.types != null)
				{
					return this.types;
				}
				return new Type[0];
			}
			set
			{
				this.types = value;
			}
		}

		/// <summary>Gets or sets the XML namespace for the XML element added to the service description by the service description format extension.</summary>
		/// <returns>The XML namespace for the XML element added to the service description by the service description format extension.</returns>
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x00045668 File Offset: 0x00043868
		// (set) Token: 0x06000A64 RID: 2660 RVA: 0x0004567E File Offset: 0x0004387E
		public string Namespace
		{
			get
			{
				if (this.ns != null)
				{
					return this.ns;
				}
				return string.Empty;
			}
			set
			{
				this.ns = value;
			}
		}

		/// <summary>Gets or sets the XML element added to the service description by the service description format extension.</summary>
		/// <returns>The XML element added to the service description by the service description format extension.</returns>
		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x00045687 File Offset: 0x00043887
		// (set) Token: 0x06000A66 RID: 2662 RVA: 0x0004569D File Offset: 0x0004389D
		public string ElementName
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x040005D4 RID: 1492
		private Type[] types;

		// Token: 0x040005D5 RID: 1493
		private string name;

		// Token: 0x040005D6 RID: 1494
		private string ns;
	}
}
