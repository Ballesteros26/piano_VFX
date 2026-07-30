using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to an index of an array.</summary>
	// Token: 0x02000752 RID: 1874
	[Serializable]
	public class CodeArrayIndexerExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayIndexerExpression" /> class.</summary>
		// Token: 0x06003B8D RID: 15245 RVA: 0x000D82AC File Offset: 0x000D64AC
		public CodeArrayIndexerExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayIndexerExpression" /> class using the specified target object and indexes.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the array the indexer targets. </param>
		/// <param name="indices">The index or indexes to reference. </param>
		// Token: 0x06003B8E RID: 15246 RVA: 0x000D84A5 File Offset: 0x000D66A5
		public CodeArrayIndexerExpression(CodeExpression targetObject, params CodeExpression[] indices)
		{
			this.TargetObject = targetObject;
			this.Indices.AddRange(indices);
		}

		/// <summary>Gets or sets the target object of the array indexer.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that represents the array being indexed.</returns>
		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x06003B8F RID: 15247 RVA: 0x000D84C0 File Offset: 0x000D66C0
		// (set) Token: 0x06003B90 RID: 15248 RVA: 0x000D84C8 File Offset: 0x000D66C8
		public CodeExpression TargetObject { get; set; }

		/// <summary>Gets or sets the index or indexes of the indexer expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the index or indexes of the indexer expression.</returns>
		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x06003B91 RID: 15249 RVA: 0x000D84D4 File Offset: 0x000D66D4
		public CodeExpressionCollection Indices
		{
			get
			{
				CodeExpressionCollection codeExpressionCollection;
				if ((codeExpressionCollection = this._indices) == null)
				{
					codeExpressionCollection = (this._indices = new CodeExpressionCollection());
				}
				return codeExpressionCollection;
			}
		}

		// Token: 0x04002D54 RID: 11604
		private CodeExpressionCollection _indices;
	}
}
