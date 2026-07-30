using System;
using System.Collections;
using System.Security.Permissions;
using System.Threading;
using System.Web.Services.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols
{
	/// <summary>The <see cref="T:System.Web.Services.Protocols.SoapHeaderHandling" /> class is used to get, set, write, and read SOAP header content to and from SOAP messages.</summary>
	// Token: 0x02000069 RID: 105
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class SoapHeaderHandling
	{
		// Token: 0x060002B3 RID: 691 RVA: 0x0000C2C4 File Offset: 0x0000A4C4
		private void OnUnknownElement(object sender, XmlElementEventArgs e)
		{
			if (Thread.CurrentThread.GetHashCode() != this.currentThread)
			{
				return;
			}
			if (e.Element == null)
			{
				return;
			}
			SoapUnknownHeader soapUnknownHeader = new SoapUnknownHeader();
			soapUnknownHeader.Element = e.Element;
			this.unknownHeaders.Add(soapUnknownHeader);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000C30C File Offset: 0x0000A50C
		private void OnUnreferencedObject(object sender, UnreferencedObjectEventArgs e)
		{
			if (Thread.CurrentThread.GetHashCode() != this.currentThread)
			{
				return;
			}
			object unreferencedObject = e.UnreferencedObject;
			if (unreferencedObject == null)
			{
				return;
			}
			if (typeof(SoapHeader).IsAssignableFrom(unreferencedObject.GetType()))
			{
				this.unreferencedHeaders.Add((SoapHeader)unreferencedObject);
			}
		}

		/// <summary>Returns a <see cref="T:System.String" /> that contains the SOAP header content of the SOAP message.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the SOAP header content of the SOAP message.</returns>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> to be used in writing the headers.</param>
		/// <param name="serializer">The <see cref="T:System.Xml.Serialization.XmlSerializer" /> to be used in reading the headers.</param>
		/// <param name="headers">The <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" /> that contains the SOAP headers.</param>
		/// <param name="mappings">An array of type <see cref="T:System.Web.Services.Protocols.SoapHeaderMapping" /> that contains the mappings for the SOAP headers.</param>
		/// <param name="direction">A <see cref="T:System.Web.Services.Protocols.SoapHeaderDirection" /> value that indicates the direction of the SOAP headers.</param>
		/// <param name="envelopeNS">A <see cref="T:System.String" /> that contains the namespace for the SOAP message envelope.</param>
		/// <param name="encodingStyle">A <see cref="T:System.String" /> that contains the encoding style for the SOAP headers.</param>
		/// <param name="checkRequiredHeaders">A <see cref="T:System.Boolean" /> that indicates whether to check for the required SOAP headers.</param>
		// Token: 0x060002B5 RID: 693 RVA: 0x0000C360 File Offset: 0x0000A560
		public string ReadHeaders(XmlReader reader, XmlSerializer serializer, SoapHeaderCollection headers, SoapHeaderMapping[] mappings, SoapHeaderDirection direction, string envelopeNS, string encodingStyle, bool checkRequiredHeaders)
		{
			string text = null;
			reader.MoveToContent();
			if (!reader.IsStartElement("Header", envelopeNS))
			{
				if (checkRequiredHeaders && mappings != null && mappings.Length != 0)
				{
					text = SoapHeaderHandling.GetHeaderElementName(mappings[0].headerType);
				}
				return text;
			}
			if (reader.IsEmptyElement)
			{
				reader.Skip();
				return text;
			}
			this.unknownHeaders = new SoapHeaderCollection();
			this.unreferencedHeaders = new SoapHeaderCollection();
			this.currentThread = Thread.CurrentThread.GetHashCode();
			this.envelopeNS = envelopeNS;
			int depth = reader.Depth;
			reader.ReadStartElement();
			reader.MoveToContent();
			XmlDeserializationEvents xmlDeserializationEvents = default(XmlDeserializationEvents);
			xmlDeserializationEvents.OnUnknownElement = new XmlElementEventHandler(this.OnUnknownElement);
			xmlDeserializationEvents.OnUnreferencedObject = new UnreferencedObjectEventHandler(this.OnUnreferencedObject);
			TraceMethod traceMethod = (Tracing.On ? new TraceMethod(this, "ReadHeaders", Array.Empty<object>()) : null);
			if (Tracing.On)
			{
				Tracing.Enter(Tracing.TraceId("TraceReadHeaders"), traceMethod, new TraceMethod(serializer, "Deserialize", new object[] { reader, encodingStyle }));
			}
			object[] array = (object[])serializer.Deserialize(reader, encodingStyle, xmlDeserializationEvents);
			if (Tracing.On)
			{
				Tracing.Exit(Tracing.TraceId("TraceReadHeaders"), traceMethod);
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					SoapHeader soapHeader = (SoapHeader)array[i];
					soapHeader.DidUnderstand = true;
					headers.Add(soapHeader);
				}
				else if (checkRequiredHeaders && text == null)
				{
					text = SoapHeaderHandling.GetHeaderElementName(mappings[i].headerType);
				}
			}
			this.currentThread = 0;
			this.envelopeNS = null;
			foreach (object obj in this.unreferencedHeaders)
			{
				SoapHeader soapHeader2 = (SoapHeader)obj;
				headers.Add(soapHeader2);
			}
			this.unreferencedHeaders = null;
			foreach (object obj2 in this.unknownHeaders)
			{
				SoapHeader soapHeader3 = (SoapHeader)obj2;
				headers.Add(soapHeader3);
			}
			this.unknownHeaders = null;
			while (depth < reader.Depth && reader.Read())
			{
			}
			if (reader.NodeType == XmlNodeType.EndElement)
			{
				reader.Read();
			}
			return text;
		}

		/// <summary>Writes the specified SOAP header content to the SOAP message.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> to be used in writing the headers.</param>
		/// <param name="serializer">The <see cref="T:System.Xml.Serialization.XmlSerializer" /> to be used in writing the headers.</param>
		/// <param name="headers">The <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" /> that contains the SOAP headers to be written.</param>
		/// <param name="mappings">An array of type <see cref="T:System.Web.Services.Protocols.SoapHeaderMapping" /> that contains the mappings for the SOAP headers.</param>
		/// <param name="direction">A <see cref="T:System.Web.Services.Protocols.SoapHeaderDirection" /> value that indicates the direction of the SOAP headers.</param>
		/// <param name="isEncoded">A <see cref="T:System.Boolean" /> that indicates whether the SOAP headers are encoded.</param>
		/// <param name="defaultNS">A <see cref="T:System.String" /> that contains the default namespace for the XML Web service.</param>
		/// <param name="serviceDefaultIsEncoded">A <see cref="T:System.Boolean" /> that indicates whether data sent to and from the XML Web service is encoded by default.</param>
		/// <param name="envelopeNS">A <see cref="T:System.String" /> that contains the namespace for the SOAP message envelope.</param>
		// Token: 0x060002B6 RID: 694 RVA: 0x0000C5D0 File Offset: 0x0000A7D0
		public static void WriteHeaders(XmlWriter writer, XmlSerializer serializer, SoapHeaderCollection headers, SoapHeaderMapping[] mappings, SoapHeaderDirection direction, bool isEncoded, string defaultNS, bool serviceDefaultIsEncoded, string envelopeNS)
		{
			if (headers.Count == 0)
			{
				return;
			}
			writer.WriteStartElement("Header", envelopeNS);
			SoapProtocolVersion soapProtocolVersion;
			string text;
			if (envelopeNS == "http://www.w3.org/2003/05/soap-envelope")
			{
				soapProtocolVersion = SoapProtocolVersion.Soap12;
				text = "http://www.w3.org/2003/05/soap-encoding";
			}
			else
			{
				soapProtocolVersion = SoapProtocolVersion.Soap11;
				text = "http://schemas.xmlsoap.org/soap/encoding/";
			}
			int num = 0;
			ArrayList arrayList = new ArrayList();
			SoapHeader[] array = new SoapHeader[mappings.Length];
			bool[] array2 = new bool[array.Length];
			for (int i = 0; i < headers.Count; i++)
			{
				SoapHeader soapHeader = headers[i];
				if (soapHeader != null)
				{
					soapHeader.version = soapProtocolVersion;
					int num2;
					if (soapHeader is SoapUnknownHeader)
					{
						arrayList.Add(soapHeader);
						num++;
					}
					else if ((num2 = SoapHeaderHandling.FindMapping(mappings, soapHeader, direction)) >= 0 && !array2[num2])
					{
						array[num2] = soapHeader;
						array2[num2] = true;
					}
					else
					{
						arrayList.Add(soapHeader);
					}
				}
			}
			int num3 = arrayList.Count - num;
			if (isEncoded && num3 > 0)
			{
				SoapHeader[] array3 = new SoapHeader[mappings.Length + num3];
				array.CopyTo(array3, 0);
				int num4 = mappings.Length;
				for (int j = 0; j < arrayList.Count; j++)
				{
					if (!(arrayList[j] is SoapUnknownHeader))
					{
						array3[num4++] = (SoapHeader)arrayList[j];
					}
				}
				array = array3;
			}
			TraceMethod traceMethod = (Tracing.On ? new TraceMethod(typeof(SoapHeaderHandling), "WriteHeaders", Array.Empty<object>()) : null);
			if (Tracing.On)
			{
				Tracing.Enter(Tracing.TraceId("TraceWriteHeaders"), traceMethod, new TraceMethod(serializer, "Serialize", new object[]
				{
					writer,
					array,
					null,
					isEncoded ? text : null,
					"h_"
				}));
			}
			serializer.Serialize(writer, array, null, isEncoded ? text : null, "h_");
			if (Tracing.On)
			{
				Tracing.Exit(Tracing.TraceId("TraceWriteHeaders"), traceMethod);
			}
			foreach (object obj in arrayList)
			{
				SoapHeader soapHeader2 = (SoapHeader)obj;
				if (soapHeader2 is SoapUnknownHeader)
				{
					SoapUnknownHeader soapUnknownHeader = (SoapUnknownHeader)soapHeader2;
					if (soapUnknownHeader.Element != null)
					{
						soapUnknownHeader.Element.WriteTo(writer);
					}
				}
				else if (!isEncoded)
				{
					string literalNamespace = SoapReflector.GetLiteralNamespace(defaultNS, serviceDefaultIsEncoded);
					XmlSerializer xmlSerializer = new XmlSerializer(soapHeader2.GetType(), literalNamespace);
					if (Tracing.On)
					{
						Tracing.Enter(Tracing.TraceId("TraceWriteHeaders"), traceMethod, new TraceMethod(xmlSerializer, "Serialize", new object[] { writer, soapHeader2 }));
					}
					xmlSerializer.Serialize(writer, soapHeader2);
					if (Tracing.On)
					{
						Tracing.Exit(Tracing.TraceId("TraceWriteHeaders"), traceMethod);
					}
				}
			}
			for (int k = 0; k < headers.Count; k++)
			{
				SoapHeader soapHeader3 = headers[k];
				if (soapHeader3 != null)
				{
					soapHeader3.version = SoapProtocolVersion.Default;
				}
			}
			writer.WriteEndElement();
			writer.Flush();
		}

		/// <summary>Writes the specified SOAP header content to the SOAP message.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> to be used in writing the headers.</param>
		/// <param name="headers">The <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" /> that contains the SOAP headers to be written.</param>
		/// <param name="envelopeNS">A <see cref="T:System.String" /> that contains the namespace for the SOAP message envelope.</param>
		// Token: 0x060002B7 RID: 695 RVA: 0x0000C8D0 File Offset: 0x0000AAD0
		public static void WriteUnknownHeaders(XmlWriter writer, SoapHeaderCollection headers, string envelopeNS)
		{
			bool flag = true;
			foreach (object obj in headers)
			{
				SoapUnknownHeader soapUnknownHeader = ((SoapHeader)obj) as SoapUnknownHeader;
				if (soapUnknownHeader != null)
				{
					if (flag)
					{
						writer.WriteStartElement("Header", envelopeNS);
						flag = false;
					}
					if (soapUnknownHeader.Element != null)
					{
						soapUnknownHeader.Element.WriteTo(writer);
					}
				}
			}
			if (!flag)
			{
				writer.WriteEndElement();
			}
		}

		/// <summary>Sets the SOAP header content for the specified SOAP message.</summary>
		/// <param name="headers">The <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" /> that contains the SOAP headers.</param>
		/// <param name="target">A <see cref="T:System.Object" /> that represents the SOAP message.</param>
		/// <param name="mappings">An array of type <see cref="T:System.Web.Services.Protocols.SoapHeaderMapping" /> that contains the mappings for the SOAP headers.</param>
		/// <param name="direction">A <see cref="T:System.Web.Services.Protocols.SoapHeaderDirection" /> value that indicates the direction of the SOAP headers.</param>
		/// <param name="client">This parameter is currently not used.</param>
		// Token: 0x060002B8 RID: 696 RVA: 0x0000C958 File Offset: 0x0000AB58
		public static void SetHeaderMembers(SoapHeaderCollection headers, object target, SoapHeaderMapping[] mappings, SoapHeaderDirection direction, bool client)
		{
			bool[] array = new bool[headers.Count];
			if (mappings != null)
			{
				foreach (SoapHeaderMapping soapHeaderMapping in mappings)
				{
					if ((soapHeaderMapping.direction & direction) != (SoapHeaderDirection)0)
					{
						if (soapHeaderMapping.repeats)
						{
							ArrayList arrayList = new ArrayList();
							for (int j = 0; j < headers.Count; j++)
							{
								SoapHeader soapHeader = headers[j];
								if (!array[j] && soapHeaderMapping.headerType.IsAssignableFrom(soapHeader.GetType()))
								{
									arrayList.Add(soapHeader);
									array[j] = true;
								}
							}
							MemberHelper.SetValue(soapHeaderMapping.memberInfo, target, arrayList.ToArray(soapHeaderMapping.headerType));
						}
						else
						{
							bool flag = false;
							for (int k = 0; k < headers.Count; k++)
							{
								SoapHeader soapHeader2 = headers[k];
								if (!array[k] && soapHeaderMapping.headerType.IsAssignableFrom(soapHeader2.GetType()))
								{
									if (flag)
									{
										soapHeader2.DidUnderstand = false;
									}
									else
									{
										flag = true;
										MemberHelper.SetValue(soapHeaderMapping.memberInfo, target, soapHeader2);
										array[k] = true;
									}
								}
							}
						}
					}
				}
			}
			for (int l = 0; l < array.Length; l++)
			{
				if (!array[l])
				{
					SoapHeader soapHeader3 = headers[l];
					if (soapHeader3.MustUnderstand && !soapHeader3.DidUnderstand)
					{
						throw new SoapHeaderException(Res.GetString("WebCannotUnderstandHeader", new object[] { SoapHeaderHandling.GetHeaderElementName(soapHeader3) }), new XmlQualifiedName("MustUnderstand", "http://schemas.xmlsoap.org/soap/envelope/"));
					}
				}
			}
		}

		/// <summary>Gets the SOAP header content for the specified SOAP message.</summary>
		/// <param name="headers">The <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" /> that contains the SOAP headers.</param>
		/// <param name="target">A <see cref="T:System.Object" /> that represents the SOAP message.</param>
		/// <param name="mappings">An array of type <see cref="T:System.Web.Services.Protocols.SoapHeaderMapping" /> that contains the mappings for the SOAP headers.</param>
		/// <param name="direction">A <see cref="T:System.Web.Services.Protocols.SoapHeaderDirection" /> value that indicates the direction of the SOAP headers.</param>
		/// <param name="client">This parameter is currently not used.</param>
		// Token: 0x060002B9 RID: 697 RVA: 0x0000CAD0 File Offset: 0x0000ACD0
		public static void GetHeaderMembers(SoapHeaderCollection headers, object target, SoapHeaderMapping[] mappings, SoapHeaderDirection direction, bool client)
		{
			if (mappings == null || mappings.Length == 0)
			{
				return;
			}
			foreach (SoapHeaderMapping soapHeaderMapping in mappings)
			{
				if ((soapHeaderMapping.direction & direction) != (SoapHeaderDirection)0)
				{
					object value = MemberHelper.GetValue(soapHeaderMapping.memberInfo, target);
					if (soapHeaderMapping.repeats)
					{
						object[] array = (object[])value;
						if (array != null)
						{
							for (int j = 0; j < array.Length; j++)
							{
								if (array[j] != null)
								{
									headers.Add((SoapHeader)array[j]);
								}
							}
						}
					}
					else if (value != null)
					{
						headers.Add((SoapHeader)value);
					}
				}
			}
		}

		/// <summary>Checks to ensure that the SOAP headers that must be understood have been understood; if not, this method throws an exception.</summary>
		/// <param name="headers">The <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" /> that contains the SOAP headers.</param>
		/// <exception cref="T:System.Web.Services.Protocols.SoapHeaderException">A SOAP header that must be understood was not understood.</exception>
		// Token: 0x060002BA RID: 698 RVA: 0x0000CB5C File Offset: 0x0000AD5C
		public static void EnsureHeadersUnderstood(SoapHeaderCollection headers)
		{
			for (int i = 0; i < headers.Count; i++)
			{
				SoapHeader soapHeader = headers[i];
				if (soapHeader.MustUnderstand && !soapHeader.DidUnderstand)
				{
					throw new SoapHeaderException(Res.GetString("WebCannotUnderstandHeader", new object[] { SoapHeaderHandling.GetHeaderElementName(soapHeader) }), new XmlQualifiedName("MustUnderstand", "http://schemas.xmlsoap.org/soap/envelope/"));
				}
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000CBC0 File Offset: 0x0000ADC0
		private static int FindMapping(SoapHeaderMapping[] mappings, SoapHeader header, SoapHeaderDirection direction)
		{
			if (mappings == null || mappings.Length == 0)
			{
				return -1;
			}
			Type type = header.GetType();
			for (int i = 0; i < mappings.Length; i++)
			{
				SoapHeaderMapping soapHeaderMapping = mappings[i];
				if ((soapHeaderMapping.direction & direction) != (SoapHeaderDirection)0 && soapHeaderMapping.custom && soapHeaderMapping.headerType.IsAssignableFrom(type))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000CC12 File Offset: 0x0000AE12
		private static string GetHeaderElementName(Type headerType)
		{
			return SoapReflector.CreateXmlImporter(null, false).ImportTypeMapping(headerType).XsdElementName;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000CC26 File Offset: 0x0000AE26
		private static string GetHeaderElementName(SoapHeader header)
		{
			if (header is SoapUnknownHeader)
			{
				return ((SoapUnknownHeader)header).Element.LocalName;
			}
			return SoapHeaderHandling.GetHeaderElementName(header.GetType());
		}

		// Token: 0x04000289 RID: 649
		private SoapHeaderCollection unknownHeaders;

		// Token: 0x0400028A RID: 650
		private SoapHeaderCollection unreferencedHeaders;

		// Token: 0x0400028B RID: 651
		private int currentThread;

		// Token: 0x0400028C RID: 652
		private string envelopeNS;
	}
}
