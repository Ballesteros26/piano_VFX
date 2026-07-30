using System;

namespace System.Web.Services.Protocols
{
	/// <summary>Applying this attribute to an XML Web service client using HTTP-GET or HTTP-POST, sets the types that serialize the parameters sent to an XML Web service method and read the response from the XML Web service method. This class cannot be inherited.</summary>
	// Token: 0x02000036 RID: 54
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class HttpMethodAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.HttpMethodAttribute" /> class.</summary>
		// Token: 0x0600011D RID: 285 RVA: 0x00005680 File Offset: 0x00003880
		public HttpMethodAttribute()
		{
			this.returnFormatter = null;
			this.parameterFormatter = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.HttpMethodAttribute" />.</summary>
		/// <param name="returnFormatter">Initializes the <see cref="P:System.Web.Services.Protocols.HttpMethodAttribute.ReturnFormatter" /> property to a <see cref="T:System.Type" /> that deserializes the response from an XML Web service method. </param>
		/// <param name="parameterFormatter">Initializes the <see cref="P:System.Web.Services.Protocols.HttpMethodAttribute.ParameterFormatter" /> property to a <see cref="T:System.Type" /> that serializes parameters sent from an XML Web service client to an XML Web service method. </param>
		// Token: 0x0600011E RID: 286 RVA: 0x00005696 File Offset: 0x00003896
		public HttpMethodAttribute(Type returnFormatter, Type parameterFormatter)
		{
			this.returnFormatter = returnFormatter;
			this.parameterFormatter = parameterFormatter;
		}

		/// <summary>Gets or sets a <see cref="T:System.Type" /> that deserializes the response from an XML Web service method.</summary>
		/// <returns>A <see cref="T:System.Type" /> that deserializes the response from an XML Web service method. There is no default.</returns>
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600011F RID: 287 RVA: 0x000056AC File Offset: 0x000038AC
		// (set) Token: 0x06000120 RID: 288 RVA: 0x000056B4 File Offset: 0x000038B4
		public Type ReturnFormatter
		{
			get
			{
				return this.returnFormatter;
			}
			set
			{
				this.returnFormatter = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Type" /> that serializes parameters sent from an XML Web service client to the XML Web service method.</summary>
		/// <returns>A <see cref="T:System.Type" /> that serializes parameters sent from an XML Web service client to an XML Web service method. There is no default.</returns>
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000121 RID: 289 RVA: 0x000056BD File Offset: 0x000038BD
		// (set) Token: 0x06000122 RID: 290 RVA: 0x000056C5 File Offset: 0x000038C5
		public Type ParameterFormatter
		{
			get
			{
				return this.parameterFormatter;
			}
			set
			{
				this.parameterFormatter = value;
			}
		}

		// Token: 0x040001F1 RID: 497
		private Type returnFormatter;

		// Token: 0x040001F2 RID: 498
		private Type parameterFormatter;
	}
}
