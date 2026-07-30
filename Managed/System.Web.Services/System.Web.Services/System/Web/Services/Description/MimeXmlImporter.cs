using System;
using System.CodeDom;
using System.Data;
using System.Data.Design;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	// Token: 0x020000DE RID: 222
	internal class MimeXmlImporter : MimeImporter
	{
		// Token: 0x0600059E RID: 1438 RVA: 0x00006C2F File Offset: 0x00004E2F
		internal override MimeParameterCollection ImportParameters()
		{
			return null;
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00019224 File Offset: 0x00017424
		internal override MimeReturn ImportReturn()
		{
			MimeContentBinding mimeContentBinding = (MimeContentBinding)base.ImportContext.OperationBinding.Output.Extensions.Find(typeof(MimeContentBinding));
			if (mimeContentBinding != null)
			{
				if (!ContentType.MatchesBase(mimeContentBinding.Type, "text/xml"))
				{
					return null;
				}
				return new MimeReturn
				{
					TypeName = typeof(XmlElement).FullName,
					ReaderType = typeof(XmlReturnReader)
				};
			}
			else
			{
				MimeXmlBinding mimeXmlBinding = (MimeXmlBinding)base.ImportContext.OperationBinding.Output.Extensions.Find(typeof(MimeXmlBinding));
				if (mimeXmlBinding == null)
				{
					return null;
				}
				MimeXmlReturn mimeXmlReturn = new MimeXmlReturn();
				int count = base.ImportContext.OutputMessage.Parts.Count;
				if (count != 0)
				{
					MessagePart messagePart;
					if (count != 1)
					{
						messagePart = base.ImportContext.OutputMessage.FindPartByName(mimeXmlBinding.Part);
					}
					else if (mimeXmlBinding.Part == null || mimeXmlBinding.Part.Length == 0)
					{
						messagePart = base.ImportContext.OutputMessage.Parts[0];
					}
					else
					{
						messagePart = base.ImportContext.OutputMessage.FindPartByName(mimeXmlBinding.Part);
					}
					mimeXmlReturn.TypeMapping = this.Importer.ImportTypeMapping(messagePart.Element);
					mimeXmlReturn.TypeName = mimeXmlReturn.TypeMapping.TypeFullName;
					mimeXmlReturn.ReaderType = typeof(XmlReturnReader);
					this.Exporter.AddMappingMetadata(mimeXmlReturn.Attributes, mimeXmlReturn.TypeMapping, string.Empty);
					return mimeXmlReturn;
				}
				throw new InvalidOperationException(Res.GetString("MessageHasNoParts1", new object[] { base.ImportContext.InputMessage.Name }));
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x000193D4 File Offset: 0x000175D4
		private XmlSchemaImporter Importer
		{
			get
			{
				if (this.importer == null)
				{
					this.importer = new XmlSchemaImporter(base.ImportContext.ConcreteSchemas, base.ImportContext.ServiceImporter.CodeGenerationOptions, base.ImportContext.ServiceImporter.CodeGenerator, base.ImportContext.ImportContext);
					foreach (Type type in base.ImportContext.ServiceImporter.Extensions)
					{
						this.importer.Extensions.Add(type.FullName, type);
					}
					this.importer.Extensions.Add(new TypedDataSetSchemaImporterExtension());
					this.importer.Extensions.Add(new DataSetSchemaImporterExtension());
				}
				return this.importer;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x000194C0 File Offset: 0x000176C0
		private XmlCodeExporter Exporter
		{
			get
			{
				if (this.exporter == null)
				{
					this.exporter = new XmlCodeExporter(base.ImportContext.CodeNamespace, base.ImportContext.ServiceImporter.CodeCompileUnit, base.ImportContext.ServiceImporter.CodeGenerator, base.ImportContext.ServiceImporter.CodeGenerationOptions, base.ImportContext.ExportContext);
				}
				return this.exporter;
			}
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0001952C File Offset: 0x0001772C
		internal override void GenerateCode(MimeReturn[] importedReturns, MimeParameterCollection[] importedParameters)
		{
			for (int i = 0; i < importedReturns.Length; i++)
			{
				if (importedReturns[i] is MimeXmlReturn)
				{
					this.GenerateCode((MimeXmlReturn)importedReturns[i]);
				}
			}
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0001955F File Offset: 0x0001775F
		private void GenerateCode(MimeXmlReturn importedReturn)
		{
			this.Exporter.ExportTypeMapping(importedReturn.TypeMapping);
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00019574 File Offset: 0x00017774
		internal override void AddClassMetadata(CodeTypeDeclaration codeClass)
		{
			foreach (object obj in this.Exporter.IncludeMetadata)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = (CodeAttributeDeclaration)obj;
				codeClass.CustomAttributes.Add(codeAttributeDeclaration);
			}
		}

		// Token: 0x040003A0 RID: 928
		private XmlSchemaImporter importer;

		// Token: 0x040003A1 RID: 929
		private XmlCodeExporter exporter;
	}
}
