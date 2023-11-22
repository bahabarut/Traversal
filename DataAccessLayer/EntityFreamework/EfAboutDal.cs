﻿using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFreamework
{
    public class EfAboutDal : GenericRepository<About>, IAboutDal
    {
        public About GetAbout()
        {
            using (Context c = new Context())
            {
                return c.Abouts.FirstOrDefault();
            }
        }
    }
}
