
using FinchAPI;
using System;
using System.Collections.Generic;
using System.Linq;


namespace The_Robotic_Systems
{

    public enum Command
    {
        NONE,
        MOVEFORWARD,
        MOVEBACKWARD,
        STOPMOTORS,
        WAIT,
        TURNRIGHT,
        TURNLEFT,
        LEDON,
        LEDOFF,
        GETTEMPERATURE,
        DONE
    }
    class Program
    /************************************
   Title: The-Robotic-Systems
   Description: includes Talent Show, will include Data Recorder, Alarm System, User Programming
   Author: Chris Kieliszewski
   Date Created: 2/17/2021
   Last Modified: 3/07/2021
   ************************************/
    {
        public static void Main(string[] args)
        {
            WelcomeScreen();
            MainMenu();
            ClosingScreen();

        }
        public static void MainMenu()
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
        public static void LightAndSound(Finch robot)
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
        public static void Dance(Finch robot)
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
        public static void MixingItUp(Finch robot)
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

            //data recorder




        }

        #endregion
        #region dataRecorderModule
        public static void DataRecorderDisplayGetData(double[] temperatures)
        {
            DataRecorderDisplayTable(temperatures);

            ContinuePrompt();

        }

        public static int DataRecorderDisplayGetNumberOfDataPoints()
        {
            Console.CursorVisible = true;
            DisplayHeader("\nOption A Chosen" + "\nGet number of data points");

            Console.Write("\nHow many data points would you like? >> ");
            string userResponse = Console.ReadLine();

            int.TryParse(userResponse, out int numberofDataPoints);

            ContinuePrompt();
            return numberofDataPoints;
        }

        public static double DataRecorderDisplayGetDataPointFrequency()
        {
            Console.CursorVisible = true;
            DisplayHeader("\nOption B Chosen" + "\nGet frequency of Data Points");

            Console.Write("\nFrequency of Data Points: ");

            double.TryParse(Console.ReadLine(), out double dataPointFrequency);

            ContinuePrompt();
            return dataPointFrequency;
        }

        public static double[] DataRecorderDisplayGetData(int numberOfDataPoints, double dataPointFrequency, Finch robot)
        {
            Console.CursorVisible = false;
            double[] temperatures = new double[numberOfDataPoints];
            Console.WriteLine("\nOption C Chosen");
            DisplayHeader("\nOption C Chosen" + "\nGet Data");

            Console.WriteLine($"Number of data points: {numberOfDataPoints}");
            Console.WriteLine($"Data point frequency: {dataPointFrequency}");
            Console.WriteLine();
            Console.WriteLine("The Finch robot is ready to begin recording the temperature data.");
            ContinuePrompt();

            for (int i = 0; i < numberOfDataPoints; i++)
            {
                temperatures[i] = robot.getTemperature();
                Console.WriteLine($"Reading {i + 1}: {temperatures[i]:n2} ");
                int waitInSeconds = (int)((dataPointFrequency) * 1000);
                robot.wait(waitInSeconds);
            }

            ContinuePrompt();

            return temperatures;
        }

        public static void DataRecorderDisplayTable(double[] temperatures)
        {
            Console.CursorVisible = false;
            DisplayHeader("\nOption D Chosen" + "\nShow Data");

            Console.WriteLine(
                "Recording #".PadLeft(19) +
                "Temp".PadLeft(19)
                );
            Console.WriteLine(
                "-----------".PadLeft(19) +
                "-----------".PadLeft(19)
                );

            for (int i = 0; i < temperatures.Length; i++)
            {
                Console.WriteLine(
               (i + 1).ToString().PadLeft(19) +
               temperatures[i].ToString("n2").PadLeft(19)
               );
            }
        }

        #endregion
        #region alarmSystemModule
        public static string LightAlarmDisplaySetSensorstoMonitor()
        {
            List<string> correctSensors = new List<string>() { "left", "right", "both" };
            string sensorsToMonitor;
            DisplayHeader("\nSensors To Monitor");

            Console.Write("Sensors to Monitor? (Left, Right, Both): ");
            sensorsToMonitor = Console.ReadLine().ToLower();

            if (correctSensors.Contains(sensorsToMonitor))
            {
                return sensorsToMonitor;
            }
            else
            {
                DisplayHeader("\nPlease input \"left\", \"right\", or \"both\"");
                ContinuePrompt();
                return LightAlarmDisplaySetSensorstoMonitor();
            }
        }

        public static string LightAlarmDisplaySetRangeType()
        {
            string[] correctRange = new string[] { "minimum", "maximum" };
            string rangeType;
            DisplayHeader("\nSet Range Type");

            Console.Write("Range Type? (Minimum, Maximum): ");
            rangeType = Console.ReadLine().ToLower();

            if (correctRange.Contains(rangeType))
            {
                return rangeType;
            }
            else
            {
                DisplayHeader("\nPlease input \"maximum\" or \"minimum\"");
                ContinuePrompt();
                return LightAlarmDisplaySetRangeType();
            }
        }

        public static int LightAlarmDisplaySetMinMaxThresholdValue(Finch robot, string rangeType)
        {
            string minMaxThresholdValue;
            int nMinMaxThresholdValue;

            DisplayHeader("\nMinimum/Maximum Threshold Value");

            Console.WriteLine($"Left light sensor ambient value: {robot.getLeftLightSensor()}");
            Console.WriteLine($"Right light sensor ambient value: {robot.getRightLightSensor()}");
            Console.WriteLine();
            Console.Write($"Enter The {rangeType} light value: ");

            minMaxThresholdValue = Console.ReadLine();

            char firstChar = minMaxThresholdValue[0];
            bool isNumber = Char.IsDigit(firstChar);

            if (!isNumber)
            {
                DisplayHeader("\nPlease input an integer");
                ContinuePrompt();
                return LightAlarmDisplaySetTimeToMonitor();
            }
            else
            {
                nMinMaxThresholdValue = int.Parse(minMaxThresholdValue);
                return nMinMaxThresholdValue;
            }
        }

        public static int LightAlarmDisplaySetTimeToMonitor()
        {
            string timeToMonitor;
            int nTimeToMonitor;
            DisplayHeader("\nSet Time To Monitor");

            Console.Write("Desired monitor time? (in seconds): ");
            timeToMonitor = Console.ReadLine();

            char firstChar = timeToMonitor[0];
            bool isNumber = Char.IsDigit(firstChar);

            if (!isNumber)
            {
                DisplayHeader("\nPlease input an integer");
                ContinuePrompt();
                return LightAlarmDisplaySetTimeToMonitor();
            }
            else
            {
                nTimeToMonitor = int.Parse(timeToMonitor);
                return nTimeToMonitor;
            }
        }

        public static void LightAlarmDisplaySetAlarm(Finch robot,
                                                    int timeToMonitor,
                                                    string rangeType,
                                                    int minMaxThresholdValue,
                                                    string sensorsToMonitor)
        {
            int secondsElapsed = 0;
            bool thresholdExceeded = false;
            int currentLightSensorValue = 0;
            DisplayHeader("\nSet Light Alarm");
            Console.WriteLine($"Sensors to monitor: {sensorsToMonitor}");
            Console.WriteLine("Range Type: {0}", rangeType);
            Console.WriteLine($"Min/max Threshold Value: {minMaxThresholdValue}");
            Console.WriteLine($"Time to monitor: {timeToMonitor}");
            Console.WriteLine();

            Console.WriteLine("Press any key to begin monitoring.");
            Console.ReadKey();

            while (secondsElapsed < timeToMonitor && !thresholdExceeded)
            {
                switch (sensorsToMonitor)
                {
                    case "left":
                        currentLightSensorValue = robot.getLeftLightSensor();
                        break;

                    case "right":

                        currentLightSensorValue = robot.getRightLightSensor();
                        break;

                    case "both":
                        currentLightSensorValue = (robot.getRightLightSensor() + robot.getLeftLightSensor()) / 2;
                        break;
                }
                switch (rangeType)
                {
                    case "minimum":
                        if (currentLightSensorValue < minMaxThresholdValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;

                    case "maximum":
                        if (currentLightSensorValue > minMaxThresholdValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;
                }
                robot.wait(1000);
                secondsElapsed++;
                Console.WriteLine("Current Light Value: {0} ", currentLightSensorValue);
            }

            if (thresholdExceeded)
            {
                Console.WriteLine($"The {rangeType} threshold value was exceeded!");
                robot.noteOn(494);
                robot.wait(5000);
                robot.noteOff();

            }
            else
            {
                Console.WriteLine($"The {rangeType} threshold value was not exceeded!");
            }
            ContinuePrompt();

            return;

        }

        #endregion
        #region userProgrammingModule
        public static (int motorSpeed, int ledBrightness, double waitSeconds) DisplayUserProgrammingGetCommandsParamter()
        {
            DisplayHeader("Command Parameters");

            (int motorSpeed, int ledBrightness, double waitSeconds) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitSeconds = 0;

            GetValidInteger("Enter Motor Speed [1 - 255]: ", 1, 255, out commandParameters.motorSpeed);
            GetValidInteger("Enter LED brightness [1 - 255]: ", 1, 255, out commandParameters.ledBrightness);
            GetValidDouble("Enter time to wait in seconds: ", 0, 10, out commandParameters.waitSeconds);

            Console.WriteLine($"Motor speed: {commandParameters.motorSpeed}");
            Console.WriteLine($"LED Brightness: {commandParameters.ledBrightness}");
            Console.WriteLine($"Time to wait: {commandParameters.waitSeconds}");

            ContinuePrompt();

            return commandParameters;
        }

        public static void DisplayUserProgrammingGetFinchCommands(List<Command> commands)
        {
            Command command = Command.NONE;

            DisplayHeader("Finch Robot Commands");

            int commandCount = 1;
            Console.WriteLine("List of available commands");
            Console.WriteLine("--------------------------");
            foreach (string commandName in Enum.GetNames(typeof(Command)))
            {
                Console.Write($"- {commandName.ToLower()}");
                if (commandCount % 1 == 0) Console.Write("\n");
                commandCount++;
            }
            Console.WriteLine();

            while (command != Command.DONE)
            {
                Console.Write("Enter command: ");

                if (Enum.TryParse(Console.ReadLine().ToUpper(), out command))
                {
                    commands.Add(command);
                }
                else
                {
                    Console.WriteLine("Please enter a command from the list above.");
                }
            }
        }

        public static void DisplayUserProgrammingShowFinchCommands(List<Command> commands)
        {
            DisplayHeader("Your Commands\n");
            for (int i = 0; i <= commands.Count - 1; i++)
            {
                Console.WriteLine($"{commands[i]}");
            }
            ContinuePrompt();
        }

        public static void DisplayUserProgrammingExecuteFinchCommands
            (Finch robot,
            List<Command> commands,
            (int motorSpeed, int ledBrightness, double waitSeconds) commandParameters)
        {
            int motorSpeed = commandParameters.motorSpeed;
            int ledBrightness = commandParameters.ledBrightness;
            int waitMilliSeconds = (int)(commandParameters.waitSeconds * 1000);
            string commandsFeedback = "";
            const int TURNING_MOTOR_SPEED = 100;

            DisplayHeader("Execute Finch Commands");

            Console.WriteLine("Finch Ready To Execute Commands.");
            ContinuePrompt();

            foreach (Command command in commands)
            {
                switch (command)
                {
                    case Command.NONE:
                        break;

                    case Command.MOVEFORWARD:
                        robot.setMotors(motorSpeed, motorSpeed);
                        commandsFeedback = Command.MOVEFORWARD.ToString();
                        break;

                    case Command.MOVEBACKWARD:
                        robot.setMotors(-motorSpeed, -motorSpeed);
                        commandsFeedback = Command.MOVEBACKWARD.ToString();
                        break;

                    case Command.GETTEMPERATURE:
                        commandsFeedback = $"Temperature: {robot.getTemperature().ToString("n2")}\n";
                        break;

                    case Command.LEDOFF:
                        robot.setLED(0, 0, 0);
                        commandsFeedback = Command.LEDOFF.ToString();
                        break;

                    case Command.LEDON:
                        robot.setLED(ledBrightness, ledBrightness, ledBrightness);
                        commandsFeedback = Command.LEDON.ToString();
                        break;

                    case Command.WAIT:
                        robot.wait(waitMilliSeconds);
                        commandsFeedback = Command.WAIT.ToString();
                        break;

                    case Command.STOPMOTORS:
                        robot.setMotors(0, 0);
                        commandsFeedback = Command.STOPMOTORS.ToString();
                        break;

                    case Command.TURNLEFT:
                        robot.setMotors(TURNING_MOTOR_SPEED, -TURNING_MOTOR_SPEED);
                        commandsFeedback = Command.TURNLEFT.ToString();
                        break;

                    case Command.TURNRIGHT:
                        robot.setMotors(-TURNING_MOTOR_SPEED, TURNING_MOTOR_SPEED);
                        commandsFeedback = Command.TURNRIGHT.ToString();
                        break;
                }
                Console.WriteLine($"{commandsFeedback}");
            }
        }


        public static void GetValidInteger(string v1, int v2, int v3, out int motorSpeed)
        {
            bool validAns = false;
            do
            {
                Console.Write(v1);
                bool isNumber = Int32.TryParse(Console.ReadLine(), out motorSpeed);

                if (isNumber == true && motorSpeed <= 255 && motorSpeed >= 1)
                {
                    validAns = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Please enter an integer between '1' and '255'\n");
                }
            } while (!validAns);
        }

        public static void GetValidDouble(string v1, int v2, int v3, out double waitSeconds)
        {
            bool validAns = false;
            do
            {
                Console.Write(v1);
                bool isDouble = Double.TryParse(Console.ReadLine(), out waitSeconds);

                if (isDouble == false || waitSeconds < 0 || waitSeconds > 10)
                {
                    Console.Clear();
                    Console.WriteLine("Please enter an number between '0' and '10'");
                }
                else
                {
                    validAns = true;
                }
            } while (!validAns);
        }


        #endregion
        #region moduleMenus
        public static void DisplayTalentShow(Finch robot)
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



        public static void DisplayDataRecorder(Finch robot)
        {
            Console.CursorVisible = true;
            int numberOfDataPoints = 0;
            double dataPointFrequency = 0;
            double[] temperatures = null;

            robot.connect();
            bool quitData = false;
            do
            {
                DisplayHeader("\nData Recorder");
                Console.WriteLine("\nWhat would you like to see?");
                Console.WriteLine("a) Number of Data Points");
                Console.WriteLine("b) Frequency of Data Points");
                Console.WriteLine("c) Get Data");
                Console.WriteLine("d) Show Data");
                Console.WriteLine("e) Return to main menu");

                string MenuChoice = Console.ReadLine().ToLower();
                switch (MenuChoice)
                {
                    case "a":
                        numberOfDataPoints = DataRecorderDisplayGetNumberOfDataPoints();
                        break;

                    case "b":
                        dataPointFrequency = DataRecorderDisplayGetDataPointFrequency();
                        break;

                    case "c":
                        temperatures = DataRecorderDisplayGetData(numberOfDataPoints, dataPointFrequency, robot);
                        break;

                    case "d":
                        DataRecorderDisplayGetData(temperatures);
                        break;

                    case "e":
                        quitData = true;
                        break;


                }
            } while (!quitData);
        }
        public static void DisplayAlarmSystem(Finch robot)
        {
            Console.CursorVisible = true;

            robot.connect();
            bool quitAlarm = false;

            string sensorsToMonitor = "";
            string rangeType = "";
            int minMaxThresholdValue = 0;
            int timeToMonitor = 0;

            do
            {
                DisplayHeader("\nAlarm System");
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("a) Set sensors to monitor");
                Console.WriteLine("b) Set range type");
                Console.WriteLine("c) Set minumum/maximum threshold");
                Console.WriteLine("d) Set time to monitor");
                Console.WriteLine("e) Set alarm");
                Console.WriteLine("f) Return to main menu");

                string MenuChoice = Console.ReadLine().ToLower();
                switch (MenuChoice)
                {
                    case "a":
                        sensorsToMonitor = LightAlarmDisplaySetSensorstoMonitor();
                        break;

                    case "b":
                        rangeType = LightAlarmDisplaySetRangeType();
                        break;

                    case "c":
                        minMaxThresholdValue = LightAlarmDisplaySetMinMaxThresholdValue(robot, rangeType);
                        break;

                    case "d":
                        timeToMonitor = LightAlarmDisplaySetTimeToMonitor();
                        break;

                    case "e":
                        LightAlarmDisplaySetAlarm(robot, timeToMonitor, rangeType, minMaxThresholdValue, sensorsToMonitor);
                        break;

                    case "f":
                        quitAlarm = true;
                        break;

                }
            } while (!quitAlarm);
        }
        public static void DisplayUserProgramming(Finch robot)
        {
            robot.connect();
            bool quitUP = false;

            (int motorSpeed, int ledBrightness, double waitSeconds) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitSeconds = 0;

            List<Command> commands = new List<Command>();

            do
            {
                DisplayHeader("User Programming");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("a) Set command parameters");
                Console.WriteLine("b) Add commands");
                Console.WriteLine("c) View commands");
                Console.WriteLine("d) Execute commands");
                Console.WriteLine("e) Return to main menu");
                DisplayChooseAnOption();
                string MenuChoice = Console.ReadLine().ToLower();
                switch (MenuChoice)
                {
                    case "a":
                        commandParameters = DisplayUserProgrammingGetCommandsParamter();
                        break;

                    case "b":
                        DisplayUserProgrammingGetFinchCommands(commands);
                        break;

                    case "c":
                        DisplayUserProgrammingShowFinchCommands(commands);
                        break;

                    case "d":
                        DisplayUserProgrammingExecuteFinchCommands(robot, commands, commandParameters);
                        break;

                    case "e":
                        quitUP = true;
                        Console.Clear();
                        break;
                        

                    default:
                        DisplayIncorrectInput();
                        break;
                }
            } while (!quitUP);

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
        public static void ConnectToFinchRobot(Finch robot)
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
        public static void DisplayDisConnectFinchRobot(Finch robot)
        {
            DisplayHeader("Disconnect from Finch");
            ContinuePrompt();
            robot.disConnect();
            DisplayHeader("Finch has been disconnected");
            ContinuePrompt();
        }
        public static void DisplayChooseAnOption()
        {
            Console.Write("Choose an option>> ");
        }
        public static void DisplayIncorrectInput()
        {
            Console.Clear();
            Console.WriteLine("\nSorry, that is not one of the options.");
            Console.WriteLine("Press any key to try again.");
            Console.ReadKey();
        }
        #endregion

    }
}