using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a unique command identifier that consists of a numeric command ID and a GUID menu group identifier.</summary>
	// Token: 0x02000308 RID: 776
	[ComVisible(true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class CommandID
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CommandID" /> class using the specified menu group GUID and command ID number.</summary>
		/// <param name="menuGroup">The GUID of the group that this menu command belongs to. </param>
		/// <param name="commandID">The numeric identifier of this menu command. </param>
		// Token: 0x060018D3 RID: 6355 RVA: 0x000693AD File Offset: 0x000675AD
		public CommandID(Guid menuGroup, int commandID)
		{
			this.menuGroup = menuGroup;
			this.commandID = commandID;
		}

		/// <summary>Gets the numeric command ID.</summary>
		/// <returns>The command ID number.</returns>
		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060018D4 RID: 6356 RVA: 0x000693C3 File Offset: 0x000675C3
		public virtual int ID
		{
			get
			{
				return this.commandID;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.Design.CommandID" /> instances are equal.</summary>
		/// <returns>true if the specified object is equivalent to this one; otherwise, false.</returns>
		/// <param name="obj">The object to compare. </param>
		// Token: 0x060018D5 RID: 6357 RVA: 0x000693CC File Offset: 0x000675CC
		public override bool Equals(object obj)
		{
			if (!(obj is CommandID))
			{
				return false;
			}
			CommandID commandID = (CommandID)obj;
			return commandID.menuGroup.Equals(this.menuGroup) && commandID.commandID == this.commandID;
		}

		/// <returns>A hash code for the current object.</returns>
		// Token: 0x060018D6 RID: 6358 RVA: 0x00069410 File Offset: 0x00067610
		public override int GetHashCode()
		{
			return (this.menuGroup.GetHashCode() << 2) | this.commandID;
		}

		/// <summary>Gets the GUID of the menu group that the menu command identified by this <see cref="T:System.ComponentModel.Design.CommandID" /> belongs to.</summary>
		/// <returns>The GUID of the command group for this command.</returns>
		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060018D7 RID: 6359 RVA: 0x0006943A File Offset: 0x0006763A
		public virtual Guid Guid
		{
			get
			{
				return this.menuGroup;
			}
		}

		/// <summary>Returns a <see cref="T:System.String" /> that represents the current object.</summary>
		/// <returns>A string that contains the command ID information, both the GUID and integer identifier. </returns>
		// Token: 0x060018D8 RID: 6360 RVA: 0x00069444 File Offset: 0x00067644
		public override string ToString()
		{
			return this.menuGroup.ToString() + " : " + this.commandID.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x04001450 RID: 5200
		private readonly Guid menuGroup;

		// Token: 0x04001451 RID: 5201
		private readonly int commandID;
	}
}
