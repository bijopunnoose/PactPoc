using PactNet;

namespace PactPublisherApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var pactPublisher = new PactPublisher("http://104.214.219.231/");

            pactPublisher.PublishToBroker(
                "..\\..\\..\\..\\FixtureServicePactTest\\Consumer.Tests\\pacts\\eventapiconsumer-operationservices.json", "1.1.1.1");
        }
    }
}
