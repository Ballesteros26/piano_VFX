using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;

namespace System.Runtime.Remoting
{
	/// <summary>Provides several methods for using and publishing remoted objects in SOAP format.</summary>
	// Token: 0x02000763 RID: 1891
	[ComVisible(true)]
	public class SoapServices
	{
		// Token: 0x06004E25 RID: 20005 RVA: 0x00002111 File Offset: 0x00000311
		private SoapServices()
		{
		}

		/// <summary>Gets the XML namespace prefix for common language runtime types.</summary>
		/// <returns>The XML namespace prefix for common language runtime types.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x06004E26 RID: 20006 RVA: 0x0011A5F8 File Offset: 0x001187F8
		public static string XmlNsForClrType
		{
			get
			{
				return "http://schemas.microsoft.com/clr/";
			}
		}

		/// <summary>Gets the default XML namespace prefix that should be used for XML encoding of a common language runtime class that has an assembly, but no native namespace.</summary>
		/// <returns>The default XML namespace prefix that should be used for XML encoding of a common language runtime class that has an assembly, but no native namespace.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x06004E27 RID: 20007 RVA: 0x0011A5FF File Offset: 0x001187FF
		public static string XmlNsForClrTypeWithAssembly
		{
			get
			{
				return "http://schemas.microsoft.com/clr/assem/";
			}
		}

		/// <summary>Gets the XML namespace prefix that should be used for XML encoding of a common language runtime class that is part of the mscorlib.dll file.</summary>
		/// <returns>The XML namespace prefix that should be used for XML encoding of a common language runtime class that is part of the mscorlib.dll file.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x06004E28 RID: 20008 RVA: 0x0011A606 File Offset: 0x00118806
		public static string XmlNsForClrTypeWithNs
		{
			get
			{
				return "http://schemas.microsoft.com/clr/ns/";
			}
		}

		/// <summary>Gets the default XML namespace prefix that should be used for XML encoding of a common language runtime class that has both a common language runtime namespace and an assembly.</summary>
		/// <returns>The default XML namespace prefix that should be used for XML encoding of a common language runtime class that has both a common language runtime namespace and an assembly.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06004E29 RID: 20009 RVA: 0x0011A60D File Offset: 0x0011880D
		public static string XmlNsForClrTypeWithNsAndAssembly
		{
			get
			{
				return "http://schemas.microsoft.com/clr/nsassem/";
			}
		}

		/// <summary>Returns the common language runtime type namespace name from the provided namespace and assembly names.</summary>
		/// <returns>The common language runtime type namespace name from the provided namespace and assembly names.</returns>
		/// <param name="typeNamespace">The namespace that is to be coded. </param>
		/// <param name="assemblyName">The name of the assembly that is to be coded. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="assemblyName" /> and <paramref name="typeNamespace" /> parameters are both either null or empty. </exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004E2A RID: 20010 RVA: 0x0011A614 File Offset: 0x00118814
		public static string CodeXmlNamespaceForClrTypeNamespace(string typeNamespace, string assemblyName)
		{
			if (assemblyName == string.Empty)
			{
				return SoapServices.XmlNsForClrTypeWithNs + typeNamespace;
			}
			if (typeNamespace == string.Empty)
			{
				return SoapServices.EncodeNs(SoapServices.XmlNsForClrTypeWithAssembly + assemblyName);
			}
			return SoapServices.EncodeNs(SoapServices.XmlNsForClrTypeWithNsAndAssembly + typeNamespace + "/" + assemblyName);
		}

		/// <summary>Decodes the XML namespace and assembly names from the provided common language runtime namespace.</summary>
		/// <returns>true if the namespace and assembly names were successfully decoded; otherwise, false.</returns>
		/// <param name="inNamespace">The common language runtime namespace. </param>
		/// <param name="typeNamespace">When this method returns, contains a <see cref="T:System.String" /> that holds the decoded namespace name. This parameter is passed uninitialized. </param>
		/// <param name="assemblyName">When this method returns, contains a <see cref="T:System.String" /> that holds the decoded assembly name. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inNamespace" /> parameter is null or empty. </exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004E2B RID: 20011 RVA: 0x0011A670 File Offset: 0x00118870
		public static bool DecodeXmlNamespaceForClrTypeNamespace(string inNamespace, out string typeNamespace, out string assemblyName)
		{
			if (inNamespace == null)
			{
				throw new ArgumentNullException("inNamespace");
			}
			inNamespace = SoapServices.DecodeNs(inNamespace);
			typeNamespace = null;
			assemblyName = null;
			if (inNamespace.StartsWith(SoapServices.XmlNsForClrTypeWithNsAndAssembly))
			{
				int length = SoapServices.XmlNsForClrTypeWithNsAndAssembly.Length;
				if (length >= inNamespace.Length)
				{
					return false;
				}
				int num = inNamespace.IndexOf('/', length + 1);
				if (num == -1)
				{
					return false;
				}
				typeNamespace = inNamespace.Substring(length, num - length);
				assemblyName = inNamespace.Substring(num + 1);
				return true;
			}
			else
			{
				if (inNamespace.StartsWith(SoapServices.XmlNsForClrTypeWithNs))
				{
					int length2 = SoapServices.XmlNsForClrTypeWithNs.Length;
					typeNamespace = inNamespace.Substring(length2);
					return true;
				}
				if (inNamespace.StartsWith(SoapServices.XmlNsForClrTypeWithAssembly))
				{
					int length3 = SoapServices.XmlNsForClrTypeWithAssembly.Length;
					assemblyName = inNamespace.Substring(length3);
					return true;
				}
				return false;
			}
		}

		/// <summary>Retrieves field type from XML attribute name, namespace, and the <see cref="T:System.Type" /> of the containing object.</summary>
		/// <param name="containingType">The <see cref="T:System.Type" /> of the object that contains the field. </param>
		/// <param name="xmlAttribute">The XML attribute name of the field type. </param>
		/// <param name="xmlNamespace">The XML namespace of the field type. </param>
		/// <param name="type">When this method returns, contains a <see cref="T:System.Type" /> of the field. This parameter is passed uninitialized. </param>
		/// <param name="name">When this method returns, contains a <see cref="T:System.String" /> that holds the name of the field. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004E2C RID: 20012 RVA: 0x0011A730 File Offset: 0x00118930
		public static void GetInteropFieldTypeAndNameFromXmlAttribute(Type containingType, string xmlAttribute, string xmlNamespace, out Type type, out string name)
		{
			SoapServices.TypeInfo typeInfo = (SoapServices.TypeInfo)SoapServices._typeInfos[containingType];
			SoapServices.GetInteropFieldInfo((typeInfo != null) ? typeInfo.Attributes : null, xmlAttribute, xmlNamespace, out type, out name);
		}

		/// <summary>Retrieves the <see cref="T:System.Type" /> and name of a field from the provided XML element name, namespace, and the containing type.</summary>
		/// <param name="containingType">The <see cref="T:System.Type" /> of the object that contains the field. </param>
		/// <param name="xmlElement">The XML element name of field. </param>
		/// <param name="xmlNamespace">The XML namespace of the field type. </param>
		/// <param name="type">When this method returns, contains a <see cref="T:System.Type" /> of the field. This parameter is passed uninitialized. </param>
		/// <param name="name">When this method returns, contains a <see cref="T:System.String" /> that holds the name of the field. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004E2D RID: 20013 RVA: 0x0011A764 File Offset: 0x00118964
		public static void GetInteropFieldTypeAndNameFromXmlElement(Type containingType, string xmlElement, string xmlNamespace, out Type type, out string name)
		{
			SoapServices.TypeInfo typeInfo = (SoapServices.TypeInfo)SoapServices._typeInfos[containingType];
			SoapServices.GetInteropFieldInfo((typeInfo != null) ? typeInfo.Elements : null, xmlElement, xmlNamespace, out type, out name);
		}

		// Token: 0x06004E2E RID: 20014 RVA: 0x0011A798 File Offset: 0x00118998
		private static void GetInteropFieldInfo(Hashtable fields, string xmlName, string xmlNamespace, out Type type, out string name)
		{
			if (fields != null)
			{
				FieldInfo fieldInfo = (FieldInfo)fields[SoapServices.GetNameKey(xmlName, xmlNamespace)];
				if (fieldInfo != null)
				{
					type = fieldInfo.FieldType;
					name = fieldInfo.Name;
					return;
				}
			}
			type = null;
			name = null;
		}

		// Token: 0x06004E2F RID: 20015 RVA: 0x0011A7DD File Offset: 0x001189DD
		private static string GetNameKey(string name, string namspace)
		{
			if (namspace == null)
			{
				return name;
			}
			return name + " " + namspace;
		}

		/// <summary>Retrieves the <see cref="T:System.Type" /> that should be used during deserialization of an unrecognized object type with the given XML element name and namespace.</summary>
		/// <returns>The <see cref="T:System.Type" /> of object associated with the specified XML element name and namespace.</returns>
		/// <param name="xmlElement">The XML element name of the unknown object type. </param>
		/// <param name="xmlNamespace">The XML namespace of the unknown object type. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004E30 RID: 20016 RVA: 0x0011A7F0 File Offset: 0x001189F0
		public static Type GetInteropTypeFromXmlElement(string xmlElement, string xmlNamespace)
		{
			object syncRoot = SoapServices._xmlElements.SyncRoot;
			Type type;
			lock (syncRoot)
			{
				type = (Type)SoapServices._xmlElements[xmlElement + " " + xmlNamespace];
			}
			return type;
		}

		/// <summary>Retrieves the object <see cref="T:System.Type" /> that should be used during deserialization of an unrecognized object type with the given XML type name and namespace.</summary>
		/// <returns>The <see cref="T:System.Type" /> of object associated with the specified XML type name and namespace.</returns>
		/// <param name="xmlType">The XML type of the unknown object type. </param>
		/// <param name="xmlTypeNamespace">The XML type namespace of the unknown object type. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004E31 RID: 20017 RVA: 0x0011A84C File Offset: 0x00118A4C
		public static Type GetInteropTypeFromXmlType(string xmlType, string xmlTypeNamespace)
		{
			object syncRoot = SoapServices._xmlTypes.SyncRoot;
			Type type;
			lock (syncRoot)
			{
				type = (Type)SoapServices._xmlTypes[xmlType + " " + xmlTypeNamespace];
			}
			return type;
		}

		// Token: 0x06004E32 RID: 20018 RVA: 0x0011A8A8 File Offset: 0x00118AA8
		private static string GetAssemblyName(MethodBase mb)
		{
			if (mb.DeclaringType.Assembly == typeof(object).Assembly)
			{
				return string.Empty;
			}
			return mb.DeclaringType.Assembly.GetName().Name;
		}

		/// <summary>Returns the SOAPAction value associated with the method specified in the given <see cref="T:System.Reflection.MethodBase" />.</summary>
		/// <returns>The SOAPAction value associated with the method specified in the given <see cref="T:System.Reflection.MethodBase" />.</returns>
		/// <param name="mb">The <see cref="T:System.Reflection.MethodBase" /> that contains the method for which a SOAPAction is requested. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004E33 RID: 20019 RVA: 0x0011A8E6 File Offset: 0x00118AE6
		public static string GetSoapActionFromMethodBase(MethodBase mb)
		{
			return SoapServices.InternalGetSoapAction(mb);
		}

		/// <summary>Determines the type and method name of the method associated with the specified SOAPAction value.</summary>
		/// <returns>true if the type and method name were successfully recovered; otherwise, false.</returns>
		/// <param name="soapAction">The SOAPAction of the method for which the type and method names were requested. </param>
		/// <param name="typeName">When this method returns, contains a <see cref="T:System.String" /> that holds the type name of the method in question. This parameter is passed uninitialized. </param>
		/// <param name="methodName">When this method returns, contains a <see cref="T:System.String" /> that holds the method name of the method in question. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The SOAPAction value does not start and end with quotes. </exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004E34 RID: 20020 RVA: 0x0011A8F0 File Offset: 0x00118AF0
		public static bool GetTypeAndMethodNameFromSoapAction(string soapAction, out string typeName, out string methodName)
		{
			object syncRoot = SoapServices._soapActions.SyncRoot;
			lock (syncRoot)
			{
				MethodBase methodBase = (MethodBase)SoapServices._soapActionsMethods[soapAction];
				if (methodBase != null)
				{
					typeName = methodBase.DeclaringType.AssemblyQualifiedName;
					methodName = methodBase.Name;
					return true;
				}
			}
			typeName = null;
			methodName = null;
			int num = soapAction.LastIndexOf('#');
			if (num == -1)
			{
				return false;
			}
			methodName = soapAction.Substring(num + 1);
			string text;
			string text2;
			if (!SoapServices.DecodeXmlNamespaceForClrTypeNamespace(soapAction.Substring(0, num), out text, out text2))
			{
				return false;
			}
			if (text2 == null)
			{
				typeName = text + ", " + typeof(object).Assembly.GetName().Name;
			}
			else
			{
				typeName = text + ", " + text2;
			}
			return true;
		}

		/// <summary>Returns XML element information that should be used when serializing the given type.</summary>
		/// <returns>true if the requested values have been set flagged with <see cref="T:System.Runtime.Remoting.Metadata.SoapTypeAttribute" />; otherwise, false.</returns>
		/// <param name="type">The object <see cref="T:System.Type" /> for which the XML element and namespace names were requested. </param>
		/// <param name="xmlElement">When this method returns, contains a <see cref="T:System.String" /> that holds the XML element name of the specified object type. This parameter is passed uninitialized. </param>
		/// <param name="xmlNamespace">When this method returns, contains a <see cref="T:System.String" /> that holds the XML namespace name of the specified object type. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004E35 RID: 20021 RVA: 0x0011A9DC File Offset: 0x00118BDC
		public static bool GetXmlElementForInteropType(Type type, out string xmlElement, out string xmlNamespace)
		{
			SoapTypeAttribute soapTypeAttribute = (SoapTypeAttribute)InternalRemotingServices.GetCachedSoapAttribute(type);
			if (!soapTypeAttribute.IsInteropXmlElement)
			{
				xmlElement = null;
				xmlNamespace = null;
				return false;
			}
			xmlElement = soapTypeAttribute.XmlElementName;
			xmlNamespace = soapTypeAttribute.XmlNamespace;
			return true;
		}

		/// <summary>Retrieves the XML namespace used during remote calls of the method specified in the given <see cref="T:System.Reflection.MethodBase" />.</summary>
		/// <returns>The XML namespace used during remote calls of the specified method.</returns>
		/// <param name="mb">The <see cref="T:System.Reflection.MethodBase" /> of the method for which the XML namespace was requested. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004E36 RID: 20022 RVA: 0x0011AA16 File Offset: 0x00118C16
		public static string GetXmlNamespaceForMethodCall(MethodBase mb)
		{
			return SoapServices.CodeXmlNamespaceForClrTypeNamespace(mb.DeclaringType.FullName, SoapServices.GetAssemblyName(mb));
		}

		/// <summary>Retrieves the XML namespace used during the generation of responses to the remote call to the method specified in the given <see cref="T:System.Reflection.MethodBase" />.</summary>
		/// <returns>The XML namespace used during the generation of responses to a remote method call.</returns>
		/// <param name="mb">The <see cref="T:System.Reflection.MethodBase" /> of the method for which the XML namespace was requested. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004E37 RID: 20023 RVA: 0x0011AA16 File Offset: 0x00118C16
		public static string GetXmlNamespaceForMethodResponse(MethodBase mb)
		{
			return SoapServices.CodeXmlNamespaceForClrTypeNamespace(mb.DeclaringType.FullName, SoapServices.GetAssemblyName(mb));
		}

		/// <summary>Returns XML type information that should be used when serializing the given <see cref="T:System.Type" />.</summary>
		/// <returns>true if the requested values have been set flagged with <see cref="T:System.Runtime.Remoting.Metadata.SoapTypeAttribute" />; otherwise, false.</returns>
		/// <param name="type">The object <see cref="T:System.Type" /> for which the XML element and namespace names were requested. </param>
		/// <param name="xmlType">The XML type of the specified object <see cref="T:System.Type" />. </param>
		/// <param name="xmlTypeNamespace">The XML type namespace of the specified object <see cref="T:System.Type" />. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004E38 RID: 20024 RVA: 0x0011AA30 File Offset: 0x00118C30
		public static bool GetXmlTypeForInteropType(Type type, out string xmlType, out string xmlTypeNamespace)
		{
			SoapTypeAttribute soapTypeAttribute = (SoapTypeAttribute)InternalRemotingServices.GetCachedSoapAttribute(type);
			if (!soapTypeAttribute.IsInteropXmlType)
			{
				xmlType = null;
				xmlTypeNamespace = null;
				return false;
			}
			xmlType = soapTypeAttribute.XmlTypeName;
			xmlTypeNamespace = soapTypeAttribute.XmlTypeNamespace;
			return true;
		}

		/// <summary>Returns a Boolean value that indicates whether the specified namespace is native to the common language runtime.</summary>
		/// <returns>true if the given namespace is native to the common language runtime; otherwise, false.</returns>
		/// <param name="namespaceString">The namespace to check in the common language runtime. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004E39 RID: 20025 RVA: 0x0011AA6A File Offset: 0x00118C6A
		public static bool IsClrTypeNamespace(string namespaceString)
		{
			return namespaceString.StartsWith(SoapServices.XmlNsForClrType);
		}

		/// <summary>Determines if the specified SOAPAction is acceptable for a given <see cref="T:System.Reflection.MethodBase" />.</summary>
		/// <returns>true if the specified SOAPAction is acceptable for a given <see cref="T:System.Reflection.MethodBase" />; otherwise, false.</returns>
		/// <param name="soapAction">The SOAPAction to check against the given <see cref="T:System.Reflection.MethodBase" />. </param>
		/// <param name="mb">The <see cref="T:System.Reflection.MethodBase" /> the specified SOAPAction is checked against. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004E3A RID: 20026 RVA: 0x0011AA78 File Offset: 0x00118C78
		public static bool IsSoapActionValidForMethodBase(string soapAction, MethodBase mb)
		{
			string text;
			string text2;
			SoapServices.GetTypeAndMethodNameFromSoapAction(soapAction, out text, out text2);
			if (text2 != mb.Name)
			{
				return false;
			}
			string assemblyQualifiedName = mb.DeclaringType.AssemblyQualifiedName;
			return text == assemblyQualifiedName;
		}

		/// <summary>Preloads every <see cref="T:System.Type" /> found in the specified <see cref="T:System.Reflection.Assembly" /> from the information found in the <see cref="T:System.Runtime.Remoting.Metadata.SoapTypeAttribute" /> associated with each type.</summary>
		/// <param name="assembly">The <see cref="T:System.Reflection.Assembly" /> for each type of which to call <see cref="M:System.Runtime.Remoting.SoapServices.PreLoad(System.Type)" />. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004E3B RID: 20027 RVA: 0x0011AAB4 File Offset: 0x00118CB4
		public static void PreLoad(Assembly assembly)
		{
			Type[] types = assembly.GetTypes();
			for (int i = 0; i < types.Length; i++)
			{
				SoapServices.PreLoad(types[i]);
			}
		}

		/// <summary>Preloads the given <see cref="T:System.Type" /> based on values set in a <see cref="T:System.Runtime.Remoting.Metadata.SoapTypeAttribute" /> on the type.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> to preload. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004E3C RID: 20028 RVA: 0x0011AAE0 File Offset: 0x00118CE0
		public static void PreLoad(Type type)
		{
			SoapServices.TypeInfo typeInfo = SoapServices._typeInfos[type] as SoapServices.TypeInfo;
			if (typeInfo != null)
			{
				return;
			}
			string text;
			string text2;
			if (SoapServices.GetXmlTypeForInteropType(type, out text, out text2))
			{
				SoapServices.RegisterInteropXmlType(text, text2, type);
			}
			if (SoapServices.GetXmlElementForInteropType(type, out text, out text2))
			{
				SoapServices.RegisterInteropXmlElement(text, text2, type);
			}
			object syncRoot = SoapServices._typeInfos.SyncRoot;
			lock (syncRoot)
			{
				typeInfo = new SoapServices.TypeInfo();
				foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
				{
					SoapFieldAttribute soapFieldAttribute = (SoapFieldAttribute)InternalRemotingServices.GetCachedSoapAttribute(fieldInfo);
					if (soapFieldAttribute.IsInteropXmlElement())
					{
						string nameKey = SoapServices.GetNameKey(soapFieldAttribute.XmlElementName, soapFieldAttribute.XmlNamespace);
						if (soapFieldAttribute.UseAttribute)
						{
							if (typeInfo.Attributes == null)
							{
								typeInfo.Attributes = new Hashtable();
							}
							typeInfo.Attributes[nameKey] = fieldInfo;
						}
						else
						{
							if (typeInfo.Elements == null)
							{
								typeInfo.Elements = new Hashtable();
							}
							typeInfo.Elements[nameKey] = fieldInfo;
						}
					}
				}
				SoapServices._typeInfos[type] = typeInfo;
			}
		}

		/// <summary>Associates the given XML element name and namespace with a run-time type that should be used for deserialization.</summary>
		/// <param name="xmlElement">The XML element name to use in deserialization. </param>
		/// <param name="xmlNamespace">The XML namespace to use in deserialization. </param>
		/// <param name="type">The run-time <see cref="T:System.Type" /> to use in deserialization. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004E3D RID: 20029 RVA: 0x0011AC14 File Offset: 0x00118E14
		public static void RegisterInteropXmlElement(string xmlElement, string xmlNamespace, Type type)
		{
			object syncRoot = SoapServices._xmlElements.SyncRoot;
			lock (syncRoot)
			{
				SoapServices._xmlElements[xmlElement + " " + xmlNamespace] = type;
			}
		}

		/// <summary>Associates the given XML type name and namespace with the run-time type that should be used for deserialization.</summary>
		/// <param name="xmlType">The XML type to use in deserialization. </param>
		/// <param name="xmlTypeNamespace">The XML namespace to use in deserialization. </param>
		/// <param name="type">The run-time <see cref="T:System.Type" /> to use in deserialization. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004E3E RID: 20030 RVA: 0x0011AC6C File Offset: 0x00118E6C
		public static void RegisterInteropXmlType(string xmlType, string xmlTypeNamespace, Type type)
		{
			object syncRoot = SoapServices._xmlTypes.SyncRoot;
			lock (syncRoot)
			{
				SoapServices._xmlTypes[xmlType + " " + xmlTypeNamespace] = type;
			}
		}

		/// <summary>Associates the specified <see cref="T:System.Reflection.MethodBase" /> with the SOAPAction cached with it.</summary>
		/// <param name="mb">The <see cref="T:System.Reflection.MethodBase" /> of the method to associate with the SOAPAction cached with it. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004E3F RID: 20031 RVA: 0x0011ACC4 File Offset: 0x00118EC4
		public static void RegisterSoapActionForMethodBase(MethodBase mb)
		{
			SoapServices.InternalGetSoapAction(mb);
		}

		// Token: 0x06004E40 RID: 20032 RVA: 0x0011ACD0 File Offset: 0x00118ED0
		private static string InternalGetSoapAction(MethodBase mb)
		{
			object syncRoot = SoapServices._soapActions.SyncRoot;
			string text2;
			lock (syncRoot)
			{
				string text = (string)SoapServices._soapActions[mb];
				if (text == null)
				{
					text = ((SoapMethodAttribute)InternalRemotingServices.GetCachedSoapAttribute(mb)).SoapAction;
					SoapServices._soapActions[mb] = text;
					SoapServices._soapActionsMethods[text] = mb;
				}
				text2 = text;
			}
			return text2;
		}

		/// <summary>Associates the provided SOAPAction value with the given <see cref="T:System.Reflection.MethodBase" /> for use in channel sinks.</summary>
		/// <param name="mb">The <see cref="T:System.Reflection.MethodBase" /> to associate with the provided SOAPAction. </param>
		/// <param name="soapAction">The SOAPAction value to associate with the given <see cref="T:System.Reflection.MethodBase" />. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004E41 RID: 20033 RVA: 0x0011AD50 File Offset: 0x00118F50
		public static void RegisterSoapActionForMethodBase(MethodBase mb, string soapAction)
		{
			object syncRoot = SoapServices._soapActions.SyncRoot;
			lock (syncRoot)
			{
				SoapServices._soapActions[mb] = soapAction;
				SoapServices._soapActionsMethods[soapAction] = mb;
			}
		}

		// Token: 0x06004E42 RID: 20034 RVA: 0x0011ADA8 File Offset: 0x00118FA8
		private static string EncodeNs(string ns)
		{
			ns = ns.Replace(",", "%2C");
			ns = ns.Replace(" ", "%20");
			return ns.Replace("=", "%3D");
		}

		// Token: 0x06004E43 RID: 20035 RVA: 0x0011ADDE File Offset: 0x00118FDE
		private static string DecodeNs(string ns)
		{
			ns = ns.Replace("%2C", ",");
			ns = ns.Replace("%20", " ");
			return ns.Replace("%3D", "=");
		}

		// Token: 0x040029D7 RID: 10711
		private static Hashtable _xmlTypes = new Hashtable();

		// Token: 0x040029D8 RID: 10712
		private static Hashtable _xmlElements = new Hashtable();

		// Token: 0x040029D9 RID: 10713
		private static Hashtable _soapActions = new Hashtable();

		// Token: 0x040029DA RID: 10714
		private static Hashtable _soapActionsMethods = new Hashtable();

		// Token: 0x040029DB RID: 10715
		private static Hashtable _typeInfos = new Hashtable();

		// Token: 0x02000764 RID: 1892
		private class TypeInfo
		{
			// Token: 0x040029DC RID: 10716
			public Hashtable Attributes;

			// Token: 0x040029DD RID: 10717
			public Hashtable Elements;
		}
	}
}
