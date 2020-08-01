using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using K4os.Compression.LZ4;

namespace Ruminoid.Common.Renderer.Core
{
    public sealed class AssRenderCore : IDisposable
    {
        #region User Data

        private readonly int _width, _height, _threadCount;

        #endregion

        #region Core Data

        private IntPtr _rcContext;
        private IntPtr[] _renderContexts;

        #endregion

        #region Constructor

        public AssRenderCore(
            ref string subData,
            int width,
            int height,
            int threadCount,
            int glyphMax,
            int bitmapMax)
        {
            // Initialize User Data
            _width = width;
            _height = height;
            _threadCount = threadCount;

            // Initialize Core Data
            _rcContext = ruminoid_rendercore.RuminoidRcNewContext();
            _renderContexts = new IntPtr[threadCount];
            for (int i = 0; i < threadCount; i++)
            {
                _renderContexts[i] = ruminoid_rendercore.RuminoidRcNewRenderContext(_rcContext);
                ruminoid_rendercore.RuminoidRcSetCacheLimits(_renderContexts[i], glyphMax, bitmapMax);
            }
            UpdateSubtitle(ref subData, (ulong)subData.Length);
        }

        #endregion

        #region Methods

        public RuminoidImageT Render(int threadIndex, int milliSec)
        {
            ruminoid_rendercore.RuminoidRcRenderFrame(_renderContexts[threadIndex], _width, _height, milliSec);
            return CopyImage(ruminoid_rendercore.RuminoidRcGetResult(_renderContexts[threadIndex]));
        }

        private static unsafe RuminoidImageT CopyImage(RuminoidImageT img)
        {
            RuminoidImageT result = new RuminoidImageT()
            {
                CompressedSize = img.CompressedSize,
                Width = img.Width,
                Height = img.Height,
                Stride = img.Stride,
                Buffer = (byte*)Marshal.AllocHGlobal((int)img.CompressedSize)
            };

            for (ulong i = 0; i < img.CompressedSize; i++)
                result.Buffer[i] = img.Buffer[i];

            return result;
        }

        public void UpdateSubtitle(ref string subData, ulong length) =>
            ruminoid_rendercore.RuminoidRcUpdateSubtitle(_rcContext, subData, length);

        public static unsafe IntPtr Decode(RuminoidImageT image)
        {
            int width = image.Width,
                height = image.Height,
                stride = image.Stride;

            byte* result = (byte*) Marshal.AllocHGlobal(width * height * 4);

            LZ4Codec.Decode(image.Buffer, (int)image.CompressedSize, result, height * stride);
            if (image.Width * 4 != image.Stride)
                for (int line = 1; line < height; line++)
                {
                    for (int i = 0; i < width * 4; i++)
                        result[line * width * 4 + i] = result[line * stride + i];
                }

            return (IntPtr) result;
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            for (int i = 0; i < _threadCount; i++)
                if (_renderContexts[i] != IntPtr.Zero)
                    ruminoid_rendercore.RuminoidRcDestroyRenderContext(_renderContexts[i]);
            if (_rcContext != IntPtr.Zero)
                ruminoid_rendercore.RuminoidRcDestroyContext(_rcContext);
        }

        #endregion
    }
}
