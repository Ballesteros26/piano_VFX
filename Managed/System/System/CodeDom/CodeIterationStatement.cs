using System;

namespace System.CodeDom
{
	/// <summary>Represents a for statement, or a loop through a block of statements, using a test expression as a condition for continuing to loop.</summary>
	// Token: 0x02000774 RID: 1908
	[Serializable]
	public class CodeIterationStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeIterationStatement" /> class.</summary>
		// Token: 0x06003C70 RID: 15472 RVA: 0x000D9161 File Offset: 0x000D7361
		public CodeIterationStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeIterationStatement" /> class using the specified parameters.</summary>
		/// <param name="initStatement">A <see cref="T:System.CodeDom.CodeStatement" /> containing the loop initialization statement. </param>
		/// <param name="testExpression">A <see cref="T:System.CodeDom.CodeExpression" /> containing the expression to test for exit condition. </param>
		/// <param name="incrementStatement">A <see cref="T:System.CodeDom.CodeStatement" /> containing the per-cycle increment statement. </param>
		/// <param name="statements">An array of type <see cref="T:System.CodeDom.CodeStatement" /> containing the statements within the loop. </param>
		// Token: 0x06003C71 RID: 15473 RVA: 0x000D9174 File Offset: 0x000D7374
		public CodeIterationStatement(CodeStatement initStatement, CodeExpression testExpression, CodeStatement incrementStatement, params CodeStatement[] statements)
		{
			this.InitStatement = initStatement;
			this.TestExpression = testExpression;
			this.IncrementStatement = incrementStatement;
			this.Statements.AddRange(statements);
		}

		/// <summary>Gets or sets the loop initialization statement.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatement" /> that indicates the loop initialization statement.</returns>
		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x06003C72 RID: 15474 RVA: 0x000D91A9 File Offset: 0x000D73A9
		// (set) Token: 0x06003C73 RID: 15475 RVA: 0x000D91B1 File Offset: 0x000D73B1
		public CodeStatement InitStatement { get; set; }

		/// <summary>Gets or sets the expression to test as the condition that continues the loop.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the expression to test.</returns>
		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x06003C74 RID: 15476 RVA: 0x000D91BA File Offset: 0x000D73BA
		// (set) Token: 0x06003C75 RID: 15477 RVA: 0x000D91C2 File Offset: 0x000D73C2
		public CodeExpression TestExpression { get; set; }

		/// <summary>Gets or sets the statement that is called after each loop cycle.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatement" /> that indicates the per cycle increment statement.</returns>
		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x06003C76 RID: 15478 RVA: 0x000D91CB File Offset: 0x000D73CB
		// (set) Token: 0x06003C77 RID: 15479 RVA: 0x000D91D3 File Offset: 0x000D73D3
		public CodeStatement IncrementStatement { get; set; }

		/// <summary>Gets the collection of statements to be executed within the loop.</summary>
		/// <returns>An array of type <see cref="T:System.CodeDom.CodeStatement" /> that indicates the statements within the loop.</returns>
		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x06003C78 RID: 15480 RVA: 0x000D91DC File Offset: 0x000D73DC
		public CodeStatementCollection Statements { get; } = new CodeStatementCollection();
	}
}
