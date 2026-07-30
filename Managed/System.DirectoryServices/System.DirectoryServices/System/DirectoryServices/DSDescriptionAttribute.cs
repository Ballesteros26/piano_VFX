using System;
using System.ComponentModel;

namespace System.DirectoryServices
{
	/// <summary>Supports the .NET Framework infrastructure and is not intended to be used directly from code.          </summary>
	// Token: 0x02000012 RID: 18
	[AttributeUsage(AttributeTargets.All)]
	public class DSDescriptionAttribute : DescriptionAttribute
	{
		/// <summary>Supports the .NET Framework infrastructure and is not intended to be used directly from code.</summary>
		/// <param name="description">The description text.</param>
		// Token: 0x06000043 RID: 67 RVA: 0x0000226C File Offset: 0x0000046C
		public DSDescriptionAttribute(string description)
			: base(description)
		{
		}

		/// <summary>Supports the .NET Framework infrastructure and is not intended to be used directly from code.          </summary>
		/// <returns>A string that contains a description of a property or other element.  The <see cref="P:System.DirectoryServices.DSDescriptionAttribute.Description" /> property contains a description that is meaningful to the user.</returns>
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002275 File Offset: 0x00000475
		public override string Description
		{
			get
			{
				return base.Description;
			}
		}
	}
}
