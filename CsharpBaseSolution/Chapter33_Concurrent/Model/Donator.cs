﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter33_Concurrent.Model
{

    public class Donator
    {
        public int DonatorID { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime DonateDate { get; set; }
        public int DonateTime { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }

}
