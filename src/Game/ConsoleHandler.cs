using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Casino_Schmirtz_Royale.Game
{
    public static class ConsoleHandler
    {
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private static MainWindow w;

        private static bool doAutoroll;

        public static void Initialize(MainWindow w)
        {
            ConsoleHandler.w = w;
            AllocConsole();
            Console.WriteLine("Commands: \n" +
                "/givemoney <value> \n" +
                "/removemoney <value> \n" +
                "/autoplay <times> \n" +
                "/cancelautoplay \n");

            Task.Run(HandleInput);
        }

        public static Task HandleInput()
        {
            while (true)
            {
                string cmd = Console.ReadLine();
                try
                {
                    if (cmd.StartsWith("/givemoney") || cmd.StartsWith("/removemoney"))
                    {
                        w.Dispatcher.Invoke(() => GameManager.ChangeBalance(cmd[1] == 'g' ? int.Parse(cmd.Split(' ')[1]) : (int.Parse(cmd.Split(' ')[1])) * -1));
                    }
                    else if (cmd.StartsWith("/autoplay"))
                    {
                        Task.Run(() => AutoRollTask(int.Parse(cmd.Split(' ')[1])));
                    }
                    else if (cmd == "/cancelautoplay")
                    {
                        doAutoroll = false;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something could not be parsed, check the syntax! (" + ex.Message + ")");
                }
            }
        }

        private static async Task AutoRollTask(int times)
        {
            doAutoroll = true;
            w.Dispatcher.Invoke(() => { w.respinBtn.IsEnabled = false; });
            for(int i = 0; i < times; i++)
            {
                await w.Dispatcher.Invoke(async () => { await GameManager.DoSpin(); });
                await Task.Delay(100);
                if (!doAutoroll)
                {
                    w.Dispatcher.Invoke(() => { w.respinBtn.IsEnabled = true; });
                    return;
                }
            }
            w.Dispatcher.Invoke(() => { w.respinBtn.IsEnabled = true; });

        }
    }
}
