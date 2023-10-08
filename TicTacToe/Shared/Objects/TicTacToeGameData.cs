using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Shared.Objects
{
    public class TicTacToeGameData
    {
        /// <summary>
        /// State of the current board
        /// </summary>
        public int[] board = new int[9];
        
        public int NumberOfPlayers = 0;

        /// <summary>
        /// Logic for checking if a valid move occurs will only change values if valid game move is made 
        /// </summary>
        /// <param name="newBoard"></param>
        /// <returns></returns>
        public (bool,TurnResult) TryTurn(int[] newBoard)
        {
            Dictionary<int,int> DifferenceDictionary = new Dictionary<int,int>();
            
            for(int i = 0; i < newBoard.Length; i++)
            {
                if (newBoard[i] != board[i])
                {
                    DifferenceDictionary[i] = newBoard[i];
                }
            }

            if(DifferenceDictionary.Count > 1)
                return (false,TurnResult.MoreThanOneMove);

            if (DifferenceDictionary.Count == 0)
                return (false, TurnResult.NoMove);

            if(board[DifferenceDictionary.First().Key] ==0)
            {
                board[DifferenceDictionary.First().Key] = DifferenceDictionary.First().Value;
                
                return (true,TurnResult.ValidMove);

            }
            else
            {
                return (false,TurnResult.NotValidMove);
            }
        }

        public GameResult CheckGameStatus()
        {
            
            #region Horzontal Winning Condtion
            //Winning Condition For First Row
            if (board[0] == board[1] && board[1] == board[2])
            {
                return CheckWinner(board[0]);
            }
            //Winning Condition For Second Row
            else if (board[3] == board[4] && board[4] == board[5])
            {
                return CheckWinner(board[3]);
            }
            //Winning Condition For Third Row
            else if (board[6] == board[7] && board[7] == board[8])
            {
                return CheckWinner(board[6]);
            }
            #endregion
            #region vertical Winning Condtion
            //Winning Condition For First Column
            else if (board[0] == board[3] && board[3] == board[6])
            {
                return CheckWinner(board[0]);
            }
            //Winning Condition For Second Column
            else if (board[1] == board[4] && board[4] == board[7])
            {
                return CheckWinner(board[1]);
            }
            //Winning Condition For Third Column
            else if (board[2] == board[5] && board[5] == board[8])
            {
                return CheckWinner(board[2]);
            }
            #endregion
            #region Diagonal Winning Condition
            else if (board[0] == board[4] && board[4] == board[8])
            {
                return CheckWinner(board[0]);
            }
            else if (board[2] == board[4] && board[4] == board[6])
            {
                return CheckWinner(board[0]);
            }
            #endregion
            #region Checking For Draw
            // If all the cells or values filled with X or O then any player has won the match
            else if (board.All(x=>x !=0))
            {
                return GameResult.Draw;
            }
            #endregion
            else
            {
                return GameResult.Continue;
            }
        }
        /// <summary>
        ///  converts integer to GameResult if a player is winning 
        /// </summary>
        /// <param name="winner"></param>
        /// <returns></returns>
        private GameResult CheckWinner(int winner)
        {
            return winner switch
            {
                1 => GameResult.XWins,
                2 => GameResult.OWins,
                // TODO throw an error after some testing
                _ => GameResult.Continue,
            };
        }
    }
   
    /// <summary>
    /// Result of an attempt to play a turn.
    /// </summary>
    public enum TurnResult
    {
        NotValidMove,
        MoreThanOneMove,
        NoMove,
        ValidMove
    }
    /// <summary>
    /// Results of Game
    /// </summary>
    public enum GameResult
    {
        XWins,
        OWins,
        Draw,
        Continue
    }
}