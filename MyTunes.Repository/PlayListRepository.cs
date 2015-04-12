using System;
using System.Collections.Generic;
using System.Linq;
using MyTunes.Dominio;
using System.Data.Entity;

namespace MyTunes.Repository
{
    public class PlayListRepository : Repository<Playlist, ChinookContext>
    {
        //private ChinookContext _context;

        public PlayListRepository(ChinookContext context)
            : base(context)
        {
            //_context = (ChinookContext) context;
        }
            
        //public IQueryable<Playlist> Get()
        //{
        //    return _context.Playlist.AsQueryable();
        //}

        //public void Dispose()
        //{
        //    _context = null;
        //}

        //public void Create(Playlist playList)
        //{
        //    _context.Playlist.Add(playList);
        //    _context.SaveChanges();
        //}

        //public void Update(Playlist playList)
        //{
        //    _context.SaveChanges();
        //}
    }
}
