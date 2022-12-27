using Autofac.Extras.Moq;
using AutoMapper;
using FirstDemo.Infrastructure.Exceptions;
using FirstDemo.Infrastructure.Repositories;
using FirstDemo.Infrastructure.Services;
using FirstDemo.Infrastructure.UnitOfWorks;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Linq.Expressions;
using CourseBO = FirstDemo.Infrastructure.BusinessObjects.Course;
using CourseEO = FirstDemo.Infrastructure.Entities.Course;

namespace FirstDemo.Infrastructure.Tests
{
    public class CourseServiceTests
    {
        private AutoMock _mock;
        private Mock<IApplicationUnitOfWork> _applicationtUnitOfWork;
        private Mock<ICourseRepository> _courseRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private ICourseService _courseService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [SetUp]
        public void Setup()
        {
            _applicationtUnitOfWork = _mock.Mock<IApplicationUnitOfWork>();
            _courseRepositoryMock = _mock.Mock<ICourseRepository>();
            _mapperMock = _mock.Mock<IMapper>();
            _courseService = _mock.Create<CourseService>();
        }

        [TearDown]
        public void TearDown()
        {
            _applicationtUnitOfWork.Reset();
            _courseRepositoryMock.Reset();
            _mapperMock.Reset();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock?.Dispose();
        }

        [Test]
        public void CreateCourse_CourseDoesNotExists_CreatesCourse()
        {
            // Arrange
            CourseBO course = new CourseBO
            {
                Name = "ABC"
            };

            CourseEO courseEntity = new CourseEO
            {
                Title = "ABC"
            };

            _applicationtUnitOfWork.Setup(x => x.Courses)
                .Returns(_courseRepositoryMock.Object);

            _courseRepositoryMock.Setup(x => x.GetCount(
                It.Is<Expression<Func<CourseEO, bool>>>(y => y.Compile()(courseEntity))))
                .Returns(0).Verifiable();

            _mapperMock.Setup(x => x.Map<CourseEO>(course))
                .Returns(courseEntity).Verifiable();

            _applicationtUnitOfWork.Setup(x => x.Save()).Verifiable();
            _courseRepositoryMock.Setup(x => x.Add(It.Is<CourseEO>(y => y.Title == course.Name)))
                .Verifiable();

            // Act
            _courseService.CreateCourse(course);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => _applicationtUnitOfWork.VerifyAll(),
                () => _courseRepositoryMock.VerifyAll()
            );

        }

        [Test]
        public void CreateCourse_CourseExists_ThrowsError()
        {
            // Arrange
            CourseBO course = new CourseBO
            {
                Name = "ABC"
            };

            _applicationtUnitOfWork.Setup(x => x.Courses)
                .Returns(_courseRepositoryMock.Object);

            _courseRepositoryMock.Setup(x => x.GetCount(
                It.IsAny<Expression<Func<CourseEO, bool>>>())).Returns(1);

            _mapperMock.Setup(x => x.Map<CourseEO>(course))
                .Returns(new CourseEO() { Title= course.Name });

            _applicationtUnitOfWork.Setup(x => x.Save()).Verifiable();
            _courseRepositoryMock.Setup(x => x.Add(It.Is<CourseEO>(y => y.Title == course.Name)))
                .Verifiable();

            // Act
            Should.Throw<DuplicateException>(
                () => _courseService.CreateCourse(course)
            );
        }

        [Test]
        public void GetCourse_ValidId_ReturnsCourse()
        {
            // Arrange
            var id = new Guid("A5DDE200-5C9B-46E1-9E55-16FD0BF7C721");

            CourseEO courseEntity = new CourseEO
            {
                Id = id
            };

            CourseBO course = new CourseBO() { Id = courseEntity.Id };

            _applicationtUnitOfWork.Setup(x => x.Courses)
                .Returns(_courseRepositoryMock.Object);

            _courseRepositoryMock.Setup(x => x.GetById(id)).Returns(courseEntity);

            _mapperMock.Setup(x => x.Map<CourseBO>(courseEntity))
                .Returns(course);

            // Act
            var result = _courseService.GetCourse(id);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => result.ShouldNotBeNull(),
                () => result.Id.ShouldBe(id)
            );
        }
    }
}