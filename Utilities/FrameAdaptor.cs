using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruminoid.Common.Renderer.Utilities
{
    public sealed class FrameAdaptor
    {
        #region Structure

        public int FrameRate;
        public int TotalFrame;

        #endregion

        #region Constructor

        public FrameAdaptor(int frameRate, int total)
        {
            FrameRate = frameRate;
            TotalFrame = (int) Math.Floor((double) total * frameRate / 1000);
        }

        #endregion

        #region Methods

        public int GetFrameIndex(int milliSec) =>
            (int) Math.Floor((double) milliSec * FrameRate / 1000);

        public int GetMilliSec(int frameIndex) =>
            (int) Math.Floor((double) frameIndex * 1000 / FrameRate);

        #endregion
    }
}
