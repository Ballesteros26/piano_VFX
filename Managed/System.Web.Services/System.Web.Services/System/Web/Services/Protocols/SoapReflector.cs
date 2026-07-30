using System;
using System.Collections;
using System.Reflection;
using System.Threading;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols
{
	// Token: 0x02000076 RID: 118
	internal static class SoapReflector
	{
		// Token: 0x06000302 RID: 770 RVA: 0x0000D319 File Offset: 0x0000B519
		internal static bool ServiceDefaultIsEncoded(Type type)
		{
			return SoapReflector.ServiceDefaultIsEncoded(SoapReflector.GetSoapServiceAttribute(type));
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000D326 File Offset: 0x0000B526
		internal static bool ServiceDefaultIsEncoded(object soapServiceAttribute)
		{
			if (soapServiceAttribute == null)
			{
				return false;
			}
			if (soapServiceAttribute is SoapDocumentServiceAttribute)
			{
				return ((SoapDocumentServiceAttribute)soapServiceAttribute).Use == SoapBindingUse.Encoded;
			}
			return soapServiceAttribute is SoapRpcServiceAttribute && ((SoapRpcServiceAttribute)soapServiceAttribute).Use == SoapBindingUse.Encoded;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000D35C File Offset: 0x0000B55C
		internal static string GetEncodedNamespace(string ns, bool serviceDefaultIsEncoded)
		{
			if (serviceDefaultIsEncoded)
			{
				return ns;
			}
			if (ns.EndsWith("/", StringComparison.Ordinal))
			{
				return ns + "encodedTypes";
			}
			return ns + "/encodedTypes";
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000D388 File Offset: 0x0000B588
		internal static string GetLiteralNamespace(string ns, bool serviceDefaultIsEncoded)
		{
			if (!serviceDefaultIsEncoded)
			{
				return ns;
			}
			if (ns.EndsWith("/", StringComparison.Ordinal))
			{
				return ns + "literalTypes";
			}
			return ns + "/literalTypes";
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000D3B4 File Offset: 0x0000B5B4
		internal static SoapReflectionImporter CreateSoapImporter(string defaultNs, bool serviceDefaultIsEncoded)
		{
			return new SoapReflectionImporter(SoapReflector.GetEncodedNamespace(defaultNs, serviceDefaultIsEncoded));
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000D3C2 File Offset: 0x0000B5C2
		internal static XmlReflectionImporter CreateXmlImporter(string defaultNs, bool serviceDefaultIsEncoded)
		{
			return new XmlReflectionImporter(SoapReflector.GetLiteralNamespace(defaultNs, serviceDefaultIsEncoded));
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000D3D0 File Offset: 0x0000B5D0
		internal static void IncludeTypes(LogicalMethodInfo[] methods, SoapReflectionImporter importer)
		{
			for (int i = 0; i < methods.Length; i++)
			{
				SoapReflector.IncludeTypes(methods[i], importer);
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000D3F4 File Offset: 0x0000B5F4
		internal static void IncludeTypes(LogicalMethodInfo method, SoapReflectionImporter importer)
		{
			if (method.Declaration != null)
			{
				importer.IncludeTypes(method.Declaration.DeclaringType);
				importer.IncludeTypes(method.Declaration);
			}
			importer.IncludeTypes(method.DeclaringType);
			importer.IncludeTypes(method.CustomAttributeProvider);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000D444 File Offset: 0x0000B644
		internal static object GetSoapMethodAttribute(LogicalMethodInfo methodInfo)
		{
			object[] customAttributes = methodInfo.GetCustomAttributes(typeof(SoapRpcMethodAttribute));
			object[] customAttributes2 = methodInfo.GetCustomAttributes(typeof(SoapDocumentMethodAttribute));
			if (customAttributes.Length != 0)
			{
				if (customAttributes2.Length != 0)
				{
					throw new ArgumentException(Res.GetString("WebBothMethodAttrs"), "methodInfo");
				}
				return customAttributes[0];
			}
			else
			{
				if (customAttributes2.Length != 0)
				{
					return customAttributes2[0];
				}
				return null;
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000D4A0 File Offset: 0x0000B6A0
		internal static object GetSoapServiceAttribute(Type type)
		{
			object[] customAttributes = type.GetCustomAttributes(typeof(SoapRpcServiceAttribute), false);
			object[] customAttributes2 = type.GetCustomAttributes(typeof(SoapDocumentServiceAttribute), false);
			if (customAttributes.Length != 0)
			{
				if (customAttributes2.Length != 0)
				{
					throw new ArgumentException(Res.GetString("WebBothServiceAttrs"), "methodInfo");
				}
				return customAttributes[0];
			}
			else
			{
				if (customAttributes2.Length != 0)
				{
					return customAttributes2[0];
				}
				return null;
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000D4FB File Offset: 0x0000B6FB
		internal static SoapServiceRoutingStyle GetSoapServiceRoutingStyle(object soapServiceAttribute)
		{
			if (soapServiceAttribute is SoapRpcServiceAttribute)
			{
				return ((SoapRpcServiceAttribute)soapServiceAttribute).RoutingStyle;
			}
			if (soapServiceAttribute is SoapDocumentServiceAttribute)
			{
				return ((SoapDocumentServiceAttribute)soapServiceAttribute).RoutingStyle;
			}
			return SoapServiceRoutingStyle.SoapAction;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000D528 File Offset: 0x0000B728
		internal static string GetSoapMethodBinding(LogicalMethodInfo method)
		{
			object[] array = method.GetCustomAttributes(typeof(SoapDocumentMethodAttribute));
			string text;
			if (array.Length == 0)
			{
				array = method.GetCustomAttributes(typeof(SoapRpcMethodAttribute));
				if (array.Length == 0)
				{
					text = string.Empty;
				}
				else
				{
					text = ((SoapRpcMethodAttribute)array[0]).Binding;
				}
			}
			else
			{
				text = ((SoapDocumentMethodAttribute)array[0]).Binding;
			}
			if (method.Binding == null)
			{
				return text;
			}
			if (text.Length > 0 && text != method.Binding.Name)
			{
				throw new InvalidOperationException(Res.GetString("WebInvalidBindingName", new object[]
				{
					text,
					method.Binding.Name
				}));
			}
			return method.Binding.Name;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000D5E0 File Offset: 0x0000B7E0
		internal static SoapReflectedMethod ReflectMethod(LogicalMethodInfo methodInfo, bool client, XmlReflectionImporter xmlImporter, SoapReflectionImporter soapImporter, string defaultNs)
		{
			SoapReflectedMethod soapReflectedMethod2;
			try
			{
				string key = methodInfo.GetKey();
				SoapReflectedMethod soapReflectedMethod = new SoapReflectedMethod();
				SoapReflector.MethodAttribute methodAttribute = new SoapReflector.MethodAttribute();
				object soapServiceAttribute = SoapReflector.GetSoapServiceAttribute(methodInfo.DeclaringType);
				bool flag = SoapReflector.ServiceDefaultIsEncoded(soapServiceAttribute);
				object obj = SoapReflector.GetSoapMethodAttribute(methodInfo);
				if (obj == null)
				{
					if (client)
					{
						return null;
					}
					if (soapServiceAttribute is SoapRpcServiceAttribute)
					{
						obj = new SoapRpcMethodAttribute
						{
							Use = ((SoapRpcServiceAttribute)soapServiceAttribute).Use
						};
					}
					else if (soapServiceAttribute is SoapDocumentServiceAttribute)
					{
						obj = new SoapDocumentMethodAttribute
						{
							Use = ((SoapDocumentServiceAttribute)soapServiceAttribute).Use
						};
					}
					else
					{
						obj = new SoapDocumentMethodAttribute();
					}
				}
				if (obj is SoapRpcMethodAttribute)
				{
					SoapRpcMethodAttribute soapRpcMethodAttribute = (SoapRpcMethodAttribute)obj;
					soapReflectedMethod.rpc = true;
					soapReflectedMethod.use = soapRpcMethodAttribute.Use;
					soapReflectedMethod.oneWay = soapRpcMethodAttribute.OneWay;
					methodAttribute.action = soapRpcMethodAttribute.Action;
					methodAttribute.binding = soapRpcMethodAttribute.Binding;
					methodAttribute.requestName = soapRpcMethodAttribute.RequestElementName;
					methodAttribute.requestNs = soapRpcMethodAttribute.RequestNamespace;
					methodAttribute.responseName = soapRpcMethodAttribute.ResponseElementName;
					methodAttribute.responseNs = soapRpcMethodAttribute.ResponseNamespace;
				}
				else
				{
					SoapDocumentMethodAttribute soapDocumentMethodAttribute = (SoapDocumentMethodAttribute)obj;
					soapReflectedMethod.rpc = false;
					soapReflectedMethod.use = soapDocumentMethodAttribute.Use;
					soapReflectedMethod.paramStyle = soapDocumentMethodAttribute.ParameterStyle;
					soapReflectedMethod.oneWay = soapDocumentMethodAttribute.OneWay;
					methodAttribute.action = soapDocumentMethodAttribute.Action;
					methodAttribute.binding = soapDocumentMethodAttribute.Binding;
					methodAttribute.requestName = soapDocumentMethodAttribute.RequestElementName;
					methodAttribute.requestNs = soapDocumentMethodAttribute.RequestNamespace;
					methodAttribute.responseName = soapDocumentMethodAttribute.ResponseElementName;
					methodAttribute.responseNs = soapDocumentMethodAttribute.ResponseNamespace;
					if (soapReflectedMethod.use == SoapBindingUse.Default)
					{
						if (soapServiceAttribute is SoapDocumentServiceAttribute)
						{
							soapReflectedMethod.use = ((SoapDocumentServiceAttribute)soapServiceAttribute).Use;
						}
						if (soapReflectedMethod.use == SoapBindingUse.Default)
						{
							soapReflectedMethod.use = SoapBindingUse.Literal;
						}
					}
					if (soapReflectedMethod.paramStyle == SoapParameterStyle.Default)
					{
						if (soapServiceAttribute is SoapDocumentServiceAttribute)
						{
							soapReflectedMethod.paramStyle = ((SoapDocumentServiceAttribute)soapServiceAttribute).ParameterStyle;
						}
						if (soapReflectedMethod.paramStyle == SoapParameterStyle.Default)
						{
							soapReflectedMethod.paramStyle = SoapParameterStyle.Wrapped;
						}
					}
				}
				if (methodAttribute.binding.Length > 0)
				{
					if (client)
					{
						throw new InvalidOperationException(Res.GetString("WebInvalidBindingPlacement", new object[] { obj.GetType().Name }));
					}
					soapReflectedMethod.binding = WebServiceBindingReflector.GetAttribute(methodInfo, methodAttribute.binding);
				}
				WebMethodAttribute methodAttribute2 = methodInfo.MethodAttribute;
				soapReflectedMethod.name = methodAttribute2.MessageName;
				if (soapReflectedMethod.name.Length == 0)
				{
					soapReflectedMethod.name = methodInfo.Name;
				}
				string text;
				if (soapReflectedMethod.rpc)
				{
					text = ((methodAttribute.requestName.Length == 0 || !client) ? methodInfo.Name : methodAttribute.requestName);
				}
				else
				{
					text = ((methodAttribute.requestName.Length == 0) ? soapReflectedMethod.name : methodAttribute.requestName);
				}
				string text2 = methodAttribute.requestNs;
				if (text2 == null)
				{
					if (soapReflectedMethod.binding != null && soapReflectedMethod.binding.Namespace != null && soapReflectedMethod.binding.Namespace.Length != 0)
					{
						text2 = soapReflectedMethod.binding.Namespace;
					}
					else
					{
						text2 = defaultNs;
					}
				}
				string text3;
				if (soapReflectedMethod.rpc && soapReflectedMethod.use != SoapBindingUse.Encoded)
				{
					text3 = methodInfo.Name + "Response";
				}
				else
				{
					text3 = ((methodAttribute.responseName.Length == 0) ? (soapReflectedMethod.name + "Response") : methodAttribute.responseName);
				}
				string text4 = methodAttribute.responseNs;
				if (text4 == null)
				{
					if (soapReflectedMethod.binding != null && soapReflectedMethod.binding.Namespace != null && soapReflectedMethod.binding.Namespace.Length != 0)
					{
						text4 = soapReflectedMethod.binding.Namespace;
					}
					else
					{
						text4 = defaultNs;
					}
				}
				SoapReflector.SoapParameterInfo[] array = SoapReflector.ReflectParameters(methodInfo.InParameters, text2);
				SoapReflector.SoapParameterInfo[] array2 = SoapReflector.ReflectParameters(methodInfo.OutParameters, text4);
				soapReflectedMethod.action = methodAttribute.action;
				if (soapReflectedMethod.action == null)
				{
					soapReflectedMethod.action = SoapReflector.GetDefaultAction(defaultNs, methodInfo);
				}
				soapReflectedMethod.methodInfo = methodInfo;
				if (soapReflectedMethod.oneWay)
				{
					if (array2.Length != 0)
					{
						throw new ArgumentException(Res.GetString("WebOneWayOutParameters"), "methodInfo");
					}
					if (methodInfo.ReturnType != typeof(void))
					{
						throw new ArgumentException(Res.GetString("WebOneWayReturnValue"), "methodInfo");
					}
				}
				XmlReflectionMember[] array3 = new XmlReflectionMember[array.Length];
				for (int i = 0; i < array3.Length; i++)
				{
					SoapReflector.SoapParameterInfo soapParameterInfo = array[i];
					XmlReflectionMember xmlReflectionMember = new XmlReflectionMember();
					xmlReflectionMember.MemberName = soapParameterInfo.parameterInfo.Name;
					xmlReflectionMember.MemberType = soapParameterInfo.parameterInfo.ParameterType;
					if (xmlReflectionMember.MemberType.IsByRef)
					{
						xmlReflectionMember.MemberType = xmlReflectionMember.MemberType.GetElementType();
					}
					xmlReflectionMember.XmlAttributes = soapParameterInfo.xmlAttributes;
					xmlReflectionMember.SoapAttributes = soapParameterInfo.soapAttributes;
					array3[i] = xmlReflectionMember;
				}
				soapReflectedMethod.requestMappings = SoapReflector.ImportMembersMapping(xmlImporter, soapImporter, flag, soapReflectedMethod.rpc, soapReflectedMethod.use, soapReflectedMethod.paramStyle, text, text2, methodAttribute.requestNs == null, array3, true, false, key, client);
				if (SoapReflector.GetSoapServiceRoutingStyle(soapServiceAttribute) == SoapServiceRoutingStyle.RequestElement && soapReflectedMethod.paramStyle == SoapParameterStyle.Bare && soapReflectedMethod.requestMappings.Count != 1)
				{
					throw new ArgumentException(Res.GetString("WhenUsingAMessageStyleOfParametersAsDocument0"), "methodInfo");
				}
				string text5 = "";
				string text6 = "";
				if (soapReflectedMethod.paramStyle == SoapParameterStyle.Bare)
				{
					if (soapReflectedMethod.requestMappings.Count == 1)
					{
						text5 = soapReflectedMethod.requestMappings[0].XsdElementName;
						text6 = soapReflectedMethod.requestMappings[0].Namespace;
					}
				}
				else
				{
					text5 = soapReflectedMethod.requestMappings.XsdElementName;
					text6 = soapReflectedMethod.requestMappings.Namespace;
				}
				soapReflectedMethod.requestElementName = new XmlQualifiedName(text5, text6);
				if (!soapReflectedMethod.oneWay)
				{
					int num = array2.Length;
					int num2 = 0;
					CodeIdentifiers codeIdentifiers = null;
					if (methodInfo.ReturnType != typeof(void))
					{
						num++;
						num2 = 1;
						codeIdentifiers = new CodeIdentifiers();
					}
					array3 = new XmlReflectionMember[num];
					foreach (SoapReflector.SoapParameterInfo soapParameterInfo2 in array2)
					{
						XmlReflectionMember xmlReflectionMember2 = new XmlReflectionMember();
						xmlReflectionMember2.MemberName = soapParameterInfo2.parameterInfo.Name;
						xmlReflectionMember2.MemberType = soapParameterInfo2.parameterInfo.ParameterType;
						if (xmlReflectionMember2.MemberType.IsByRef)
						{
							xmlReflectionMember2.MemberType = xmlReflectionMember2.MemberType.GetElementType();
						}
						xmlReflectionMember2.XmlAttributes = soapParameterInfo2.xmlAttributes;
						xmlReflectionMember2.SoapAttributes = soapParameterInfo2.soapAttributes;
						array3[num2++] = xmlReflectionMember2;
						if (codeIdentifiers != null)
						{
							codeIdentifiers.Add(xmlReflectionMember2.MemberName, null);
						}
					}
					if (methodInfo.ReturnType != typeof(void))
					{
						array3[0] = new XmlReflectionMember
						{
							MemberName = codeIdentifiers.MakeUnique(soapReflectedMethod.name + "Result"),
							MemberType = methodInfo.ReturnType,
							IsReturnValue = true,
							XmlAttributes = new XmlAttributes(methodInfo.ReturnTypeCustomAttributeProvider),
							XmlAttributes = 
							{
								XmlRoot = null
							},
							SoapAttributes = new SoapAttributes(methodInfo.ReturnTypeCustomAttributeProvider)
						};
					}
					soapReflectedMethod.responseMappings = SoapReflector.ImportMembersMapping(xmlImporter, soapImporter, flag, soapReflectedMethod.rpc, soapReflectedMethod.use, soapReflectedMethod.paramStyle, text3, text4, methodAttribute.responseNs == null, array3, false, false, key + ":Response", !client);
				}
				SoapExtensionAttribute[] array4 = (SoapExtensionAttribute[])methodInfo.GetCustomAttributes(typeof(SoapExtensionAttribute));
				soapReflectedMethod.extensions = new SoapReflectedExtension[array4.Length];
				for (int k = 0; k < array4.Length; k++)
				{
					soapReflectedMethod.extensions[k] = new SoapReflectedExtension(array4[k].ExtensionType, array4[k]);
				}
				Array.Sort<SoapReflectedExtension>(soapReflectedMethod.extensions);
				SoapHeaderAttribute[] array5 = (SoapHeaderAttribute[])methodInfo.GetCustomAttributes(typeof(SoapHeaderAttribute));
				Array.Sort(array5, new SoapHeaderAttributeComparer());
				Hashtable hashtable = new Hashtable();
				soapReflectedMethod.headers = new SoapReflectedHeader[array5.Length];
				int num3 = 0;
				int num4 = soapReflectedMethod.headers.Length;
				ArrayList arrayList = new ArrayList();
				ArrayList arrayList2 = new ArrayList();
				for (int l = 0; l < soapReflectedMethod.headers.Length; l++)
				{
					SoapHeaderAttribute soapHeaderAttribute = array5[l];
					SoapReflectedHeader soapReflectedHeader = new SoapReflectedHeader();
					Type declaringType = methodInfo.DeclaringType;
					if ((soapReflectedHeader.memberInfo = declaringType.GetField(soapHeaderAttribute.MemberName)) != null)
					{
						soapReflectedHeader.headerType = ((FieldInfo)soapReflectedHeader.memberInfo).FieldType;
					}
					else
					{
						if (!((soapReflectedHeader.memberInfo = declaringType.GetProperty(soapHeaderAttribute.MemberName)) != null))
						{
							throw SoapReflector.HeaderException(soapHeaderAttribute.MemberName, methodInfo.DeclaringType, "WebHeaderMissing");
						}
						soapReflectedHeader.headerType = ((PropertyInfo)soapReflectedHeader.memberInfo).PropertyType;
					}
					if (soapReflectedHeader.headerType.IsArray)
					{
						soapReflectedHeader.headerType = soapReflectedHeader.headerType.GetElementType();
						soapReflectedHeader.repeats = true;
						if (soapReflectedHeader.headerType != typeof(SoapUnknownHeader) && soapReflectedHeader.headerType != typeof(SoapHeader))
						{
							throw SoapReflector.HeaderException(soapHeaderAttribute.MemberName, methodInfo.DeclaringType, "WebHeaderType");
						}
					}
					if (MemberHelper.IsStatic(soapReflectedHeader.memberInfo))
					{
						throw SoapReflector.HeaderException(soapHeaderAttribute.MemberName, methodInfo.DeclaringType, "WebHeaderStatic");
					}
					if (!MemberHelper.CanRead(soapReflectedHeader.memberInfo))
					{
						throw SoapReflector.HeaderException(soapHeaderAttribute.MemberName, methodInfo.DeclaringType, "WebHeaderRead");
					}
					if (!MemberHelper.CanWrite(soapReflectedHeader.memberInfo))
					{
						throw SoapReflector.HeaderException(soapHeaderAttribute.MemberName, methodInfo.DeclaringType, "WebHeaderWrite");
					}
					if (!typeof(SoapHeader).IsAssignableFrom(soapReflectedHeader.headerType))
					{
						throw SoapReflector.HeaderException(soapHeaderAttribute.MemberName, methodInfo.DeclaringType, "WebHeaderType");
					}
					SoapHeaderDirection direction = soapHeaderAttribute.Direction;
					if (soapReflectedMethod.oneWay && (direction & (SoapHeaderDirection.Out | SoapHeaderDirection.Fault)) != (SoapHeaderDirection)0)
					{
						throw SoapReflector.HeaderException(soapHeaderAttribute.MemberName, methodInfo.DeclaringType, "WebHeaderOneWayOut");
					}
					if (hashtable.Contains(soapReflectedHeader.headerType))
					{
						SoapHeaderDirection soapHeaderDirection = (SoapHeaderDirection)hashtable[soapReflectedHeader.headerType];
						if ((soapHeaderDirection & direction) != (SoapHeaderDirection)0)
						{
							throw SoapReflector.HeaderException(soapHeaderAttribute.MemberName, methodInfo.DeclaringType, "WebMultiplyDeclaredHeaderTypes");
						}
						hashtable[soapReflectedHeader.headerType] = direction | soapHeaderDirection;
					}
					else
					{
						hashtable[soapReflectedHeader.headerType] = direction;
					}
					if (soapReflectedHeader.headerType != typeof(SoapHeader) && soapReflectedHeader.headerType != typeof(SoapUnknownHeader))
					{
						XmlReflectionMember xmlReflectionMember3 = new XmlReflectionMember();
						xmlReflectionMember3.MemberName = soapReflectedHeader.headerType.Name;
						xmlReflectionMember3.MemberType = soapReflectedHeader.headerType;
						XmlAttributes xmlAttributes = new XmlAttributes(soapReflectedHeader.headerType);
						if (xmlAttributes.XmlRoot != null)
						{
							xmlReflectionMember3.XmlAttributes = new XmlAttributes();
							XmlElementAttribute xmlElementAttribute = new XmlElementAttribute();
							xmlElementAttribute.ElementName = xmlAttributes.XmlRoot.ElementName;
							xmlElementAttribute.Namespace = xmlAttributes.XmlRoot.Namespace;
							xmlReflectionMember3.XmlAttributes.XmlElements.Add(xmlElementAttribute);
						}
						xmlReflectionMember3.OverrideIsNullable = true;
						if ((direction & SoapHeaderDirection.In) != (SoapHeaderDirection)0)
						{
							arrayList.Add(xmlReflectionMember3);
						}
						if ((direction & (SoapHeaderDirection.Out | SoapHeaderDirection.Fault)) != (SoapHeaderDirection)0)
						{
							arrayList2.Add(xmlReflectionMember3);
						}
						soapReflectedHeader.custom = true;
					}
					soapReflectedHeader.direction = direction;
					if (!soapReflectedHeader.custom)
					{
						soapReflectedMethod.headers[--num4] = soapReflectedHeader;
					}
					else
					{
						soapReflectedMethod.headers[num3++] = soapReflectedHeader;
					}
				}
				soapReflectedMethod.inHeaderMappings = SoapReflector.ImportMembersMapping(xmlImporter, soapImporter, flag, false, soapReflectedMethod.use, SoapParameterStyle.Bare, text + "InHeaders", defaultNs, true, (XmlReflectionMember[])arrayList.ToArray(typeof(XmlReflectionMember)), false, true, key + ":InHeaders", client);
				if (!soapReflectedMethod.oneWay)
				{
					soapReflectedMethod.outHeaderMappings = SoapReflector.ImportMembersMapping(xmlImporter, soapImporter, flag, false, soapReflectedMethod.use, SoapParameterStyle.Bare, text3 + "OutHeaders", defaultNs, true, (XmlReflectionMember[])arrayList2.ToArray(typeof(XmlReflectionMember)), false, true, key + ":OutHeaders", !client);
				}
				soapReflectedMethod2 = soapReflectedMethod;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new InvalidOperationException(Res.GetString("WebReflectionErrorMethod", new object[]
				{
					methodInfo.DeclaringType.Name,
					methodInfo.Name
				}), ex);
			}
			return soapReflectedMethod2;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000E2A0 File Offset: 0x0000C4A0
		private static XmlMembersMapping ImportMembersMapping(XmlReflectionImporter xmlImporter, SoapReflectionImporter soapImporter, bool serviceDefaultIsEncoded, bool rpc, SoapBindingUse use, SoapParameterStyle paramStyle, string elementName, string elementNamespace, bool nsIsDefault, XmlReflectionMember[] members, bool validate, bool openModel, string key, bool writeAccess)
		{
			XmlMembersMapping xmlMembersMapping;
			if (use == SoapBindingUse.Encoded)
			{
				string text = ((!rpc && paramStyle != SoapParameterStyle.Bare && nsIsDefault) ? SoapReflector.GetEncodedNamespace(elementNamespace, serviceDefaultIsEncoded) : elementNamespace);
				xmlMembersMapping = soapImporter.ImportMembersMapping(elementName, text, members, rpc || paramStyle != SoapParameterStyle.Bare, rpc, validate, writeAccess ? XmlMappingAccess.Write : XmlMappingAccess.Read);
			}
			else
			{
				string text2 = (nsIsDefault ? SoapReflector.GetLiteralNamespace(elementNamespace, serviceDefaultIsEncoded) : elementNamespace);
				xmlMembersMapping = xmlImporter.ImportMembersMapping(elementName, text2, members, paramStyle != SoapParameterStyle.Bare, rpc, openModel, writeAccess ? XmlMappingAccess.Write : XmlMappingAccess.Read);
			}
			if (xmlMembersMapping != null)
			{
				xmlMembersMapping.SetKey(key);
			}
			return xmlMembersMapping;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000E337 File Offset: 0x0000C537
		private static Exception HeaderException(string memberName, Type declaringType, string description)
		{
			return new Exception(Res.GetString(description, new object[] { declaringType.Name, memberName }));
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000E358 File Offset: 0x0000C558
		private static SoapReflector.SoapParameterInfo[] ReflectParameters(ParameterInfo[] paramInfos, string ns)
		{
			SoapReflector.SoapParameterInfo[] array = new SoapReflector.SoapParameterInfo[paramInfos.Length];
			for (int i = 0; i < paramInfos.Length; i++)
			{
				SoapReflector.SoapParameterInfo soapParameterInfo = new SoapReflector.SoapParameterInfo();
				ParameterInfo parameterInfo = paramInfos[i];
				if (parameterInfo.ParameterType.IsArray && parameterInfo.ParameterType.GetArrayRank() > 1)
				{
					throw new InvalidOperationException(Res.GetString("WebMultiDimArray"));
				}
				soapParameterInfo.xmlAttributes = new XmlAttributes(parameterInfo);
				soapParameterInfo.soapAttributes = new SoapAttributes(parameterInfo);
				soapParameterInfo.parameterInfo = parameterInfo;
				array[i] = soapParameterInfo;
			}
			return array;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000E3D8 File Offset: 0x0000C5D8
		private static string GetDefaultAction(string defaultNs, LogicalMethodInfo methodInfo)
		{
			string text = methodInfo.MethodAttribute.MessageName;
			if (text.Length == 0)
			{
				text = methodInfo.Name;
			}
			if (defaultNs.EndsWith("/", StringComparison.Ordinal))
			{
				return defaultNs + text;
			}
			return defaultNs + "/" + text;
		}

		// Token: 0x02000077 RID: 119
		private class SoapParameterInfo
		{
			// Token: 0x040002C2 RID: 706
			internal ParameterInfo parameterInfo;

			// Token: 0x040002C3 RID: 707
			internal XmlAttributes xmlAttributes;

			// Token: 0x040002C4 RID: 708
			internal SoapAttributes soapAttributes;
		}

		// Token: 0x02000078 RID: 120
		private class MethodAttribute
		{
			// Token: 0x040002C5 RID: 709
			internal string action;

			// Token: 0x040002C6 RID: 710
			internal string binding;

			// Token: 0x040002C7 RID: 711
			internal string requestName;

			// Token: 0x040002C8 RID: 712
			internal string requestNs;

			// Token: 0x040002C9 RID: 713
			internal string responseName;

			// Token: 0x040002CA RID: 714
			internal string responseNs;
		}
	}
}
