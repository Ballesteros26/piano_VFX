using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	/// <summary>Implements a base class that holds the configuration information used to activate an instance of a remote type.</summary>
	// Token: 0x02000765 RID: 1893
	[ComVisible(true)]
	public class TypeEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.TypeEntry" /> class.</summary>
		// Token: 0x06004E46 RID: 20038 RVA: 0x00002111 File Offset: 0x00000311
		protected TypeEntry()
		{
		}

		/// <summary>Gets the assembly name of the object type configured to be a remote-activated type.</summary>
		/// <returns>The assembly name of the object type configured to be a remote-activated type.</returns>
		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06004E47 RID: 20039 RVA: 0x0011AE48 File Offset: 0x00119048
		// (set) Token: 0x06004E48 RID: 20040 RVA: 0x0011AE50 File Offset: 0x00119050
		public string AssemblyName
		{
			get
			{
				return this.assembly_name;
			}
			set
			{
				this.assembly_name = value;
			}
		}

		/// <summary>Gets the full type name of the object type configured to be a remote-activated type.</summary>
		/// <returns>The full type name of the object type configured to be a remote-activated type.</returns>
		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x06004E49 RID: 20041 RVA: 0x0011AE59 File Offset: 0x00119059
		// (set) Token: 0x06004E4A RID: 20042 RVA: 0x0011AE61 File Offset: 0x00119061
		public string TypeName
		{
			get
			{
				return this.type_name;
			}
			set
			{
				this.type_name = value;
			}
		}

		// Token: 0x040029DE RID: 10718
		private string assembly_name;

		// Token: 0x040029DF RID: 10719
		private string type_name;
	}
}
