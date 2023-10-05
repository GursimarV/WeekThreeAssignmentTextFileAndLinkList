using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekThreeAssignmentTextFileAndLinkList
{
    class Monster
    {
        public Monster(string type, int hp, int mp, int ap, int def)
        {
            //strings of the stats of the monsters
            Type = type;
            HP = hp;
            MP = mp;
            AP = ap;
            DEF = def;
        }

        public string Type { get; set; }
        public int HP { get; set; } 
        public int MP { get; set; }
        public int AP { get; set; }
        public int DEF { get; set; }
        public bool IsAlive { get { return HP > 0; } }

        public void Attack(Monster target)
        {
            //This code implements the calculation on damage and who damages who
            int damage = Math.Max(0, AP - target.DEF);
            target.HP -= damage;
            Console.WriteLine($"{Type} attacks {target.Type} for {damage} damage!");
        }
    }
}
