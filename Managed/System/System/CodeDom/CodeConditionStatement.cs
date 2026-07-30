using System;

namespace System.CodeDom
{
	/// <summary>Represents a conditional branch statement, typically represented as an if statement.</summary>
	// Token: 0x02000764 RID: 1892
	[Serializable]
	public class CodeConditionStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeConditionStatement" /> class.</summary>
		// Token: 0x06003C18 RID: 15384 RVA: 0x000D8CA5 File Offset: 0x000D6EA5
		public CodeConditionStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeConditionStatement" /> class using the specified condition and statements.</summary>
		/// <param name="condition">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the expression to evaluate. </param>
		/// <param name="trueStatements">An array of type <see cref="T:System.CodeDom.CodeStatement" /> containing the statements to execute if the condition is true. </param>
		// Token: 0x06003C19 RID: 15385 RVA: 0x000D8CC3 File Offset: 0x000D6EC3
		public CodeConditionStatement(CodeExpression condition, params CodeStatement[] trueStatements)
		{
			this.Condition = condition;
			this.TrueStatements.AddRange(trueStatements);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeConditionStatement" /> class using the specified condition and statements.</summary>
		/// <param name="condition">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the condition to evaluate. </param>
		/// <param name="trueStatements">An array of type <see cref="T:System.CodeDom.CodeStatement" /> containing the statements to execute if the condition is true. </param>
		/// <param name="falseStatements">An array of type <see cref="T:System.CodeDom.CodeStatement" /> containing the statements to execute if the condition is false. </param>
		// Token: 0x06003C1A RID: 15386 RVA: 0x000D8CF4 File Offset: 0x000D6EF4
		public CodeConditionStatement(CodeExpression condition, CodeStatement[] trueStatements, CodeStatement[] falseStatements)
		{
			this.Condition = condition;
			this.TrueStatements.AddRange(trueStatements);
			this.FalseStatements.AddRange(falseStatements);
		}

		/// <summary>Gets or sets the expression to evaluate true or false.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> to evaluate true or false.</returns>
		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06003C1B RID: 15387 RVA: 0x000D8D31 File Offset: 0x000D6F31
		// (set) Token: 0x06003C1C RID: 15388 RVA: 0x000D8D39 File Offset: 0x000D6F39
		public CodeExpression Condition { get; set; }

		/// <summary>Gets the collection of statements to execute if the conditional expression evaluates to true.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> containing the statements to execute if the conditional expression evaluates to true.</returns>
		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x06003C1D RID: 15389 RVA: 0x000D8D42 File Offset: 0x000D6F42
		public CodeStatementCollection TrueStatements { get; } = new CodeStatementCollection();

		/// <summary>Gets the collection of statements to execute if the conditional expression evaluates to false.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> containing the statements to execute if the conditional expression evaluates to false.</returns>
		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06003C1E RID: 15390 RVA: 0x000D8D4A File Offset: 0x000D6F4A
		public CodeStatementCollection FalseStatements { get; } = new CodeStatementCollection();
	}
}
