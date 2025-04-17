using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class Shop
    {
        public Inventory<Item> _shopInventory;

        public Shop()
        {
            _shopInventory = new Inventory<Item>();

            _shopInventory.AddItem(new Item("더블배럴샷건", ItemType.공격력, 55, "어디선가 헤비메탈이 들립니다.", 3000));
            _shopInventory.AddItem(new Item("이진검", ItemType.공격력, 10, "일반 검처럼 생겼으나 이진법의 패턴이 그려진 칼입니다.", 500));
            _shopInventory.AddItem(new Item("스페이스마린갑주", ItemType.방어력, 40, "For The Emperor!!!!", 2500));
            _shopInventory.AddItem(new Item("전신금갑옷", ItemType.방어력, 5, "겉만 화려한 갑옷입니다.", 700));
            _shopInventory.AddItem(new Item("불사의 물약", ItemType.체력회복, 1, "불사의 물약이 이렇게 싸도 되나?", 100));
        }
    }
}
