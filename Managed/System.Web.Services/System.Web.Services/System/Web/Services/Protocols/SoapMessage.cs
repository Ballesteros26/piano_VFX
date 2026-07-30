using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Web.Services.Diagnostics;

namespace System.Web.Services.Protocols
{
	/// <summary>Represents the data in a SOAP request or SOAP response at a specific <see cref="T:System.Web.Services.Protocols.SoapMessageStage" />.</summary>
	// Token: 0x0200006E RID: 110
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class SoapMessage
	{
		// Token: 0x060002D7 RID: 727 RVA: 0x0000CDA9 File Offset: 0x0000AFA9
		internal SoapMessage()
		{
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000CDBC File Offset: 0x0000AFBC
		internal void SetParameterValues(object[] parameterValues)
		{
			this.parameterValues = parameterValues;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000CDC5 File Offset: 0x0000AFC5
		internal object[] GetParameterValues()
		{
			return this.parameterValues;
		}

		/// <summary>Gets a value indicating the <see cref="P:System.Web.Services.Protocols.SoapDocumentMethodAttribute.OneWay" /> property of either the <see cref="T:System.Web.Services.Protocols.SoapDocumentMethodAttribute" /> or the <see cref="T:System.Web.Services.Protocols.SoapRpcMethodAttribute" /> attribute applied to the XML Web service method.</summary>
		/// <returns>true if the <see cref="P:System.Web.Services.Protocols.SoapDocumentMethodAttribute.OneWay" /> property of the <see cref="T:System.Web.Services.Protocols.SoapDocumentMethodAttribute" /> or <see cref="T:System.Web.Services.Protocols.SoapRpcMethodAttribute" /> applied to the XML Web service method is true; otherwise, false.</returns>
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002DA RID: 730
		public abstract bool OneWay { get; }

		/// <summary>Gets the parameter passed into the XML Web service method at the specified index.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the parameter at the specified index.</returns>
		/// <param name="index">The zero-based index of the parameter in the array of parameters. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is less than 0 or greater than the length of the parameters array. </exception>
		/// <exception cref="T:System.InvalidOperationException">Accessing this property when in parameters are not available. For details see the Remarks section. </exception>
		// Token: 0x060002DB RID: 731 RVA: 0x0000CDD0 File Offset: 0x0000AFD0
		public object GetInParameterValue(int index)
		{
			this.EnsureInStage();
			this.EnsureNoException();
			if (index < 0 || index >= this.parameterValues.Length)
			{
				throw new IndexOutOfRangeException(Res.GetString("indexMustBeBetweenAnd0Inclusive", new object[] { this.parameterValues.Length }));
			}
			return this.parameterValues[index];
		}

		/// <summary>Gets the out parameter passed into the XML Web service method at the specified index.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the parameter at the specified index.</returns>
		/// <param name="index">The zero-based index of the parameter in the array of parameters. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is greater than the length of the parameters array. </exception>
		/// <exception cref="T:System.InvalidOperationException">Accessing this property when out parameters are not available. For details see the Remarks section. </exception>
		// Token: 0x060002DC RID: 732 RVA: 0x0000CE28 File Offset: 0x0000B028
		public object GetOutParameterValue(int index)
		{
			this.EnsureOutStage();
			this.EnsureNoException();
			if (!this.MethodInfo.IsVoid)
			{
				if (index == 2147483647)
				{
					throw new IndexOutOfRangeException(Res.GetString("indexMustBeBetweenAnd0Inclusive", new object[] { this.parameterValues.Length }));
				}
				index++;
			}
			if (index < 0 || index >= this.parameterValues.Length)
			{
				throw new IndexOutOfRangeException(Res.GetString("indexMustBeBetweenAnd0Inclusive", new object[] { this.parameterValues.Length }));
			}
			return this.parameterValues[index];
		}

		/// <summary>Gets the return value of an XML Web service method.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the return value of the XML Web service method.</returns>
		/// <exception cref="T:System.InvalidOperationException">The XML Web service method does not have a return value.OR The return value is not available. For details see the Remarks section </exception>
		// Token: 0x060002DD RID: 733 RVA: 0x0000CEBE File Offset: 0x0000B0BE
		public object GetReturnValue()
		{
			this.EnsureOutStage();
			this.EnsureNoException();
			if (this.MethodInfo.IsVoid)
			{
				throw new InvalidOperationException(Res.GetString("WebNoReturnValue"));
			}
			return this.parameterValues[0];
		}

		/// <summary>When overridden in a derived class, asserts that the current <see cref="T:System.Web.Services.Protocols.SoapMessageStage" /> stage is a stage where out parameters are available.</summary>
		/// <exception cref="T:System.InvalidOperationException">Out parameters are not available. </exception>
		// Token: 0x060002DE RID: 734
		protected abstract void EnsureOutStage();

		/// <summary>When overridden in a derived class, asserts that the current <see cref="T:System.Web.Services.Protocols.SoapMessageStage" /> is a stage where in parameters are available.</summary>
		/// <exception cref="T:System.InvalidOperationException">In parameters are not available. </exception>
		// Token: 0x060002DF RID: 735
		protected abstract void EnsureInStage();

		// Token: 0x060002E0 RID: 736 RVA: 0x0000CEF1 File Offset: 0x0000B0F1
		private void EnsureNoException()
		{
			if (this.exception != null)
			{
				throw new InvalidOperationException(Res.GetString("WebCannotAccessValue"), this.exception);
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Services.Protocols.SoapException" /> from the call to the XML Web service method.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Protocols.SoapException" /> that occurred in the call to the XML Web service method. null if no <see cref="T:System.Web.Services.Protocols.SoapException" /> has occurred during the call to the Web Sevice method.</returns>
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000CF11 File Offset: 0x0000B111
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x0000CF19 File Offset: 0x0000B119
		public SoapException Exception
		{
			get
			{
				return this.exception;
			}
			set
			{
				this.exception = value;
			}
		}

		/// <summary>When overridden in a derived class, gets a representation of the method prototype for the XML Web service method for which the SOAP request is intended.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> representing the XML Web service method for which the SOAP request is intended.</returns>
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002E3 RID: 739
		public abstract LogicalMethodInfo MethodInfo { get; }

		/// <summary>Ensures that the <see cref="T:System.Web.Services.Protocols.SoapMessageStage" /> of the call to the XML Web service method is the stage or stages passed in. If the current processing stage is not one of the stages passed in, an exception is thrown.</summary>
		/// <param name="stage">The <see cref="T:System.Web.Services.Protocols.SoapMessageStage" /> asserted. </param>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Web.Services.Protocols.SoapMessageStage" /> is not the asserted stage or stages. </exception>
		// Token: 0x060002E4 RID: 740 RVA: 0x0000CF22 File Offset: 0x0000B122
		protected void EnsureStage(SoapMessageStage stage)
		{
			if ((this.stage & stage) == (SoapMessageStage)0)
			{
				throw new InvalidOperationException(Res.GetString("WebCannotAccessValueStage", new object[] { this.stage.ToString() }));
			}
		}

		/// <summary>A collection of the SOAP headers applied to the current SOAP request or SOAP response.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" /> of the SOAP headers applied to the current SOAP request or SOAP response. null, if there are no SOAP headers.</returns>
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000CF58 File Offset: 0x0000B158
		public SoapHeaderCollection Headers
		{
			get
			{
				return this.headers;
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000CF60 File Offset: 0x0000B160
		internal void SetStream(Stream stream)
		{
			if (this.extensionStream != null)
			{
				this.extensionStream.SetInnerStream(stream);
				this.extensionStream.SetStreamReady();
				this.extensionStream = null;
				return;
			}
			this.stream = stream;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000CF90 File Offset: 0x0000B190
		internal void SetExtensionStream(SoapExtensionStream extensionStream)
		{
			this.extensionStream = extensionStream;
			this.stream = extensionStream;
		}

		/// <summary>Gets the data representing the SOAP request or SOAP response in the form of a <see cref="T:System.IO.Stream" />.</summary>
		/// <returns>A read-only instance of the <see cref="T:System.IO.Stream" /> class.</returns>
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000CFA0 File Offset: 0x0000B1A0
		public Stream Stream
		{
			get
			{
				return this.stream;
			}
		}

		/// <summary>Gets or sets the HTTP Content-Type of the SOAP request or SOAP response.</summary>
		/// <returns>The HTTP Content-Type of the SOAP request or SOAP response. The default is "text/xml".</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Web.Services.Protocols.SoapMessage.ContentType" /> is accessed <see cref="F:System.Web.Services.Protocols.SoapMessageStage.AfterSerialize" /> or <see cref="F:System.Web.Services.Protocols.SoapMessageStage.AfterDeserialize" /> stages. </exception>
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000CFA8 File Offset: 0x0000B1A8
		// (set) Token: 0x060002EA RID: 746 RVA: 0x0000CFB7 File Offset: 0x0000B1B7
		public string ContentType
		{
			get
			{
				this.EnsureStage((SoapMessageStage)5);
				return this.contentType;
			}
			set
			{
				this.EnsureStage((SoapMessageStage)5);
				this.contentType = value;
			}
		}

		/// <summary>Gets or sets the contents of the Content-Encoding HTTP header.</summary>
		/// <returns>The contents of the Content-Encoding HTTP header.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Web.Services.Protocols.SoapMessageStage" /> is <see cref="F:System.Web.Services.Protocols.SoapMessageStage.AfterSerialize" /> or <see cref="F:System.Web.Services.Protocols.SoapMessageStage.AfterDeserialize" /> stages. </exception>
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000CFC7 File Offset: 0x0000B1C7
		// (set) Token: 0x060002EC RID: 748 RVA: 0x0000CFD6 File Offset: 0x0000B1D6
		public string ContentEncoding
		{
			get
			{
				this.EnsureStage((SoapMessageStage)5);
				return this.contentEncoding;
			}
			set
			{
				this.EnsureStage((SoapMessageStage)5);
				this.contentEncoding = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Services.Protocols.SoapMessageStage" /> of the <see cref="T:System.Web.Services.Protocols.SoapMessage" />.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Protocols.SoapMessageStage" /> of the <see cref="T:System.Web.Services.Protocols.SoapMessage" />.</returns>
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000CFE6 File Offset: 0x0000B1E6
		public SoapMessageStage Stage
		{
			get
			{
				return this.stage;
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000CFEE File Offset: 0x0000B1EE
		internal void SetStage(SoapMessageStage stage)
		{
			this.stage = stage;
		}

		/// <summary>When overridden in a derived class, gets the base URL of the XML Web service.</summary>
		/// <returns>The base URL of the XML Web service.</returns>
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002EF RID: 751
		public abstract string Url { get; }

		/// <summary>When overridden in a derived class, gets the SOAPAction HTTP request header field for the SOAP request or SOAP response.</summary>
		/// <returns>The SOAPAction HTTP request header field for the SOAP request or SOAP response.</returns>
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002F0 RID: 752
		public abstract string Action { get; }

		/// <summary>Gets the version of the SOAP protocol used to communicate with the XML Web service.</summary>
		/// <returns>One of the <see cref="T:System.Web.Services.Protocols.SoapProtocolVersion" /> values. The default is <see cref="F:System.Web.Services.Protocols.SoapProtocolVersion.Default" />.</returns>
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00002B51 File Offset: 0x00000D51
		[DefaultValue(SoapProtocolVersion.Default)]
		[ComVisible(false)]
		public virtual SoapProtocolVersion SoapVersion
		{
			get
			{
				return SoapProtocolVersion.Default;
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000CFF8 File Offset: 0x0000B1F8
		internal static SoapExtension[] InitializeExtensions(SoapReflectedExtension[] reflectedExtensions, object[] extensionInitializers)
		{
			if (reflectedExtensions == null)
			{
				return null;
			}
			SoapExtension[] array = new SoapExtension[reflectedExtensions.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = reflectedExtensions[i].CreateInstance(extensionInitializers[i]);
			}
			return array;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000D030 File Offset: 0x0000B230
		internal void InitExtensionStreamChain(SoapExtension[] extensions)
		{
			if (extensions == null)
			{
				return;
			}
			for (int i = 0; i < extensions.Length; i++)
			{
				this.stream = extensions[i].ChainStream(this.stream);
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000D064 File Offset: 0x0000B264
		internal void RunExtensions(SoapExtension[] extensions, bool throwOnException)
		{
			if (extensions == null)
			{
				return;
			}
			TraceMethod traceMethod = (Tracing.On ? new TraceMethod(this, "RunExtensions", new object[] { extensions, throwOnException }) : null);
			if ((this.stage & (SoapMessageStage)12) != (SoapMessageStage)0)
			{
				for (int i = 0; i < extensions.Length; i++)
				{
					if (Tracing.On)
					{
						Tracing.Enter("SoapExtension", traceMethod, new TraceMethod(extensions[i], "ProcessMessage", new object[] { this.stage }));
					}
					extensions[i].ProcessMessage(this);
					if (Tracing.On)
					{
						Tracing.Exit("SoapExtension", traceMethod);
					}
					if (this.Exception != null)
					{
						if (throwOnException)
						{
							throw this.Exception;
						}
						if (Tracing.On)
						{
							Tracing.ExceptionIgnore(TraceEventType.Warning, traceMethod, this.Exception);
						}
					}
				}
				return;
			}
			for (int j = extensions.Length - 1; j >= 0; j--)
			{
				if (Tracing.On)
				{
					Tracing.Enter("SoapExtension", traceMethod, new TraceMethod(extensions[j], "ProcessMessage", new object[] { this.stage }));
				}
				extensions[j].ProcessMessage(this);
				if (Tracing.On)
				{
					Tracing.Exit("SoapExtension", traceMethod);
				}
				if (this.Exception != null)
				{
					if (throwOnException)
					{
						throw this.Exception;
					}
					if (Tracing.On)
					{
						Tracing.ExceptionIgnore(TraceEventType.Warning, traceMethod, this.Exception);
					}
				}
			}
		}

		// Token: 0x04000295 RID: 661
		private SoapMessageStage stage;

		// Token: 0x04000296 RID: 662
		private SoapHeaderCollection headers = new SoapHeaderCollection();

		// Token: 0x04000297 RID: 663
		private Stream stream;

		// Token: 0x04000298 RID: 664
		private SoapExtensionStream extensionStream;

		// Token: 0x04000299 RID: 665
		private string contentType;

		// Token: 0x0400029A RID: 666
		private string contentEncoding;

		// Token: 0x0400029B RID: 667
		private object[] parameterValues;

		// Token: 0x0400029C RID: 668
		private SoapException exception;
	}
}
