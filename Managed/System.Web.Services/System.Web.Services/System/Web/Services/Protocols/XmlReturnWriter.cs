using System;
using System.IO;
using System.Text;
using System.Web.Services.Diagnostics;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols
{
	// Token: 0x02000094 RID: 148
	internal class XmlReturnWriter : MimeReturnWriter
	{
		// Token: 0x060003DF RID: 991 RVA: 0x00012274 File Offset: 0x00010474
		public override void Initialize(object o)
		{
			this.xmlSerializer = (XmlSerializer)o;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00012188 File Offset: 0x00010388
		public override object[] GetInitializers(LogicalMethodInfo[] methodInfos)
		{
			return XmlReturn.GetInitializers(methodInfos);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00012190 File Offset: 0x00010390
		public override object GetInitializer(LogicalMethodInfo methodInfo)
		{
			return XmlReturn.GetInitializer(methodInfo);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00012284 File Offset: 0x00010484
		internal override void Write(HttpResponse response, Stream outputStream, object returnValue)
		{
			Encoding encoding = new UTF8Encoding(false);
			response.ContentType = ContentType.Compose("text/xml", encoding);
			StreamWriter streamWriter = new StreamWriter(outputStream, encoding);
			TraceMethod traceMethod = (Tracing.On ? new TraceMethod(this, "Write", Array.Empty<object>()) : null);
			if (Tracing.On)
			{
				Tracing.Enter(Tracing.TraceId("TraceWriteResponse"), traceMethod, new TraceMethod(this.xmlSerializer, "Serialize", new object[] { streamWriter, returnValue }));
			}
			this.xmlSerializer.Serialize(streamWriter, returnValue);
			if (Tracing.On)
			{
				Tracing.Exit(Tracing.TraceId("TraceWriteResponse"), traceMethod);
			}
		}

		// Token: 0x04000311 RID: 785
		private XmlSerializer xmlSerializer;
	}
}
