using System;
using System.Collections;
using System.Security.Permissions;
using System.Security.Policy;
using System.Web.Services.Diagnostics;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols
{
	// Token: 0x02000092 RID: 146
	internal class XmlReturn
	{
		// Token: 0x060003D5 RID: 981 RVA: 0x0000210F File Offset: 0x0000030F
		private XmlReturn()
		{
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00011F80 File Offset: 0x00010180
		internal static object[] GetInitializers(LogicalMethodInfo[] methodInfos)
		{
			if (methodInfos.Length == 0)
			{
				return new object[0];
			}
			WebServiceAttribute attribute = WebServiceReflector.GetAttribute(methodInfos);
			bool flag = SoapReflector.ServiceDefaultIsEncoded(WebServiceReflector.GetMostDerivedType(methodInfos));
			XmlReflectionImporter xmlReflectionImporter = SoapReflector.CreateXmlImporter(attribute.Namespace, flag);
			WebMethodReflector.IncludeTypes(methodInfos, xmlReflectionImporter);
			ArrayList arrayList = new ArrayList();
			bool[] array = new bool[methodInfos.Length];
			for (int i = 0; i < methodInfos.Length; i++)
			{
				LogicalMethodInfo logicalMethodInfo = methodInfos[i];
				Type returnType = logicalMethodInfo.ReturnType;
				if (XmlReturn.IsSupported(returnType) && HttpServerProtocol.AreUrlParametersSupported(logicalMethodInfo))
				{
					XmlAttributes xmlAttributes = new XmlAttributes(logicalMethodInfo.ReturnTypeCustomAttributeProvider);
					XmlTypeMapping xmlTypeMapping = xmlReflectionImporter.ImportTypeMapping(returnType, xmlAttributes.XmlRoot);
					xmlTypeMapping.SetKey(logicalMethodInfo.GetKey() + ":Return");
					arrayList.Add(xmlTypeMapping);
					array[i] = true;
				}
			}
			if (arrayList.Count == 0)
			{
				return new object[0];
			}
			XmlMapping[] array2 = (XmlMapping[])arrayList.ToArray(typeof(XmlMapping));
			Evidence evidenceForType = XmlReturn.GetEvidenceForType(methodInfos[0].DeclaringType);
			TraceMethod traceMethod = (Tracing.On ? new TraceMethod(typeof(XmlReturn), "GetInitializers", methodInfos) : null);
			if (Tracing.On)
			{
				Tracing.Enter(Tracing.TraceId("TraceCreateSerializer"), traceMethod, new TraceMethod(typeof(XmlSerializer), "FromMappings", new object[] { array2, evidenceForType }));
			}
			XmlSerializer[] array3;
			if (AppDomain.CurrentDomain.IsHomogenous)
			{
				array3 = XmlSerializer.FromMappings(array2);
			}
			else
			{
				array3 = XmlSerializer.FromMappings(array2, evidenceForType);
			}
			if (Tracing.On)
			{
				Tracing.Exit(Tracing.TraceId("TraceCreateSerializer"), traceMethod);
			}
			object[] array4 = new object[methodInfos.Length];
			int num = 0;
			for (int j = 0; j < array4.Length; j++)
			{
				if (array[j])
				{
					array4[j] = array3[num++];
				}
			}
			return array4;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0001214A File Offset: 0x0001034A
		private static bool IsSupported(Type returnType)
		{
			return returnType != typeof(void);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0001215C File Offset: 0x0001035C
		internal static object GetInitializer(LogicalMethodInfo methodInfo)
		{
			return XmlReturn.GetInitializers(new LogicalMethodInfo[] { methodInfo });
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0001216D File Offset: 0x0001036D
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private static Evidence GetEvidenceForType(Type type)
		{
			return type.Assembly.Evidence;
		}
	}
}
