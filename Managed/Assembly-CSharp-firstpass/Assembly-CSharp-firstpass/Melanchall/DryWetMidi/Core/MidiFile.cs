using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200017D RID: 381
	public sealed class MidiFile
	{
		// Token: 0x0600094C RID: 2380 RVA: 0x0002086F File Offset: 0x0001EA6F
		public MidiFile()
		{
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0002088D File Offset: 0x0001EA8D
		public MidiFile(IEnumerable<MidiChunk> chunks)
		{
			ThrowIfArgument.IsNull("chunks", chunks);
			this.Chunks.AddRange(chunks);
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x000208C2 File Offset: 0x0001EAC2
		public MidiFile(params MidiChunk[] chunks)
			: this(chunks)
		{
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x000208CB File Offset: 0x0001EACB
		// (set) Token: 0x06000950 RID: 2384 RVA: 0x000208D3 File Offset: 0x0001EAD3
		public TimeDivision TimeDivision { get; set; } = new TicksPerQuarterNoteTimeDivision();

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x000208DC File Offset: 0x0001EADC
		public ChunksCollection Chunks { get; } = new ChunksCollection();

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x000208E4 File Offset: 0x0001EAE4
		// (set) Token: 0x06000953 RID: 2387 RVA: 0x00020934 File Offset: 0x0001EB34
		public MidiFileFormat OriginalFormat
		{
			get
			{
				if (this._originalFormat == null)
				{
					throw new InvalidOperationException("Unable to get original format of the file.");
				}
				ushort value = this._originalFormat.Value;
				if (!Enum.IsDefined(typeof(MidiFileFormat), value))
				{
					throw new UnknownFileFormatException(value);
				}
				return (MidiFileFormat)value;
			}
			internal set
			{
				this._originalFormat = new ushort?((ushort)value);
			}
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00020944 File Offset: 0x0001EB44
		public static MidiFile Read(string filePath, ReadingSettings settings = null)
		{
			MidiFile midiFile;
			using (FileStream fileStream = FileUtilities.OpenFileForRead(filePath))
			{
				midiFile = MidiFile.Read(fileStream, settings);
			}
			return midiFile;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00020980 File Offset: 0x0001EB80
		public void Write(string filePath, bool overwriteFile = false, MidiFileFormat format = MidiFileFormat.MultiTrack, WritingSettings settings = null)
		{
			ThrowIfArgument.IsInvalidEnumValue<MidiFileFormat>("format", format);
			using (FileStream fileStream = FileUtilities.OpenFileForWrite(filePath, overwriteFile))
			{
				this.Write(fileStream, format, settings);
			}
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x000209C8 File Offset: 0x0001EBC8
		public static MidiFile Read(Stream stream, ReadingSettings settings = null)
		{
			ThrowIfArgument.IsNull("stream", stream);
			if (!stream.CanRead)
			{
				throw new ArgumentException("Stream doesn't support reading.", "stream");
			}
			if (settings == null)
			{
				settings = new ReadingSettings();
			}
			if (settings.ReaderSettings == null)
			{
				settings.ReaderSettings = new ReaderSettings();
			}
			settings.PrepareReadingHandlers();
			bool useReadingHandlers = settings.UseReadingHandlers;
			ICollection<ReadingHandler> fileReadingHandlers = settings.FileReadingHandlers;
			ICollection<ReadingHandler> trackChunkReadingHandlers = settings.TrackChunkReadingHandlers;
			if (useReadingHandlers)
			{
				foreach (ReadingHandler readingHandler in fileReadingHandlers)
				{
					readingHandler.OnStartFileReading();
				}
			}
			MidiFile midiFile = new MidiFile();
			int? num = null;
			int num2 = 0;
			bool flag = false;
			try
			{
				using (MidiReader midiReader = new MidiReader(stream, settings.ReaderSettings))
				{
					if (midiReader.EndReached)
					{
						throw new ArgumentException("Stream is already read.", "stream");
					}
					long? num3 = null;
					string text = midiReader.ReadString("RIFF".Length);
					if (text == "RIFF")
					{
						midiReader.Position += 12L;
						uint num4 = midiReader.ReadDword();
						num3 = new long?(midiReader.Position + (long)((ulong)num4));
					}
					else
					{
						midiReader.Position -= (long)text.Length;
					}
					while (!midiReader.EndReached)
					{
						if (num3 != null)
						{
							long position = midiReader.Position;
							long? num5 = num3;
							if (!((position < num5.GetValueOrDefault()) & (num5 != null)))
							{
								break;
							}
						}
						MidiChunk midiChunk = MidiFile.ReadChunk(midiReader, settings, num2, num, trackChunkReadingHandlers);
						if (midiChunk != null)
						{
							HeaderChunk headerChunk = midiChunk as HeaderChunk;
							if (headerChunk != null)
							{
								if (!flag)
								{
									num = new int?((int)headerChunk.TracksNumber);
									midiFile.TimeDivision = headerChunk.TimeDivision;
									midiFile._originalFormat = new ushort?(headerChunk.FileFormat);
									if (useReadingHandlers)
									{
										foreach (ReadingHandler readingHandler2 in fileReadingHandlers)
										{
											readingHandler2.OnFinishHeaderChunkReading(headerChunk.TimeDivision);
										}
									}
								}
								flag = true;
							}
							else
							{
								if (midiChunk is TrackChunk)
								{
									num2++;
								}
								midiFile.Chunks.Add(midiChunk);
							}
						}
					}
					if (num != null)
					{
						int num6 = num2;
						int? num7 = num;
						if (!((num6 == num7.GetValueOrDefault()) & (num7 != null)))
						{
							MidiFile.ReactOnUnexpectedTrackChunksCount(settings.UnexpectedTrackChunksCountPolicy, num2, num.Value);
						}
					}
				}
				if (!flag)
				{
					midiFile.TimeDivision = null;
					if (settings.NoHeaderChunkPolicy == NoHeaderChunkPolicy.Abort)
					{
						throw new NoHeaderChunkException();
					}
				}
			}
			catch (NotEnoughBytesException ex)
			{
				MidiFile.ReactOnNotEnoughBytes(settings.NotEnoughBytesPolicy, ex);
			}
			catch (EndOfStreamException ex2)
			{
				MidiFile.ReactOnNotEnoughBytes(settings.NotEnoughBytesPolicy, ex2);
			}
			if (useReadingHandlers)
			{
				foreach (ReadingHandler readingHandler3 in fileReadingHandlers)
				{
					readingHandler3.OnFinishFileReading(midiFile);
				}
			}
			return midiFile;
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00020D3C File Offset: 0x0001EF3C
		public void Write(Stream stream, MidiFileFormat format = MidiFileFormat.MultiTrack, WritingSettings settings = null)
		{
			ThrowIfArgument.IsNull("stream", stream);
			ThrowIfArgument.IsInvalidEnumValue<MidiFileFormat>("format", format);
			if (this.TimeDivision == null)
			{
				throw new InvalidOperationException("Time division is null.");
			}
			if (!stream.CanWrite)
			{
				throw new ArgumentException("Stream doesn't support writing.", "stream");
			}
			if (settings == null)
			{
				settings = new WritingSettings();
			}
			using (MidiWriter midiWriter = new MidiWriter(stream))
			{
				IEnumerable<MidiChunk> enumerable = ChunksConverterFactory.GetConverter(format).Convert(this.Chunks);
				int num = enumerable.Count((MidiChunk c) => c is TrackChunk);
				if (num > 65535)
				{
					throw new TooManyTrackChunksException(num);
				}
				new HeaderChunk
				{
					FileFormat = (ushort)format,
					TimeDivision = this.TimeDivision,
					TracksNumber = (ushort)num
				}.Write(midiWriter, settings);
				foreach (MidiChunk midiChunk in enumerable)
				{
					if (!settings.CompressionPolicy.HasFlag(CompressionPolicy.DeleteUnknownChunks) || !(midiChunk is UnknownChunk))
					{
						midiChunk.Write(midiWriter, settings);
					}
				}
			}
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00020E7C File Offset: 0x0001F07C
		public MidiFile Clone()
		{
			return new MidiFile(this.Chunks.Select((MidiChunk c) => c.Clone()))
			{
				TimeDivision = this.TimeDivision.Clone(),
				_originalFormat = this._originalFormat
			};
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00020ED8 File Offset: 0x0001F0D8
		public static bool Equals(MidiFile midiFile1, MidiFile midiFile2)
		{
			string text;
			return MidiFile.Equals(midiFile1, midiFile2, out text);
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x00020EEE File Offset: 0x0001F0EE
		public static bool Equals(MidiFile midiFile1, MidiFile midiFile2, out string message)
		{
			return MidiFile.Equals(midiFile1, midiFile2, null, out message);
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00020EF9 File Offset: 0x0001F0F9
		public static bool Equals(MidiFile midiFile1, MidiFile midiFile2, MidiFileEqualityCheckSettings settings, out string message)
		{
			return MidiFileEquality.Equals(midiFile1, midiFile2, settings ?? new MidiFileEqualityCheckSettings(), out message);
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00020F10 File Offset: 0x0001F110
		private static MidiChunk ReadChunk(MidiReader reader, ReadingSettings settings, int actualTrackChunksCount, int? expectedTrackChunksCount, ICollection<ReadingHandler> trackChunkReadingHandlers)
		{
			MidiChunk midiChunk = null;
			try
			{
				string text = reader.ReadString(4);
				if (text.Length < 4)
				{
					NotEnoughBytesPolicy notEnoughBytesPolicy = settings.NotEnoughBytesPolicy;
					if (notEnoughBytesPolicy == NotEnoughBytesPolicy.Abort)
					{
						throw new NotEnoughBytesException("Chunk ID cannot be read since the reader's underlying stream doesn't have enough bytes.", 4L, (long)text.Length);
					}
					if (notEnoughBytesPolicy == NotEnoughBytesPolicy.Ignore)
					{
						return null;
					}
				}
				if (!(text == "MThd"))
				{
					if (!(text == "MTrk"))
					{
						midiChunk = MidiFile.TryCreateChunk(text, settings.CustomChunkTypes);
					}
					else
					{
						if (settings.UseReadingHandlers)
						{
							foreach (ReadingHandler readingHandler in trackChunkReadingHandlers)
							{
								readingHandler.OnStartTrackChunkReading();
							}
						}
						midiChunk = new TrackChunk();
					}
				}
				else
				{
					midiChunk = new HeaderChunk();
				}
				if (midiChunk == null)
				{
					switch (settings.UnknownChunkIdPolicy)
					{
					case UnknownChunkIdPolicy.ReadAsUnknownChunk:
						midiChunk = new UnknownChunk(text);
						break;
					case UnknownChunkIdPolicy.Skip:
					{
						uint num = reader.ReadDword();
						reader.Position += (long)((ulong)num);
						return null;
					}
					case UnknownChunkIdPolicy.Abort:
						throw new UnknownChunkException(text);
					}
				}
				if (midiChunk is TrackChunk && expectedTrackChunksCount != null)
				{
					int? num2 = expectedTrackChunksCount;
					if ((actualTrackChunksCount >= num2.GetValueOrDefault()) & (num2 != null))
					{
						MidiFile.ReactOnUnexpectedTrackChunksCount(settings.UnexpectedTrackChunksCountPolicy, actualTrackChunksCount, expectedTrackChunksCount.Value);
						ExtraTrackChunkPolicy extraTrackChunkPolicy = settings.ExtraTrackChunkPolicy;
						if (extraTrackChunkPolicy != ExtraTrackChunkPolicy.Read && extraTrackChunkPolicy == ExtraTrackChunkPolicy.Skip)
						{
							uint num3 = reader.ReadDword();
							reader.Position += (long)((ulong)num3);
							return null;
						}
					}
				}
				if (midiChunk != null)
				{
					midiChunk.Read(reader, settings);
				}
				if (settings.UseReadingHandlers && text == "MTrk")
				{
					foreach (ReadingHandler readingHandler2 in trackChunkReadingHandlers)
					{
						readingHandler2.OnFinishTrackChunkReading((TrackChunk)midiChunk);
					}
				}
			}
			catch (NotEnoughBytesException ex)
			{
				MidiFile.ReactOnNotEnoughBytes(settings.NotEnoughBytesPolicy, ex);
			}
			catch (EndOfStreamException ex2)
			{
				MidiFile.ReactOnNotEnoughBytes(settings.NotEnoughBytesPolicy, ex2);
			}
			return midiChunk;
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00021164 File Offset: 0x0001F364
		private static void ReactOnUnexpectedTrackChunksCount(UnexpectedTrackChunksCountPolicy policy, int actualTrackChunksCount, int expectedTrackChunksCount)
		{
			if (policy != UnexpectedTrackChunksCountPolicy.Ignore && policy == UnexpectedTrackChunksCountPolicy.Abort)
			{
				throw new UnexpectedTrackChunksCountException(actualTrackChunksCount, expectedTrackChunksCount);
			}
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00021175 File Offset: 0x0001F375
		private static void ReactOnNotEnoughBytes(NotEnoughBytesPolicy policy, Exception exception)
		{
			if (policy == NotEnoughBytesPolicy.Abort)
			{
				throw new NotEnoughBytesException("MIDI file cannot be read since the reader's underlying stream doesn't have enough bytes.", exception);
			}
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00021188 File Offset: 0x0001F388
		private static MidiChunk TryCreateChunk(string chunkId, ChunkTypesCollection chunksTypes)
		{
			Type type = null;
			if (chunksTypes == null || !chunksTypes.TryGetType(chunkId, out type) || !MidiFile.IsChunkType(type))
			{
				return null;
			}
			return (MidiChunk)Activator.CreateInstance(type);
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x000211BA File Offset: 0x0001F3BA
		private static bool IsChunkType(Type type)
		{
			return type != null && type.IsSubclassOf(typeof(MidiChunk)) && type.GetConstructor(Type.EmptyTypes) != null;
		}

		// Token: 0x040008E8 RID: 2280
		private const string RiffChunkId = "RIFF";

		// Token: 0x040008E9 RID: 2281
		private const int RmidPreambleSize = 12;

		// Token: 0x040008EA RID: 2282
		internal ushort? _originalFormat;
	}
}
