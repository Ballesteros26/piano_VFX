using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic.Utils;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Emits or clears a sequence point for debug information. This allows the debugger to highlight the correct source code when debugging.</summary>
	// Token: 0x0200025A RID: 602
	[DebuggerTypeProxy(typeof(Expression.DebugInfoExpressionProxy))]
	public class DebugInfoExpression : Expression
	{
		// Token: 0x06001084 RID: 4228 RVA: 0x00035F0F File Offset: 0x0003410F
		internal DebugInfoExpression(SymbolDocumentInfo document)
		{
			this.Document = document;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.DebugInfoExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x00035F1E File Offset: 0x0003411E
		public sealed override Type Type
		{
			get
			{
				return typeof(void);
			}
		}

		/// <summary>Returns the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06001086 RID: 4230 RVA: 0x00035F2A File Offset: 0x0003412A
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.DebugInfo;
			}
		}

		/// <summary>Gets the start line of this <see cref="T:System.Linq.Expressions.DebugInfoExpression" />.</summary>
		/// <returns>The number of the start line of the code that was used to generate the wrapped expression.</returns>
		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06001087 RID: 4231 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		public virtual int StartLine
		{
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		/// <summary>Gets the start column of this <see cref="T:System.Linq.Expressions.DebugInfoExpression" />.</summary>
		/// <returns>The number of the start column of the code that was used to generate the wrapped expression.</returns>
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		public virtual int StartColumn
		{
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		/// <summary>Gets the end line of this <see cref="T:System.Linq.Expressions.DebugInfoExpression" />.</summary>
		/// <returns>The number of the end line of the code that was used to generate the wrapped expression.</returns>
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06001089 RID: 4233 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		public virtual int EndLine
		{
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		/// <summary>Gets the end column of this <see cref="T:System.Linq.Expressions.DebugInfoExpression" />.</summary>
		/// <returns>The number of the end column of the code that was used to generate the wrapped expression.</returns>
		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x0600108A RID: 4234 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		public virtual int EndColumn
		{
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		/// <summary>Gets the <see cref="T:System.Linq.Expressions.SymbolDocumentInfo" /> that represents the source file.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.SymbolDocumentInfo" /> that represents the source file.</returns>
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x0600108B RID: 4235 RVA: 0x00035F2E File Offset: 0x0003412E
		public SymbolDocumentInfo Document { get; }

		/// <summary>Gets the value to indicate if the <see cref="T:System.Linq.Expressions.DebugInfoExpression" /> is for clearing a sequence point.</summary>
		/// <returns>True if the <see cref="T:System.Linq.Expressions.DebugInfoExpression" /> is for clearing a sequence point, otherwise false.</returns>
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x0600108C RID: 4236 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		public virtual bool IsClear
		{
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		/// <summary>Dispatches to the specific visit method for this node type. For example, <see cref="T:System.Linq.Expressions.MethodCallExpression" /> calls the <see cref="M:System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression)" />.</summary>
		/// <returns>The result of visiting this node.</returns>
		/// <param name="visitor">The visitor to visit this node with.</param>
		// Token: 0x0600108D RID: 4237 RVA: 0x00035F36 File Offset: 0x00034136
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitDebugInfo(this);
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x0000220F File Offset: 0x0000040F
		internal DebugInfoExpression()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
