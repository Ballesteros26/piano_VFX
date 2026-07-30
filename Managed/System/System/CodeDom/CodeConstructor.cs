using System;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for an instance constructor of a type.</summary>
	// Token: 0x02000765 RID: 1893
	[Serializable]
	public class CodeConstructor : CodeMemberMethod
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeConstructor" /> class.</summary>
		// Token: 0x06003C1F RID: 15391 RVA: 0x000D8D52 File Offset: 0x000D6F52
		public CodeConstructor()
		{
			base.Name = ".ctor";
		}

		/// <summary>Gets the collection of base constructor arguments.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that contains the base constructor arguments.</returns>
		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06003C20 RID: 15392 RVA: 0x000D8D7B File Offset: 0x000D6F7B
		public CodeExpressionCollection BaseConstructorArgs { get; } = new CodeExpressionCollection();

		/// <summary>Gets the collection of chained constructor arguments.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that contains the chained constructor arguments.</returns>
		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06003C21 RID: 15393 RVA: 0x000D8D83 File Offset: 0x000D6F83
		public CodeExpressionCollection ChainedConstructorArgs { get; } = new CodeExpressionCollection();
	}
}
