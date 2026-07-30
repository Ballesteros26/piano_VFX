using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Web.UI
{
	/// <summary>Used by data source controls when implementing the members defined by the <see cref="T:System.ComponentModel.IListSource" /> interface. This class cannot be inherited.</summary>
	// Token: 0x020001E2 RID: 482
	public static class ListSourceHelper
	{
		/// <summary>Indicates whether the specified data source control contains a collection of data source view objects.</summary>
		/// <returns>true if the data source control contains a collection of data source view objects; otherwise, false.</returns>
		/// <param name="dataSource">An <see cref="T:System.Web.UI.IDataSource" /> that specifies the data source control to test for associated data source view objects.</param>
		// Token: 0x06001390 RID: 5008 RVA: 0x000352AF File Offset: 0x000334AF
		public static bool ContainsListCollection(IDataSource dataSource)
		{
			return dataSource.GetViewNames().Count > 0;
		}

		/// <summary>Retrieves an <see cref="T:System.Collections.IList" /> collection of data source objects.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> of one <see cref="T:System.Web.UI.IDataSource" />, if the <see cref="T:System.Web.UI.IDataSource" /> has one or more associated <see cref="T:System.Web.UI.DataSourceView" /> objects; otherwise, returns null. </returns>
		/// <param name="dataSource">An <see cref="T:System.Web.UI.IDataSource" /> that contains one or more associated <see cref="T:System.Web.UI.DataSourceView" /> objects, which are retrieved by a call to <see cref="M:System.Web.UI.DataSourceControl.GetViewNames" />.</param>
		// Token: 0x06001391 RID: 5009 RVA: 0x000352BF File Offset: 0x000334BF
		public static IList GetList(IDataSource dataSource)
		{
			if (dataSource.GetViewNames().Count == 0)
			{
				return null;
			}
			return new ListSourceHelper.ListSourceList { dataSource };
		}

		// Token: 0x020001E3 RID: 483
		private sealed class ListSourceList : List<IDataSource>, ITypedList
		{
			// Token: 0x06001392 RID: 5010 RVA: 0x000352DC File Offset: 0x000334DC
			PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
			{
				ICollection viewNames = base[0].GetViewNames();
				PropertyDescriptor[] array = new PropertyDescriptor[viewNames.Count];
				int num = 0;
				foreach (object obj in viewNames)
				{
					string text = (string)obj;
					array[num++] = new ListSourceHelper.ListSourcePropertyDescriptor(text, null);
				}
				return new PropertyDescriptorCollection(array);
			}

			// Token: 0x06001393 RID: 5011 RVA: 0x0000EE9B File Offset: 0x0000D09B
			string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
			{
				return string.Empty;
			}
		}

		// Token: 0x020001E4 RID: 484
		private sealed class ListSourcePropertyDescriptor : PropertyDescriptor
		{
			// Token: 0x06001395 RID: 5013 RVA: 0x00035364 File Offset: 0x00033564
			public ListSourcePropertyDescriptor(MemberDescriptor descr)
				: base(descr)
			{
			}

			// Token: 0x06001396 RID: 5014 RVA: 0x0003536D File Offset: 0x0003356D
			public ListSourcePropertyDescriptor(string name, Attribute[] attrs)
				: base(name, attrs)
			{
			}

			// Token: 0x06001397 RID: 5015 RVA: 0x00035377 File Offset: 0x00033577
			public ListSourcePropertyDescriptor(MemberDescriptor descr, Attribute[] attrs)
				: base(descr, attrs)
			{
			}

			// Token: 0x06001398 RID: 5016 RVA: 0x00003A1F File Offset: 0x00001C1F
			public override bool CanResetValue(object component)
			{
				throw new NotImplementedException();
			}

			// Token: 0x1700061C RID: 1564
			// (get) Token: 0x06001399 RID: 5017 RVA: 0x00003A1F File Offset: 0x00001C1F
			public override Type ComponentType
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x0600139A RID: 5018 RVA: 0x00035384 File Offset: 0x00033584
			public override object GetValue(object component)
			{
				IDataSource dataSource = component as IDataSource;
				if (dataSource == null)
				{
					return null;
				}
				return dataSource.GetView(this.Name).ExecuteSelect(DataSourceSelectArguments.Empty);
			}

			// Token: 0x1700061D RID: 1565
			// (get) Token: 0x0600139B RID: 5019 RVA: 0x00008B66 File Offset: 0x00006D66
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700061E RID: 1566
			// (get) Token: 0x0600139C RID: 5020 RVA: 0x000353B3 File Offset: 0x000335B3
			public override Type PropertyType
			{
				get
				{
					return typeof(IEnumerable);
				}
			}

			// Token: 0x0600139D RID: 5021 RVA: 0x00003A1F File Offset: 0x00001C1F
			public override void ResetValue(object component)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600139E RID: 5022 RVA: 0x00003A1F File Offset: 0x00001C1F
			public override void SetValue(object component, object value)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600139F RID: 5023 RVA: 0x00003A1F File Offset: 0x00001C1F
			public override bool ShouldSerializeValue(object component)
			{
				throw new NotImplementedException();
			}
		}
	}
}
