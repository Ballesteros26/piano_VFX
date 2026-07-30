using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Supplies an explicit <see cref="T:System.Guid" /> when an automatic GUID is undesirable.</summary>
	// Token: 0x020008C0 RID: 2240
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	public sealed class GuidAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.GuidAttribute" /> class with the specified GUID.</summary>
		/// <param name="guid">The <see cref="T:System.Guid" /> to be assigned. </param>
		// Token: 0x06005507 RID: 21767 RVA: 0x001285AE File Offset: 0x001267AE
		public GuidAttribute(string guid)
		{
			this._val = guid;
		}

		/// <summary>Gets the <see cref="T:System.Guid" /> of the class.</summary>
		/// <returns>The <see cref="T:System.Guid" /> of the class.</returns>
		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x06005508 RID: 21768 RVA: 0x001285BD File Offset: 0x001267BD
		public string Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002C8F RID: 11407
		internal string _val;
	}
}
