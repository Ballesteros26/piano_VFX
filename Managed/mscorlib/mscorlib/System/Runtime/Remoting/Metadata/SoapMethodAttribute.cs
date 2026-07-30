using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	/// <summary>Customizes SOAP generation and processing for a method. This class cannot be inherited.</summary>
	// Token: 0x020007C9 RID: 1993
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class SoapMethodAttribute : SoapAttribute
	{
		/// <summary>Gets or sets the XML element name to use for the method response to the target method.</summary>
		/// <returns>The XML element name to use for the method response to the target method.</returns>
		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x0600505B RID: 20571 RVA: 0x0011F7F8 File Offset: 0x0011D9F8
		// (set) Token: 0x0600505C RID: 20572 RVA: 0x0011F800 File Offset: 0x0011DA00
		public string ResponseXmlElementName
		{
			get
			{
				return this._responseElement;
			}
			set
			{
				this._responseElement = value;
			}
		}

		/// <summary>Gets or sets the XML element namesapce used for method response to the target method.</summary>
		/// <returns>The XML element namesapce used for method response to the target method.</returns>
		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x0600505D RID: 20573 RVA: 0x0011F809 File Offset: 0x0011DA09
		// (set) Token: 0x0600505E RID: 20574 RVA: 0x0011F811 File Offset: 0x0011DA11
		public string ResponseXmlNamespace
		{
			get
			{
				return this._responseNamespace;
			}
			set
			{
				this._responseNamespace = value;
			}
		}

		/// <summary>Gets or sets the XML element name used for the return value from the target method.</summary>
		/// <returns>The XML element name used for the return value from the target method.</returns>
		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x0600505F RID: 20575 RVA: 0x0011F81A File Offset: 0x0011DA1A
		// (set) Token: 0x06005060 RID: 20576 RVA: 0x0011F822 File Offset: 0x0011DA22
		public string ReturnXmlElementName
		{
			get
			{
				return this._returnElement;
			}
			set
			{
				this._returnElement = value;
			}
		}

		/// <summary>Gets or sets the SOAPAction header field used with HTTP requests sent with this method. This property is currently not implemented.</summary>
		/// <returns>The SOAPAction header field used with HTTP requests sent with this method.</returns>
		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x06005061 RID: 20577 RVA: 0x0011F82B File Offset: 0x0011DA2B
		// (set) Token: 0x06005062 RID: 20578 RVA: 0x0011F833 File Offset: 0x0011DA33
		public string SoapAction
		{
			get
			{
				return this._soapAction;
			}
			set
			{
				this._soapAction = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the target of the current attribute will be serialized as an XML attribute instead of an XML field.</summary>
		/// <returns>The current implementation always returns false.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">An attempt was made to set the current property. </exception>
		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x06005063 RID: 20579 RVA: 0x0011F83C File Offset: 0x0011DA3C
		// (set) Token: 0x06005064 RID: 20580 RVA: 0x0011F844 File Offset: 0x0011DA44
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

		/// <summary>Gets or sets the XML namespace that is used during serialization of remote method calls of the target method.</summary>
		/// <returns>The XML namespace that is used during serialization of remote method calls of the target method.</returns>
		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x06005065 RID: 20581 RVA: 0x0011F84D File Offset: 0x0011DA4D
		// (set) Token: 0x06005066 RID: 20582 RVA: 0x0011F855 File Offset: 0x0011DA55
		public override string XmlNamespace
		{
			get
			{
				return this._namespace;
			}
			set
			{
				this._namespace = value;
			}
		}

		// Token: 0x06005067 RID: 20583 RVA: 0x0011F860 File Offset: 0x0011DA60
		internal override void SetReflectionObject(object reflectionObject)
		{
			MethodBase methodBase = (MethodBase)reflectionObject;
			if (this._responseElement == null)
			{
				this._responseElement = methodBase.Name + "Response";
			}
			if (this._responseNamespace == null)
			{
				this._responseNamespace = SoapServices.GetXmlNamespaceForMethodResponse(methodBase);
			}
			if (this._returnElement == null)
			{
				this._returnElement = "return";
			}
			if (this._soapAction == null)
			{
				this._soapAction = SoapServices.GetXmlNamespaceForMethodCall(methodBase) + "#" + methodBase.Name;
			}
			if (this._namespace == null)
			{
				this._namespace = SoapServices.GetXmlNamespaceForMethodCall(methodBase);
			}
		}

		// Token: 0x04002A7B RID: 10875
		private string _responseElement;

		// Token: 0x04002A7C RID: 10876
		private string _responseNamespace;

		// Token: 0x04002A7D RID: 10877
		private string _returnElement;

		// Token: 0x04002A7E RID: 10878
		private string _soapAction;

		// Token: 0x04002A7F RID: 10879
		private bool _useAttribute;

		// Token: 0x04002A80 RID: 10880
		private string _namespace;
	}
}
