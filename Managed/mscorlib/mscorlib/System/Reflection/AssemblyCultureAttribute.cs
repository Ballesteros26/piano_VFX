using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies which culture the assembly supports.</summary>
	// Token: 0x020002CB RID: 715
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyCultureAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyCultureAttribute" /> class with the culture supported by the assembly being attributed.</summary>
		/// <param name="culture">The culture supported by the attributed assembly. </param>
		// Token: 0x06002033 RID: 8243 RVA: 0x0007DED6 File Offset: 0x0007C0D6
		public AssemblyCultureAttribute(string culture)
		{
			this.m_culture = culture;
		}

		/// <summary>Gets the supported culture of the attributed assembly.</summary>
		/// <returns>A string containing the name of the supported culture.</returns>
		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06002034 RID: 8244 RVA: 0x0007DEE5 File Offset: 0x0007C0E5
		public string Culture
		{
			get
			{
				return this.m_culture;
			}
		}

		// Token: 0x0400116B RID: 4459
		private string m_culture;
	}
}
