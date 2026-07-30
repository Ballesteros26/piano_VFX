using System;
using System.Reflection;

namespace System.CodeDom
{
	/// <summary>Represents a delegate declaration.</summary>
	// Token: 0x02000796 RID: 1942
	[Serializable]
	public class CodeTypeDelegate : CodeTypeDeclaration
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeDelegate" /> class.</summary>
		// Token: 0x06003D7F RID: 15743 RVA: 0x000DA740 File Offset: 0x000D8940
		public CodeTypeDelegate()
		{
			base.TypeAttributes &= ~TypeAttributes.ClassSemanticsMask;
			base.TypeAttributes |= TypeAttributes.NotPublic;
			base.BaseTypes.Clear();
			base.BaseTypes.Add(new CodeTypeReference("System.Delegate"));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeDelegate" /> class.</summary>
		/// <param name="name">The name of the delegate. </param>
		// Token: 0x06003D80 RID: 15744 RVA: 0x000DA79C File Offset: 0x000D899C
		public CodeTypeDelegate(string name)
			: this()
		{
			base.Name = name;
		}

		/// <summary>Gets or sets the return type of the delegate.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the return type of the delegate.</returns>
		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x06003D81 RID: 15745 RVA: 0x000DA7AC File Offset: 0x000D89AC
		// (set) Token: 0x06003D82 RID: 15746 RVA: 0x000DA7D6 File Offset: 0x000D89D6
		public CodeTypeReference ReturnType
		{
			get
			{
				CodeTypeReference codeTypeReference;
				if ((codeTypeReference = this._returnType) == null)
				{
					codeTypeReference = (this._returnType = new CodeTypeReference(""));
				}
				return codeTypeReference;
			}
			set
			{
				this._returnType = value;
			}
		}

		/// <summary>Gets the parameters of the delegate.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> that indicates the parameters of the delegate.</returns>
		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x06003D83 RID: 15747 RVA: 0x000DA7DF File Offset: 0x000D89DF
		public CodeParameterDeclarationExpressionCollection Parameters { get; } = new CodeParameterDeclarationExpressionCollection();

		// Token: 0x04002DF7 RID: 11767
		private CodeTypeReference _returnType;
	}
}
