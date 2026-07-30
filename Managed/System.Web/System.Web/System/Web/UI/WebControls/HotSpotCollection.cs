using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.HotSpot" /> objects inside an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control. This class cannot be inherited.</summary>
	// Token: 0x020003B0 RID: 944
	[Editor("System.Web.UI.Design.WebControls.HotSpotCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HotSpotCollection : StateManagedCollection
	{
		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object at the specified index in the <see cref="T:System.Web.UI.WebControls.HotSpotCollection" /> collection.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.HotSpot" /> object at the specified index in the <see cref="T:System.Web.UI.WebControls.HotSpotCollection" /> collection.</returns>
		/// <param name="index">The ordinal index value that specifies the location of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object in the collection. </param>
		// Token: 0x17000C52 RID: 3154
		public HotSpot this[int index]
		{
			get
			{
				return (HotSpot)((IList)this)[index];
			}
		}

		/// <summary>Appends a specified <see cref="T:System.Web.UI.WebControls.HotSpot" /> object to the end of the <see cref="T:System.Web.UI.WebControls.HotSpotCollection" /> collection.</summary>
		/// <returns>The index at which the object was added to the collection.</returns>
		/// <param name="spot">The <see cref="T:System.Web.UI.WebControls.HotSpot" /> object to append to the collection. </param>
		// Token: 0x0600269B RID: 9883 RVA: 0x00064EFA File Offset: 0x000630FA
		public int Add(HotSpot spot)
		{
			return ((IList)this).Add(spot);
		}

		// Token: 0x0600269C RID: 9884 RVA: 0x00064F03 File Offset: 0x00063103
		protected override object CreateKnownType(int index)
		{
			switch (index)
			{
			case 0:
				return new CircleHotSpot();
			case 1:
				return new PolygonHotSpot();
			case 2:
				return new RectangleHotSpot();
			default:
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x0600269D RID: 9885 RVA: 0x00064F35 File Offset: 0x00063135
		protected override Type[] GetKnownTypes()
		{
			return HotSpotCollection._knownTypes;
		}

		/// <summary>Inserts a specified <see cref="T:System.Web.UI.WebControls.HotSpot" /> object into the <see cref="T:System.Web.UI.WebControls.HotSpotCollection" /> collection at the specified index location.</summary>
		/// <param name="index">The array index at which to add the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object. </param>
		/// <param name="spot">The <see cref="T:System.Web.UI.WebControls.HotSpot" /> object to add to the collection. </param>
		// Token: 0x0600269E RID: 9886 RVA: 0x00055562 File Offset: 0x00053762
		public void Insert(int index, HotSpot spot)
		{
			((IList)this).Insert(index, spot);
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x00064F3C File Offset: 0x0006313C
		protected override void OnValidate(object o)
		{
			base.OnValidate(o);
			if (!(o is HotSpot))
			{
				throw new ArgumentException("o is not a HotSpot");
			}
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.HotSpot" /> object from the <see cref="T:System.Web.UI.WebControls.HotSpotCollection" /> collection.</summary>
		/// <param name="spot">The <see cref="T:System.Web.UI.WebControls.HotSpot" /> object to remove from the collection. </param>
		// Token: 0x060026A0 RID: 9888 RVA: 0x0005556C File Offset: 0x0005376C
		public void Remove(HotSpot spot)
		{
			((IList)this).Remove(spot);
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object at the specified index location from the collection.</summary>
		/// <param name="index">The array index from which to remove the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object. </param>
		// Token: 0x060026A1 RID: 9889 RVA: 0x00055575 File Offset: 0x00053775
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x00064F58 File Offset: 0x00063158
		protected override void SetDirtyObject(object o)
		{
			((HotSpot)o).SetDirty();
		}

		// Token: 0x04001A51 RID: 6737
		private static Type[] _knownTypes = new Type[]
		{
			typeof(CircleHotSpot),
			typeof(PolygonHotSpot),
			typeof(RectangleHotSpot)
		};
	}
}
