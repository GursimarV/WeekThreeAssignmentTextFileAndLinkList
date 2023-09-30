/*
    Made by: Gursimar Virdi
    Date: September 27th, 2023
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WeekThreeAssignmentTextFileAndLinkList
{
    class Program 
    {
        static List<Monster> LoadMonsters(string Stats)
        {
            //Lists out the monsters that is being read from the file
            List<Monster> monsters = new List<Monster>();
            try
            {
                using (StreamReader reader = new StreamReader(Stats))
                {
                    //This reads the file with the stats of the monster and adds it to the list
                    reader.ReadLine();
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        //I did get help from https://www.dotnetperls.com/parse about the Parse to add the numbers of the stats of the monster to the data in order for the monster with their stats to be added to the list.
                        string[] data = line.Split(' ');
                        string type = data[0];
                        int hp = int.Parse(data[1]);
                        int mp = int.Parse(data[2]);
                        int ap = int.Parse(data[3]);
                        int def = int.Parse(data[4]);
                        monsters.Add(new Monster(type, hp, mp, ap, def));
                    }
                }
            } 
            catch (Exception ex)
            {
                //This reads out the message that the file could not have been found
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ex.Message);
            }
            return monsters;
        }

        static void Main(string[] args)
        {
            //This is the directory path to the Stats.txt file, I got help from https://learn.microsoft.com/en-us/dotnet/api/system.io.directory.getcurrentdirectory?view=net-7.0 for this part 
            var path = Directory.GetCurrentDirectory() + @"\Stats.txt";
            //This is list of the monster that
            List<Monster> monsters = LoadMonsters("Stats.txt");
            List<Monster> restofMonsters = monsters.ToList();
            
            //These are the stats of the player
            int playerHP = 100;
            int playerAP = 30;

            Random random = new Random();
            //Beginning text to enter the game
            Console.WriteLine("Welcome to the Slither Lockup Game");

            while (restofMonsters.Count > 0 && playerHP > 0) 
            {
                //This displays the monster being encountered in the room that the player is in, it randomize which monster you are fighting in the room
                Monster currentMonster = restofMonsters[random.Next(restofMonsters.Count)];
                Console.WriteLine($"You enter a room and encounter a {currentMonster.Type}!");

                while (currentMonster.IsAlive && playerHP > 0)
                {
                    //This displays the amount of HP the player and monster have, as well as display which monster is it
                    Console.WriteLine($"Your HP: {playerHP}, {currentMonster.Type}'s HP: {currentMonster.HP}");
                    // The monster attack the player
                    currentMonster.Attack(currentMonster); 

                    if (currentMonster.IsAlive)
                    {
                        //This is the shows how the player takes damage then will subtract the amount of damage from the health of player
                        int damage = Math.Max(2, currentMonster.AP - playerHP);
                        playerHP -= damage;
                        Console.WriteLine($"{currentMonster.Type} attacks you and has dealt {damage} damage!");
                    }
                }

                if (!currentMonster.IsAlive) 
                {
                    //This is the text if the player defeats a monster in the dungeon
                    Console.WriteLine($"You have defeated {currentMonster.Type}!");
                    restofMonsters.Remove(currentMonster);
                }
                else
                {
                    //This is the text if the player has been killed by one of the three monsters
                    Console.WriteLine($"You have died to {currentMonster.Type }!");
                    break;
                }
            }

            if (playerHP <= 0)
            {
                //This is the end text if the player has died from a monster
                Console.WriteLine("Game Over, You Died!!!");
            }
            else
            {
                //This is the end text if the player killed all the monsters
                Console.WriteLine("Congrats! You killed all the monster in the dungeon!");
            }
        }
    }
}