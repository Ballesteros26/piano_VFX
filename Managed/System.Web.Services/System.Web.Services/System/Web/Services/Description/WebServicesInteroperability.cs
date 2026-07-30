using System;
using System.Collections;
using System.Collections.Specialized;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>The <see cref="T:System.Web.Services.Description.WebServicesInteroperability" /> class provides methods to verify whether a given Web service or services conforms to a given Web Services Interoperability (WS-I) Organization specification.</summary>
	// Token: 0x02000139 RID: 313
	public sealed class WebServicesInteroperability
	{
		// Token: 0x06000987 RID: 2439 RVA: 0x0000210F File Offset: 0x0000030F
		private WebServicesInteroperability()
		{
		}

		/// <summary>Verifies whether a given Web service or services conforms to a given WS-I specification, and provides a list of any violations that it finds.</summary>
		/// <returns>true if the Web service described by <paramref name="description" /> conforms to the Web services interoperability specification indicated by <paramref name="claims" />; otherwise false.</returns>
		/// <param name="claims">A member of <see cref="T:System.Web.Services.WsiProfiles" /> that indicates a Web services interoperability specification.</param>
		/// <param name="description">A <see cref="T:System.Web.Services.Description.ServiceDescription" /> that describes a Web service.</param>
		/// <param name="violations">A <see cref="T:System.Web.Services.Description.BasicProfileViolationCollection" /> that contains any violations that were found.</param>
		// Token: 0x06000988 RID: 2440 RVA: 0x0004190C File Offset: 0x0003FB0C
		public static bool CheckConformance(WsiProfiles claims, ServiceDescription description, BasicProfileViolationCollection violations)
		{
			if (description == null)
			{
				throw new ArgumentNullException("description");
			}
			return WebServicesInteroperability.CheckConformance(claims, new ServiceDescriptionCollection { description }, violations);
		}

		/// <summary>Verifies whether a given Web service or services conforms to a given WS-I specification, and provides a list of any violations that it finds.</summary>
		/// <returns>true if the Web service descriptions contained in <paramref name="descriptions" /> conform to the Web services interoperability specification indicated by <paramref name="claims" />; false otherwise.</returns>
		/// <param name="claims">A member of <see cref="T:System.Web.Services.WsiProfiles" /> that indicates a Web services interoperability specification.</param>
		/// <param name="descriptions">A <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" /> that contains Web service descriptions.</param>
		/// <param name="violations">A <see cref="T:System.Web.Services.Description.BasicProfileViolationCollection" /> that contains any violations that were found.</param>
		// Token: 0x06000989 RID: 2441 RVA: 0x0004193D File Offset: 0x0003FB3D
		public static bool CheckConformance(WsiProfiles claims, ServiceDescriptionCollection descriptions, BasicProfileViolationCollection violations)
		{
			if ((claims & WsiProfiles.BasicProfile1_1) == WsiProfiles.None)
			{
				return true;
			}
			if (descriptions == null)
			{
				throw new ArgumentNullException("descriptions");
			}
			if (violations == null)
			{
				throw new ArgumentNullException("violations");
			}
			int count = violations.Count;
			WebServicesInteroperability.AnalyzeDescription(descriptions, violations);
			return count == violations.Count;
		}

		/// <summary>Verifies whether a given Web service or services conforms to a given WS-I specification, and provides a list of any violations that it finds.</summary>
		/// <returns>true if the Web service described by <paramref name="webReference" /> conforms to the Web services interoperability specification indicated by <paramref name="claims" />; false otherwise.</returns>
		/// <param name="claims">A member of <see cref="T:System.Web.Services.WsiProfiles" /> that indicates a Web services interoperability specification.</param>
		/// <param name="webReference">A <see cref="T:System.Web.Services.Description.WebReference" /> that describes a Web service.</param>
		/// <param name="violations">A <see cref="T:System.Web.Services.Description.BasicProfileViolationCollection" /> that contains any violations that were found.</param>
		// Token: 0x0600098A RID: 2442 RVA: 0x00041978 File Offset: 0x0003FB78
		public static bool CheckConformance(WsiProfiles claims, WebReference webReference, BasicProfileViolationCollection violations)
		{
			if ((claims & WsiProfiles.BasicProfile1_1) == WsiProfiles.None)
			{
				return true;
			}
			if (webReference == null)
			{
				return true;
			}
			if (violations == null)
			{
				throw new ArgumentNullException("violations");
			}
			XmlSchemas xmlSchemas = new XmlSchemas();
			ServiceDescriptionCollection serviceDescriptionCollection = new ServiceDescriptionCollection();
			StringCollection stringCollection = new StringCollection();
			foreach (object obj in webReference.Documents)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				ServiceDescriptionImporter.AddDocument((string)dictionaryEntry.Key, dictionaryEntry.Value, xmlSchemas, serviceDescriptionCollection, stringCollection);
			}
			int count = violations.Count;
			WebServicesInteroperability.AnalyzeDescription(serviceDescriptionCollection, violations);
			return count == violations.Count;
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x00041A2C File Offset: 0x0003FC2C
		internal static bool AnalyzeBinding(Binding binding, ServiceDescription description, ServiceDescriptionCollection descriptions, BasicProfileViolationCollection violations)
		{
			bool flag = false;
			bool flag2 = false;
			SoapBinding soapBinding = (SoapBinding)binding.Extensions.Find(typeof(SoapBinding));
			if (soapBinding == null || soapBinding.GetType() != typeof(SoapBinding))
			{
				return false;
			}
			SoapBindingStyle soapBindingStyle = ((soapBinding.Style == SoapBindingStyle.Default) ? SoapBindingStyle.Document : soapBinding.Style);
			if (soapBinding.Transport.Length == 0)
			{
				violations.Add("R2701", Res.GetString("BindingMissingAttribute", new object[] { binding.Name, description.TargetNamespace, "transport" }));
			}
			else if (soapBinding.Transport != "http://schemas.xmlsoap.org/soap/http")
			{
				violations.Add("R2702", Res.GetString("BindingInvalidAttribute", new object[] { binding.Name, description.TargetNamespace, "transport", soapBinding.Transport }));
			}
			PortType portType = descriptions.GetPortType(binding.Type);
			Hashtable hashtable = new Hashtable();
			if (portType != null)
			{
				foreach (object obj in portType.Operations)
				{
					Operation operation = (Operation)obj;
					if (operation.Messages.Flow == OperationFlow.Notification)
					{
						violations.Add("R2303", Res.GetString("OperationFlowNotification", new object[]
						{
							operation.Name,
							binding.Type.Namespace,
							binding.Type.Namespace
						}));
					}
					if (operation.Messages.Flow == OperationFlow.SolicitResponse)
					{
						violations.Add("R2303", Res.GetString("OperationFlowSolicitResponse", new object[]
						{
							operation.Name,
							binding.Type.Namespace,
							binding.Type.Namespace
						}));
					}
					if (hashtable[operation.Name] != null)
					{
						violations.Add("R2304", Res.GetString("Operation", new object[]
						{
							operation.Name,
							binding.Type.Name,
							binding.Type.Namespace
						}));
					}
					else
					{
						OperationBinding operationBinding = null;
						foreach (object obj2 in binding.Operations)
						{
							OperationBinding operationBinding2 = (OperationBinding)obj2;
							if (operation.IsBoundBy(operationBinding2))
							{
								if (operationBinding != null)
								{
									violations.Add("R2304", Res.GetString("OperationBinding", new object[]
									{
										operationBinding.Name,
										operationBinding.Parent.Name,
										description.TargetNamespace
									}));
								}
								operationBinding = operationBinding2;
							}
						}
						if (operationBinding == null)
						{
							violations.Add("R2718", Res.GetString("OperationMissingBinding", new object[]
							{
								operation.Name,
								binding.Type.Name,
								binding.Type.Namespace
							}));
						}
						else
						{
							hashtable.Add(operation.Name, operation);
						}
					}
				}
			}
			Hashtable hashtable2 = new Hashtable();
			SoapBindingStyle soapBindingStyle2 = SoapBindingStyle.Default;
			foreach (object obj3 in binding.Operations)
			{
				OperationBinding operationBinding3 = (OperationBinding)obj3;
				SoapBindingStyle soapBindingStyle3 = soapBindingStyle;
				string name = operationBinding3.Name;
				if (name != null)
				{
					if (hashtable[name] == null)
					{
						violations.Add("R2718", Res.GetString("PortTypeOperationMissing", new object[]
						{
							operationBinding3.Name,
							binding.Name,
							description.TargetNamespace,
							binding.Type.Name,
							binding.Type.Namespace
						}));
					}
					Operation operation2 = WebServicesInteroperability.FindOperation(portType.Operations, operationBinding3);
					SoapOperationBinding soapOperationBinding = (SoapOperationBinding)operationBinding3.Extensions.Find(typeof(SoapOperationBinding));
					if (soapOperationBinding != null)
					{
						if (soapBindingStyle2 == SoapBindingStyle.Default)
						{
							soapBindingStyle2 = soapOperationBinding.Style;
						}
						flag |= soapBindingStyle2 != soapOperationBinding.Style;
						soapBindingStyle3 = ((soapOperationBinding.Style != SoapBindingStyle.Default) ? soapOperationBinding.Style : soapBindingStyle);
					}
					if (operationBinding3.Input != null)
					{
						SoapBodyBinding soapBodyBinding = WebServicesInteroperability.FindSoapBodyBinding(true, operationBinding3.Input.Extensions, violations, soapBindingStyle3 == SoapBindingStyle.Document, operationBinding3.Name, binding.Name, description.TargetNamespace);
						if (soapBodyBinding != null && soapBodyBinding.Use != SoapBindingUse.Encoded)
						{
							Message message = ((operation2 == null) ? null : ((operation2.Messages.Input == null) ? null : descriptions.GetMessage(operation2.Messages.Input.Message)));
							if (soapBindingStyle3 == SoapBindingStyle.Rpc)
							{
								WebServicesInteroperability.CheckMessageParts(message, soapBodyBinding.Parts, false, operationBinding3.Name, binding.Name, description.TargetNamespace, hashtable2, violations);
							}
							else
							{
								flag2 = flag2 || (soapBodyBinding.Parts != null && soapBodyBinding.Parts.Length > 1);
								bool flag3 = soapBodyBinding.Parts != null && soapBodyBinding.Parts.Length != 0;
								WebServicesInteroperability.CheckMessageParts(message, soapBodyBinding.Parts, true, operationBinding3.Name, binding.Name, description.TargetNamespace, hashtable2, violations);
								if (!flag3 && message != null && message.Parts.Count > 1)
								{
									violations.Add("R2210", Res.GetString("OperationBinding", new object[] { operationBinding3.Name, binding.Name, description.TargetNamespace }));
								}
							}
						}
					}
					if (operationBinding3.Output != null)
					{
						SoapBodyBinding soapBodyBinding2 = WebServicesInteroperability.FindSoapBodyBinding(false, operationBinding3.Output.Extensions, violations, soapBindingStyle3 == SoapBindingStyle.Document, operationBinding3.Name, binding.Name, description.TargetNamespace);
						if (soapBodyBinding2 != null && soapBodyBinding2.Use != SoapBindingUse.Encoded)
						{
							Message message2 = ((operation2 == null) ? null : ((operation2.Messages.Output == null) ? null : descriptions.GetMessage(operation2.Messages.Output.Message)));
							if (soapBindingStyle3 == SoapBindingStyle.Rpc)
							{
								WebServicesInteroperability.CheckMessageParts(message2, soapBodyBinding2.Parts, false, operationBinding3.Name, binding.Name, description.TargetNamespace, null, violations);
							}
							else
							{
								flag2 = flag2 || (soapBodyBinding2.Parts != null && soapBodyBinding2.Parts.Length > 1);
								bool flag4 = soapBodyBinding2.Parts != null && soapBodyBinding2.Parts.Length != 0;
								WebServicesInteroperability.CheckMessageParts(message2, soapBodyBinding2.Parts, true, operationBinding3.Name, binding.Name, description.TargetNamespace, null, violations);
								if (!flag4 && message2 != null && message2.Parts.Count > 1)
								{
									violations.Add("R2210", Res.GetString("OperationBinding", new object[] { operationBinding3.Name, binding.Name, description.TargetNamespace }));
								}
							}
						}
					}
					foreach (object obj4 in operationBinding3.Faults)
					{
						FaultBinding faultBinding = (FaultBinding)obj4;
						foreach (object obj5 in faultBinding.Extensions)
						{
							ServiceDescriptionFormatExtension serviceDescriptionFormatExtension = (ServiceDescriptionFormatExtension)obj5;
							if (serviceDescriptionFormatExtension is SoapFaultBinding)
							{
								SoapFaultBinding soapFaultBinding = (SoapFaultBinding)serviceDescriptionFormatExtension;
								if (soapFaultBinding.Use == SoapBindingUse.Encoded)
								{
									violations.Add("R2706", WebServicesInteroperability.MessageString(soapFaultBinding, operationBinding3.Name, binding.Name, description.TargetNamespace, false, null));
								}
								else
								{
									if (soapFaultBinding.Name == null || soapFaultBinding.Name.Length == 0)
									{
										violations.Add("R2721", Res.GetString("FaultBinding", new object[] { faultBinding.Name, operationBinding3.Name, binding.Name, description.TargetNamespace }));
									}
									else if (soapFaultBinding.Name != faultBinding.Name)
									{
										violations.Add("R2754", Res.GetString("FaultBinding", new object[] { faultBinding.Name, operationBinding3.Name, binding.Name, description.TargetNamespace }));
									}
									if (soapFaultBinding.Namespace != null && soapFaultBinding.Namespace.Length > 0)
									{
										violations.Add((soapBindingStyle3 == SoapBindingStyle.Document) ? "R2716" : "R2726", WebServicesInteroperability.MessageString(soapFaultBinding, operationBinding3.Name, binding.Name, description.TargetNamespace, false, null));
									}
								}
							}
						}
					}
					if (hashtable[operationBinding3.Name] == null)
					{
						violations.Add("R2718", Res.GetString("PortTypeOperationMissing", new object[]
						{
							operationBinding3.Name,
							binding.Name,
							description.TargetNamespace,
							binding.Type.Name,
							binding.Type.Namespace
						}));
					}
				}
			}
			if (flag2)
			{
				violations.Add("R2201", Res.GetString("BindingMultipleParts", new object[] { binding.Name, description.TargetNamespace, "parts" }));
			}
			if (flag)
			{
				violations.Add("R2705", Res.GetString("Binding", new object[] { binding.Name, description.TargetNamespace }));
			}
			return true;
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x00042444 File Offset: 0x00040644
		internal static void AnalyzeDescription(ServiceDescriptionCollection descriptions, BasicProfileViolationCollection violations)
		{
			bool flag = false;
			foreach (object obj in descriptions)
			{
				ServiceDescription serviceDescription = (ServiceDescription)obj;
				SchemaCompiler.Compile(serviceDescription.Types.Schemas);
				WebServicesInteroperability.CheckWsdlImports(serviceDescription, violations);
				WebServicesInteroperability.CheckTypes(serviceDescription, violations);
				foreach (string text in serviceDescription.ValidationWarnings)
				{
					violations.Add("R2028, R2029", text);
				}
				foreach (object obj2 in serviceDescription.Bindings)
				{
					Binding binding = (Binding)obj2;
					flag |= WebServicesInteroperability.AnalyzeBinding(binding, serviceDescription, descriptions, violations);
				}
			}
			if (flag)
			{
				WebServicesInteroperability.CheckExtensions(descriptions, violations);
				return;
			}
			violations.Add("Rxxxx");
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x00042578 File Offset: 0x00040778
		private static void CheckWsdlImports(ServiceDescription description, BasicProfileViolationCollection violations)
		{
			foreach (object obj in description.Imports)
			{
				Import import = (Import)obj;
				if (import.Location == null || import.Location.Length == 0)
				{
					violations.Add("R2007", Res.GetString("Description", new object[] { description.TargetNamespace }));
				}
				string @namespace = import.Namespace;
				Uri uri;
				if (@namespace.Length != 0 && !Uri.TryCreate(@namespace, UriKind.Absolute, out uri))
				{
					violations.Add("R2803", Res.GetString("Description", new object[] { description.TargetNamespace }));
				}
			}
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0004264C File Offset: 0x0004084C
		private static void CheckTypes(ServiceDescription description, BasicProfileViolationCollection violations)
		{
			foreach (object obj in description.Types.Schemas)
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				if (xmlSchema.TargetNamespace == null || xmlSchema.TargetNamespace.Length == 0)
				{
					using (XmlSchemaObjectEnumerator enumerator2 = xmlSchema.Items.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							if (!(enumerator2.Current is XmlSchemaAnnotation))
							{
								violations.Add("R2105", Res.GetString("Element", new object[] { "schema", description.TargetNamespace }));
								return;
							}
						}
					}
				}
			}
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x00042730 File Offset: 0x00040930
		private static void CheckMessagePart(MessagePart part, bool element, string message, string operation, string binding, string ns, Hashtable wireSignatures, BasicProfileViolationCollection violations)
		{
			if (part == null)
			{
				if (!element)
				{
					WebServicesInteroperability.AddSignature(wireSignatures, operation, ns, message, ns, violations);
					return;
				}
				WebServicesInteroperability.AddSignature(wireSignatures, null, null, message, ns, violations);
				return;
			}
			else
			{
				if (part.Type != null && !part.Type.IsEmpty && part.Element != null && !part.Element.IsEmpty)
				{
					violations.Add("R2306", Res.GetString("Part", new object[] { part.Name, message, ns }));
				}
				else
				{
					XmlQualifiedName xmlQualifiedName = ((part.Type == null || part.Type.IsEmpty) ? part.Element : part.Type);
					if (xmlQualifiedName.Namespace == null || xmlQualifiedName.Namespace.Length == 0)
					{
						violations.Add("R1014", Res.GetString("Part", new object[] { part.Name, message, ns }));
					}
				}
				if (!element && (part.Type == null || part.Type.IsEmpty))
				{
					violations.Add("R2203", Res.GetString("Part", new object[] { part.Name, message, ns }));
				}
				if (element && (part.Element == null || part.Element.IsEmpty))
				{
					violations.Add("R2204", Res.GetString("Part", new object[] { part.Name, message, ns }));
				}
				if (!element)
				{
					WebServicesInteroperability.AddSignature(wireSignatures, operation, ns, message, ns, violations);
					return;
				}
				if (part.Element != null)
				{
					WebServicesInteroperability.AddSignature(wireSignatures, part.Element.Name, part.Element.Namespace, message, ns, violations);
				}
				return;
			}
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x00042914 File Offset: 0x00040B14
		private static void AddSignature(Hashtable wireSignatures, string name, string ns, string message, string messageNs, BasicProfileViolationCollection violations)
		{
			if (wireSignatures == null)
			{
				return;
			}
			string text = ns + ":" + name;
			string text2 = (string)wireSignatures[text];
			string text3 = ((ns == null && name == null) ? Res.GetString("WireSignatureEmpty", new object[] { message, messageNs }) : Res.GetString("WireSignature", new object[] { message, messageNs, ns, name }));
			if (text2 != null)
			{
				if (text2.Length > 0)
				{
					violations.Add("R2710", text2);
					violations.Add("R2710", text3);
					wireSignatures[text] = string.Empty;
					return;
				}
			}
			else
			{
				wireSignatures[text] = text3;
			}
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x000429C0 File Offset: 0x00040BC0
		private static void CheckMessageParts(Message message, string[] parts, bool element, string operation, string binding, string ns, Hashtable wireSignatures, BasicProfileViolationCollection violations)
		{
			if (message == null)
			{
				return;
			}
			if (message.Parts == null || message.Parts.Count == 0)
			{
				if (!element)
				{
					WebServicesInteroperability.AddSignature(wireSignatures, operation, ns, message.Name, ns, violations);
					return;
				}
				WebServicesInteroperability.AddSignature(wireSignatures, null, null, message.Name, ns, violations);
				return;
			}
			else
			{
				if (parts == null || parts.Length == 0)
				{
					for (int i = 0; i < message.Parts.Count; i++)
					{
						WebServicesInteroperability.CheckMessagePart(message.Parts[i], element, message.Name, operation, binding, ns, (i == 0) ? wireSignatures : null, violations);
					}
					return;
				}
				for (int j = 0; j < parts.Length; j++)
				{
					if (parts[j] != null)
					{
						MessagePart messagePart = message.Parts[parts[j]];
						WebServicesInteroperability.CheckMessagePart(message.Parts[j], element, message.Name, operation, binding, ns, (j == 0) ? wireSignatures : null, violations);
					}
				}
				return;
			}
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00042AA0 File Offset: 0x00040CA0
		private static SoapBodyBinding FindSoapBodyBinding(bool input, ServiceDescriptionFormatExtensionCollection extensions, BasicProfileViolationCollection violations, bool documentBinding, string operationName, string bindingName, string bindingNs)
		{
			SoapBodyBinding soapBodyBinding = null;
			for (int i = 0; i < extensions.Count; i++)
			{
				object obj = extensions[i];
				string text = null;
				bool flag = false;
				bool flag2 = false;
				if (obj is SoapBodyBinding)
				{
					flag = true;
					soapBodyBinding = (SoapBodyBinding)obj;
					text = soapBodyBinding.Namespace;
					flag2 = soapBodyBinding.Use == SoapBindingUse.Encoded;
				}
				else if (obj is SoapHeaderBinding)
				{
					flag = true;
					SoapHeaderBinding soapHeaderBinding = (SoapHeaderBinding)obj;
					text = soapHeaderBinding.Namespace;
					flag2 = soapHeaderBinding.Use == SoapBindingUse.Encoded;
					if (!flag2 && (soapHeaderBinding.Part == null || soapHeaderBinding.Part.Length == 0))
					{
						violations.Add("R2720", WebServicesInteroperability.MessageString(soapHeaderBinding, operationName, bindingName, bindingNs, input, null));
					}
					if (soapHeaderBinding.Fault != null)
					{
						flag2 |= soapHeaderBinding.Fault.Use == SoapBindingUse.Encoded;
						if (!flag2)
						{
							if (soapHeaderBinding.Fault.Part == null || soapHeaderBinding.Fault.Part.Length == 0)
							{
								violations.Add("R2720", WebServicesInteroperability.MessageString(soapHeaderBinding.Fault, operationName, bindingName, bindingNs, input, null));
							}
							if (soapHeaderBinding.Fault.Namespace != null && soapHeaderBinding.Fault.Namespace.Length > 0)
							{
								violations.Add(documentBinding ? "R2716" : "R2726", WebServicesInteroperability.MessageString(obj, operationName, bindingName, bindingNs, input, null));
							}
						}
					}
				}
				if (flag2)
				{
					violations.Add("R2706", WebServicesInteroperability.MessageString(obj, operationName, bindingName, bindingNs, input, null));
				}
				else if (flag)
				{
					Uri uri;
					if (text == null || text.Length == 0)
					{
						if (!documentBinding && obj is SoapBodyBinding)
						{
							violations.Add("R2717", WebServicesInteroperability.MessageString(obj, operationName, bindingName, bindingNs, input, null));
						}
					}
					else if (documentBinding || !(obj is SoapBodyBinding))
					{
						violations.Add(documentBinding ? "R2716" : "R2726", WebServicesInteroperability.MessageString(obj, operationName, bindingName, bindingNs, input, null));
					}
					else if (!Uri.TryCreate(text, UriKind.Absolute, out uri))
					{
						violations.Add("R2717", WebServicesInteroperability.MessageString(obj, operationName, bindingName, bindingNs, input, Res.GetString("UriValueRelative", new object[] { text })));
					}
				}
			}
			return soapBodyBinding;
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x00042CDC File Offset: 0x00040EDC
		private static string MessageString(object item, string operation, string binding, string ns, bool input, string details)
		{
			string text = null;
			string text2 = null;
			if (item is SoapBodyBinding)
			{
				text = (input ? "InputElement" : "OutputElement");
				text2 = "soapbind:body";
			}
			else if (item is SoapHeaderBinding)
			{
				text = (input ? "InputElement" : "OutputElement");
				text2 = "soapbind:header";
			}
			else if (item is SoapFaultBinding)
			{
				text = "Fault";
				text2 = ((SoapFaultBinding)item).Name;
			}
			else if (item is SoapHeaderFaultBinding)
			{
				text = "HeaderFault";
				text2 = "soapbind:headerfault";
			}
			if (text == null)
			{
				return null;
			}
			return Res.GetString(text, new object[] { text2, operation, binding, ns, details });
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00042D88 File Offset: 0x00040F88
		private static bool CheckExtensions(ServiceDescriptionFormatExtensionCollection extensions)
		{
			using (IEnumerator enumerator = extensions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((ServiceDescriptionFormatExtension)enumerator.Current).Required)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00042DE4 File Offset: 0x00040FE4
		private static void CheckExtensions(Binding binding, ServiceDescription description, BasicProfileViolationCollection violations)
		{
			SoapBinding soapBinding = (SoapBinding)binding.Extensions.Find(typeof(SoapBinding));
			if (soapBinding == null || soapBinding.GetType() != typeof(SoapBinding))
			{
				return;
			}
			if (!WebServicesInteroperability.CheckExtensions(binding.Extensions))
			{
				violations.Add("R2026", Res.GetString("BindingInvalidAttribute", new object[] { binding.Name, description.TargetNamespace, "wsdl:required", "true" }));
			}
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00042E74 File Offset: 0x00041074
		private static void CheckExtensions(ServiceDescriptionCollection descriptions, BasicProfileViolationCollection violations)
		{
			Hashtable hashtable = new Hashtable();
			foreach (object obj in descriptions)
			{
				ServiceDescription serviceDescription = (ServiceDescription)obj;
				if (ServiceDescription.GetConformanceClaims(serviceDescription.Types.DocumentationElement) == WsiProfiles.BasicProfile1_1 && !WebServicesInteroperability.CheckExtensions(serviceDescription.Extensions))
				{
					violations.Add("R2026", Res.GetString("Element", new object[] { "wsdl:types", serviceDescription.TargetNamespace }));
				}
				foreach (object obj2 in serviceDescription.Services)
				{
					Service service = (Service)obj2;
					foreach (object obj3 in service.Ports)
					{
						Port port = (Port)obj3;
						if (ServiceDescription.GetConformanceClaims(port.DocumentationElement) == WsiProfiles.BasicProfile1_1)
						{
							if (!WebServicesInteroperability.CheckExtensions(port.Extensions))
							{
								violations.Add("R2026", Res.GetString("Port", new object[] { port.Name, service.Name, serviceDescription.TargetNamespace }));
							}
							Binding binding = descriptions.GetBinding(port.Binding);
							if (hashtable[binding] != null)
							{
								WebServicesInteroperability.CheckExtensions(binding, serviceDescription, violations);
								hashtable.Add(binding, binding);
							}
						}
					}
				}
				foreach (object obj4 in serviceDescription.Bindings)
				{
					Binding binding2 = (Binding)obj4;
					SoapBinding soapBinding = (SoapBinding)binding2.Extensions.Find(typeof(SoapBinding));
					if (soapBinding != null && !(soapBinding.GetType() != typeof(SoapBinding)) && hashtable[binding2] == null && ServiceDescription.GetConformanceClaims(binding2.DocumentationElement) == WsiProfiles.BasicProfile1_1)
					{
						WebServicesInteroperability.CheckExtensions(binding2, serviceDescription, violations);
						hashtable.Add(binding2, binding2);
					}
				}
			}
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00043114 File Offset: 0x00041314
		private static Operation FindOperation(OperationCollection operations, OperationBinding bindingOperation)
		{
			foreach (object obj in operations)
			{
				Operation operation = (Operation)obj;
				if (operation.IsBoundBy(bindingOperation))
				{
					return operation;
				}
			}
			return null;
		}
	}
}
