using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x02000017 RID: 23
	[AddComponentMenu("UI/Dropdown - TextMeshPro", 35)]
	[RequireComponent(typeof(RectTransform))]
	public class TMP_Dropdown : Selectable, IPointerClickHandler, IEventSystemHandler, ISubmitHandler, ICancelHandler
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003949 File Offset: 0x00001B49
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00003951 File Offset: 0x00001B51
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

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003960 File Offset: 0x00001B60
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00003968 File Offset: 0x00001B68
		public TMP_Text captionText
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

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003977 File Offset: 0x00001B77
		// (set) Token: 0x06000089 RID: 137 RVA: 0x0000397F File Offset: 0x00001B7F
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

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600008A RID: 138 RVA: 0x0000398E File Offset: 0x00001B8E
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00003996 File Offset: 0x00001B96
		public Graphic placeholder
		{
			get
			{
				return this.m_Placeholder;
			}
			set
			{
				this.m_Placeholder = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000039A5 File Offset: 0x00001BA5
		// (set) Token: 0x0600008D RID: 141 RVA: 0x000039AD File Offset: 0x00001BAD
		public TMP_Text itemText
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

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000039BC File Offset: 0x00001BBC
		// (set) Token: 0x0600008F RID: 143 RVA: 0x000039C4 File Offset: 0x00001BC4
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

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000039D3 File Offset: 0x00001BD3
		// (set) Token: 0x06000091 RID: 145 RVA: 0x000039E0 File Offset: 0x00001BE0
		public List<TMP_Dropdown.OptionData> options
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

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000039F4 File Offset: 0x00001BF4
		// (set) Token: 0x06000093 RID: 147 RVA: 0x000039FC File Offset: 0x00001BFC
		public TMP_Dropdown.DropdownEvent onValueChanged
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

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003A05 File Offset: 0x00001C05
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00003A0D File Offset: 0x00001C0D
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

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003A16 File Offset: 0x00001C16
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00003A1E File Offset: 0x00001C1E
		public int value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.SetValue(value, true);
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003A28 File Offset: 0x00001C28
		public void SetValueWithoutNotify(int input)
		{
			this.SetValue(input, false);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003A34 File Offset: 0x00001C34
		private void SetValue(int value, bool sendCallback = true)
		{
			if (Application.isPlaying && (value == this.m_Value || this.options.Count == 0))
			{
				return;
			}
			this.m_Value = Mathf.Clamp(value, this.m_Placeholder ? (-1) : 0, this.options.Count - 1);
			this.RefreshShownValue();
			if (sendCallback)
			{
				UISystemProfilerApi.AddMarker("Dropdown.value", this);
				this.m_OnValueChanged.Invoke(this.m_Value);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003AAE File Offset: 0x00001CAE
		public bool IsExpanded
		{
			get
			{
				return this.m_Dropdown != null;
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003ABC File Offset: 0x00001CBC
		protected TMP_Dropdown()
		{
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003AF0 File Offset: 0x00001CF0
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

		// Token: 0x0600009D RID: 157 RVA: 0x00003B5B File Offset: 0x00001D5B
		protected override void Start()
		{
			base.Start();
			this.RefreshShownValue();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003B69 File Offset: 0x00001D69
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

		// Token: 0x0600009F RID: 159 RVA: 0x00003B98 File Offset: 0x00001D98
		public void RefreshShownValue()
		{
			TMP_Dropdown.OptionData optionData = TMP_Dropdown.s_NoOptionData;
			if (this.options.Count > 0 && this.m_Value >= 0)
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
			if (this.m_Placeholder)
			{
				this.m_Placeholder.enabled = this.options.Count == 0 || this.m_Value == -1;
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003C9E File Offset: 0x00001E9E
		public void AddOptions(List<TMP_Dropdown.OptionData> options)
		{
			this.options.AddRange(options);
			this.RefreshShownValue();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003CB4 File Offset: 0x00001EB4
		public void AddOptions(List<string> options)
		{
			for (int i = 0; i < options.Count; i++)
			{
				this.options.Add(new TMP_Dropdown.OptionData(options[i]));
			}
			this.RefreshShownValue();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003CF0 File Offset: 0x00001EF0
		public void AddOptions(List<Sprite> options)
		{
			for (int i = 0; i < options.Count; i++)
			{
				this.options.Add(new TMP_Dropdown.OptionData(options[i]));
			}
			this.RefreshShownValue();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003D2B File Offset: 0x00001F2B
		public void ClearOptions()
		{
			this.options.Clear();
			this.m_Value = (this.m_Placeholder ? (-1) : 0);
			this.RefreshShownValue();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003D58 File Offset: 0x00001F58
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
			TMP_Dropdown.DropdownItem dropdownItem = componentInChildren.gameObject.AddComponent<TMP_Dropdown.DropdownItem>();
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
			Canvas orAddComponent = TMP_Dropdown.GetOrAddComponent<Canvas>(gameObject);
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
				TMP_Dropdown.GetOrAddComponent<GraphicRaycaster>(gameObject);
			}
			TMP_Dropdown.GetOrAddComponent<CanvasGroup>(gameObject);
			gameObject.SetActive(false);
			this.validTemplate = true;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003F88 File Offset: 0x00002188
		private static T GetOrAddComponent<T>(GameObject go) where T : Component
		{
			T t = go.GetComponent<T>();
			if (!t)
			{
				t = go.AddComponent<T>();
			}
			return t;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003FB1 File Offset: 0x000021B1
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			this.Show();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003FB1 File Offset: 0x000021B1
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.Show();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003FB9 File Offset: 0x000021B9
		public virtual void OnCancel(BaseEventData eventData)
		{
			this.Hide();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003FC4 File Offset: 0x000021C4
		public void Show()
		{
			if (!this.IsActive() || !this.IsInteractable() || this.m_Dropdown != null)
			{
				return;
			}
			List<Canvas> list = TMP_ListPool<Canvas>.Get();
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
			TMP_ListPool<Canvas>.Release(list);
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
			TMP_Dropdown.DropdownItem componentInChildren = this.m_Dropdown.GetComponentInChildren<TMP_Dropdown.DropdownItem>();
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
				TMP_Dropdown.OptionData optionData = this.options[j];
				TMP_Dropdown.DropdownItem item = this.AddItem(optionData, this.value == j, componentInChildren, this.m_Items);
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

		// Token: 0x060000AA RID: 170 RVA: 0x000045B0 File Offset: 0x000027B0
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
				TMP_Dropdown.GetOrAddComponent<GraphicRaycaster>(gameObject);
			}
			gameObject.AddComponent<Image>().color = Color.clear;
			gameObject.AddComponent<Button>().onClick.AddListener(new UnityAction(this.Hide));
			return gameObject;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000046EA File Offset: 0x000028EA
		protected virtual void DestroyBlocker(GameObject blocker)
		{
			global::UnityEngine.Object.Destroy(blocker);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000046F2 File Offset: 0x000028F2
		protected virtual GameObject CreateDropdownList(GameObject template)
		{
			return global::UnityEngine.Object.Instantiate<GameObject>(template);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000046EA File Offset: 0x000028EA
		protected virtual void DestroyDropdownList(GameObject dropdownList)
		{
			global::UnityEngine.Object.Destroy(dropdownList);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000046FA File Offset: 0x000028FA
		protected virtual TMP_Dropdown.DropdownItem CreateItem(TMP_Dropdown.DropdownItem itemTemplate)
		{
			return global::UnityEngine.Object.Instantiate<TMP_Dropdown.DropdownItem>(itemTemplate);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000027BA File Offset: 0x000009BA
		protected virtual void DestroyItem(TMP_Dropdown.DropdownItem item)
		{
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004704 File Offset: 0x00002904
		private TMP_Dropdown.DropdownItem AddItem(TMP_Dropdown.OptionData data, bool selected, TMP_Dropdown.DropdownItem itemTemplate, List<TMP_Dropdown.DropdownItem> items)
		{
			TMP_Dropdown.DropdownItem dropdownItem = this.CreateItem(itemTemplate);
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

		// Token: 0x060000B1 RID: 177 RVA: 0x000047F8 File Offset: 0x000029F8
		private void AlphaFadeList(float duration, float alpha)
		{
			CanvasGroup component = this.m_Dropdown.GetComponent<CanvasGroup>();
			this.AlphaFadeList(duration, component.alpha, alpha);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004820 File Offset: 0x00002A20
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

		// Token: 0x060000B3 RID: 179 RVA: 0x00004881 File Offset: 0x00002A81
		private void SetAlpha(float alpha)
		{
			if (!this.m_Dropdown)
			{
				return;
			}
			this.m_Dropdown.GetComponent<CanvasGroup>().alpha = alpha;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000048A4 File Offset: 0x00002AA4
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

		// Token: 0x060000B5 RID: 181 RVA: 0x00004912 File Offset: 0x00002B12
		private IEnumerator DelayedDestroyDropdownList(float delay)
		{
			yield return new WaitForSecondsRealtime(delay);
			this.ImmediateDestroyDropdownList();
			yield break;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004928 File Offset: 0x00002B28
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

		// Token: 0x060000B7 RID: 183 RVA: 0x000049A0 File Offset: 0x00002BA0
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

		// Token: 0x04000072 RID: 114
		[SerializeField]
		private RectTransform m_Template;

		// Token: 0x04000073 RID: 115
		[SerializeField]
		private TMP_Text m_CaptionText;

		// Token: 0x04000074 RID: 116
		[SerializeField]
		private Image m_CaptionImage;

		// Token: 0x04000075 RID: 117
		[SerializeField]
		private Graphic m_Placeholder;

		// Token: 0x04000076 RID: 118
		[Space]
		[SerializeField]
		private TMP_Text m_ItemText;

		// Token: 0x04000077 RID: 119
		[SerializeField]
		private Image m_ItemImage;

		// Token: 0x04000078 RID: 120
		[Space]
		[SerializeField]
		private int m_Value;

		// Token: 0x04000079 RID: 121
		[Space]
		[SerializeField]
		private TMP_Dropdown.OptionDataList m_Options = new TMP_Dropdown.OptionDataList();

		// Token: 0x0400007A RID: 122
		[Space]
		[SerializeField]
		private TMP_Dropdown.DropdownEvent m_OnValueChanged = new TMP_Dropdown.DropdownEvent();

		// Token: 0x0400007B RID: 123
		[SerializeField]
		private float m_AlphaFadeSpeed = 0.15f;

		// Token: 0x0400007C RID: 124
		private GameObject m_Dropdown;

		// Token: 0x0400007D RID: 125
		private GameObject m_Blocker;

		// Token: 0x0400007E RID: 126
		private List<TMP_Dropdown.DropdownItem> m_Items = new List<TMP_Dropdown.DropdownItem>();

		// Token: 0x0400007F RID: 127
		private TweenRunner<FloatTween> m_AlphaTweenRunner;

		// Token: 0x04000080 RID: 128
		private bool validTemplate;

		// Token: 0x04000081 RID: 129
		private static TMP_Dropdown.OptionData s_NoOptionData = new TMP_Dropdown.OptionData();

		// Token: 0x0200007B RID: 123
		protected internal class DropdownItem : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, ICancelHandler
		{
			// Token: 0x17000157 RID: 343
			// (get) Token: 0x06000591 RID: 1425 RVA: 0x00036EAE File Offset: 0x000350AE
			// (set) Token: 0x06000592 RID: 1426 RVA: 0x00036EB6 File Offset: 0x000350B6
			public TMP_Text text
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

			// Token: 0x17000158 RID: 344
			// (get) Token: 0x06000593 RID: 1427 RVA: 0x00036EBF File Offset: 0x000350BF
			// (set) Token: 0x06000594 RID: 1428 RVA: 0x00036EC7 File Offset: 0x000350C7
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

			// Token: 0x17000159 RID: 345
			// (get) Token: 0x06000595 RID: 1429 RVA: 0x00036ED0 File Offset: 0x000350D0
			// (set) Token: 0x06000596 RID: 1430 RVA: 0x00036ED8 File Offset: 0x000350D8
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

			// Token: 0x1700015A RID: 346
			// (get) Token: 0x06000597 RID: 1431 RVA: 0x00036EE1 File Offset: 0x000350E1
			// (set) Token: 0x06000598 RID: 1432 RVA: 0x00036EE9 File Offset: 0x000350E9
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

			// Token: 0x06000599 RID: 1433 RVA: 0x00036EF2 File Offset: 0x000350F2
			public virtual void OnPointerEnter(PointerEventData eventData)
			{
				EventSystem.current.SetSelectedGameObject(base.gameObject);
			}

			// Token: 0x0600059A RID: 1434 RVA: 0x00036F04 File Offset: 0x00035104
			public virtual void OnCancel(BaseEventData eventData)
			{
				TMP_Dropdown componentInParent = base.GetComponentInParent<TMP_Dropdown>();
				if (componentInParent)
				{
					componentInParent.Hide();
				}
			}

			// Token: 0x04000527 RID: 1319
			[SerializeField]
			private TMP_Text m_Text;

			// Token: 0x04000528 RID: 1320
			[SerializeField]
			private Image m_Image;

			// Token: 0x04000529 RID: 1321
			[SerializeField]
			private RectTransform m_RectTransform;

			// Token: 0x0400052A RID: 1322
			[SerializeField]
			private Toggle m_Toggle;
		}

		// Token: 0x0200007C RID: 124
		[Serializable]
		public class OptionData
		{
			// Token: 0x1700015B RID: 347
			// (get) Token: 0x0600059C RID: 1436 RVA: 0x00036F26 File Offset: 0x00035126
			// (set) Token: 0x0600059D RID: 1437 RVA: 0x00036F2E File Offset: 0x0003512E
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

			// Token: 0x1700015C RID: 348
			// (get) Token: 0x0600059E RID: 1438 RVA: 0x00036F37 File Offset: 0x00035137
			// (set) Token: 0x0600059F RID: 1439 RVA: 0x00036F3F File Offset: 0x0003513F
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

			// Token: 0x060005A0 RID: 1440 RVA: 0x00002DE9 File Offset: 0x00000FE9
			public OptionData()
			{
			}

			// Token: 0x060005A1 RID: 1441 RVA: 0x00036F48 File Offset: 0x00035148
			public OptionData(string text)
			{
				this.text = text;
			}

			// Token: 0x060005A2 RID: 1442 RVA: 0x00036F57 File Offset: 0x00035157
			public OptionData(Sprite image)
			{
				this.image = image;
			}

			// Token: 0x060005A3 RID: 1443 RVA: 0x00036F66 File Offset: 0x00035166
			public OptionData(string text, Sprite image)
			{
				this.text = text;
				this.image = image;
			}

			// Token: 0x0400052B RID: 1323
			[SerializeField]
			private string m_Text;

			// Token: 0x0400052C RID: 1324
			[SerializeField]
			private Sprite m_Image;
		}

		// Token: 0x0200007D RID: 125
		[Serializable]
		public class OptionDataList
		{
			// Token: 0x1700015D RID: 349
			// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00036F7C File Offset: 0x0003517C
			// (set) Token: 0x060005A5 RID: 1445 RVA: 0x00036F84 File Offset: 0x00035184
			public List<TMP_Dropdown.OptionData> options
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

			// Token: 0x060005A6 RID: 1446 RVA: 0x00036F8D File Offset: 0x0003518D
			public OptionDataList()
			{
				this.options = new List<TMP_Dropdown.OptionData>();
			}

			// Token: 0x0400052D RID: 1325
			[SerializeField]
			private List<TMP_Dropdown.OptionData> m_Options;
		}

		// Token: 0x0200007E RID: 126
		[Serializable]
		public class DropdownEvent : UnityEvent<int>
		{
		}
	}
}
