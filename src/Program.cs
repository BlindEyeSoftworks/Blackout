// (c) 2022 BlindEye Softworks. All rights reserved.

using System;
using System.Runtime.InteropServices;

namespace Blackout
{
    class Program
    {
        const int PowerMode = 214,
                  Off = 5,
                  ElementSize = 260; // Factoring in 32-bit pointers.

        delegate bool MonitorEnumProc(
            IntPtr monitor, IntPtr deviceContext, IntPtr clippingRect, IntPtr data);

        static void Main() =>
            EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, MonitorEnumCallback, IntPtr.Zero);

        static bool MonitorEnumCallback(
            IntPtr monitor, IntPtr deviceContext, IntPtr clippingRect, IntPtr data)
        {
            GetNumberOfPhysicalMonitorsFromHMONITOR(monitor, out uint count);
            IntPtr buffer = Marshal.AllocHGlobal(ElementSize * (int)count);
            GetPhysicalMonitorsFromHMONITOR(monitor, count, buffer);

            for (int i = 0; i < count; i++)
                SetVCPFeature(Marshal.ReadIntPtr(buffer + (i * ElementSize)), PowerMode, Off);

            DestroyPhysicalMonitors(count, buffer);
            Marshal.FreeHGlobal(buffer);

            return true;
        }

        [DllImport("user32.dll")]
        static extern bool EnumDisplayMonitors(
            IntPtr deviceContext, IntPtr clippingRect, MonitorEnumProc callback, IntPtr data);

        [DllImport("dxva2.dll")]
        static extern bool DestroyPhysicalMonitors(uint count, IntPtr buffer);

        [DllImport("dxva2.dll")]
        static extern bool GetNumberOfPhysicalMonitorsFromHMONITOR(IntPtr monitor, out uint count);

        [DllImport("dxva2.dll")]
        static extern bool GetPhysicalMonitorsFromHMONITOR(
            IntPtr monitor, uint count, IntPtr monitors);

        [DllImport("dxva2.dll")]
        public static extern bool SetVCPFeature(IntPtr monitor, byte code, uint value);
    }
}
