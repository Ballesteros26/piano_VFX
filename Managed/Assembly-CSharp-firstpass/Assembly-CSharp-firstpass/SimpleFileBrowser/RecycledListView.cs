using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleFileBrowser
{
	// Token: 0x0200000E RID: 14
	[RequireComponent(typeof(ScrollRect))]
	public class RecycledListView : MonoBehaviour
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x0000420C File Offset: 0x0000240C
		private void Start()
		{
			this.viewportHeight = this.viewportTransform.rect.height;
			base.GetComponent<ScrollRect>().onValueChanged.AddListener(delegate(Vector2 pos)
			{
				this.UpdateItemsInTheList(false);
			});
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000424E File Offset: 0x0000244E
		public void SetAdapter(IListViewAdapter adapter)
		{
			this.adapter = adapter;
			this.itemHeight = adapter.ItemHeight;
			this._1OverItemHeight = 1f / this.itemHeight;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004278 File Offset: 0x00002478
		public void UpdateList()
		{
			float num = Mathf.Max(1f, (float)this.adapter.Count * this.itemHeight);
			this.contentTransform.sizeDelta = new Vector2(0f, num);
			this.viewportHeight = this.viewportTransform.rect.height;
			this.UpdateItemsInTheList(true);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000042DC File Offset: 0x000024DC
		public void OnViewportDimensionsChanged()
		{
			this.viewportHeight = this.viewportTransform.rect.height;
			this.UpdateItemsInTheList(false);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000430C File Offset: 0x0000250C
		private void UpdateItemsInTheList(bool updateAllVisibleItems = false)
		{
			if (this.adapter.Count > 0)
			{
				float num = this.contentTransform.anchoredPosition.y - 1f;
				int num2 = (int)(num * this._1OverItemHeight);
				int num3 = (int)((num + this.viewportHeight + 2f) * this._1OverItemHeight);
				if (num2 < 0)
				{
					num2 = 0;
				}
				if (num3 > this.adapter.Count - 1)
				{
					num3 = this.adapter.Count - 1;
				}
				if (this.currentTopIndex == -1)
				{
					updateAllVisibleItems = true;
					this.currentTopIndex = num2;
					this.currentBottomIndex = num3;
					this.CreateItemsBetweenIndices(num2, num3);
				}
				else
				{
					if (num3 < this.currentTopIndex || num2 > this.currentBottomIndex)
					{
						updateAllVisibleItems = true;
						this.DestroyItemsBetweenIndices(this.currentTopIndex, this.currentBottomIndex);
						this.CreateItemsBetweenIndices(num2, num3);
					}
					else
					{
						if (num2 > this.currentTopIndex)
						{
							this.DestroyItemsBetweenIndices(this.currentTopIndex, num2 - 1);
						}
						if (num3 < this.currentBottomIndex)
						{
							this.DestroyItemsBetweenIndices(num3 + 1, this.currentBottomIndex);
						}
						if (num2 < this.currentTopIndex)
						{
							this.CreateItemsBetweenIndices(num2, this.currentTopIndex - 1);
							if (!updateAllVisibleItems)
							{
								this.UpdateItemContentsBetweenIndices(num2, this.currentTopIndex - 1);
							}
						}
						if (num3 > this.currentBottomIndex)
						{
							this.CreateItemsBetweenIndices(this.currentBottomIndex + 1, num3);
							if (!updateAllVisibleItems)
							{
								this.UpdateItemContentsBetweenIndices(this.currentBottomIndex + 1, num3);
							}
						}
					}
					this.currentTopIndex = num2;
					this.currentBottomIndex = num3;
				}
				if (updateAllVisibleItems)
				{
					this.UpdateItemContentsBetweenIndices(this.currentTopIndex, this.currentBottomIndex);
					return;
				}
			}
			else if (this.currentTopIndex != -1)
			{
				this.DestroyItemsBetweenIndices(this.currentTopIndex, this.currentBottomIndex);
				this.currentTopIndex = -1;
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000044AC File Offset: 0x000026AC
		private void CreateItemsBetweenIndices(int topIndex, int bottomIndex)
		{
			for (int i = topIndex; i <= bottomIndex; i++)
			{
				this.CreateItemAtIndex(i);
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000044CC File Offset: 0x000026CC
		private void CreateItemAtIndex(int index)
		{
			ListItem listItem;
			if (this.pooledItems.Count > 0)
			{
				listItem = this.pooledItems.Pop();
				listItem.gameObject.SetActive(true);
			}
			else
			{
				listItem = this.adapter.CreateItem();
				listItem.transform.SetParent(this.contentTransform, false);
				listItem.SetAdapter(this.adapter);
			}
			((RectTransform)listItem.transform).anchoredPosition = new Vector2(1f, (float)(-(float)index) * this.itemHeight);
			this.items[index] = listItem;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000455C File Offset: 0x0000275C
		private void DestroyItemsBetweenIndices(int topIndex, int bottomIndex)
		{
			for (int i = topIndex; i <= bottomIndex; i++)
			{
				ListItem listItem = this.items[i];
				listItem.gameObject.SetActive(false);
				this.pooledItems.Push(listItem);
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000459C File Offset: 0x0000279C
		private void UpdateItemContentsBetweenIndices(int topIndex, int bottomIndex)
		{
			for (int i = topIndex; i <= bottomIndex; i++)
			{
				ListItem listItem = this.items[i];
				listItem.Position = i;
				this.adapter.SetItemContent(listItem);
			}
		}

		// Token: 0x04000062 RID: 98
		public RectTransform viewportTransform;

		// Token: 0x04000063 RID: 99
		public RectTransform contentTransform;

		// Token: 0x04000064 RID: 100
		private float itemHeight;

		// Token: 0x04000065 RID: 101
		private float _1OverItemHeight;

		// Token: 0x04000066 RID: 102
		private float viewportHeight;

		// Token: 0x04000067 RID: 103
		private readonly Dictionary<int, ListItem> items = new Dictionary<int, ListItem>();

		// Token: 0x04000068 RID: 104
		private readonly Stack<ListItem> pooledItems = new Stack<ListItem>();

		// Token: 0x04000069 RID: 105
		private IListViewAdapter adapter;

		// Token: 0x0400006A RID: 106
		private int currentTopIndex = -1;

		// Token: 0x0400006B RID: 107
		private int currentBottomIndex = -1;
	}
}
