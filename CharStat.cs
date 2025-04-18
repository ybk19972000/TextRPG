using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
     public class CharStat
    {
        // 캐릭터 스탯 따로 분할
        public int level { get; set; } = 1;
        public int attackStat { get; set; } = 10;
        public int defendStat { get; set; } = 5;
        public int maxHealth { get; set; } = 100;
        public int gold { get; set; } = 5000;
    }
}
