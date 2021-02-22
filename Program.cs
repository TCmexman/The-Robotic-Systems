
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using FinchAPI;


namespace The_Robotic_Systems
{

    class Program
    /************************************
   Title: The-Robotic-Systems
   Description: includes Talent Show, will include Data Recorder, Alarm System, User Programming
   Author: Chris Kieliszewski
   Date Created: 2/17/2021
   Last Modified: 2/21/2021
   ************************************/
    {
        public static void Main(string[] args)
        {
            WelcomeScreen();
            MainMenu();
            ClosingScreen();
        }
        private static void MainMenu()
        {
            Finch robot = new Finch();
            DisplayHeader("Main Menu");
            bool quitApplication = false;
            do
            {
                Console.WriteLine("Lets Get Started Pick A Letter");
                Console.WriteLine("A) Connect to Finch");
                Console.WriteLine("B) Talent Show");
                Console.WriteLine("C) Data Recorder");
                Console.WriteLine("D) Alarm System");
                Console.WriteLine("E) User Programming");
                Console.WriteLine("F) Disconnect from Finch");
                Console.WriteLine("G) Exit");
                string menuChoice = Console.ReadLine().ToUpper();
                switch (menuChoice)
                {
                    case "A":
                        ConnectToFinchRobot(robot);
                        break;
                    case "B":
                        DisplayTalentShow(robot);
                        break;

                    case "C":
                        DisplayDataRecorder(robot);
                        break;

                    case "D":
                        DisplayAlarmSystem(robot);
                        break;

                    case "E":
                        DisplayUserProgramming(robot);
                        break;

                    case "F":
                        DisplayDisConnectFinchRobot(robot);
                        break;

                    case "G":
                        DisplayDisConnectFinchRobot(robot);
                        quitApplication = true;
                        break;

                    default:
                        break;
                }
            } while (!quitApplication);
        }


        #region talentShowModule
        // light up and high pitch sound for 2 sec. //
        private static void LightAndSound(Finch robot)
        {
            Console.WriteLine("Working...");
            robot.setLED(200, 130, 5);
            robot.noteOn(7902);
            robot.wait(2000);
            robot.setLED(0, 0, 0);
            robot.noteOff();
            ContinuePrompt();
        }
        //quick dance back and Forth
        private static void Dance(Finch robot)
        {
            Console.WriteLine("I Can Dance");
            robot.setMotors(150, -150);
            robot.wait(500);
            robot.setMotors(-150, 150);
            robot.wait(500);
            robot.setMotors(150, -150);
            robot.wait(500);
            robot.setMotors(-150, 150);
            robot.wait(500);
            robot.setMotors(0, 0);
            ContinuePrompt();
        }

        // A bit of song and dance and lights. hot cross buns //
        private static void MixingItUp(Finch robot)
        {
            Console.WriteLine("Here I Go!");
            robot.setLED(235, 64, 52);
            robot.noteOn(494);
            robot.wait(500);
            robot.noteOn(440);
            robot.wait(500);
            robot.setMotors(95, -50);
            robot.noteOn(392);
            robot.wait(500);
            robot.noteOn(494);
            robot.wait(500);
            robot.noteOn(440);
            robot.setMotors(-95, 50);
            robot.wait(500);
            robot.noteOn(392);
            robot.wait(1000);
            robot.noteOn(392);
            robot.setLED(252, 186, 3);
            robot.wait(500);
            robot.noteOn(392);
            robot.setMotors(95, -50);
            robot.wait(500);
            robot.noteOn(392);
            robot.wait(500);
            robot.noteOn(392);
            robot.wait(500);
            robot.noteOn(440);
            robot.wait(500);
            robot.noteOn(440);
            robot.wait(500);
            robot.noteOn(440);
            robot.setMotors(-95, 50);
            robot.setLED(3, 252, 73);
            robot.wait(500);
            robot.noteOn(494);
            robot.wait(500);
            robot.noteOn(440);
            robot.wait(500);
            robot.noteOn(392);
            robot.wait(2000);
            robot.noteOff();
            robot.setMotors(0, 0);
            ContinuePrompt();


        }

        #endregion
        #region dataRecorderModule

        #endregion
        #region alarmSystemModule

        #endregion
        #region userProgrammingModule

        #endregion
        #region moduleMenus
        private static void DisplayTalentShow(Finch robot)
        {
            robot.connect();
            bool leave = false;
            DisplayHeader("Talent Show");
            do
            {
                Console.Clear();
                Console.WriteLine("Pick A Letter");
                Console.WriteLine("A) Light And Sound");
                Console.WriteLine("B) Dance");
                Console.WriteLine("C) Mixing It Up");
                Console.WriteLine("D) Return to Main Menu");
                string menuChoice = Console.ReadLine().ToUpper();
                switch (menuChoice)
                {
                    case "A":
                        LightAndSound(robot);
                        break;
                    case "B":
                        Dance(robot);
                        break;
                    case "C":
                        MixingItUp(robot);
                        break;
                    case "D":
                        MainMenu();
                        break;
                    default:

                        break;
                }
            } while (!leave);
        }



        private static void DisplayDataRecorder(Finch robot)
        {
            DisplayHeader("Data Recorder");
            Console.WriteLine("Under Construction");
            ContinuePrompt();
        }
        private static void DisplayAlarmSystem(Finch robot)
        {
            DisplayHeader("Alarm System");
            Console.WriteLine("Under Construction");
            ContinuePrompt();
        }
        private static void DisplayUserProgramming(Finch robot)
        {
            DisplayHeader("User Programing System");
            Console.WriteLine("Under Construction");
            ContinuePrompt();
        }
        #endregion
        #region tools
        public static void ContinuePrompt()
        {
            Console.WriteLine("Press Any Key To Continue.");
            Console.ReadKey();
        }
        public static void DisplayHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine(headerText);
        }
        public static void WelcomeScreen()
        {
            Console.Clear();
            Console.WriteLine("Welcome To My Robot");
            Console.WriteLine("Explores What The Finch Robot Can Do");
            ContinuePrompt();
        }
        public static void ClosingScreen()
        {
            Console.Clear();
            Console.WriteLine("Thank You For Stopping By");
            ContinuePrompt();
        }
        private static void ConnectToFinchRobot(Finch robot)
        {
            Console.CursorVisible = false;
            DisplayHeader("Connect to Finch");
            ContinuePrompt();
            robot.connect();
            bool level = robot.isFinchLevel();
            if (level == true)
            {
                Console.Clear();
                Console.WriteLine("Finch is connected");

            }
            if (level == false)
            {
                Console.Clear();
                Console.WriteLine("Please plug in your Finch or put it on its tummy and try again.");

            }
            ContinuePrompt();
            MainMenu();

        }
        private static void DisplayDisConnectFinchRobot(Finch robot)
        {
            DisplayHeader("Disconnect from Finch");
            ContinuePrompt();
            robot.disConnect();
            DisplayHeader("Finch has been disconnected");
            ContinuePrompt();
        }
        #endregion
    }
}
