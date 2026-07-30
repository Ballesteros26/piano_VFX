using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200000B RID: 11
	public class ContextualMenuManipulator : MouseManipulator
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002C0C File Offset: 0x00000E0C
		public ContextualMenuManipulator(Action<ContextualMenuPopulateEvent> menuBuilder)
		{
			this.m_MenuBuilder = menuBuilder;
			base.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.RightMouse
			});
			bool flag = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;
			if (flag)
			{
				base.activators.Add(new ManipulatorActivationFilter
				{
					button = MouseButton.LeftMouse,
					modifiers = EventModifiers.Control
				});
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002C88 File Offset: 0x00000E88
		protected override void RegisterCallbacksOnTarget()
		{
			bool flag = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;
			if (flag)
			{
				base.target.RegisterCallback<MouseDownEvent>(new EventCallback<MouseDownEvent>(this.OnMouseUpDownEvent), TrickleDown.NoTrickleDown);
			}
			else
			{
				base.target.RegisterCallback<MouseUpEvent>(new EventCallback<MouseUpEvent>(this.OnMouseUpDownEvent), TrickleDown.NoTrickleDown);
			}
			base.target.RegisterCallback<KeyUpEvent>(new EventCallback<KeyUpEvent>(this.OnKeyUpEvent), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<ContextualMenuPopulateEvent>(new EventCallback<ContextualMenuPopulateEvent>(this.OnContextualMenuEvent), TrickleDown.NoTrickleDown);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002D18 File Offset: 0x00000F18
		protected override void UnregisterCallbacksFromTarget()
		{
			bool flag = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;
			if (flag)
			{
				base.target.UnregisterCallback<MouseDownEvent>(new EventCallback<MouseDownEvent>(this.OnMouseUpDownEvent), TrickleDown.NoTrickleDown);
			}
			else
			{
				base.target.UnregisterCallback<MouseUpEvent>(new EventCallback<MouseUpEvent>(this.OnMouseUpDownEvent), TrickleDown.NoTrickleDown);
			}
			base.target.UnregisterCallback<KeyUpEvent>(new EventCallback<KeyUpEvent>(this.OnKeyUpEvent), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<ContextualMenuPopulateEvent>(new EventCallback<ContextualMenuPopulateEvent>(this.OnContextualMenuEvent), TrickleDown.NoTrickleDown);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002DA8 File Offset: 0x00000FA8
		private void OnMouseUpDownEvent(IMouseEvent evt)
		{
			bool flag = base.CanStartManipulation(evt);
			if (flag)
			{
				bool flag2 = base.target.elementPanel != null && base.target.elementPanel.contextualMenuManager != null;
				if (flag2)
				{
					EventBase eventBase = evt as EventBase;
					base.target.elementPanel.contextualMenuManager.DisplayMenu(eventBase, base.target);
					eventBase.StopPropagation();
					eventBase.PreventDefault();
				}
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002E20 File Offset: 0x00001020
		private void OnKeyUpEvent(KeyUpEvent evt)
		{
			bool flag = evt.keyCode == KeyCode.Menu;
			if (flag)
			{
				bool flag2 = base.target.elementPanel != null && base.target.elementPanel.contextualMenuManager != null;
				if (flag2)
				{
					base.target.elementPanel.contextualMenuManager.DisplayMenu(evt, base.target);
					evt.StopPropagation();
					evt.PreventDefault();
				}
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002E98 File Offset: 0x00001098
		private void OnContextualMenuEvent(ContextualMenuPopulateEvent evt)
		{
			bool flag = this.m_MenuBuilder != null;
			if (flag)
			{
				this.m_MenuBuilder.Invoke(evt);
			}
		}

		// Token: 0x0400001A RID: 26
		private Action<ContextualMenuPopulateEvent> m_MenuBuilder;
	}
}
