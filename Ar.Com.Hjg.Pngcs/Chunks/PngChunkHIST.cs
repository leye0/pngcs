// --------------------------------------------------------------------------------------------------
// This file was automatically generated by J2CS Translator (http://j2cstranslator.sourceforge.net/). 
// Version 1.3.6.20110331_01     
// 6/1/11 9:13 a.m.    
// ${CustomMessageForDisclaimer}                                                                             
// --------------------------------------------------------------------------------------------------
 namespace Ar.Com.Hjg.Pngcs.Chunks {
	
	using Ar.Com.Hjg.Pngcs;
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.IO;
	using System.Runtime.CompilerServices;
	
	/*
	 */
	public class PngChunkHIST : PngChunkSingle {
        public readonly static String ID = ChunkHelper.hIST;

    // http://www.w3.org/TR/PNG/#11hIST
	// only for palette images

	private int[] hist = new int[0]; // should have same lenght as palette

    public PngChunkHIST(ImageInfo info)
        : base(ID, info)
    {	}
	

	public override ChunkOrderingConstraint GetOrderingConstraint() {
		return ChunkOrderingConstraint.AFTER_PLTE_BEFORE_IDAT;
	}

    public override ChunkRaw CreateRawChunk()
    {
        ChunkRaw c = null;
        if (!ImgInfo.Indexed)
            throw new PngjException("only indexed images accept a HIST chunk");

             c = createEmptyChunk(hist.Length * 2, true);
             for (int i = 0; i < hist.Length; i++)
             {
                 PngHelperInternal.WriteInt2tobytes(hist[i], c.Data, i * 2);
             }
             return c;
		}
	
		public override void ParseFromRaw(ChunkRaw c) {
            if (!ImgInfo.Indexed)
                throw new PngjException("only indexed images accept a HIST chunk");
            int nentries = c.Data.Length / 2;
            hist = new int[nentries];
            for (int i = 0; i < hist.Length; i++)
            {
                hist[i] = PngHelperInternal.ReadInt2fromBytes(c.Data, i * 2);
            }
		}
	
		public override void CloneDataFromRead(PngChunk other) {
            PngChunkHIST otherx = (PngChunkHIST)other;
            hist = new int[otherx.hist.Length];
            System.Array.Copy((Array)(otherx.hist), 0, (Array)(this.hist), 0, otherx.hist.Length);
		}

        public int[] GetHist()
        {
            return hist;
        }

        public void SetHist(int[] hist)
        {
            this.hist = hist;
        }

	}
}