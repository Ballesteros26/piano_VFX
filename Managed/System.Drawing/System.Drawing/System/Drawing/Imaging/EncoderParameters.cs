using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	/// <summary>Encapsulates an array of <see cref="T:System.Drawing.Imaging.EncoderParameter" /> objects.</summary>
	// Token: 0x02000100 RID: 256
	public sealed class EncoderParameters : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.EncoderParameters" /> class that can contain the specified number of <see cref="T:System.Drawing.Imaging.EncoderParameter" /> objects.</summary>
		/// <param name="count">An integer that specifies the number of <see cref="T:System.Drawing.Imaging.EncoderParameter" /> objects that the <see cref="T:System.Drawing.Imaging.EncoderParameters" /> object can contain. </param>
		// Token: 0x06000C43 RID: 3139 RVA: 0x0001B9A8 File Offset: 0x00019BA8
		public EncoderParameters(int count)
		{
			this._param = new EncoderParameter[count];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.EncoderParameters" /> class that can contain one <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object.</summary>
		// Token: 0x06000C44 RID: 3140 RVA: 0x0001B9BC File Offset: 0x00019BBC
		public EncoderParameters()
		{
			this._param = new EncoderParameter[1];
		}

		/// <summary>Gets or sets an array of <see cref="T:System.Drawing.Imaging.EncoderParameter" /> objects.</summary>
		/// <returns>The array of <see cref="T:System.Drawing.Imaging.EncoderParameter" /> objects.</returns>
		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x0001B9D0 File Offset: 0x00019BD0
		// (set) Token: 0x06000C46 RID: 3142 RVA: 0x0001B9D8 File Offset: 0x00019BD8
		public EncoderParameter[] Param
		{
			get
			{
				return this._param;
			}
			set
			{
				this._param = value;
			}
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0001B9E4 File Offset: 0x00019BE4
		internal IntPtr ConvertToMemory()
		{
			int num = Marshal.SizeOf(typeof(EncoderParameter));
			int num2 = this._param.Length;
			IntPtr intPtr;
			long num3;
			checked
			{
				intPtr = Marshal.AllocHGlobal(num2 * num + Marshal.SizeOf(typeof(IntPtr)));
				if (intPtr == IntPtr.Zero)
				{
					throw SafeNativeMethods.Gdip.StatusException(3);
				}
				Marshal.WriteIntPtr(intPtr, (IntPtr)num2);
				num3 = (long)intPtr + unchecked((long)Marshal.SizeOf(typeof(IntPtr)));
			}
			for (int i = 0; i < num2; i++)
			{
				Marshal.StructureToPtr<EncoderParameter>(this._param[i], (IntPtr)(num3 + (long)(i * num)), false);
			}
			return intPtr;
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0001BA88 File Offset: 0x00019C88
		internal static EncoderParameters ConvertFromMemory(IntPtr memory)
		{
			if (memory == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(2);
			}
			int num = Marshal.ReadIntPtr(memory).ToInt32();
			EncoderParameters encoderParameters = new EncoderParameters(num);
			int num2 = Marshal.SizeOf(typeof(EncoderParameter));
			long num3 = (long)memory + (long)Marshal.SizeOf(typeof(IntPtr));
			for (int i = 0; i < num; i++)
			{
				Guid guid = (Guid)Marshal.PtrToStructure((IntPtr)((long)(i * num2) + num3), typeof(Guid));
				int num4 = Marshal.ReadInt32((IntPtr)((long)(i * num2) + num3 + 16L));
				EncoderParameterValueType encoderParameterValueType = (EncoderParameterValueType)Marshal.ReadInt32((IntPtr)((long)(i * num2) + num3 + 20L));
				IntPtr intPtr = Marshal.ReadIntPtr((IntPtr)((long)(i * num2) + num3 + 24L));
				encoderParameters._param[i] = new EncoderParameter(new Encoder(guid), num4, encoderParameterValueType, intPtr);
			}
			return encoderParameters;
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Imaging.EncoderParameters" /> object.</summary>
		// Token: 0x06000C49 RID: 3145 RVA: 0x0001BB84 File Offset: 0x00019D84
		public void Dispose()
		{
			foreach (EncoderParameter encoderParameter in this._param)
			{
				if (encoderParameter != null)
				{
					encoderParameter.Dispose();
				}
			}
			this._param = null;
		}

		// Token: 0x04000971 RID: 2417
		private EncoderParameter[] _param;
	}
}
