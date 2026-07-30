using System;

namespace System.Linq.Expressions
{
	/// <summary>Describes the node types for the nodes of an expression tree.</summary>
	// Token: 0x02000270 RID: 624
	public enum ExpressionType
	{
		/// <summary>An addition operation, such as a + b, without overflow checking, for numeric operands.</summary>
		// Token: 0x04000904 RID: 2308
		Add,
		/// <summary>An addition operation, such as (a + b), with overflow checking, for numeric operands.</summary>
		// Token: 0x04000905 RID: 2309
		AddChecked,
		/// <summary>A bitwise or logical AND operation, such as (a &amp; b) in C# and (a And b) in Visual Basic.</summary>
		// Token: 0x04000906 RID: 2310
		And,
		/// <summary>A conditional AND operation that evaluates the second operand only if the first operand evaluates to true. It corresponds to (a &amp;&amp; b) in C# and (a AndAlso b) in Visual Basic.</summary>
		// Token: 0x04000907 RID: 2311
		AndAlso,
		/// <summary>An operation that obtains the length of a one-dimensional array, such as array.Length.</summary>
		// Token: 0x04000908 RID: 2312
		ArrayLength,
		/// <summary>An indexing operation in a one-dimensional array, such as array[index] in C# or array(index) in Visual Basic.</summary>
		// Token: 0x04000909 RID: 2313
		ArrayIndex,
		/// <summary>A method call, such as in the obj.sampleMethod() expression.</summary>
		// Token: 0x0400090A RID: 2314
		Call,
		/// <summary>A node that represents a null coalescing operation, such as (a ?? b) in C# or If(a, b) in Visual Basic.</summary>
		// Token: 0x0400090B RID: 2315
		Coalesce,
		/// <summary>A conditional operation, such as a &gt; b ? a : b in C# or If(a &gt; b, a, b) in Visual Basic.</summary>
		// Token: 0x0400090C RID: 2316
		Conditional,
		/// <summary>A constant value.</summary>
		// Token: 0x0400090D RID: 2317
		Constant,
		/// <summary>A cast or conversion operation, such as (SampleType)obj in C#or CType(obj, SampleType) in Visual Basic. For a numeric conversion, if the converted value is too large for the destination type, no exception is thrown.</summary>
		// Token: 0x0400090E RID: 2318
		Convert,
		/// <summary>A cast or conversion operation, such as (SampleType)obj in C#or CType(obj, SampleType) in Visual Basic. For a numeric conversion, if the converted value does not fit the destination type, an exception is thrown.</summary>
		// Token: 0x0400090F RID: 2319
		ConvertChecked,
		/// <summary>A division operation, such as (a / b), for numeric operands.</summary>
		// Token: 0x04000910 RID: 2320
		Divide,
		/// <summary>A node that represents an equality comparison, such as (a == b) in C# or (a = b) in Visual Basic.</summary>
		// Token: 0x04000911 RID: 2321
		Equal,
		/// <summary>A bitwise or logical XOR operation, such as (a ^ b) in C# or (a Xor b) in Visual Basic.</summary>
		// Token: 0x04000912 RID: 2322
		ExclusiveOr,
		/// <summary>A "greater than" comparison, such as (a &gt; b).</summary>
		// Token: 0x04000913 RID: 2323
		GreaterThan,
		/// <summary>A "greater than or equal to" comparison, such as (a &gt;= b).</summary>
		// Token: 0x04000914 RID: 2324
		GreaterThanOrEqual,
		/// <summary>An operation that invokes a delegate or lambda expression, such as sampleDelegate.Invoke().</summary>
		// Token: 0x04000915 RID: 2325
		Invoke,
		/// <summary>A lambda expression, such as a =&gt; a + a in C# or Function(a) a + a in Visual Basic.</summary>
		// Token: 0x04000916 RID: 2326
		Lambda,
		/// <summary>A bitwise left-shift operation, such as (a &lt;&lt; b).</summary>
		// Token: 0x04000917 RID: 2327
		LeftShift,
		/// <summary>A "less than" comparison, such as (a &lt; b).</summary>
		// Token: 0x04000918 RID: 2328
		LessThan,
		/// <summary>A "less than or equal to" comparison, such as (a &lt;= b).</summary>
		// Token: 0x04000919 RID: 2329
		LessThanOrEqual,
		/// <summary>An operation that creates a new <see cref="T:System.Collections.IEnumerable" /> object and initializes it from a list of elements, such as new List&lt;SampleType&gt;(){ a, b, c } in C# or Dim sampleList = { a, b, c } in Visual Basic.</summary>
		// Token: 0x0400091A RID: 2330
		ListInit,
		/// <summary>An operation that reads from a field or property, such as obj.SampleProperty.</summary>
		// Token: 0x0400091B RID: 2331
		MemberAccess,
		/// <summary>An operation that creates a new object and initializes one or more of its members, such as new Point { X = 1, Y = 2 } in C# or New Point With {.X = 1, .Y = 2} in Visual Basic.</summary>
		// Token: 0x0400091C RID: 2332
		MemberInit,
		/// <summary>An arithmetic remainder operation, such as (a % b) in C# or (a Mod b) in Visual Basic.</summary>
		// Token: 0x0400091D RID: 2333
		Modulo,
		/// <summary>A multiplication operation, such as (a * b), without overflow checking, for numeric operands.</summary>
		// Token: 0x0400091E RID: 2334
		Multiply,
		/// <summary>An multiplication operation, such as (a * b), that has overflow checking, for numeric operands.</summary>
		// Token: 0x0400091F RID: 2335
		MultiplyChecked,
		/// <summary>An arithmetic negation operation, such as (-a). The object a should not be modified in place.</summary>
		// Token: 0x04000920 RID: 2336
		Negate,
		/// <summary>A unary plus operation, such as (+a). The result of a predefined unary plus operation is the value of the operand, but user-defined implementations might have unusual results.</summary>
		// Token: 0x04000921 RID: 2337
		UnaryPlus,
		/// <summary>An arithmetic negation operation, such as (-a), that has overflow checking. The object a should not be modified in place.</summary>
		// Token: 0x04000922 RID: 2338
		NegateChecked,
		/// <summary>An operation that calls a constructor to create a new object, such as new SampleType().</summary>
		// Token: 0x04000923 RID: 2339
		New,
		/// <summary>An operation that creates a new one-dimensional array and initializes it from a list of elements, such as new SampleType[]{a, b, c} in C# or New SampleType(){a, b, c} in Visual Basic.</summary>
		// Token: 0x04000924 RID: 2340
		NewArrayInit,
		/// <summary>An operation that creates a new array, in which the bounds for each dimension are specified, such as new SampleType[dim1, dim2] in C# or New SampleType(dim1, dim2) in Visual Basic.</summary>
		// Token: 0x04000925 RID: 2341
		NewArrayBounds,
		/// <summary>A bitwise complement or logical negation operation. In C#, it is equivalent to (~a) for integral types and to (!a) for Boolean values. In Visual Basic, it is equivalent to (Not a). The object a should not be modified in place.</summary>
		// Token: 0x04000926 RID: 2342
		Not,
		/// <summary>An inequality comparison, such as (a != b) in C# or (a &lt;&gt; b) in Visual Basic.</summary>
		// Token: 0x04000927 RID: 2343
		NotEqual,
		/// <summary>A bitwise or logical OR operation, such as (a | b) in C# or (a Or b) in Visual Basic.</summary>
		// Token: 0x04000928 RID: 2344
		Or,
		/// <summary>A short-circuiting conditional OR operation, such as (a || b) in C# or (a OrElse b) in Visual Basic.</summary>
		// Token: 0x04000929 RID: 2345
		OrElse,
		/// <summary>A reference to a parameter or variable that is defined in the context of the expression. For more information, see <see cref="T:System.Linq.Expressions.ParameterExpression" />.</summary>
		// Token: 0x0400092A RID: 2346
		Parameter,
		/// <summary>A mathematical operation that raises a number to a power, such as (a ^ b) in Visual Basic.</summary>
		// Token: 0x0400092B RID: 2347
		Power,
		/// <summary>An expression that has a constant value of type <see cref="T:System.Linq.Expressions.Expression" />. A <see cref="F:System.Linq.Expressions.ExpressionType.Quote" /> node can contain references to parameters that are defined in the context of the expression it represents.</summary>
		// Token: 0x0400092C RID: 2348
		Quote,
		/// <summary>A bitwise right-shift operation, such as (a &gt;&gt; b).</summary>
		// Token: 0x0400092D RID: 2349
		RightShift,
		/// <summary>A subtraction operation, such as (a - b), without overflow checking, for numeric operands.</summary>
		// Token: 0x0400092E RID: 2350
		Subtract,
		/// <summary>An arithmetic subtraction operation, such as (a - b), that has overflow checking, for numeric operands.</summary>
		// Token: 0x0400092F RID: 2351
		SubtractChecked,
		/// <summary>An explicit reference or boxing conversion in which null is supplied if the conversion fails, such as (obj as SampleType) in C# or TryCast(obj, SampleType) in Visual Basic.</summary>
		// Token: 0x04000930 RID: 2352
		TypeAs,
		/// <summary>A type test, such as obj is SampleType in C# or TypeOf obj is SampleType in Visual Basic.</summary>
		// Token: 0x04000931 RID: 2353
		TypeIs,
		/// <summary>An assignment operation, such as (a = b).</summary>
		// Token: 0x04000932 RID: 2354
		Assign,
		/// <summary>A block of expressions.</summary>
		// Token: 0x04000933 RID: 2355
		Block,
		/// <summary>Debugging information.</summary>
		// Token: 0x04000934 RID: 2356
		DebugInfo,
		/// <summary>A unary decrement operation, such as (a - 1) in C# and Visual Basic. The object a should not be modified in place.</summary>
		// Token: 0x04000935 RID: 2357
		Decrement,
		/// <summary>A dynamic operation.</summary>
		// Token: 0x04000936 RID: 2358
		Dynamic,
		/// <summary>A default value.</summary>
		// Token: 0x04000937 RID: 2359
		Default,
		/// <summary>An extension expression.</summary>
		// Token: 0x04000938 RID: 2360
		Extension,
		/// <summary>A "go to" expression, such as goto Label in C# or GoTo Label in Visual Basic.</summary>
		// Token: 0x04000939 RID: 2361
		Goto,
		/// <summary>A unary increment operation, such as (a + 1) in C# and Visual Basic. The object a should not be modified in place.</summary>
		// Token: 0x0400093A RID: 2362
		Increment,
		/// <summary>An index operation or an operation that accesses a property that takes arguments. </summary>
		// Token: 0x0400093B RID: 2363
		Index,
		/// <summary>A label.</summary>
		// Token: 0x0400093C RID: 2364
		Label,
		/// <summary>A list of run-time variables. For more information, see <see cref="T:System.Linq.Expressions.RuntimeVariablesExpression" />.</summary>
		// Token: 0x0400093D RID: 2365
		RuntimeVariables,
		/// <summary>A loop, such as for or while.</summary>
		// Token: 0x0400093E RID: 2366
		Loop,
		/// <summary>A switch operation, such as switch in C# or Select Case in Visual Basic.</summary>
		// Token: 0x0400093F RID: 2367
		Switch,
		/// <summary>An operation that throws an exception, such as throw new Exception().</summary>
		// Token: 0x04000940 RID: 2368
		Throw,
		/// <summary>A try-catch expression.</summary>
		// Token: 0x04000941 RID: 2369
		Try,
		/// <summary>An unbox value type operation, such as unbox and unbox.any instructions in MSIL. </summary>
		// Token: 0x04000942 RID: 2370
		Unbox,
		/// <summary>An addition compound assignment operation, such as (a += b), without overflow checking, for numeric operands.</summary>
		// Token: 0x04000943 RID: 2371
		AddAssign,
		/// <summary>A bitwise or logical AND compound assignment operation, such as (a &amp;= b) in C#.</summary>
		// Token: 0x04000944 RID: 2372
		AndAssign,
		/// <summary>An division compound assignment operation, such as (a /= b), for numeric operands.</summary>
		// Token: 0x04000945 RID: 2373
		DivideAssign,
		/// <summary>A bitwise or logical XOR compound assignment operation, such as (a ^= b) in C#.</summary>
		// Token: 0x04000946 RID: 2374
		ExclusiveOrAssign,
		/// <summary>A bitwise left-shift compound assignment, such as (a &lt;&lt;= b).</summary>
		// Token: 0x04000947 RID: 2375
		LeftShiftAssign,
		/// <summary>An arithmetic remainder compound assignment operation, such as (a %= b) in C#.</summary>
		// Token: 0x04000948 RID: 2376
		ModuloAssign,
		/// <summary>A multiplication compound assignment operation, such as (a *= b), without overflow checking, for numeric operands.</summary>
		// Token: 0x04000949 RID: 2377
		MultiplyAssign,
		/// <summary>A bitwise or logical OR compound assignment, such as (a |= b) in C#.</summary>
		// Token: 0x0400094A RID: 2378
		OrAssign,
		/// <summary>A compound assignment operation that raises a number to a power, such as (a ^= b) in Visual Basic.</summary>
		// Token: 0x0400094B RID: 2379
		PowerAssign,
		/// <summary>A bitwise right-shift compound assignment operation, such as (a &gt;&gt;= b).</summary>
		// Token: 0x0400094C RID: 2380
		RightShiftAssign,
		/// <summary>A subtraction compound assignment operation, such as (a -= b), without overflow checking, for numeric operands.</summary>
		// Token: 0x0400094D RID: 2381
		SubtractAssign,
		/// <summary>An addition compound assignment operation, such as (a += b), with overflow checking, for numeric operands.</summary>
		// Token: 0x0400094E RID: 2382
		AddAssignChecked,
		/// <summary>A multiplication compound assignment operation, such as (a *= b), that has overflow checking, for numeric operands.</summary>
		// Token: 0x0400094F RID: 2383
		MultiplyAssignChecked,
		/// <summary>A subtraction compound assignment operation, such as (a -= b), that has overflow checking, for numeric operands.</summary>
		// Token: 0x04000950 RID: 2384
		SubtractAssignChecked,
		/// <summary>A unary prefix increment, such as (++a). The object a should be modified in place.</summary>
		// Token: 0x04000951 RID: 2385
		PreIncrementAssign,
		/// <summary>A unary prefix decrement, such as (--a). The object a should be modified in place.</summary>
		// Token: 0x04000952 RID: 2386
		PreDecrementAssign,
		/// <summary>A unary postfix increment, such as (a++). The object a should be modified in place.</summary>
		// Token: 0x04000953 RID: 2387
		PostIncrementAssign,
		/// <summary>A unary postfix decrement, such as (a--). The object a should be modified in place.</summary>
		// Token: 0x04000954 RID: 2388
		PostDecrementAssign,
		/// <summary>An exact type test.</summary>
		// Token: 0x04000955 RID: 2389
		TypeEqual,
		/// <summary>A ones complement operation, such as (~a) in C#.</summary>
		// Token: 0x04000956 RID: 2390
		OnesComplement,
		/// <summary>A true condition value.</summary>
		// Token: 0x04000957 RID: 2391
		IsTrue,
		/// <summary>A false condition value.</summary>
		// Token: 0x04000958 RID: 2392
		IsFalse
	}
}
