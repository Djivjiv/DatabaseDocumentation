﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultation.Domain
{
    public class Admin
    {
        [Key]
        public int AdminID {  get; set; }

        [ForeignKey(nameof(UserID))]

        public string UserID { get; set; }
        public Users Users { get; set; }
    }
}
