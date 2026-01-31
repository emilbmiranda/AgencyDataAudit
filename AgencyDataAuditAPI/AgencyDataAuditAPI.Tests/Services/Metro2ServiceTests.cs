using System.Reflection;
using System.Reflection.Metadata;

namespace AgencyDataAuditAPI.Tests.Services
{
    public class Metro2ServiceTests
    {
        private readonly AppDbContext _context;
        private readonly Metro2Service _service;

        public Metro2ServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_Metro2")
                .Options;
            _context = new AppDbContext(options);
            _service = new Metro2Service(_context);
        }

        [Fact]
        public async Task GetMetro2sAsync_returnsMetro2s()
        {
            // Arrange
            var reportingPeriod = new ReportingPeriod()
            {
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
            var metro2= new Metro2()
            {
                ReportingPeriodFileId = 1,
                FirstName = "John",
                LastName = "Doe",
                AccountNumber = "123456789"

            };
            await _context.ReportingPeriods.AddAsync(reportingPeriod);
            await _context.ReportingPeriodFiles.AddAsync(reportingPeriodFile);
            await _context.Metro2s.AddAsync(metro2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetMetro2sAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal("John", result.First().FirstName);
            Assert.Equal("Doe", result.First().LastName);
            Assert.Equal("123456789", result.First().AccountNumber);

            Dispose();
        }

        [Fact]
        public void GetMetro2TrendingAsync_returnsMetro2TrendingList()
        {
            // Arrange
            for(int i = 0; i < 5; i++)
            {
                var reportingPeriod = new ReportingPeriod()
                {
                    SystemOfRecordId = 1,
                    ActivityDate = DateTime.Now.AddDays(i),
                    CreateDate = DateTime.Now.AddDays(i),
                    IsActive = true
                };
                var reportingPeriodFile = new ReportingPeriodFile() 
                { 
                    ConsumerReportingAgencyId = 1,
                    ReportingPeriodId = i + 1,
                    FileTypeId = 1
                };
                var metro2= new Metro2()
                {
                    ReportingPeriodFileId = i + 1,
                    FirstName = "John",
                    LastName = "Doe",
                    AccountNumber = $"123456789{i}"
                };
                _context.ReportingPeriods.Add(reportingPeriod);
                _context.ReportingPeriodFiles.Add(reportingPeriodFile);
                _context.Metro2s.Add(metro2);
            }
            _context.SaveChanges();

            // Act
            var result = _service.GetMetro2TrendingAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, _context.Metro2s.Count());
            Assert.Equal(5, _context.ReportingPeriods.Count());
            Assert.Equal(5, _context.ReportingPeriodFiles.Count());

            Dispose();
        }

        [Fact]
        public async Task GetMetro2TrendingAsync_returnsMetro2()
        {
            // Arrange
            var reportingPeriod = new ReportingPeriod()
            {
                SystemOfRecordId = 1,
                ActivityDate = DateTime.Now,
                CreateDate = DateTime.Now,
                IsActive = true
            };
            var reportingPeriodFile = new ReportingPeriodFile(){ 
                ConsumerReportingAgencyId = 1,
                ReportingPeriodId = 1,
                FileTypeId = 1
            };
            var metro2= new Metro2()
            {
                ReportingPeriodFileId = 1,
                FirstName = "John",
                LastName = "Doe",
                AccountNumber = "123456789",
            };
            await _context.ReportingPeriods.AddAsync(reportingPeriod);
            await _context.ReportingPeriodFiles.AddAsync(reportingPeriodFile);
            await _context.Metro2s.AddAsync(metro2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetMetro2AccountAsync(1, "123456789");
            Assert.NotNull(result);

            // Assert
            Assert.Equal("John", result.FirstName);
            Assert.Equal("Doe", result.LastName);
            Assert.Equal("123456789", result.AccountNumber);

            Dispose();
        }

        private void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
