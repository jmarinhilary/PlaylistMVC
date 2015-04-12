using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyTunes.Dominio;
using System.Data.Entity;

namespace MyTunes.Repository
{
    public class TrackRepository : Repository<Track, ChinookContext>
    {
        //private ChinookContext _context;

        public TrackRepository(ChinookContext context)
            : base(context)
        {
            //_context = (ChinookContext)context;
        }

        //public IQueryable<Track> Get()
        //{
        //    return _context.Track.AsQueryable();
        //}

        //public void Dispose()
        //{
        //    _context = null;
        //}

       
        //public void Create(Track playList)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Update(Track playList)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
