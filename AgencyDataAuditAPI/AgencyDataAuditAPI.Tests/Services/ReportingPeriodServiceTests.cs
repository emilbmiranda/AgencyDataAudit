using Xunit;
using AgencyDataAuditAPI.Models;
using AgencyDataAuditAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace AgencyDataAuditAPI.Tests.Services
{
    public class ReportingPeriodServiceTests
    {
        private readonly AppDbContext _context;
        private readonly ReportingPeriodService _service;

        public ReportingPeriodServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_ReportingPeriod")
                .Options;
            _context = new AppDbContext(options);
            _service = new ReportingPeriodService(_context);
        }

        [Fact]
        public async Task GetReportingPeriodsAsync_ReturnsEmptyList_WhenNoPeriodsExist()
        {
            // Act
            var result = await _service.GetReportingPeriodsAsync();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task  GetReportingPeriodsAsync_ReturnsReportingPeriods_WhenPeriodsExist()
        {
            // Arrange
            var reportingPeriod = new ReportingPeriod()
            {
                SystemOfRecordId = 1,
                ActivityDate = DateTime.Now,
                CreateDate = DateTime.Now,
                IsActive = true
            };
            _context.ReportingPeriods.Add(reportingPeriod);
            _context.SaveChanges();

            // Act
            var result = await _service.GetReportingPeriodByIdAsync(reportingPeriod.ReportingPeriodId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(reportingPeriod.ReportingPeriodId, result.ReportingPeriodId);
            Assert.Equal(reportingPeriod.SystemOfRecordId, result.SystemOfRecordId);
            Assert.Equal(reportingPeriod.ActivityDate, result.ActivityDate);
            Assert.Equal(reportingPeriod.CreateDate, result.CreateDate);
            Assert.Equal(reportingPeriod.IsActive, result.IsActive);

            Dispose();
        }

        [Fact]
        public async Task GetReportingPeriodById_ReturnsNull_WhenPeriodNotFound()
        {
            // Act
            var result = await _service.GetReportingPeriodByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetReportingPeriodByIdAsync_ReturnsReportingPeriod_WhenPeriodExists()
        {
            // Arrange
            var reportingPeriod = new ReportingPeriod()
            {
                SystemOfRecordId = 1,
                ActivityDate = DateTime.Now,
                CreateDate = DateTime.Now,
                IsActive = true
            };
            _context.ReportingPeriods.Add(reportingPeriod);
            _context.SaveChanges();

            // Act
            var result = await _service.GetReportingPeriodByIdAsync(reportingPeriod.ReportingPeriodId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(reportingPeriod.ReportingPeriodId, result.ReportingPeriodId);
            Assert.Equal(reportingPeriod.SystemOfRecordId, result.SystemOfRecordId);
            Assert.Equal(reportingPeriod.ActivityDate, result.ActivityDate);
            Assert.Equal(reportingPeriod.CreateDate, result.CreateDate);
            Assert.Equal(reportingPeriod.IsActive, result.IsActive);

            Dispose();
        }

        [Fact]
        public async Task GetReportingPeriodByReportingPeriodFileIdAsync_ReturnsReportingPeriod_WhenPeriodExists()
        {
            // Arrange
            var reportingPeriod = new ReportingPeriod()
            {
                SystemOfRecordId = 1,
                ActivityDate = DateTime.Now,
                CreateDate = DateTime.Now,
                IsActive = true
            };
            _context.ReportingPeriods.Add(reportingPeriod);
            _context.SaveChanges();

            var reportingPeriodFile = new ReportingPeriodFile()
            {
                ReportingPeriodId = reportingPeriod.ReportingPeriodId,
                FileStateId = 1,
                ConsumerReportingAgencyId = 1,
                FileTypeId = 1,
                CreateDate = DateTime.Now
            };
            _context.ReportingPeriodFiles.Add(reportingPeriodFile);
            _context.SaveChanges();

            // Act
            var result = await _service.GetReportingPeriodByReportingPeriodFileIdAsync(reportingPeriodFile.ReportingPeriodFileId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(reportingPeriod.ReportingPeriodId, result.ReportingPeriodId);

            Dispose();
        }

        private void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
