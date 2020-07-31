using System;
using System.Collections.Generic;
using System.Linq;
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
            int glaphMax,
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
                ruminoid_rendercore.RuminoidRcSetCacheLimits(_renderContexts[i], glaphMax, bitmapMax);
            }
            UpdateSubtitle(ref subData, (ulong)subData.Length);
        }

        #endregion

        #region Methods

        public RuminoidImageT Render(int threadIndex, int milliSec)
        {
            ruminoid_rendercore.RuminoidRcRenderFrame(_renderContexts[threadIndex], _width, _height, milliSec);
            return ruminoid_rendercore.RuminoidRcGetResult(_renderContexts[threadIndex]);
        }

        public void UpdateSubtitle(ref string subData, ulong length) =>
            ruminoid_rendercore.RuminoidRcUpdateSubtitle(_rcContext, subData, length);

        public static unsafe byte[] Decode(RuminoidImageT image)
        {
            int size = image.Width * image.Height * 4;
            byte[] result = new byte[size];
            fixed (byte* ptr = result)
                LZ4Codec.Decode(image.Buffer, (int) image.CompressedSize, ptr, size);
            return result;
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
