using CN.UppgiftBE.Web.Repository;
using CN.UppgiftBE.Web.Repository.DB;
using CN.UppgiftBE.Web.Service;
using CN.UppgiftBE.Web.Util;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using CN.UppgiftBE.Web.EntityModels;

namespace CN.UppgiftBE.Web.Tests.RepositoryTests.DataRepositoryTest
{
    [TestClass]
    public class DataRepository_GetArtistList
    {
        private readonly IRestMethods _restMethods;
        private readonly IRestRequestCreator _restRequestCreator;
        private readonly IDbContextFactory _dbContextFactory;
        private readonly IProductService _productService;
        private readonly ICacheManagerService _cacheManagerService;
        private readonly ITokenService _tokenService;


        private readonly IDataRepository _sut;
        public Token Token = new Token { ExpiresIn = 50000, Expires = DateTime.Now.AddHours(8), AccessToken = "ACCESS" };
        public DataRepository_GetArtistList()
        {
           
            _restMethods = A.Fake<IRestMethods>();
             _tokenService = A.Fake<ITokenService>();
            _restRequestCreator = A.Fake<IRestRequestCreator>();
            _dbContextFactory = A.Fake<IDbContextFactory>();
            _productService = A.Fake<IProductService>();
            _cacheManagerService = A.Fake<ICacheManagerService>();
            var mockHttpContext = A.Fake<HttpContextBase>();
            _sut = new DataRepository(mockHttpContext,_restRequestCreator, _restMethods, _productService, _cacheManagerService, _tokenService);
        }

        [TestMethod]
        public void DataRepository_SearchSpotifyList_ShouldReturnArtistList()
        {
            var sr = new SearchArtistResponse
            {
                Artists = new ArtistList()
                {
                    Items = new List<Artist>()
                                {
                                    new Artist(){
                                        Id = "1",
                                        Name = "Artist 1",
                                        Popularity=50
                                    },
                                    new Artist(){
                                        Id = "2",
                                        Name = "Artis 2",
                                        Popularity = 20
                                    },
                                }
                }
            };

            Token Token = new Token { ExpiresIn = 50000, Expires = DateTime.Now.AddHours(8), AccessToken = "ACCESS" };
            A.CallTo(() => _tokenService.GetToken()).Returns(Token);
            A.CallTo(() => _restRequestCreator.CreateRequestSearchArtists(Token)).Returns(new RestRequest("v1/search", Method.GET));
            A.CallTo(() => _restMethods.Get<SearchArtistResponse>(A<IRestRequest>._)).Returns(sr);
            
            var al = _sut.SearchSpotifyList();

            Assert.IsNotNull(al);
            Assert.AreEqual("1", al.Artists.Items.FirstOrDefault().Id);
        }

        [TestMethod]
        public void DataRepository_GetProductListDB_ShouldReturnProductList()
        {
            A.CallTo(() => _cacheManagerService.Get<List<ProductDB>>("userID")).Returns(
                new List<ProductDB>()
                {
                    new ProductDB{
                        Id = "1",
                        Name = "prod 1",
                        Price = "10"
                    },
                    new ProductDB{
                        Id = "2",
                        Name = "prod 2",
                        Price = "12"
                    }

                });

            A.CallTo(() => _productService.RemoveAllProducts()).Returns(1);
            A.CallTo(() => _productService.AddProduct(A<List<EntityModels.Product>>._));
            A.CallTo(() => _productService.AddProduct(A<List<EntityModels.Product>>._));
            A.CallTo(() => _productService.GetProducts()).Returns(
                new List<ProductDB>()
                {
                    new ProductDB{
                        Id = "1",
                        Name = "prod 1",
                        Price = "10"
                    },
                    new ProductDB{
                        Id = "2",
                        Name = "prod 2",
                        Price = "12"
                    }

                });

            A.CallTo(() => _cacheManagerService.Add("", A<List<EntityModels.Product>>._, A<DateTime>._));
            var pl=  _sut.GetProductListDB();

            Assert.IsNotNull(pl);
            Assert.AreEqual("1", pl.FirstOrDefault().Id);
        }
    }
}
