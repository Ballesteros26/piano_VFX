using System;
using System.ComponentModel.Design;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior
{
	/// <summary>Represents the <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> objects that are managed by a <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" />.</summary>
	// Token: 0x02000042 RID: 66
	public abstract class Behavior
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> class. </summary>
		// Token: 0x0600022B RID: 555 RVA: 0x00002364 File Offset: 0x00000564
		[MonoTODO]
		protected Behavior()
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> class with the given <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" />.</summary>
		/// <param name="callParentBehavior">true if the parent behavior should be called if it exists; otherwise, false.</param>
		/// <param name="behaviorService">The <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" />  to use.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callParentBehavior" /> is true, and <paramref name="behaviorService" /> is null.</exception>
		// Token: 0x0600022C RID: 556 RVA: 0x00002364 File Offset: 0x00000564
		[MonoTODO]
		protected Behavior(bool callParentBehavior, BehaviorService behaviorService)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the cursor that should be displayed for this behavior.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that should be displayed for this behavior.</returns>
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual Cursor Cursor
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether <see cref="T:System.ComponentModel.Design.MenuCommand" /> objects should be disabled.</summary>
		/// <returns>true if all <see cref="T:System.ComponentModel.Design.MenuCommand" /> objects the designer receives should have states set to Enabled = false when this <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> is active; otherwise, false.</returns>
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual bool DisableAllCommands
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Intercepts commands.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.MenuCommand" />. By default, <see cref="M:System.Windows.Forms.Design.Behavior.Behavior.FindCommand(System.ComponentModel.Design.CommandID)" /> returns null.</returns>
		/// <param name="commandId">A <see cref="T:System.ComponentModel.Design.CommandID" /> object.</param>
		// Token: 0x0600022F RID: 559 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual MenuCommand FindCommand(CommandID commandId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Permits custom drag-and-drop behavior.</summary>
		/// <param name="g">A <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> object on which to invoke drag-and-drop behavior.</param>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data. </param>
		// Token: 0x06000230 RID: 560 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual void OnDragDrop(Glyph g, DragEventArgs e)
		{
			throw new NotImplementedException();
		}

		/// <summary>Permits custom drag-enter behavior.</summary>
		/// <param name="g">A <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> on which to invoke drag-enter behavior.</param>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data. </param>
		// Token: 0x06000231 RID: 561 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual void OnDragEnter(Glyph g, DragEventArgs e)
		{
			throw new NotImplementedException();
		}

		/// <summary>Permits custom drag-leave behavior.</summary>
		/// <param name="g">A <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> on which to invoke drag-leave behavior.</param>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data. </param>
		// Token: 0x06000232 RID: 562 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual void OnDragLeave(Glyph g, EventArgs e)
		{
			throw new NotImplementedException();
		}

		/// <summary>Permits custom drag-over behavior.</summary>
		/// <param name="g">A <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> on which to invoke drag-over behavior.</param>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data. </param>
		// Token: 0x06000233 RID: 563 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual void OnDragOver(Glyph g, DragEventArgs e)
		{
			throw new NotImplementedException();
		}

		/// <summary>Permits custom drag-and-drop feedback behavior.</summary>
		/// <param name="g">A <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> on which to invoke drag-and-drop behavior.</param>
		/// <param name="e">A <see cref="T:System.Windows.Forms.GiveFeedbackEventArgs" /> that contains the event data.</param>
		// Token: 0x06000234 RID: 564 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual void OnGiveFeedback(Glyph g, GiveFeedbackEventArgs e)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by the adorner window when it loses mouse capture.</summary>
		/// <param name="g">A <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> on which to invoke drag-and-drop behavior.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000235 RID: 565 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual void OnLoseCapture(Glyph g, EventArgs e)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called when any double-click message enters the adorner window of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" />.</summary>
		/// <returns>true if the message was handled; otherwise, false.</returns>
		/// <param name="g">A <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</param>
		/// <param name="button">A <see cref="T:System.Windows.Forms.MouseButtons" /> value indicating which button was clicked.</param>
		/// <param name="mouseLoc">The location at which the click occurred. </param>
		// Token: 0x06000236 RID: 566 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual bool OnMouseDoubleClick(Glyph g, MouseButtons button, Point mouseLoc)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called when any mouse-down message enters the adorner window of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" />.</summary>
		/// <returns>true if the message was handled; otherwise, false.</returns>
		/// <param name="g">A <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />. </param>
		/// <param name="button">A <see cref="T:System.Windows.Forms.MouseButtons" /> value indicating which button was clicked.</param>
		/// <param name="mouseLoc">The location at which the click occurred. </param>
		// Token: 0x06000237 RID: 567 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual bool OnMouseDown(Glyph g, MouseButtons button, Point mouseLoc)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called when any mouse-enter message enters the adorner window of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" />.</summary>
		/// <returns>true if the message was handled; otherwise, false.</returns>
		/// <param name="g">A <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />. </param>
		// Token: 0x06000238 RID: 568 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual bool OnMouseEnter(Glyph g)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called when any mouse-hover message enters the adorner window of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" />.</summary>
		/// <returns>true if the message was handled; otherwise, false.</returns>
		/// <param name="g">A <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />. </param>
		/// <param name="mouseLoc">The location at which the hover occurred. </param>
		// Token: 0x06000239 RID: 569 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual bool OnMouseHover(Glyph g, Point mouseLoc)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called when any mouse-leave message enters the adorner window of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" />.</summary>
		/// <returns>true if the message was handled; otherwise, false.</returns>
		/// <param name="g">A <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</param>
		// Token: 0x0600023A RID: 570 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual bool OnMouseLeave(Glyph g)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called when any mouse-move message enters the adorner window of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" />.</summary>
		/// <returns>true if the message was handled; otherwise, false.</returns>
		/// <param name="g">A <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />. </param>
		/// <param name="button">A <see cref="T:System.Windows.Forms.MouseButtons" /> value indicating which button was clicked.</param>
		/// <param name="mouseLoc">The location at which the move occurred. </param>
		// Token: 0x0600023B RID: 571 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual bool OnMouseMove(Glyph g, MouseButtons button, Point mouseLoc)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called when any mouse-up message enters the adorner window of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" />.</summary>
		/// <returns>true if the message was handled; otherwise, false.</returns>
		/// <param name="g">A <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />. </param>
		/// <param name="button">A <see cref="T:System.Windows.Forms.MouseButtons" /> value indicating which button was clicked.</param>
		// Token: 0x0600023C RID: 572 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual bool OnMouseUp(Glyph g, MouseButtons button)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends this drag-and-drop event from the adorner window to the appropriate <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> or hit-tested <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</summary>
		/// <param name="g">A <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />. </param>
		/// <param name="e">A <see cref="T:System.Windows.Forms.QueryContinueDragEventArgs" /> that contains the event data. </param>
		// Token: 0x0600023D RID: 573 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual void OnQueryContinueDrag(Glyph g, QueryContinueDragEventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}
