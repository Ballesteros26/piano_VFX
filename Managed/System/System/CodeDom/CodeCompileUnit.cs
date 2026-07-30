using System;
using System.Collections.Specialized;

namespace System.CodeDom
{
	/// <summary>Provides a container for a CodeDOM program graph.</summary>
	// Token: 0x02000763 RID: 1891
	[Serializable]
	public class CodeCompileUnit : CodeObject
	{
		/// <summary>Gets the collection of namespaces.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeNamespaceCollection" /> that indicates the namespaces that the compile unit uses.</returns>
		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x06003C13 RID: 15379 RVA: 0x000D8BFF File Offset: 0x000D6DFF
		public CodeNamespaceCollection Namespaces { get; } = new CodeNamespaceCollection();

		/// <summary>Gets the referenced assemblies.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> that contains the file names of the referenced assemblies.</returns>
		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x06003C14 RID: 15380 RVA: 0x000D8C08 File Offset: 0x000D6E08
		public StringCollection ReferencedAssemblies
		{
			get
			{
				StringCollection stringCollection;
				if ((stringCollection = this._assemblies) == null)
				{
					stringCollection = (this._assemblies = new StringCollection());
				}
				return stringCollection;
			}
		}

		/// <summary>Gets a collection of custom attributes for the generated assembly.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the custom attributes for the generated assembly.</returns>
		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x06003C15 RID: 15381 RVA: 0x000D8C30 File Offset: 0x000D6E30
		public CodeAttributeDeclarationCollection AssemblyCustomAttributes
		{
			get
			{
				CodeAttributeDeclarationCollection codeAttributeDeclarationCollection;
				if ((codeAttributeDeclarationCollection = this._attributes) == null)
				{
					codeAttributeDeclarationCollection = (this._attributes = new CodeAttributeDeclarationCollection());
				}
				return codeAttributeDeclarationCollection;
			}
		}

		/// <summary>Gets a <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing start directives.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing start directives.</returns>
		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06003C16 RID: 15382 RVA: 0x000D8C58 File Offset: 0x000D6E58
		public CodeDirectiveCollection StartDirectives
		{
			get
			{
				CodeDirectiveCollection codeDirectiveCollection;
				if ((codeDirectiveCollection = this._startDirectives) == null)
				{
					codeDirectiveCollection = (this._startDirectives = new CodeDirectiveCollection());
				}
				return codeDirectiveCollection;
			}
		}

		/// <summary>Gets a <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing end directives.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing end directives.</returns>
		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06003C17 RID: 15383 RVA: 0x000D8C80 File Offset: 0x000D6E80
		public CodeDirectiveCollection EndDirectives
		{
			get
			{
				CodeDirectiveCollection codeDirectiveCollection;
				if ((codeDirectiveCollection = this._endDirectives) == null)
				{
					codeDirectiveCollection = (this._endDirectives = new CodeDirectiveCollection());
				}
				return codeDirectiveCollection;
			}
		}

		// Token: 0x04002D7F RID: 11647
		private StringCollection _assemblies;

		// Token: 0x04002D80 RID: 11648
		private CodeAttributeDeclarationCollection _attributes;

		// Token: 0x04002D81 RID: 11649
		private CodeDirectiveCollection _startDirectives;

		// Token: 0x04002D82 RID: 11650
		private CodeDirectiveCollection _endDirectives;
	}
}
