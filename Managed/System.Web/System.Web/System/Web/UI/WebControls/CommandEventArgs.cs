using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the Command event.</summary>
	// Token: 0x02000288 RID: 648
	public class CommandEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> class with another <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> object.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> that contains the event data. </param>
		// Token: 0x06001A77 RID: 6775 RVA: 0x00045D99 File Offset: 0x00043F99
		public CommandEventArgs(CommandEventArgs e)
			: this(e.CommandName, e.CommandArgument)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> class with the specified command name and argument.</summary>
		/// <param name="commandName">The name of the command. </param>
		/// <param name="argument">A <see cref="T:System.Object" /> that contains the arguments for the command. </param>
		// Token: 0x06001A78 RID: 6776 RVA: 0x00045DAD File Offset: 0x00043FAD
		public CommandEventArgs(string commandName, object argument)
		{
			this.commandName = commandName;
			this.argument = argument;
		}

		/// <summary>Gets the name of the command.</summary>
		/// <returns>The name of the command to perform.</returns>
		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06001A79 RID: 6777 RVA: 0x00045DC3 File Offset: 0x00043FC3
		public string CommandName
		{
			get
			{
				return this.commandName;
			}
		}

		/// <summary>Gets the argument for the command.</summary>
		/// <returns>A <see cref="T:System.Object" /> that contains the argument for the command.</returns>
		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06001A7A RID: 6778 RVA: 0x00045DCB File Offset: 0x00043FCB
		public object CommandArgument
		{
			get
			{
				return this.argument;
			}
		}

		// Token: 0x0400168C RID: 5772
		private string commandName;

		// Token: 0x0400168D RID: 5773
		private object argument;
	}
}
