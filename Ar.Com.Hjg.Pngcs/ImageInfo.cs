// --------------------------------------------------------------------------------------------------
// This file was automatically generated by J2CS Translator (http://j2cstranslator.sourceforge.net/). 
// Version 1.3.6.20110331_01     
// 6/1/11 9:13 a.m.    
// ${CustomMessageForDisclaimer}                                                                             
// --------------------------------------------------------------------------------------------------
namespace Ar.Com.Hjg.Pngcs
{

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Simple IMMUTABLE wrapper for basic image info
    /// Some parameters are clearly redundant
    /// The constructor requires an 'ortogonal' subset
    /// http://www.w3.org/TR/PNG/#11IHDR
    /// </summary>
    ///
    public class ImageInfo
    {
        /// <summary>
        /// image width, in pixels
        /// </summary>
        ///
        public readonly int Cols;

        /// <summary>
        /// image height, in pixels
        /// </summary>
        ///
        public readonly int Rows;

        /// <summary>
        /// Bits per sample (per channel) in the buffer. This is 8 or 16 for RGB/ARGB
        /// images, For grayscale, it's 8 (or 1 2 4 ) For indexed images, number of
        /// bits per palette index (1 2 4 8)
        /// </summary>
        ///
        public readonly int BitDepth;

        /// <summary>
        /// Number of channels, used in the buffer (warning!) This is 3-4 for rgb/rgba,
        /// but 1 for palette/gray !
        /// </summary>
        ///
        public readonly int Channels;

        /// <summary>
        /// Bits used for each pixel in the buffer equals channel/// bitDepth
        /// </summary>
        ///
        public readonly int BitspPixel;
        public readonly int BytesPixel; // rounded up : this is only for filter!
        /// <summary>
        /// ceil(bitspp///cols/8)
        /// </summary>
        ///
        public readonly int BytesPerRow;
        /// <summary>
        /// Equals cols x channels
        /// </summary>
        ///
        public readonly int SamplesPerRow;
        /// <summary>
        /// Samples available for our packed scanline Equals samplesPerRow if not
        /// packed Elsewhere, it's lower
        /// </summary>
        ///
        public readonly int SamplesPerRowP;
        public readonly bool Alpha;
        public readonly bool Greyscale;
        public readonly bool Indexed;
        // less than one byte per sample (bit depth 1-2-4)
        public readonly bool Packed;
        private const int MAX_COLS_ROWS_VAL = 400000; // very big value

        /// <summary>
        /// Constructor default: only for RGB (truecolor)!
        /// </summary>
        ///
        public ImageInfo(int cols_0, int rows_1, int bitdepth, bool alpha_2)
            : this(cols_0, rows_1, bitdepth, alpha_2, false, false)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        ///
        /// <param name="cols_0">width in pixels</param>
        /// <param name="rows_1">height in pixels</param>
        /// <param name="bitdepth"></param>
        /// <param name="alpha_2"></param>
        /// <param name="grayscale"></param>
        /// <param name="palette"></param>
        public ImageInfo(int cols_0, int rows_1, int bitdepth, bool alpha_2, bool grayscale,
                bool palette)
        {
            this.Cols = cols_0;
            this.Rows = rows_1;
            this.Alpha = alpha_2;
            this.Indexed = palette;
            this.Greyscale = grayscale;
            if (Greyscale && palette)
                throw new PngjException("palette and greyscale are exclusive");
            this.Channels = (grayscale || palette) ? ((alpha_2) ? 2 : 1) : ((alpha_2) ? 4 : 3);
            // http://www.w3.org/TR/PNG/#11IHDR
            this.BitDepth = bitdepth;
            this.Packed = bitdepth < 8;
            this.BitspPixel = (Channels * this.BitDepth);
            this.BytesPixel = (BitspPixel + 7) / 8;
            this.BytesPerRow = (BitspPixel * cols_0 + 7) / 8;
            this.SamplesPerRow = Channels * this.Cols;
            this.SamplesPerRowP = (Packed) ? BytesPerRow : SamplesPerRow;
            // checks
            switch (this.BitDepth)
            {
                case 1:
                case 2:
                case 4:
                    if (!(this.Indexed || this.Greyscale))
                        throw new PngjException("only indexed or grayscale can have bitdepth="
                                + this.BitDepth);
                    break;
                case 8:
                    break;
                case 16:
                    if (this.Indexed)
                        throw new PngjException("indexed can't have bitdepth=" + this.BitDepth);
                    break;
                default:
                    throw new PngjException("invalid bitdepth=" + this.BitDepth);
            }
            if (cols_0 < 1 || cols_0 > MAX_COLS_ROWS_VAL)
                throw new PngjException("invalid cols=" + cols_0 + " ???");
            if (rows_1 < 1 || rows_1 > MAX_COLS_ROWS_VAL)
                throw new PngjException("invalid rows=" + rows_1 + " ???");
        }

        public override String ToString()
        {
            return "ImageInfo [cols=" + Cols + ", rows=" + Rows + ", bitDepth=" + BitDepth
                    + ", channels=" + Channels + ", bitspPixel=" + BitspPixel + ", bytesPixel="
                    + BytesPixel + ", bytesPerRow=" + BytesPerRow + ", samplesPerRow="
                    + SamplesPerRow + ", samplesPerRowP=" + SamplesPerRowP + ", alpha=" + Alpha
                    + ", greyscale=" + Greyscale + ", indexed=" + Indexed + ", packed=" + Packed
                    + "]";
        }

        public override int GetHashCode()
        {
            int prime = 31;
            int result = 1;
            result = prime * result + ((Alpha) ? 1231 : 1237);
            result = prime * result + BitDepth;
            result = prime * result + BitspPixel;
            result = prime * result + BytesPerRow;
            result = prime * result + BytesPixel;
            result = prime * result + Channels;
            result = prime * result + Cols;
            result = prime * result + ((Greyscale) ? 1231 : 1237);
            result = prime * result + ((Indexed) ? 1231 : 1237);
            result = prime * result + ((Packed) ? 1231 : 1237);
            result = prime * result + Rows;
            result = prime * result + SamplesPerRow;
            return result;
        }

        public override bool Equals(Object obj)
        {
            if ((Object)this == obj)
                return true;
            if (obj == null)
                return false;
            if ((Object)GetType() != (Object)obj.GetType())
                return false;
            ImageInfo other = (ImageInfo)obj;
            if (Alpha != other.Alpha)
                return false;
            if (BitDepth != other.BitDepth)
                return false;
            if (BitspPixel != other.BitspPixel)
                return false;
            if (BytesPerRow != other.BytesPerRow)
                return false;
            if (BytesPixel != other.BytesPixel)
                return false;
            if (Channels != other.Channels)
                return false;
            if (Cols != other.Cols)
                return false;
            if (Greyscale != other.Greyscale)
                return false;
            if (Indexed != other.Indexed)
                return false;
            if (Packed != other.Packed)
                return false;
            if (Rows != other.Rows)
                return false;
            if (SamplesPerRow != other.SamplesPerRow)
                return false;
            return true;
        }
    }
}