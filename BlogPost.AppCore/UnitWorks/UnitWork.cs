﻿using BlogPost.AppCore.Repo;
using BlogPost.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogPost.AppCore.UnitWorks
{
    public class UnitWork : IUnitWork
    {
        private readonly ApplicationContext _context;
        public IPostRepo Posts { get; private set; }

        public UnitWork(ApplicationContext context)
        {
            _context = context;

            Posts = new PostRepo(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
