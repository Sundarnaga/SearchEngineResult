using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using NUnit.Framework;
using System.Linq;
using SearchEngineResults.Api.Logics;
using Microsoft.Extensions.Configuration;
using SearchEngineResults.Infra;
using SearchEngineResults.Models;
using Newtonsoft.Json;
using System.IO;

namespace SearchEngineResults.Test
{
    public class SearchEngineLogicTest
    {        
        private SearchEngineLogic objSearchEngineLogic;
        private Mock<ISearchEngineLogic> objSearchEngineMock;
        //private Mock<IConfiguration> objConfMock;
        private Mock<ILoggerManager> objLogMock;

        [SetUp]
        public void Setup()
        {           
            objLogMock = new Mock<ILoggerManager>();
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
            objSearchEngineLogic = new SearchEngineLogic(configuration, objLogMock.Object);
            objSearchEngineMock = new Mock<ISearchEngineLogic>();            
        }

        [Test]
        public void SearchEngineExistsTest()
        {
            List<string> engineList = new List<string>();
            engineList = objSearchEngineLogic.GetSearchEngine();
            Assert.IsNotNull(engineList);
        }
      
        [Test]
        public void SearchEngineValueTest()
        {
            List<string> mockList = new List<string>()
            {
                "Google",
                "Bing"
            };
            objSearchEngineMock.Setup(m => m.GetSearchEngine()).Returns(mockList);
            List<string> engineList = new List<string>();
            engineList = objSearchEngineLogic.GetSearchEngine();
            Assert.AreEqual(engineList[0], mockList[0]);
            Assert.AreEqual(engineList[1], mockList[1]);
        }
    }
}