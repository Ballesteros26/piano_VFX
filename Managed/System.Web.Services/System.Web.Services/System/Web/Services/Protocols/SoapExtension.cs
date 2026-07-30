using System;
using System.IO;

namespace System.Web.Services.Protocols
{
	/// <summary>The base class for SOAP extensions for XML Web services created using ASP.NET.</summary>
	// Token: 0x02000062 RID: 98
	public abstract class SoapExtension
	{
		/// <summary>When overridden in a derived class, allows a SOAP extension to initialize data specific to an XML Web service method using an attribute applied to the XML Web service method at a one time performance cost.</summary>
		/// <returns>The <see cref="T:System.Object" /> that the SOAP extension initializes for caching.</returns>
		/// <param name="methodInfo">A <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> representing the specific function prototype for the XML Web service method to which the SOAP extension is applied. </param>
		/// <param name="attribute">The <see cref="T:System.Web.Services.Protocols.SoapExtensionAttribute" /> applied to the XML Web service method. </param>
		// Token: 0x0600026F RID: 623
		public abstract object GetInitializer(LogicalMethodInfo methodInfo, SoapExtensionAttribute attribute);

		/// <summary>When overridden in a derived class, allows a SOAP extension to initialize data specific to a class implementing an XML Web service at a one time performance cost.</summary>
		/// <returns>The <see cref="T:System.Object" /> that the SOAP extension initializes for caching.</returns>
		/// <param name="serviceType">The type of the class implementing the XML Web service to which the SOAP extension is applied. </param>
		// Token: 0x06000270 RID: 624
		public abstract object GetInitializer(Type serviceType);

		/// <summary>When overridden in a derived class, allows a SOAP extension to initialize itself using the data cached in the <see cref="M:System.Web.Services.Protocols.SoapExtension.GetInitializer(System.Web.Services.Protocols.LogicalMethodInfo,System.Web.Services.Protocols.SoapExtensionAttribute)" /> method.</summary>
		/// <param name="initializer">The <see cref="T:System.Object" /> returned from <see cref="M:System.Web.Services.Protocols.SoapExtension.GetInitializer(System.Web.Services.Protocols.LogicalMethodInfo,System.Web.Services.Protocols.SoapExtensionAttribute)" /> cached by ASP.NET. </param>
		// Token: 0x06000271 RID: 625
		public abstract void Initialize(object initializer);

		/// <summary>When overridden in a derived class, allows a SOAP extension to receive a <see cref="T:System.Web.Services.Protocols.SoapMessage" /> to process at each <see cref="T:System.Web.Services.Protocols.SoapMessageStage" />.</summary>
		/// <param name="message">The <see cref="T:System.Web.Services.Protocols.SoapMessage" /> to process. </param>
		// Token: 0x06000272 RID: 626
		public abstract void ProcessMessage(SoapMessage message);

		/// <summary>When overridden in a derived class, allows a SOAP extension access to the memory buffer containing the SOAP request or response.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> representing a new memory buffer that this SOAP extension can modify.</returns>
		/// <param name="stream">A memory buffer containing the SOAP request or response. </param>
		// Token: 0x06000273 RID: 627 RVA: 0x00006C32 File Offset: 0x00004E32
		public virtual Stream ChainStream(Stream stream)
		{
			return stream;
		}
	}
}
