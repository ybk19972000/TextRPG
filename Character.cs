using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{

    public class Character
    {
        public CharStat stat { get; private set; } // Character.CharStat으로 쓰기위함

        private Inventory<Item> _userInventory;

        public Job characterJob; // 이넘 선언
        public string name;
        

        public Character(string name) //생성자
        {
            stat = new CharStat(); //생성시 스탯도 생성
            this.name = name;    
        }
        
        public void JobStat(Job job) // 선택한 직업마다 스탯이 다름
        {
            switch(job)
            {
                case Job.Warrior:
                    stat.attackStat = 10;
                    stat.defendStat = 20;
                    stat.maxHealth = 250;
                    break;
                case Job.Wizzard:
                    stat.attackStat = 30;
                    stat.defendStat = 5;
                    stat.maxHealth = 100;
                    break;
                case Job.Archer:
                    stat.attackStat = 20;
                    stat.defendStat = 10;
                    stat.maxHealth = 150;
                    break;
            }
        }

        public void CharacterInfo() //캐릭터 상태 "상태보기"에 출력
        {
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine($"Lv.{stat.level} \n{name} ({characterJob}) \n공격력: {stat.attackStat} \n방어력: {stat.defendStat} \n체력:{stat.maxHealth} \nGold: {stat.gold} G");
        }

        public void PrintItems()
        {
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다. ");
            Console.WriteLine("\n[아이템 목록]");


        }
    }
    public enum Job //캐릭터 역할군
    {
        Warrior = 1,
        Wizzard,
        Archer
    }

}
