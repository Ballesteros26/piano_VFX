using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;

namespace System.Windows.Forms
{
	/// <summary>Contains values of properties that a component might need only occasionally.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000280 RID: 640
	[Serializable]
	public class OwnerDrawPropertyBag : MarshalByRefObject, ISerializable
	{
		// Token: 0x06002994 RID: 10644 RVA: 0x000A0630 File Offset: 0x0009E830
		internal OwnerDrawPropertyBag()
		{
			this.fore_color = (this.back_color = Color.Empty);
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x000A0658 File Offset: 0x0009E858
		private OwnerDrawPropertyBag(Color fore_color, Color back_color, Font font)
		{
			this.fore_color = fore_color;
			this.back_color = back_color;
			this.font = font;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.OwnerDrawPropertyBag" /> class. </summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> value.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> value.</param>
		// Token: 0x06002996 RID: 10646 RVA: 0x000A0678 File Offset: 0x0009E878
		protected OwnerDrawPropertyBag(SerializationInfo info, StreamingContext context)
		{
			foreach (SerializationEntry serializationEntry in info)
			{
				string name = serializationEntry.Name;
				if (name != null)
				{
					if (OwnerDrawPropertyBag.<>f__switch$map9 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(3);
						dictionary.Add("Font", 0);
						dictionary.Add("ForeColor", 1);
						dictionary.Add("BackColor", 2);
						OwnerDrawPropertyBag.<>f__switch$map9 = dictionary;
					}
					int num;
					if (OwnerDrawPropertyBag.<>f__switch$map9.TryGetValue(name, ref num))
					{
						switch (num)
						{
						case 0:
							this.font = (Font)serializationEntry.Value;
							break;
						case 1:
							this.fore_color = (Color)serializationEntry.Value;
							break;
						case 2:
							this.back_color = (Color)serializationEntry.Value;
							break;
						}
					}
				}
			}
		}

		/// <summary>Populates the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="si">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		// Token: 0x06002997 RID: 10647 RVA: 0x000A0764 File Offset: 0x0009E964
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			si.AddValue("BackColor", this.BackColor);
			si.AddValue("ForeColor", this.ForeColor);
			si.AddValue("Font", this.Font);
		}

		/// <summary>Gets or sets the foreground color of the component.</summary>
		/// <returns>The foreground color of the component. The default is <see cref="F:System.Drawing.Color.Empty" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06002998 RID: 10648 RVA: 0x000A07B0 File Offset: 0x0009E9B0
		// (set) Token: 0x06002999 RID: 10649 RVA: 0x000A07B8 File Offset: 0x0009E9B8
		public Color ForeColor
		{
			get
			{
				return this.fore_color;
			}
			set
			{
				this.fore_color = value;
			}
		}

		/// <summary>Gets or sets the background color for the component.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the component. The default is <see cref="F:System.Drawing.Color.Empty" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x0600299A RID: 10650 RVA: 0x000A07C4 File Offset: 0x0009E9C4
		// (set) Token: 0x0600299B RID: 10651 RVA: 0x000A07CC File Offset: 0x0009E9CC
		public Color BackColor
		{
			get
			{
				return this.back_color;
			}
			set
			{
				this.back_color = value;
			}
		}

		/// <summary>Gets or sets the font of the text displayed by the component.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the component. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x0600299C RID: 10652 RVA: 0x000A07D8 File Offset: 0x0009E9D8
		// (set) Token: 0x0600299D RID: 10653 RVA: 0x000A07E0 File Offset: 0x0009E9E0
		public Font Font
		{
			get
			{
				return this.font;
			}
			set
			{
				this.font = value;
			}
		}

		/// <summary>Returns whether the <see cref="T:System.Windows.Forms.OwnerDrawPropertyBag" /> contains all default values.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.OwnerDrawPropertyBag" /> contains all default values; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600299E RID: 10654 RVA: 0x000A07EC File Offset: 0x0009E9EC
		public virtual bool IsEmpty()
		{
			return this.font == null && this.fore_color.IsEmpty && this.back_color.IsEmpty;
		}

		/// <summary>Copies an <see cref="T:System.Windows.Forms.OwnerDrawPropertyBag" />.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.OwnerDrawPropertyBag" />.</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.OwnerDrawPropertyBag" /> to be copied.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600299F RID: 10655 RVA: 0x000A0818 File Offset: 0x0009EA18
		public static OwnerDrawPropertyBag Copy(OwnerDrawPropertyBag value)
		{
			return new OwnerDrawPropertyBag(value.ForeColor, value.BackColor, value.Font);
		}

		// Token: 0x040014A4 RID: 5284
		private Color fore_color;

		// Token: 0x040014A5 RID: 5285
		private Color back_color;

		// Token: 0x040014A6 RID: 5286
		private Font font;
	}
}
