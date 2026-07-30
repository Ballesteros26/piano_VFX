using System;
using System.Dynamic.Utils;
using System.Linq.Expressions;

namespace System.Dynamic
{
	/// <summary>Represents the binary dynamic operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x02000305 RID: 773
	public abstract class BinaryOperationBinder : DynamicMetaObjectBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Dynamic.BinaryOperationBinder" /> class.</summary>
		/// <param name="operation">The binary operation kind.</param>
		// Token: 0x06001777 RID: 6007 RVA: 0x0004CE37 File Offset: 0x0004B037
		protected BinaryOperationBinder(ExpressionType operation)
		{
			ContractUtils.Requires(BinaryOperationBinder.OperationIsValid(operation), "operation");
			this.Operation = operation;
		}

		/// <summary>The result type of the operation.</summary>
		/// <returns>The result type of the operation.</returns>
		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x00037AE6 File Offset: 0x00035CE6
		public sealed override Type ReturnType
		{
			get
			{
				return typeof(object);
			}
		}

		/// <summary>The binary operation kind.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> object representing the kind of binary operation.</returns>
		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001779 RID: 6009 RVA: 0x0004CE56 File Offset: 0x0004B056
		public ExpressionType Operation { get; }

		/// <summary>Performs the binding of the binary dynamic operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic binary operation.</param>
		/// <param name="arg">The right hand side operand of the dynamic binary operation.</param>
		// Token: 0x0600177A RID: 6010 RVA: 0x0004CE5E File Offset: 0x0004B05E
		public DynamicMetaObject FallbackBinaryOperation(DynamicMetaObject target, DynamicMetaObject arg)
		{
			return this.FallbackBinaryOperation(target, arg, null);
		}

		/// <summary>When overridden in the derived class, performs the binding of the binary dynamic operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic binary operation.</param>
		/// <param name="arg">The right hand side operand of the dynamic binary operation.</param>
		/// <param name="errorSuggestion">The binding result if the binding fails, or null.</param>
		// Token: 0x0600177B RID: 6011
		public abstract DynamicMetaObject FallbackBinaryOperation(DynamicMetaObject target, DynamicMetaObject arg, DynamicMetaObject errorSuggestion);

		/// <summary>Performs the binding of the dynamic binary operation.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic operation.</param>
		/// <param name="args">An array of arguments of the dynamic operation.</param>
		// Token: 0x0600177C RID: 6012 RVA: 0x0004CE6C File Offset: 0x0004B06C
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.RequiresNotNull(args, "args");
			ContractUtils.Requires(args.Length == 1, "args");
			DynamicMetaObject dynamicMetaObject = args[0];
			ContractUtils.RequiresNotNull(dynamicMetaObject, "args");
			return target.BindBinaryOperation(this, dynamicMetaObject);
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x0600177D RID: 6013 RVA: 0x0000AA13 File Offset: 0x00008C13
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x0004CEB8 File Offset: 0x0004B0B8
		internal static bool OperationIsValid(ExpressionType operation)
		{
			if (operation <= ExpressionType.Multiply)
			{
				if (operation != ExpressionType.Add && operation != ExpressionType.And)
				{
					switch (operation)
					{
					case ExpressionType.Divide:
					case ExpressionType.Equal:
					case ExpressionType.ExclusiveOr:
					case ExpressionType.GreaterThan:
					case ExpressionType.GreaterThanOrEqual:
					case ExpressionType.LeftShift:
					case ExpressionType.LessThan:
					case ExpressionType.LessThanOrEqual:
					case ExpressionType.Modulo:
					case ExpressionType.Multiply:
						break;
					case ExpressionType.Invoke:
					case ExpressionType.Lambda:
					case ExpressionType.ListInit:
					case ExpressionType.MemberAccess:
					case ExpressionType.MemberInit:
						return false;
					default:
						return false;
					}
				}
			}
			else
			{
				switch (operation)
				{
				case ExpressionType.NotEqual:
				case ExpressionType.Or:
				case ExpressionType.Power:
				case ExpressionType.RightShift:
				case ExpressionType.Subtract:
					break;
				case ExpressionType.OrElse:
				case ExpressionType.Parameter:
				case ExpressionType.Quote:
					return false;
				default:
					if (operation != ExpressionType.Extension && operation - ExpressionType.AddAssign > 10)
					{
						return false;
					}
					break;
				}
			}
			return true;
		}
	}
}
