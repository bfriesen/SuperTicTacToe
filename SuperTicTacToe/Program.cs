using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SuperTicTacToe.Domain;

namespace SuperTicTacToe
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //var game = new SuperTicTacToeGame();

            //game.Move(Player.X, 3, 3);
            //game.Move(Player.X, 3, 4);
            //game.Move(Player.X, 3, 5);

            //var gameAvailableSpaces = game.GetAvailableSpaces().ToArray();
            //var spaceAvailableSpaces = ((TicTacToeGame)game.Spaces[3]).GetAvailableSpaces().ToArray();

            //game.Move(Player.X, 4, 3);
            //game.Move(Player.X, 4, 4);
            //game.Move(Player.X, 4, 5);

            //game.Move(Player.X, 0, 0);
            //game.Move(Player.O, 0, 1);
            //game.Move(Player.X, 0, 2);
            //game.Move(Player.O, 0, 3);
            //game.Move(Player.X, 0, 4);
            //game.Move(Player.O, 0, 5);
            //game.Move(Player.O, 0, 6);
            //game.Move(Player.X, 0, 7);
            //game.Move(Player.O, 0, 8);

            //gameAvailableSpaces = game.GetAvailableSpaces().ToArray();
            //spaceAvailableSpaces = ((TicTacToeGame)game.Spaces[0]).GetAvailableSpaces().ToArray();

            //game.Move(Player.X, 5, 3);
            //game.Move(Player.X, 5, 4);
            //game.Move(Player.X, 5, 5);

            //gameAvailableSpaces = game.GetAvailableSpaces().ToArray();


            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}
