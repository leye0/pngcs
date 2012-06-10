// --------------------------------------------------------------------------------------------------
// This file was automatically generated by J2CS Translator (http://j2cstranslator.sourceforge.net/). 
// Version 1.3.6.20110331_01     
// 6/1/11 9:14 a.m.    
// ${CustomMessageForDisclaimer}                                                                             
// --------------------------------------------------------------------------------------------------
 namespace Ar.Com.Hjg.Pngcs {
	
	using Ar.Com.Hjg.Pngcs.Chunks;
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.IO;
	using System.Runtime.CompilerServices;
	
	/// <summary>
     /// outputs the stream for IDAT chunk , fragmented at fixed size (32k default).
	/// </summary>
	///
	internal class PngIDatChunkOutputStream : ProgressiveOutputStream {
        private const int SIZE_DEFAULT = 32768;// 32k
		private readonly Stream outputStream;
	
		public PngIDatChunkOutputStream(Stream outputStream_0) : this(outputStream_0, SIZE_DEFAULT) {
		}
	
		public PngIDatChunkOutputStream(Stream outputStream_0, int size) : base(size>8?size:SIZE_DEFAULT)
             {
			this.outputStream = outputStream_0;
		}
	
		public override void FlushBuffer(byte[] b, int len) {
			ChunkRaw c = new ChunkRaw(len, Ar.Com.Hjg.Pngcs.Chunks.ChunkHelper.b_IDAT, false);
			c.Data = b;
			c.WriteChunk(outputStream);
		}
	}
}