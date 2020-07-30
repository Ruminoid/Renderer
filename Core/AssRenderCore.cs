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

        private int _width, _height;

        #endregion

        #region Core Data

        private IntPtr _rcContext;

        #endregion

        #region Constructor

        public AssRenderCore(
            string subData,
            int width,
            int height)
        {
            // Initialize User Data
            _width = width;
            _height = height;

            // Initialize Core Data
            _rcContext = ruminoid_rendercore.RuminoidRcNewContext();
            UpdateSubtitle(subData, (ulong) subData.Length);
        }

        #endregion

        #region Methods

        public RuminoidImageT Render(int milliSec)
        {
            ruminoid_rendercore.RuminoidRcRenderFrame(_rcContext, _width, _height, milliSec);
            return ruminoid_rendercore.RuminoidRcGetResult(_rcContext);
        }

        public void UpdateSubtitle(string subData, ulong length) =>
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
            if (_rcContext != IntPtr.Zero)
                ruminoid_rendercore.RuminoidRcDestroyContext(_rcContext);
        }

        #endregion
    }
}
