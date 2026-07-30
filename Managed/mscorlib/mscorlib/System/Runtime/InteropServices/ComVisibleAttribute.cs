using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Controls accessibility of an individual managed type or member, or of all types within an assembly, to COM.</summary>
	// Token: 0x020008AC RID: 2220
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComVisibleAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the ComVisibleAttribute class.</summary>
		/// <param name="visibility">true to indicate that the type is visible to COM; otherwise, false. The default is true. </param>
		// Token: 0x060054E5 RID: 21733 RVA: 0x001283AF File Offset: 0x001265AF
		public ComVisibleAttribute(bool visibility)
		{
			this._val = visibility;
		}

		/// <summary>Gets a value that indicates whether the COM type is visible.</summary>
		/// <returns>true if the type is visible; otherwise, false. The default value is true.</returns>
		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x060054E6 RID: 21734 RVA: 0x001283BE File Offset: 0x001265BE
		public bool Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002C02 RID: 11266
		internal bool _val;
	}
}
