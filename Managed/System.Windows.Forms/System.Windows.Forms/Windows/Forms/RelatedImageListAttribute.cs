using System;

namespace System.Windows.Forms
{
	/// <summary>Indicates which <see cref="T:System.Windows.Forms.ImageList" /> a property is related to.</summary>
	// Token: 0x020002B4 RID: 692
	[AttributeUsage(128, AllowMultiple = false, Inherited = true)]
	public sealed class RelatedImageListAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.RelatedImageListAttribute" /> class. </summary>
		/// <param name="relatedImageList">The name of the <see cref="T:System.Windows.Forms.ImageList" /> the property relates to.</param>
		// Token: 0x06002E16 RID: 11798 RVA: 0x000B1BA4 File Offset: 0x000AFDA4
		public RelatedImageListAttribute(string relatedImageList)
		{
			this.related_image_list = relatedImageList;
		}

		/// <summary>Gets the name of the related <see cref="T:System.Windows.Forms.ImageList" /></summary>
		/// <returns>The name of the related <see cref="T:System.Windows.Forms.ImageList" /></returns>
		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06002E17 RID: 11799 RVA: 0x000B1BB4 File Offset: 0x000AFDB4
		public string RelatedImageList
		{
			get
			{
				return this.related_image_list;
			}
		}

		// Token: 0x0400161F RID: 5663
		private string related_image_list;
	}
}
