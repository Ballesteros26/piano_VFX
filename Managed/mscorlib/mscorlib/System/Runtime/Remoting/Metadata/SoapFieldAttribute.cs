using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	/// <summary>Customizes SOAP generation and processing for a field. This class cannot be inherited.</summary>
	// Token: 0x020007C8 RID: 1992
	[AttributeUsage(AttributeTargets.Field)]
	[ComVisible(true)]
	public sealed class SoapFieldAttribute : SoapAttribute
	{
		/// <summary>Gets or sets the order of the current field attribute.</summary>
		/// <returns>The order of the current field attribute.</returns>
		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x06005054 RID: 20564 RVA: 0x0011F79A File Offset: 0x0011D99A
		// (set) Token: 0x06005055 RID: 20565 RVA: 0x0011F7A2 File Offset: 0x0011D9A2
		public int Order
		{
			get
			{
				return this._order;
			}
			set
			{
				this._order = value;
			}
		}

		/// <summary>Gets or sets the XML element name of the field contained in the <see cref="T:System.Runtime.Remoting.Metadata.SoapFieldAttribute" /> attribute.</summary>
		/// <returns>The XML element name of the field contained in this attribute.</returns>
		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x06005056 RID: 20566 RVA: 0x0011F7AB File Offset: 0x0011D9AB
		// (set) Token: 0x06005057 RID: 20567 RVA: 0x0011F7B3 File Offset: 0x0011D9B3
		public string XmlElementName
		{
			get
			{
				return this._elementName;
			}
			set
			{
				this._isElement = value != null;
				this._elementName = value;
			}
		}

		/// <summary>Returns a value indicating whether the current attribute contains interop XML element values.</summary>
		/// <returns>true if the current attribute contains interop XML element values; otherwise, false.</returns>
		// Token: 0x06005058 RID: 20568 RVA: 0x0011F7C6 File Offset: 0x0011D9C6
		public bool IsInteropXmlElement()
		{
			return this._isElement;
		}

		// Token: 0x06005059 RID: 20569 RVA: 0x0011F7D0 File Offset: 0x0011D9D0
		internal override void SetReflectionObject(object reflectionObject)
		{
			FieldInfo fieldInfo = (FieldInfo)reflectionObject;
			if (this._elementName == null)
			{
				this._elementName = fieldInfo.Name;
			}
		}

		// Token: 0x04002A78 RID: 10872
		private int _order;

		// Token: 0x04002A79 RID: 10873
		private string _elementName;

		// Token: 0x04002A7A RID: 10874
		private bool _isElement;
	}
}
