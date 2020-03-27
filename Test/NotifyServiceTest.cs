using Common.Services;
using Common.Tools.Database;
using Common.Tools.WebSite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    [TestClass]
    public class NotifyServiceTest
    {
        string[] inputData = { };

        [TestMethod]
        //有効な通知先情報が1つの場合
        public void DoServiceTest01()
        {
            //Arrange
            List<Account> tokens = new List<Account>
            {
                new Account
                {
                    is_valid = 1,
                    id = "testId",
                    password = "testPwd",
                    access_token = "oA4C4FdNKL9ZB9Uo90XwUJ05vr15Cw2yNA2bhUrbh4h"
                }
            };

            EventInfo eventInfo = new EventInfo
            {
                title = "testTitle",
                artist = "testArtist"
            };

            int expectedListSize = tokens.Count;
            string expectedToken = tokens[0].access_token;
            string expectedMsg = Messages.AM01(eventInfo.title, eventInfo.artist) + Messages.URL;


            IDbAccessor db = new StubDbAccessor(tokens);
            IEventInfoConverter ei = new StubEventInfoConverter(eventInfo);
            MockLineMessenger lm = new MockLineMessenger();

            //Act
            using (NotifyService service = new NotifyService(db, ei, lm))
            {
                service.DoService(inputData);
            }

            //Assert
            int actualListSize = lm.InputList.Count;
            string actualToken = lm.InputList[0].token;
            string actualMsg = lm.InputList[0].message;

            Assert.IsTrue(expectedListSize == actualListSize);
            Assert.AreEqual(expectedToken, actualToken);
            Assert.AreEqual(expectedMsg, actualMsg);
            
        }

        [TestMethod]
        //有効な通知先情報が2つの場合
        public void DoServiceTest02()
        {
            //Arrange
            List<Account> tokens = new List<Account>
            {
                new Account
                {
                    is_valid = 1,
                    id = "testId0",
                    password = "testPwd0",
                    access_token = "testToken0"
                },
                new Account
                {
                    is_valid = 1,
                    id = "testId1",
                    password = "testPwd1",
                    access_token = "testToken1"
                }
            };

            EventInfo eventInfo = new EventInfo
            {
                title = "testTitle",
                artist = "testArtist"
            };

            int expectedListSize = tokens.Count;
            string[] expectedToken = { tokens[0].access_token, tokens[1].access_token };

            string expectedMsg = Messages.AM01(eventInfo.title, eventInfo.artist) + Messages.URL;

            IDbAccessor db = new StubDbAccessor(tokens);
            IEventInfoConverter ei = new StubEventInfoConverter(eventInfo);
            MockLineMessenger lm = new MockLineMessenger();

            //Act
            using (NotifyService service = new NotifyService(db, ei, lm))
            {
                service.DoService(inputData);
            }

            //Assert
            int actualListSize = lm.InputList.Count;
            string[] actualToken = { lm.InputList[0].token, lm.InputList[1].token };
            string[] actualMsg = { lm.InputList[0].message, lm.InputList[1].message };

            Assert.IsTrue(actualListSize == expectedListSize);
            for (int i = 0; i < actualListSize; i++) {
                Assert.AreEqual(expectedToken[i], actualToken[i]);
                Assert.AreEqual(expectedMsg, actualMsg[i]);
            }
        }

        [TestMethod]
        //有効な通知先情報がない場合
        public void DoServiceTest03()
        {
            //Arrange
            List<Account> tokens = new List<Account>();

            EventInfo eventInfo = new EventInfo
            {
                title = "testTitle",
                artist = "testArtist"
            };

            int expectedListSize = tokens.Count;

            IDbAccessor db = new StubDbAccessor(tokens);
            IEventInfoConverter ei = new StubEventInfoConverter(eventInfo);
            MockLineMessenger lm = new MockLineMessenger();

            //Act
            using (NotifyService service = new NotifyService(db, ei, lm))
            {
                service.DoService(inputData);
            }

            //Assert
            int actualListSize = lm.InputList.Count;

            Assert.IsTrue(expectedListSize == actualListSize);
        }


        [TestMethod]
        //有効な通知先情報が100個の場合
        public void DoServiceTest04()
        {
            //Arrange
            int n = 100;

            List<Account> tokens = new List<Account>();

            for (int i = 0; i < n; i++)
            {
                tokens.Add(new Account
                {
                    is_valid = 1,
                    id = "testId0",
                    password = "testPwd0",
                    access_token = "oA4C4FdNKL9ZB9Uo90XwUJ05vr15Cw2yNA2bhUrbh4" + i.ToString()
                }); ;
            }


            EventInfo eventInfo = new EventInfo
            {
                title = "testTitle",
                artist = "testArtist"
            };

            int expectedListSize = tokens.Count;

            string expectedMsg = Messages.AM01(eventInfo.title, eventInfo.artist) + Messages.URL;

            IDbAccessor db = new StubDbAccessor(tokens);
            IEventInfoConverter ei = new StubEventInfoConverter(eventInfo);
            MockLineMessenger lm = new MockLineMessenger();

            //Act
            using (NotifyService service = new NotifyService(db, ei, lm))
            {
                service.DoService(inputData);
            }

            //Assert
            int actualListSize = lm.InputList.Count;

            Assert.IsTrue(actualListSize == expectedListSize);
            for (int i = 0; i < actualListSize; i++)
            {
                Assert.AreEqual(tokens[i].access_token, lm.InputList[i].token);
                Assert.AreEqual(expectedMsg, lm.InputList[i].message);
            }
        }
        [TestMethod]
        //有効な通知先情報=10, うち送信失敗=3
        public void DoServiceTest05()
        {
            //Arrange
            int n = 10;

            List<Account> tokens = new List<Account>();

            for (int i = 0; i < n; i++)
            {
                tokens.Add(new Account
                {
                    is_valid = 1,
                    id = "testId0",
                    password = "testPwd0",
                    access_token = i%3==1 ? "false" : "oA4C4FdNKL9ZB9Uo90XwUJ05vr15Cw2yNA2bhUrbh4"
                }); ;
            }


            EventInfo eventInfo = new EventInfo
            {
                title = "testTitle",
                artist = "testArtist"
            };

            var expectedList = tokens.Where(x => x.access_token != "false").ToList();
            int expectedListSize = expectedList.Count;

            string expectedMsg = Messages.AM01(eventInfo.title, eventInfo.artist) + Messages.URL;

            IDbAccessor db = new StubDbAccessor(tokens);
            IEventInfoConverter ei = new StubEventInfoConverter(eventInfo);
            MockLineMessenger lm = new MockLineMessenger();

            //Act
            using (NotifyService service = new NotifyService(db, ei, lm))
            {
                service.DoService(inputData);
            }

            //Assert
            int actualListSize = lm.InputList.Count;

            Assert.IsTrue(actualListSize == expectedListSize);
            for (int i = 0; i < actualListSize; i++)
            {
                Assert.AreEqual(expectedList[i].access_token, lm.InputList[i].token);
                Assert.AreEqual(expectedMsg, lm.InputList[i].message);
            }
        }
    }
}
