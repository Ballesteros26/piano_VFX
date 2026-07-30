using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>Specifies the name of the return value of a method in a Windows Runtime component.</summary>
	// Token: 0x0200095D RID: 2397
	[AttributeUsage(AttributeTargets.Delegate | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = false)]
	public sealed class ReturnValueNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.WindowsRuntime.ReturnValueNameAttribute" /> class, and specifies the name of the return value.</summary>
		/// <param name="name">The name of the return value. </param>
		// Token: 0x06005932 RID: 22834 RVA: 0x0012ABBB File Offset: 0x00128DBB
		public ReturnValueNameAttribute(string name)
		{
			this.m_Name = name;
		}

		/// <summary>Gets the name that was specified for the return value of a method in a Windows Runtime component.</summary>
		/// <returns>The name of the method's return value. </returns>
		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x06005933 RID: 22835 RVA: 0x0012ABCA File Offset: 0x00128DCA
		public string Name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x04002E09 RID: 11785
		private string m_Name;
	}
}
