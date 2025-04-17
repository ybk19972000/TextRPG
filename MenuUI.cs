using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class MenuUI
    {
        enum MenuList
        {
            상태보기 = 1 ,
            인벤토리 ,
            상점
        }


        private string[] _menuOptions = {"나가기","상태 보기","인벤토리","상점"};
        private string[] _itemOptions = {"장착[해제]하기", "구매하기","판매하기" };
        
        public int ShowVillage() //마을 화면
        {

            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

            for (int i = 1; i < _menuOptions.Length;i++ )
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(i + ". ");
                Console.ResetColor();
                Console.WriteLine(_menuOptions[i]+"\n");
            }

            PrintMenuInput();

            int input = int.Parse(Console.ReadLine());
            return input;
        }
   

            
        public void MenuControl(int OpInput,Character character, Shop shop) //메뉴마다 선택지 컨트롤
        {
            MenuList menuList = (MenuList)OpInput;

            switch(menuList)
            {
                case MenuList.상태보기:
                    PrintMenuTitle(_menuOptions[OpInput]);
                    character.CharacterInfo();
                    Console.WriteLine($"\n0.{_menuOptions[0]}");
                    RetunrVillage();

                    break;

                case MenuList.인벤토리:
                    PrintMenuTitle(_menuOptions[OpInput]);
                    character.PrintItems();
                    Console.WriteLine($"\n1.{_itemOptions[0]}\n0.{_menuOptions[0]}");
                    RetunrVillage();

                    break;

                case MenuList.상점:
                    PrintMenuTitle(_menuOptions[OpInput]);
                    shop.PrintShop(character);
                    Console.WriteLine($"\n2.{_itemOptions[2]}\n1.{_itemOptions[1]}\n0.{_menuOptions[0]}");
                    RetunrVillage();
                    break;

                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }


        } 

        private void PrintMenuInput()
        {
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(">>");
            Console.ResetColor();
        }

        private void PrintMenuTitle(string title)
        {
            Thread.Sleep(1000);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(title);
            Console.ResetColor();
        }

        private void RetunrVillage()
        {
            bool isInt = false; 
            int input = 0;
            PrintMenuInput();

            while (true)
            {
                isInt = int.TryParse(Console.ReadLine(), out input);
                if(isInt && input ==0)
                {
                    break;
                }
            }
        }
    }
}
