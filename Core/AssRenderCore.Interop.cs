using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Security;

namespace Ruminoid.Common.Renderer.Core
{
    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ruminoid_rc_log_callback(int _0, [MarshalAs(UnmanagedType.LPUTF8Str)] string _1);

    public unsafe class RuminoidImageT : IDisposable
    {
        [StructLayout(LayoutKind.Explicit, Size = 32)]
        public struct __Internal
        {
            [FieldOffset(0)]
            internal int width;

            [FieldOffset(4)]
            internal int height;

            [FieldOffset(8)]
            internal int stride;

            [FieldOffset(16)]
            internal IntPtr buffer;

            [FieldOffset(24)]
            internal ulong compressed_size;

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/ruminoid_rendercore.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "??0ruminoid_image_t@@QEAA@AEBU0@@Z")]
            internal static extern IntPtr cctor(IntPtr __instance, IntPtr _0);
        }

        public IntPtr __Instance { get; protected set; }

        internal static readonly ConcurrentDictionary<IntPtr, RuminoidImageT> NativeToManagedMap = new ConcurrentDictionary<IntPtr, RuminoidImageT>();

        protected bool __ownsNativeInstance;

        internal static RuminoidImageT __CreateInstance(IntPtr native, bool skipVTables = false)
        {
            return new RuminoidImageT(native.ToPointer(), skipVTables);
        }

        internal static RuminoidImageT __CreateInstance(__Internal native, bool skipVTables = false)
        {
            return new RuminoidImageT(native, skipVTables);
        }

        private static void* __CopyValue(__Internal native)
        {
            var ret = Marshal.AllocHGlobal(sizeof(__Internal));
            *(__Internal*)ret = native;
            return ret.ToPointer();
        }

        private RuminoidImageT(__Internal native, bool skipVTables = false)
            : this(__CopyValue(native), skipVTables)
        {
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
        }

        protected RuminoidImageT(void* native, bool skipVTables = false)
        {
            if (native == null)
                return;
            __Instance = new IntPtr(native);
        }

        public RuminoidImageT()
        {
            __Instance = Marshal.AllocHGlobal(sizeof(__Internal));
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
        }

        public RuminoidImageT(RuminoidImageT _0)
        {
            __Instance = Marshal.AllocHGlobal(sizeof(__Internal));
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
            *((__Internal*)__Instance) = *((__Internal*)_0.__Instance);
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
            RuminoidImageT __dummy;
            NativeToManagedMap.TryRemove(__Instance, out __dummy);
            if (__ownsNativeInstance)
                Marshal.FreeHGlobal(__Instance);
            __Instance = IntPtr.Zero;
        }

        public int Width
        {
            get
            {
                return ((__Internal*)__Instance)->width;
            }

            set
            {
                ((__Internal*)__Instance)->width = value;
            }
        }

        public int Height
        {
            get
            {
                return ((__Internal*)__Instance)->height;
            }

            set
            {
                ((__Internal*)__Instance)->height = value;
            }
        }

        public int Stride
        {
            get
            {
                return ((__Internal*)__Instance)->stride;
            }

            set
            {
                ((__Internal*)__Instance)->stride = value;
            }
        }

        public byte* Buffer
        {
            get
            {
                return (byte*)((__Internal*)__Instance)->buffer;
            }

            set
            {
                ((__Internal*)__Instance)->buffer = (IntPtr)value;
            }
        }

        public ulong CompressedSize
        {
            get
            {
                return ((__Internal*)__Instance)->compressed_size;
            }

            set
            {
                ((__Internal*)__Instance)->compressed_size = value;
            }
        }
    }

    internal class ruminoid_rendercore
    {
        public struct __Internal
        {
            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/ruminoid_rendercore.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "ruminoid_rc_new_context")]
            internal static extern IntPtr RuminoidRcNewContext();

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/ruminoid_rendercore.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "ruminoid_rc_destroy_context")]
            internal static extern void RuminoidRcDestroyContext(IntPtr context);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/ruminoid_rendercore.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "ruminoid_rc_render_frame")]
            internal static extern void RuminoidRcRenderFrame(IntPtr context, int width, int height, int timeMs);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/ruminoid_rendercore.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "ruminoid_rc_update_subtitle")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool RuminoidRcUpdateSubtitle(IntPtr context, [MarshalAs(UnmanagedType.LPUTF8Str)] string content, ulong length);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/ruminoid_rendercore.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "ruminoid_rc_get_result")]
            internal static extern IntPtr RuminoidRcGetResult(IntPtr context);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/ruminoid_rendercore.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "ruminoid_rc_attach_log_callback")]
            internal static extern void RuminoidRcAttachLogCallback(IntPtr context, IntPtr callback);
        }

        public static IntPtr RuminoidRcNewContext()
        {
            var __ret = __Internal.RuminoidRcNewContext();
            return __ret;
        }

        public static void RuminoidRcDestroyContext(IntPtr context)
        {
            __Internal.RuminoidRcDestroyContext(context);
        }

        public static void RuminoidRcRenderFrame(IntPtr context, int width, int height, int timeMs)
        {
            __Internal.RuminoidRcRenderFrame(context, width, height, timeMs);
        }

        public static bool RuminoidRcUpdateSubtitle(IntPtr context, string content, ulong length)
        {
            var __ret = __Internal.RuminoidRcUpdateSubtitle(context, content, length);
            return __ret;
        }

        public static RuminoidImageT RuminoidRcGetResult(IntPtr context)
        {
            var __ret = __Internal.RuminoidRcGetResult(context);
            RuminoidImageT __result0;
            if (__ret == IntPtr.Zero) __result0 = null;
            else if (RuminoidImageT.NativeToManagedMap.ContainsKey(__ret))
                __result0 = RuminoidImageT.NativeToManagedMap[__ret];
            else __result0 = RuminoidImageT.__CreateInstance(__ret);
            return __result0;
        }

        public static void RuminoidRcAttachLogCallback(IntPtr context, ruminoid_rc_log_callback callback)
        {
            var __arg1 = callback == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(callback);
            __Internal.RuminoidRcAttachLogCallback(context, __arg1);
        }
    }
}
