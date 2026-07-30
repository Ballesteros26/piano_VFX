using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000015 RID: 21
	public class DropdownMenu
	{
		// Token: 0x06000068 RID: 104 RVA: 0x000032BC File Offset: 0x000014BC
		public List<DropdownMenuItem> MenuItems()
		{
			return this.menuItems;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000032D4 File Offset: 0x000014D4
		public void AppendAction(string actionName, Action<DropdownMenuAction> action, Func<DropdownMenuAction, DropdownMenuAction.Status> actionStatusCallback, object userData = null)
		{
			DropdownMenuAction dropdownMenuAction = new DropdownMenuAction(actionName, action, actionStatusCallback, userData);
			this.menuItems.Add(dropdownMenuAction);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000032FC File Offset: 0x000014FC
		public void AppendAction(string actionName, Action<DropdownMenuAction> action, DropdownMenuAction.Status status = DropdownMenuAction.Status.Normal)
		{
			bool flag = status == DropdownMenuAction.Status.Normal;
			if (flag)
			{
				this.AppendAction(actionName, action, new Func<DropdownMenuAction, DropdownMenuAction.Status>(DropdownMenuAction.AlwaysEnabled), null);
			}
			else
			{
				bool flag2 = status == DropdownMenuAction.Status.Disabled;
				if (flag2)
				{
					this.AppendAction(actionName, action, new Func<DropdownMenuAction, DropdownMenuAction.Status>(DropdownMenuAction.AlwaysDisabled), null);
				}
				else
				{
					this.AppendAction(actionName, action, (DropdownMenuAction e) => status, null);
				}
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003380 File Offset: 0x00001580
		public void InsertAction(int atIndex, string actionName, Action<DropdownMenuAction> action, Func<DropdownMenuAction, DropdownMenuAction.Status> actionStatusCallback, object userData = null)
		{
			DropdownMenuAction dropdownMenuAction = new DropdownMenuAction(actionName, action, actionStatusCallback, userData);
			this.menuItems.Insert(atIndex, dropdownMenuAction);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000033A8 File Offset: 0x000015A8
		public void InsertAction(int atIndex, string actionName, Action<DropdownMenuAction> action, DropdownMenuAction.Status status = DropdownMenuAction.Status.Normal)
		{
			bool flag = status == DropdownMenuAction.Status.Normal;
			if (flag)
			{
				this.InsertAction(atIndex, actionName, action, new Func<DropdownMenuAction, DropdownMenuAction.Status>(DropdownMenuAction.AlwaysEnabled), null);
			}
			else
			{
				bool flag2 = status == DropdownMenuAction.Status.Disabled;
				if (flag2)
				{
					this.InsertAction(atIndex, actionName, action, new Func<DropdownMenuAction, DropdownMenuAction.Status>(DropdownMenuAction.AlwaysDisabled), null);
				}
				else
				{
					this.InsertAction(atIndex, actionName, action, (DropdownMenuAction e) => status, null);
				}
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003430 File Offset: 0x00001630
		public void AppendSeparator(string subMenuPath = null)
		{
			bool flag = this.menuItems.Count > 0 && !(this.menuItems[this.menuItems.Count - 1] is DropdownMenuSeparator);
			if (flag)
			{
				DropdownMenuSeparator dropdownMenuSeparator = new DropdownMenuSeparator(subMenuPath ?? string.Empty);
				this.menuItems.Add(dropdownMenuSeparator);
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003498 File Offset: 0x00001698
		public void InsertSeparator(string subMenuPath, int atIndex)
		{
			bool flag = atIndex > 0 && atIndex <= this.menuItems.Count && !(this.menuItems[atIndex - 1] is DropdownMenuSeparator);
			if (flag)
			{
				DropdownMenuSeparator dropdownMenuSeparator = new DropdownMenuSeparator(subMenuPath ?? string.Empty);
				this.menuItems.Insert(atIndex, dropdownMenuSeparator);
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000034F8 File Offset: 0x000016F8
		public void RemoveItemAt(int index)
		{
			this.menuItems.RemoveAt(index);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003508 File Offset: 0x00001708
		public void PrepareForDisplay(EventBase e)
		{
			this.m_DropdownMenuEventInfo = ((e != null) ? new DropdownMenuEventInfo(e) : null);
			bool flag = this.menuItems.Count == 0;
			if (!flag)
			{
				foreach (DropdownMenuItem dropdownMenuItem in this.menuItems)
				{
					DropdownMenuAction dropdownMenuAction = dropdownMenuItem as DropdownMenuAction;
					bool flag2 = dropdownMenuAction != null;
					if (flag2)
					{
						dropdownMenuAction.UpdateActionStatus(this.m_DropdownMenuEventInfo);
					}
				}
				bool flag3 = this.menuItems[this.menuItems.Count - 1] is DropdownMenuSeparator;
				if (flag3)
				{
					this.menuItems.RemoveAt(this.menuItems.Count - 1);
				}
			}
		}

		// Token: 0x04000030 RID: 48
		private List<DropdownMenuItem> menuItems = new List<DropdownMenuItem>();

		// Token: 0x04000031 RID: 49
		private DropdownMenuEventInfo m_DropdownMenuEventInfo;
	}
}
