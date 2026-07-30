using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Defines a friendly default alias for an assembly manifest.</summary>
	// Token: 0x020002C8 RID: 712
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyDefaultAliasAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyDefaultAliasAttribute" /> class.</summary>
		/// <param name="defaultAlias">The assembly default alias information. </param>
		// Token: 0x0600202D RID: 8237 RVA: 0x0007DE83 File Offset: 0x0007C083
		public AssemblyDefaultAliasAttribute(string defaultAlias)
		{
			this.m_defaultAlias = defaultAlias;
		}

		/// <summary>Gets default alias information.</summary>
		/// <returns>A string containing the default alias information.</returns>
		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x0600202E RID: 8238 RVA: 0x0007DE92 File Offset: 0x0007C092
		public string DefaultAlias
		{
			get
			{
				return this.m_defaultAlias;
			}
		}

		// Token: 0x04001168 RID: 4456
		private string m_defaultAlias;
	}
}
