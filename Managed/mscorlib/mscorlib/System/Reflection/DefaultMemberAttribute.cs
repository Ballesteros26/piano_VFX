using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Defines the member of a type that is the default member used by <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[])" />. </summary>
	// Token: 0x020002DB RID: 731
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
	[ComVisible(true)]
	[Serializable]
	public sealed class DefaultMemberAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.DefaultMemberAttribute" /> class.</summary>
		/// <param name="memberName">A String containing the name of the member to invoke. This may be a constructor, method, property, or field. A suitable invocation attribute must be specified when the member is invoked. The default member of a class can be specified by passing an empty String as the name of the member.The default member of a type is marked with the DefaultMemberAttribute custom attribute or marked in COM in the usual way. </param>
		// Token: 0x06002054 RID: 8276 RVA: 0x0007DFCB File Offset: 0x0007C1CB
		public DefaultMemberAttribute(string memberName)
		{
			this.m_memberName = memberName;
		}

		/// <summary>Gets the name from the attribute.</summary>
		/// <returns>A string representing the member name.</returns>
		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06002055 RID: 8277 RVA: 0x0007DFDA File Offset: 0x0007C1DA
		public string MemberName
		{
			get
			{
				return this.m_memberName;
			}
		}

		// Token: 0x040011A1 RID: 4513
		private string m_memberName;
	}
}
