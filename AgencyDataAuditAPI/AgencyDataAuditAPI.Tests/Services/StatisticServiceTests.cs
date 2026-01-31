using Xunit;
using AgencyDataAuditAPI.Models;
using AgencyDataAuditAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace AgencyDataAuditAPI.Tests.Services
{
    public class StatisticServiceTests
    {
        private readonly AppDbContext _context;
        private readonly Mock<IReportingPeriodService> _mockReportingPeriodService;
        private readonly StatisticService _service;

        public StatisticServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_Statistic")
                .Options;
            _context = new AppDbContext(options);
            _mockReportingPeriodService = new Mock<IReportingPeriodService>();
            _service = new StatisticService(_context, _mockReportingPeriodService.Object);
        }

        [Fact]
        public async Task GetStatisticComparisonAsync_returnsStatisticList()
        {
            // Arrange
            var reportingPeriod = new ReportingPeriod()
            {
                ReportingPeriodId = 1,
                SystemOfRecordId = 1,
                ActivityDate = DateTime.Now,
                CreateDate = DateTime.Now,
                IsActive = true
            };
            var reportingPeriodFile = new ReportingPeriodFile()
            {
                ConsumerReportingAgencyId = 1,
                ReportingPeriodId = 1,
                FileTypeId = 1
            };            
            var reportingPeriodFile2 = new ReportingPeriodFile()
            {
                ConsumerReportingAgencyId = 1,
                ReportingPeriodId = 1,
                FileTypeId = 2
            };
            var metro2 = new Metro2()
            {
                ReportingPeriodFileId = 1,
                FirstName = "John",
                LastName = "Doe",
                AccountNumber = "123456789"

            };
            var statistic = new Statistic()
            {
                ReportingPeriodFileId = 2,
                RecordCount = 100,
            };
            var consumerReportingAgency = new ConsumerReportingAgency()
            {
                ConsumerReportingAgencyId = 1,
                Name = "Equifax"
            };
            var consumerReportingAgency2 = new ConsumerReportingAgency()
            {
                ConsumerReportingAgencyId = 2,
                Name = "Experian"
            };
            var consumerReportingAgency3 = new ConsumerReportingAgency()
            {
                ConsumerReportingAgencyId = 3,
                Name = "Innovis"
            };
            var consumerReportingAgency4 = new ConsumerReportingAgency()
            {
                ConsumerReportingAgencyId = 4,
                Name = "TransUnion"
            };
            await _context.ConsumerReportingAgencies.AddAsync(consumerReportingAgency);
            await _context.ConsumerReportingAgencies.AddAsync(consumerReportingAgency2);
            await _context.ConsumerReportingAgencies.AddAsync(consumerReportingAgency3);
            await _context.ConsumerReportingAgencies.AddAsync(consumerReportingAgency4);
            await _context.ReportingPeriods.AddAsync(reportingPeriod);
            await _context.ReportingPeriodFiles.AddAsync(reportingPeriodFile);
            await _context.ReportingPeriodFiles.AddAsync(reportingPeriodFile2);
            await _context.Metro2s.AddAsync(metro2);
            await _context.Statistics.AddAsync(statistic);
            await _context.SaveChangesAsync(); 

            // Act
            var result = await _service.GetStatisticComparisonAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<StatisticComparison>>(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Metro2", result[0].Source);
            Assert.Equal(1, result[0].RecordCount);
            Assert.Equal("Equifax", result[1].Source);
            Assert.Equal(100, result[1].RecordCount);

            Dispose();
        }

        private void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
