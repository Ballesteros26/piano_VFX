using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI.CoroutineTween;

namespace UnityEngine.UI
{
	// Token: 0x0200000E RID: 14
	[AddComponentMenu("UI/Dropdown", 35)]
	[RequireComponent(typeof(RectTransform))]
	public class Dropdown : Selectable, IPointerClickHandler, IEventSystemHandler, ISubmitHandler, ICancelHandler
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003F41 File Offset: 0x00002141
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00003F49 File Offset: 0x00002149
		public RectTransform template
		{
			get
			{
				return this.m_Template;
			}
			set
			{
				this.m_Template = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003F58 File Offset: 0x00002158
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00003F60 File Offset: 0x00002160
		public Text captionText
		{
			get
			{
				return this.m_CaptionText;
			}
			set
			{
				this.m_CaptionText = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003F6F File Offset: 0x0000216F
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00003F77 File Offset: 0x00002177
		public Image captionImage
		{
			get
			{
				return this.m_CaptionImage;
			}
			set
			{
				this.m_CaptionImage = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003F86 File Offset: 0x00002186
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00003F8E File Offset: 0x0000218E
		public Text itemText
		{
			get
			{
				return this.m_ItemText;
			}
			set
			{
				this.m_ItemText = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003F9D File Offset: 0x0000219D
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00003FA5 File Offset: 0x000021A5
		public Image itemImage
		{
			get
			{
				return this.m_ItemImage;
			}
			set
			{
				this.m_ItemImage = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003FB4 File Offset: 0x000021B4
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00003FC1 File Offset: 0x000021C1
		public List<Dropdown.OptionData> options
		{
			get
			{
				return this.m_Options.options;
			}
			set
			{
				this.m_Options.options = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003FD5 File Offset: 0x000021D5
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00003FDD File Offset: 0x000021DD
		public Dropdown.DropdownEvent onValueChanged
		{
			get
			{
				return this.m_OnValueChanged;
			}
			set
			{
				this.m_OnValueChanged = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003FE6 File Offset: 0x000021E6
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00003FEE File Offset: 0x000021EE
		public float alphaFadeSpeed
		{
			get
			{
				return this.m_AlphaFadeSpeed;
			}
			set
			{
				this.m_AlphaFadeSpeed = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003FF7 File Offset: 0x000021F7
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00003FFF File Offset: 0x000021FF
		public int value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.Set(value, true);
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004009 File Offset: 0x00002209
		public void SetValueWithoutNotify(int input)
		{
			this.Set(input, false);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004014 File Offset: 0x00002214
		private void Set(int value, bool sendCallback = true)
		{
			if (Application.isPlaying && (value == this.m_Value || this.options.Count == 0))
			{
				return;
			}
			this.m_Value = Mathf.Clamp(value, 0, this.options.Count - 1);
			this.RefreshShownValue();
			if (sendCallback)
			{
				UISystemProfilerApi.AddMarker("Dropdown.value", this);
				this.m_OnValueChanged.Invoke(this.m_Value);
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000407E File Offset: 0x0000227E
		protected Dropdown()
		{
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000040B4 File Offset: 0x000022B4
		protected override void Awake()
		{
			this.m_AlphaTweenRunner = new TweenRunner<FloatTween>();
			this.m_AlphaTweenRunner.Init(this);
			if (this.m_CaptionImage)
			{
				this.m_CaptionImage.enabled = this.m_CaptionImage.sprite != null;
			}
			if (this.m_Template)
			{
				this.m_Template.gameObject.SetActive(false);
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000411F File Offset: 0x0000231F
		protected override void Start()
		{
			base.Start();
			this.RefreshShownValue();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000412D File Offset: 0x0000232D
		protected override void OnDisable()
		{
			this.ImmediateDestroyDropdownList();
			if (this.m_Blocker != null)
			{
				this.DestroyBlocker(this.m_Blocker);
			}
			this.m_Blocker = null;
			base.OnDisable();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000415C File Offset: 0x0000235C
		public void RefreshShownValue()
		{
			Dropdown.OptionData optionData = Dropdown.s_NoOptionData;
			if (this.options.Count > 0)
			{
				optionData = this.options[Mathf.Clamp(this.m_Value, 0, this.options.Count - 1)];
			}
			if (this.m_CaptionText)
			{
				if (optionData != null && optionData.text != null)
				{
					this.m_CaptionText.text = optionData.text;
				}
				else
				{
					this.m_CaptionText.text = "";
				}
			}
			if (this.m_CaptionImage)
			{
				if (optionData != null)
				{
					this.m_CaptionImage.sprite = optionData.image;
				}
				else
				{
					this.m_CaptionImage.sprite = null;
				}
				this.m_CaptionImage.enabled = this.m_CaptionImage.sprite != null;
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004228 File Offset: 0x00002428
		public void AddOptions(List<Dropdown.OptionData> options)
		{
			this.options.AddRange(options);
			this.RefreshShownValue();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000423C File Offset: 0x0000243C
		public void AddOptions(List<string> options)
		{
			for (int i = 0; i < options.Count; i++)
			{
				this.options.Add(new Dropdown.OptionData(options[i]));
			}
			this.RefreshShownValue();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004278 File Offset: 0x00002478
		public void AddOptions(List<Sprite> options)
		{
			for (int i = 0; i < options.Count; i++)
			{
				this.options.Add(new Dropdown.OptionData(options[i]));
			}
			this.RefreshShownValue();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000042B3 File Offset: 0x000024B3
		public void ClearOptions()
		{
			this.options.Clear();
			this.m_Value = 0;
			this.RefreshShownValue();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000042D0 File Offset: 0x000024D0
		private void SetupTemplate()
		{
			this.validTemplate = false;
			if (!this.m_Template)
			{
				Debug.LogError("The dropdown template is not assigned. The template needs to be assigned and must have a child GameObject with a Toggle component serving as the item.", this);
				return;
			}
			GameObject gameObject = this.m_Template.gameObject;
			gameObject.SetActive(true);
			Toggle componentInChildren = this.m_Template.GetComponentInChildren<Toggle>();
			this.validTemplate = true;
			if (!componentInChildren || componentInChildren.transform == this.template)
			{
				this.validTemplate = false;
				Debug.LogError("The dropdown template is not valid. The template must have a child GameObject with a Toggle component serving as the item.", this.template);
			}
			else if (!(componentInChildren.transform.parent is RectTransform))
			{
				this.validTemplate = false;
				Debug.LogError("The dropdown template is not valid. The child GameObject with a Toggle component (the item) must have a RectTransform on its parent.", this.template);
			}
			else if (this.itemText != null && !this.itemText.transform.IsChildOf(componentInChildren.transform))
			{
				this.validTemplate = false;
				Debug.LogError("The dropdown template is not valid. The Item Text must be on the item GameObject or children of it.", this.template);
			}
			else if (this.itemImage != null && !this.itemImage.transform.IsChildOf(componentInChildren.transform))
			{
				this.validTemplate = false;
				Debug.LogError("The dropdown template is not valid. The Item Image must be on the item GameObject or children of it.", this.template);
			}
			if (!this.validTemplate)
			{
				gameObject.SetActive(false);
				return;
			}
			Dropdown.DropdownItem dropdownItem = componentInChildren.gameObject.AddComponent<Dropdown.DropdownItem>();
			dropdownItem.text = this.m_ItemText;
			dropdownItem.image = this.m_ItemImage;
			dropdownItem.toggle = componentInChildren;
			dropdownItem.rectTransform = (RectTransform)componentInChildren.transform;
			Canvas canvas = null;
			Transform transform = this.m_Template.parent;
			while (transform != null)
			{
				canvas = transform.GetComponent<Canvas>();
				if (canvas != null)
				{
					break;
				}
				transform = transform.parent;
			}
			Canvas orAddComponent = Dropdown.GetOrAddComponent<Canvas>(gameObject);
			orAddComponent.overrideSorting = true;
			orAddComponent.sortingOrder = 30000;
			if (canvas != null)
			{
				Component[] components = canvas.GetComponents<BaseRaycaster>();
				Component[] array = components;
				for (int i = 0; i < array.Length; i++)
				{
					Type type = array[i].GetType();
					if (gameObject.GetComponent(type) == null)
					{
						gameObject.AddComponent(type);
					}
				}
			}
			else
			{
				Dropdown.GetOrAddComponent<GraphicRaycaster>(gameObject);
			}
			Dropdown.GetOrAddComponent<CanvasGroup>(gameObject);
			gameObject.SetActive(false);
			this.validTemplate = true;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004500 File Offset: 0x00002700
		private static T GetOrAddComponent<T>(GameObject go) where T : Component
		{
			T t = go.GetComponent<T>();
			if (!t)
			{
				t = go.AddComponent<T>();
			}
			return t;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004529 File Offset: 0x00002729
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			this.Show();
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004529 File Offset: 0x00002729
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.Show();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004531 File Offset: 0x00002731
		public virtual void OnCancel(BaseEventData eventData)
		{
			this.Hide();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000453C File Offset: 0x0000273C
		public void Show()
		{
			if (!this.IsActive() || !this.IsInteractable() || this.m_Dropdown != null)
			{
				return;
			}
			List<Canvas> list = ListPool<Canvas>.Get();
			base.gameObject.GetComponentsInParent<Canvas>(false, list);
			if (list.Count == 0)
			{
				return;
			}
			Canvas canvas = list[list.Count - 1];
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].isRootCanvas)
				{
					canvas = list[i];
					break;
				}
			}
			ListPool<Canvas>.Release(list);
			if (!this.validTemplate)
			{
				this.SetupTemplate();
				if (!this.validTemplate)
				{
					return;
				}
			}
			this.m_Template.gameObject.SetActive(true);
			this.m_Template.GetComponent<Canvas>().sortingLayerID = canvas.sortingLayerID;
			this.m_Dropdown = this.CreateDropdownList(this.m_Template.gameObject);
			this.m_Dropdown.name = "Dropdown List";
			this.m_Dropdown.SetActive(true);
			RectTransform rectTransform = this.m_Dropdown.transform as RectTransform;
			rectTransform.SetParent(this.m_Template.transform.parent, false);
			Dropdown.DropdownItem componentInChildren = this.m_Dropdown.GetComponentInChildren<Dropdown.DropdownItem>();
			RectTransform rectTransform2 = componentInChildren.rectTransform.parent.gameObject.transform as RectTransform;
			componentInChildren.rectTransform.gameObject.SetActive(true);
			Rect rect = rectTransform2.rect;
			Rect rect2 = componentInChildren.rectTransform.rect;
			Vector2 vector = rect2.min - rect.min + componentInChildren.rectTransform.localPosition;
			Vector2 vector2 = rect2.max - rect.max + componentInChildren.rectTransform.localPosition;
			Vector2 size = rect2.size;
			this.m_Items.Clear();
			Toggle toggle = null;
			for (int j = 0; j < this.options.Count; j++)
			{
				Dropdown.OptionData optionData = this.options[j];
				Dropdown.DropdownItem item = this.AddItem(optionData, this.value == j, componentInChildren, this.m_Items);
				if (!(item == null))
				{
					item.toggle.isOn = this.value == j;
					item.toggle.onValueChanged.AddListener(delegate(bool x)
					{
						this.OnSelectItem(item.toggle);
					});
					if (item.toggle.isOn)
					{
						item.toggle.Select();
					}
					if (toggle != null)
					{
						Navigation navigation = toggle.navigation;
						Navigation navigation2 = item.toggle.navigation;
						navigation.mode = Navigation.Mode.Explicit;
						navigation2.mode = Navigation.Mode.Explicit;
						navigation.selectOnDown = item.toggle;
						navigation.selectOnRight = item.toggle;
						navigation2.selectOnLeft = toggle;
						navigation2.selectOnUp = toggle;
						toggle.navigation = navigation;
						item.toggle.navigation = navigation2;
					}
					toggle = item.toggle;
				}
			}
			Vector2 sizeDelta = rectTransform2.sizeDelta;
			sizeDelta.y = size.y * (float)this.m_Items.Count + vector.y - vector2.y;
			rectTransform2.sizeDelta = sizeDelta;
			float num = rectTransform.rect.height - rectTransform2.rect.height;
			if (num > 0f)
			{
				rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y - num);
			}
			Vector3[] array = new Vector3[4];
			rectTransform.GetWorldCorners(array);
			RectTransform rectTransform3 = canvas.transform as RectTransform;
			Rect rect3 = rectTransform3.rect;
			for (int k = 0; k < 2; k++)
			{
				bool flag = false;
				for (int l = 0; l < 4; l++)
				{
					Vector3 vector3 = rectTransform3.InverseTransformPoint(array[l]);
					if ((vector3[k] < rect3.min[k] && !Mathf.Approximately(vector3[k], rect3.min[k])) || (vector3[k] > rect3.max[k] && !Mathf.Approximately(vector3[k], rect3.max[k])))
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					RectTransformUtility.FlipLayoutOnAxis(rectTransform, k, false, false);
				}
			}
			for (int m = 0; m < this.m_Items.Count; m++)
			{
				RectTransform rectTransform4 = this.m_Items[m].rectTransform;
				rectTransform4.anchorMin = new Vector2(rectTransform4.anchorMin.x, 0f);
				rectTransform4.anchorMax = new Vector2(rectTransform4.anchorMax.x, 0f);
				rectTransform4.anchoredPosition = new Vector2(rectTransform4.anchoredPosition.x, vector.y + size.y * (float)(this.m_Items.Count - 1 - m) + size.y * rectTransform4.pivot.y);
				rectTransform4.sizeDelta = new Vector2(rectTransform4.sizeDelta.x, size.y);
			}
			this.AlphaFadeList(this.m_AlphaFadeSpeed, 0f, 1f);
			this.m_Template.gameObject.SetActive(false);
			componentInChildren.gameObject.SetActive(false);
			this.m_Blocker = this.CreateBlocker(canvas);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004B28 File Offset: 0x00002D28
		protected virtual GameObject CreateBlocker(Canvas rootCanvas)
		{
			GameObject gameObject = new GameObject("Blocker");
			RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
			rectTransform.SetParent(rootCanvas.transform, false);
			rectTransform.anchorMin = Vector3.zero;
			rectTransform.anchorMax = Vector3.one;
			rectTransform.sizeDelta = Vector2.zero;
			Canvas canvas = gameObject.AddComponent<Canvas>();
			canvas.overrideSorting = true;
			Canvas component = this.m_Dropdown.GetComponent<Canvas>();
			canvas.sortingLayerID = component.sortingLayerID;
			canvas.sortingOrder = component.sortingOrder - 1;
			Canvas canvas2 = null;
			Transform transform = this.m_Template.parent;
			while (transform != null)
			{
				canvas2 = transform.GetComponent<Canvas>();
				if (canvas2 != null)
				{
					break;
				}
				transform = transform.parent;
			}
			if (canvas2 != null)
			{
				Component[] components = canvas2.GetComponents<BaseRaycaster>();
				Component[] array = components;
				for (int i = 0; i < array.Length; i++)
				{
					Type type = array[i].GetType();
					if (gameObject.GetComponent(type) == null)
					{
						gameObject.AddComponent(type);
					}
				}
			}
			else
			{
				Dropdown.GetOrAddComponent<GraphicRaycaster>(gameObject);
			}
			gameObject.AddComponent<Image>().color = Color.clear;
			gameObject.AddComponent<Button>().onClick.AddListener(new UnityAction(this.Hide));
			return gameObject;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004C62 File Offset: 0x00002E62
		protected virtual void DestroyBlocker(GameObject blocker)
		{
			Object.Destroy(blocker);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004C6A File Offset: 0x00002E6A
		protected virtual GameObject CreateDropdownList(GameObject template)
		{
			return Object.Instantiate<GameObject>(template);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004C62 File Offset: 0x00002E62
		protected virtual void DestroyDropdownList(GameObject dropdownList)
		{
			Object.Destroy(dropdownList);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004C72 File Offset: 0x00002E72
		protected virtual Dropdown.DropdownItem CreateItem(Dropdown.DropdownItem itemTemplate)
		{
			return Object.Instantiate<Dropdown.DropdownItem>(itemTemplate);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004C7A File Offset: 0x00002E7A
		protected virtual void DestroyItem(Dropdown.DropdownItem item)
		{
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004C7C File Offset: 0x00002E7C
		private Dropdown.DropdownItem AddItem(Dropdown.OptionData data, bool selected, Dropdown.DropdownItem itemTemplate, List<Dropdown.DropdownItem> items)
		{
			Dropdown.DropdownItem dropdownItem = this.CreateItem(itemTemplate);
			dropdownItem.rectTransform.SetParent(itemTemplate.rectTransform.parent, false);
			dropdownItem.gameObject.SetActive(true);
			dropdownItem.gameObject.name = "Item " + items.Count + ((data.text != null) ? (": " + data.text) : "");
			if (dropdownItem.toggle != null)
			{
				dropdownItem.toggle.isOn = false;
			}
			if (dropdownItem.text)
			{
				dropdownItem.text.text = data.text;
			}
			if (dropdownItem.image)
			{
				dropdownItem.image.sprite = data.image;
				dropdownItem.image.enabled = dropdownItem.image.sprite != null;
			}
			items.Add(dropdownItem);
			return dropdownItem;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004D70 File Offset: 0x00002F70
		private void AlphaFadeList(float duration, float alpha)
		{
			CanvasGroup component = this.m_Dropdown.GetComponent<CanvasGroup>();
			this.AlphaFadeList(duration, component.alpha, alpha);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004D98 File Offset: 0x00002F98
		private void AlphaFadeList(float duration, float start, float end)
		{
			if (end.Equals(start))
			{
				return;
			}
			FloatTween floatTween = new FloatTween
			{
				duration = duration,
				startValue = start,
				targetValue = end
			};
			floatTween.AddOnChangedCallback(new UnityAction<float>(this.SetAlpha));
			floatTween.ignoreTimeScale = true;
			this.m_AlphaTweenRunner.StartTween(floatTween);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004DF9 File Offset: 0x00002FF9
		private void SetAlpha(float alpha)
		{
			if (!this.m_Dropdown)
			{
				return;
			}
			this.m_Dropdown.GetComponent<CanvasGroup>().alpha = alpha;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004E1C File Offset: 0x0000301C
		public void Hide()
		{
			if (this.m_Dropdown != null)
			{
				this.AlphaFadeList(this.m_AlphaFadeSpeed, 0f);
				if (this.IsActive())
				{
					base.StartCoroutine(this.DelayedDestroyDropdownList(this.m_AlphaFadeSpeed));
				}
			}
			if (this.m_Blocker != null)
			{
				this.DestroyBlocker(this.m_Blocker);
			}
			this.m_Blocker = null;
			this.Select();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004E8A File Offset: 0x0000308A
		private IEnumerator DelayedDestroyDropdownList(float delay)
		{
			yield return new WaitForSecondsRealtime(delay);
			this.ImmediateDestroyDropdownList();
			yield break;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004EA0 File Offset: 0x000030A0
		private void ImmediateDestroyDropdownList()
		{
			for (int i = 0; i < this.m_Items.Count; i++)
			{
				if (this.m_Items[i] != null)
				{
					this.DestroyItem(this.m_Items[i]);
				}
			}
			this.m_Items.Clear();
			if (this.m_Dropdown != null)
			{
				this.DestroyDropdownList(this.m_Dropdown);
			}
			this.m_Dropdown = null;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004F18 File Offset: 0x00003118
		private void OnSelectItem(Toggle toggle)
		{
			if (!toggle.isOn)
			{
				toggle.isOn = true;
			}
			int num = -1;
			Transform transform = toggle.transform;
			Transform parent = transform.parent;
			for (int i = 0; i < parent.childCount; i++)
			{
				if (parent.GetChild(i) == transform)
				{
					num = i - 1;
					break;
				}
			}
			if (num < 0)
			{
				return;
			}
			this.value = num;
			this.Hide();
		}

		// Token: 0x04000030 RID: 48
		[SerializeField]
		private RectTransform m_Template;

		// Token: 0x04000031 RID: 49
		[SerializeField]
		private Text m_CaptionText;

		// Token: 0x04000032 RID: 50
		[SerializeField]
		private Image m_CaptionImage;

		// Token: 0x04000033 RID: 51
		[Space]
		[SerializeField]
		private Text m_ItemText;

		// Token: 0x04000034 RID: 52
		[SerializeField]
		private Image m_ItemImage;

		// Token: 0x04000035 RID: 53
		[Space]
		[SerializeField]
		private int m_Value;

		// Token: 0x04000036 RID: 54
		[Space]
		[SerializeField]
		private Dropdown.OptionDataList m_Options = new Dropdown.OptionDataList();

		// Token: 0x04000037 RID: 55
		[Space]
		[SerializeField]
		private Dropdown.DropdownEvent m_OnValueChanged = new Dropdown.DropdownEvent();

		// Token: 0x04000038 RID: 56
		[SerializeField]
		private float m_AlphaFadeSpeed = 0.15f;

		// Token: 0x04000039 RID: 57
		private GameObject m_Dropdown;

		// Token: 0x0400003A RID: 58
		private GameObject m_Blocker;

		// Token: 0x0400003B RID: 59
		private List<Dropdown.DropdownItem> m_Items = new List<Dropdown.DropdownItem>();

		// Token: 0x0400003C RID: 60
		private TweenRunner<FloatTween> m_AlphaTweenRunner;

		// Token: 0x0400003D RID: 61
		private bool validTemplate;

		// Token: 0x0400003E RID: 62
		private static Dropdown.OptionData s_NoOptionData = new Dropdown.OptionData();

		// Token: 0x02000079 RID: 121
		protected internal class DropdownItem : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, ICancelHandler
		{
			// Token: 0x170001B0 RID: 432
			// (get) Token: 0x0600062F RID: 1583 RVA: 0x00019955 File Offset: 0x00017B55
			// (set) Token: 0x06000630 RID: 1584 RVA: 0x0001995D File Offset: 0x00017B5D
			public Text text
			{
				get
				{
					return this.m_Text;
				}
				set
				{
					this.m_Text = value;
				}
			}

			// Token: 0x170001B1 RID: 433
			// (get) Token: 0x06000631 RID: 1585 RVA: 0x00019966 File Offset: 0x00017B66
			// (set) Token: 0x06000632 RID: 1586 RVA: 0x0001996E File Offset: 0x00017B6E
			public Image image
			{
				get
				{
					return this.m_Image;
				}
				set
				{
					this.m_Image = value;
				}
			}

			// Token: 0x170001B2 RID: 434
			// (get) Token: 0x06000633 RID: 1587 RVA: 0x00019977 File Offset: 0x00017B77
			// (set) Token: 0x06000634 RID: 1588 RVA: 0x0001997F File Offset: 0x00017B7F
			public RectTransform rectTransform
			{
				get
				{
					return this.m_RectTransform;
				}
				set
				{
					this.m_RectTransform = value;
				}
			}

			// Token: 0x170001B3 RID: 435
			// (get) Token: 0x06000635 RID: 1589 RVA: 0x00019988 File Offset: 0x00017B88
			// (set) Token: 0x06000636 RID: 1590 RVA: 0x00019990 File Offset: 0x00017B90
			public Toggle toggle
			{
				get
				{
					return this.m_Toggle;
				}
				set
				{
					this.m_Toggle = value;
				}
			}

			// Token: 0x06000637 RID: 1591 RVA: 0x00019999 File Offset: 0x00017B99
			public virtual void OnPointerEnter(PointerEventData eventData)
			{
				EventSystem.current.SetSelectedGameObject(base.gameObject);
			}

			// Token: 0x06000638 RID: 1592 RVA: 0x000199AC File Offset: 0x00017BAC
			public virtual void OnCancel(BaseEventData eventData)
			{
				Dropdown componentInParent = base.GetComponentInParent<Dropdown>();
				if (componentInParent)
				{
					componentInParent.Hide();
				}
			}

			// Token: 0x0400022F RID: 559
			[SerializeField]
			private Text m_Text;

			// Token: 0x04000230 RID: 560
			[SerializeField]
			private Image m_Image;

			// Token: 0x04000231 RID: 561
			[SerializeField]
			private RectTransform m_RectTransform;

			// Token: 0x04000232 RID: 562
			[SerializeField]
			private Toggle m_Toggle;
		}

		// Token: 0x0200007A RID: 122
		[Serializable]
		public class OptionData
		{
			// Token: 0x170001B4 RID: 436
			// (get) Token: 0x0600063A RID: 1594 RVA: 0x000199CE File Offset: 0x00017BCE
			// (set) Token: 0x0600063B RID: 1595 RVA: 0x000199D6 File Offset: 0x00017BD6
			public string text
			{
				get
				{
					return this.m_Text;
				}
				set
				{
					this.m_Text = value;
				}
			}

			// Token: 0x170001B5 RID: 437
			// (get) Token: 0x0600063C RID: 1596 RVA: 0x000199DF File Offset: 0x00017BDF
			// (set) Token: 0x0600063D RID: 1597 RVA: 0x000199E7 File Offset: 0x00017BE7
			public Sprite image
			{
				get
				{
					return this.m_Image;
				}
				set
				{
					this.m_Image = value;
				}
			}

			// Token: 0x0600063E RID: 1598 RVA: 0x00005114 File Offset: 0x00003314
			public OptionData()
			{
			}

			// Token: 0x0600063F RID: 1599 RVA: 0x000199F0 File Offset: 0x00017BF0
			public OptionData(string text)
			{
				this.text = text;
			}

			// Token: 0x06000640 RID: 1600 RVA: 0x000199FF File Offset: 0x00017BFF
			public OptionData(Sprite image)
			{
				this.image = image;
			}

			// Token: 0x06000641 RID: 1601 RVA: 0x00019A0E File Offset: 0x00017C0E
			public OptionData(string text, Sprite image)
			{
				this.text = text;
				this.image = image;
			}

			// Token: 0x04000233 RID: 563
			[SerializeField]
			private string m_Text;

			// Token: 0x04000234 RID: 564
			[SerializeField]
			private Sprite m_Image;
		}

		// Token: 0x0200007B RID: 123
		[Serializable]
		public class OptionDataList
		{
			// Token: 0x170001B6 RID: 438
			// (get) Token: 0x06000642 RID: 1602 RVA: 0x00019A24 File Offset: 0x00017C24
			// (set) Token: 0x06000643 RID: 1603 RVA: 0x00019A2C File Offset: 0x00017C2C
			public List<Dropdown.OptionData> options
			{
				get
				{
					return this.m_Options;
				}
				set
				{
					this.m_Options = value;
				}
			}

			// Token: 0x06000644 RID: 1604 RVA: 0x00019A35 File Offset: 0x00017C35
			public OptionDataList()
			{
				this.options = new List<Dropdown.OptionData>();
			}

			// Token: 0x04000235 RID: 565
			[SerializeField]
			private List<Dropdown.OptionData> m_Options;
		}

		// Token: 0x0200007C RID: 124
		[Serializable]
		public class DropdownEvent : UnityEvent<int>
		{
		}
	}
}
