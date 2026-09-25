using GameApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacadePattern
{
    public class GameFacade
    {
        private readonly GraphicsSystem graphics;
        private readonly AudioSystem audio;
        private readonly SaveSystem saveSystem;
        private readonly NetworkService network;
        private readonly GameEngine gameEngine;

        public GameFacade()
        {
            graphics = new GraphicsSystem();
            audio = new AudioSystem();
            saveSystem = new SaveSystem();
            network = new NetworkService();
            gameEngine = new GameEngine();
        }

        public void StartGame(bool developerMode, bool onlineMode)
        {
            Console.WriteLine("Game wordt gestart...");

            graphics.Initialize();
            graphics.SetResolution(1920, 1080);

            audio.Initialize();
            audio.SetVolume(70);

            saveSystem.LoadSettings();
            saveSystem.LoadPlayer();

            if (onlineMode)
            {
                network.Connect();
                network.Login();
            }

            gameEngine.LoadWorld();

            if (developerMode)
            {
                Console.WriteLine("Developer Mode actief.");
            }

            gameEngine.Start();
        }

        public void StopGame(bool onlineMode)
        {
            Console.WriteLine("Game wordt gestopt...");

            gameEngine.Stop();

            saveSystem.SaveGame();

            if (onlineMode)
            {
                network.Disconnect();
            }

            audio.Mute();
            graphics.Shutdown();

            Console.WriteLine("Game volledig afgesloten.");
        }
    }
}
