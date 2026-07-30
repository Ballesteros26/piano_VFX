using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents initializing members of a member of a newly created object.</summary>
	// Token: 0x02000294 RID: 660
	public sealed class MemberMemberBinding : MemberBinding
	{
		// Token: 0x0600134E RID: 4942 RVA: 0x0003BEBB File Offset: 0x0003A0BB
		internal MemberMemberBinding(MemberInfo member, ReadOnlyCollection<MemberBinding> bindings)
			: base(MemberBindingType.MemberBinding, member)
		{
			this.Bindings = bindings;
		}

		/// <summary>Gets the bindings that describe how to initialize the members of a member.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.MemberBinding" /> objects that describe how to initialize the members of the member.</returns>
		// Token: 0x1700034C RID: 844
		// (get) Token: 0x0600134F RID: 4943 RVA: 0x0003BECC File Offset: 0x0003A0CC
		public ReadOnlyCollection<MemberBinding> Bindings { get; }

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="bindings">The <see cref="P:System.Linq.Expressions.MemberMemberBinding.Bindings" /> property of the result.</param>
		// Token: 0x06001350 RID: 4944 RVA: 0x0003BED4 File Offset: 0x0003A0D4
		public MemberMemberBinding Update(IEnumerable<MemberBinding> bindings)
		{
			if (bindings != null && ExpressionUtils.SameElements<MemberBinding>(ref bindings, this.Bindings))
			{
				return this;
			}
			return Expression.MemberBind(base.Member, bindings);
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x00003C4C File Offset: 0x00001E4C
		internal override void ValidateAsDefinedHere(int index)
		{
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x0000220F File Offset: 0x0000040F
		internal MemberMemberBinding()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
