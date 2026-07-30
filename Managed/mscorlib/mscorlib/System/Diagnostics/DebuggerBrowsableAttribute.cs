using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Determines if and how a member is displayed in the debugger variable windows. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000A67 RID: 2663
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public sealed class DebuggerBrowsableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerBrowsableAttribute" /> class. </summary>
		/// <param name="state">One of the <see cref="T:System.Diagnostics.DebuggerBrowsableState" /> values that specifies how to display the member.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="state" /> is not one of the <see cref="T:System.Diagnostics.DebuggerBrowsableState" /> values.</exception>
		// Token: 0x06006170 RID: 24944 RVA: 0x0013FF93 File Offset: 0x0013E193
		public DebuggerBrowsableAttribute(DebuggerBrowsableState state)
		{
			if (state < DebuggerBrowsableState.Never || state > DebuggerBrowsableState.RootHidden)
			{
				throw new ArgumentOutOfRangeException("state");
			}
			this.state = state;
		}

		/// <summary>Gets the display state for the attribute.</summary>
		/// <returns>One of the <see cref="T:System.Diagnostics.DebuggerBrowsableState" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700116F RID: 4463
		// (get) Token: 0x06006171 RID: 24945 RVA: 0x0013FFB5 File Offset: 0x0013E1B5
		public DebuggerBrowsableState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x040030AE RID: 12462
		private DebuggerBrowsableState state;
	}
}
