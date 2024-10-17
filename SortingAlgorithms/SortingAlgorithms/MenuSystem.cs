﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgorithms
{
    internal class MenuSystem
    {
        string callingClass;
        Dictionary<string, MethodInfo> menuOptions;
        
        public MenuSystem(string callingClassName)
        {
            callingClass = callingClassName;
            menuOptions = new Dictionary<string, MethodInfo>();

            Type thisType = typeof(Environment);
            AddMenuItem("Exit Application", thisType.GetMethod("Exit"));

        }

        public void RunForever()
        {
            //infinite loop (until application exited)
            while (true)
            {
                Run();
            }
        }

        public void Run()
        {
            DisplayOptions();
            GetMenuSelection();
        }
        void DisplayOptions()
        {
            Console.Clear();
            Console.WriteLine("Please choose from the following options: \n");
            int i = 0;

            foreach (string menuItem in menuOptions.Keys)
            {
                Console.WriteLine("(" + i + ") " + menuItem);
                i++;
            }
            Console.WriteLine("\n\n");
        }

        void GetMenuSelection()
        {
            string userInput = Console.ReadLine();
            MethodInfo mi;

            //Has the user entered an integer value?
            if (int.TryParse(userInput, out int integerInput))
            {
                if (integerInput == 0)
                {
                    mi = menuOptions.ElementAt(integerInput).Value;
                    mi.Invoke(this, new object[] { 0 });
                    Console.ReadKey();

                }
                else if (integerInput > 0 && integerInput < menuOptions.Count)
                {
                    mi = menuOptions.ElementAt(integerInput).Value;
                    mi.Invoke(this, null);
                    Console.ReadKey();
                }
            }
        }
        
        public void AddMenuItem(string menuMessage, MethodInfo methodInfo)
        {
            menuOptions.Add(menuMessage, methodInfo);

        }

        public void AddMenuItem(string menuMessage, string methodName, Type callingType)
        {
            MethodInfo methodInfo = callingType.GetMethod(methodName);
            menuOptions.Add(menuMessage, methodInfo);
        }


    }
}