using System;

namespace System.CodeDom
{
	/// <summary>Represents a goto statement.</summary>
	// Token: 0x02000772 RID: 1906
	[Serializable]
	public class CodeGotoStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeGotoStatement" /> class. </summary>
		// Token: 0x06003C67 RID: 15463 RVA: 0x000D84F9 File Offset: 0x000D66F9
		public CodeGotoStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeGotoStatement" /> class using the specified label name.</summary>
		/// <param name="label">The name of the label at which to continue program execution. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="Label" /> is null.</exception>
		// Token: 0x06003C68 RID: 15464 RVA: 0x000D90DD File Offset: 0x000D72DD
		public CodeGotoStatement(string label)
		{
			this.Label = label;
		}

		/// <summary>Gets or sets the name of the label at which to continue program execution.</summary>
		/// <returns>A string that indicates the name of the label at which to continue program execution.</returns>
		/// <exception cref="T:System.ArgumentNullException">The label cannot be set because<paramref name=" value" /> is null or an empty string.</exception>
		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x06003C69 RID: 15465 RVA: 0x000D90EC File Offset: 0x000D72EC
		// (set) Token: 0x06003C6A RID: 15466 RVA: 0x000D90F4 File Offset: 0x000D72F4
		public string Label
		{
			get
			{
				return this._label;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException("value");
				}
				this._label = value;
			}
		}

		// Token: 0x04002D96 RID: 11670
		private string _label;
	}
}
