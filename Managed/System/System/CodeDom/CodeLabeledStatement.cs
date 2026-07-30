using System;

namespace System.CodeDom
{
	/// <summary>Represents a labeled statement or a stand-alone label.</summary>
	// Token: 0x02000775 RID: 1909
	[Serializable]
	public class CodeLabeledStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeLabeledStatement" /> class.</summary>
		// Token: 0x06003C79 RID: 15481 RVA: 0x000D84F9 File Offset: 0x000D66F9
		public CodeLabeledStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeLabeledStatement" /> class using the specified label name.</summary>
		/// <param name="label">The name of the label. </param>
		// Token: 0x06003C7A RID: 15482 RVA: 0x000D91E4 File Offset: 0x000D73E4
		public CodeLabeledStatement(string label)
		{
			this._label = label;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeLabeledStatement" /> class using the specified label name and statement.</summary>
		/// <param name="label">The name of the label. </param>
		/// <param name="statement">The <see cref="T:System.CodeDom.CodeStatement" /> to associate with the label. </param>
		// Token: 0x06003C7B RID: 15483 RVA: 0x000D91F3 File Offset: 0x000D73F3
		public CodeLabeledStatement(string label, CodeStatement statement)
		{
			this._label = label;
			this.Statement = statement;
		}

		/// <summary>Gets or sets the name of the label.</summary>
		/// <returns>The name of the label.</returns>
		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x06003C7C RID: 15484 RVA: 0x000D9209 File Offset: 0x000D7409
		// (set) Token: 0x06003C7D RID: 15485 RVA: 0x000D921A File Offset: 0x000D741A
		public string Label
		{
			get
			{
				return this._label ?? string.Empty;
			}
			set
			{
				this._label = value;
			}
		}

		/// <summary>Gets or sets the optional associated statement.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatement" /> that indicates the statement associated with the label.</returns>
		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x06003C7E RID: 15486 RVA: 0x000D9223 File Offset: 0x000D7423
		// (set) Token: 0x06003C7F RID: 15487 RVA: 0x000D922B File Offset: 0x000D742B
		public CodeStatement Statement { get; set; }

		// Token: 0x04002D9D RID: 11677
		private string _label;
	}
}
