using System;
using System.CodeDom.Compiler;
using Unity;

namespace System.Xml.Serialization
{
	/// <summary>Maps a code entity in a .NET Framework Web service method to an element in a Web Services Description Language (WSDL) message.</summary>
	// Token: 0x02000337 RID: 823
	public class XmlMemberMapping
	{
		// Token: 0x06001F77 RID: 8055 RVA: 0x000A9E1D File Offset: 0x000A801D
		internal XmlMemberMapping(MemberMapping mapping)
		{
			this.mapping = mapping;
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001F78 RID: 8056 RVA: 0x000A9E2C File Offset: 0x000A802C
		internal MemberMapping Mapping
		{
			get
			{
				return this.mapping;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001F79 RID: 8057 RVA: 0x000A9E34 File Offset: 0x000A8034
		internal Accessor Accessor
		{
			get
			{
				return this.mapping.Accessor;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the .NET Framework type maps to an XML element or attribute of any type. </summary>
		/// <returns>true, if the type maps to an XML any element or attribute; otherwise, false.</returns>
		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001F7A RID: 8058 RVA: 0x000A9E41 File Offset: 0x000A8041
		public bool Any
		{
			get
			{
				return this.Accessor.Any;
			}
		}

		/// <summary>Gets the unqualified name of the XML element declaration that applies to this mapping. </summary>
		/// <returns>The unqualified name of the XML element declaration that applies to this mapping.</returns>
		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001F7B RID: 8059 RVA: 0x000A9E4E File Offset: 0x000A804E
		public string ElementName
		{
			get
			{
				return Accessor.UnescapeName(this.Accessor.Name);
			}
		}

		/// <summary>Gets the XML element name as it appears in the service description document.</summary>
		/// <returns>The XML element name.</returns>
		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001F7C RID: 8060 RVA: 0x000A9E60 File Offset: 0x000A8060
		public string XsdElementName
		{
			get
			{
				return this.Accessor.Name;
			}
		}

		/// <summary>Gets the XML namespace that applies to this mapping. </summary>
		/// <returns>The XML namespace that applies to this mapping.</returns>
		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001F7D RID: 8061 RVA: 0x000A9E6D File Offset: 0x000A806D
		public string Namespace
		{
			get
			{
				return this.Accessor.Namespace;
			}
		}

		/// <summary>Gets the name of the Web service method member that is represented by this mapping. </summary>
		/// <returns>The name of the Web service method member represented by this mapping.</returns>
		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001F7E RID: 8062 RVA: 0x000A9E7A File Offset: 0x000A807A
		public string MemberName
		{
			get
			{
				return this.mapping.Name;
			}
		}

		/// <summary>Gets the type name of the .NET Framework type for this mapping. </summary>
		/// <returns>The type name of the .NET Framework type for this mapping.</returns>
		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001F7F RID: 8063 RVA: 0x000A9E87 File Offset: 0x000A8087
		public string TypeName
		{
			get
			{
				if (this.Accessor.Mapping == null)
				{
					return string.Empty;
				}
				return this.Accessor.Mapping.TypeName;
			}
		}

		/// <summary>Gets the namespace of the .NET Framework type for this mapping.</summary>
		/// <returns>The namespace of the .NET Framework type for this mapping.</returns>
		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001F80 RID: 8064 RVA: 0x000A9EAC File Offset: 0x000A80AC
		public string TypeNamespace
		{
			get
			{
				if (this.Accessor.Mapping == null)
				{
					return null;
				}
				return this.Accessor.Mapping.Namespace;
			}
		}

		/// <summary>Gets the fully qualified type name of the .NET Framework type for this mapping. </summary>
		/// <returns>The fully qualified type name of the .NET Framework type for this mapping.</returns>
		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001F81 RID: 8065 RVA: 0x000A9ECD File Offset: 0x000A80CD
		public string TypeFullName
		{
			get
			{
				return this.mapping.TypeDesc.FullName;
			}
		}

		/// <summary>Gets a value that indicates whether the accompanying field in the .NET Framework type has a value specified.</summary>
		/// <returns>true, if the accompanying field has a value specified; otherwise, false.</returns>
		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001F82 RID: 8066 RVA: 0x000A9EDF File Offset: 0x000A80DF
		public bool CheckSpecified
		{
			get
			{
				return this.mapping.CheckSpecified > SpecifiedAccessor.None;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001F83 RID: 8067 RVA: 0x000A9EEF File Offset: 0x000A80EF
		internal bool IsNullable
		{
			get
			{
				return this.mapping.IsNeedNullable;
			}
		}

		/// <summary>Returns the name of the type associated with the specified <see cref="T:System.CodeDom.Compiler.CodeDomProvider" />.</summary>
		/// <returns>The name of the type.</returns>
		/// <param name="codeProvider">A <see cref="T:System.CodeDom.Compiler.CodeDomProvider" />  that contains the name of the type.</param>
		// Token: 0x06001F84 RID: 8068 RVA: 0x000A9EFC File Offset: 0x000A80FC
		public string GenerateTypeName(CodeDomProvider codeProvider)
		{
			return this.mapping.GetTypeName(codeProvider);
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x000728B0 File Offset: 0x00070AB0
		internal XmlMemberMapping()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001748 RID: 5960
		private MemberMapping mapping;
	}
}
