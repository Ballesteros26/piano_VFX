using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	/// <summary>Defines an array of colors that make up a color palette. The colors are 32-bit ARGB colors. Not inheritable.</summary>
	// Token: 0x020000F9 RID: 249
	public sealed class ColorPalette
	{
		/// <summary>Gets a value that specifies how to interpret the color information in the array of colors.</summary>
		/// <returns>The following flag values are valid: 0x00000001The color values in the array contain alpha information. 0x00000002The colors in the array are grayscale values. 0x00000004The colors in the array are halftone values. </returns>
		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x0001AB51 File Offset: 0x00018D51
		public int Flags
		{
			get
			{
				return this._flags;
			}
		}

		/// <summary>Gets an array of <see cref="T:System.Drawing.Color" /> structures.</summary>
		/// <returns>The array of <see cref="T:System.Drawing.Color" /> structure that make up this <see cref="T:System.Drawing.Imaging.ColorPalette" />.</returns>
		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x0001AB59 File Offset: 0x00018D59
		public Color[] Entries
		{
			get
			{
				return this._entries;
			}
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0001AB61 File Offset: 0x00018D61
		internal ColorPalette(int count)
		{
			this._entries = new Color[count];
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0001AB75 File Offset: 0x00018D75
		internal ColorPalette()
		{
			this._entries = new Color[1];
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0001AB8C File Offset: 0x00018D8C
		internal void ConvertFromMemory(IntPtr memory)
		{
			this._flags = Marshal.ReadInt32(memory);
			int num = Marshal.ReadInt32((IntPtr)((long)memory + 4L));
			this._entries = new Color[num];
			for (int i = 0; i < num; i++)
			{
				int num2 = Marshal.ReadInt32((IntPtr)((long)memory + 8L + (long)(i * 4)));
				this._entries[i] = Color.FromArgb(num2);
			}
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0001ABFC File Offset: 0x00018DFC
		internal IntPtr ConvertToMemory()
		{
			int num = this._entries.Length;
			IntPtr intPtr;
			checked
			{
				intPtr = Marshal.AllocHGlobal(4 * (2 + num));
				Marshal.WriteInt32(intPtr, 0, this._flags);
				Marshal.WriteInt32((IntPtr)((long)intPtr + 4L), 0, num);
			}
			for (int i = 0; i < num; i++)
			{
				Marshal.WriteInt32((IntPtr)((long)intPtr + (long)(4 * (i + 2))), 0, this._entries[i].ToArgb());
			}
			return intPtr;
		}

		// Token: 0x04000852 RID: 2130
		private int _flags;

		// Token: 0x04000853 RID: 2131
		private Color[] _entries;
	}
}
