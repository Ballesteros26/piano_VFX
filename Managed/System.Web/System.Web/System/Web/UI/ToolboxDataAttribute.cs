using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Specifies the default tag generated for a custom control when it is dragged from a toolbox in a tool such as Microsoft Visual Studio.</summary>
	// Token: 0x0200023C RID: 572
	[AttributeUsage(AttributeTargets.Class)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class ToolboxDataAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ToolboxDataAttribute" /> class. </summary>
		/// <param name="data">The string to be set as the <see cref="P:System.Web.UI.ToolboxDataAttribute.Data" />.</param>
		// Token: 0x060017AD RID: 6061 RVA: 0x0004068F File Offset: 0x0003E88F
		public ToolboxDataAttribute(string data)
		{
			this.data = data;
		}

		/// <summary>Gets the string representing the initial values of the control's property, which is used in a visual designer for creating an instance of the control.</summary>
		/// <returns>A string representing the initial values for this attribute.</returns>
		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x0004069E File Offset: 0x0003E89E
		public string Data
		{
			get
			{
				return this.data;
			}
		}

		/// <summary>Tests whether the <see cref="T:System.Web.UI.ToolboxDataAttribute" /> object is equal to the given object.</summary>
		/// <returns>true, if the <see cref="T:System.Web.UI.ToolboxDataAttribute" /> object is equal to the given object; otherwise, false.</returns>
		/// <param name="obj">The object to compare to.</param>
		// Token: 0x060017AF RID: 6063 RVA: 0x000406A8 File Offset: 0x0003E8A8
		public override bool Equals(object obj)
		{
			ToolboxDataAttribute toolboxDataAttribute = obj as ToolboxDataAttribute;
			return toolboxDataAttribute != null && toolboxDataAttribute.Data == this.data;
		}

		/// <summary>Returns the hash code of the custom control.</summary>
		/// <returns>A 32-bit signed integer representing the hash code.</returns>
		// Token: 0x060017B0 RID: 6064 RVA: 0x000406D2 File Offset: 0x0003E8D2
		public override int GetHashCode()
		{
			if (this.data == null)
			{
				return -1;
			}
			return this.data.GetHashCode();
		}

		/// <summary>Tests whether the <see cref="T:System.Web.UI.ToolboxDataAttribute" /> object contains the default value for the <see cref="P:System.Web.UI.ToolboxDataAttribute.Data" /> property.</summary>
		/// <returns>true, if the <see cref="T:System.Web.UI.ToolboxDataAttribute" /> contains the default value for the <see cref="P:System.Web.UI.ToolboxDataAttribute.Data" /> property; otherwise, false.</returns>
		// Token: 0x060017B1 RID: 6065 RVA: 0x000406E9 File Offset: 0x0003E8E9
		public override bool IsDefaultAttribute()
		{
			return this.data == null || this.data.Length == 0;
		}

		/// <summary>Represents the default <see cref="T:System.Web.UI.ToolboxDataAttribute" /> value for a custom control.</summary>
		// Token: 0x040015ED RID: 5613
		public static readonly ToolboxDataAttribute Default = new ToolboxDataAttribute(string.Empty);

		// Token: 0x040015EE RID: 5614
		private string data;
	}
}
