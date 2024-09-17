using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualSerial
{
    class InputHistory<T>
    {
        List<T> container = new List<T>();
        int cursor = 0;
        T buffer;
        public void Commit()
        {
            container.Add(buffer);
            cursor = container.Count();
            buffer = default;
        }
        public void UpdateBuffer(T s)
        {
            buffer = s;
        }
        public T GetCursor()
        {
            if (cursor == container.Count())
            {
                return buffer;
            }
            return container.ElementAtOrDefault(cursor);
        }
        public T Get()
        {
            return container.ElementAtOrDefault(cursor);
        }
        List<T> Data()
        {
            return container;
        }
        public void Back()
        {
            cursor = Math.Max(cursor-1, 0);
        }
        public void Forward()
        {
            cursor = Math.Min(cursor + 1, container.Count());
        }
    }
}
