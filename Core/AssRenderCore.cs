using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using K4os.Compression.LZ4;

namespace Ruminoid.Common.Renderer.Core
{
    public sealed class RenderedImage
    {
        public static readonly RenderedImage Empty = new RenderedImage();
        public int Width, Height, Stride;
        public byte[] Buffer;

        private bool Equals(RenderedImage other)
        {
            return Width == other.Width && Height == other.Height && Stride == other.Stride && Equals(Buffer, other.Buffer);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is RenderedImage other && Equals(other);
        }

        public override int GetHashCode()
        {
            if (ReferenceEquals(this, Empty)) return 0;
            unchecked
            {
                var hashCode = Width;
                hashCode = (hashCode * 397) ^ Height;
                hashCode = (hashCode * 397) ^ Stride;
                hashCode = (hashCode * 397) ^ (Buffer != null ? Buffer.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    public class DecodedImage
    {
        public int Width, Height;
        public byte[] Buffer;
    }

    public sealed class RenderCoreRenderer : IDisposable
    {
        private IntPtr _this;

        public RenderCoreRenderer(IntPtr _this)
        {
            this._this = _this;
        }

        public unsafe RenderedImage Render(int width, int height, int milliSec)
        {
            ruminoid_rendercore.RuminoidRcRenderFrame(_this, width, height, milliSec);
            var result = ruminoid_rendercore.RuminoidRcGetResult(_this);

            var image = new RenderedImage {
                Width = result.Width, Height = result.Height, Stride = result.Stride
            };
            image.Buffer = new byte[result.CompressedSize];
            Marshal.Copy((IntPtr) result.Buffer, image.Buffer, 0, (int)result.CompressedSize);

            return image;
        }

        public void Dispose()
        {
            ruminoid_rendercore.RuminoidRcDestroyRenderContext(_this);
            _this = IntPtr.Zero;
        }
    }

    public sealed class AssRenderCore : IDisposable
    {
        #region User Data

        private readonly int _width, _height, _glyphMax, _bitmapMax;

        #endregion

        #region Core Data

        private IntPtr _rcContext;

        #endregion

        #region Constructor

        public AssRenderCore(
            ref string subData,
            int width,
            int height,
            int glyphMax,
            int bitmapMax)
        {
            // Initialize User Data
            _width = width;
            _height = height;
            _glyphMax = glyphMax;
            _bitmapMax = bitmapMax;

            // Initialize Core Data
            _rcContext = ruminoid_rendercore.RuminoidRcNewContext();
            UpdateSubtitle(ref subData, (ulong)Encoding.UTF8.GetByteCount(subData));
        }

        #endregion

        #region Methods

        public RenderCoreRenderer CreateRenderer()
        {
            var context = ruminoid_rendercore.RuminoidRcNewRenderContext(_rcContext);
            ruminoid_rendercore.RuminoidRcSetCacheLimits(context, _glyphMax, _bitmapMax);
            return new RenderCoreRenderer(context);
        }

        public void UpdateSubtitle(ref string subData, ulong length) =>
            ruminoid_rendercore.RuminoidRcUpdateSubtitle(_rcContext, subData, length);

        public static (DecodedImage, byte[]) Decode(byte[] buffer, RenderedImage compressedImage)
        {
            int width = compressedImage.Width,
                height = compressedImage.Height,
                stride = compressedImage.Stride;

            if (buffer == null || buffer.Length < width * stride)
                buffer = new byte[width * stride];
            LZ4Codec.Decode(
                compressedImage.Buffer, 0, compressedImage.Buffer.Length,
                buffer, 0, buffer.Length);
            if (width * 4 != stride)
                for (var line = 0; line < height; line++)
                {
                    Array.Copy(buffer, line * stride,
                        buffer, line * width * 4, width * 4);
                }
            return (new DecodedImage { Width = width, Height = height, Buffer = buffer}, buffer);
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            ruminoid_rendercore.RuminoidRcDestroyContext(_rcContext); // no need to check
            _rcContext = IntPtr.Zero;
        }

        #endregion
    }
}
