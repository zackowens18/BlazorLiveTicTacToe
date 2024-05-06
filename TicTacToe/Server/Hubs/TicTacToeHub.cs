using Microsoft.AspNetCore.SignalR;
using TicTacToe.Shared.Services;
using TicTacToe.Shared.Objects;
namespace TicTacToe.Server.Hubs
{
    public class TicTacToeHub : Hub
    {
        // needs to be converted to in memory caches due to constructor being called everytime
        Dictionary<string, TicTacToeGameData> RoomGameDict;

        public TicTacToeHub(GameService gameService)
        {
            RoomGameDict = gameService._gameData;
        }
        public override async Task OnConnectedAsync()
        {
            await SendMessage("A new user", "userConnected");
            await base.OnConnectedAsync();
        }

        public async Task SendMessage(string room, string message)
        {
            await Clients.Group(room).SendAsync("ReceivedMove", room, message);
        }

        public async Task<JoinedState> JoinGame(string room)
        {
            if (RoomGameDict.ContainsKey(room))
            {
                if (RoomGameDict[room].NumberOfPlayers >= 2)
                    return new JoinedState(false,"Too Many Players", false, new int[0]);
                
                if(RoomGameDict[room].NumberOfPlayers == 0)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, room);
                    RoomGameDict[room].NumberOfPlayers++;
                    // Player is X
                    return new JoinedState(true,string.Empty,true, RoomGameDict[room].board);
                }
                else
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, room);
                    RoomGameDict[room].NumberOfPlayers++;
                    // Player is O
                    return new JoinedState(true, string.Empty, false, RoomGameDict[room].board);
                }
            }
            else
            {
                RoomGameDict[room] = new TicTacToeGameData();
                RoomGameDict[room].NumberOfPlayers++;

                await Groups.AddToGroupAsync(Context.ConnectionId, room);
                return new JoinedState(true, string.Empty, true, RoomGameDict[room].board);
            }       
        }

        public async Task LeaveGame(string room)
        {
            RoomGameDict[room].NumberOfPlayers--;
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, room);
        }

        public async Task SendMove(int[] board, string room)
        {
            var (isMoveValid,result) = RoomGameDict[room].TryTurn(board);
            if (!isMoveValid)
            {
                Console.WriteLine($"Move {isMoveValid} - {result}");
                return;
            }

            var gameResult = RoomGameDict[room].CheckGameStatus();
            await Clients.Group(room).SendAsync("Move", room, board, gameResult);
            if(gameResult != GameResult.Continue)
            {
                RoomGameDict[room] = new TicTacToeGameData();
            }
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
