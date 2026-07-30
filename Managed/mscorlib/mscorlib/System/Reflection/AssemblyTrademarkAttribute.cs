using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Defines a trademark custom attribute for an assembly manifest.</summary>
	// Token: 0x020002C2 RID: 706
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyTrademarkAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyTrademarkAttribute" /> class.</summary>
		/// <param name="trademark">The trademark information. </param>
		// Token: 0x06002021 RID: 8225 RVA: 0x0007DDF9 File Offset: 0x0007BFF9
		public AssemblyTrademarkAttribute(string trademark)
		{
			this.m_trademark = trademark;
		}

		/// <summary>Gets trademark information.</summary>
		/// <returns>A String containing trademark information.</returns>
		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06002022 RID: 8226 RVA: 0x0007DE08 File Offset: 0x0007C008
		public string Trademark
		{
			get
			{
				return this.m_trademark;
			}
		}

		// Token: 0x04001162 RID: 4450
		private string m_trademark;
	}
}
