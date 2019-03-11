using System.Collections.Generic;
using System.Linq;

namespace WebApp
{
    public class DataItemService
    {
        private class DataItemEntry
        {
            public Size Size { get; set; }
            public object Value { get; set; }
        }

        private readonly MemoryAllocator _allocator;
        private readonly IDictionary<int, DataItemEntry> _dataItems = new Dictionary<int, DataItemEntry>();

        public DataItemService(MemoryAllocator allocator)
        {
            _allocator = allocator;
        }

        public DataItem Create(Size size)
        {
            var memory = _allocator.Allocate(size);
            var id = GetNextId();
            var entry = new DataItemEntry { Size = size, Value = memory };
            _dataItems.Add(id, entry);
            return new DataItem
            {
                Id = id,
                Size = size
            };
        }

        private int GetNextId()
        {
            if (_dataItems.Count == 0)
            {
                return 1;
            }

            return _dataItems.Keys.Max() + 1;
        }

        public void Delete(int id)
        {
            _dataItems.Remove(id);
        }

        public IReadOnlyList<DataItem> GetAll()
        {
            return _dataItems
                .Select(e => new DataItem(e.Key, e.Value.Size))
                .OrderBy(i => i.Id)
                .ToList();
        }
    }
}