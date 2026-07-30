using System;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x02000049 RID: 73
	internal abstract class Win32NamedPipe : IPipe
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000169 RID: 361 RVA: 0x000042AC File Offset: 0x000024AC
		public string Name
		{
			get
			{
				if (this.name_cache != null)
				{
					return this.name_cache;
				}
				byte[] array = new byte[200];
				int num;
				int num2;
				int num3;
				int num4;
				while (Win32Marshal.GetNamedPipeHandleState(this.Handle, out num, out num2, out num3, out num4, array, array.Length))
				{
					if (array[array.Length - 1] == 0)
					{
						this.name_cache = Encoding.Default.GetString(array);
						return this.name_cache;
					}
					array = new byte[array.Length * 10];
				}
				throw Win32PipeError.GetException();
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600016A RID: 362
		public abstract SafePipeHandle Handle { get; }

		// Token: 0x0600016B RID: 363 RVA: 0x0000227E File Offset: 0x0000047E
		public void WaitForPipeDrain()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000238 RID: 568
		private string name_cache;
	}
}
