using System;

namespace System.CodeDom
{
	/// <summary>Represents a catch exception block of a try/catch statement.</summary>
	// Token: 0x0200075D RID: 1885
	[Serializable]
	public class CodeCatchClause
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCatchClause" /> class.</summary>
		// Token: 0x06003BDA RID: 15322 RVA: 0x000020EB File Offset: 0x000002EB
		public CodeCatchClause()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCatchClause" /> class using the specified local variable name for the exception.</summary>
		/// <param name="localName">The name of the local variable declared in the catch clause for the exception. This is optional. </param>
		// Token: 0x06003BDB RID: 15323 RVA: 0x000D88E4 File Offset: 0x000D6AE4
		public CodeCatchClause(string localName)
		{
			this._localName = localName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCatchClause" /> class using the specified local variable name for the exception and exception type.</summary>
		/// <param name="localName">The name of the local variable declared in the catch clause for the exception. This is optional. </param>
		/// <param name="catchExceptionType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type of exception to catch. </param>
		// Token: 0x06003BDC RID: 15324 RVA: 0x000D88F3 File Offset: 0x000D6AF3
		public CodeCatchClause(string localName, CodeTypeReference catchExceptionType)
		{
			this._localName = localName;
			this._catchExceptionType = catchExceptionType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCatchClause" /> class using the specified local variable name for the exception, exception type and statement collection.</summary>
		/// <param name="localName">The name of the local variable declared in the catch clause for the exception. This is optional. </param>
		/// <param name="catchExceptionType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type of exception to catch. </param>
		/// <param name="statements">An array of <see cref="T:System.CodeDom.CodeStatement" /> objects that represent the contents of the catch block. </param>
		// Token: 0x06003BDD RID: 15325 RVA: 0x000D8909 File Offset: 0x000D6B09
		public CodeCatchClause(string localName, CodeTypeReference catchExceptionType, params CodeStatement[] statements)
		{
			this._localName = localName;
			this._catchExceptionType = catchExceptionType;
			this.Statements.AddRange(statements);
		}

		/// <summary>Gets or sets the variable name of the exception that the catch clause handles.</summary>
		/// <returns>The name for the exception variable that the catch clause handles.</returns>
		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06003BDE RID: 15326 RVA: 0x000D892B File Offset: 0x000D6B2B
		// (set) Token: 0x06003BDF RID: 15327 RVA: 0x000D893C File Offset: 0x000D6B3C
		public string LocalName
		{
			get
			{
				return this._localName ?? string.Empty;
			}
			set
			{
				this._localName = value;
			}
		}

		/// <summary>Gets or sets the type of the exception to handle with the catch block.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type of the exception to handle.</returns>
		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x06003BE0 RID: 15328 RVA: 0x000D8948 File Offset: 0x000D6B48
		// (set) Token: 0x06003BE1 RID: 15329 RVA: 0x000D8977 File Offset: 0x000D6B77
		public CodeTypeReference CatchExceptionType
		{
			get
			{
				CodeTypeReference codeTypeReference;
				if ((codeTypeReference = this._catchExceptionType) == null)
				{
					codeTypeReference = (this._catchExceptionType = new CodeTypeReference(typeof(Exception)));
				}
				return codeTypeReference;
			}
			set
			{
				this._catchExceptionType = value;
			}
		}

		/// <summary>Gets the statements within the catch block.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> containing the statements within the catch block.</returns>
		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x06003BE2 RID: 15330 RVA: 0x000D8980 File Offset: 0x000D6B80
		public CodeStatementCollection Statements
		{
			get
			{
				CodeStatementCollection codeStatementCollection;
				if ((codeStatementCollection = this._statements) == null)
				{
					codeStatementCollection = (this._statements = new CodeStatementCollection());
				}
				return codeStatementCollection;
			}
		}

		// Token: 0x04002D76 RID: 11638
		private CodeStatementCollection _statements;

		// Token: 0x04002D77 RID: 11639
		private CodeTypeReference _catchExceptionType;

		// Token: 0x04002D78 RID: 11640
		private string _localName;
	}
}
