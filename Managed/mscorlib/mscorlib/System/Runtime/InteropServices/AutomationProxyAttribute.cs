using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies whether the type should be marshaled using the Automation marshaler or a custom proxy and stub.</summary>
	// Token: 0x020008CB RID: 2251
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
	public sealed class AutomationProxyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.AutomationProxyAttribute" /> class.</summary>
		/// <param name="val">true if the class should be marshaled using the Automation Marshaler; false if a proxy stub marshaler should be used. </param>
		// Token: 0x06005528 RID: 21800 RVA: 0x0012893F File Offset: 0x00126B3F
		public AutomationProxyAttribute(bool val)
		{
			this._val = val;
		}

		/// <summary>Gets a value indicating the type of marshaler to use.</summary>
		/// <returns>true if the class should be marshaled using the Automation Marshaler; false if a proxy stub marshaler should be used.</returns>
		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06005529 RID: 21801 RVA: 0x0012894E File Offset: 0x00126B4E
		public bool Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002CA9 RID: 11433
		internal bool _val;
	}
}
