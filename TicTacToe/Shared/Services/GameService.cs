using TicTacToe.Shared.Objects;
namespace TicTacToe.Shared.Services
{
    /// <summary>
    ///  Class that represents the room to game data needed as Hubs are reconstructed on each call
    /// </summary>
    public class GameService 
    {
        /// <summary>
        /// Represents the room : game data for lookups in hub
        /// </summary>
        public Dictionary<string, TicTacToeGameData> _gameData;

        /// <summary>
        /// Sets up the Game Service for later use
        /// </summary>
        public GameService()
        {
            _gameData = new Dictionary<string,TicTacToeGameData>();
        }
    }
}
