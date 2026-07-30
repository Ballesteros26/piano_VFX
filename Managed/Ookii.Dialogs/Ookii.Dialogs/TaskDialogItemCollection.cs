using System;
using System.Collections.ObjectModel;
using Ookii.Dialogs.Properties;

namespace Ookii.Dialogs
{
	// Token: 0x02000021 RID: 33
	public class TaskDialogItemCollection<T> : Collection<T> where T : TaskDialogItem
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x00007F18 File Offset: 0x00006118
		internal TaskDialogItemCollection(TaskDialog owner)
		{
			this._owner = owner;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00007F2C File Offset: 0x0000612C
		protected override void ClearItems()
		{
			foreach (T t in this)
			{
				t.Owner = null;
			}
			base.ClearItems();
			this._owner.UpdateDialog();
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00007F94 File Offset: 0x00006194
		protected override void InsertItem(int index, T item)
		{
			bool flag = item == null;
			if (flag)
			{
				throw new ArgumentNullException("item");
			}
			bool flag2 = item.Owner != null;
			if (flag2)
			{
				throw new ArgumentException(Resources.TaskDialogItemHasOwnerError);
			}
			item.Owner = this._owner;
			try
			{
				item.CheckDuplicate(null);
			}
			catch (InvalidOperationException)
			{
				item.Owner = null;
				throw;
			}
			base.InsertItem(index, item);
			this._owner.UpdateDialog();
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00008030 File Offset: 0x00006230
		protected override void RemoveItem(int index)
		{
			base[index].Owner = null;
			base.RemoveItem(index);
			this._owner.UpdateDialog();
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000805C File Offset: 0x0000625C
		protected override void SetItem(int index, T item)
		{
			bool flag = item == null;
			if (flag)
			{
				throw new ArgumentNullException("item");
			}
			bool flag2 = base[index] != item;
			if (flag2)
			{
				bool flag3 = item.Owner != null;
				if (flag3)
				{
					throw new ArgumentException(Resources.TaskDialogItemHasOwnerError);
				}
				item.Owner = this._owner;
				try
				{
					item.CheckDuplicate(base[index]);
				}
				catch (InvalidOperationException)
				{
					item.Owner = null;
					throw;
				}
				base[index].Owner = null;
				base.SetItem(index, item);
				this._owner.UpdateDialog();
			}
		}

		// Token: 0x040000A2 RID: 162
		private TaskDialog _owner;
	}
}
