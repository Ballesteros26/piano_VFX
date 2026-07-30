using System;

namespace System.CodeDom
{
	/// <summary>Represents an expression cast to a data type or interface.</summary>
	// Token: 0x0200075C RID: 1884
	[Serializable]
	public class CodeCastExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCastExpression" /> class.</summary>
		// Token: 0x06003BD2 RID: 15314 RVA: 0x000D82AC File Offset: 0x000D64AC
		public CodeCastExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCastExpression" /> class using the specified destination type and expression.</summary>
		/// <param name="targetType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the destination type of the cast. </param>
		/// <param name="expression">The <see cref="T:System.CodeDom.CodeExpression" /> to cast. </param>
		// Token: 0x06003BD3 RID: 15315 RVA: 0x000D8854 File Offset: 0x000D6A54
		public CodeCastExpression(CodeTypeReference targetType, CodeExpression expression)
		{
			this.TargetType = targetType;
			this.Expression = expression;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCastExpression" /> class using the specified destination type and expression.</summary>
		/// <param name="targetType">The name of the destination type of the cast. </param>
		/// <param name="expression">The <see cref="T:System.CodeDom.CodeExpression" /> to cast. </param>
		// Token: 0x06003BD4 RID: 15316 RVA: 0x000D886A File Offset: 0x000D6A6A
		public CodeCastExpression(string targetType, CodeExpression expression)
		{
			this.TargetType = new CodeTypeReference(targetType);
			this.Expression = expression;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCastExpression" /> class using the specified destination type and expression.</summary>
		/// <param name="targetType">The destination data type of the cast. </param>
		/// <param name="expression">The <see cref="T:System.CodeDom.CodeExpression" /> to cast. </param>
		// Token: 0x06003BD5 RID: 15317 RVA: 0x000D8885 File Offset: 0x000D6A85
		public CodeCastExpression(Type targetType, CodeExpression expression)
		{
			this.TargetType = new CodeTypeReference(targetType);
			this.Expression = expression;
		}

		/// <summary>Gets or sets the destination type of the cast.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the destination type to cast to.</returns>
		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06003BD6 RID: 15318 RVA: 0x000D88A0 File Offset: 0x000D6AA0
		// (set) Token: 0x06003BD7 RID: 15319 RVA: 0x000D88CA File Offset: 0x000D6ACA
		public CodeTypeReference TargetType
		{
			get
			{
				CodeTypeReference codeTypeReference;
				if ((codeTypeReference = this._targetType) == null)
				{
					codeTypeReference = (this._targetType = new CodeTypeReference(""));
				}
				return codeTypeReference;
			}
			set
			{
				this._targetType = value;
			}
		}

		/// <summary>Gets or sets the expression to cast.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the code to cast.</returns>
		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06003BD8 RID: 15320 RVA: 0x000D88D3 File Offset: 0x000D6AD3
		// (set) Token: 0x06003BD9 RID: 15321 RVA: 0x000D88DB File Offset: 0x000D6ADB
		public CodeExpression Expression { get; set; }

		// Token: 0x04002D74 RID: 11636
		private CodeTypeReference _targetType;
	}
}
