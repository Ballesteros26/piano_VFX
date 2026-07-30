using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Services.Diagnostics;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols
{
	/// <summary>Reads return values from XML that is encoded in the body of incoming responses for Web service clients implemented using HTTP but without SOAP.</summary>
	// Token: 0x02000093 RID: 147
	public class XmlReturnReader : MimeReturnReader
	{
		/// <summary>Initializes an instance.</summary>
		/// <param name="o">An <see cref="T:System.Xml.Serialization.XmlSerializer" /> for the return type of the Web method being invoked.</param>
		// Token: 0x060003DA RID: 986 RVA: 0x0001217A File Offset: 0x0001037A
		public override void Initialize(object o)
		{
			this.xmlSerializer = (XmlSerializer)o;
		}

		/// <summary>Returns an array of initializer objects corresponding to an input array of method definitions.</summary>
		/// <returns>An array of initializer objects corresponding to an input array of method definitions.</returns>
		/// <param name="methodInfos">An array of type <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> that specifies the Web methods for which the initializers are obtained.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060003DB RID: 987 RVA: 0x00012188 File Offset: 0x00010388
		public override object[] GetInitializers(LogicalMethodInfo[] methodInfos)
		{
			return XmlReturn.GetInitializers(methodInfos);
		}

		/// <summary>Returns an initializer for the specified method.</summary>
		/// <returns>An initializer for the specified method.</returns>
		/// <param name="methodInfo">A <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> that specifies the Web method for which the initializer is obtained.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060003DC RID: 988 RVA: 0x00012190 File Offset: 0x00010390
		public override object GetInitializer(LogicalMethodInfo methodInfo)
		{
			return XmlReturn.GetInitializer(methodInfo);
		}

		/// <summary>Gets a return value deserialized from an XML document contained in the HTTP response.</summary>
		/// <returns>A return value deserialized from an XML document contained in the HTTP response.</returns>
		/// <param name="response">An <see cref="T:System.Web.HttpRequest" /> object containing the output message for an operation.</param>
		/// <param name="responseStream">A <see cref="T:System.IO.Stream" /> whose content is the body of the HTTP response represented by the <paramref name="response" /> parameter.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060003DD RID: 989 RVA: 0x00012198 File Offset: 0x00010398
		public override object Read(WebResponse response, Stream responseStream)
		{
			object obj2;
			try
			{
				if (response == null)
				{
					throw new ArgumentNullException("response");
				}
				if (!ContentType.MatchesBase(response.ContentType, "text/xml"))
				{
					throw new InvalidOperationException(Res.GetString("WebResultNotXml"));
				}
				Encoding encoding = RequestResponseUtils.GetEncoding(response.ContentType);
				StreamReader streamReader = new StreamReader(responseStream, encoding, true);
				TraceMethod traceMethod = (Tracing.On ? new TraceMethod(this, "Read", Array.Empty<object>()) : null);
				if (Tracing.On)
				{
					Tracing.Enter(Tracing.TraceId("TraceReadResponse"), traceMethod, new TraceMethod(this.xmlSerializer, "Deserialize", new object[] { streamReader }));
				}
				object obj = this.xmlSerializer.Deserialize(streamReader);
				if (Tracing.On)
				{
					Tracing.Exit(Tracing.TraceId("TraceReadResponse"), traceMethod);
				}
				obj2 = obj;
			}
			finally
			{
				response.Close();
			}
			return obj2;
		}

		// Token: 0x04000310 RID: 784
		private XmlSerializer xmlSerializer;
	}
}
