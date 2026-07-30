using System;

namespace System.Windows.Forms.Design
{
	/// <summary>Provides a systematic way to manage event handlers for the current document.</summary>
	// Token: 0x0200001D RID: 29
	public sealed class EventHandlerService
	{
		/// <summary>Fires an OnEventHandlerChanged event.</summary>
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600012F RID: 303 RVA: 0x00004F28 File Offset: 0x00003128
		// (remove) Token: 0x06000130 RID: 304 RVA: 0x00004F60 File Offset: 0x00003160
		public event EventHandler EventHandlerChanged;

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.EventHandlerService" /> class. </summary>
		/// <param name="focusWnd">The <see cref="T:System.Windows.Forms.Control" /> which is being designed.</param>
		// Token: 0x06000131 RID: 305 RVA: 0x00004F95 File Offset: 0x00003195
		public EventHandlerService(Control focusWnd)
		{
			this._focusWnd = focusWnd;
		}

		/// <summary>Gets the currently active event handler of the specified type.</summary>
		/// <returns>An instance of the handler, or null if there is no handler of the requested type.</returns>
		/// <param name="handlerType">The type of the handler to get. </param>
		// Token: 0x06000132 RID: 306 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public object GetHandler(Type handlerType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Pops the given handler off of the stack.</summary>
		/// <param name="handler">The handler to remove from the stack. </param>
		// Token: 0x06000133 RID: 307 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void PopHandler(object handler)
		{
			throw new NotImplementedException();
		}

		/// <summary>Pushes a new event handler on the stack.</summary>
		/// <param name="handler">The handler to add to the stack. </param>
		// Token: 0x06000134 RID: 308 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void PushHandler(object handler)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the control to which event handlers are attached.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> which was attached through the constructor.</returns>
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00004FA4 File Offset: 0x000031A4
		public Control FocusWindow
		{
			get
			{
				return this._focusWnd;
			}
		}

		// Token: 0x04000041 RID: 65
		private readonly Control _focusWnd;
	}
}
