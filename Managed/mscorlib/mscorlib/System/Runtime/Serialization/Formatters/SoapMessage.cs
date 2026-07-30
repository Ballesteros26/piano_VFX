using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters
{
	/// <summary>Holds the names and types of parameters required during serialization of a SOAP RPC (Remote Procedure Call).</summary>
	// Token: 0x02000703 RID: 1795
	[ComVisible(true)]
	[Serializable]
	public class SoapMessage : ISoapMessage
	{
		/// <summary>Gets or sets the parameter names for the called method.</summary>
		/// <returns>The parameter names for the called method.</returns>
		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x06004B4A RID: 19274 RVA: 0x0010CA64 File Offset: 0x0010AC64
		// (set) Token: 0x06004B4B RID: 19275 RVA: 0x0010CA6C File Offset: 0x0010AC6C
		public string[] ParamNames
		{
			get
			{
				return this.paramNames;
			}
			set
			{
				this.paramNames = value;
			}
		}

		/// <summary>Gets or sets the parameter values for the called method.</summary>
		/// <returns>Parameter values for the called method.</returns>
		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x06004B4C RID: 19276 RVA: 0x0010CA75 File Offset: 0x0010AC75
		// (set) Token: 0x06004B4D RID: 19277 RVA: 0x0010CA7D File Offset: 0x0010AC7D
		public object[] ParamValues
		{
			get
			{
				return this.paramValues;
			}
			set
			{
				this.paramValues = value;
			}
		}

		/// <summary>This property is reserved. Use the <see cref="P:System.Runtime.Serialization.Formatters.SoapMessage.ParamNames" /> and/or <see cref="P:System.Runtime.Serialization.Formatters.SoapMessage.ParamValues" /> properties instead.</summary>
		/// <returns>Parameter types for the called method.</returns>
		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06004B4E RID: 19278 RVA: 0x0010CA86 File Offset: 0x0010AC86
		// (set) Token: 0x06004B4F RID: 19279 RVA: 0x0010CA8E File Offset: 0x0010AC8E
		public Type[] ParamTypes
		{
			get
			{
				return this.paramTypes;
			}
			set
			{
				this.paramTypes = value;
			}
		}

		/// <summary>Gets or sets the name of the called method.</summary>
		/// <returns>The name of the called method.</returns>
		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x06004B50 RID: 19280 RVA: 0x0010CA97 File Offset: 0x0010AC97
		// (set) Token: 0x06004B51 RID: 19281 RVA: 0x0010CA9F File Offset: 0x0010AC9F
		public string MethodName
		{
			get
			{
				return this.methodName;
			}
			set
			{
				this.methodName = value;
			}
		}

		/// <summary>Gets or sets the XML namespace name where the object that contains the called method is located.</summary>
		/// <returns>The XML namespace name where the object that contains the called method is located.</returns>
		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06004B52 RID: 19282 RVA: 0x0010CAA8 File Offset: 0x0010ACA8
		// (set) Token: 0x06004B53 RID: 19283 RVA: 0x0010CAB0 File Offset: 0x0010ACB0
		public string XmlNameSpace
		{
			get
			{
				return this.xmlNameSpace;
			}
			set
			{
				this.xmlNameSpace = value;
			}
		}

		/// <summary>Gets or sets the out-of-band data of the called method.</summary>
		/// <returns>The out-of-band data of the called method.</returns>
		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06004B54 RID: 19284 RVA: 0x0010CAB9 File Offset: 0x0010ACB9
		// (set) Token: 0x06004B55 RID: 19285 RVA: 0x0010CAC1 File Offset: 0x0010ACC1
		public Header[] Headers
		{
			get
			{
				return this.headers;
			}
			set
			{
				this.headers = value;
			}
		}

		// Token: 0x04002748 RID: 10056
		internal string[] paramNames;

		// Token: 0x04002749 RID: 10057
		internal object[] paramValues;

		// Token: 0x0400274A RID: 10058
		internal Type[] paramTypes;

		// Token: 0x0400274B RID: 10059
		internal string methodName;

		// Token: 0x0400274C RID: 10060
		internal string xmlNameSpace;

		// Token: 0x0400274D RID: 10061
		internal Header[] headers;
	}
}
