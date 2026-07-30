using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Discovers the attributes of a local variable and provides access to local variable metadata.</summary>
	// Token: 0x0200031C RID: 796
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public class LocalVariableInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.LocalVariableInfo" /> class.</summary>
		// Token: 0x060022DE RID: 8926 RVA: 0x00002111 File Offset: 0x00000311
		protected LocalVariableInfo()
		{
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the object referred to by the local variable is pinned in memory.</summary>
		/// <returns>true if the object referred to by the variable is pinned in memory; otherwise, false.</returns>
		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060022DF RID: 8927 RVA: 0x00081D5F File Offset: 0x0007FF5F
		public virtual bool IsPinned
		{
			get
			{
				return this.is_pinned;
			}
		}

		/// <summary>Gets the index of the local variable within the method body.</summary>
		/// <returns>An integer value that represents the order of declaration of the local variable within the method body.</returns>
		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060022E0 RID: 8928 RVA: 0x00081D67 File Offset: 0x0007FF67
		public virtual int LocalIndex
		{
			get
			{
				return (int)this.position;
			}
		}

		/// <summary>Gets the type of the local variable.</summary>
		/// <returns>The type of the local variable.</returns>
		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060022E1 RID: 8929 RVA: 0x00081D6F File Offset: 0x0007FF6F
		public virtual Type LocalType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Returns a user-readable string that describes the local variable.</summary>
		/// <returns>A string that displays information about the local variable, including the type name, index, and pinned status.</returns>
		// Token: 0x060022E2 RID: 8930 RVA: 0x00081D78 File Offset: 0x0007FF78
		public override string ToString()
		{
			if (this.is_pinned)
			{
				return string.Format("{0} ({1}) (pinned)", this.type, this.position);
			}
			return string.Format("{0} ({1})", this.type, this.position);
		}

		// Token: 0x04001321 RID: 4897
		internal Type type;

		// Token: 0x04001322 RID: 4898
		internal bool is_pinned;

		// Token: 0x04001323 RID: 4899
		internal ushort position;
	}
}
