using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using TSP_Covid21.Areas.Game.ViewModel;
using TSP_Covid21.Models.ShopEntity;

namespace TSP_Covid21.Areas.Game.Models
{
    public class BauCua
    {
        private COVIDEntities db;

        public BauCua()
        {
            db = new COVIDEntities();
        }
        
        public IEnumerable<ww_BAUCUA_ROOM> ListRoom()
        {
            var result = db.ww_BAUCUA_ROOM.ToList();
            return result;
        }

        public ww_BAUCUA_ACCOUNT Account(string user)
        {
            return db.ww_BAUCUA_ACCOUNT.Find(user);
        }

        public int IsPass(int id)
        {
            var result = db.ww_BAUCUA_ROOM.Find(id);
            if(result == null)
            {
                return 2;
            }

            if(result.PASS == null)
            {
                return 0;
            }
            return 1;
        }

        public bool CheckPass(int id, string pass)
        {
            var result = db.ww_BAUCUA_ROOM.Find(id);

            if(result.PASS == pass)
            {
                return true;
            }
            return false;
        }

        public ww_BAUCUA_ROOM CreateRoom(string user)
        {
            ww_BAUCUA_ROOM room = new ww_BAUCUA_ROOM()
            {
                AMOUNTPERSON = 0,
                ROOMSTATUS = false,
                MINBETS = 1000,
                OWNER = user,
                PASS = null,
                DICE1 = null,
                DICE2 = null,
                DICE3 = null,
            };
            db.ww_BAUCUA_ROOM.AddOrUpdate(room);
            db.SaveChanges();

            var roomNew = db.ww_BAUCUA_ROOM.Where(t => t.OWNER == user).FirstOrDefault();

            /*
             Lí do tạo player cho chủ phòng
                - Phải tồn tạo trong player thì mới tương tác được với bảng chat
                - Tại sao colorId mặc định bằng 7, để khỏi dành màu của những player join vào room
             */
            ww_BAUCUA_PLAYER player = new ww_BAUCUA_PLAYER()
            {
                ROOMID = roomNew.ROOMID,
                USERNAME = user,
                READY = false,
                COLORID = 7,
                DICE = false,
            };
            db.ww_BAUCUA_PLAYER.AddOrUpdate(player);
            db.SaveChanges();

            return roomNew;
        }

        public ww_BAUCUA_ACCOUNT Owner(int roomId)
        {
            var result = db.ww_BAUCUA_ROOM.Where(t => t.ROOMID == roomId).Select(c => c.ww_BAUCUA_ACCOUNT).FirstOrDefault();
            return result;
        }

        public int CreatePlayer(int id, string user)
        {
            var color = 0;
            var listColor = db.ww_BAUCUA_COLOR.ToList();
            // Kiểm tra màu nào những player trước vào chưa sử dụng
            foreach(var item in listColor)
            {
                var check = db.ww_BAUCUA_PLAYER.Where(t => t.ROOMID == id && t.COLORID == item.COLORID).ToList();
                if(check.Count() == 0)
                {
                    color = item.COLORID;
                    break;
                }
            }

            ww_BAUCUA_PLAYER player = new ww_BAUCUA_PLAYER()
            {
                ROOMID = id,
                USERNAME = user,
                READY = false,
                COLORID = color,
                DICE = false,
            };
            db.ww_BAUCUA_PLAYER.AddOrUpdate(player);
            db.SaveChanges();

            ww_BAUCUA_ROOM room = db.ww_BAUCUA_ROOM.Find(id);
            room.AMOUNTPERSON++;
            db.SaveChanges();

            int playerId = db.ww_BAUCUA_PLAYER.Where(t => t.USERNAME == user).Select(c => c.PLAYERID).FirstOrDefault();

            var list_animals = db.ww_BAUCUA_ANIMAL.ToList();
            foreach(var item in list_animals)
            {
                ww_BAUCUA_PLAYERDETAIL playerDetail = new ww_BAUCUA_PLAYERDETAIL()
                {
                    PLAYERID = playerId,
                    ANIMALID = item.ANIMALID,
                    BETS = 0,
                };
                db.ww_BAUCUA_PLAYERDETAIL.AddOrUpdate(playerDetail);
            }
            db.SaveChanges();

            return playerId;
        }

        public ww_BAUCUA_ROOM TakeRoom(int playerId)
        {
            var result = db.ww_BAUCUA_PLAYER.Find(playerId).ww_BAUCUA_ROOM;

            return result;
        }

        public IEnumerable<ww_BAUCUA_PLAYER> CheckChessBoard(int playerId)
        {
            var result = db.ww_BAUCUA_PLAYER.Where(t => t.PLAYERID == playerId);
            return result;
        }

        // Tại sao không dùng IEnumerable mà dùng List, do dùng List mới add thêm số lượng bị thiếu
        public List<ww_BAUCUA_PLAYER> ListPlayer(int id)
        {
            var result = db.ww_BAUCUA_PLAYER.Where(t => t.ROOMID == id).ToList();
            return result;
        }

        // Tại sao không dùng IEnumerable mà dùng List, do dùng List mới add thêm số lượng bị thiếu
        public List<ww_BAUCUA_PLAYERDETAIL> listPlayerDetail(int roomId)
        {
            var result = db.ww_BAUCUA_PLAYERDETAIL.Where(t => t.ww_BAUCUA_PLAYER.ROOMID == roomId).ToList();
            return result;
        }

        //Chỉ mới xóa phòng chưa tính toán hết
        public void DelRoom(int roomId)
        {
            var owner = db.ww_BAUCUA_ROOM.Find(roomId).ww_BAUCUA_ACCOUNT;
            //Xóa phòng khỏi hệ thống và đền bù player
            var list_playerDetail = db.ww_BAUCUA_PLAYERDETAIL.Where(t => t.ww_BAUCUA_PLAYER.ROOMID == roomId);

            foreach(var item in list_playerDetail)
            {
                var account = db.ww_BAUCUA_PLAYER.Find(item.PLAYERID).ww_BAUCUA_ACCOUNT;
                account.MONEY += item.BETS * 2;
                owner.MONEY -= item.BETS * 2;
                db.ww_BAUCUA_PLAYERDETAIL.Remove(item);
            }
            if(owner.MONEY < 0)
            {
                owner.MONEY = 0;
            }
            db.SaveChanges();

            //Xóa đoạn chat của player
            var list_chat = db.ww_BAUCUA_CHAT.Where(t => t.ww_BAUCUA_PLAYER.ROOMID == roomId);

            foreach(var item in list_chat)
            {
                db.ww_BAUCUA_CHAT.Remove(item);
            }
            db.SaveChanges();

            //Xóa player
            var list_player = db.ww_BAUCUA_PLAYER.Where(t => t.ROOMID == roomId);

            foreach(var item in list_player)
            {
                db.ww_BAUCUA_PLAYER.Remove(item);
            }
            db.SaveChanges();

            //Xóa room
            var room = db.ww_BAUCUA_ROOM.Find(roomId);
            db.ww_BAUCUA_ROOM.Remove(room);
            db.SaveChanges();
        }

        public void ExitJoinRoom(int playerId)
        {
            var list_playerDetail = db.ww_BAUCUA_PLAYERDETAIL.Where(t => t.PLAYERID == playerId);

            var roomId = db.ww_BAUCUA_PLAYER.Find(playerId).ROOMID;

            var listChat = db.ww_BAUCUA_CHAT.Where(t => t.PLAYERID == playerId);

            ww_BAUCUA_ACCOUNT owner = db.ww_BAUCUA_ROOM.Find(roomId).ww_BAUCUA_ACCOUNT;

            // Xóa những tiền cược của player và tiền cược được tính vào chủ phòng
            foreach (var item in list_playerDetail)
            {
                owner.MONEY += item.BETS;
                db.ww_BAUCUA_PLAYERDETAIL.Remove(item);
            }
            db.SaveChanges();

            // Xóa đoạn chat của player
            foreach(var item in listChat)
            {
                db.ww_BAUCUA_CHAT.Remove(item);
            }
            db.SaveChanges();

            // Xóa player
            var player = db.ww_BAUCUA_PLAYER.Find(playerId);
            db.ww_BAUCUA_PLAYER.Remove(player);
            db.SaveChanges();

            // Giảm số lượng player
            ww_BAUCUA_ROOM room = db.ww_BAUCUA_ROOM.Find(roomId);
            room.AMOUNTPERSON--;
            db.SaveChanges();
        }

        public void KickPlayer(int playerId)
        {
            var playerDetail = db.ww_BAUCUA_PLAYERDETAIL.Where(t => t.PLAYERID == playerId);

            var player = db.ww_BAUCUA_PLAYER.Find(playerId);

            var account = db.ww_BAUCUA_ACCOUNT.Find(player.USERNAME);

            var listChat = db.ww_BAUCUA_CHAT.Where(t => t.PLAYERID == playerId);

            //Trả tiền cho player và xóa những tiền cược
            foreach (var item in playerDetail)
            {
                account.MONEY += item.BETS;
                db.ww_BAUCUA_PLAYERDETAIL.Remove(item);
            }
            db.SaveChanges();

            //Xóa đoạn tin nhắn của player
            foreach(var item in listChat)
            {
                db.ww_BAUCUA_CHAT.Remove(item);
            }
            db.SaveChanges();

            //Giảm số lượng player
            ww_BAUCUA_ROOM room = db.ww_BAUCUA_ROOM.Find(player.ROOMID);
            room.AMOUNTPERSON--;
            db.SaveChanges();

            //Xóa player
            db.ww_BAUCUA_PLAYER.Remove(player);
            db.SaveChanges();
        }

        public double Bets(int animalId, int playerId, string betsValue, int roomId)
        {
            double minBets = db.ww_BAUCUA_ROOM.Find(roomId).MINBETS;
            string user = db.ww_BAUCUA_PLAYER.Find(playerId).USERNAME;
            double moneyPlayer = db.ww_BAUCUA_ACCOUNT.Find(user).MONEY;
            double betsPlayer = double.Parse(betsValue);
            double maxBets = minBets * 5;

            if(moneyPlayer < minBets)
            {
                return 0;
            }

            if(betsPlayer < minBets)
            {
                UpdateBets(playerId, animalId, minBets);
                return minBets;
            }

            if(betsPlayer > moneyPlayer)
            {
                if(moneyPlayer > maxBets)
                {
                    UpdateBets(playerId, animalId, maxBets);
                    return maxBets;
                }
                UpdateBets(playerId, animalId, moneyPlayer);
                return moneyPlayer;
            }
            else
            {
                if(betsPlayer > maxBets)
                {
                    UpdateBets(playerId, animalId, maxBets);
                    return maxBets;
                }
            }
            UpdateBets(playerId, animalId, betsPlayer);
            return betsPlayer;
        }

        private void UpdateBets(int playerId, int animalId, double value)
        {
            var playerDetail = db.ww_BAUCUA_PLAYERDETAIL.Where(t => t.PLAYERID == playerId && t.ANIMALID == animalId).FirstOrDefault();
            var user = db.ww_BAUCUA_PLAYER.Find(playerId).USERNAME;
            var player = db.ww_BAUCUA_ACCOUNT.Find(user);

            player.MONEY += playerDetail.BETS;
            db.SaveChanges();

            playerDetail.BETS = value;
            db.SaveChanges();

            player.MONEY -= value;
            db.SaveChanges();
        }

        public int CheckAccount(string user, string pass)
        {
            var result = db.ww_BAUCUA_ACCOUNT.Where(t => t.USERNAME == user && t.PASS == pass).FirstOrDefault();

            if(result == null)
            {
                return 0;
            }
            if(result.AC_STATUS == true)
            {
                return 2;
            }
            result.AC_STATUS = true;
            db.SaveChanges();
            return 1;
        }

        // Tại sao không dùng IEnumerable mà dùng List, do dùng List mới add thêm số lượng bị thiếu
        public List<ww_BAUCUA_CHAT> ListChat(int roomId)
        {
            List<ww_BAUCUA_CHAT> listChat = new List<ww_BAUCUA_CHAT>();

            var room = db.ww_BAUCUA_ROOM.Find(roomId);
            if(room == null)
            {
                return listChat;
            }

            var numberPerson = db.ww_BAUCUA_ROOM.Find(roomId).AMOUNTPERSON;

            var listPlayer = db.ww_BAUCUA_PLAYER.Where(t => t.ROOMID == roomId).Select(c => c.PLAYERID);

            foreach(var item in listPlayer)
            {
                var chat = db.ww_BAUCUA_CHAT.Where(t => t.PLAYERID == item).Select(c => c).FirstOrDefault();
                if(chat != null)
                {
                    listChat.Add(chat);
                    chat.NUMBERVIEWERS++;
                    if(chat.NUMBERVIEWERS-1 >= numberPerson){
                        db.ww_BAUCUA_CHAT.Remove(chat);
                    }
                }
                else
                {
                    ww_BAUCUA_CHAT tempChat = new ww_BAUCUA_CHAT()
                    {
                        PLAYERID = 0,
                        TEXTCHAT = "None",
                        NUMBERVIEWERS = 0,
                    };
                    listChat.Add(tempChat);
                }
            }
            db.SaveChanges();

            return listChat;
        }

        public void SendChat(int playerId, string textChat)
        {
            ww_BAUCUA_CHAT chat = new ww_BAUCUA_CHAT()
            {
                PLAYERID = playerId,
                TEXTCHAT = textChat,
                NUMBERVIEWERS = 0,
            };
            db.ww_BAUCUA_CHAT.AddOrUpdate(chat);
            db.SaveChanges();
        }

        public void Setting(int roomId, string pass, double bets)
        {
            ww_BAUCUA_ROOM room = db.ww_BAUCUA_ROOM.Find(roomId);
            if(pass != "")
            {
                room.PASS = pass;
            }
            room.MINBETS = bets;
            db.SaveChanges();
        }

        public Inf_Room LoadInfRoom(int playerId)
        {
            var player = db.ww_BAUCUA_PLAYER.Find(playerId);
            var room = player.ww_BAUCUA_ROOM;

            Inf_Room infRoom = new Inf_Room()
            {
                minBets = room.MINBETS,
                money = player.ww_BAUCUA_ACCOUNT.MONEY,
            };

            return infRoom;
        }

        public double Money(string userOwner)
        {
            var result = db.ww_BAUCUA_ACCOUNT.Find(userOwner).MONEY;
            return result;
        }

        public void LogOut(string user)
        {
            var account = db.ww_BAUCUA_ACCOUNT.Find(user);
            account.AC_STATUS = false;
            db.SaveChanges();
        }

        public void Dice(int roomId,int dice01, int dice02, int dice03, int deer, int gourd, int chicken, int fish, int crab, int shrimp)
        {
            var room = db.ww_BAUCUA_ROOM.Find(roomId);
            room.DICE1 = dice01;
            room.DICE2 = dice02;
            room.DICE3 = dice03;
            db.SaveChanges();

            var listPlayer = db.ww_BAUCUA_PLAYER.Where(t => t.ww_BAUCUA_ROOM.ROOMID == roomId);
            foreach(var item in listPlayer)
            {
                item.READY = false;
                item.DICE = true;
            }
            db.SaveChanges();

            var listPlayerDetail_Deer = db.ww_BAUCUA_PLAYERDETAIL.Where(t => t.ANIMALID == 1 && t.ww_BAUCUA_PLAYER.ROOMID == roomId);
            var listPlayerDetail_Gourd = db.ww_BAUCUA_PLAYERDETAIL.Where(t => t.ANIMALID == 2 && t.ww_BAUCUA_PLAYER.ROOMID == roomId);
            var listPlayerDetail_Chicken = db.ww_BAUCUA_PLAYERDETAIL.Where(t => t.ANIMALID == 3 && t.ww_BAUCUA_PLAYER.ROOMID == roomId);
            var listPlayerDetail_Fish = db.ww_BAUCUA_PLAYERDETAIL.Where(t => t.ANIMALID == 4 && t.ww_BAUCUA_PLAYER.ROOMID == roomId);
            var listPlayerDetail_Crab = db.ww_BAUCUA_PLAYERDETAIL.Where(t => t.ANIMALID == 5 && t.ww_BAUCUA_PLAYER.ROOMID == roomId);
            var listPlayerDetail_Shrimp = db.ww_BAUCUA_PLAYERDETAIL.Where(t => t.ANIMALID == 6 && t.ww_BAUCUA_PLAYER.ROOMID == roomId);

            foreach (var item in listPlayerDetail_Deer)
            {
                if(deer == 0)
                {
                    room.ww_BAUCUA_ACCOUNT.MONEY += item.BETS;
                    item.BETS = 0;
                }
                else
                {
                    room.ww_BAUCUA_ACCOUNT.MONEY -= item.BETS *(deer+1);
                    item.BETS += item.BETS * (deer + 1);
                }
            }
            db.SaveChanges();

            foreach (var item in listPlayerDetail_Gourd)
            {
                if (gourd == 0)
                {
                    room.ww_BAUCUA_ACCOUNT.MONEY += item.BETS;
                    item.BETS = 0;
                }
                else
                {
                    room.ww_BAUCUA_ACCOUNT.MONEY -= item.BETS * (deer + 1);
                    item.BETS += item.BETS * (deer + 1);
                }
            }
            db.SaveChanges();

            foreach (var item in listPlayerDetail_Chicken)
            {
                if (chicken == 0)
                {
                    room.ww_BAUCUA_ACCOUNT.MONEY += item.BETS;
                    item.BETS = 0;
                }
                else
                {
                    room.ww_BAUCUA_ACCOUNT.MONEY -= item.BETS * (deer + 1);
                    item.BETS += item.BETS * (deer + 1);
                }
            }
            db.SaveChanges();

            foreach (var item in listPlayerDetail_Fish)
            {
                if (fish == 0)
                {
                    room.ww_BAUCUA_ACCOUNT.MONEY += item.BETS;
                    item.BETS = 0;
                }
                else
                {
                    room.ww_BAUCUA_ACCOUNT.MONEY -= item.BETS * (deer + 1);
                    item.BETS += item.BETS * (deer + 1);
                }
            }
            db.SaveChanges();

            foreach (var item in listPlayerDetail_Crab)
            {
                if (crab == 0)
                {
                    room.ww_BAUCUA_ACCOUNT.MONEY += item.BETS;
                    item.BETS = 0;
                }
                else
                {
                    room.ww_BAUCUA_ACCOUNT.MONEY -= item.BETS * (deer + 1);
                    item.BETS += item.BETS * (deer + 1);
                }
            }
            db.SaveChanges();

            foreach (var item in listPlayerDetail_Shrimp)
            {
                if (shrimp == 0)
                {
                    room.ww_BAUCUA_ACCOUNT.MONEY += item.BETS;
                    item.BETS = 0;
                }
                else
                {
                    room.ww_BAUCUA_ACCOUNT.MONEY -= item.BETS * (deer + 1);
                    item.BETS += item.BETS * (deer + 1);
                }
            }
            db.SaveChanges();
        }

        public ww_BAUCUA_ROOM TakeDice(int roomId, int playerId)
        {
            var player = db.ww_BAUCUA_PLAYER.Find(playerId);
            player.DICE = false;
            db.SaveChanges();
            var room = db.ww_BAUCUA_ROOM.Find(roomId);

            return room;
        }

        public bool CheckDice(int playerId)
        {
            var room = (bool)db.ww_BAUCUA_PLAYER.Find(playerId).DICE;
            return room;
        }

        public void ReadyPlayer(int playerId, int isReady)
        {
            //0: Not ready || 1: Ready || 2: Continue
            var player = db.ww_BAUCUA_PLAYER.Find(playerId);
            if(isReady == 0)
            {
                player.READY = true;
            }
            else if(isReady == 1)
            {
                player.READY = false;
            }
            
            db.SaveChanges();
        }

        public bool checkReady(int roomId)
        {
            var room = db.ww_BAUCUA_ROOM.Find(roomId);
            var listPlayer = db.ww_BAUCUA_PLAYER.Where(t => t.ww_BAUCUA_ROOM.ROOMID == roomId && t.READY == true);

            int dem = 0;
            foreach(var item in listPlayer)
            {
                dem++;
            }
            if(dem == room.AMOUNTPERSON)
            {
                return true;
            }
            return false;
        }

        public void TakeMoney(int playerId)
        {
            var player = db.ww_BAUCUA_PLAYER.Find(playerId);

            var playerDetail = db.ww_BAUCUA_PLAYERDETAIL.Where(t => t.PLAYERID == playerId);
            foreach (var item in playerDetail)
            {
                player.ww_BAUCUA_ACCOUNT.MONEY += item.BETS;
                item.BETS = 0;
            }
            db.SaveChanges();
        }

        public bool CheckRoom(int roomId)
        {
            var room = db.ww_BAUCUA_ROOM.Find(roomId);
            if(room == null)
            {
                return false;
            }
            return true;
        }

        public ww_BAUCUA_ROOM CheckAmount(int roomId)
        {
            return db.ww_BAUCUA_ROOM.Find(roomId);
        }

        public double CheckMoney(string user)
        {
            return db.ww_BAUCUA_ACCOUNT.Find(user).MONEY;
        }

        public ww_BAUCUA_ACCOUNT CheckUserExist(string user)
        {
            return db.ww_BAUCUA_ACCOUNT.Find(user);
        }

        public void CreateAccount(string user, string pass, string name)
        {
            ww_BAUCUA_ACCOUNT account = new ww_BAUCUA_ACCOUNT()
            {
                USERNAME = user,
                PASS = pass,
                ACCOUNTNAME = name,
                AC_STATUS = false,
                MONEY = 10000,
            };
            db.ww_BAUCUA_ACCOUNT.AddOrUpdate(account);
            db.SaveChanges();
        }
    }
}