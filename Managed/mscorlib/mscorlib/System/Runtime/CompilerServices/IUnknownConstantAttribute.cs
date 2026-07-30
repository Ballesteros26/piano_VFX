using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates that the default value for the attributed field or parameter is an instance of <see cref="T:System.Runtime.InteropServices.UnknownWrapper" />, where the <see cref="P:System.Runtime.InteropServices.UnknownWrapper.WrappedObject" /> is null. This class cannot be inherited. </summary>
	// Token: 0x02000889 RID: 2185
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class IUnknownConstantAttribute : CustomConstantAttribute
	{
		/// <summary>Gets the IUnknown constant stored in this attribute.</summary>
		/// <returns>The IUnknown constant stored in this attribute. Only null is allowed for an IUnknown constant value.</returns>
		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x06005466 RID: 21606 RVA: 0x0012767D File Offset: 0x0012587D
		public override object Value
		{
			get
			{
				return new UnknownWrapper(null);
			}
		}
	}
}
