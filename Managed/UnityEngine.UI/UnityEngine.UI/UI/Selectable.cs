using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000034 RID: 52
	[AddComponentMenu("UI/Selectable", 70)]
	[ExecuteAlways]
	[SelectionBase]
	[DisallowMultipleComponent]
	public class Selectable : UIBehaviour, IMoveHandler, IEventSystemHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000398 RID: 920 RVA: 0x00012434 File Offset: 0x00010634
		public static Selectable[] allSelectablesArray
		{
			get
			{
				Selectable[] array = new Selectable[Selectable.s_SelectableCount];
				Array.Copy(Selectable.s_Selectables, array, Selectable.s_SelectableCount);
				return array;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0001245D File Offset: 0x0001065D
		public static int allSelectableCount
		{
			get
			{
				return Selectable.s_SelectableCount;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00012464 File Offset: 0x00010664
		[Obsolete("Replaced with allSelectablesArray to have better performance when disabling a element", false)]
		public static List<Selectable> allSelectables
		{
			get
			{
				return new List<Selectable>(Selectable.allSelectablesArray);
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00012470 File Offset: 0x00010670
		public static int AllSelectablesNoAlloc(Selectable[] selectables)
		{
			int num = ((selectables.Length < Selectable.s_SelectableCount) ? selectables.Length : Selectable.s_SelectableCount);
			Array.Copy(Selectable.s_Selectables, selectables, num);
			return num;
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0001249F File Offset: 0x0001069F
		// (set) Token: 0x0600039D RID: 925 RVA: 0x000124A7 File Offset: 0x000106A7
		public Navigation navigation
		{
			get
			{
				return this.m_Navigation;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Navigation>(ref this.m_Navigation, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600039E RID: 926 RVA: 0x000124BD File Offset: 0x000106BD
		// (set) Token: 0x0600039F RID: 927 RVA: 0x000124C5 File Offset: 0x000106C5
		public Selectable.Transition transition
		{
			get
			{
				return this.m_Transition;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Selectable.Transition>(ref this.m_Transition, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x000124DB File Offset: 0x000106DB
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x000124E3 File Offset: 0x000106E3
		public ColorBlock colors
		{
			get
			{
				return this.m_Colors;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<ColorBlock>(ref this.m_Colors, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x000124F9 File Offset: 0x000106F9
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x00012501 File Offset: 0x00010701
		public SpriteState spriteState
		{
			get
			{
				return this.m_SpriteState;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<SpriteState>(ref this.m_SpriteState, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x00012517 File Offset: 0x00010717
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x0001251F File Offset: 0x0001071F
		public AnimationTriggers animationTriggers
		{
			get
			{
				return this.m_AnimationTriggers;
			}
			set
			{
				if (SetPropertyUtility.SetClass<AnimationTriggers>(ref this.m_AnimationTriggers, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x00012535 File Offset: 0x00010735
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x0001253D File Offset: 0x0001073D
		public Graphic targetGraphic
		{
			get
			{
				return this.m_TargetGraphic;
			}
			set
			{
				if (SetPropertyUtility.SetClass<Graphic>(ref this.m_TargetGraphic, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x00012553 File Offset: 0x00010753
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x0001255C File Offset: 0x0001075C
		public bool interactable
		{
			get
			{
				return this.m_Interactable;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<bool>(ref this.m_Interactable, value))
				{
					if (!this.m_Interactable && EventSystem.current != null && EventSystem.current.currentSelectedGameObject == base.gameObject)
					{
						EventSystem.current.SetSelectedGameObject(null);
					}
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060003AA RID: 938 RVA: 0x000125B4 File Offset: 0x000107B4
		// (set) Token: 0x060003AB RID: 939 RVA: 0x000125BC File Offset: 0x000107BC
		private bool isPointerInside { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003AC RID: 940 RVA: 0x000125C5 File Offset: 0x000107C5
		// (set) Token: 0x060003AD RID: 941 RVA: 0x000125CD File Offset: 0x000107CD
		private bool isPointerDown { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003AE RID: 942 RVA: 0x000125D6 File Offset: 0x000107D6
		// (set) Token: 0x060003AF RID: 943 RVA: 0x000125DE File Offset: 0x000107DE
		private bool hasSelection { get; set; }

		// Token: 0x060003B0 RID: 944 RVA: 0x000125E8 File Offset: 0x000107E8
		protected Selectable()
		{
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x00012643 File Offset: 0x00010843
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x00012650 File Offset: 0x00010850
		public Image image
		{
			get
			{
				return this.m_TargetGraphic as Image;
			}
			set
			{
				this.m_TargetGraphic = value;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x00012659 File Offset: 0x00010859
		public Animator animator
		{
			get
			{
				return base.GetComponent<Animator>();
			}
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00012661 File Offset: 0x00010861
		protected override void Awake()
		{
			if (this.m_TargetGraphic == null)
			{
				this.m_TargetGraphic = base.GetComponent<Graphic>();
			}
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00012680 File Offset: 0x00010880
		protected override void OnCanvasGroupChanged()
		{
			bool flag = true;
			Transform transform = base.transform;
			while (transform != null)
			{
				transform.GetComponents<CanvasGroup>(this.m_CanvasGroupCache);
				bool flag2 = false;
				for (int i = 0; i < this.m_CanvasGroupCache.Count; i++)
				{
					if (!this.m_CanvasGroupCache[i].interactable)
					{
						flag = false;
						flag2 = true;
					}
					if (this.m_CanvasGroupCache[i].ignoreParentGroups)
					{
						flag2 = true;
					}
				}
				if (flag2)
				{
					break;
				}
				transform = transform.parent;
			}
			if (flag != this.m_GroupsAllowInteraction)
			{
				this.m_GroupsAllowInteraction = flag;
				this.OnSetProperty();
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00012711 File Offset: 0x00010911
		public virtual bool IsInteractable()
		{
			return this.m_GroupsAllowInteraction && this.m_Interactable;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00012723 File Offset: 0x00010923
		protected override void OnDidApplyAnimationProperties()
		{
			this.OnSetProperty();
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0001272C File Offset: 0x0001092C
		protected override void OnEnable()
		{
			base.OnEnable();
			if (Selectable.s_SelectableCount == Selectable.s_Selectables.Length)
			{
				Selectable[] array = new Selectable[Selectable.s_Selectables.Length * 2];
				Array.Copy(Selectable.s_Selectables, array, Selectable.s_Selectables.Length);
				Selectable.s_Selectables = array;
			}
			this.m_CurrentIndex = Selectable.s_SelectableCount;
			Selectable.s_Selectables[this.m_CurrentIndex] = this;
			Selectable.s_SelectableCount++;
			this.isPointerDown = false;
			this.DoStateTransition(this.currentSelectionState, true);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x000127AC File Offset: 0x000109AC
		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			this.OnCanvasGroupChanged();
		}

		// Token: 0x060003BA RID: 954 RVA: 0x000127BA File Offset: 0x000109BA
		private void OnSetProperty()
		{
			this.DoStateTransition(this.currentSelectionState, false);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x000127CC File Offset: 0x000109CC
		protected override void OnDisable()
		{
			Selectable.s_SelectableCount--;
			Selectable.s_Selectables[Selectable.s_SelectableCount].m_CurrentIndex = this.m_CurrentIndex;
			Selectable.s_Selectables[this.m_CurrentIndex] = Selectable.s_Selectables[Selectable.s_SelectableCount];
			Selectable.s_Selectables[Selectable.s_SelectableCount] = null;
			this.InstantClearState();
			base.OnDisable();
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0001282A File Offset: 0x00010A2A
		protected Selectable.SelectionState currentSelectionState
		{
			get
			{
				if (!this.IsInteractable())
				{
					return Selectable.SelectionState.Disabled;
				}
				if (this.isPointerDown)
				{
					return Selectable.SelectionState.Pressed;
				}
				if (this.hasSelection)
				{
					return Selectable.SelectionState.Selected;
				}
				if (this.isPointerInside)
				{
					return Selectable.SelectionState.Highlighted;
				}
				return Selectable.SelectionState.Normal;
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00012858 File Offset: 0x00010A58
		protected virtual void InstantClearState()
		{
			string normalTrigger = this.m_AnimationTriggers.normalTrigger;
			this.isPointerInside = false;
			this.isPointerDown = false;
			this.hasSelection = false;
			switch (this.m_Transition)
			{
			case Selectable.Transition.ColorTint:
				this.StartColorTween(Color.white, true);
				return;
			case Selectable.Transition.SpriteSwap:
				this.DoSpriteSwap(null);
				return;
			case Selectable.Transition.Animation:
				this.TriggerAnimation(normalTrigger);
				return;
			default:
				return;
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x000128C0 File Offset: 0x00010AC0
		protected virtual void DoStateTransition(Selectable.SelectionState state, bool instant)
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			Color color;
			Sprite sprite;
			string text;
			switch (state)
			{
			case Selectable.SelectionState.Normal:
				color = this.m_Colors.normalColor;
				sprite = null;
				text = this.m_AnimationTriggers.normalTrigger;
				break;
			case Selectable.SelectionState.Highlighted:
				color = this.m_Colors.highlightedColor;
				sprite = this.m_SpriteState.highlightedSprite;
				text = this.m_AnimationTriggers.highlightedTrigger;
				break;
			case Selectable.SelectionState.Pressed:
				color = this.m_Colors.pressedColor;
				sprite = this.m_SpriteState.pressedSprite;
				text = this.m_AnimationTriggers.pressedTrigger;
				break;
			case Selectable.SelectionState.Selected:
				color = this.m_Colors.selectedColor;
				sprite = this.m_SpriteState.selectedSprite;
				text = this.m_AnimationTriggers.selectedTrigger;
				break;
			case Selectable.SelectionState.Disabled:
				color = this.m_Colors.disabledColor;
				sprite = this.m_SpriteState.disabledSprite;
				text = this.m_AnimationTriggers.disabledTrigger;
				break;
			default:
				color = Color.black;
				sprite = null;
				text = string.Empty;
				break;
			}
			switch (this.m_Transition)
			{
			case Selectable.Transition.ColorTint:
				this.StartColorTween(color * this.m_Colors.colorMultiplier, instant);
				return;
			case Selectable.Transition.SpriteSwap:
				this.DoSpriteSwap(sprite);
				return;
			case Selectable.Transition.Animation:
				this.TriggerAnimation(text);
				return;
			default:
				return;
			}
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00012A08 File Offset: 0x00010C08
		public Selectable FindSelectable(Vector3 dir)
		{
			dir = dir.normalized;
			Vector3 vector = Quaternion.Inverse(base.transform.rotation) * dir;
			Vector3 vector2 = base.transform.TransformPoint(Selectable.GetPointOnRectEdge(base.transform as RectTransform, vector));
			float num = float.NegativeInfinity;
			Selectable selectable = null;
			for (int i = 0; i < Selectable.s_SelectableCount; i++)
			{
				Selectable selectable2 = Selectable.s_Selectables[i];
				if (!(selectable2 == this) && selectable2.IsInteractable() && selectable2.navigation.mode != Navigation.Mode.None)
				{
					RectTransform rectTransform = selectable2.transform as RectTransform;
					Vector3 vector3 = ((rectTransform != null) ? rectTransform.rect.center : Vector3.zero);
					Vector3 vector4 = selectable2.transform.TransformPoint(vector3) - vector2;
					float num2 = Vector3.Dot(dir, vector4);
					if (num2 > 0f)
					{
						float num3 = num2 / vector4.sqrMagnitude;
						if (num3 > num)
						{
							num = num3;
							selectable = selectable2;
						}
					}
				}
			}
			return selectable;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00012B28 File Offset: 0x00010D28
		private static Vector3 GetPointOnRectEdge(RectTransform rect, Vector2 dir)
		{
			if (rect == null)
			{
				return Vector3.zero;
			}
			if (dir != Vector2.zero)
			{
				dir /= Mathf.Max(Mathf.Abs(dir.x), Mathf.Abs(dir.y));
			}
			dir = rect.rect.center + Vector2.Scale(rect.rect.size, dir * 0.5f);
			return dir;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00012BAD File Offset: 0x00010DAD
		private void Navigate(AxisEventData eventData, Selectable sel)
		{
			if (sel != null && sel.IsActive())
			{
				eventData.selectedObject = sel.gameObject;
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00012BCC File Offset: 0x00010DCC
		public virtual Selectable FindSelectableOnLeft()
		{
			if (this.m_Navigation.mode == Navigation.Mode.Explicit)
			{
				return this.m_Navigation.selectOnLeft;
			}
			if ((this.m_Navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None)
			{
				return this.FindSelectable(base.transform.rotation * Vector3.left);
			}
			return null;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00012C20 File Offset: 0x00010E20
		public virtual Selectable FindSelectableOnRight()
		{
			if (this.m_Navigation.mode == Navigation.Mode.Explicit)
			{
				return this.m_Navigation.selectOnRight;
			}
			if ((this.m_Navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None)
			{
				return this.FindSelectable(base.transform.rotation * Vector3.right);
			}
			return null;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00012C74 File Offset: 0x00010E74
		public virtual Selectable FindSelectableOnUp()
		{
			if (this.m_Navigation.mode == Navigation.Mode.Explicit)
			{
				return this.m_Navigation.selectOnUp;
			}
			if ((this.m_Navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None)
			{
				return this.FindSelectable(base.transform.rotation * Vector3.up);
			}
			return null;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00012CC8 File Offset: 0x00010EC8
		public virtual Selectable FindSelectableOnDown()
		{
			if (this.m_Navigation.mode == Navigation.Mode.Explicit)
			{
				return this.m_Navigation.selectOnDown;
			}
			if ((this.m_Navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None)
			{
				return this.FindSelectable(base.transform.rotation * Vector3.down);
			}
			return null;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00012D1C File Offset: 0x00010F1C
		public virtual void OnMove(AxisEventData eventData)
		{
			switch (eventData.moveDir)
			{
			case MoveDirection.Left:
				this.Navigate(eventData, this.FindSelectableOnLeft());
				return;
			case MoveDirection.Up:
				this.Navigate(eventData, this.FindSelectableOnUp());
				return;
			case MoveDirection.Right:
				this.Navigate(eventData, this.FindSelectableOnRight());
				return;
			case MoveDirection.Down:
				this.Navigate(eventData, this.FindSelectableOnDown());
				return;
			default:
				return;
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00012D7E File Offset: 0x00010F7E
		private void StartColorTween(Color targetColor, bool instant)
		{
			if (this.m_TargetGraphic == null)
			{
				return;
			}
			this.m_TargetGraphic.CrossFadeColor(targetColor, instant ? 0f : this.m_Colors.fadeDuration, true, true);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00012DB2 File Offset: 0x00010FB2
		private void DoSpriteSwap(Sprite newSprite)
		{
			if (this.image == null)
			{
				return;
			}
			this.image.overrideSprite = newSprite;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00012DD0 File Offset: 0x00010FD0
		private void TriggerAnimation(string triggername)
		{
			if (this.transition != Selectable.Transition.Animation || this.animator == null || !this.animator.isActiveAndEnabled || !this.animator.hasBoundPlayables || string.IsNullOrEmpty(triggername))
			{
				return;
			}
			this.animator.ResetTrigger(this.m_AnimationTriggers.normalTrigger);
			this.animator.ResetTrigger(this.m_AnimationTriggers.highlightedTrigger);
			this.animator.ResetTrigger(this.m_AnimationTriggers.pressedTrigger);
			this.animator.ResetTrigger(this.m_AnimationTriggers.selectedTrigger);
			this.animator.ResetTrigger(this.m_AnimationTriggers.disabledTrigger);
			this.animator.SetTrigger(triggername);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00012E91 File Offset: 0x00011091
		protected bool IsHighlighted()
		{
			return this.IsActive() && this.IsInteractable() && (this.isPointerInside && !this.isPointerDown) && !this.hasSelection;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00012EC0 File Offset: 0x000110C0
		protected bool IsPressed()
		{
			return this.IsActive() && this.IsInteractable() && this.isPointerDown;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00012EDA File Offset: 0x000110DA
		private void EvaluateAndTransitionToSelectionState()
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			this.DoStateTransition(this.currentSelectionState, false);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00012EFC File Offset: 0x000110FC
		public virtual void OnPointerDown(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (this.IsInteractable() && this.navigation.mode != Navigation.Mode.None && EventSystem.current != null)
			{
				EventSystem.current.SetSelectedGameObject(base.gameObject, eventData);
			}
			this.isPointerDown = true;
			this.EvaluateAndTransitionToSelectionState();
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00012F55 File Offset: 0x00011155
		public virtual void OnPointerUp(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.isPointerDown = false;
			this.EvaluateAndTransitionToSelectionState();
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00012F6D File Offset: 0x0001116D
		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			this.isPointerInside = true;
			this.EvaluateAndTransitionToSelectionState();
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00012F7C File Offset: 0x0001117C
		public virtual void OnPointerExit(PointerEventData eventData)
		{
			this.isPointerInside = false;
			this.EvaluateAndTransitionToSelectionState();
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00012F8B File Offset: 0x0001118B
		public virtual void OnSelect(BaseEventData eventData)
		{
			this.hasSelection = true;
			this.EvaluateAndTransitionToSelectionState();
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00012F9A File Offset: 0x0001119A
		public virtual void OnDeselect(BaseEventData eventData)
		{
			this.hasSelection = false;
			this.EvaluateAndTransitionToSelectionState();
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00012FA9 File Offset: 0x000111A9
		public virtual void Select()
		{
			if (EventSystem.current == null || EventSystem.current.alreadySelecting)
			{
				return;
			}
			EventSystem.current.SetSelectedGameObject(base.gameObject);
		}

		// Token: 0x0400013D RID: 317
		protected static Selectable[] s_Selectables = new Selectable[10];

		// Token: 0x0400013E RID: 318
		protected static int s_SelectableCount = 0;

		// Token: 0x0400013F RID: 319
		[FormerlySerializedAs("navigation")]
		[SerializeField]
		private Navigation m_Navigation = Navigation.defaultNavigation;

		// Token: 0x04000140 RID: 320
		[FormerlySerializedAs("transition")]
		[SerializeField]
		private Selectable.Transition m_Transition = Selectable.Transition.ColorTint;

		// Token: 0x04000141 RID: 321
		[FormerlySerializedAs("colors")]
		[SerializeField]
		private ColorBlock m_Colors = ColorBlock.defaultColorBlock;

		// Token: 0x04000142 RID: 322
		[FormerlySerializedAs("spriteState")]
		[SerializeField]
		private SpriteState m_SpriteState;

		// Token: 0x04000143 RID: 323
		[FormerlySerializedAs("animationTriggers")]
		[SerializeField]
		private AnimationTriggers m_AnimationTriggers = new AnimationTriggers();

		// Token: 0x04000144 RID: 324
		[Tooltip("Can the Selectable be interacted with?")]
		[SerializeField]
		private bool m_Interactable = true;

		// Token: 0x04000145 RID: 325
		[FormerlySerializedAs("highlightGraphic")]
		[FormerlySerializedAs("m_HighlightGraphic")]
		[SerializeField]
		private Graphic m_TargetGraphic;

		// Token: 0x04000146 RID: 326
		private bool m_GroupsAllowInteraction = true;

		// Token: 0x04000147 RID: 327
		protected int m_CurrentIndex = -1;

		// Token: 0x0400014B RID: 331
		private readonly List<CanvasGroup> m_CanvasGroupCache = new List<CanvasGroup>();

		// Token: 0x020000A6 RID: 166
		public enum Transition
		{
			// Token: 0x040002D8 RID: 728
			None,
			// Token: 0x040002D9 RID: 729
			ColorTint,
			// Token: 0x040002DA RID: 730
			SpriteSwap,
			// Token: 0x040002DB RID: 731
			Animation
		}

		// Token: 0x020000A7 RID: 167
		protected enum SelectionState
		{
			// Token: 0x040002DD RID: 733
			Normal,
			// Token: 0x040002DE RID: 734
			Highlighted,
			// Token: 0x040002DF RID: 735
			Pressed,
			// Token: 0x040002E0 RID: 736
			Selected,
			// Token: 0x040002E1 RID: 737
			Disabled
		}
	}
}
