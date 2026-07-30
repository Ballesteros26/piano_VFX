using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Allows the user to specify the ProgID of a class.</summary>
	// Token: 0x020008B1 RID: 2225
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	[ComVisible(true)]
	public sealed class ProgIdAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the ProgIdAttribute with the specified ProgID.</summary>
		/// <param name="progId">The ProgID to be assigned to the class. </param>
		// Token: 0x060054ED RID: 21741 RVA: 0x001283F9 File Offset: 0x001265F9
		public ProgIdAttribute(string progId)
		{
			this._val = progId;
		}

		/// <summary>Gets the ProgID of the class.</summary>
		/// <returns>The ProgID of the class.</returns>
		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x060054EE RID: 21742 RVA: 0x00128408 File Offset: 0x00126608
		public string Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002C05 RID: 11269
		internal string _val;
	}
}
