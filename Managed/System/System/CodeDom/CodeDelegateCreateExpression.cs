using System;

namespace System.CodeDom
{
	/// <summary>Represents an expression that creates a delegate.</summary>
	// Token: 0x02000767 RID: 1895
	[Serializable]
	public class CodeDelegateCreateExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDelegateCreateExpression" /> class.</summary>
		// Token: 0x06003C26 RID: 15398 RVA: 0x000D82AC File Offset: 0x000D64AC
		public CodeDelegateCreateExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDelegateCreateExpression" /> class.</summary>
		/// <param name="delegateType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the delegate. </param>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object containing the event-handler method. </param>
		/// <param name="methodName">The name of the event-handler method. </param>
		// Token: 0x06003C27 RID: 15399 RVA: 0x000D8DCF File Offset: 0x000D6FCF
		public CodeDelegateCreateExpression(CodeTypeReference delegateType, CodeExpression targetObject, string methodName)
		{
			this._delegateType = delegateType;
			this.TargetObject = targetObject;
			this._methodName = methodName;
		}

		/// <summary>Gets or sets the data type of the delegate.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the delegate.</returns>
		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x06003C28 RID: 15400 RVA: 0x000D8DEC File Offset: 0x000D6FEC
		// (set) Token: 0x06003C29 RID: 15401 RVA: 0x000D8E16 File Offset: 0x000D7016
		public CodeTypeReference DelegateType
		{
			get
			{
				CodeTypeReference codeTypeReference;
				if ((codeTypeReference = this._delegateType) == null)
				{
					codeTypeReference = (this._delegateType = new CodeTypeReference(""));
				}
				return codeTypeReference;
			}
			set
			{
				this._delegateType = value;
			}
		}

		/// <summary>Gets or sets the object that contains the event-handler method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object containing the event-handler method.</returns>
		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x06003C2A RID: 15402 RVA: 0x000D8E1F File Offset: 0x000D701F
		// (set) Token: 0x06003C2B RID: 15403 RVA: 0x000D8E27 File Offset: 0x000D7027
		public CodeExpression TargetObject { get; set; }

		/// <summary>Gets or sets the name of the event handler method.</summary>
		/// <returns>The name of the event handler method.</returns>
		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x06003C2C RID: 15404 RVA: 0x000D8E30 File Offset: 0x000D7030
		// (set) Token: 0x06003C2D RID: 15405 RVA: 0x000D8E41 File Offset: 0x000D7041
		public string MethodName
		{
			get
			{
				return this._methodName ?? string.Empty;
			}
			set
			{
				this._methodName = value;
			}
		}

		// Token: 0x04002D8A RID: 11658
		private CodeTypeReference _delegateType;

		// Token: 0x04002D8B RID: 11659
		private string _methodName;
	}
}
