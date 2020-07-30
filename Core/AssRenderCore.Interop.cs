using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Ruminoid.Common.Renderer.Core
{
    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
    public unsafe delegate void ruminoid_rc_log_callback(int _0, [MarshalAs(UnmanagedType.LPUTF8Str)] string _1);

    public unsafe partial class RuminoidImageT : IDisposable
    {
        [StructLayout(LayoutKind.Explicit, Size = 32)]
        public partial struct __Internal
        {
            [FieldOffset(0)]
            internal int width;

            [FieldOffset(4)]
            internal int height;

            [FieldOffset(8)]
            internal int stride;

            [FieldOffset(16)]
            internal global::System.IntPtr buffer;

            [FieldOffset(24)]
            internal ulong compressed_size;

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Ruminoid.Common.Renderer.Core.dll", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "??0ruminoid_image_t@@QEAA@AEBU0@@Z")]
            internal static extern global::System.IntPtr cctor(global::System.IntPtr __instance, global::System.IntPtr _0);
        }

        public global::System.IntPtr __Instance { get; protected set; }

        internal static readonly global::System.Collections.Concurrent.ConcurrentDictionary<IntPtr, global::Ruminoid.Common.Renderer.Core.RuminoidImageT> NativeToManagedMap = new global::System.Collections.Concurrent.ConcurrentDictionary<IntPtr, global::Ruminoid.Common.Renderer.Core.RuminoidImageT>();

        protected bool __ownsNativeInstance;

        internal static global::Ruminoid.Common.Renderer.Core.RuminoidImageT __CreateInstance(global::System.IntPtr native, bool skipVTables = false)
        {
            return new global::Ruminoid.Common.Renderer.Core.RuminoidImageT(native.ToPointer(), skipVTables);
        }

        internal static global::Ruminoid.Common.Renderer.Core.RuminoidImageT __CreateInstance(global::Ruminoid.Common.Renderer.Core.RuminoidImageT.__Internal native, bool skipVTables = false)
        {
            return new global::Ruminoid.Common.Renderer.Core.RuminoidImageT(native, skipVTables);
        }

        private static void* __CopyValue(global::Ruminoid.Common.Renderer.Core.RuminoidImageT.__Internal native)
        {
            var ret = Marshal.AllocHGlobal(sizeof(global::Ruminoid.Common.Renderer.Core.RuminoidImageT.__Internal));
            *(global::Ruminoid.Common.Renderer.Core.RuminoidImageT.__Internal*)ret = native;
            return ret.ToPointer();
        }

        private RuminoidImageT(global::Ruminoid.Common.Renderer.Core.RuminoidImageT.__Internal native, bool skipVTables = false)
            : this(__CopyValue(native), skipVTables)
        {
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
        }

        protected RuminoidImageT(void* native, bool skipVTables = false)
        {
            if (native == null)
                return;
            __Instance = new global::System.IntPtr(native);
        }

        public RuminoidImageT()
        {
            __Instance = Marshal.AllocHGlobal(sizeof(global::Ruminoid.Common.Renderer.Core.RuminoidImageT.__Internal));
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
        }

        public RuminoidImageT(global::Ruminoid.Common.Renderer.Core.RuminoidImageT _0)
        {
            __Instance = Marshal.AllocHGlobal(sizeof(global::Ruminoid.Common.Renderer.Core.RuminoidImageT.__Internal));
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
            *((global::Ruminoid.Common.Renderer.Core.RuminoidImageT.__Internal*)__Instance) = *((global::Ruminoid.Common.Renderer.Core.RuminoidImageT.__Internal*)_0.__Instance);
        }

        ~RuminoidImageT()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (__Instance == IntPtr.Zero)
                return;
            global::Ruminoid.Common.Renderer.Core.RuminoidImageT __dummy;
            NativeToManagedMap.TryRemove(__Instance, out __dummy);
            if (__ownsNativeInstance)
                Marshal.FreeHGlobal(__Instance);
            __Instance = IntPtr.Zero;
        }

        public int Width
        {
            get
            {
                return ((global::Ruminoid.Common.Renderer.Core.RuminoidImageT.__Internal*)__Instance)->width;
            }

            set
            {
                ((global::Ruminoid.Common.Renderer.Core.RuminoidImageT.__Internal*)__Instance)->width = value;
            }
        }

        public int Height
        {
            get
            {
                return ((global::Ruminoid.Common.Renderer.Core.RuminoidImageT.__Internal*)__Instance)->height;
            }

            set
            {
                ((global::Ruminoid.Common.Renderer.Core.RuminoidImageT.__Internal*)__Instance)->height = value;
            }
        }

        public int Stride
        {
            get
            {
                return ((global::Ruminoid.Common.Renderer.Core.RuminoidImageT.__Internal*)__Instance)->stride;
            }

            set
            {
                ((global::Ruminoid.Common.Renderer.Core.RuminoidImageT.__Internal*)__Instance)->stride = value;
            }
        }

        public byte* Buffer
        {
            get
            {
                return (byte*)((global::Ruminoid.Common.Renderer.Core.RuminoidImageT.__Internal*)__Instance)->buffer;
            }

            set
            {
                ((global::Ruminoid.Common.Renderer.Core.RuminoidImageT.__Internal*)__Instance)->buffer = (global::System.IntPtr)value;
            }
        }

        public ulong CompressedSize
        {
            get
            {
                return ((global::Ruminoid.Common.Renderer.Core.RuminoidImageT.__Internal*)__Instance)->compressed_size;
            }

            set
            {
                ((global::Ruminoid.Common.Renderer.Core.RuminoidImageT.__Internal*)__Instance)->compressed_size = value;
            }
        }
    }

    public unsafe partial class ruminoid_rendercore
    {
        public partial struct __Internal
        {
            [SuppressUnmanagedCodeSecurity]
            [DllImport("ruminoid_rendercore", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "ruminoid_rc_new_context")]
            internal static extern global::System.IntPtr RuminoidRcNewContext();

            [SuppressUnmanagedCodeSecurity]
            [DllImport("ruminoid_rendercore", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "ruminoid_rc_destroy_context")]
            internal static extern void RuminoidRcDestroyContext(global::System.IntPtr context);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("ruminoid_rendercore", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "ruminoid_rc_render_frame")]
            internal static extern void RuminoidRcRenderFrame(global::System.IntPtr context, int width, int height, int timeMs);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("ruminoid_rendercore", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "ruminoid_rc_update_subtitle")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool RuminoidRcUpdateSubtitle(global::System.IntPtr context, [MarshalAs(UnmanagedType.LPUTF8Str)] string content, ulong length);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("ruminoid_rendercore", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "ruminoid_rc_get_result")]
            internal static extern global::System.IntPtr RuminoidRcGetResult(global::System.IntPtr context);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("ruminoid_rendercore", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "ruminoid_rc_attach_log_callback")]
            internal static extern void RuminoidRcAttachLogCallback(global::System.IntPtr context, global::System.IntPtr callback);
        }

        public static global::System.IntPtr RuminoidRcNewContext()
        {
            var __ret = __Internal.RuminoidRcNewContext();
            return __ret;
        }

        public static void RuminoidRcDestroyContext(global::System.IntPtr context)
        {
            __Internal.RuminoidRcDestroyContext(context);
        }

        public static void RuminoidRcRenderFrame(global::System.IntPtr context, int width, int height, int timeMs)
        {
            __Internal.RuminoidRcRenderFrame(context, width, height, timeMs);
        }

        public static bool RuminoidRcUpdateSubtitle(global::System.IntPtr context, string content, ulong length)
        {
            var __ret = __Internal.RuminoidRcUpdateSubtitle(context, content, length);
            return __ret;
        }

        public static global::Ruminoid.Common.Renderer.Core.RuminoidImageT RuminoidRcGetResult(global::System.IntPtr context)
        {
            var __ret = __Internal.RuminoidRcGetResult(context);
            global::Ruminoid.Common.Renderer.Core.RuminoidImageT __result0;
            if (__ret == IntPtr.Zero) __result0 = null;
            else if (global::Ruminoid.Common.Renderer.Core.RuminoidImageT.NativeToManagedMap.ContainsKey(__ret))
                __result0 = (global::Ruminoid.Common.Renderer.Core.RuminoidImageT)global::Ruminoid.Common.Renderer.Core.RuminoidImageT.NativeToManagedMap[__ret];
            else __result0 = global::Ruminoid.Common.Renderer.Core.RuminoidImageT.__CreateInstance(__ret);
            return __result0;
        }

        public static void RuminoidRcAttachLogCallback(global::System.IntPtr context, global::Ruminoid.Common.Renderer.Core.ruminoid_rc_log_callback callback)
        {
            var __arg1 = callback == null ? global::System.IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(callback);
            __Internal.RuminoidRcAttachLogCallback(context, __arg1);
        }
    }
}
