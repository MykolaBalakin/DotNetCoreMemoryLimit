using System;
using System.Collections.Generic;

namespace WebApp
{
    public class MemoryAllocator
    {
        private const int MaxAllocation = 1024;
        private Random _random = new Random();

        public object Allocate(Size size)
        {
            var chunkCount = 1 + size.Bytes / MaxAllocation;
            var memory = new List<object>(chunkCount);

            var toAllocate = size.Bytes;
            while (toAllocate > 0)
            {
                var chunkSize = Math.Min(toAllocate, MaxAllocation);
                var chunk = Allocate(chunkSize);
                memory.Add(chunk);
                toAllocate -= chunkSize;
            }

            return memory;
        }

        private object Allocate(int bytes)
        {
            var memory = new byte[bytes];
            _random.NextBytes(memory);
            return memory;
        }
    }
}