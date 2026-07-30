using System;

namespace System.CodeDom
{
	/// <summary>Represents the abstract base class from which all code statements derive.</summary>
	// Token: 0x0200078E RID: 1934
	[Serializable]
	public class CodeStatement : CodeObject
	{
		/// <summary>Gets or sets the line on which the code statement occurs. </summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeLinePragma" /> object that indicates the context of the code statement.</returns>
		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x06003D3E RID: 15678 RVA: 0x000DA18C File Offset: 0x000D838C
		// (set) Token: 0x06003D3F RID: 15679 RVA: 0x000DA194 File Offset: 0x000D8394
		public CodeLinePragma LinePragma { get; set; }

		/// <summary>Gets a <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object that contains start directives.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing start directives.</returns>
		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x06003D40 RID: 15680 RVA: 0x000DA1A0 File Offset: 0x000D83A0
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

		/// <summary>Gets a <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object that contains end directives.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing end directives.</returns>
		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x06003D41 RID: 15681 RVA: 0x000DA1C8 File Offset: 0x000D83C8
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

		// Token: 0x04002DE4 RID: 11748
		private CodeDirectiveCollection _startDirectives;

		// Token: 0x04002DE5 RID: 11749
		private CodeDirectiveCollection _endDirectives;
	}
}
