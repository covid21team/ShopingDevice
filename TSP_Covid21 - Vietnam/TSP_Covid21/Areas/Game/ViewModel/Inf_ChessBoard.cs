using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSP_Covid21.Areas.Game.ViewModel
{
    // Dùng load lại giá trị của bàn cờ, bao gồm giá trị của xúc xắc
    public class Inf_ChessBoard
    {
        public int roomId { get; set; }
        public int valueDeer { get; set; }
        public int valueGourd { get; set; }
        public int valueChicken { get; set; }
        public int valueFish { get; set; }
        public int valueCrab { get; set; }
        public int valueShrimp { get; set; }
    }
}