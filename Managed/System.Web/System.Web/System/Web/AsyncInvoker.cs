using System;
using System.Threading;

namespace System.Web
{
	// Token: 0x0200007C RID: 124
	internal class AsyncInvoker
	{
		// Token: 0x0600055A RID: 1370 RVA: 0x0000CA60 File Offset: 0x0000AC60
		public AsyncInvoker(BeginEventHandler bh, EndEventHandler eh, HttpApplication a, object d)
		{
			this.begin = bh;
			this.end = eh;
			this.data = d;
			this.app = a;
			this.callback = new AsyncCallback(this.doAsyncCallback);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0000CA97 File Offset: 0x0000AC97
		public AsyncInvoker(BeginEventHandler bh, EndEventHandler eh, HttpApplication app)
			: this(bh, eh, app, null)
		{
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0000CAA3 File Offset: 0x0000ACA3
		public void Invoke(object sender, EventArgs e)
		{
			this.begin(this.app, e, this.callback, this.data);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0000CAC4 File Offset: 0x0000ACC4
		private void doAsyncCallback(IAsyncResult res)
		{
			ThreadPool.QueueUserWorkItem(delegate(object ores)
			{
				IAsyncResult asyncResult = (IAsyncResult)ores;
				try
				{
					this.end(asyncResult);
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine(ex.ToString());
				}
			}, res);
		}

		// Token: 0x04000EDE RID: 3806
		public BeginEventHandler begin;

		// Token: 0x04000EDF RID: 3807
		public EndEventHandler end;

		// Token: 0x04000EE0 RID: 3808
		public object data;

		// Token: 0x04000EE1 RID: 3809
		private HttpApplication app;

		// Token: 0x04000EE2 RID: 3810
		private AsyncCallback callback;
	}
}
