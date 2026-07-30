using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a verb that can be invoked from a designer.</summary>
	// Token: 0x02000319 RID: 793
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class DesignerVerb : MenuCommand
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerVerb" /> class.</summary>
		/// <param name="text">The text of the menu command that is shown to the user. </param>
		/// <param name="handler">The event handler that performs the actions of the verb. </param>
		// Token: 0x06001945 RID: 6469 RVA: 0x00069CC7 File Offset: 0x00067EC7
		public DesignerVerb(string text, EventHandler handler)
			: base(handler, StandardCommands.VerbFirst)
		{
			this.Properties["Text"] = ((text == null) ? null : Regex.Replace(text, "\\(\\&.\\)", ""));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerVerb" /> class.</summary>
		/// <param name="text">The text of the menu command that is shown to the user. </param>
		/// <param name="handler">The event handler that performs the actions of the verb. </param>
		/// <param name="startCommandID">The starting command ID for this verb. By default, the designer architecture sets aside a range of command IDs for verbs. You can override this by providing a custom command ID. </param>
		// Token: 0x06001946 RID: 6470 RVA: 0x00069CFB File Offset: 0x00067EFB
		public DesignerVerb(string text, EventHandler handler, CommandID startCommandID)
			: base(handler, startCommandID)
		{
			this.Properties["Text"] = ((text == null) ? null : Regex.Replace(text, "\\(\\&.\\)", ""));
		}

		/// <summary>Gets or sets the description of the menu item for the verb.</summary>
		/// <returns>A string describing the menu item. </returns>
		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001947 RID: 6471 RVA: 0x00069D2C File Offset: 0x00067F2C
		// (set) Token: 0x06001948 RID: 6472 RVA: 0x00069D59 File Offset: 0x00067F59
		public string Description
		{
			get
			{
				object obj = this.Properties["Description"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.Properties["Description"] = value;
			}
		}

		/// <summary>Gets the text description for the verb command on the menu.</summary>
		/// <returns>A description for the verb command.</returns>
		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001949 RID: 6473 RVA: 0x00069D6C File Offset: 0x00067F6C
		public string Text
		{
			get
			{
				object obj = this.Properties["Text"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
		}

		/// <summary>Overrides <see cref="M:System.Object.ToString" />.</summary>
		/// <returns>The verb's text, or an empty string ("") if the text field is empty.</returns>
		// Token: 0x0600194A RID: 6474 RVA: 0x00069D99 File Offset: 0x00067F99
		public override string ToString()
		{
			return this.Text + " : " + base.ToString();
		}
	}
}
