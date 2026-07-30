using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates the COM alias for a parameter or field type.</summary>
	// Token: 0x020008CA RID: 2250
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComAliasNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComAliasNameAttribute" /> class with the alias for the attributed field or parameter.</summary>
		/// <param name="alias">The alias for the field or parameter as found in the type library when it was imported. </param>
		// Token: 0x06005526 RID: 21798 RVA: 0x00128928 File Offset: 0x00126B28
		public ComAliasNameAttribute(string alias)
		{
			this._val = alias;
		}

		/// <summary>Gets the alias for the field or parameter as found in the type library when it was imported.</summary>
		/// <returns>The alias for the field or parameter as found in the type library when it was imported.</returns>
		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x06005527 RID: 21799 RVA: 0x00128937 File Offset: 0x00126B37
		public string Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002CA8 RID: 11432
		internal string _val;
	}
}
