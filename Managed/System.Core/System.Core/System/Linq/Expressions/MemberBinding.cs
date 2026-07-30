using System;
using System.Reflection;

namespace System.Linq.Expressions
{
	/// <summary>Provides the base class from which the classes that represent bindings that are used to initialize members of a newly created object derive.</summary>
	// Token: 0x0200028E RID: 654
	public abstract class MemberBinding
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Linq.Expressions.MemberBinding" /> class.</summary>
		/// <param name="type">The <see cref="T:System.Linq.Expressions.MemberBindingType" /> that discriminates the type of binding that is represented.</param>
		/// <param name="member">The <see cref="T:System.Reflection.MemberInfo" /> that represents a field or property to be initialized.</param>
		// Token: 0x06001326 RID: 4902 RVA: 0x0003BB8A File Offset: 0x00039D8A
		[Obsolete("Do not use this constructor. It will be removed in future releases.")]
		protected MemberBinding(MemberBindingType type, MemberInfo member)
		{
			this.BindingType = type;
			this.Member = member;
		}

		/// <summary>Gets the type of binding that is represented.</summary>
		/// <returns>One of the <see cref="T:System.Linq.Expressions.MemberBindingType" /> values.</returns>
		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001327 RID: 4903 RVA: 0x0003BBA0 File Offset: 0x00039DA0
		public MemberBindingType BindingType { get; }

		/// <summary>Gets the field or property to be initialized.</summary>
		/// <returns>The <see cref="T:System.Reflection.MemberInfo" /> that represents the field or property to be initialized.</returns>
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x0003BBA8 File Offset: 0x00039DA8
		public MemberInfo Member { get; }

		/// <summary>Returns a textual representation of the <see cref="T:System.Linq.Expressions.MemberBinding" />.</summary>
		/// <returns>A textual representation of the <see cref="T:System.Linq.Expressions.MemberBinding" />.</returns>
		// Token: 0x06001329 RID: 4905 RVA: 0x0003BBB0 File Offset: 0x00039DB0
		public override string ToString()
		{
			return ExpressionStringBuilder.MemberBindingToString(this);
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0003BBB8 File Offset: 0x00039DB8
		internal virtual void ValidateAsDefinedHere(int index)
		{
			throw Error.UnknownBindingType(index);
		}
	}
}
