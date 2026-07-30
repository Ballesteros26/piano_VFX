using System;

namespace System.Web.Services.Protocols
{
	/// <summary>This attribute is applied to an XML Web service method or an XML Web service client to specify a SOAP header that the XML Web service method or XML Web service client can process. This class cannot be inherited.</summary>
	// Token: 0x0200006A RID: 106
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public sealed class SoapHeaderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapHeaderAttribute" /> class, setting the member of the XML Web service class representing the SOAP header contents.</summary>
		/// <param name="memberName">The member of the XML Web service class representing the SOAP header contents. The <see cref="P:System.Web.Services.Protocols.SoapHeaderAttribute.MemberName" /> property will be set to the value of this parameter. </param>
		// Token: 0x060002BF RID: 703 RVA: 0x0000CC4C File Offset: 0x0000AE4C
		public SoapHeaderAttribute(string memberName)
		{
			this.memberName = memberName;
		}

		/// <summary>Gets or sets the member of the XML Web service class representing the SOAP header contents.</summary>
		/// <returns>The member of the XML Web service class representing the SOAP header contents. There is no default.</returns>
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000CC69 File Offset: 0x0000AE69
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x0000CC7F File Offset: 0x0000AE7F
		public string MemberName
		{
			get
			{
				if (this.memberName != null)
				{
					return this.memberName;
				}
				return string.Empty;
			}
			set
			{
				this.memberName = value;
			}
		}

		/// <summary>Gets or sets whether the SOAP header is intended for the XML Web service or the XML Web service client or both.</summary>
		/// <returns>The intended recipient of the SOAP header. The default is <see cref="F:System.Web.Services.Protocols.SoapHeaderDirection.In" />, which means the intended recipient is just the XML Web service.</returns>
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000CC88 File Offset: 0x0000AE88
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x0000CC90 File Offset: 0x0000AE90
		public SoapHeaderDirection Direction
		{
			get
			{
				return this.direction;
			}
			set
			{
				this.direction = value;
			}
		}

		/// <summary>This member is obsolete and has no functionality.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value.</returns>
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000CC99 File Offset: 0x0000AE99
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x0000CCA1 File Offset: 0x0000AEA1
		[Obsolete("This property will be removed from a future version. The presence of a particular header in a SOAP message is no longer enforced", false)]
		public bool Required
		{
			get
			{
				return this.required;
			}
			set
			{
				this.required = value;
			}
		}

		// Token: 0x0400028D RID: 653
		private string memberName;

		// Token: 0x0400028E RID: 654
		private SoapHeaderDirection direction = SoapHeaderDirection.In;

		// Token: 0x0400028F RID: 655
		private bool required = true;
	}
}
