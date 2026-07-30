using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200020C RID: 524
	internal static class VisualElementFactoryRegistry
	{
		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x000398C4 File Offset: 0x00037AC4
		internal static Dictionary<string, List<IUxmlFactory>> factories
		{
			get
			{
				bool flag = VisualElementFactoryRegistry.s_Factories == null;
				if (flag)
				{
					VisualElementFactoryRegistry.s_Factories = new Dictionary<string, List<IUxmlFactory>>();
					VisualElementFactoryRegistry.RegisterEngineFactories();
				}
				return VisualElementFactoryRegistry.s_Factories;
			}
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x000398FC File Offset: 0x00037AFC
		internal static void RegisterFactory(IUxmlFactory factory)
		{
			List<IUxmlFactory> list;
			bool flag = VisualElementFactoryRegistry.factories.TryGetValue(factory.uxmlQualifiedName, ref list);
			if (flag)
			{
				foreach (IUxmlFactory uxmlFactory in list)
				{
					bool flag2 = uxmlFactory.GetType() == factory.GetType();
					if (flag2)
					{
						throw new ArgumentException("A factory of this type was already registered");
					}
				}
				list.Add(factory);
			}
			else
			{
				list = new List<IUxmlFactory>();
				list.Add(factory);
				VisualElementFactoryRegistry.factories.Add(factory.uxmlQualifiedName, list);
			}
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x000399AC File Offset: 0x00037BAC
		internal static bool TryGetValue(string fullTypeName, out List<IUxmlFactory> factoryList)
		{
			factoryList = null;
			return VisualElementFactoryRegistry.factories.TryGetValue(fullTypeName, ref factoryList);
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x000399D0 File Offset: 0x00037BD0
		private static void RegisterEngineFactories()
		{
			IUxmlFactory[] array = new IUxmlFactory[]
			{
				new UxmlRootElementFactory(),
				new UxmlTemplateFactory(),
				new UxmlStyleFactory(),
				new UxmlAttributeOverridesFactory(),
				new Button.UxmlFactory(),
				new VisualElement.UxmlFactory(),
				new IMGUIContainer.UxmlFactory(),
				new Image.UxmlFactory(),
				new Label.UxmlFactory(),
				new RepeatButton.UxmlFactory(),
				new ScrollView.UxmlFactory(),
				new Scroller.UxmlFactory(),
				new Slider.UxmlFactory(),
				new SliderInt.UxmlFactory(),
				new MinMaxSlider.UxmlFactory(),
				new Toggle.UxmlFactory(),
				new TextField.UxmlFactory(),
				new TemplateContainer.UxmlFactory(),
				new Box.UxmlFactory(),
				new HelpBox.UxmlFactory(),
				new PopupWindow.UxmlFactory(),
				new ListView.UxmlFactory(),
				new TreeView.UxmlFactory(),
				new Foldout.UxmlFactory(),
				new BindableElement.UxmlFactory()
			};
			foreach (IUxmlFactory uxmlFactory in array)
			{
				VisualElementFactoryRegistry.RegisterFactory(uxmlFactory);
			}
		}

		// Token: 0x0400068E RID: 1678
		private static Dictionary<string, List<IUxmlFactory>> s_Factories;
	}
}
