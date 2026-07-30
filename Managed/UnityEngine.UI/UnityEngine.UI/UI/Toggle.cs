using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200003A RID: 58
	[AddComponentMenu("UI/Toggle", 31)]
	[RequireComponent(typeof(RectTransform))]
	public class Toggle : Selectable, IPointerClickHandler, IEventSystemHandler, ISubmitHandler, ICanvasElement
	{
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x0001496E File Offset: 0x00012B6E
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x00014976 File Offset: 0x00012B76
		public ToggleGroup group
		{
			get
			{
				return this.m_Group;
			}
			set
			{
				this.SetToggleGroup(value, true);
				this.PlayEffect(true);
			}
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00014987 File Offset: 0x00012B87
		protected Toggle()
		{
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00004C7A File Offset: 0x00002E7A
		public virtual void Rebuild(CanvasUpdate executing)
		{
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00004C7A File Offset: 0x00002E7A
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00004C7A File Offset: 0x00002E7A
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x000149A1 File Offset: 0x00012BA1
		protected override void OnDestroy()
		{
			if (this.m_Group != null)
			{
				this.m_Group.EnsureValidState();
			}
			base.OnDestroy();
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x000149C2 File Offset: 0x00012BC2
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetToggleGroup(this.m_Group, false);
			this.PlayEffect(true);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x000149DE File Offset: 0x00012BDE
		protected override void OnDisable()
		{
			this.SetToggleGroup(null, false);
			base.OnDisable();
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x000149F0 File Offset: 0x00012BF0
		protected override void OnDidApplyAnimationProperties()
		{
			if (this.graphic != null)
			{
				bool flag = !Mathf.Approximately(this.graphic.canvasRenderer.GetColor().a, 0f);
				if (this.m_IsOn != flag)
				{
					this.m_IsOn = flag;
					this.Set(!flag, true);
				}
			}
			base.OnDidApplyAnimationProperties();
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00014A50 File Offset: 0x00012C50
		private void SetToggleGroup(ToggleGroup newGroup, bool setMemberValue)
		{
			if (this.m_Group != null)
			{
				this.m_Group.UnregisterToggle(this);
			}
			if (setMemberValue)
			{
				this.m_Group = newGroup;
			}
			if (newGroup != null && this.IsActive())
			{
				newGroup.RegisterToggle(this);
			}
			if (newGroup != null && this.isOn && this.IsActive())
			{
				newGroup.NotifyToggleOn(this, true);
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x00014ABA File Offset: 0x00012CBA
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x00014AC2 File Offset: 0x00012CC2
		public bool isOn
		{
			get
			{
				return this.m_IsOn;
			}
			set
			{
				this.Set(value, true);
			}
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00014ACC File Offset: 0x00012CCC
		public void SetIsOnWithoutNotify(bool value)
		{
			this.Set(value, false);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00014AD8 File Offset: 0x00012CD8
		private void Set(bool value, bool sendCallback = true)
		{
			if (this.m_IsOn == value)
			{
				return;
			}
			this.m_IsOn = value;
			if (this.m_Group != null && this.m_Group.isActiveAndEnabled && this.IsActive() && (this.m_IsOn || (!this.m_Group.AnyTogglesOn() && !this.m_Group.allowSwitchOff)))
			{
				this.m_IsOn = true;
				this.m_Group.NotifyToggleOn(this, sendCallback);
			}
			this.PlayEffect(this.toggleTransition == Toggle.ToggleTransition.None);
			if (sendCallback)
			{
				UISystemProfilerApi.AddMarker("Toggle.value", this);
				this.onValueChanged.Invoke(this.m_IsOn);
			}
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00014B7D File Offset: 0x00012D7D
		private void PlayEffect(bool instant)
		{
			if (this.graphic == null)
			{
				return;
			}
			this.graphic.CrossFadeAlpha(this.m_IsOn ? 1f : 0f, instant ? 0f : 0.1f, true);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00014BBD File Offset: 0x00012DBD
		protected override void Start()
		{
			this.PlayEffect(true);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00014BC6 File Offset: 0x00012DC6
		private void InternalToggle()
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			this.isOn = !this.isOn;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00014BE8 File Offset: 0x00012DE8
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.InternalToggle();
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00014BF9 File Offset: 0x00012DF9
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.InternalToggle();
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00005DE4 File Offset: 0x00003FE4
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x04000168 RID: 360
		public Toggle.ToggleTransition toggleTransition = Toggle.ToggleTransition.Fade;

		// Token: 0x04000169 RID: 361
		public Graphic graphic;

		// Token: 0x0400016A RID: 362
		[SerializeField]
		private ToggleGroup m_Group;

		// Token: 0x0400016B RID: 363
		public Toggle.ToggleEvent onValueChanged = new Toggle.ToggleEvent();

		// Token: 0x0400016C RID: 364
		[Tooltip("Is the toggle currently on or off?")]
		[SerializeField]
		private bool m_IsOn;

		// Token: 0x020000AC RID: 172
		public enum ToggleTransition
		{
			// Token: 0x040002F5 RID: 757
			None,
			// Token: 0x040002F6 RID: 758
			Fade
		}

		// Token: 0x020000AD RID: 173
		[Serializable]
		public class ToggleEvent : UnityEvent<bool>
		{
		}
	}
}
