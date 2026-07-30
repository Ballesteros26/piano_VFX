using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Indicates to compilers that a method call or attribute should be ignored unless a specified conditional compilation symbol is defined.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000A5F RID: 2655
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
	[ComVisible(true)]
	[Serializable]
	public sealed class ConditionalAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ConditionalAttribute" /> class.</summary>
		/// <param name="conditionString">A string that specifies the case-sensitive conditional compilation symbol that is associated with the attribute. </param>
		// Token: 0x06006165 RID: 24933 RVA: 0x0013FF12 File Offset: 0x0013E112
		public ConditionalAttribute(string conditionString)
		{
			this.m_conditionString = conditionString;
		}

		/// <summary>Gets the conditional compilation symbol that is associated with the <see cref="T:System.Diagnostics.ConditionalAttribute" /> attribute.</summary>
		/// <returns>A string that specifies the case-sensitive conditional compilation symbol that is associated with the <see cref="T:System.Diagnostics.ConditionalAttribute" /> attribute.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700116B RID: 4459
		// (get) Token: 0x06006166 RID: 24934 RVA: 0x0013FF21 File Offset: 0x0013E121
		public string ConditionString
		{
			get
			{
				return this.m_conditionString;
			}
		}

		// Token: 0x040030A2 RID: 12450
		private string m_conditionString;
	}
}
