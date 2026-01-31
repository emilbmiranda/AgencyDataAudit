namespace AgencyDataAuditAPI.Tests.Services
{
    public class ErrorServiceTests
    {
        private readonly AppDbContext _context;
        private readonly ErrorService _service;

        public ErrorServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new AppDbContext(options);
            _service = new ErrorService(_context);
        }

        [Fact]
        public async Task GetAllErrorComparisonsAsync_ReturnsEmptyList_WhenNoErrorsExist()
        {
            //Arange
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
                FileTypeId = 3
            };
            var errorCategory = new ErrorCategory()
            {
                ErrorCategoryId = 1,
                ErrorCategoryName = "Test Error"
            };
            var error = new Error()
            {
                ErrorId = 1,
                ReportingPeriodFileId = 1,
                ErrorCategoryId = 1,
                RecordCount = 10,
            };
            await _context.ReportingPeriods.AddAsync(reportingPeriod);
            await _context.ReportingPeriodFiles.AddAsync(reportingPeriodFile);
            await _context.ErrorCategories.AddAsync(errorCategory);
            await _context.Errors.AddAsync(error);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetErrorComparisonsAsync(1);

            // Assert
            var errorComparison = new ErrorComparison()
            {
                ErrorCategoryName = "Test Error",
                EquifaxRecordCount = 10,
                ExperianRecordCount = 0,
                InnovisRecordCount = 0,
                TransUnionRecordCount = 0,
            };
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(errorComparison.EquifaxRecordCount, result.Where(x => x.ErrorCategoryName == "Test Error").First().EquifaxRecordCount);
            Assert.Equal(errorComparison.ExperianRecordCount, result.Where(x => x.ErrorCategoryName == "Test Error").First().ExperianRecordCount);
            Assert.Equal(errorComparison.InnovisRecordCount, result.Where(x => x.ErrorCategoryName == "Test Error").First().InnovisRecordCount);
            Assert.Equal(errorComparison.TransUnionRecordCount, result.Where(x => x.ErrorCategoryName == "Test Error").First().TransUnionRecordCount);
        }
    }
}
