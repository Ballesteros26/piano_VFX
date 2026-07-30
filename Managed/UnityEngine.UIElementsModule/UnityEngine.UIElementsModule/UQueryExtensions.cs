using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000082 RID: 130
	public static class UQueryExtensions
	{
		// Token: 0x06000334 RID: 820 RVA: 0x0000B8C8 File Offset: 0x00009AC8
		public static T Q<T>(this VisualElement e, string name = null, params string[] classes) where T : VisualElement
		{
			return e.Query(name, classes).Build().First();
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000B8F4 File Offset: 0x00009AF4
		public static VisualElement Q(this VisualElement e, string name = null, params string[] classes)
		{
			return e.Query(name, classes).Build().First();
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000B920 File Offset: 0x00009B20
		public static T Q<T>(this VisualElement e, string name = null, string className = null) where T : VisualElement
		{
			bool flag = typeof(T) == typeof(VisualElement);
			T t;
			if (flag)
			{
				t = e.Q(name, className) as T;
			}
			else
			{
				bool flag2 = name == null;
				if (flag2)
				{
					bool flag3 = className == null;
					if (flag3)
					{
						UQueryState<VisualElement> uqueryState = UQueryExtensions.SingleElementTypeQuery.RebuildOn(e);
						uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreatePredicate(UQuery.IsOfType<T>.s_Instance);
						t = uqueryState.First() as T;
					}
					else
					{
						UQueryState<VisualElement> uqueryState = UQueryExtensions.SingleElementTypeAndClassQuery.RebuildOn(e);
						uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreatePredicate(UQuery.IsOfType<T>.s_Instance);
						uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[1] = StyleSelectorPart.CreateClass(className);
						t = uqueryState.First() as T;
					}
				}
				else
				{
					bool flag4 = className == null;
					if (flag4)
					{
						UQueryState<VisualElement> uqueryState = UQueryExtensions.SingleElementTypeAndNameQuery.RebuildOn(e);
						uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreatePredicate(UQuery.IsOfType<T>.s_Instance);
						uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[1] = StyleSelectorPart.CreateId(name);
						t = uqueryState.First() as T;
					}
					else
					{
						UQueryState<VisualElement> uqueryState = UQueryExtensions.SingleElementTypeAndNameAndClassQuery.RebuildOn(e);
						uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreatePredicate(UQuery.IsOfType<T>.s_Instance);
						uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[1] = StyleSelectorPart.CreateId(name);
						uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[2] = StyleSelectorPart.CreateClass(className);
						t = uqueryState.First() as T;
					}
				}
			}
			return t;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000BB68 File Offset: 0x00009D68
		internal static T MandatoryQ<T>(this VisualElement e, string name, string className = null) where T : VisualElement
		{
			T t = e.Q(name, className);
			bool flag = t == null;
			if (flag)
			{
				throw new UQueryExtensions.MissingVisualElementException("Element not found: " + name);
			}
			return t;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000BBA4 File Offset: 0x00009DA4
		public static VisualElement Q(this VisualElement e, string name = null, string className = null)
		{
			bool flag = name == null;
			VisualElement visualElement;
			if (flag)
			{
				bool flag2 = className == null;
				if (flag2)
				{
					visualElement = UQueryExtensions.SingleElementEmptyQuery.RebuildOn(e).First();
				}
				else
				{
					UQueryState<VisualElement> uqueryState = UQueryExtensions.SingleElementClassQuery.RebuildOn(e);
					uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreateClass(className);
					visualElement = uqueryState.First();
				}
			}
			else
			{
				bool flag3 = className == null;
				if (flag3)
				{
					UQueryState<VisualElement> uqueryState = UQueryExtensions.SingleElementNameQuery.RebuildOn(e);
					uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreateId(name);
					visualElement = uqueryState.First();
				}
				else
				{
					UQueryState<VisualElement> uqueryState = UQueryExtensions.SingleElementNameAndClassQuery.RebuildOn(e);
					uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreateId(name);
					uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[1] = StyleSelectorPart.CreateClass(className);
					visualElement = uqueryState.First();
				}
			}
			return visualElement;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000BCD8 File Offset: 0x00009ED8
		internal static VisualElement MandatoryQ(this VisualElement e, string name, string className = null)
		{
			VisualElement visualElement = e.Q(name, className);
			bool flag = visualElement == null;
			if (flag)
			{
				throw new UQueryExtensions.MissingVisualElementException("Element not found: " + name);
			}
			return visualElement;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000BD10 File Offset: 0x00009F10
		public static UQueryBuilder<VisualElement> Query(this VisualElement e, string name = null, params string[] classes)
		{
			return e.Query(name, classes);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000BD2C File Offset: 0x00009F2C
		public static UQueryBuilder<VisualElement> Query(this VisualElement e, string name = null, string className = null)
		{
			return e.Query(name, className);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000BD48 File Offset: 0x00009F48
		public static UQueryBuilder<T> Query<T>(this VisualElement e, string name = null, params string[] classes) where T : VisualElement
		{
			return new UQueryBuilder<VisualElement>(e).OfType<T>(name, classes);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000BD6C File Offset: 0x00009F6C
		public static UQueryBuilder<T> Query<T>(this VisualElement e, string name = null, string className = null) where T : VisualElement
		{
			return new UQueryBuilder<VisualElement>(e).OfType<T>(name, className);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000BD90 File Offset: 0x00009F90
		public static UQueryBuilder<VisualElement> Query(this VisualElement e)
		{
			return new UQueryBuilder<VisualElement>(e);
		}

		// Token: 0x0400017B RID: 379
		private static UQueryState<VisualElement> SingleElementEmptyQuery = new UQueryBuilder<VisualElement>(null).Build();

		// Token: 0x0400017C RID: 380
		private static UQueryState<VisualElement> SingleElementNameQuery = new UQueryBuilder<VisualElement>(null).Name(string.Empty).Build();

		// Token: 0x0400017D RID: 381
		private static UQueryState<VisualElement> SingleElementClassQuery = new UQueryBuilder<VisualElement>(null).Class(string.Empty).Build();

		// Token: 0x0400017E RID: 382
		private static UQueryState<VisualElement> SingleElementNameAndClassQuery = new UQueryBuilder<VisualElement>(null).Name(string.Empty).Class(string.Empty).Build();

		// Token: 0x0400017F RID: 383
		private static UQueryState<VisualElement> SingleElementTypeQuery = new UQueryBuilder<VisualElement>(null).SingleBaseType().Build();

		// Token: 0x04000180 RID: 384
		private static UQueryState<VisualElement> SingleElementTypeAndNameQuery = new UQueryBuilder<VisualElement>(null).SingleBaseType().Name(string.Empty).Build();

		// Token: 0x04000181 RID: 385
		private static UQueryState<VisualElement> SingleElementTypeAndClassQuery = new UQueryBuilder<VisualElement>(null).SingleBaseType().Class(string.Empty).Build();

		// Token: 0x04000182 RID: 386
		private static UQueryState<VisualElement> SingleElementTypeAndNameAndClassQuery = new UQueryBuilder<VisualElement>(null).SingleBaseType().Name(string.Empty).Class(string.Empty)
			.Build();

		// Token: 0x02000083 RID: 131
		private class MissingVisualElementException : Exception
		{
			// Token: 0x06000340 RID: 832 RVA: 0x0000BED5 File Offset: 0x0000A0D5
			public MissingVisualElementException()
			{
			}

			// Token: 0x06000341 RID: 833 RVA: 0x0000BEDF File Offset: 0x0000A0DF
			public MissingVisualElementException(string message)
				: base(message)
			{
			}
		}
	}
}
