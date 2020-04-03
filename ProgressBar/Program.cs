using ProgressBar.ProgressBars;
using System;
using System.Threading;

namespace ProgressBar
{
    class Program
    {
        static void Main(string[] args)
        {
            //FollowerProgressBarExample();
            NormProgressBarExample();
        }

        public static void FollowerProgressBarExample()
        {
            // Please use the following styles of the progress bar:
            // 1. Head
            // 2. Rear
            var pb = new FollowerProgressBar(FollowerProgressBarStyle.Rear);
            pb.SetInfo("Reading...", ConsoleColor.Cyan);
            pb.Start();
            Thread.Sleep(5000); // do something here.
            pb.SetInfo("Analysing...", ConsoleColor.Cyan);
            Thread.Sleep(3000); // do something here.
            pb.SetInfo("Executing...", ConsoleColor.Cyan);
            Thread.Sleep(2000); // do something here.
            pb.SetInfo("Done.\r\nPress any key to exit...");
            pb.Stop(); // stop it manually when the task has been finished.

            Console.ReadKey();
        }

        public static void NormProgressBarExample()
        {
            // Please use the following styles of the progress bar:
            // 1. CharacterFill
            // 2. CharacterReplace
            // 3. ColorBlock
            var pb = new NormProgressBar(NormProgressBarStyle.CharacterReplace);
            /* Only config for ColorBlock sub-style */
            //pb.Background = ConsoleColor.DarkYellow;
            //pb.BlockColor = ConsoleColor.Magenta; 

            /* Config for CharacterFill and CharacterReplace sub-styles */
            //pb.Foreground = ConsoleColor.Green;
            //pb.ProgressBarStartChar = '[';
            //pb.ReplacedProgressBackgroundChar = '=';
            //pb.FilledProgressBodyChar = 'O';
            //pb.FilledProgressHeaderChar = 'O';
            //pb.ProgressBarEndChar = ']';

            pb.Initialize();

            // Message list
            string[] msgs = new string[]
            {
                "Processing...\r\nFor Task1: Reading data.",
                "Processing...\r\nFor Task2: Analysing data.",
                "Processing...\r\nFor Task3: Executing operation on data.",
                "Completed"
            };

            var value = 0.0;
            var index = 0;
            string msg = string.Empty;
            while (!pb.Done)
            {
                if (value == 0.0)
                {
                    msg = msgs[index++];
                }
                else if (value == 30.0)
                {
                    msg = msgs[index++];
                }
                else if (value == 65.0)
                {
                    msg = msgs[index++];
                }
                else if (value == 99.0)
                {
                    msg = msgs[index++];
                }

                pb.SetInfo(value++, msg + "\t" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ConsoleColor.White, ConsoleColor.Green);

                // do something in while loop
                // maybe there is a job list
                Thread.Sleep(200);

                // if the value is not equal to 100.0, progress will not stop, even if the "Stop" function has been called.
                pb.Stop(); 
            }

            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
