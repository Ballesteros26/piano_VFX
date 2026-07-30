using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Configuration;
using System.Security.Permissions;
using System.Xml.Serialization.Advanced;
using System.Xml.Serialization.Configuration;
using Microsoft.CSharp;
using Unity;

namespace System.Xml.Serialization
{
	/// <summary>Describes a schema importer.</summary>
	// Token: 0x02000304 RID: 772
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class SchemaImporter
	{
		// Token: 0x06001CC9 RID: 7369 RVA: 0x0009CDD4 File Offset: 0x0009AFD4
		internal SchemaImporter(XmlSchemas schemas, CodeGenerationOptions options, CodeDomProvider codeProvider, ImportContext context)
		{
			if (!schemas.Contains("http://www.w3.org/2001/XMLSchema"))
			{
				schemas.AddReference(XmlSchemas.XsdSchema);
				schemas.SchemaSet.Add(XmlSchemas.XsdSchema);
			}
			if (!schemas.Contains("http://www.w3.org/XML/1998/namespace"))
			{
				schemas.AddReference(XmlSchemas.XmlSchema);
				schemas.SchemaSet.Add(XmlSchemas.XmlSchema);
			}
			this.schemas = schemas;
			this.options = options;
			this.codeProvider = codeProvider;
			this.context = context;
			this.Schemas.SetCache(this.Context.Cache, this.Context.ShareTypes);
			SchemaImporterExtensionsSection schemaImporterExtensionsSection = PrivilegedConfigurationManager.GetSection(ConfigurationStrings.SchemaImporterExtensionsSectionPath) as SchemaImporterExtensionsSection;
			if (schemaImporterExtensionsSection != null)
			{
				this.extensions = schemaImporterExtensionsSection.SchemaImporterExtensionsInternal;
				return;
			}
			this.extensions = new SchemaImporterExtensionCollection();
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001CCA RID: 7370 RVA: 0x0009CEA2 File Offset: 0x0009B0A2
		internal ImportContext Context
		{
			get
			{
				if (this.context == null)
				{
					this.context = new ImportContext();
				}
				return this.context;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001CCB RID: 7371 RVA: 0x0009CEBD File Offset: 0x0009B0BD
		internal CodeDomProvider CodeProvider
		{
			get
			{
				if (this.codeProvider == null)
				{
					this.codeProvider = new CSharpCodeProvider();
				}
				return this.codeProvider;
			}
		}

		/// <summary>Gets a collection of schema importer extensions.</summary>
		/// <returns>A <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionElementCollection" /> containing a collection of schema importer extensions.</returns>
		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001CCC RID: 7372 RVA: 0x0009CED8 File Offset: 0x0009B0D8
		public SchemaImporterExtensionCollection Extensions
		{
			get
			{
				if (this.extensions == null)
				{
					this.extensions = new SchemaImporterExtensionCollection();
				}
				return this.extensions;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001CCD RID: 7373 RVA: 0x0009CEF3 File Offset: 0x0009B0F3
		internal Hashtable ImportedElements
		{
			get
			{
				return this.Context.Elements;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001CCE RID: 7374 RVA: 0x0009CF00 File Offset: 0x0009B100
		internal Hashtable ImportedMappings
		{
			get
			{
				return this.Context.Mappings;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001CCF RID: 7375 RVA: 0x0009CF0D File Offset: 0x0009B10D
		internal CodeIdentifiers TypeIdentifiers
		{
			get
			{
				return this.Context.TypeIdentifiers;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x0009CF1A File Offset: 0x0009B11A
		internal XmlSchemas Schemas
		{
			get
			{
				if (this.schemas == null)
				{
					this.schemas = new XmlSchemas();
				}
				return this.schemas;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001CD1 RID: 7377 RVA: 0x0009CF35 File Offset: 0x0009B135
		internal TypeScope Scope
		{
			get
			{
				if (this.scope == null)
				{
					this.scope = new TypeScope();
				}
				return this.scope;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x0009CF50 File Offset: 0x0009B150
		internal NameTable GroupsInUse
		{
			get
			{
				if (this.groupsInUse == null)
				{
					this.groupsInUse = new NameTable();
				}
				return this.groupsInUse;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x0009CF6B File Offset: 0x0009B16B
		internal NameTable TypesInUse
		{
			get
			{
				if (this.typesInUse == null)
				{
					this.typesInUse = new NameTable();
				}
				return this.typesInUse;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001CD4 RID: 7380 RVA: 0x0009CF86 File Offset: 0x0009B186
		internal CodeGenerationOptions Options
		{
			get
			{
				return this.options;
			}
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x0009CF90 File Offset: 0x0009B190
		internal void MakeDerived(StructMapping structMapping, Type baseType, bool baseTypeCanBeIndirect)
		{
			structMapping.ReferencedByTopLevelElement = true;
			if (baseType != null)
			{
				TypeDesc typeDesc = this.Scope.GetTypeDesc(baseType);
				if (typeDesc != null)
				{
					TypeDesc typeDesc2 = structMapping.TypeDesc;
					if (baseTypeCanBeIndirect)
					{
						while (typeDesc2.BaseTypeDesc != null && typeDesc2.BaseTypeDesc != typeDesc)
						{
							typeDesc2 = typeDesc2.BaseTypeDesc;
						}
					}
					if (typeDesc2.BaseTypeDesc != null && typeDesc2.BaseTypeDesc != typeDesc)
					{
						throw new InvalidOperationException(Res.GetString("Type {0} cannot derive from {1} because it already has base type {2}.", new object[]
						{
							structMapping.TypeDesc.FullName,
							baseType.FullName,
							typeDesc2.BaseTypeDesc.FullName
						}));
					}
					typeDesc2.BaseTypeDesc = typeDesc;
				}
			}
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x0009D037 File Offset: 0x0009B237
		internal string GenerateUniqueTypeName(string typeName)
		{
			typeName = CodeIdentifier.MakeValid(typeName);
			return this.TypeIdentifiers.AddUnique(typeName, typeName);
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x0009D050 File Offset: 0x0009B250
		private StructMapping CreateRootMapping()
		{
			TypeDesc typeDesc = this.Scope.GetTypeDesc(typeof(object));
			return new StructMapping
			{
				TypeDesc = typeDesc,
				Members = new MemberMapping[0],
				IncludeInSchema = false,
				TypeName = "anyType",
				Namespace = "http://www.w3.org/2001/XMLSchema"
			};
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x0009D0A8 File Offset: 0x0009B2A8
		internal StructMapping GetRootMapping()
		{
			if (this.root == null)
			{
				this.root = this.CreateRootMapping();
			}
			return this.root;
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x0009D0C4 File Offset: 0x0009B2C4
		internal StructMapping ImportRootMapping()
		{
			if (!this.rootImported)
			{
				this.rootImported = true;
				this.ImportDerivedTypes(XmlQualifiedName.Empty);
			}
			return this.GetRootMapping();
		}

		// Token: 0x06001CDA RID: 7386
		internal abstract void ImportDerivedTypes(XmlQualifiedName baseName);

		// Token: 0x06001CDB RID: 7387 RVA: 0x0009D0E8 File Offset: 0x0009B2E8
		internal void AddReference(XmlQualifiedName name, NameTable references, string error)
		{
			if (name.Namespace == "http://www.w3.org/2001/XMLSchema")
			{
				return;
			}
			if (references[name] != null)
			{
				throw new InvalidOperationException(Res.GetString(error, new object[] { name.Name, name.Namespace }));
			}
			references[name] = name;
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x0009D13D File Offset: 0x0009B33D
		internal void RemoveReference(XmlQualifiedName name, NameTable references)
		{
			references[name] = null;
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x0009D147 File Offset: 0x0009B347
		internal void AddReservedIdentifiersForDataBinding(CodeIdentifiers scope)
		{
			if ((this.options & CodeGenerationOptions.EnableDataBinding) != CodeGenerationOptions.None)
			{
				scope.AddReserved(CodeExporter.PropertyChangedEvent.Name);
				scope.AddReserved(CodeExporter.RaisePropertyChangedEventMethod.Name);
			}
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x000728B0 File Offset: 0x00070AB0
		internal SchemaImporter()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001679 RID: 5753
		private XmlSchemas schemas;

		// Token: 0x0400167A RID: 5754
		private StructMapping root;

		// Token: 0x0400167B RID: 5755
		private CodeGenerationOptions options;

		// Token: 0x0400167C RID: 5756
		private CodeDomProvider codeProvider;

		// Token: 0x0400167D RID: 5757
		private TypeScope scope;

		// Token: 0x0400167E RID: 5758
		private ImportContext context;

		// Token: 0x0400167F RID: 5759
		private bool rootImported;

		// Token: 0x04001680 RID: 5760
		private NameTable typesInUse;

		// Token: 0x04001681 RID: 5761
		private NameTable groupsInUse;

		// Token: 0x04001682 RID: 5762
		private SchemaImporterExtensionCollection extensions;
	}
}
