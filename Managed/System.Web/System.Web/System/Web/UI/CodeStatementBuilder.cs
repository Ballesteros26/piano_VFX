using System;
using System.CodeDom;
using Unity;

namespace System.Web.UI
{
	/// <summary>Generates Code DOM statements.</summary>
	// Token: 0x02000789 RID: 1929
	public abstract class CodeStatementBuilder : ControlBuilder
	{
		/// <summary>Initializes a new instance of the CodeStatementBuilder class.</summary>
		// Token: 0x06004E44 RID: 20036 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected CodeStatementBuilder()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Builds a <see cref="T:System.CodeDom.CodeStatement" /> object for a generated Render method.</summary>
		/// <returns>An object that represents a code statement.</returns>
		/// <param name="writerReferenceExpression">Represents a reference to the value of an argument.</param>
		// Token: 0x06004E45 RID: 20037
		public abstract CodeStatement BuildStatement(CodeArgumentReferenceExpression writerReferenceExpression);
	}
}
