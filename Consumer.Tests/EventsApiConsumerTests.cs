using FixtureServiceConsumer;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using PactNet.Matchers;
using System;
using System.Collections.Generic;
using Xunit;

namespace Consumer.Tests
{
    public class EventsApiConsumerTests : IClassFixture<ConsumerEventApiPact>
    {
        private readonly IMockProviderService _mockProviderService;
        private readonly string _mockProviderServiceBaseUri;

        public EventsApiConsumerTests(ConsumerEventApiPact data)
        {
            _mockProviderService = data.MockProviderService;
            _mockProviderServiceBaseUri = data.MockProviderServiceBaseUri;
            _mockProviderService.ClearInteractions();
        }

        //[Fact]
        //public void GetAllEvents_WithNoAuthorizationToken_ShouldFail()
        //{
        //    //Arrange
        //    var fixtureId = Guid.NewGuid();
        //    _mockProviderService.Given("I call fixture service using a valid fixture Id 12")
        //        .UponReceiving("a request to retrieve fixture id events with no authorization")
        //        .With(new ProviderServiceRequest
        //        {
        //            Method = HttpVerb.Get,
        //            Path = $"/fixtures/{fixtureId}",
        //            Headers = new Dictionary<string, object>
        //            {
        //                { "Accept", "application/json" },
        //            }
        //        })
        //        .WillRespondWith(new ProviderServiceResponse
        //        {
        //            Status = 401,
        //            Headers = new Dictionary<string, object>
        //            {
        //                { "Content-Type", "application/json; charset=utf-8" }
        //            },
        //            Body = new
        //            {
        //                message = "Authorization has been denied for this request."
        //            }
        //        });

        //    var consumer = new FixtureConsumer(_mockProviderServiceBaseUri);

        //    //Act //Assert
        //    Assert.ThrowsAny<Exception>(() => consumer.GetFixture(fixtureId, "dfgfdgfdgfdgdfgdfgdfgdfg"));

        //    _mockProviderService.VerifyInteractions();
        //}

        [Fact]
        public void BasicfixtureTest()
        {
            //Arrange
            Guid fixtureIdSet = new Guid("713be2bd-36e3-43b8-ae2b-0ddeac06cd9f");
            _mockProviderService.Given("a request to check the api response")
                .UponReceiving("I call fixture service using a valid fixture Id")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = $"/api/v1.0/fixture/{fixtureIdSet}",
                    Headers = new Dictionary<string, object> {
                        { "Accept", "application/json" },
                        { "X-Clarksons-Security-Cloud", "9xoNm1ZZk6zn3uzu2X18xXtRM5MurptRBsWGA4A1zIM+HSZdJDp9aqlRD+oCsNDOL4UPwU5oFNJHa3T/c1FeVG4EBodw/ybiZq8xb4XSPcELNZ3IKbM1d1tvVGBbWi8q7QfxRODngc+yd25V6fW+Lw=="}
                        }
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, object>
                    {
                      { "Content-Type", "application/json" }
                    },
                    Body = new
                    {
                        fixtureId = Match.Type(fixtureIdSet),
                        fixtureNumber = Match.Regex("145393-02-DR-03-18", "[0-9]{6}-[0-9]{2}-[a-zA-Z]{2}-[0-9]{2}-[0-9]{2}"),
                    }
                });

            var consumer = new FixtureConsumer(_mockProviderServiceBaseUri);

            
            var result = consumer.GetFixture(fixtureIdSet, "9xoNm1ZZk6zn3uzu2X18xXtRM5MurptRBsWGA4A1zIM+HSZdJDp9aqlRD+oCsNDOL4UPwU5oFNJHa3T/c1FeVG4EBodw/ybiZq8xb4XSPcELNZ3IKbM1d1tvVGBbWi8q7QfxRODngc+yd25V6fW+Lw==");

            //Assert
            //Assert.Equal(fixtureIdSet, result.FixtureId);

            _mockProviderService.VerifyInteractions();
        }

        [Fact]
        public void CheckNullableFields()
        {
            //Arrange
            Guid fixtureIdSet = new Guid("713be2bd-36e3-43b8-ae2b-0ddeac06cd9f");
            _mockProviderService.Given("a request to check the api response")
                .UponReceiving("I call fixture service using a valid fixture Id to test cargo can be null")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = $"/api/v1.0/fixture/{fixtureIdSet}",
                    Headers = new Dictionary<string, object> {
                        { "Accept", "application/json" },
                        { "X-Clarksons-Security-Cloud", "9xoNm1ZZk6zn3uzu2X18xXtRM5MurptRBsWGA4A1zIM+HSZdJDp9aqlRD+oCsNDOL4UPwU5oFNJHa3T/c1FeVG4EBodw/ybiZq8xb4XSPcELNZ3IKbM1d1tvVGBbWi8q7QfxRODngc+yd25V6fW+Lw=="}
                        }
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, object>
                    {
                      { "Content-Type", "application/json" }
                    },
                    Body = new
                    {
                        cargo = Match.Type(null),
                        broker = new List<dynamic>
                        {
                            new{
                                emailAddress = Match.Type(null),
                                groupEmailAddress=Match.Type(null)
                            }
                        }
                    }
                });

            var consumer = new FixtureConsumer(_mockProviderServiceBaseUri);

            //Act
            var result = consumer.GetFixture(fixtureIdSet, "9xoNm1ZZk6zn3uzu2X18xXtRM5MurptRBsWGA4A1zIM+HSZdJDp9aqlRD+oCsNDOL4UPwU5oFNJHa3T/c1FeVG4EBodw/ybiZq8xb4XSPcELNZ3IKbM1d1tvVGBbWi8q7QfxRODngc+yd25V6fW+Lw==");

            //Assert
            //Assert.Equal(fixtureIdSet, result.FixtureId);

            _mockProviderService.VerifyInteractions();
        }
    }
}
