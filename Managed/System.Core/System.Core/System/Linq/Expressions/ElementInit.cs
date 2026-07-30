using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents an initializer for a single element of an <see cref="T:System.Collections.IEnumerable" /> collection.</summary>
	// Token: 0x0200026D RID: 621
	public sealed class ElementInit : IArgumentProvider
	{
		// Token: 0x06001149 RID: 4425 RVA: 0x00038929 File Offset: 0x00036B29
		internal ElementInit(MethodInfo addMethod, ReadOnlyCollection<Expression> arguments)
		{
			this.AddMethod = addMethod;
			this.Arguments = arguments;
		}

		/// <summary>Gets the instance method that is used to add an element to an <see cref="T:System.Collections.IEnumerable" /> collection.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> that represents an instance method that adds an element to a collection.</returns>
		// Token: 0x170002FA RID: 762
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x0003893F File Offset: 0x00036B3F
		public MethodInfo AddMethod { get; }

		/// <summary>Gets the collection of arguments that are passed to a method that adds an element to an <see cref="T:System.Collections.IEnumerable" /> collection.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.Expression" /> objects that represent the arguments for a method that adds an element to a collection.</returns>
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x00038947 File Offset: 0x00036B47
		public ReadOnlyCollection<Expression> Arguments { get; }

		// Token: 0x0600114C RID: 4428 RVA: 0x0003894F File Offset: 0x00036B4F
		public Expression GetArgument(int index)
		{
			return this.Arguments[index];
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x0600114D RID: 4429 RVA: 0x0003895D File Offset: 0x00036B5D
		public int ArgumentCount
		{
			get
			{
				return this.Arguments.Count;
			}
		}

		/// <summary>Returns a textual representation of an <see cref="T:System.Linq.Expressions.ElementInit" /> object.</summary>
		/// <returns>A textual representation of the <see cref="T:System.Linq.Expressions.ElementInit" /> object.</returns>
		// Token: 0x0600114E RID: 4430 RVA: 0x0003896A File Offset: 0x00036B6A
		public override string ToString()
		{
			return ExpressionStringBuilder.ElementInitBindingToString(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="arguments">The <see cref="P:System.Linq.Expressions.ElementInit.Arguments" /> property of the result.</param>
		// Token: 0x0600114F RID: 4431 RVA: 0x00038972 File Offset: 0x00036B72
		public ElementInit Update(IEnumerable<Expression> arguments)
		{
			if (arguments == this.Arguments)
			{
				return this;
			}
			return Expression.ElementInit(this.AddMethod, arguments);
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x0000220F File Offset: 0x0000040F
		internal ElementInit()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
