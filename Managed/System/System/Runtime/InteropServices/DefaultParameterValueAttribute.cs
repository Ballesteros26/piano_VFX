using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Sets the default value of a parameter when called from a language that supports default parameters. This class cannot be inherited. </summary>
	// Token: 0x0200035E RID: 862
	[AttributeUsage(AttributeTargets.Parameter)]
	public sealed class DefaultParameterValueAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.DefaultParameterValueAttribute" /> class with the default value of a parameter.</summary>
		/// <param name="value">An object that represents the default value of a parameter.</param>
		// Token: 0x06001AB9 RID: 6841 RVA: 0x0006BBC1 File Offset: 0x00069DC1
		public DefaultParameterValueAttribute(object value)
		{
			this.value = value;
		}

		/// <summary>Gets the default value of a parameter.</summary>
		/// <returns>An object that represents the default value of a parameter.</returns>
		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001ABA RID: 6842 RVA: 0x0006BBD0 File Offset: 0x00069DD0
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04001853 RID: 6227
		private object value;
	}
}
