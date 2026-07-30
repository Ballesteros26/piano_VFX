using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000013 RID: 19
	public class DropdownMenuAction : DropdownMenuItem
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000031F4 File Offset: 0x000013F4
		public string name { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000031FC File Offset: 0x000013FC
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00003204 File Offset: 0x00001404
		public DropdownMenuAction.Status status { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000320D File Offset: 0x0000140D
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00003215 File Offset: 0x00001415
		public DropdownMenuEventInfo eventInfo { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000061 RID: 97 RVA: 0x0000321E File Offset: 0x0000141E
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00003226 File Offset: 0x00001426
		public object userData { get; private set; }

		// Token: 0x06000063 RID: 99 RVA: 0x00003230 File Offset: 0x00001430
		public static DropdownMenuAction.Status AlwaysEnabled(DropdownMenuAction a)
		{
			return DropdownMenuAction.Status.Normal;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003244 File Offset: 0x00001444
		public static DropdownMenuAction.Status AlwaysDisabled(DropdownMenuAction a)
		{
			return DropdownMenuAction.Status.Disabled;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003257 File Offset: 0x00001457
		public DropdownMenuAction(string actionName, Action<DropdownMenuAction> actionCallback, Func<DropdownMenuAction, DropdownMenuAction.Status> actionStatusCallback, object userData = null)
		{
			this.name = actionName;
			this.actionCallback = actionCallback;
			this.actionStatusCallback = actionStatusCallback;
			this.userData = userData;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000327F File Offset: 0x0000147F
		public void UpdateActionStatus(DropdownMenuEventInfo eventInfo)
		{
			this.eventInfo = eventInfo;
			Func<DropdownMenuAction, DropdownMenuAction.Status> func = this.actionStatusCallback;
			this.status = ((func != null) ? func.Invoke(this) : DropdownMenuAction.Status.Hidden);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000032A4 File Offset: 0x000014A4
		public void Execute()
		{
			Action<DropdownMenuAction> action = this.actionCallback;
			if (action != null)
			{
				action.Invoke(this);
			}
		}

		// Token: 0x04000028 RID: 40
		private readonly Action<DropdownMenuAction> actionCallback;

		// Token: 0x04000029 RID: 41
		private readonly Func<DropdownMenuAction, DropdownMenuAction.Status> actionStatusCallback;

		// Token: 0x02000014 RID: 20
		[Flags]
		public enum Status
		{
			// Token: 0x0400002B RID: 43
			None = 0,
			// Token: 0x0400002C RID: 44
			Normal = 1,
			// Token: 0x0400002D RID: 45
			Disabled = 2,
			// Token: 0x0400002E RID: 46
			Checked = 4,
			// Token: 0x0400002F RID: 47
			Hidden = 8
		}
	}
}
