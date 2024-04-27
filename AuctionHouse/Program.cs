using AuctionHouse.Dialogs.Menus;
using System.Text;
using static System.Console;

namespace AuctionHouse
{
    /// <summary>
    /// Program entry point.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main entry point.
        /// </summary>
        /// <param name="args">Command line arguments: if supplied.</param>
        static void Main(string[] args)
        {
            // Do not need this possibly
            InputEncoding = Encoding.Unicode;
            OutputEncoding = Encoding.Unicode;

            WriteLine("+------------------------------+");
            WriteLine("| Welcome to the Auction House |");
            WriteLine("+------------------------------+");

            MainMenuDialog mainMenu = new MainMenuDialog(new AuctionHouse());
            mainMenu.Display();
        }
    }
}