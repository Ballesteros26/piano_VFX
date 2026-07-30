using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000034 RID: 52
	internal class DebugActionState
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000064E1 File Offset: 0x000046E1
		// (set) Token: 0x06000144 RID: 324 RVA: 0x000064E9 File Offset: 0x000046E9
		internal bool runningAction { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000145 RID: 325 RVA: 0x000064F2 File Offset: 0x000046F2
		// (set) Token: 0x06000146 RID: 326 RVA: 0x000064FA File Offset: 0x000046FA
		internal float actionState { get; private set; }

		// Token: 0x06000147 RID: 327 RVA: 0x00006504 File Offset: 0x00004704
		private void Trigger(int triggerCount, float state)
		{
			this.actionState = state;
			this.runningAction = true;
			this.m_Timer = 0f;
			this.m_TriggerPressedUp = new bool[triggerCount];
			for (int i = 0; i < this.m_TriggerPressedUp.Length; i++)
			{
				this.m_TriggerPressedUp[i] = false;
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00006552 File Offset: 0x00004752
		public void TriggerWithButton(string[] buttons, float state)
		{
			this.m_Type = DebugActionState.DebugActionKeyType.Button;
			this.m_PressedButtons = buttons;
			this.m_PressedAxis = "";
			this.Trigger(buttons.Length, state);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00006577 File Offset: 0x00004777
		public void TriggerWithAxis(string axis, float state)
		{
			this.m_Type = DebugActionState.DebugActionKeyType.Axis;
			this.m_PressedAxis = axis;
			this.Trigger(1, state);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000658F File Offset: 0x0000478F
		public void TriggerWithKey(KeyCode[] keys, float state)
		{
			this.m_Type = DebugActionState.DebugActionKeyType.Key;
			this.m_PressedKeys = keys;
			this.m_PressedAxis = "";
			this.Trigger(keys.Length, state);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000065B4 File Offset: 0x000047B4
		private void Reset()
		{
			this.runningAction = false;
			this.m_Timer = 0f;
			this.m_TriggerPressedUp = null;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000065D0 File Offset: 0x000047D0
		public void Update(DebugActionDesc desc)
		{
			this.actionState = 0f;
			if (this.m_TriggerPressedUp != null)
			{
				this.m_Timer += Time.deltaTime;
				for (int i = 0; i < this.m_TriggerPressedUp.Length; i++)
				{
					if (this.m_Type == DebugActionState.DebugActionKeyType.Button)
					{
						this.m_TriggerPressedUp[i] |= Input.GetButtonUp(this.m_PressedButtons[i]);
					}
					else if (this.m_Type == DebugActionState.DebugActionKeyType.Axis)
					{
						this.m_TriggerPressedUp[i] |= Mathf.Approximately(Input.GetAxis(this.m_PressedAxis), 0f);
					}
					else
					{
						this.m_TriggerPressedUp[i] |= Input.GetKeyUp(this.m_PressedKeys[i]);
					}
				}
				bool flag = true;
				foreach (bool flag2 in this.m_TriggerPressedUp)
				{
					flag = flag && flag2;
				}
				if (flag || (this.m_Timer > desc.repeatDelay && desc.repeatMode == DebugActionRepeatMode.Delay))
				{
					this.Reset();
				}
			}
		}

		// Token: 0x040000F0 RID: 240
		private DebugActionState.DebugActionKeyType m_Type;

		// Token: 0x040000F1 RID: 241
		private string[] m_PressedButtons;

		// Token: 0x040000F2 RID: 242
		private string m_PressedAxis = "";

		// Token: 0x040000F3 RID: 243
		private KeyCode[] m_PressedKeys;

		// Token: 0x040000F4 RID: 244
		private bool[] m_TriggerPressedUp;

		// Token: 0x040000F5 RID: 245
		private float m_Timer;

		// Token: 0x020000C0 RID: 192
		private enum DebugActionKeyType
		{
			// Token: 0x04000276 RID: 630
			Button,
			// Token: 0x04000277 RID: 631
			Axis,
			// Token: 0x04000278 RID: 632
			Key
		}
	}
}
