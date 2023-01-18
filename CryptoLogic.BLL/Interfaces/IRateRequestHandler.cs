﻿using Repository.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWidget.BLL.Interfaces
{
    public interface IRateRequestHandler
    {
        Task<List<SingleRate>> GetCurrentRates();
    }
}
