using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Defines a company name custom attribute for an assembly manifest.</summary>
	// Token: 0x020002C4 RID: 708
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyCompanyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyCompanyAttribute" /> class.</summary>
		/// <param name="company">The company name information. </param>
		// Token: 0x06002025 RID: 8229 RVA: 0x0007DE27 File Offset: 0x0007C027
		public AssemblyCompanyAttribute(string company)
		{
			this.m_company = company;
		}

		/// <summary>Gets company name information.</summary>
		/// <returns>A string containing the company name.</returns>
		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06002026 RID: 8230 RVA: 0x0007DE36 File Offset: 0x0007C036
		public string Company
		{
			get
			{
				return this.m_company;
			}
		}

		// Token: 0x04001164 RID: 4452
		private string m_company;
	}
}
