﻿using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Repositories
{
    public interface IBrandRepository
    {
        Brand Get(int id);

        void Add(Brand brand);

        void Update(Brand brand);

        void Delete(Brand brand);

        IQueryable<Brand> Query();
    }
}
