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

        public double DateConverter(string dirtyDateTime)
        {
            var splitDirtyDateTime = dirtyDateTime.Split(' ');
            var parsedDate = splitDirtyDateTime[0];
            var parsedTime = splitDirtyDateTime[1].Replace('-', ':');
            var parsedDateTime = DateTime.Parse(parsedDate + " " + parsedTime);
            var baseDateTime = new DateTime(1900, 01, 01, 00, 00, 00);
            var diff = parsedDateTime - baseDateTime;
            var diffAsdays = diff.TotalDays;
            return diffAsdays;
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