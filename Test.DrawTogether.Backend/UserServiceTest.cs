using DrawTogether.Backend;
using DrawTogether.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DrawTogether.Backend
{
    [TestFixture]
    public class UserServiceTest
    {
        IUserService service;
        ServiceFactory factory;

        [SetUp]
        public void SetUp()
        {
            this.factory = new ServiceFactory();
            this.service = this.factory.CreateUserService(new Callback(this));
        }

        [TearDown]
        public void TearDown()
        {
            this.factory.Dispose();
        }

        [Test]
        public void TestRegisterUser()
        {
            var user = this.service.RegisterUser("user1");

            Assert.That(user, Is.Not.EqualTo(null));
            Assert.That(user.Name, Is.EqualTo("user1"));
        }

        [Test]
        public void TestUserIdUniqueness()
        {
            var user1 = this.service.RegisterUser("user1");
            var user2 = this.service.RegisterUser("user2");
            var user3 = this.service.RegisterUser("user3");

            Assert.That(user1.Id, Is.Not.EqualTo(user2.Id));
            Assert.That(user1.Id, Is.Not.EqualTo(user3.Id));
            Assert.That(user2.Id, Is.Not.EqualTo(user3.Id));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRegisterUserNameCheck()
        {
            this.service.RegisterUser("U1");
            this.service.RegisterUser("U1");
        }

        [Test]
        public void TestLogoffUser()
        {
            var user1 = this.service.RegisterUser("U1");

            Assert.That(this.service.LogoffUser(user1.Id), Is.True);
            Assert.That(this.service.LogoffUser(user1.Id), Is.False);
        }

        [Test]
        public void TestReRegisterUser()
        {
            var user1 = this.service.RegisterUser("U1");
            Assert.That(this.service.LogoffUser(user1.Id), Is.True);

            user1 = this.service.RegisterUser("U1");
            Assert.That(this.service.LogoffUser(user1.Id), Is.True);
        }

        class Callback : IUserServiceCallback
        {
            readonly UserServiceTest owner;

            public Callback(UserServiceTest owner)
            {
                this.owner = owner;
            }
        }
    }
}
