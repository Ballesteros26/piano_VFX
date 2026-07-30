using System;
using System.Globalization;

namespace System.Data
{
	// Token: 0x020000B7 RID: 183
	internal sealed class ExprException
	{
		// Token: 0x06000A9B RID: 2715 RVA: 0x00005C14 File Offset: 0x00003E14
		private ExprException()
		{
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00031F54 File Offset: 0x00030154
		private static OverflowException _Overflow(string error)
		{
			OverflowException ex = new OverflowException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00031F63 File Offset: 0x00030163
		private static InvalidExpressionException _Expr(string error)
		{
			InvalidExpressionException ex = new InvalidExpressionException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00031F72 File Offset: 0x00030172
		private static SyntaxErrorException _Syntax(string error)
		{
			SyntaxErrorException ex = new SyntaxErrorException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00031F81 File Offset: 0x00030181
		private static EvaluateException _Eval(string error)
		{
			EvaluateException ex = new EvaluateException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00031F81 File Offset: 0x00030181
		private static EvaluateException _Eval(string error, Exception innerException)
		{
			EvaluateException ex = new EvaluateException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00031F90 File Offset: 0x00030190
		public static Exception InvokeArgument()
		{
			return ExceptionBuilder._Argument("Need a row or a table to Invoke DataFilter.");
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00031F9C File Offset: 0x0003019C
		public static Exception NYI(string moreinfo)
		{
			return ExprException._Expr(SR.Format("The feature not implemented. {0}.", moreinfo));
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00031FAE File Offset: 0x000301AE
		public static Exception MissingOperand(OperatorInfo before)
		{
			return ExprException._Syntax(SR.Format("Syntax error: Missing operand after '{0}' operator.", Operators.ToString(before._op)));
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00031FCA File Offset: 0x000301CA
		public static Exception MissingOperator(string token)
		{
			return ExprException._Syntax(SR.Format("Syntax error: Missing operand after '{0}' operator.", token));
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x00031FDC File Offset: 0x000301DC
		public static Exception TypeMismatch(string expr)
		{
			return ExprException._Eval(SR.Format("Type mismatch in expression '{0}'.", expr));
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00031FEE File Offset: 0x000301EE
		public static Exception FunctionArgumentOutOfRange(string arg, string func)
		{
			return ExceptionBuilder._ArgumentOutOfRange(arg, SR.Format("{0}() argument is out of range.", func));
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00032001 File Offset: 0x00030201
		public static Exception ExpressionTooComplex()
		{
			return ExprException._Eval("Expression is too complex.");
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0003200D File Offset: 0x0003020D
		public static Exception UnboundName(string name)
		{
			return ExprException._Eval(SR.Format("Cannot find column [{0}].", name));
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0003201F File Offset: 0x0003021F
		public static Exception InvalidString(string str)
		{
			return ExprException._Syntax(SR.Format("The expression contains an invalid string constant: {0}.", str));
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00032031 File Offset: 0x00030231
		public static Exception UndefinedFunction(string name)
		{
			return ExprException._Eval(SR.Format("The expression contains undefined function call {0}().", name));
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00032043 File Offset: 0x00030243
		public static Exception SyntaxError()
		{
			return ExprException._Syntax("Syntax error in the expression.");
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0003204F File Offset: 0x0003024F
		public static Exception FunctionArgumentCount(string name)
		{
			return ExprException._Eval(SR.Format("Invalid number of arguments: function {0}().", name));
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00032061 File Offset: 0x00030261
		public static Exception MissingRightParen()
		{
			return ExprException._Syntax("The expression is missing the closing parenthesis.");
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0003206D File Offset: 0x0003026D
		public static Exception UnknownToken(string token, int position)
		{
			return ExprException._Syntax(SR.Format("Cannot interpret token '{0}' at position {1}.", token, position.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0003208B File Offset: 0x0003028B
		public static Exception UnknownToken(Tokens tokExpected, Tokens tokCurr, int position)
		{
			return ExprException._Syntax(SR.Format("Expected {0}, but actual token at the position {2} is {1}.", tokExpected.ToString(), tokCurr.ToString(), position.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x000320C2 File Offset: 0x000302C2
		public static Exception DatatypeConvertion(Type type1, Type type2)
		{
			return ExprException._Eval(SR.Format("Cannot convert from {0} to {1}.", type1.ToString(), type2.ToString()));
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x000320DF File Offset: 0x000302DF
		public static Exception DatavalueConvertion(object value, Type type, Exception innerException)
		{
			return ExprException._Eval(SR.Format("Cannot convert value '{0}' to Type: {1}.", value.ToString(), type.ToString()), innerException);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x000320FD File Offset: 0x000302FD
		public static Exception InvalidName(string name)
		{
			return ExprException._Syntax(SR.Format("Invalid column name [{0}].", name));
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0003210F File Offset: 0x0003030F
		public static Exception InvalidDate(string date)
		{
			return ExprException._Syntax(SR.Format("The expression contains invalid date constant '{0}'.", date));
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00032121 File Offset: 0x00030321
		public static Exception NonConstantArgument()
		{
			return ExprException._Eval("Only constant expressions are allowed in the expression list for the IN operator.");
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0003212D File Offset: 0x0003032D
		public static Exception InvalidPattern(string pat)
		{
			return ExprException._Eval(SR.Format("Error in Like operator: the string pattern '{0}' is invalid.", pat));
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0003213F File Offset: 0x0003033F
		public static Exception InWithoutParentheses()
		{
			return ExprException._Syntax("Syntax error: The items following the IN keyword must be separated by commas and be enclosed in parentheses.");
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0003214B File Offset: 0x0003034B
		public static Exception InWithoutList()
		{
			return ExprException._Syntax("Syntax error: The IN keyword must be followed by a non-empty list of expressions separated by commas, and also must be enclosed in parentheses.");
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00032157 File Offset: 0x00030357
		public static Exception InvalidIsSyntax()
		{
			return ExprException._Syntax("Syntax error: Invalid usage of 'Is' operator. Correct syntax: <expression> Is [Not] Null.");
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00032163 File Offset: 0x00030363
		public static Exception Overflow(Type type)
		{
			return ExprException._Overflow(SR.Format("Value is either too large or too small for Type '{0}'.", type.Name));
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0003217A File Offset: 0x0003037A
		public static Exception ArgumentType(string function, int arg, Type type)
		{
			return ExprException._Eval(SR.Format("Type mismatch in function argument: {0}(), argument {1}, expected {2}.", function, arg.ToString(CultureInfo.InvariantCulture), type.ToString()));
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0003219E File Offset: 0x0003039E
		public static Exception ArgumentTypeInteger(string function, int arg)
		{
			return ExprException._Eval(SR.Format("Type mismatch in function argument: {0}(), argument {1}, expected one of the Integer types.", function, arg.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x000321BC File Offset: 0x000303BC
		public static Exception TypeMismatchInBinop(int op, Type type1, Type type2)
		{
			return ExprException._Eval(SR.Format("Cannot perform '{0}' operation on {1} and {2}.", Operators.ToString(op), type1.ToString(), type2.ToString()));
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x000321DF File Offset: 0x000303DF
		public static Exception AmbiguousBinop(int op, Type type1, Type type2)
		{
			return ExprException._Eval(SR.Format("Operator '{0}' is ambiguous on operands of type '{1}' and '{2}'. Cannot mix signed and unsigned types. Please use explicit Convert() function.", Operators.ToString(op), type1.ToString(), type2.ToString()));
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00032202 File Offset: 0x00030402
		public static Exception UnsupportedOperator(int op)
		{
			return ExprException._Eval(SR.Format("The expression contains unsupported operator '{0}'.", Operators.ToString(op)));
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00032219 File Offset: 0x00030419
		public static Exception InvalidNameBracketing(string name)
		{
			return ExprException._Syntax(SR.Format("The expression contains invalid name: '{0}'.", name));
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0003222B File Offset: 0x0003042B
		public static Exception MissingOperandBefore(string op)
		{
			return ExprException._Syntax(SR.Format("Syntax error: Missing operand before '{0}' operator.", op));
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0003223D File Offset: 0x0003043D
		public static Exception TooManyRightParentheses()
		{
			return ExprException._Syntax("The expression has too many closing parentheses.");
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00032249 File Offset: 0x00030449
		public static Exception UnresolvedRelation(string name, string expr)
		{
			return ExprException._Eval(SR.Format("The table [{0}] involved in more than one relation. You must explicitly mention a relation name in the expression '{1}'.", name, expr));
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0003225C File Offset: 0x0003045C
		internal static EvaluateException BindFailure(string relationName)
		{
			return ExprException._Eval(SR.Format("Cannot find the parent relation '{0}'.", relationName));
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0003226E File Offset: 0x0003046E
		public static Exception AggregateArgument()
		{
			return ExprException._Syntax("Syntax error in aggregate argument: Expecting a single column argument with possible 'Child' qualifier.");
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0003227A File Offset: 0x0003047A
		public static Exception AggregateUnbound(string expr)
		{
			return ExprException._Eval(SR.Format("Unbound reference in the aggregate expression '{0}'.", expr));
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0003228C File Offset: 0x0003048C
		public static Exception EvalNoContext()
		{
			return ExprException._Eval("Cannot evaluate non-constant expression without current row.");
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00032298 File Offset: 0x00030498
		public static Exception ExpressionUnbound(string expr)
		{
			return ExprException._Eval(SR.Format("Unbound reference in the expression '{0}'.", expr));
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x000322AA File Offset: 0x000304AA
		public static Exception ComputeNotAggregate(string expr)
		{
			return ExprException._Eval(SR.Format("Cannot evaluate. Expression '{0}' is not an aggregate.", expr));
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x000322BC File Offset: 0x000304BC
		public static Exception FilterConvertion(string expr)
		{
			return ExprException._Eval(SR.Format("Filter expression '{0}' does not evaluate to a Boolean term.", expr));
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x000322CE File Offset: 0x000304CE
		public static Exception LookupArgument()
		{
			return ExprException._Syntax("Syntax error in Lookup expression: Expecting keyword 'Parent' followed by a single column argument with possible relation qualifier: Parent[(<relation_name>)].<column_name>.");
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x000322DA File Offset: 0x000304DA
		public static Exception InvalidType(string typeName)
		{
			return ExprException._Eval(SR.Format("Invalid type name '{0}'.", typeName));
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x000322EC File Offset: 0x000304EC
		public static Exception InvalidHoursArgument()
		{
			return ExprException._Eval("'hours' argument is out of range. Value must be between -14 and +14.");
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x000322F8 File Offset: 0x000304F8
		public static Exception InvalidMinutesArgument()
		{
			return ExprException._Eval("'minutes' argument is out of range. Value must be between -59 and +59.");
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00032304 File Offset: 0x00030504
		public static Exception InvalidTimeZoneRange()
		{
			return ExprException._Eval("Provided range for time one exceeds total of 14 hours.");
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00032310 File Offset: 0x00030510
		public static Exception MismatchKindandTimeSpan()
		{
			return ExprException._Eval("Kind property of provided DateTime argument, does not match 'hours' and 'minutes' arguments.");
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0003231C File Offset: 0x0003051C
		public static Exception UnsupportedDataType(Type type)
		{
			return ExceptionBuilder._Argument(SR.Format("A DataColumn of type '{0}' does not support expression.", type.FullName));
		}
	}
}
