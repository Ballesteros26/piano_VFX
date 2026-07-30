using System;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for a field of a type.</summary>
	// Token: 0x02000778 RID: 1912
	[Serializable]
	public class CodeMemberField : CodeTypeMember
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMemberField" /> class.</summary>
		// Token: 0x06003C8C RID: 15500 RVA: 0x000D9275 File Offset: 0x000D7475
		public CodeMemberField()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMemberField" /> class using the specified field type and field name.</summary>
		/// <param name="type">An object that indicates the type of the field. </param>
		/// <param name="name">The name of the field. </param>
		// Token: 0x06003C8D RID: 15501 RVA: 0x000D92E9 File Offset: 0x000D74E9
		public CodeMemberField(CodeTypeReference type, string name)
		{
			this.Type = type;
			base.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMemberField" /> class using the specified field type and field name.</summary>
		/// <param name="type">The type of the field. </param>
		/// <param name="name">The name of the field. </param>
		// Token: 0x06003C8E RID: 15502 RVA: 0x000D92FF File Offset: 0x000D74FF
		public CodeMemberField(string type, string name)
		{
			this.Type = new CodeTypeReference(type);
			base.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMemberField" /> class using the specified field type and field name.</summary>
		/// <param name="type">The type of the field. </param>
		/// <param name="name">The name of the field. </param>
		// Token: 0x06003C8F RID: 15503 RVA: 0x000D931A File Offset: 0x000D751A
		public CodeMemberField(Type type, string name)
		{
			this.Type = new CodeTypeReference(type);
			base.Name = name;
		}

		/// <summary>Gets or sets the type of the field.</summary>
		/// <returns>The type of the field.</returns>
		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x06003C90 RID: 15504 RVA: 0x000D9338 File Offset: 0x000D7538
		// (set) Token: 0x06003C91 RID: 15505 RVA: 0x000D9362 File Offset: 0x000D7562
		public CodeTypeReference Type
		{
			get
			{
				CodeTypeReference codeTypeReference;
				if ((codeTypeReference = this._type) == null)
				{
					codeTypeReference = (this._type = new CodeTypeReference(""));
				}
				return codeTypeReference;
			}
			set
			{
				this._type = value;
			}
		}

		/// <summary>Gets or sets the initialization expression for the field.</summary>
		/// <returns>The initialization expression for the field.</returns>
		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x06003C92 RID: 15506 RVA: 0x000D936B File Offset: 0x000D756B
		// (set) Token: 0x06003C93 RID: 15507 RVA: 0x000D9373 File Offset: 0x000D7573
		public CodeExpression InitExpression { get; set; }

		// Token: 0x04002DA4 RID: 11684
		private CodeTypeReference _type;
	}
}
