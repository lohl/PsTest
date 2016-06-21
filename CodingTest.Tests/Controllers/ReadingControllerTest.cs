using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodingTest;
using CodingTest.Controllers;
using CodingTest.Models;
using CodingTest.Repositories;
using Moq;

namespace CodingTest.Tests.Controllers
{
    [TestClass]
    public class ReadingControllerTest
    {
        private Mock<DataContext> _mockContext;
        private Mock<IRepositoryFactory> _mockRepositoryFactory;
        private Mock<IRepository<Reading>> _mockMagRepository;
        private Mock<IRepository<ReadingGrav>> _mockGravRepository;
        private ReadingsController _sut;

        [TestInitialize]
        public void Initialize()
        {
            _mockContext = new Mock<DataContext>();
            _mockRepositoryFactory = new Mock<IRepositoryFactory>();
            _mockMagRepository = new Mock<IRepository<Reading>>();
            _mockGravRepository = new Mock<IRepository<ReadingGrav>>();
            _sut = new ReadingsController(_mockContext.Object, _mockRepositoryFactory.Object);
        }
        [TestMethod]
        public void Should_show_dialog_for_completion_of_newly_added_record_by_setting_message()
        {
            // Arrange
            string s = "Hooray!";

            // Act
            ViewResult result = _sut.Index(s) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(s, result.ViewBag.Confirmation);
        }

        [TestMethod]
        public void Should_show_details_screen_for_selected_mag_readings()
        {
            // Arrange
            var model = new Reading();
            _mockRepositoryFactory.Setup(c => c.GetRepository<Reading>()).Returns(_mockMagRepository.Object);
            _mockMagRepository.Setup(c => c.Get(1)).ReturnsAsync(model);
            // Act
            ViewResult result = _sut.Details(1, "Reading").Result as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailsMag", result.ViewName);
            Assert.AreEqual(model, result.Model);
        }

        [TestMethod]
        public void Should_show_details_screen_for_selected_grav_readings()
        {
            // Arrange
            var model = new ReadingGrav();
            _mockRepositoryFactory.Setup(c => c.GetRepository<ReadingGrav>()).Returns(_mockGravRepository.Object);
            _mockGravRepository.Setup(c => c.Get(1)).ReturnsAsync(model);
            // Act
            ViewResult result = _sut.Details(1, "ReadingGrav").Result as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("DetailsGrav", result.ViewName);
            Assert.AreEqual(model, result.Model);
        }

        [TestMethod]
        public void Should_show_edit_screen_for_selected_mag_readings()
        {
            // Arrange
            var model = new Reading();
            _mockRepositoryFactory.Setup(c => c.GetRepository<Reading>()).Returns(_mockMagRepository.Object);
            _mockMagRepository.Setup(c => c.Get(1)).ReturnsAsync(model);
            // Act
            ViewResult result = _sut.Edit(1, "Reading").Result as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("EditMag", result.ViewName);
            Assert.AreEqual(model, result.Model);
        }

        [TestMethod]
        public void Should_show_edit_screen_for_selected_grav_readings()
        {
            // Arrange
            var model = new ReadingGrav();
            _mockRepositoryFactory.Setup(c => c.GetRepository<ReadingGrav>()).Returns(_mockGravRepository.Object);
            _mockGravRepository.Setup(c => c.Get(1)).ReturnsAsync(model);
            // Act
            ViewResult result = _sut.Edit(1, "ReadingGrav").Result as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("EditGrav", result.ViewName);
            Assert.AreEqual(model, result.Model);
        }

        [TestMethod]
        public void Should_edit_for_selected_mag_readings()
        {
            // Arrange
            _mockRepositoryFactory.Setup(c => c.GetRepository<Reading>()).Returns(_mockMagRepository.Object);
            _sut.ControllerContext = new ControllerContext();
            FormCollection fakeForm = new FormCollection();
            fakeForm.Add("Id", "222");
            fakeForm.Add("Depth", "1.0");
            fakeForm.Add("MagX", "5.5");
            fakeForm.Add("MagY", "6.6");
            fakeForm.Add("MaxZ", "7.7");
            _sut.ValueProvider = fakeForm.ToValueProvider();

            // Act
            var actionResult = _sut.EditMag();

            // Assert
            _mockMagRepository.Verify(m => m.Update(It.IsAny<Reading>()), Times.Once());


        }

    }
}
