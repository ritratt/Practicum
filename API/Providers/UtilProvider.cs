using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Models;

namespace API.Providers
{
    public class UtilProvider : IUtilProvider
    {
        public List<SwipeColumns> SwipeColumninator(IQueryable<DoorSwipe> rawSwipes)
        {
            return rawSwipes
                .Select(d => new SwipeColumns
                {
                    CustId = d.C_CUST_ID_,
                    DoorAccessPostDateTime = d.C_DoorAccessPostDateTime_,
                    TransactionId = d.C_TRAN_ID_,
                    ActualDateTime = d.C_ACTUALDATETIME_,
                    PostDateTime = d.C_POSTDATETIME_,
                    DoorId = d.C_DOOR_ID_
                })
                .ToList();
        }
    }

    public class SwipeColumns
    {
        public String CustId { get; set; }

        public String DoorAccessPostDateTime { get; set; }

        public String TransactionId { get; set; }

        public String ActualDateTime { get; set; }

        public String PostDateTime { get; set; }

        public String DoorId { get; set; }
    }
}