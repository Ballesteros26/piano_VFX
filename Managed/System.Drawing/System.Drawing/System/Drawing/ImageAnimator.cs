using System;
using System.Collections;
using System.Drawing.Imaging;
using System.Threading;

namespace System.Drawing
{
	/// <summary>Animates an image that has time-based frames.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000074 RID: 116
	public sealed class ImageAnimator
	{
		// Token: 0x0600051A RID: 1306 RVA: 0x00002050 File Offset: 0x00000250
		private ImageAnimator()
		{
		}

		/// <summary>Displays a multiple-frame image as an animation.</summary>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> object to animate. </param>
		/// <param name="onFrameChangedHandler">An EventHandler object that specifies the method that is called when the animation frame changes. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600051B RID: 1307 RVA: 0x0000EC30 File Offset: 0x0000CE30
		public static void Animate(Image image, EventHandler onFrameChangedHandler)
		{
			if (!ImageAnimator.CanAnimate(image))
			{
				return;
			}
			if (ImageAnimator.ht.ContainsKey(image))
			{
				return;
			}
			byte[] value = image.GetPropertyItem(20736).Value;
			int[] array = new int[value.Length >> 2];
			int i = 0;
			int num = 0;
			while (i < value.Length)
			{
				int num2 = BitConverter.ToInt32(value, i) * 10;
				array[num] = ((num2 < 100) ? 100 : num2);
				i += 4;
				num++;
			}
			AnimateEventArgs animateEventArgs = new AnimateEventArgs(image);
			Thread thread = new Thread(new ThreadStart(new WorkerThread(onFrameChangedHandler, animateEventArgs, array).LoopHandler));
			thread.IsBackground = true;
			animateEventArgs.RunThread = thread;
			ImageAnimator.ht.Add(image, animateEventArgs);
			thread.Start();
		}

		/// <summary>Returns a Boolean value indicating whether the specified image contains time-based frames.</summary>
		/// <returns>This method returns true if the specified image contains time-based frames; otherwise, false.</returns>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> object to test. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600051C RID: 1308 RVA: 0x0000ECE8 File Offset: 0x0000CEE8
		public static bool CanAnimate(Image image)
		{
			if (image == null)
			{
				return false;
			}
			int num = image.FrameDimensionsList.Length;
			if (num < 1)
			{
				return false;
			}
			for (int i = 0; i < num; i++)
			{
				if (image.FrameDimensionsList[i].Equals(FrameDimension.Time.Guid))
				{
					return image.GetFrameCount(FrameDimension.Time) > 1;
				}
			}
			return false;
		}

		/// <summary>Terminates a running animation.</summary>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> object to stop animating. </param>
		/// <param name="onFrameChangedHandler">An EventHandler object that specifies the method that is called when the animation frame changes. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600051D RID: 1309 RVA: 0x0000ED42 File Offset: 0x0000CF42
		public static void StopAnimate(Image image, EventHandler onFrameChangedHandler)
		{
			if (image == null)
			{
				return;
			}
			if (ImageAnimator.ht.ContainsKey(image))
			{
				((AnimateEventArgs)ImageAnimator.ht[image]).RunThread.Abort();
				ImageAnimator.ht.Remove(image);
			}
		}

		/// <summary>Advances the frame in all images currently being animated. The new frame is drawn the next time the image is rendered.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600051E RID: 1310 RVA: 0x0000ED7C File Offset: 0x0000CF7C
		public static void UpdateFrames()
		{
			foreach (object obj in ImageAnimator.ht.Keys)
			{
				ImageAnimator.UpdateImageFrame((Image)obj);
			}
		}

		/// <summary>Advances the frame in the specified image. The new frame is drawn the next time the image is rendered. This method applies only to images with time-based frames.</summary>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> object for which to update frames. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600051F RID: 1311 RVA: 0x0000EDD8 File Offset: 0x0000CFD8
		public static void UpdateFrames(Image image)
		{
			if (image == null)
			{
				return;
			}
			if (ImageAnimator.ht.ContainsKey(image))
			{
				ImageAnimator.UpdateImageFrame(image);
			}
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0000EDF4 File Offset: 0x0000CFF4
		private static void UpdateImageFrame(Image image)
		{
			AnimateEventArgs animateEventArgs = (AnimateEventArgs)ImageAnimator.ht[image];
			image.SelectActiveFrame(FrameDimension.Time, animateEventArgs.GetNextFrame());
		}

		// Token: 0x040003F6 RID: 1014
		private static Hashtable ht = Hashtable.Synchronized(new Hashtable());
	}
}
