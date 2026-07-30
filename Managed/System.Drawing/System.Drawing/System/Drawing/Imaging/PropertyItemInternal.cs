using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	// Token: 0x02000112 RID: 274
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class PropertyItemInternal : IDisposable
	{
		// Token: 0x06000CC1 RID: 3265 RVA: 0x0001C810 File Offset: 0x0001AA10
		internal PropertyItemInternal()
		{
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0001C824 File Offset: 0x0001AA24
		~PropertyItemInternal()
		{
			this.Dispose(false);
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0001C854 File Offset: 0x0001AA54
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0001C85D File Offset: 0x0001AA5D
		private void Dispose(bool disposing)
		{
			if (this.value != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.value);
				this.value = IntPtr.Zero;
			}
			if (disposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0001C890 File Offset: 0x0001AA90
		internal static PropertyItemInternal ConvertFromPropertyItem(PropertyItem propItem)
		{
			PropertyItemInternal propertyItemInternal = new PropertyItemInternal();
			propertyItemInternal.id = propItem.Id;
			propertyItemInternal.len = 0;
			propertyItemInternal.type = propItem.Type;
			byte[] array = propItem.Value;
			if (array != null)
			{
				int num = array.Length;
				propertyItemInternal.len = num;
				propertyItemInternal.value = Marshal.AllocHGlobal(num);
				Marshal.Copy(array, 0, propertyItemInternal.value, num);
			}
			return propertyItemInternal;
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0001C8F4 File Offset: 0x0001AAF4
		internal static PropertyItem[] ConvertFromMemory(IntPtr propdata, int count)
		{
			PropertyItem[] array = new PropertyItem[count];
			for (int i = 0; i < count; i++)
			{
				PropertyItemInternal propertyItemInternal = null;
				try
				{
					propertyItemInternal = (PropertyItemInternal)Marshal.PtrToStructure(propdata, typeof(PropertyItemInternal));
					array[i] = new PropertyItem();
					array[i].Id = propertyItemInternal.id;
					array[i].Len = propertyItemInternal.len;
					array[i].Type = propertyItemInternal.type;
					array[i].Value = propertyItemInternal.Value;
					propertyItemInternal.value = IntPtr.Zero;
				}
				finally
				{
					if (propertyItemInternal != null)
					{
						propertyItemInternal.Dispose();
					}
				}
				propdata = (IntPtr)((long)propdata + (long)Marshal.SizeOf(typeof(PropertyItemInternal)));
			}
			return array;
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x0001C9B8 File Offset: 0x0001ABB8
		public byte[] Value
		{
			get
			{
				if (this.len == 0)
				{
					return null;
				}
				byte[] array = new byte[this.len];
				Marshal.Copy(this.value, array, 0, this.len);
				return array;
			}
		}

		// Token: 0x04000A29 RID: 2601
		public int id;

		// Token: 0x04000A2A RID: 2602
		public int len;

		// Token: 0x04000A2B RID: 2603
		public short type;

		// Token: 0x04000A2C RID: 2604
		public IntPtr value = IntPtr.Zero;
	}
}
