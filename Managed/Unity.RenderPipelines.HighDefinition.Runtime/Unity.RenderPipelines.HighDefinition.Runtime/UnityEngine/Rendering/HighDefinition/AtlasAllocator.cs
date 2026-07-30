using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200014E RID: 334
	internal class AtlasAllocator
	{
		// Token: 0x060009C4 RID: 2500 RVA: 0x0004D858 File Offset: 0x0004BA58
		public AtlasAllocator(int width, int height, bool potPadding)
		{
			this.m_Root = new AtlasAllocator.AtlasNode();
			this.m_Root.m_Rect.Set((float)width, (float)height, 0f, 0f);
			this.m_Width = width;
			this.m_Height = height;
			this.powerOfTwoPadding = potPadding;
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0004D8AC File Offset: 0x0004BAAC
		public bool Allocate(ref Vector4 result, int width, int height)
		{
			AtlasAllocator.AtlasNode atlasNode = this.m_Root.Allocate(width, height, this.powerOfTwoPadding);
			if (atlasNode != null)
			{
				result = atlasNode.m_Rect;
				return true;
			}
			result = Vector4.zero;
			return false;
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0004D8EA File Offset: 0x0004BAEA
		public void Reset()
		{
			this.m_Root.Release();
			this.m_Root.m_Rect.Set((float)this.m_Width, (float)this.m_Height, 0f, 0f);
		}

		// Token: 0x04000F27 RID: 3879
		private AtlasAllocator.AtlasNode m_Root;

		// Token: 0x04000F28 RID: 3880
		private int m_Width;

		// Token: 0x04000F29 RID: 3881
		private int m_Height;

		// Token: 0x04000F2A RID: 3882
		private bool powerOfTwoPadding;

		// Token: 0x0200028E RID: 654
		private class AtlasNode
		{
			// Token: 0x06000CBA RID: 3258 RVA: 0x0005A0FC File Offset: 0x000582FC
			public AtlasAllocator.AtlasNode Allocate(int width, int height, bool powerOfTwoPadding)
			{
				if (this.m_RightChild != null)
				{
					AtlasAllocator.AtlasNode atlasNode = this.m_RightChild.Allocate(width, height, powerOfTwoPadding);
					if (atlasNode == null)
					{
						atlasNode = this.m_BottomChild.Allocate(width, height, powerOfTwoPadding);
					}
					return atlasNode;
				}
				int num = 0;
				int num2 = 0;
				if (powerOfTwoPadding)
				{
					num = (int)this.m_Rect.x % width;
					num2 = (int)this.m_Rect.y % height;
				}
				if ((float)width <= this.m_Rect.x - (float)num && (float)height <= this.m_Rect.y - (float)num2)
				{
					this.m_RightChild = new AtlasAllocator.AtlasNode();
					this.m_BottomChild = new AtlasAllocator.AtlasNode();
					this.m_Rect.z = this.m_Rect.z + (float)num;
					this.m_Rect.w = this.m_Rect.w + (float)num2;
					this.m_Rect.x = this.m_Rect.x - (float)num;
					this.m_Rect.y = this.m_Rect.y - (float)num2;
					if (width > height)
					{
						this.m_RightChild.m_Rect.z = this.m_Rect.z + (float)width;
						this.m_RightChild.m_Rect.w = this.m_Rect.w;
						this.m_RightChild.m_Rect.x = this.m_Rect.x - (float)width;
						this.m_RightChild.m_Rect.y = (float)height;
						this.m_BottomChild.m_Rect.z = this.m_Rect.z;
						this.m_BottomChild.m_Rect.w = this.m_Rect.w + (float)height;
						this.m_BottomChild.m_Rect.x = this.m_Rect.x;
						this.m_BottomChild.m_Rect.y = this.m_Rect.y - (float)height;
					}
					else
					{
						this.m_RightChild.m_Rect.z = this.m_Rect.z + (float)width;
						this.m_RightChild.m_Rect.w = this.m_Rect.w;
						this.m_RightChild.m_Rect.x = this.m_Rect.x - (float)width;
						this.m_RightChild.m_Rect.y = this.m_Rect.y;
						this.m_BottomChild.m_Rect.z = this.m_Rect.z;
						this.m_BottomChild.m_Rect.w = this.m_Rect.w + (float)height;
						this.m_BottomChild.m_Rect.x = (float)width;
						this.m_BottomChild.m_Rect.y = this.m_Rect.y - (float)height;
					}
					this.m_Rect.x = (float)width;
					this.m_Rect.y = (float)height;
					return this;
				}
				return null;
			}

			// Token: 0x06000CBB RID: 3259 RVA: 0x0005A3BC File Offset: 0x000585BC
			public void Release()
			{
				if (this.m_RightChild != null)
				{
					this.m_RightChild.Release();
					this.m_BottomChild.Release();
				}
				this.m_RightChild = null;
				this.m_BottomChild = null;
			}

			// Token: 0x040016E2 RID: 5858
			public AtlasAllocator.AtlasNode m_RightChild;

			// Token: 0x040016E3 RID: 5859
			public AtlasAllocator.AtlasNode m_BottomChild;

			// Token: 0x040016E4 RID: 5860
			public Vector4 m_Rect = new Vector4(0f, 0f, 0f, 0f);
		}
	}
}
