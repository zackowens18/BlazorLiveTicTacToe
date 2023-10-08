using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Shared.Objects
{
    public record JoinedState(bool joined, string reason,bool isPlayerX, int[] board);
}
