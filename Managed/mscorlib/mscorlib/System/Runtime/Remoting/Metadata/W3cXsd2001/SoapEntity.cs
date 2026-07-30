using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML ENTITY attribute.</summary>
	// Token: 0x020007D6 RID: 2006
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapEntity : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntity" /> class.</summary>
		// Token: 0x060050B2 RID: 20658 RVA: 0x00002111 File Offset: 0x00000311
		public SoapEntity()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntity" /> class with an XML ENTITY attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML ENTITY attribute. </param>
		// Token: 0x060050B3 RID: 20659 RVA: 0x0012011C File Offset: 0x0011E31C
		public SoapEntity(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets an XML ENTITY attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML ENTITY attribute.</returns>
		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x060050B4 RID: 20660 RVA: 0x00120130 File Offset: 0x0011E330
		// (set) Token: 0x060050B5 RID: 20661 RVA: 0x00120138 File Offset: 0x0011E338
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x060050B6 RID: 20662 RVA: 0x00120141 File Offset: 0x0011E341
		public static string XsdType
		{
			get
			{
				return "ENTITY";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060050B7 RID: 20663 RVA: 0x00120148 File Offset: 0x0011E348
		public string GetXsdType()
		{
			return SoapEntity.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntity" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntities" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x060050B8 RID: 20664 RVA: 0x0012014F File Offset: 0x0011E34F
		public static SoapEntity Parse(string value)
		{
			return new SoapEntity(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntity.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntity.Value" />.</returns>
		// Token: 0x060050B9 RID: 20665 RVA: 0x00120130 File Offset: 0x0011E330
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x04002A9E RID: 10910
		private string _value;
	}
}
