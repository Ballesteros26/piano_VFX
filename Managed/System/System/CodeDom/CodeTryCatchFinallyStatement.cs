using System;

namespace System.CodeDom
{
	/// <summary>Represents a try block with any number of catch clauses and, optionally, a finally block.</summary>
	// Token: 0x02000792 RID: 1938
	[Serializable]
	public class CodeTryCatchFinallyStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTryCatchFinallyStatement" /> class.</summary>
		// Token: 0x06003D56 RID: 15702 RVA: 0x000DA2BC File Offset: 0x000D84BC
		public CodeTryCatchFinallyStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTryCatchFinallyStatement" /> class using the specified statements for try and catch clauses.</summary>
		/// <param name="tryStatements">An array of <see cref="T:System.CodeDom.CodeStatement" /> objects that indicate the statements to try. </param>
		/// <param name="catchClauses">An array of <see cref="T:System.CodeDom.CodeCatchClause" /> objects that indicate the clauses to catch. </param>
		// Token: 0x06003D57 RID: 15703 RVA: 0x000DA2E8 File Offset: 0x000D84E8
		public CodeTryCatchFinallyStatement(CodeStatement[] tryStatements, CodeCatchClause[] catchClauses)
		{
			this.TryStatements.AddRange(tryStatements);
			this.CatchClauses.AddRange(catchClauses);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTryCatchFinallyStatement" /> class using the specified statements for try, catch clauses, and finally statements.</summary>
		/// <param name="tryStatements">An array of <see cref="T:System.CodeDom.CodeStatement" /> objects that indicate the statements to try. </param>
		/// <param name="catchClauses">An array of <see cref="T:System.CodeDom.CodeCatchClause" /> objects that indicate the clauses to catch. </param>
		/// <param name="finallyStatements">An array of <see cref="T:System.CodeDom.CodeStatement" /> objects that indicate the finally statements to use. </param>
		// Token: 0x06003D58 RID: 15704 RVA: 0x000DA334 File Offset: 0x000D8534
		public CodeTryCatchFinallyStatement(CodeStatement[] tryStatements, CodeCatchClause[] catchClauses, CodeStatement[] finallyStatements)
		{
			this.TryStatements.AddRange(tryStatements);
			this.CatchClauses.AddRange(catchClauses);
			this.FinallyStatements.AddRange(finallyStatements);
		}

		/// <summary>Gets the statements to try.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that indicates the statements to try.</returns>
		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x06003D59 RID: 15705 RVA: 0x000DA38C File Offset: 0x000D858C
		public CodeStatementCollection TryStatements { get; } = new CodeStatementCollection();

		/// <summary>Gets the catch clauses to use.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCatchClauseCollection" /> that indicates the catch clauses to use.</returns>
		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06003D5A RID: 15706 RVA: 0x000DA394 File Offset: 0x000D8594
		public CodeCatchClauseCollection CatchClauses { get; } = new CodeCatchClauseCollection();

		/// <summary>Gets the finally statements to use.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that indicates the finally statements.</returns>
		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x06003D5B RID: 15707 RVA: 0x000DA39C File Offset: 0x000D859C
		public CodeStatementCollection FinallyStatements { get; } = new CodeStatementCollection();
	}
}
