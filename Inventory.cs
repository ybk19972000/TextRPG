using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class Inventory<T> //제너릭 
    {
        public List<T> _items = new List<T>();

        public List<T> GetItems() //아이템 목록 반환
        {
            return _items;
        }
        public void AddItem(T item)  //아이템 목록에 추가
        { 
            _items.Add(item); 
        }

        public void RemoveItem(T item)//아이템 목록에서 제거
        {
            _items.Remove(item);
        }


    }

}
