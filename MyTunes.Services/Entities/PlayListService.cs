using System;
using System.Collections.Generic;
using System.Linq;
using MyTunes.Dominio;
using MyTunes.Repository;
using MyTunes.Common.ViewModels;

namespace MyTunes.Services.Entities
{
    public class PlayListService : IDisposable
    {
        private IRepository<Playlist> _playListRepository;
        private IRepository<Customer> _customerRepository;
        private IRepository<Track> _trackRepository;
        private ChinookContext _chinookContext;


        public PlayListService()
        {
            _chinookContext = new ChinookContext();
            _playListRepository = new PlayListRepository(_chinookContext);
            _customerRepository = new CustomerRepository(_chinookContext);
            _trackRepository = new TrackRepository(_chinookContext);
        }

        public PlayListService(IRepository<Playlist> playlistRepositoryStub,
            IRepository<Customer> customerRepositoryStub, IRepository<Track> trackRepositoryStub)
        {
            // TODO: Complete member initialization
            this._playListRepository = playlistRepositoryStub;
            this._customerRepository = customerRepositoryStub;
            this._trackRepository = trackRepositoryStub;
        }

        public IEnumerable<PlayListViewModel> GetPlayLists(string userName)
        {
            //var customer = _customerRepository.GetByEmail(userName);
            var customer = _customerRepository.Get().FirstOrDefault(x => x.Email == userName);
            if (customer == null)
                throw new InvalidOperationException(string.Format("Cliente no encontrado {0}", userName));
            var playLists = _playListRepository.Get().Where(x => x.CustomerId == customer.Id).ToList(); // PlayList
            // aqui se tiene que hacer un mapeo del dominio al viewmodel
            return playLists.Select(playList => new PlayListViewModel(playList)).ToList();
        }

        public void Dispose()
        {
            _playListRepository = null;
            _customerRepository = null;
            _trackRepository = null;
            _chinookContext = null;
        }

        public void Create(PlaylistCreateViewModel model)
        {
            var customer = _customerRepository.Get().FirstOrDefault(x => x.Email == model.CustomerUserName);
            if (customer == null)
                throw new InvalidOperationException(string.Format("Cliente no encontrado {0}", model.CustomerUserName));
            var playList = new Playlist() {Name = model.Nombre, CustomerId = customer.Id};
            _playListRepository.Create(playList);
        }

        public PlaylistEditViewModel Get(int playlistId)
        {
            var playList = _playListRepository.Get().FirstOrDefault(x => x.Id == playlistId);
            if (playList == null) throw new InvalidOperationException("Playlist no encontrado");
            return new PlaylistEditViewModel()
            {
                Name = playList.Name,
                Id = playList.Id,
                CustomerId = playList.CustomerId,
                Tracks = playList.Track.Select(track => new TracksListViewModel(track, playList.Id)).ToList()
            };
        }

        public IEnumerable<TracksListViewModel> GetTracksFrom(PlaylistSearchTrackViewModel request)
        {
            var tracklist = _trackRepository.Get().Where(x => x.Name.Contains(request.TrackName)).ToList();
            return tracklist.Select(track => new TracksListViewModel(track, request.PlayListId)).ToList();
        }

        public void AddTrack(int playListId, int trackId)
        {
            var playList = _playListRepository.Get().FirstOrDefault(x => x.Id == playListId);
            if (playList == null) throw new InvalidOperationException("Playlist no encontrado");
            var track = _trackRepository.Get().FirstOrDefault(x => x.Id == trackId);
            if (playList == null) throw new InvalidOperationException("Track no encontrado");
            playList.Track.Add(track);
            _playListRepository.Update(playList);
        }
    }
}
