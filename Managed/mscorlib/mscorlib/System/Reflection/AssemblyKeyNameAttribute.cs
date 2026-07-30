using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies the name of a key container within the CSP containing the key pair used to generate a strong name.</summary>
	// Token: 0x020002D3 RID: 723
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class AssemblyKeyNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyKeyNameAttribute" /> class with the name of the container holding the key pair used to generate a strong name for the assembly being attributed.</summary>
		/// <param name="keyName">The name of the container containing the key pair. </param>
		// Token: 0x06002049 RID: 8265 RVA: 0x0007DFAC File Offset: 0x0007C1AC
		public AssemblyKeyNameAttribute(string keyName)
		{
			this.m_keyName = keyName;
		}

		/// <summary>Gets the name of the container having the key pair that is used to generate a strong name for the attributed assembly.</summary>
		/// <returns>A string containing the name of the container that has the relevant key pair.</returns>
		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x0600204A RID: 8266 RVA: 0x0007DFBB File Offset: 0x0007C1BB
		public string KeyName
		{
			get
			{
				return this.m_keyName;
			}
		}

		// Token: 0x04001175 RID: 4469
		private string m_keyName;
	}
}
