using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies the name of a file containing the key pair used to generate a strong name.</summary>
	// Token: 0x020002CD RID: 717
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyKeyFileAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the AssemblyKeyFileAttribute class with the name of the file containing the key pair to generate a strong name for the assembly being attributed.</summary>
		/// <param name="keyFile">The name of the file containing the key pair. </param>
		// Token: 0x06002037 RID: 8247 RVA: 0x0007DF04 File Offset: 0x0007C104
		public AssemblyKeyFileAttribute(string keyFile)
		{
			this.m_keyFile = keyFile;
		}

		/// <summary>Gets the name of the file containing the key pair used to generate a strong name for the attributed assembly.</summary>
		/// <returns>A string containing the name of the file that contains the key pair.</returns>
		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06002038 RID: 8248 RVA: 0x0007DF13 File Offset: 0x0007C113
		public string KeyFile
		{
			get
			{
				return this.m_keyFile;
			}
		}

		// Token: 0x0400116D RID: 4461
		private string m_keyFile;
	}
}
