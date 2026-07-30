using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD positiveInteger type.</summary>
	// Token: 0x020007E9 RID: 2025
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapPositiveInteger : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapPositiveInteger" /> class.</summary>
		// Token: 0x0600514A RID: 20810 RVA: 0x00002111 File Offset: 0x00000311
		public SoapPositiveInteger()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapInteger" /> class with a <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Decimal" /> value to initialize the current instance. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> is less than 1. </exception>
		// Token: 0x0600514B RID: 20811 RVA: 0x001207C2 File Offset: 0x0011E9C2
		public SoapPositiveInteger(decimal value)
		{
			if (value <= 0m)
			{
				throw SoapHelper.GetException(this, "invalid " + value);
			}
			this._value = value;
		}

		/// <summary>Gets or sets the numeric value of the current instance.</summary>
		/// <returns>A <see cref="T:System.Decimal" /> indicating the numeric value of the current instance.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> is less than 1. </exception>
		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x0600514C RID: 20812 RVA: 0x001207F5 File Offset: 0x0011E9F5
		// (set) Token: 0x0600514D RID: 20813 RVA: 0x001207FD File Offset: 0x0011E9FD
		public decimal Value
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
		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x0600514E RID: 20814 RVA: 0x00120806 File Offset: 0x0011EA06
		public static string XsdType
		{
			get
			{
				return "positiveInteger";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x0600514F RID: 20815 RVA: 0x0012080D File Offset: 0x0011EA0D
		public string GetXsdType()
		{
			return SoapPositiveInteger.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapPositiveInteger" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapPositiveInteger" /> object that is obtained from <paramref name="value" />. </returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x06005150 RID: 20816 RVA: 0x00120814 File Offset: 0x0011EA14
		public static SoapPositiveInteger Parse(string value)
		{
			return new SoapPositiveInteger(decimal.Parse(value));
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapPositiveInteger.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapPositiveInteger.Value" />.</returns>
		// Token: 0x06005151 RID: 20817 RVA: 0x00120821 File Offset: 0x0011EA21
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04002AB3 RID: 10931
		private decimal _value;
	}
}
