using System;

namespace System.Xml.Schema
{
	// Token: 0x0200038A RID: 906
	internal class BaseProcessor
	{
		// Token: 0x06002497 RID: 9367 RVA: 0x000DE75F File Offset: 0x000DC95F
		public BaseProcessor(XmlNameTable nameTable, SchemaNames schemaNames, ValidationEventHandler eventHandler)
			: this(nameTable, schemaNames, eventHandler, new XmlSchemaCompilationSettings())
		{
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x000DE76F File Offset: 0x000DC96F
		public BaseProcessor(XmlNameTable nameTable, SchemaNames schemaNames, ValidationEventHandler eventHandler, XmlSchemaCompilationSettings compilationSettings)
		{
			this.nameTable = nameTable;
			this.schemaNames = schemaNames;
			this.eventHandler = eventHandler;
			this.compilationSettings = compilationSettings;
			this.NsXml = nameTable.Add("http://www.w3.org/XML/1998/namespace");
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06002499 RID: 9369 RVA: 0x000DE7A5 File Offset: 0x000DC9A5
		protected XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x0600249A RID: 9370 RVA: 0x000DE7AD File Offset: 0x000DC9AD
		protected SchemaNames SchemaNames
		{
			get
			{
				if (this.schemaNames == null)
				{
					this.schemaNames = new SchemaNames(this.nameTable);
				}
				return this.schemaNames;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x0600249B RID: 9371 RVA: 0x000DE7CE File Offset: 0x000DC9CE
		protected ValidationEventHandler EventHandler
		{
			get
			{
				return this.eventHandler;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x0600249C RID: 9372 RVA: 0x000DE7D6 File Offset: 0x000DC9D6
		protected XmlSchemaCompilationSettings CompilationSettings
		{
			get
			{
				return this.compilationSettings;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x0600249D RID: 9373 RVA: 0x000DE7DE File Offset: 0x000DC9DE
		protected bool HasErrors
		{
			get
			{
				return this.errorCount != 0;
			}
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x000DE7EC File Offset: 0x000DC9EC
		protected void AddToTable(XmlSchemaObjectTable table, XmlQualifiedName qname, XmlSchemaObject item)
		{
			if (qname.Name.Length == 0)
			{
				return;
			}
			XmlSchemaObject xmlSchemaObject = table[qname];
			if (xmlSchemaObject == null)
			{
				table.Add(qname, item);
				return;
			}
			if (xmlSchemaObject == item)
			{
				return;
			}
			string text = "The global element '{0}' has already been declared.";
			if (item is XmlSchemaAttributeGroup)
			{
				if (Ref.Equal(this.nameTable.Add(qname.Namespace), this.NsXml))
				{
					XmlSchemaObject xmlSchemaObject2 = Preprocessor.GetBuildInSchema().AttributeGroups[qname];
					if (xmlSchemaObject == xmlSchemaObject2)
					{
						table.Insert(qname, item);
						return;
					}
					if (item == xmlSchemaObject2)
					{
						return;
					}
				}
				else if (this.IsValidAttributeGroupRedefine(xmlSchemaObject, item, table))
				{
					return;
				}
				text = "The attributeGroup '{0}' has already been declared.";
			}
			else if (item is XmlSchemaAttribute)
			{
				if (Ref.Equal(this.nameTable.Add(qname.Namespace), this.NsXml))
				{
					XmlSchemaObject xmlSchemaObject3 = Preprocessor.GetBuildInSchema().Attributes[qname];
					if (xmlSchemaObject == xmlSchemaObject3)
					{
						table.Insert(qname, item);
						return;
					}
					if (item == xmlSchemaObject3)
					{
						return;
					}
				}
				text = "The global attribute '{0}' has already been declared.";
			}
			else if (item is XmlSchemaSimpleType)
			{
				if (this.IsValidTypeRedefine(xmlSchemaObject, item, table))
				{
					return;
				}
				text = "The simpleType '{0}' has already been declared.";
			}
			else if (item is XmlSchemaComplexType)
			{
				if (this.IsValidTypeRedefine(xmlSchemaObject, item, table))
				{
					return;
				}
				text = "The complexType '{0}' has already been declared.";
			}
			else if (item is XmlSchemaGroup)
			{
				if (this.IsValidGroupRedefine(xmlSchemaObject, item, table))
				{
					return;
				}
				text = "The group '{0}' has already been declared.";
			}
			else if (item is XmlSchemaNotation)
			{
				text = "The notation '{0}' has already been declared.";
			}
			else if (item is XmlSchemaIdentityConstraint)
			{
				text = "The identity constraint '{0}' has already been declared.";
			}
			this.SendValidationEvent(text, qname.ToString(), item);
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x000DE95C File Offset: 0x000DCB5C
		private bool IsValidAttributeGroupRedefine(XmlSchemaObject existingObject, XmlSchemaObject item, XmlSchemaObjectTable table)
		{
			XmlSchemaAttributeGroup xmlSchemaAttributeGroup = item as XmlSchemaAttributeGroup;
			XmlSchemaAttributeGroup xmlSchemaAttributeGroup2 = existingObject as XmlSchemaAttributeGroup;
			if (xmlSchemaAttributeGroup2 == xmlSchemaAttributeGroup.Redefined)
			{
				if (xmlSchemaAttributeGroup2.AttributeUses.Count == 0)
				{
					table.Insert(xmlSchemaAttributeGroup.QualifiedName, xmlSchemaAttributeGroup);
					return true;
				}
			}
			else if (xmlSchemaAttributeGroup2.Redefined == xmlSchemaAttributeGroup)
			{
				return true;
			}
			return false;
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x000DE9A8 File Offset: 0x000DCBA8
		private bool IsValidGroupRedefine(XmlSchemaObject existingObject, XmlSchemaObject item, XmlSchemaObjectTable table)
		{
			XmlSchemaGroup xmlSchemaGroup = item as XmlSchemaGroup;
			XmlSchemaGroup xmlSchemaGroup2 = existingObject as XmlSchemaGroup;
			if (xmlSchemaGroup2 == xmlSchemaGroup.Redefined)
			{
				if (xmlSchemaGroup2.CanonicalParticle == null)
				{
					table.Insert(xmlSchemaGroup.QualifiedName, xmlSchemaGroup);
					return true;
				}
			}
			else if (xmlSchemaGroup2.Redefined == xmlSchemaGroup)
			{
				return true;
			}
			return false;
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x000DE9F0 File Offset: 0x000DCBF0
		private bool IsValidTypeRedefine(XmlSchemaObject existingObject, XmlSchemaObject item, XmlSchemaObjectTable table)
		{
			XmlSchemaType xmlSchemaType = item as XmlSchemaType;
			XmlSchemaType xmlSchemaType2 = existingObject as XmlSchemaType;
			if (xmlSchemaType2 == xmlSchemaType.Redefined)
			{
				if (xmlSchemaType2.ElementDecl == null)
				{
					table.Insert(xmlSchemaType.QualifiedName, xmlSchemaType);
					return true;
				}
			}
			else if (xmlSchemaType2.Redefined == xmlSchemaType)
			{
				return true;
			}
			return false;
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x000DEA37 File Offset: 0x000DCC37
		protected void SendValidationEvent(string code, XmlSchemaObject source)
		{
			this.SendValidationEvent(new XmlSchemaException(code, source), XmlSeverityType.Error);
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x000DEA47 File Offset: 0x000DCC47
		protected void SendValidationEvent(string code, string msg, XmlSchemaObject source)
		{
			this.SendValidationEvent(new XmlSchemaException(code, msg, source), XmlSeverityType.Error);
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x000DEA58 File Offset: 0x000DCC58
		protected void SendValidationEvent(string code, string msg1, string msg2, XmlSchemaObject source)
		{
			this.SendValidationEvent(new XmlSchemaException(code, new string[] { msg1, msg2 }, source), XmlSeverityType.Error);
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x000DEA77 File Offset: 0x000DCC77
		protected void SendValidationEvent(string code, string[] args, Exception innerException, XmlSchemaObject source)
		{
			this.SendValidationEvent(new XmlSchemaException(code, args, innerException, source.SourceUri, source.LineNumber, source.LinePosition, source), XmlSeverityType.Error);
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x000DEA9F File Offset: 0x000DCC9F
		protected void SendValidationEvent(string code, string msg1, string msg2, string sourceUri, int lineNumber, int linePosition)
		{
			this.SendValidationEvent(new XmlSchemaException(code, new string[] { msg1, msg2 }, sourceUri, lineNumber, linePosition), XmlSeverityType.Error);
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x000DEAC2 File Offset: 0x000DCCC2
		protected void SendValidationEvent(string code, XmlSchemaObject source, XmlSeverityType severity)
		{
			this.SendValidationEvent(new XmlSchemaException(code, source), severity);
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x000DEAD2 File Offset: 0x000DCCD2
		protected void SendValidationEvent(XmlSchemaException e)
		{
			this.SendValidationEvent(e, XmlSeverityType.Error);
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x000DEADC File Offset: 0x000DCCDC
		protected void SendValidationEvent(string code, string msg, XmlSchemaObject source, XmlSeverityType severity)
		{
			this.SendValidationEvent(new XmlSchemaException(code, msg, source), severity);
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x000DEAEE File Offset: 0x000DCCEE
		protected void SendValidationEvent(XmlSchemaException e, XmlSeverityType severity)
		{
			if (severity == XmlSeverityType.Error)
			{
				this.errorCount++;
			}
			if (this.eventHandler != null)
			{
				this.eventHandler(null, new ValidationEventArgs(e, severity));
				return;
			}
			if (severity == XmlSeverityType.Error)
			{
				throw e;
			}
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x000DEB22 File Offset: 0x000DCD22
		protected void SendValidationEventNoThrow(XmlSchemaException e, XmlSeverityType severity)
		{
			if (severity == XmlSeverityType.Error)
			{
				this.errorCount++;
			}
			if (this.eventHandler != null)
			{
				this.eventHandler(null, new ValidationEventArgs(e, severity));
			}
		}

		// Token: 0x040018DD RID: 6365
		private XmlNameTable nameTable;

		// Token: 0x040018DE RID: 6366
		private SchemaNames schemaNames;

		// Token: 0x040018DF RID: 6367
		private ValidationEventHandler eventHandler;

		// Token: 0x040018E0 RID: 6368
		private XmlSchemaCompilationSettings compilationSettings;

		// Token: 0x040018E1 RID: 6369
		private int errorCount;

		// Token: 0x040018E2 RID: 6370
		private string NsXml;
	}
}
