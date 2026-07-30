using System;
using System.Runtime.InteropServices;
using System.Web.Services.Description;

namespace System.Web.Services.Protocols
{
	/// <summary>Specifies that SOAP messages sent to and from the method use RPC formatting.</summary>
	// Token: 0x02000079 RID: 121
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class SoapRpcMethodAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapRpcMethodAttribute" /> class, setting all properties to their default values.</summary>
		// Token: 0x06000315 RID: 789 RVA: 0x0000E422 File Offset: 0x0000C622
		public SoapRpcMethodAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapRpcMethodAttribute" /> class, setting the <see cref="P:System.Web.Services.Protocols.SoapRpcMethodAttribute.Action" /> property to the value of the <paramref name="action" /> parameter.</summary>
		/// <param name="action">The intent of the SOAP request. Sets the <see cref="P:System.Web.Services.Protocols.SoapRpcMethodAttribute.Action" /> property. </param>
		// Token: 0x06000316 RID: 790 RVA: 0x0000E431 File Offset: 0x0000C631
		public SoapRpcMethodAttribute(string action)
		{
			this.action = action;
		}

		/// <summary>Gets or sets the SOAPAction HTTP header field of the SOAP request.</summary>
		/// <returns>The SOAPAction HTTP header field of the SOAP request. The default is http://tempuri.org/MethodName where MethodName is the name of the XML Web service method.</returns>
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000E447 File Offset: 0x0000C647
		// (set) Token: 0x06000318 RID: 792 RVA: 0x0000E44F File Offset: 0x0000C64F
		public string Action
		{
			get
			{
				return this.action;
			}
			set
			{
				this.action = value;
			}
		}

		/// <summary>Gets or sets the binding that an XML Web service method implements an operation for.</summary>
		/// <returns>The binding an XML Web service method implements an operation for. The default is the name of the XML Web service with "Soap" appended.</returns>
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000E458 File Offset: 0x0000C658
		// (set) Token: 0x0600031A RID: 794 RVA: 0x0000E46E File Offset: 0x0000C66E
		public string Binding
		{
			get
			{
				if (this.binding != null)
				{
					return this.binding;
				}
				return string.Empty;
			}
			set
			{
				this.binding = value;
			}
		}

		/// <summary>Gets or sets whether an XML Web service client waits for the Web server to finish processing an XML Web service method.</summary>
		/// <returns>true if the XML Web service client does not wait for the Web server to completely process an XML Web service method; otherwise, false. The default is false.</returns>
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000E477 File Offset: 0x0000C677
		// (set) Token: 0x0600031C RID: 796 RVA: 0x0000E47F File Offset: 0x0000C67F
		public bool OneWay
		{
			get
			{
				return this.oneWay;
			}
			set
			{
				this.oneWay = value;
			}
		}

		/// <summary>Gets or sets the XML namespace associated with the SOAP request for an XML Web service method.</summary>
		/// <returns>The XML namespace associated with the SOAP request for an XML Web service method. The default is http://tempuri.org/.</returns>
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000E488 File Offset: 0x0000C688
		// (set) Token: 0x0600031E RID: 798 RVA: 0x0000E490 File Offset: 0x0000C690
		public string RequestNamespace
		{
			get
			{
				return this.requestNamespace;
			}
			set
			{
				this.requestNamespace = value;
			}
		}

		/// <summary>Gets or sets the XML namespace associated with the SOAP response for an XML Web service method.</summary>
		/// <returns>The XML namespace associated with the SOAP response for an XML Web service method. The default is http://tempuri.org/.</returns>
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000E499 File Offset: 0x0000C699
		// (set) Token: 0x06000320 RID: 800 RVA: 0x0000E4A1 File Offset: 0x0000C6A1
		public string ResponseNamespace
		{
			get
			{
				return this.responseNamespace;
			}
			set
			{
				this.responseNamespace = value;
			}
		}

		/// <summary>Gets or sets the XML element associated with the SOAP request for an XML Web service method.</summary>
		/// <returns>The XML element associated with the SOAP request for an XML Web service method. The default value is the name of the XML Web service method.</returns>
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000E4AA File Offset: 0x0000C6AA
		// (set) Token: 0x06000322 RID: 802 RVA: 0x0000E4C0 File Offset: 0x0000C6C0
		public string RequestElementName
		{
			get
			{
				if (this.requestName != null)
				{
					return this.requestName;
				}
				return string.Empty;
			}
			set
			{
				this.requestName = value;
			}
		}

		/// <summary>Gets or sets the XML element associated with the SOAP response for an XML Web service method.</summary>
		/// <returns>The XML element associated with the SOAP request for an XML Web service method. The default value is WebServiceNameResult, where WebServiceName is the name of the XML Web service method.</returns>
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000E4C9 File Offset: 0x0000C6C9
		// (set) Token: 0x06000324 RID: 804 RVA: 0x0000E4DF File Offset: 0x0000C6DF
		public string ResponseElementName
		{
			get
			{
				if (this.responseName != null)
				{
					return this.responseName;
				}
				return string.Empty;
			}
			set
			{
				this.responseName = value;
			}
		}

		/// <summary>Gets or sets the binding used when invoking the method.</summary>
		/// <returns>A member of the <see cref="T:System.Web.Services.Description.SoapBindingUse" /> enumeration specifying the binding used when invoking the method.</returns>
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000E4E8 File Offset: 0x0000C6E8
		// (set) Token: 0x06000326 RID: 806 RVA: 0x0000E4F0 File Offset: 0x0000C6F0
		[ComVisible(false)]
		public SoapBindingUse Use
		{
			get
			{
				return this.use;
			}
			set
			{
				this.use = value;
			}
		}

		// Token: 0x040002CB RID: 715
		private string action;

		// Token: 0x040002CC RID: 716
		private string requestName;

		// Token: 0x040002CD RID: 717
		private string responseName;

		// Token: 0x040002CE RID: 718
		private string requestNamespace;

		// Token: 0x040002CF RID: 719
		private string responseNamespace;

		// Token: 0x040002D0 RID: 720
		private bool oneWay;

		// Token: 0x040002D1 RID: 721
		private string binding;

		// Token: 0x040002D2 RID: 722
		private SoapBindingUse use = SoapBindingUse.Encoded;
	}
}
