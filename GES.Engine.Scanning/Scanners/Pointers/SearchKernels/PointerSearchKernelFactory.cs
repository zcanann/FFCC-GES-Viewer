﻿namespace GES.Engine.Scanning.Scanners.Pointers.SearchKernels
{
    using GES.Engine.Scanning.Scanners.Pointers.Structures;
    using GES.Engine.Scanning.Snapshots;
    using System;

    internal class PointerSearchKernelFactory
    {
        public static IVectorPointerSearchKernel GetSearchKernel(Snapshot boundsSnapshot, UInt32 maxOffset, PointerSize pointerSize)
        {
            if (boundsSnapshot.ByteCount < 64)
            {
                // Linear is fast for small region sizes
                return new LinearPointerSearchKernel(boundsSnapshot, maxOffset, pointerSize);
            }
            else
            {
                return new SpanPointerSearchKernel(boundsSnapshot, maxOffset, pointerSize);
            }
        }
    }
    //// End class
}
//// End namespace