using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sach.Models;

namespace Sach.Models.Bus
{
    public class Books
    {
        public IEnumerable<NHAXUATBAN> loadNXB()
        {
            QLBANSACHEntities db = new QLBANSACHEntities();
            var result = db.NHAXUATBANs.ToList();
            return result;
        }
        public IEnumerable<CHUDE> loadChuDe()
        {
            QLBANSACHEntities db = new QLBANSACHEntities();
            var result = db.CHUDEs.ToList();
            return result;
        }
        public IEnumerable<SACH> loadSach()
        {
            QLBANSACHEntities db = new QLBANSACHEntities();
            var result = db.SACHes.ToList();
            return result;
        }
    }
}