using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Used to set the default loader optimization policy for the main method of an executable application.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000122 RID: 290
	[AttributeUsage(AttributeTargets.Method)]
	[ComVisible(true)]
	public sealed class LoaderOptimizationAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.LoaderOptimizationAttribute" /> class to the specified value.</summary>
		/// <param name="value">A value equivalent to a <see cref="T:System.LoaderOptimization" /> constant. </param>
		// Token: 0x06000A2F RID: 2607 RVA: 0x00032529 File Offset: 0x00030729
		public LoaderOptimizationAttribute(byte value)
		{
			this._val = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.LoaderOptimizationAttribute" /> class to the specified value.</summary>
		/// <param name="value">A <see cref="T:System.LoaderOptimization" /> constant. </param>
		// Token: 0x06000A30 RID: 2608 RVA: 0x00032538 File Offset: 0x00030738
		public LoaderOptimizationAttribute(LoaderOptimization value)
		{
			this._val = (byte)value;
		}

		/// <summary>Gets the current <see cref="T:System.LoaderOptimization" /> value for this instance.</summary>
		/// <returns>A <see cref="T:System.LoaderOptimization" /> constant.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x00032548 File Offset: 0x00030748
		public LoaderOptimization Value
		{
			get
			{
				return (LoaderOptimization)this._val;
			}
		}

		// Token: 0x04000796 RID: 1942
		internal byte _val;
	}
}
