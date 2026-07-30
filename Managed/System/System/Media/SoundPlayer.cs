using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Threading;
using Mono.Audio;

namespace System.Media
{
	/// <summary>Controls playback of a sound from a .wav file.</summary>
	// Token: 0x02000124 RID: 292
	[ToolboxItem(false)]
	[Serializable]
	public class SoundPlayer : Component, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Media.SoundPlayer" /> class.</summary>
		// Token: 0x060007CE RID: 1998 RVA: 0x00026EB2 File Offset: 0x000250B2
		public SoundPlayer()
		{
			this.sound_location = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Media.SoundPlayer" /> class, and attaches the .wav file within the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> to a .wav file.</param>
		// Token: 0x060007CF RID: 1999 RVA: 0x00026EDB File Offset: 0x000250DB
		public SoundPlayer(Stream stream)
			: this()
		{
			this.audiostream = stream;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Media.SoundPlayer" /> class, and attaches the specified .wav file.</summary>
		/// <param name="soundLocation">The location of a .wav file to load.</param>
		/// <exception cref="T:System.UriFormatException">The URL value specified by <paramref name="soundLocation" /> cannot be resolved.</exception>
		// Token: 0x060007D0 RID: 2000 RVA: 0x00026EEA File Offset: 0x000250EA
		public SoundPlayer(string soundLocation)
			: this()
		{
			if (soundLocation == null)
			{
				throw new ArgumentNullException("soundLocation");
			}
			this.sound_location = soundLocation;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Media.SoundPlayer" /> class.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used for deserialization.</param>
		/// <param name="context">The destination to be used for deserialization.</param>
		/// <exception cref="T:System.UriFormatException">The <see cref="P:System.Media.SoundPlayer.SoundLocation" /> specified in <paramref name="serializationInfo" /> cannot be resolved.</exception>
		// Token: 0x060007D1 RID: 2001 RVA: 0x00026F07 File Offset: 0x00025107
		protected SoundPlayer(SerializationInfo serializationInfo, StreamingContext context)
			: this()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00026F14 File Offset: 0x00025114
		private void LoadFromStream(Stream s)
		{
			this.mstream = new MemoryStream();
			byte[] array = new byte[4096];
			int num;
			while ((num = s.Read(array, 0, 4096)) > 0)
			{
				this.mstream.Write(array, 0, num);
			}
			this.mstream.Position = 0L;
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00026F68 File Offset: 0x00025168
		private void LoadFromUri(string location)
		{
			this.mstream = null;
			if (string.IsNullOrEmpty(location))
			{
				return;
			}
			Stream stream;
			if (File.Exists(location))
			{
				stream = new FileStream(location, FileMode.Open, FileAccess.Read, FileShare.Read);
			}
			else
			{
				stream = WebRequest.Create(location).GetResponse().GetResponseStream();
			}
			using (stream)
			{
				this.LoadFromStream(stream);
			}
		}

		/// <summary>Loads a sound synchronously.</summary>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The elapsed time during loading exceeds the time, in milliseconds, specified by <see cref="P:System.Media.SoundPlayer.LoadTimeout" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> cannot be found.</exception>
		// Token: 0x060007D4 RID: 2004 RVA: 0x00026FD4 File Offset: 0x000251D4
		public void Load()
		{
			if (this.load_completed)
			{
				return;
			}
			if (this.audiostream != null)
			{
				this.LoadFromStream(this.audiostream);
			}
			else
			{
				this.LoadFromUri(this.sound_location);
			}
			this.adata = null;
			this.adev = null;
			this.load_completed = true;
			AsyncCompletedEventArgs asyncCompletedEventArgs = new AsyncCompletedEventArgs(null, false, this);
			this.OnLoadCompleted(asyncCompletedEventArgs);
			if (this.LoadCompleted != null)
			{
				this.LoadCompleted(this, asyncCompletedEventArgs);
			}
			if (SoundPlayer.use_win32_player)
			{
				if (this.win32_player == null)
				{
					this.win32_player = new Win32SoundPlayer(this.mstream);
					return;
				}
				this.win32_player.Stream = this.mstream;
			}
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00027078 File Offset: 0x00025278
		private void AsyncFinished(IAsyncResult ar)
		{
			(ar.AsyncState as ThreadStart).EndInvoke(ar);
		}

		/// <summary>Loads a .wav file from a stream or a Web resource using a new thread.</summary>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The elapsed time during loading exceeds the time, in milliseconds, specified by <see cref="P:System.Media.SoundPlayer.LoadTimeout" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> cannot be found.</exception>
		// Token: 0x060007D6 RID: 2006 RVA: 0x0002708C File Offset: 0x0002528C
		public void LoadAsync()
		{
			if (this.load_completed)
			{
				return;
			}
			ThreadStart threadStart = new ThreadStart(this.Load);
			threadStart.BeginInvoke(new AsyncCallback(this.AsyncFinished), threadStart);
		}

		/// <summary>Raises the <see cref="E:System.Media.SoundPlayer.LoadCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.ComponentModel.AsyncCompletedEventArgs" />  that contains the event data. </param>
		// Token: 0x060007D7 RID: 2007 RVA: 0x000027E8 File Offset: 0x000009E8
		protected virtual void OnLoadCompleted(AsyncCompletedEventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Media.SoundPlayer.SoundLocationChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060007D8 RID: 2008 RVA: 0x000027E8 File Offset: 0x000009E8
		protected virtual void OnSoundLocationChanged(EventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Media.SoundPlayer.StreamChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060007D9 RID: 2009 RVA: 0x000027E8 File Offset: 0x000009E8
		protected virtual void OnStreamChanged(EventArgs e)
		{
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x000270C3 File Offset: 0x000252C3
		private void Start()
		{
			if (!SoundPlayer.use_win32_player)
			{
				this.stopped = false;
				if (this.adata != null)
				{
					this.adata.IsStopped = false;
				}
			}
			if (!this.load_completed)
			{
				this.Load();
			}
		}

		/// <summary>Plays the .wav file using a new thread, and loads the .wav file first if it has not been loaded.</summary>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The elapsed time during loading exceeds the time, in milliseconds, specified by <see cref="P:System.Media.SoundPlayer.LoadTimeout" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> cannot be found.</exception>
		/// <exception cref="T:System.InvalidOperationException">The .wav header is corrupted; the file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> is not a PCM .wav file.</exception>
		// Token: 0x060007DB RID: 2011 RVA: 0x000270F8 File Offset: 0x000252F8
		public void Play()
		{
			if (!SoundPlayer.use_win32_player)
			{
				ThreadStart threadStart = new ThreadStart(this.PlaySync);
				threadStart.BeginInvoke(new AsyncCallback(this.AsyncFinished), threadStart);
				return;
			}
			this.Start();
			if (this.mstream == null)
			{
				SystemSounds.Beep.Play();
				return;
			}
			this.win32_player.Play();
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00027152 File Offset: 0x00025352
		private void PlayLoop()
		{
			this.Start();
			if (this.mstream == null)
			{
				SystemSounds.Beep.Play();
				return;
			}
			while (!this.stopped)
			{
				this.PlaySync();
			}
		}

		/// <summary>Plays and loops the .wav file using a new thread, and loads the .wav file first if it has not been loaded.</summary>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The elapsed time during loading exceeds the time, in milliseconds, specified by <see cref="P:System.Media.SoundPlayer.LoadTimeout" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> cannot be found.</exception>
		/// <exception cref="T:System.InvalidOperationException">The .wav header is corrupted; the file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> is not a PCM .wav file.</exception>
		// Token: 0x060007DD RID: 2013 RVA: 0x0002717C File Offset: 0x0002537C
		public void PlayLooping()
		{
			if (!SoundPlayer.use_win32_player)
			{
				ThreadStart threadStart = new ThreadStart(this.PlayLoop);
				threadStart.BeginInvoke(new AsyncCallback(this.AsyncFinished), threadStart);
				return;
			}
			this.Start();
			if (this.mstream == null)
			{
				SystemSounds.Beep.Play();
				return;
			}
			this.win32_player.PlayLooping();
		}

		/// <summary>Plays the .wav file and loads the .wav file first if it has not been loaded.</summary>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The elapsed time during loading exceeds the time, in milliseconds, specified by <see cref="P:System.Media.SoundPlayer.LoadTimeout" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> cannot be found.</exception>
		/// <exception cref="T:System.InvalidOperationException">The .wav header is corrupted; the file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> is not a PCM .wav file.</exception>
		// Token: 0x060007DE RID: 2014 RVA: 0x000271D8 File Offset: 0x000253D8
		public void PlaySync()
		{
			this.Start();
			if (this.mstream == null)
			{
				SystemSounds.Beep.Play();
				return;
			}
			if (!SoundPlayer.use_win32_player)
			{
				try
				{
					if (this.adata == null)
					{
						this.adata = new WavData(this.mstream);
					}
					if (this.adev == null)
					{
						this.adev = AudioDevice.CreateDevice(null);
					}
					if (this.adata != null)
					{
						this.adata.Setup(this.adev);
						this.adata.Play(this.adev);
					}
					return;
				}
				catch
				{
					return;
				}
			}
			this.win32_player.PlaySync();
		}

		/// <summary>Stops playback of the sound if playback is occurring.</summary>
		// Token: 0x060007DF RID: 2015 RVA: 0x0002727C File Offset: 0x0002547C
		public void Stop()
		{
			if (!SoundPlayer.use_win32_player)
			{
				this.stopped = true;
				if (this.adata != null)
				{
					this.adata.IsStopped = true;
					return;
				}
			}
			else
			{
				this.win32_player.Stop();
			}
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> method.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" />  to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
		// Token: 0x060007E0 RID: 2016 RVA: 0x000027E8 File Offset: 0x000009E8
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
		}

		/// <summary>Gets a value indicating whether loading of a .wav file has successfully completed.</summary>
		/// <returns>true if a .wav file is loaded; false if a .wav file has not yet been loaded.</returns>
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x000272AC File Offset: 0x000254AC
		public bool IsLoadCompleted
		{
			get
			{
				return this.load_completed;
			}
		}

		/// <summary>Gets or sets the time, in milliseconds, in which the .wav file must load.</summary>
		/// <returns>The number of milliseconds to wait. The default is 10000 (10 seconds).</returns>
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x000272B4 File Offset: 0x000254B4
		// (set) Token: 0x060007E3 RID: 2019 RVA: 0x000272BC File Offset: 0x000254BC
		public int LoadTimeout
		{
			get
			{
				return this.load_timeout;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("timeout must be >= 0");
				}
				this.load_timeout = value;
			}
		}

		/// <summary>Gets or sets the file path or URL of the .wav file to load.</summary>
		/// <returns>The file path or URL from which to load a .wav file, or <see cref="F:System.String.Empty" /> if no file path is present. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x000272D4 File Offset: 0x000254D4
		// (set) Token: 0x060007E5 RID: 2021 RVA: 0x000272DC File Offset: 0x000254DC
		public string SoundLocation
		{
			get
			{
				return this.sound_location;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.sound_location = value;
				this.load_completed = false;
				this.OnSoundLocationChanged(EventArgs.Empty);
				if (this.SoundLocationChanged != null)
				{
					this.SoundLocationChanged(this, EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.IO.Stream" /> from which to load the .wav file.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> from which to load the .wav file, or null if no stream is available. The default is null.</returns>
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x00027329 File Offset: 0x00025529
		// (set) Token: 0x060007E7 RID: 2023 RVA: 0x00027331 File Offset: 0x00025531
		public Stream Stream
		{
			get
			{
				return this.audiostream;
			}
			set
			{
				if (this.audiostream != value)
				{
					this.audiostream = value;
					this.load_completed = false;
					this.OnStreamChanged(EventArgs.Empty);
					if (this.StreamChanged != null)
					{
						this.StreamChanged(this, EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Object" /> that contains data about the <see cref="T:System.Media.SoundPlayer" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains data about the <see cref="T:System.Media.SoundPlayer" />.</returns>
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x0002736E File Offset: 0x0002556E
		// (set) Token: 0x060007E9 RID: 2025 RVA: 0x00027376 File Offset: 0x00025576
		public object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		/// <summary>Occurs when a .wav file has been successfully or unsuccessfully loaded.</summary>
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060007EA RID: 2026 RVA: 0x00027380 File Offset: 0x00025580
		// (remove) Token: 0x060007EB RID: 2027 RVA: 0x000273B8 File Offset: 0x000255B8
		public event AsyncCompletedEventHandler LoadCompleted;

		/// <summary>Occurs when a new audio source path for this <see cref="T:System.Media.SoundPlayer" /> has been set.</summary>
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060007EC RID: 2028 RVA: 0x000273F0 File Offset: 0x000255F0
		// (remove) Token: 0x060007ED RID: 2029 RVA: 0x00027428 File Offset: 0x00025628
		public event EventHandler SoundLocationChanged;

		/// <summary>Occurs when a new <see cref="T:System.IO.Stream" /> audio source for this <see cref="T:System.Media.SoundPlayer" /> has been set.</summary>
		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060007EE RID: 2030 RVA: 0x00027460 File Offset: 0x00025660
		// (remove) Token: 0x060007EF RID: 2031 RVA: 0x00027498 File Offset: 0x00025698
		public event EventHandler StreamChanged;

		// Token: 0x04000D77 RID: 3447
		private string sound_location;

		// Token: 0x04000D78 RID: 3448
		private Stream audiostream;

		// Token: 0x04000D79 RID: 3449
		private object tag = string.Empty;

		// Token: 0x04000D7A RID: 3450
		private MemoryStream mstream;

		// Token: 0x04000D7B RID: 3451
		private bool load_completed;

		// Token: 0x04000D7C RID: 3452
		private int load_timeout = 10000;

		// Token: 0x04000D7D RID: 3453
		private AudioDevice adev;

		// Token: 0x04000D7E RID: 3454
		private AudioData adata;

		// Token: 0x04000D7F RID: 3455
		private bool stopped;

		// Token: 0x04000D80 RID: 3456
		private Win32SoundPlayer win32_player;

		// Token: 0x04000D81 RID: 3457
		private static readonly bool use_win32_player = Environment.OSVersion.Platform != PlatformID.Unix;
	}
}
