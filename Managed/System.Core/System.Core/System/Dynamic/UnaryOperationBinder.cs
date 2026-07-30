using System;
using System.Dynamic.Utils;
using System.Linq.Expressions;

namespace System.Dynamic
{
	/// <summary>Represents the unary dynamic operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x02000337 RID: 823
	public abstract class UnaryOperationBinder : DynamicMetaObjectBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Dynamic.BinaryOperationBinder" /> class.</summary>
		/// <param name="operation">The unary operation kind.</param>
		// Token: 0x060018D4 RID: 6356 RVA: 0x00050149 File Offset: 0x0004E349
		protected UnaryOperationBinder(ExpressionType operation)
		{
			ContractUtils.Requires(UnaryOperationBinder.OperationIsValid(operation), "operation");
			this.Operation = operation;
		}

		/// <summary>The result type of the operation.</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the result type of the operation.</returns>
		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060018D5 RID: 6357 RVA: 0x00050168 File Offset: 0x0004E368
		public sealed override Type ReturnType
		{
			get
			{
				ExpressionType operation = this.Operation;
				if (operation - ExpressionType.IsTrue <= 1)
				{
					return typeof(bool);
				}
				return typeof(object);
			}
		}

		/// <summary>The unary operation kind.</summary>
		/// <returns>The object of the <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents the unary operation kind.</returns>
		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060018D6 RID: 6358 RVA: 0x00050198 File Offset: 0x0004E398
		public ExpressionType Operation { get; }

		/// <summary>Performs the binding of the unary dynamic operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic unary operation.</param>
		// Token: 0x060018D7 RID: 6359 RVA: 0x000501A0 File Offset: 0x0004E3A0
		public DynamicMetaObject FallbackUnaryOperation(DynamicMetaObject target)
		{
			return this.FallbackUnaryOperation(target, null);
		}

		/// <summary>Performs the binding of the unary dynamic operation if the target dynamic object cannot bind.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic unary operation.</param>
		/// <param name="errorSuggestion">The binding result in case the binding fails, or null.</param>
		// Token: 0x060018D8 RID: 6360
		public abstract DynamicMetaObject FallbackUnaryOperation(DynamicMetaObject target, DynamicMetaObject errorSuggestion);

		/// <summary>Performs the binding of the dynamic unary operation.</summary>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		/// <param name="target">The target of the dynamic operation.</param>
		/// <param name="args">An array of arguments of the dynamic operation.</param>
		// Token: 0x060018D9 RID: 6361 RVA: 0x000501AA File Offset: 0x0004E3AA
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.Requires(args == null || args.Length == 0, "args");
			return target.BindUnaryOperation(this);
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x060018DA RID: 6362 RVA: 0x0000AA13 File Offset: 0x00008C13
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x000501D3 File Offset: 0x0004E3D3
		internal static bool OperationIsValid(ExpressionType operation)
		{
			if (operation <= ExpressionType.Decrement)
			{
				if (operation - ExpressionType.Negate > 1 && operation != ExpressionType.Not && operation != ExpressionType.Decrement)
				{
					return false;
				}
			}
			else if (operation != ExpressionType.Extension && operation != ExpressionType.Increment && operation - ExpressionType.OnesComplement > 2)
			{
				return false;
			}
			return true;
		}
	}
}
