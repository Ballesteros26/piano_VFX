using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000002 RID: 2
	public class BindableElement : VisualElement, IBindable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public IBinding binding { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002069 File Offset: 0x00000269
		public string bindingPath { get; set; }

		// Token: 0x02000003 RID: 3
		public new class UxmlFactory : UxmlFactory<BindableElement, BindableElement.UxmlTraits>
		{
		}

		// Token: 0x02000004 RID: 4
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x06000007 RID: 7 RVA: 0x00002084 File Offset: 0x00000284
			public UxmlTraits()
			{
				this.m_PropertyPath = new UxmlStringAttributeDescription
				{
					name = "binding-path"
				};
			}

			// Token: 0x06000008 RID: 8 RVA: 0x000020A8 File Offset: 0x000002A8
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				string valueFromBag = this.m_PropertyPath.GetValueFromBag(bag, cc);
				bool flag = !string.IsNullOrEmpty(valueFromBag);
				if (flag)
				{
					IBindable bindable = ve as IBindable;
					bool flag2 = bindable != null;
					if (flag2)
					{
						bindable.bindingPath = valueFromBag;
					}
				}
			}

			// Token: 0x04000003 RID: 3
			private UxmlStringAttributeDescription m_PropertyPath;
		}
	}
}
