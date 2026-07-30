using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x02000094 RID: 148
	public class DebugUIHandlerBitField : DebugUIHandlerWidget
	{
		// Token: 0x0600038F RID: 911 RVA: 0x0000DE08 File Offset: 0x0000C008
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.BitField>();
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
			this.nameLabel.text = this.m_Field.displayName;
			int i = 0;
			foreach (GUIContent guicontent in this.m_Field.enumNames)
			{
				if (i < this.toggles.Count)
				{
					DebugUIHandlerIndirectToggle debugUIHandlerIndirectToggle = this.toggles[i];
					debugUIHandlerIndirectToggle.getter = new Func<int, bool>(this.GetValue);
					debugUIHandlerIndirectToggle.setter = new Action<int, bool>(this.SetValue);
					debugUIHandlerIndirectToggle.nextUIHandler = ((i < this.m_Field.enumNames.Length - 1) ? this.toggles[i + 1] : null);
					debugUIHandlerIndirectToggle.previousUIHandler = ((i > 0) ? this.toggles[i - 1] : null);
					debugUIHandlerIndirectToggle.parentUIHandler = this;
					debugUIHandlerIndirectToggle.index = i;
					debugUIHandlerIndirectToggle.nameLabel.text = guicontent.text;
					debugUIHandlerIndirectToggle.Init();
					i++;
				}
			}
			while (i < this.toggles.Count)
			{
				this.toggles[i].transform.SetParent(null);
				i++;
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000DF48 File Offset: 0x0000C148
		private bool GetValue(int index)
		{
			if (index == 0)
			{
				return false;
			}
			index--;
			return (Convert.ToInt32(this.m_Field.GetValue()) & (1 << index)) != 0;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000DF70 File Offset: 0x0000C170
		private void SetValue(int index, bool value)
		{
			if (index == 0)
			{
				this.m_Field.SetValue(Enum.ToObject(this.m_Field.enumType, 0));
				using (List<DebugUIHandlerIndirectToggle>.Enumerator enumerator = this.toggles.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DebugUIHandlerIndirectToggle debugUIHandlerIndirectToggle = enumerator.Current;
						if (debugUIHandlerIndirectToggle.getter != null)
						{
							debugUIHandlerIndirectToggle.UpdateValueLabel();
						}
					}
					return;
				}
			}
			int num = Convert.ToInt32(this.m_Field.GetValue());
			if (value)
			{
				num |= this.m_Field.enumValues[index];
			}
			else
			{
				num &= ~this.m_Field.enumValues[index];
			}
			this.m_Field.SetValue(Enum.ToObject(this.m_Field.enumType, num));
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000E03C File Offset: 0x0000C23C
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			if (fromNext || !this.valueToggle.isOn)
			{
				this.nameLabel.color = this.colorSelected;
			}
			else if (this.valueToggle.isOn)
			{
				if (this.m_Container.IsDirectChild(previous))
				{
					this.nameLabel.color = this.colorSelected;
				}
				else
				{
					DebugUIHandlerWidget lastItem = this.m_Container.GetLastItem();
					DebugManager.instance.ChangeSelection(lastItem, false);
				}
			}
			return true;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000E0B3 File Offset: 0x0000C2B3
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000E0C6 File Offset: 0x0000C2C6
		public override void OnIncrement(bool fast)
		{
			this.valueToggle.isOn = true;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000E0D4 File Offset: 0x0000C2D4
		public override void OnDecrement(bool fast)
		{
			this.valueToggle.isOn = false;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000E0E2 File Offset: 0x0000C2E2
		public override void OnAction()
		{
			this.valueToggle.isOn = !this.valueToggle.isOn;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000E100 File Offset: 0x0000C300
		public override DebugUIHandlerWidget Next()
		{
			if (!this.valueToggle.isOn || this.m_Container == null)
			{
				return base.Next();
			}
			DebugUIHandlerWidget firstItem = this.m_Container.GetFirstItem();
			if (firstItem == null)
			{
				return base.Next();
			}
			return firstItem;
		}

		// Token: 0x040001D4 RID: 468
		public Text nameLabel;

		// Token: 0x040001D5 RID: 469
		public UIFoldout valueToggle;

		// Token: 0x040001D6 RID: 470
		public List<DebugUIHandlerIndirectToggle> toggles;

		// Token: 0x040001D7 RID: 471
		private DebugUI.BitField m_Field;

		// Token: 0x040001D8 RID: 472
		private DebugUIHandlerContainer m_Container;
	}
}
