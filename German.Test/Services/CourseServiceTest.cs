using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using German.Application.Services;
using German.Core.Interfaces;
using German.Core.Entities;
using Moq;
using Xunit;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace German.Test.Services
{
   public class CourseServiceTest
    {
        private Mock<ILogger<CourseService>> logger;
        private  Mock<IAppDbContext> _appDbContextMock;
        private  ICourseService _courseServiceMock;
        private readonly Mapper mapper;

        public CourseServiceTest() {
            this._appDbContextMock= new Mock<IAppDbContext>();
            this.logger = new Mock<ILogger<CourseService>>();
            this._courseServiceMock = new CourseService(this.logger.Object, this._appDbContextMock.Object);
            this.mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Course, Course>()));
        }
        //report, the test didnt run until I added xunit as a package. [Fact] only added
        [Fact]
        public async Task ShouldReturnAllCourses()
        {
            //arrange
            List<Course> database = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name = "Course 1"
                },
                new Course
                {
                    Id = 2,
                    Name = "Course 2"
                }
            };

            this._appDbContextMock.Setup(db => 
                db.SelectAllCoursesAsync())
                .ReturnsAsync(database);

            //act
            List<Course> actualCourses = await _courseServiceMock.GetCoursesAsync();


            //assert
            //db should be called once.
            //returns a list of courses
            

            database.Should().NotBeEmpty();
            actualCourses.Should().BeEquivalentTo(database);
          
            _appDbContextMock.Verify(db => db.SelectAllCoursesAsync(), Times.Once);
            _appDbContextMock.VerifyNoOtherCalls();
            logger.VerifyNoOtherCalls();



        }

        [Fact]
        public async Task ShouldReturnSingleCourse()
        {
            //given
            Course expectedCourse = new()
            {
                Id = 1,
                Name = "Introduction to Remote Sensing"
            };
            //clone course
            Course databaseCourse = this.mapper.Map<Course>(expectedCourse);

            this._appDbContextMock.Setup(db => 
                db.SelectCourseByIdAsync(databaseCourse.Id))
                    .ReturnsAsync(databaseCourse);
            //when
            Course actualCourse = await _courseServiceMock.GetCourseAsync(databaseCourse.Id);

            //assert
            //1. Actual Course == Expected Course
            // Db hit once
            //logger was not hit

            actualCourse.Should().BeEquivalentTo(expectedCourse);
            _appDbContextMock.Verify(db => db.SelectCourseByIdAsync(databaseCourse.Id), Times.Once);
            _appDbContextMock.VerifyNoOtherCalls();
            logger.VerifyNoOtherCalls();
            //I found out that the order of the code matters too, wasted about an hour trying to debug
            //I initslly put the verifyNoother calls before the hit once.

        }

        [Fact]

        public async Task ShouldThrowApplicationExceptionErrorIfInvalidId()
        {
            //given
         
            int invalidId = 100;
            Course invalidCourse = null;
            //clone course

            this._appDbContextMock.Setup(db =>
                db.SelectCourseByIdAsync(invalidId))
                    .ReturnsAsync(invalidCourse);
            //when
            var actualCourse =  _courseServiceMock.GetCourseAsync(invalidId);

            //assert
            //1. Actual Course == Expected Course
            // Db hit once
            //logger was not hit

            await Assert.ThrowsAsync<ApplicationException>(() => actualCourse); //why I had to make actualCourse sync, I dont know
            _appDbContextMock.Verify(db => db.SelectCourseByIdAsync(invalidId), Times.Once);
            _appDbContextMock.VerifyNoOtherCalls();
            logger.VerifyNoOtherCalls();
        }
    }
}
