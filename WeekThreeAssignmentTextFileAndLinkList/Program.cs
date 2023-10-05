/*
    Made by: Gursimar Virdi
    Date: September 27th, 2023
    I got help from this video https://www.youtube.com/watch?v=GcC5kW9tyOQ on how to create the double linked list
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WeekThreeAssignmentTextFileAndLinkList
{
    class Program 
    {
        static void Main(string[] args)
        {
            //This is the directory path to the Stats.txt file, I got help from https://learn.microsoft.com/en-us/dotnet/api/system.io.directory.getcurrentdirectory?view=net-7.0 for this part 
            var path = Directory.GetCurrentDirectory() + @"\Stats.txt";
            //This is list of the monster that
            DoubleLinkedList<Monster> monsters = LoadMonsters("Stats.txt");
            DoubleLinkedList<Monster> restofMonsters = new DoubleLinkedList<Monster>();
            //monsters.ToList();

            foreach (var monster in monsters.AsEnumerable())
            {
                restofMonsters.Add(monster);
            }

            //These are the stats of the player
            int playerHP = 100;
            int playerAP = 30;

            Random random = new Random();
            //Beginning text to enter the game
            Console.WriteLine("Welcome to the Slither Lockup Game");

            while (restofMonsters.Count > 0 && playerHP > 0) 
            {
                //This displays the monster being encountered in the room that the player is in, it randomize which monster you are fighting in the room
                //Monster currentMonster = restofMonsters[random.Next(restofMonsters.Count)];
                Monster currentMonster = restofMonsters.Find(restofMonsters.AsEnumerable().ElementAt(random.Next(restofMonsters.Count))).Value;
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
                    var nodeToRemove = restofMonsters.Find(currentMonster);
                    restofMonsters.Remove(nodeToRemove);
                    //restofMonsters.Remove(currentMonster);
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

            //This writes down the results of the game into the Results.txt file! I recieved help from this website to understand how to do it https://www.c-sharpcorner.com/article/csharp-streamwriter-example/
            using (StreamWriter writer = new StreamWriter("Results.txt"))
            {
                writer.WriteLine("Game Results:");
                writer.WriteLine("Player HP: " + playerHP);
                writer.WriteLine("Monsters defeated: " + (monsters.Count - restofMonsters.Count));
            }
            //The Double Linked List that Loads the monsters from the text file
            static DoubleLinkedList<Monster> LoadMonsters(string path)
            {
                DoubleLinkedList<Monster> monsters = new DoubleLinkedList<Monster>();

                try
                {
                    //This reads the all the text within the Stats.txt file
                    string[] lines = File.ReadAllLines(path);

                    foreach (string line in lines)
                    {
                        //I learned this from https://learn.microsoft.com/en-us/dotnet/api/system.string.split?view=net-7.0 which is about spliting the lists based off the part of the text file
                        string[] parts = line.Split(','); 

                        if (parts.Length == 5)
                        {
                            //Adds the monster and their stats, I learned about the parse from https://www.tutorialspoint.com/chash-int-tryparse-method
                            string type = parts[0];
                            if (int.TryParse(parts[1], out int hp) && int.TryParse(parts[2], out int mp) && int.TryParse(parts[3], out int ap) && int.TryParse(parts[4], out int def))
                            {
                                monsters.Add(new Monster(type, hp, mp, ap, def));
                            }
                        }
                        else
                        {
                            //This is just in case it doesn't read the monster's stats
                            Console.WriteLine($"Invalid line: {line}");
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    //This reads out the message that the file could not have been found
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine($"Error reading file: {ex.Message}");
                }

                return monsters;
            }
        }
    }
}