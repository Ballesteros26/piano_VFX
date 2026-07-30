using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	// Token: 0x02000136 RID: 310
	internal class WebReferenceOptionsSerializationWriter : XmlSerializationWriter
	{
		// Token: 0x06000973 RID: 2419 RVA: 0x000410D4 File Offset: 0x0003F2D4
		private string Write1_CodeGenerationOptions(CodeGenerationOptions v)
		{
			switch (v)
			{
			case CodeGenerationOptions.GenerateProperties:
				return "properties";
			case CodeGenerationOptions.GenerateNewAsync:
				return "newAsync";
			case CodeGenerationOptions.GenerateProperties | CodeGenerationOptions.GenerateNewAsync:
				break;
			case CodeGenerationOptions.GenerateOldAsync:
				return "oldAsync";
			default:
				if (v == CodeGenerationOptions.GenerateOrder)
				{
					return "order";
				}
				if (v == CodeGenerationOptions.EnableDataBinding)
				{
					return "enableDataBinding";
				}
				break;
			}
			return XmlSerializationWriter.FromEnum((long)v, new string[] { "properties", "newAsync", "oldAsync", "order", "enableDataBinding" }, new long[] { 1L, 2L, 4L, 8L, 16L }, "System.Xml.Serialization.CodeGenerationOptions");
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0004117C File Offset: 0x0003F37C
		private string Write2_ServiceDescriptionImportStyle(ServiceDescriptionImportStyle v)
		{
			string text;
			switch (v)
			{
			case ServiceDescriptionImportStyle.Client:
				text = "client";
				break;
			case ServiceDescriptionImportStyle.Server:
				text = "server";
				break;
			case ServiceDescriptionImportStyle.ServerInterface:
				text = "serverInterface";
				break;
			default:
				throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "System.Web.Services.Description.ServiceDescriptionImportStyle");
			}
			return text;
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x000411D4 File Offset: 0x0003F3D4
		private void Write4_WebReferenceOptions(string n, string ns, WebReferenceOptions o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType && !(o.GetType() == typeof(WebReferenceOptions)))
			{
				throw base.CreateUnknownTypeException(o);
			}
			base.EscapeName = false;
			base.WriteStartElement(n, ns, o);
			if (needType)
			{
				base.WriteXsiType("webReferenceOptions", "http://microsoft.com/webReference/");
			}
			if (o.CodeGenerationOptions != CodeGenerationOptions.GenerateOldAsync)
			{
				base.WriteElementString("codeGenerationOptions", "http://microsoft.com/webReference/", this.Write1_CodeGenerationOptions(o.CodeGenerationOptions));
			}
			StringCollection schemaImporterExtensions = o.SchemaImporterExtensions;
			if (schemaImporterExtensions != null)
			{
				base.WriteStartElement("schemaImporterExtensions", "http://microsoft.com/webReference/");
				for (int i = 0; i < schemaImporterExtensions.Count; i++)
				{
					base.WriteNullableStringLiteral("type", "http://microsoft.com/webReference/", schemaImporterExtensions[i]);
				}
				base.WriteEndElement();
			}
			if (o.Style != ServiceDescriptionImportStyle.Client)
			{
				base.WriteElementString("style", "http://microsoft.com/webReference/", this.Write2_ServiceDescriptionImportStyle(o.Style));
			}
			base.WriteElementStringRaw("verbose", "http://microsoft.com/webReference/", XmlConvert.ToString(o.Verbose));
			base.WriteEndElement(o);
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0000210D File Offset: 0x0000030D
		protected override void InitCallbacks()
		{
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x000412EB File Offset: 0x0003F4EB
		internal void Write5_webReferenceOptions(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteNullTagLiteral("webReferenceOptions", "http://microsoft.com/webReference/");
				return;
			}
			base.TopLevelElement();
			this.Write4_WebReferenceOptions("webReferenceOptions", "http://microsoft.com/webReference/", (WebReferenceOptions)o, true, false);
		}
	}
}
