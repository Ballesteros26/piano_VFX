using System;
using System.Globalization;
using System.IO;
using System.Web.Services.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols
{
	// Token: 0x0200004D RID: 77
	internal class RuntimeUtils
	{
		// Token: 0x0600019F RID: 415 RVA: 0x0000210F File Offset: 0x0000030F
		private RuntimeUtils()
		{
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00008338 File Offset: 0x00006538
		internal static XmlDeserializationEvents GetDeserializationEvents()
		{
			return new XmlDeserializationEvents
			{
				OnUnknownElement = new XmlElementEventHandler(RuntimeUtils.OnUnknownElement),
				OnUnknownAttribute = new XmlAttributeEventHandler(RuntimeUtils.OnUnknownAttribute)
			};
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00008374 File Offset: 0x00006574
		private static void OnUnknownAttribute(object sender, XmlAttributeEventArgs e)
		{
			if (e.Attr == null)
			{
				return;
			}
			if (RuntimeUtils.IsKnownNamespace(e.Attr.NamespaceURI))
			{
				return;
			}
			Tracing.OnUnknownAttribute(sender, e);
			if (e.ExpectedAttributes == null)
			{
				throw new InvalidOperationException(Res.GetString("WebUnknownAttribute", new object[]
				{
					e.Attr.Name,
					e.Attr.Value
				}));
			}
			if (e.ExpectedAttributes.Length == 0)
			{
				throw new InvalidOperationException(Res.GetString("WebUnknownAttribute2", new object[]
				{
					e.Attr.Name,
					e.Attr.Value
				}));
			}
			throw new InvalidOperationException(Res.GetString("WebUnknownAttribute3", new object[]
			{
				e.Attr.Name,
				e.Attr.Value,
				e.ExpectedAttributes
			}));
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00008458 File Offset: 0x00006658
		internal static string ElementString(XmlElement element)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			stringWriter.Write("<");
			stringWriter.Write(element.Name);
			if (element.NamespaceURI != null && element.NamespaceURI.Length > 0)
			{
				stringWriter.Write(" xmlns");
				if (element.Prefix != null && element.Prefix.Length > 0)
				{
					stringWriter.Write(":");
					stringWriter.Write(element.Prefix);
				}
				stringWriter.Write("='");
				stringWriter.Write(element.NamespaceURI);
				stringWriter.Write("'");
			}
			stringWriter.Write(">..</");
			stringWriter.Write(element.Name);
			stringWriter.Write(">");
			return stringWriter.ToString();
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00008520 File Offset: 0x00006720
		internal static void OnUnknownElement(object sender, XmlElementEventArgs e)
		{
			if (e.Element == null)
			{
				return;
			}
			string text = RuntimeUtils.ElementString(e.Element);
			Tracing.OnUnknownElement(sender, e);
			if (e.ExpectedElements == null)
			{
				throw new InvalidOperationException(Res.GetString("WebUnknownElement", new object[] { text }));
			}
			if (e.ExpectedElements.Length == 0)
			{
				throw new InvalidOperationException(Res.GetString("WebUnknownElement1", new object[] { text }));
			}
			throw new InvalidOperationException(Res.GetString("WebUnknownElement2", new object[] { text, e.ExpectedElements }));
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000085B4 File Offset: 0x000067B4
		internal static bool IsKnownNamespace(string ns)
		{
			return ns == "http://www.w3.org/2001/XMLSchema-instance" || ns == "http://www.w3.org/XML/1998/namespace" || (ns == "http://schemas.xmlsoap.org/soap/encoding/" || ns == "http://schemas.xmlsoap.org/soap/envelope/") || (ns == "http://www.w3.org/2003/05/soap-envelope" || ns == "http://www.w3.org/2003/05/soap-encoding" || ns == "http://www.w3.org/2003/05/soap-rpc");
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00008625 File Offset: 0x00006825
		internal static string EscapeUri(Uri uri)
		{
			if (null == uri)
			{
				throw new ArgumentNullException("uri");
			}
			return uri.GetComponents(UriComponents.SerializationInfoString, UriFormat.UriEscaped).Replace("#", "%23");
		}
	}
}
