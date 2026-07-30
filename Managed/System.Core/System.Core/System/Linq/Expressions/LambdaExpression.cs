using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic.Utils;
using System.Linq.Expressions.Compiler;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Describes a lambda expression. This captures a block of code that is similar to a .NET method body.</summary>
	// Token: 0x02000282 RID: 642
	[DebuggerTypeProxy(typeof(Expression.LambdaExpressionProxy))]
	public abstract class LambdaExpression : Expression, IParameterProvider
	{
		// Token: 0x060012C8 RID: 4808 RVA: 0x0003B4BD File Offset: 0x000396BD
		internal LambdaExpression(Expression body)
		{
			this._body = body;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.LambdaExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x0003B4CC File Offset: 0x000396CC
		public sealed override Type Type
		{
			get
			{
				return this.TypeCore;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060012CA RID: 4810
		internal abstract Type TypeCore { get; }

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060012CB RID: 4811
		internal abstract Type PublicType { get; }

		/// <summary>Returns the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x0003B4D4 File Offset: 0x000396D4
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Lambda;
			}
		}

		/// <summary>Gets the parameters of the lambda expression.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects that represent the parameters of the lambda expression.</returns>
		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060012CD RID: 4813 RVA: 0x0003B4D8 File Offset: 0x000396D8
		public ReadOnlyCollection<ParameterExpression> Parameters
		{
			get
			{
				return this.GetOrMakeParameters();
			}
		}

		/// <summary>Gets the name of the lambda expression.</summary>
		/// <returns>The name of the lambda expression.</returns>
		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x0003B4E0 File Offset: 0x000396E0
		public string Name
		{
			get
			{
				return this.NameCore;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060012CF RID: 4815 RVA: 0x00005E51 File Offset: 0x00004051
		internal virtual string NameCore
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the body of the lambda expression.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the body of the lambda expression.</returns>
		// Token: 0x17000325 RID: 805
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x0003B4E8 File Offset: 0x000396E8
		public Expression Body
		{
			get
			{
				return this._body;
			}
		}

		/// <summary>Gets the return type of the lambda expression.</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the type of the lambda expression.</returns>
		// Token: 0x17000326 RID: 806
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x0003B4F0 File Offset: 0x000396F0
		public Type ReturnType
		{
			get
			{
				return this.Type.GetInvokeMethod().ReturnType;
			}
		}

		/// <summary>Gets the value that indicates if the lambda expression will be compiled with the tail call optimization.</summary>
		/// <returns>True if the lambda expression will be compiled with the tail call optimization, otherwise false.</returns>
		// Token: 0x17000327 RID: 807
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x0003B502 File Offset: 0x00039702
		public bool TailCall
		{
			get
			{
				return this.TailCallCore;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x00002285 File Offset: 0x00000485
		internal virtual bool TailCallCore
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		internal virtual ReadOnlyCollection<ParameterExpression> GetOrMakeParameters()
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x0003B50A File Offset: 0x0003970A
		[ExcludeFromCodeCoverage]
		ParameterExpression IParameterProvider.GetParameter(int index)
		{
			return this.GetParameter(index);
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		internal virtual ParameterExpression GetParameter(int index)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x0003B513 File Offset: 0x00039713
		[ExcludeFromCodeCoverage]
		int IParameterProvider.ParameterCount
		{
			get
			{
				return this.ParameterCount;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		internal virtual int ParameterCount
		{
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		/// <summary>Produces a delegate that represents the lambda expression.</summary>
		/// <returns>A <see cref="T:System.Delegate" /> that contains the compiled version of the lambda expression.</returns>
		// Token: 0x060012D9 RID: 4825 RVA: 0x0003B51B File Offset: 0x0003971B
		public Delegate Compile()
		{
			return this.Compile(false);
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x0003B524 File Offset: 0x00039724
		public Delegate Compile(bool preferInterpretation)
		{
			return LambdaCompiler.Compile(this);
		}

		/// <summary>Compiles the lambda into a method definition.</summary>
		/// <param name="method">A <see cref="T:System.Reflection.Emit.MethodBuilder" /> which will be used to hold the lambda's IL.</param>
		// Token: 0x060012DB RID: 4827 RVA: 0x0003B52C File Offset: 0x0003972C
		public void CompileToMethod(MethodBuilder method)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ContractUtils.Requires(method.IsStatic, "method");
			if (method.DeclaringType as TypeBuilder == null)
			{
				throw Error.MethodBuilderDoesNotHaveTypeBuilder();
			}
			LambdaCompiler.Compile(this, method);
		}

		// Token: 0x060012DC RID: 4828
		internal abstract LambdaExpression Accept(StackSpiller spiller);

		/// <summary>Produces a delegate that represents the lambda expression.</summary>
		/// <returns>A delegate containing the compiled version of the lambda.</returns>
		/// <param name="debugInfoGenerator">Debugging information generator used by the compiler to mark sequence points and annotate local variables.</param>
		// Token: 0x060012DD RID: 4829 RVA: 0x0003B569 File Offset: 0x00039769
		public Delegate Compile(DebugInfoGenerator debugInfoGenerator)
		{
			return this.Compile();
		}

		/// <summary>Compiles the lambda into a method definition and custom debug information.</summary>
		/// <param name="method">A <see cref="T:System.Reflection.Emit.MethodBuilder" /> which will be used to hold the lambda's IL.</param>
		/// <param name="debugInfoGenerator">Debugging information generator used by the compiler to mark sequence points and annotate local variables.</param>
		// Token: 0x060012DE RID: 4830 RVA: 0x0003B571 File Offset: 0x00039771
		public void CompileToMethod(MethodBuilder method, DebugInfoGenerator debugInfoGenerator)
		{
			this.CompileToMethod(method);
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x0000220F File Offset: 0x0000040F
		internal LambdaExpression()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400097B RID: 2427
		private readonly Expression _body;
	}
}
