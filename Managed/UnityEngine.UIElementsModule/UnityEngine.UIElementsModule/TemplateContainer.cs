using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000061 RID: 97
	public class TemplateContainer : BindableElement
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600023A RID: 570 RVA: 0x000085AE File Offset: 0x000067AE
		// (set) Token: 0x0600023B RID: 571 RVA: 0x000085B6 File Offset: 0x000067B6
		public string templateId { get; private set; }

		// Token: 0x0600023C RID: 572 RVA: 0x000085BF File Offset: 0x000067BF
		public TemplateContainer()
			: this(null)
		{
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000085CA File Offset: 0x000067CA
		public TemplateContainer(string templateId)
		{
			this.templateId = templateId;
			this.m_ContentContainer = this;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600023E RID: 574 RVA: 0x000085E4 File Offset: 0x000067E4
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_ContentContainer;
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000085FC File Offset: 0x000067FC
		internal void SetContentContainer(VisualElement content)
		{
			this.m_ContentContainer = content;
		}

		// Token: 0x04000128 RID: 296
		private VisualElement m_ContentContainer;

		// Token: 0x02000062 RID: 98
		public new class UxmlFactory : UxmlFactory<TemplateContainer, TemplateContainer.UxmlTraits>
		{
			// Token: 0x17000083 RID: 131
			// (get) Token: 0x06000240 RID: 576 RVA: 0x00008606 File Offset: 0x00006806
			public override string uxmlName
			{
				get
				{
					return "Instance";
				}
			}

			// Token: 0x17000084 RID: 132
			// (get) Token: 0x06000241 RID: 577 RVA: 0x0000860D File Offset: 0x0000680D
			public override string uxmlQualifiedName
			{
				get
				{
					return this.uxmlNamespace + "." + this.uxmlName;
				}
			}

			// Token: 0x04000129 RID: 297
			internal const string k_ElementName = "Instance";
		}

		// Token: 0x02000063 RID: 99
		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			// Token: 0x17000085 RID: 133
			// (get) Token: 0x06000243 RID: 579 RVA: 0x00008630 File Offset: 0x00006830
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}

			// Token: 0x06000244 RID: 580 RVA: 0x00008650 File Offset: 0x00006850
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				TemplateContainer templateContainer = (TemplateContainer)ve;
				templateContainer.templateId = this.m_Template.GetValueFromBag(bag, cc);
				VisualTreeAsset visualTreeAsset = cc.visualTreeAsset;
				VisualTreeAsset visualTreeAsset2 = ((visualTreeAsset != null) ? visualTreeAsset.ResolveTemplate(templateContainer.templateId) : null);
				bool flag = visualTreeAsset2 == null;
				if (flag)
				{
					templateContainer.Add(new Label(string.Format("Unknown Template: '{0}'", templateContainer.templateId)));
				}
				else
				{
					TemplateAsset templateAsset = bag as TemplateAsset;
					List<TemplateAsset.AttributeOverride> list = ((templateAsset != null) ? templateAsset.attributeOverrides : null);
					List<TemplateAsset.AttributeOverride> attributeOverrides = cc.attributeOverrides;
					List<TemplateAsset.AttributeOverride> list2 = null;
					bool flag2 = list != null || attributeOverrides != null;
					if (flag2)
					{
						list2 = new List<TemplateAsset.AttributeOverride>();
						bool flag3 = attributeOverrides != null;
						if (flag3)
						{
							list2.AddRange(attributeOverrides);
						}
						bool flag4 = list != null;
						if (flag4)
						{
							list2.AddRange(list);
						}
					}
					visualTreeAsset2.CloneTree(ve, cc.slotInsertionPoints, list2);
				}
				bool flag5 = visualTreeAsset2 == null;
				if (flag5)
				{
					Debug.LogErrorFormat("Could not resolve template with name '{0}'", new object[] { templateContainer.templateId });
				}
			}

			// Token: 0x0400012A RID: 298
			internal const string k_TemplateAttributeName = "template";

			// Token: 0x0400012B RID: 299
			private UxmlStringAttributeDescription m_Template = new UxmlStringAttributeDescription
			{
				name = "template",
				use = UxmlAttributeDescription.Use.Required
			};
		}
	}
}
