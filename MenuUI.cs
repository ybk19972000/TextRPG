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
            상태보기 = 1,
            인벤토리,
            상점
        }


        private string[] _menuOptions = { "나가기", "상태 보기", "인벤토리", "상점" };
        private string[] _itemOptions = { "나가기", "구매하기", "판매하기" };

        public int ShowVillage() //마을 화면
        {

            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

            for (int i = 1; i < _menuOptions.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(i + ". ");
                Console.ResetColor();
                Console.WriteLine(_menuOptions[i] + "\n");
            }

            PrintMenuInput();

            int input = int.Parse(Console.ReadLine());
            return input;
        }



        public void MenuControl(int OpInput, Character character, Shop shop) //메뉴마다 선택지 컨트롤
        {
            MenuList menuList = (MenuList)OpInput;

            switch (menuList)
            {
                case MenuList.상태보기:
                    PrintMenuTitle(_menuOptions[OpInput]);
                    character.CharacterInfo();
                    ReturnVillage(shop, character);

                    break;

                case MenuList.인벤토리:
                    PrintMenuTitle(_menuOptions[OpInput]);
                    EquipInventory(character);
                    PrintInven(character);

                    break;

                case MenuList.상점:
                    PrintMenuTitle(_menuOptions[OpInput]);
                    PrintShop(character, shop);
                    Console.WriteLine($"\n2.{_itemOptions[2]}\n1.{_itemOptions[1]}");
                    ReturnVillage(shop, character);
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

        private void ReturnVillage(Shop shop, Character character)
        {
            bool isInt = false;
            int input = 0;
            Console.WriteLine($"0.{_menuOptions[0]}");
            PrintMenuInput();

            while (true)
            {
                isInt = int.TryParse(Console.ReadLine(), out input);
                if (isInt && input == 0)//0이 눌리면 메인화면으로 다시 감,인풋이 1일때 구매 로직 추가 (메소드추가) 현재 문제 아이템 구매시 2번이 눌리면 인벤토리창으로감
                {
                    Console.WriteLine("나가기");
                    break;
                }
                else if (isInt && input == 1)
                {
                    BuyItem(shop, character);
                    break;
                }
                else if (isInt && input == 2)
                {
                    SellItem(shop, character);
                    break;
                }
                else 
                {
                    Console.WriteLine("다시 입력해주세요.");
                }
            }
        }

        private void BuyItem(Shop shop, Character character)
        {
            bool isExit = false;
            bool isInt = false;
            int input = -1;
           
            while (!isExit)
            {
                var item = shop._shopInventory.GetItems(); 

                Console.Clear();
                Console.WriteLine("구매창입니다.\n[구매를 원하시면 아이템 번호를 입력해주세요]\n");
                Console.WriteLine($"\n[보유 골드]\n{character.stat.gold}G\n");
                PrintShopItem(shop, character);
                Console.WriteLine($"0.{_menuOptions[0]}");
                PrintMenuInput();

                isInt = int.TryParse(Console.ReadLine(), out input);
                if (isInt && input >= 1 && input <= item.Count)
                {
                    var selectedItem = item[input-1];

                    if (character.stat.gold >= selectedItem.sellPrice)
                    {
                        character.stat.gold -= selectedItem.sellPrice;
                        character._userInventory.AddItem(selectedItem);

                        Console.WriteLine(selectedItem.itemName + "를 구매하셨습니다.");
                        Console.WriteLine($"{character.stat.gold}G가 있습니다.");
                        
                    }
                    else 
                    {
                        Console.WriteLine($"골드가 부족합니다.");
                    }
                    Thread.Sleep(1000);
                }
                else if (isInt && input == 0)
                {
                    Console.WriteLine("나가기");
                    isExit = true;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }   

        }


        private void SellItem(Shop shop, Character character) //내일 여기서부터 시작
        {
            bool isExit = false;
            bool isInt = false;
            int input = -1;

            while (!isExit)
            {
                var item = character._userInventory.GetItems(); 

                Console.Clear();
                Console.WriteLine("판매창입니다.\n[판매를 원하시면 아이템 번호를 입력해주세요]\n");
                Console.WriteLine($"\n[보유 골드]\n{character.stat.gold}G\n");

                PrintCharacterInven(character);
                Console.WriteLine($"0.{_menuOptions[0]}");
                PrintMenuInput();

                isInt = int.TryParse(Console.ReadLine(), out input);
                if (isInt && input >= 1 && input <= item.Count )
                {
                    var selectedItem = item[input - 1];

                    character.stat.gold += (int)(selectedItem.sellPrice * 0.85f);
                    character._userInventory.RemoveItem(selectedItem);

                    Console.WriteLine(selectedItem.itemName + "를 판매하셨습니다.");
                    Console.WriteLine($"골드가 {character.stat.gold}G 남았습니다.");

                    Thread.Sleep(1000);
                }
                else if (isInt && input == 0)
                {
                    isExit = true;
                }
            }
        }

        private void EquipInventory(Character character)
        {

            bool isExit = false;
            bool isInt = false;
            int input = -1;

            while (!isExit)
            {
                var item = character._userInventory.GetItems();

                PrintInven(character);
                Console.WriteLine($"0.{_menuOptions[0]}");
                PrintMenuInput();

                isInt = int.TryParse(Console.ReadLine(), out input);
                if (isInt && input >= 1 && input <= item.Count)
                {
                    var selectedItem = item[input - 1];

                    switch (selectedItem.itemType)
                    {
                        case ItemType.체력회복:
                            character.stat.maxHealth += selectedItem.itemStat;
                            character._userInventory.RemoveItem(selectedItem);
                            break;
                        case ItemType.방어력:
                            if (character.equippedArmor == selectedItem)
                            {
                                character.stat.defendStat -= selectedItem.itemStat;
                                character.equippedArmor = null; //장비 해제
                            }
                            else 
                            {
                                if (character.equippedArmor != null)
                                {
                                    character.stat.defendStat -= character.equippedArmor.itemStat; //장착 했던 장비 빼고 
                                }
                                character.equippedArmor = selectedItem; //장비 장착
                                character.stat.defendStat += selectedItem.itemStat;
                            }
                            break;
                        case ItemType.공격력:
                            if (character.equippedWeapon == selectedItem)
                            {
                                character.stat.attackStat -= selectedItem.itemStat;
                                character.equippedWeapon = null; 
                            }
                            else
                            {
                                if(character.equippedWeapon != null)
                                {
                                    character.stat.attackStat -= character.equippedWeapon.itemStat;
                                }

                                character.equippedWeapon = selectedItem; 
                                character.stat.attackStat += selectedItem.itemStat;
                            }
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                    }

                    Thread.Sleep(1000);
                }
                else if (isInt && input == 0)
                {
                    isExit = true;
                }
            }
        }

        private void PrintShop(Character character, Shop shop)
        {
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine($"\n[보유 골드]\n{character.stat.gold}G\n");
            PrintShopItem(shop,character);

        }

        private void PrintShopItem(Shop shop,Character character)
        {
            var items = shop._shopInventory.GetItems();

            //foreach (var item in _shopInventory.GetItems())//아이템 넘버 주기| 대신 for문으로 i를 아이템번호 
            for (int i = 1; i < shop._shopInventory.GetItems().Count + 1; i++)
            {
                var item = items[i-1];
                bool isSelled = character._userInventory.GetItems().Contains(item); //해당 캐릭터가 아이템을 보유하고 있는지 확인

                if (isSelled)
                {
                    Console.WriteLine($"{i}. [{item.itemName}]|{item.itemType}:+{item.itemStat}|'{item.describe}'|[구매완료]|\n ");
                }
                else
                {
                    Console.WriteLine($"{i}. [{item.itemName}]|{item.itemType}:+{item.itemStat}|'{item.describe}'|{item.sellPrice}G|\n ");
                }
            }

        }

        private void PrintCharacterInven(Character character)
        {
            string equipTag;
            var items = character._userInventory.GetItems();

            for (int i = 1; i < character._userInventory.GetItems().Count + 1; i++)
            {
                var item = items[i-1];
                bool isEquipped = ((item == character.equippedWeapon) || (item == character.equippedArmor));

                if (isEquipped)
                {
                    equipTag = "[E]";
                }
                else
                {
                    equipTag = "";
                }

                Console.WriteLine($"{i}. {equipTag} [{item.itemName}]|{item.itemType}:+{item.itemStat}|'{item.describe}'|\n ");

            }
        }

        private void PrintInven(Character character)
        {
            Console.Clear();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다. ");
            Console.WriteLine("\n[아이템 목록]");
            PrintCharacterInven(character);
        }

    }
}

