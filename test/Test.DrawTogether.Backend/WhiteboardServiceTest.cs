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
    public class WhiteboardServiceTest
    {
        IWhiteboardService service;
        IUserService userService;
        ServiceFactory factory;
        dynamic callbackArgs;

        [SetUp]
        public void SetUp()
        {
            this.factory = new ServiceFactory();
            this.service = this.factory.CreateWhiteboardService(new Callback(this));
            this.userService = this.factory.CreateUserService(new UserCallback());
        }

        [TearDown]
        public void TearDown()
        {
            this.callbackArgs = null;
            this.factory.Dispose();
        }

        [Test]
        public void TestCreate()
        {
            var whiteboard = this.service.Create("Hello");

            Assert.That(whiteboard, Is.Not.Null);
            Assert.That(callbackArgs, Is.Not.Null);
            Assert.That(whiteboard.Id, Is.EqualTo(callbackArgs.Id));
            Assert.That(whiteboard.Name, Is.EqualTo(callbackArgs.Name));
            Assert.That(whiteboard.Name, Is.EqualTo("Hello"));
            Assert.That(whiteboard.AttachedUsers, Is.Empty);
            Assert.That(whiteboard.Figures, Is.Empty);
        }

        [Test]
        public void TestWhiteboardIdUniqueness()
        {
            var whiteboard1 = this.service.Create("W1");
            var whiteboard2 = this.service.Create("W2");
            var whiteboard3 = this.service.Create("W3");

            Assert.That(whiteboard1.Id, Is.Not.EqualTo(whiteboard2.Id));
            Assert.That(whiteboard1.Id, Is.Not.EqualTo(whiteboard3.Id));
            Assert.That(whiteboard2.Id, Is.Not.EqualTo(whiteboard3.Id));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWhiteboardNameCheck()
        {
            var whiteboard1 = this.service.Create("W1");
            this.service.Create("W1");
        }

        [Test]
        public void TestAddFigure()
        {
            var whiteboard1 = this.service.Create("W1");
            var user = this.userService.RegisterUser("user");
            var figure = FigureContract.FromPolygon(user.Id, Argb.FromArgb(255, 255, 0, 0),
                new[] { new VertexContract(0, 0), new VertexContract(100, 100) });
            this.service.AddFigure(whiteboard1.Id, figure);

            figure = callbackArgs.Figure;
            Assert.That(figure, Is.Not.Null);
            Assert.That(figure.UserId, Is.EqualTo(user.Id));
            Assert.That(figure.Argb, Is.EqualTo(Argb.FromArgb(255, 255, 0, 0)));

            var vertices = figure.Vertices;
            Assert.That(vertices, Is.EqualTo(
                new[] { new VertexContract(0, 0), new VertexContract(100, 100) }));
        }

        [Test]
        public void TestRemoveFigure()
        {
            var whiteboard1 = this.service.Create("W1");
            var user = this.userService.RegisterUser("user");
            var figure = FigureContract.FromPolygon(user.Id, Argb.FromArgb(255, 255, 0, 0),
                new[] { new VertexContract(0, 0), new VertexContract(100, 100) });
            this.service.AddFigure(whiteboard1.Id, figure);
            this.service.RemoveFigure(whiteboard1.Id, 0);

            Assert.That(callbackArgs.Id, Is.EqualTo(whiteboard1.Id));
            Assert.That(callbackArgs.FigureIndex, Is.EqualTo(0));

            var whiteboard2 = this.service.Get(whiteboard1.Id);

            Assert.That(whiteboard2.Figures, Is.Empty);
        }

        [Test]
        public void TestDelete()
        {
            var whiteboard1 = this.service.Create("W1");
            var result = this.service.Delete(whiteboard1.Id);
            Assert.That(result, Is.True);
            Assert.That(this.callbackArgs, Is.EqualTo(whiteboard1.Id));
            Assert.That(this.service.Get(whiteboard1.Id), Is.Null);
        }

        [Test]
        public void TestGet()
        {
            var whiteboard1 = this.service.Create("W1");
            var whiteboard2 = this.service.Get(whiteboard1.Id);

            Assert.That(whiteboard2.Id, Is.EqualTo(whiteboard1.Id));
            Assert.That(whiteboard2.Name, Is.EqualTo(whiteboard2.Name));
        }

        [Test]
        public void TestGetByName()
        {
            var whiteboard1 = this.service.Create("W1");
            var whiteboard2 = this.service.GetByName("W1");

            Assert.That(whiteboard2.Id, Is.EqualTo(whiteboard1.Id));
            Assert.That(whiteboard2.Name, Is.EqualTo(whiteboard2.Name));

            var whiteboard3 = this.service.GetByName("no_whiteboard_with_this_name");

            Assert.That(whiteboard3, Is.EqualTo(null));
        }

        [Test]
        public void TestAttachUser()
        {
            var whiteboard = this.service.Create("W1");
            var user = this.userService.RegisterUser("user");

            this.service.AttachUser(whiteboard.Id, user.Id);

            Assert.That(this.callbackArgs.Id, Is.EqualTo(whiteboard.Id));
            Assert.That(this.callbackArgs.User.Id, Is.EqualTo(user.Id));
            Assert.That(this.callbackArgs.User.Name, Is.EqualTo(user.Name));
        }

        class Callback : IWhiteboardServiceCallback
        {
            readonly WhiteboardServiceTest owner;

            public Callback(WhiteboardServiceTest owner)
            {
                this.owner = owner;
            }

            public void NotifyWhiteboardCreated(WhiteboardContract whiteboard)
            {
                owner.callbackArgs = whiteboard;
            }

            public void NotifyUserAttached(int id, UserContract user)
            {
                owner.callbackArgs = new { Id = id, User = user };
            }

            public void NotifyUserDetached(int id, int userId)
            {
                owner.callbackArgs = new { Id = id, UserId = userId };
            }

            public void NotifyFigureAdded(int id, FigureContract figure)
            {
                owner.callbackArgs = new { Id = id, Figure = figure };
            }

            public void NotifyFigureRemoved(int id, int figureIndex)
            {
                owner.callbackArgs = new { Id = id, FigureIndex = figureIndex };
            }

            public void NotifyWhiteboardDeleted(int id)
            {
                owner.callbackArgs = id;
            }
        }

        class UserCallback : IUserServiceCallback
        {
        }
    }
}
