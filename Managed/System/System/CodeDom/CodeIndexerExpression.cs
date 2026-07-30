using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to an indexer property of an object.</summary>
	// Token: 0x02000773 RID: 1907
	[Serializable]
	public class CodeIndexerExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeIndexerExpression" /> class.</summary>
		// Token: 0x06003C6B RID: 15467 RVA: 0x000D82AC File Offset: 0x000D64AC
		public CodeIndexerExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeIndexerExpression" /> class using the specified target object and index.</summary>
		/// <param name="targetObject">The target object. </param>
		/// <param name="indices">The index or indexes of the indexer expression. </param>
		// Token: 0x06003C6C RID: 15468 RVA: 0x000D9110 File Offset: 0x000D7310
		public CodeIndexerExpression(CodeExpression targetObject, params CodeExpression[] indices)
		{
			this.TargetObject = targetObject;
			this.Indices.AddRange(indices);
		}

		/// <summary>Gets or sets the target object that can be indexed.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the indexer object.</returns>
		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x06003C6D RID: 15469 RVA: 0x000D912B File Offset: 0x000D732B
		// (set) Token: 0x06003C6E RID: 15470 RVA: 0x000D9133 File Offset: 0x000D7333
		public CodeExpression TargetObject { get; set; }

		/// <summary>Gets the collection of indexes of the indexer expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the index or indexes of the indexer expression.</returns>
		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x06003C6F RID: 15471 RVA: 0x000D913C File Offset: 0x000D733C
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

		// Token: 0x04002D97 RID: 11671
		private CodeExpressionCollection _indices;
	}
}
