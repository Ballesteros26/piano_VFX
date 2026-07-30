using System;
using System.CodeDom;
using System.Security.Permissions;

namespace System.Web.Services.Description
{
	/// <summary>Provides a common interface and functionality for classes to generate code attributes that specify SOAP extensions.</summary>
	// Token: 0x0200011D RID: 285
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class SoapExtensionImporter
	{
		/// <summary>When overridden in a derived class, adds code attribute declarations to any method that represents an operation in a binding.</summary>
		/// <param name="metadata">A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> into which the <see cref="M:System.Web.Services.Description.SoapExtensionImporter.ImportMethod(System.CodeDom.CodeAttributeDeclarationCollection)" />  method can place new <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> instances.</param>
		// Token: 0x0600089A RID: 2202
		public abstract void ImportMethod(CodeAttributeDeclarationCollection metadata);

		/// <summary>Gets or sets the <see cref="T:System.Web.Services.Description.SoapProtocolImporter" /> instance that invokes the <see cref="M:System.Web.Services.Description.SoapExtensionImporter.ImportMethod(System.CodeDom.CodeAttributeDeclarationCollection)" /> method.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Description.SoapProtocolImporter" /> instance that invokes the <see cref="M:System.Web.Services.Description.SoapExtensionImporter.ImportMethod(System.CodeDom.CodeAttributeDeclarationCollection)" /> method.</returns>
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x0003C3EB File Offset: 0x0003A5EB
		// (set) Token: 0x0600089C RID: 2204 RVA: 0x0003C3F3 File Offset: 0x0003A5F3
		public SoapProtocolImporter ImportContext
		{
			get
			{
				return this.protocolImporter;
			}
			set
			{
				this.protocolImporter = value;
			}
		}

		// Token: 0x04000527 RID: 1319
		private SoapProtocolImporter protocolImporter;
	}
}
