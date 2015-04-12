using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MyTunes.Common.ViewModels;
using MyTunes.Repository;
using MyTunes.Services;
using NUnit.Framework;
using Rhino.Mocks;
using MyTunes.Dominio;
using MyTunes.Services.Entities;

//Nunit, FluentAssertions

namespace MyTunes.Test.Services
{
    [TestFixture]
    public class PlaylistServiceTest
    {
        private IRepository<Playlist> playlistRepositoryStub;
        private IRepository<Customer> customerRepositoryStub;
        private IRepository<Track> trackRepositoryStub;
        private PlayListService playlistService;

        [SetUp]
        public void Setup()
        {
            playlistRepositoryStub = MockRepository.GenerateMock<IRepository<Playlist>>();
            customerRepositoryStub = MockRepository.GenerateMock<IRepository<Customer>>();
            trackRepositoryStub = MockRepository.GenerateMock<IRepository<Track>>();
            playlistService = new PlayListService(playlistRepositoryStub, customerRepositoryStub, trackRepositoryStub);
        }

        [TearDown]
        public void TearDown()
        {
            playlistRepositoryStub = null;
            customerRepositoryStub = null;
            trackRepositoryStub = null;
            playlistService = null;
        }

        [Test]
        public void GetPlayList_DadoUsuarioInvalido_DevuelveException()
        {
            try
            {
                customerRepositoryStub.Stub(m => m.Get()).Return(new EnumerableQuery<Customer>(new List<Customer>()));
                var playlists = playlistService.GetPlayLists("invaliduser@example.com");
                Assert.Fail("Deberia lanzar el InvalidOperation Exception"); //
            }
            catch (InvalidOperationException e)
            {
                Assert.AreEqual("Cliente no encontrado invaliduser@example.com", e.Message);
            }
            catch (Exception e)
            {
                Assert.Fail("Deberia lanzar el InvalidOperation Exception");
            }
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException),
            ExpectedMessage = "Cliente no encontrado invaliduser@example.com")]
        public void GetPlayList_DadoUsuarioInvalido_DevuelveException_Ver2()
        {
            customerRepositoryStub.Stub(m => m.Get()).Return(new EnumerableQuery<Customer>(new List<Customer>()));
            var playlists = playlistService.GetPlayLists("invaliduser@example.com");
        }

        [Test]
        public void GetPlayList_DadoUsuarioValidoSinPlaylist_DevuelvePlayListVacio()
        {
            customerRepositoryStub.Stub(m => m.Get()).Return(new EnumerableQuery<Customer>(new List<Customer>
            {
                new Customer
                {
                    Email = "jmarinhilary@gmail.com"
                }
            }));
            playlistRepositoryStub.Stub(m => m.Get()).Return(new EnumerableQuery<Playlist>(new List<Playlist>()));
            var playlists = playlistService.GetPlayLists("jmarinhilary@gmail.com");
            //Assert.IsEmpty(playlists,"Deberia ser un playlist vacio");  solo cuando usas el NUNIT
            playlists.Should().BeEmpty();
        }

        [Test]
        public void GetPlayList_DadoUsuarioValidoSinPlaylist_DevuelvePlayList()
        {
            customerRepositoryStub.Stub(m => m.Get()).Return(new EnumerableQuery<Customer>(new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    Email = "jmarinhilary@gmail.com"
                }
            }));
            playlistRepositoryStub.Stub(m => m.Get()).Return(new EnumerableQuery<Playlist>(new List<Playlist>()
            {
                new Playlist() {CustomerId = 1, Id = 1, Name = "Soft Rock"},
                new Playlist() {CustomerId = 2, Id = 1, Name = "Hard Rock"},
                new Playlist() {CustomerId = 1, Id = 1, Name = "Grunge"}
            }));
            var playlists = playlistService.GetPlayLists("jmarinhilary@gmail.com");
            playlists.Should().NotBeEmpty("Deberia ser un playlist vacio");
            playlists.Count().Should().Be(2, "Deberia tener 2 elementos");
        }

        [Test]
        public void Get_DadoPlaylistIdInvalidoDevuelveExcepcion()
        {
            try
            {
                playlistRepositoryStub.Stub(m => m.Get()).Return(new EnumerableQuery<Playlist>(new List<Playlist>()));
                var playlists = playlistService.Get(3);
                Assert.Fail("Deberia lanzar el InvalidOperationException");
            }
            catch (InvalidOperationException e)
            {
                Assert.AreEqual("Playlist no encontrado", e.Message);
            }
            catch (Exception e)
            {
                Assert.Fail("Deberia lanzar el InvalidOperation Exception");
            }
        }

        [Test]
        public void Get_DevuelvePlaylist()
        {
            playlistRepositoryStub.Stub(m => m.Get()).Return(new EnumerableQuery<Playlist>(new List<Playlist>()
            {
                new Playlist()
                {
                    CustomerId = 2,
                    Name = "Soft",
                    Id = 1,
                    Track = new List<Track>()
                    {
                        new Track()
                        {
                            Name = "Pop", 
                            AlbumId = 1, 
                            UnitPrice = 25, 
                            Album = new Album()
                            {
                                Title = "Titulo",
                                Artist = new Artist()
                                {
                                    Name = "Artista"
                                }
                            }
                        }
                    }
                }
            }));
            var results = playlistService.Get(1);
            results.Should().NotBeNull();
        }

        [Test]
        public void Create_Playlist()
        {
            customerRepositoryStub.Stub(m => m.Get()).Return(new EnumerableQuery<Customer>(new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    Email = "jmarinhilary@gmail.com"
                }
            }));
            PlaylistCreateViewModel model = new PlaylistCreateViewModel();
            model.CustomerUserName = "jmarinhilary@gmail.com";
            model.Nombre = "MyPlaylist ACDC";
            playlistService.Create(model);
        }
    }
}
