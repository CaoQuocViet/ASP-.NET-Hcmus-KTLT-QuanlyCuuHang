﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Repo
{
    public interface ILT_DonXuat : IRepository<DonXuat>
    {
        public void CapNhatDS(MatHang mathangOld, MatHang mathangNew);
    }
}