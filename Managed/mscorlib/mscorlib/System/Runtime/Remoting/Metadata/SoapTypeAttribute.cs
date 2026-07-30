using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	/// <summary>Customizes SOAP generation and processing for target types. This class cannot be inherited.</summary>
	// Token: 0x020007CC RID: 1996
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface)]
	[ComVisible(true)]
	public sealed class SoapTypeAttribute : SoapAttribute
	{
		/// <summary>Gets or sets a <see cref="T:System.Runtime.Remoting.Metadata.SoapOption" /> configuration value.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.SoapOption" /> configuration value.</returns>
		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x0600506A RID: 20586 RVA: 0x0011F8F1 File Offset: 0x0011DAF1
		// (set) Token: 0x0600506B RID: 20587 RVA: 0x0011F8F9 File Offset: 0x0011DAF9
		public SoapOption SoapOptions
		{
			get
			{
				return this._soapOption;
			}
			set
			{
				this._soapOption = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the target of the current attribute will be serialized as an XML attribute instead of an XML field.</summary>
		/// <returns>The current implementation always returns false.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">An attempt was made to set the current property. </exception>
		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x0600506C RID: 20588 RVA: 0x0011F902 File Offset: 0x0011DB02
		// (set) Token: 0x0600506D RID: 20589 RVA: 0x0011F90A File Offset: 0x0011DB0A
		public override bool UseAttribute
		{
			get
			{
				return this._useAttribute;
			}
			set
			{
				this._useAttribute = value;
			}
		}

		/// <summary>Gets or sets the XML element name.</summary>
		/// <returns>The XML element name.</returns>
		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x0600506E RID: 20590 RVA: 0x0011F913 File Offset: 0x0011DB13
		// (set) Token: 0x0600506F RID: 20591 RVA: 0x0011F91B File Offset: 0x0011DB1B
		public string XmlElementName
		{
			get
			{
				return this._xmlElementName;
			}
			set
			{
				this._isElement = value != null;
				this._xmlElementName = value;
			}
		}

		/// <summary>Gets or sets the XML field order for the target object type.</summary>
		/// <returns>The XML field order for the target object type.</returns>
		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x06005070 RID: 20592 RVA: 0x0011F92E File Offset: 0x0011DB2E
		// (set) Token: 0x06005071 RID: 20593 RVA: 0x0011F936 File Offset: 0x0011DB36
		public XmlFieldOrderOption XmlFieldOrder
		{
			get
			{
				return this._xmlFieldOrder;
			}
			set
			{
				this._xmlFieldOrder = value;
			}
		}

		/// <summary>Gets or sets the XML namespace that is used during serialization of the target object type.</summary>
		/// <returns>The XML namespace that is used during serialization of the target object type.</returns>
		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x06005072 RID: 20594 RVA: 0x0011F93F File Offset: 0x0011DB3F
		// (set) Token: 0x06005073 RID: 20595 RVA: 0x0011F947 File Offset: 0x0011DB47
		public override string XmlNamespace
		{
			get
			{
				return this._xmlNamespace;
			}
			set
			{
				this._isElement = value != null;
				this._xmlNamespace = value;
			}
		}

		/// <summary>Gets or sets the XML type name for the target object type.</summary>
		/// <returns>The XML type name for the target object type.</returns>
		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x06005074 RID: 20596 RVA: 0x0011F95A File Offset: 0x0011DB5A
		// (set) Token: 0x06005075 RID: 20597 RVA: 0x0011F962 File Offset: 0x0011DB62
		public string XmlTypeName
		{
			get
			{
				return this._xmlTypeName;
			}
			set
			{
				this._isType = value != null;
				this._xmlTypeName = value;
			}
		}

		/// <summary>Gets or sets the XML type namespace for the current object type.</summary>
		/// <returns>The XML type namespace for the current object type.</returns>
		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x06005076 RID: 20598 RVA: 0x0011F975 File Offset: 0x0011DB75
		// (set) Token: 0x06005077 RID: 20599 RVA: 0x0011F97D File Offset: 0x0011DB7D
		public string XmlTypeNamespace
		{
			get
			{
				return this._xmlTypeNamespace;
			}
			set
			{
				this._isType = value != null;
				this._xmlTypeNamespace = value;
			}
		}

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x06005078 RID: 20600 RVA: 0x0011F990 File Offset: 0x0011DB90
		internal bool IsInteropXmlElement
		{
			get
			{
				return this._isElement;
			}
		}

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x06005079 RID: 20601 RVA: 0x0011F998 File Offset: 0x0011DB98
		internal bool IsInteropXmlType
		{
			get
			{
				return this._isType;
			}
		}

		// Token: 0x0600507A RID: 20602 RVA: 0x0011F9A0 File Offset: 0x0011DBA0
		internal override void SetReflectionObject(object reflectionObject)
		{
			Type type = (Type)reflectionObject;
			if (this._xmlElementName == null)
			{
				this._xmlElementName = type.Name;
			}
			if (this._xmlTypeName == null)
			{
				this._xmlTypeName = type.Name;
			}
			if (this._xmlTypeNamespace == null)
			{
				string text;
				if (type.Assembly == typeof(object).Assembly)
				{
					text = string.Empty;
				}
				else
				{
					text = type.Assembly.GetName().Name;
				}
				this._xmlTypeNamespace = SoapServices.CodeXmlNamespaceForClrTypeNamespace(type.Namespace, text);
			}
			if (this._xmlNamespace == null)
			{
				this._xmlNamespace = this._xmlTypeNamespace;
			}
		}

		// Token: 0x04002A88 RID: 10888
		private SoapOption _soapOption;

		// Token: 0x04002A89 RID: 10889
		private bool _useAttribute;

		// Token: 0x04002A8A RID: 10890
		private string _xmlElementName;

		// Token: 0x04002A8B RID: 10891
		private XmlFieldOrderOption _xmlFieldOrder;

		// Token: 0x04002A8C RID: 10892
		private string _xmlNamespace;

		// Token: 0x04002A8D RID: 10893
		private string _xmlTypeName;

		// Token: 0x04002A8E RID: 10894
		private string _xmlTypeNamespace;

		// Token: 0x04002A8F RID: 10895
		private bool _isType;

		// Token: 0x04002A90 RID: 10896
		private bool _isElement;
	}
}
