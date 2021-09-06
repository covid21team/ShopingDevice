using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSP_Covid21.Areas.Game.Models;
using TSP_Covid21.Areas.Game.ViewModel;
using TSP_Covid21.Models.ShopEntity;

namespace TSP_Covid21.Areas.Game.Controllers
{
    public class BauCuaController : Controller
    {
        private BauCua BC;

        public BauCuaController()
        {
            BC = new BauCua();
        }

        // GET: Game/BauCua
        public ActionResult Home()
        {
            return View();
        }

        // Run khi vừa dăng nhập thành công
        public ActionResult ListRoom()
        {
            Session["user_BauCua"] = Session["user_BauCua"].ToString();
            return PartialView(BC.ListRoom());
        }

        public ww_BAUCUA_ACCOUNT Account(string user)
        {
            return BC.Account(user);
        }

        //Hàm dành cho player tạo phòng
        public ActionResult ExitRoom(int roomId)
        {
            BC.DelRoom(roomId);
            return RedirectToAction("ListRoom");
        }

        // Load danh sách để biết thêm sự kiện mới
        public ActionResult Load_ListRoom()
        {
            return PartialView(BC.ListRoom());
        }

        //Session được kêu gọi trong controller
        public ActionResult JoinRoom(int id)
        {
            var user = Session["user_BauCua"].ToString();
            //result: playerId
            var result = BC.CreatePlayer(id, user);
            return PartialView(result);
        }

        public ActionResult ExitJoinRoom(int playerId)
        {
            BC.ExitJoinRoom(playerId);
            return RedirectToAction("ListRoom");
        }

        public void KickPlayer(int playerId)
        {
            BC.KickPlayer(playerId);
        }

        public ww_BAUCUA_ROOM TakeRoom(int playerId)
        {
            return BC.TakeRoom(playerId);
        }

        public bool CheckChessBoard(int playerId)
        {
            var result = BC.CheckChessBoard(playerId);

            if (result.Count() == 0)
            {
                return false; //Phòng đã bị xóa
            }
            return true; //Phòng còn tồn lại
        }

        public ActionResult LoadRoom(int roomId, int valueDeer, int valueGourd, int valueChicken, int valueFish, int valueCrab, int valueShrimp)
        {
            Session["user_BauCua"] = Session["user_BauCua"].ToString();

            Inf_ChessBoard inf_ChessBoard = new Inf_ChessBoard()
            {
                roomId = roomId,
                valueDeer = valueDeer,
                valueCrab = valueCrab,
                valueChicken = valueChicken,
                valueFish = valueFish,
                valueGourd = valueGourd,
                valueShrimp = valueShrimp,
            };
            return PartialView(inf_ChessBoard);
        }

        public IEnumerable<ww_BAUCUA_PLAYER> ListPlayer(int id)
        {
            var result = BC.ListPlayer(id);

            //Mặc định load đúng 7 player tuy không đủ 7 player, nhầm giúp phần giao diện dễ xử lí
            while (result.Count() < 7)
            {
                ww_BAUCUA_PLAYER player = new ww_BAUCUA_PLAYER()
                {
                    ROOMID = id,
                    USERNAME = "None",
                    READY = false,
                    COLORID = 0,
                };
                result.Add(player);
            }

            return result;
        }

        public IEnumerable<ww_BAUCUA_PLAYERDETAIL> listPlayerDetail(int roomId)
        {
            var result = BC.listPlayerDetail(roomId);

            //Mặc định load đúng 36 vị trí cá cược của 6 player dù không đủ 6 player join room, nhầm giúp phần giao diện dễ xử lí
            while (result.Count() < 36)
            {
                ww_BAUCUA_PLAYERDETAIL playerDetail = new ww_BAUCUA_PLAYERDETAIL()
                {
                    PLAYERID = 0,
                    ANIMALID = 0,
                    BETS = 0,
                };
                result.Add(playerDetail);
            }

            return result;
        }

        public ActionResult Player(int roomId, int count)
        {
            return PartialView();
        }

        //Session được kêu gọi trong controller
        public ActionResult CreateRoom()
        {
            var user = Session["user_BauCua"].ToString();
            var result = BC.CreateRoom(user);
            return PartialView(result);
        }

        public int IsPass(int id)
        {
            return BC.IsPass(id);
        }

        public bool CheckPass(int id, string pass)
        {
            return BC.CheckPass(id, pass);
        }

        public ww_BAUCUA_ACCOUNT Owner(int roomId)
        {
            return BC.Owner(roomId);
        }

        public double Bets(int animalId, int playerId, string betsValue, int roomId)
        {
            var result = BC.Bets(animalId, playerId, betsValue, roomId);

            return result;
        }

        public int CheckAccount(string user, string pass)
        {
            var result = BC.CheckAccount(user, pass);
            if(result == 1)
            {
                Session["user_BauCua"] = user;
            }
            return result;
        }

        public ActionResult ListChat(int roomId)
        {
            var listChat = BC.ListChat(roomId);

            foreach(var item in listChat)
            {
                if(item.PLAYERID == null)
                {
                    item.PLAYERID = 1;
                }
            }

            while(listChat.Count() < 7)
            {
                ww_BAUCUA_CHAT chat = new ww_BAUCUA_CHAT()
                {
                    PLAYERID = 0, //0: có nghĩa là không tồn tại
                    TEXTCHAT = "None",
                };
                listChat.Add(chat);
            }

            return PartialView(listChat);
        }

        public void SendChat(int playerId, string textChat)
        {
            BC.SendChat(playerId, textChat);
        }

        public ActionResult PlayerOfRoom(int roomId)
        {
            var result = BC.ListPlayer(roomId);
            List<ww_BAUCUA_PLAYER> listPlayer = result.ToList();
            listPlayer.RemoveAt(0);
            return PartialView(listPlayer);
        }

        public void Setting(int roomId, string pass, double bets)
        {
            BC.Setting(roomId, pass, bets);
        }

        public string LoadInfRoom(int playerId)
        {
            var result = BC.LoadInfRoom(playerId);
            string str = "";
            str += result.minBets.ToString();
            str += "," + result.money.ToString();

            return str;
        }

        public double Money(string userOwner)
        {
            return BC.Money(userOwner);
        }

        public void LogOut(string user)
        {
            Session["user_BauCua"] = "myfirstgameonline";
            BC.LogOut(user);
        }

        public void Dice(int roomId)
        {
            int dice01, dice02, dice03;
            Random random = new Random();
            dice01 = random.Next(1, 6);
            dice02 = random.Next(1, 6);
            dice03 = random.Next(1, 6);

            int deer = 0, gourd = 0, chicken = 0, fish = 0, crab = 0, shrimp = 0;
            switch (dice01)
            {
                case 1:
                    deer++;
                    break;
                case 2:
                    gourd++;
                    break;
                case 3:
                    chicken++;
                    break;
                case 4:
                    fish++;
                    break;
                case 5:
                    crab++;
                    break;
                case 6:
                    shrimp++;
                    break;
            }
            switch (dice02)
            {
                case 1:
                    deer++;
                    break;
                case 2:
                    gourd++;
                    break;
                case 3:
                    chicken++;
                    break;
                case 4:
                    fish++;
                    break;
                case 5:
                    crab++;
                    break;
                case 6:
                    shrimp++;
                    break;
            }
            switch (dice03)
            {
                case 1:
                    deer++;
                    break;
                case 2:
                    gourd++;
                    break;
                case 3:
                    chicken++;
                    break;
                case 4:
                    fish++;
                    break;
                case 5:
                    crab++;
                    break;
                case 6:
                    shrimp++;
                    break;
            }

            BC.Dice(roomId, dice01, dice02, dice03, deer, gourd, chicken, fish, crab, shrimp);
        }

        public int TakeDice(int roomId, int playerId)
        {
            var result = BC.TakeDice(roomId, playerId);
            int str = 0;
            str = (int)result.DICE1 + (int)result.DICE2*10 + (int)result.DICE3*100;
            return str;
        }

        public ActionResult TakePic(int code)
        {
            return PartialView(code);
        }

        public bool CheckDice(int playerId)
        {
            return BC.CheckDice(playerId);
        }

        public void ReadyPlayer(int playerId, int isReady)
        {
            BC.ReadyPlayer(playerId, isReady);
        }

        public bool checkReady(int roomId)
        {
            return BC.checkReady(roomId);
        }

        public void TakeMoney(int playerId)
        {
            BC.TakeMoney(playerId);
        }

        //Dùng cho chủ phòng: kiểm đã bấm nút thoát chưa
        public bool CheckRoom(int roomId)
        {
            return BC.CheckRoom(roomId);
        }

        public bool CheckAmount(int roomId)
        {
            var result = BC.CheckAmount(roomId);

            if(result.AMOUNTPERSON == 6)
            {
                return false;
            }
            return true;
        }

        //Kiểm tra đủ xèng để tạo phòng không
        public bool CheckMoney(int minBets)
        {
            var user = Session["user_BauCua"].ToString();
            var result = BC.CheckMoney(user);
            if(result >= minBets*10)
            {
                return true;
            }
            return false;
        }

        public bool CheckUserExist(string user)
        {
            var result = BC.CheckUserExist(user);
            if(result == null)
            {
                return false;
            }
            return true;
        }

        public void CreateAccount(string user, string pass, string name)
        {
            BC.CreateAccount(user, pass, name);
        }
    }
}