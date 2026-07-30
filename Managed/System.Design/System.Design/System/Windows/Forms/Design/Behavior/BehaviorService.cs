using System;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior
{
	/// <summary>Manages user interface in the designer. This class cannot be inherited.</summary>
	// Token: 0x02000045 RID: 69
	public sealed class BehaviorService : IDisposable
	{
		// Token: 0x06000244 RID: 580 RVA: 0x00002352 File Offset: 0x00000552
		internal BehaviorService()
		{
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" /> starts a drag-and-drop operation.</summary>
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000245 RID: 581 RVA: 0x000088DC File Offset: 0x00006ADC
		// (remove) Token: 0x06000246 RID: 582 RVA: 0x00008914 File Offset: 0x00006B14
		public event BehaviorDragDropEventHandler BeginDrag;

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" /> completes a drag operation.</summary>
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000247 RID: 583 RVA: 0x0000894C File Offset: 0x00006B4C
		// (remove) Token: 0x06000248 RID: 584 RVA: 0x00008984 File Offset: 0x00006B84
		public event BehaviorDragDropEventHandler EndDrag;

		/// <summary>Occurs when the current selection should be refreshed.</summary>
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000249 RID: 585 RVA: 0x000089BC File Offset: 0x00006BBC
		// (remove) Token: 0x0600024A RID: 586 RVA: 0x000089F4 File Offset: 0x00006BF4
		public event EventHandler Synchronize;

		/// <summary>Gets the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" />.</summary>
		/// <returns>A collection of adorner.</returns>
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public BehaviorServiceAdornerCollection Adorners
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> for the adorner window.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> for the adorner window.</returns>
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public Graphics AdornerWindowGraphics
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> at the top of the behavior stack without removing it.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> at the top of the behavior stack.</returns>
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public Behavior CurrentBehavior
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Translates a <see cref="T:System.Drawing.Point" /> in the adorner window to screen coordinates.</summary>
		/// <returns>The transformed <see cref="T:System.Drawing.Point" /> value, in screen coordinates.</returns>
		/// <param name="p">The <see cref="T:System.Drawing.Point" /> value to transform.</param>
		// Token: 0x0600024E RID: 590 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public Point AdornerWindowPointToScreen(Point p)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the location of the adorner window in screen coordinates.</summary>
		/// <returns>The location, from the upper-left corner of the adorner window, in screen coordinates.</returns>
		// Token: 0x0600024F RID: 591 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public Point AdornerWindowToScreen()
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the bounding <see cref="T:System.Drawing.Rectangle" /> of a <see cref="T:System.Windows.Forms.Control" />.</summary>
		/// <returns>The bounding <see cref="T:System.Drawing.Rectangle" /> of a <see cref="T:System.Windows.Forms.Control" /> translated to the adorner window coordinates.</returns>
		/// <param name="c">The <see cref="T:System.Windows.Forms.Control" /> to translate.</param>
		// Token: 0x06000250 RID: 592 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public Rectangle ControlRectInAdornerWindow(Control c)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the location of a <see cref="T:System.Windows.Forms.Control" /> translated to adorner window coordinates.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> value indicating the location of <paramref name="c" /> in adorner window coordinates.</returns>
		/// <param name="c">The <see cref="T:System.Windows.Forms.Control" /> to translate.</param>
		// Token: 0x06000251 RID: 593 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public Point ControlToAdornerWindow(Control c)
		{
			throw new NotImplementedException();
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" />. </summary>
		// Token: 0x06000252 RID: 594 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void Dispose()
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> immediately after the given <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> in the behavior stack.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> immediately after <paramref name="behavior" /> in the behavior stack, or null if there is no following behavior.</returns>
		/// <param name="behavior">The <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> preceding the <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> to be returned.</param>
		// Token: 0x06000253 RID: 595 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public Behavior GetNextBehavior(Behavior behavior)
		{
			throw new NotImplementedException();
		}

		/// <summary>Invalidates the adorner window of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" />.</summary>
		// Token: 0x06000254 RID: 596 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void Invalidate()
		{
			throw new NotImplementedException();
		}

		/// <summary>Invalidates, within the adorner window, the specified area of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" />.</summary>
		/// <param name="rect">The rectangular area to invalidate.</param>
		// Token: 0x06000255 RID: 597 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void Invalidate(Rectangle rect)
		{
			throw new NotImplementedException();
		}

		/// <summary>Invalidates, within the adorner window, the specified area of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" />.</summary>
		/// <param name="r">The region to invalidate.</param>
		// Token: 0x06000256 RID: 598 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void Invalidate(Region r)
		{
			throw new NotImplementedException();
		}

		/// <summary>Converts a point in a handle's coordinate system to the adorner window coordinates.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> in the adorner window coordinates.</returns>
		/// <param name="handle">An adorner window's handle.</param>
		/// <param name="pt">A <see cref="T:System.Drawing.Point" /> in a handle's coordinate system.</param>
		// Token: 0x06000257 RID: 599 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public Point MapAdornerWindowPoint(IntPtr handle, Point pt)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes and returns the <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> at the top of the stack.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> that was removed from the stack.</returns>
		/// <param name="behavior">The <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> to remove from the stack.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> stack is empty.</exception>
		// Token: 0x06000258 RID: 600 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public Behavior PopBehavior(Behavior behavior)
		{
			throw new NotImplementedException();
		}

		/// <summary>Pushes a <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> onto the behavior stack.</summary>
		/// <param name="behavior">The <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> to push.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="behavior" /> is null.</exception>
		// Token: 0x06000259 RID: 601 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void PushBehavior(Behavior behavior)
		{
			throw new NotImplementedException();
		}

		/// <summary>Pushes a <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> onto the behavior stack and assigns mouse capture to the behavior.</summary>
		/// <param name="behavior">The <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> to push.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="behavior" /> is null.</exception>
		// Token: 0x0600025A RID: 602 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void PushCaptureBehavior(Behavior behavior)
		{
			throw new NotImplementedException();
		}

		/// <summary>Translates a point in screen coordinates into the adorner window coordinates of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" />.</summary>
		/// <returns>The transformed <see cref="T:System.Drawing.Point" /> value, in adorner window coordinates.</returns>
		/// <param name="p">The <see cref="T:System.Drawing.Point" /> value to transform.</param>
		// Token: 0x0600025B RID: 603 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public Point ScreenToAdornerWindow(Point p)
		{
			throw new NotImplementedException();
		}

		/// <summary>Synchronizes all selection glyphs.</summary>
		// Token: 0x0600025C RID: 604 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void SyncSelection()
		{
			throw new NotImplementedException();
		}
	}
}
