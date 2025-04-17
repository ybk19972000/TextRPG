using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    class GameManage
    {
        private MenuUI _menu = new MenuUI(); //메뉴UI 참조 변수에 저장
        private Character _character; 
        private Inventory<Item> inventory = new Inventory<Item>() ; //인벤토리 아이템리스트에 저장
        private Shop _shop = new Shop();

        public Job characterJob;

        private bool _isGameOver = false;
        private int menuSelect = 0;

        //게임시작
        public void StartGame() //게임시작
        {
            _shop.ShopItems();
            Init();

            //게임종료
            while(_isGameOver == false) //게임오버거 참이면 종료
            {
                InputHandler();
            }
        }   
        private void Init()
        {
            if(_isGameOver)
            {
                return;
            }

            Console.Clear();
            string? characterName = null;

            //게임 시작 시 캐릭터 이름설정
            while (string.IsNullOrEmpty(characterName)) 
            {
                Console.Clear();
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n원하시는 이름을 설정해주세요.");
                characterName = Console.ReadLine();

                if (string.IsNullOrEmpty(characterName))
                {
                    Console.WriteLine("잘못된 이름입니다. 다시 설정해주세요.");
                    Thread.Sleep(1000);
                }
                else
                {
                    _character = new Character(characterName);
                    Console.WriteLine($"\n{_character.name} 입장");
                }

            }

            Console.WriteLine("직업을 선택하세요. [1.전사 | 2.법사 | 3.궁수]");
            int jobChoice = int.Parse(Console.ReadLine());

            _character.characterJob = (Job)jobChoice;
            if (jobChoice >= 1 && jobChoice <= 3)
            {
                
                switch (_character.characterJob)
                {
                    case Job.Warrior:
                        Console.WriteLine($"전사를 선택했습니다.");
                        _character.JobStat(_character.characterJob);
                        break;
                    case Job.Wizzard:
                        Console.WriteLine($"마법사를 선택했습니다.");
                        _character.JobStat(_character.characterJob);
                        break;
                    case Job.Archer:
                        Console.WriteLine($"궁수를 선택했습니다.");
                        _character.JobStat(_character.characterJob);
                        break;
                    default:
                        Console.WriteLine();
                        break;
                }
            }

            while(!_isGameOver) 
            {
                menuSelect = _menu.ShowVillage(); //마을 화면
                _menu.MenuControl(menuSelect, _character, _shop); //입력에 따라 메뉴창으로 이동
            }

        }

        //게임 종료기능
        private void InputHandler() //esc 키가 눌릴시
        {
            var input = Console.ReadKey();
            if(input.Key == ConsoleKey.Escape)
            {
                _isGameOver = true;
            }
        }
                

    }
}
