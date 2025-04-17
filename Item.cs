using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    //아이템 종류
    public enum ItemType
    {
        공격력,
        방어력,
        체력회복 = 5,
        기타 = 22
    }
    public class Item //아이템
    {

        public int itemStat { get; set; } = 5;
        public int sellPrice { get; set; } = 0;
        public string describe { get; set; } = "";
        public string itemName { get; set; } = "";
        public ItemType itemType { get; set; }  
 

        public Item(string name,ItemType Type, int stat, string itemDesc,int gold)  //아이템 이름, 종류,  설명, 스탯, 판매가
        {
            itemStat = stat;
            sellPrice = gold;
            itemName = name;
            describe = itemDesc;
            itemType = Type;
        }


       

    }
 
}
