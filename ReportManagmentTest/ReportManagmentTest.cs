using NUnit.Framework;
using ReportManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReportManagmentTest
{
    public class ReportManagerTests
    {
        private ReportManager _reportManager;

        [SetUp]
        public void Setup()
        {
            _reportManager = new ReportManager();  // Создание экземпляра ReportManager перед каждым тестом
        }

        [Test] 
        public void CreateReport_ValidReport_ReturnsTrue()
        {
            Report report = new Report {Id = 1, Type = "Финансовый", Content = "Ежеквартальный отчет о доходах", UserId = 456, CreationDate = DateTime.Now, Status = "Завершен"};
            Report report2 = new Report {Id = 2, Type = "Аналитический", Content = "Анализ рыночных тенденций", UserId = 789, CreationDate = DateTime.Now.AddDays(-7), Status = "В процессе"};
            Report report3 = new Report {Id = 3, Type = "Маркетинговый", Content = "План маркетинговой кампании на следующий квартал", UserId = 222, CreationDate = DateTime.Now.AddMonths(1), Status = "Запланирован" };

            bool result = _reportManager.CreateReport(report);
            Assert.IsTrue(result); 
        }

        [Test]
        public void DeleteReport_ExistingReportId_ReturnsTrue()
        {
            int reportId = 1;
            _reportManager.CreateReport(new Report { Id = 1, Type = "Финансовый", Content = "Ежеквартальный отчет о доходах", UserId = 456, CreationDate = DateTime.Now, Status = "Завершен" });
            _reportManager.CreateReport(new Report { Id = 2, Type = "Аналитический", Content = "Анализ рыночных тенденций", UserId = 789, CreationDate = DateTime.Now.AddDays(-7), Status = "В процессе" });
            _reportManager.CreateReport(new Report { Id = 3, Type = "Маркетинговый", Content = "План маркетинговой кампании на следующий квартал", UserId = 222, CreationDate = DateTime.Now.AddMonths(1), Status = "Запланирован" }); 
            
            bool result = _reportManager.DeleteReport(reportId); 
            Assert.IsTrue(result); 
        }

        [Test] 
        public void UpdateReport_ExistingReport_ReturnsTrue()
        {
            int reportId = 1;
            _reportManager.CreateReport(new Report { Id = 1, Type = "Финансовый", Content = "Ежеквартальный отчет о доходах", UserId = 456, CreationDate = DateTime.Now, Status = "Завершен" });
            _reportManager.CreateReport(new Report { Id = 2, Type = "Аналитический", Content = "Анализ рыночных тенденций", UserId = 789, CreationDate = DateTime.Now.AddDays(-7), Status = "В процессе" });
            _reportManager.CreateReport(new Report { Id = 3, Type = "Маркетинговый", Content = "План маркетинговой кампании на следующий квартал", UserId = 222, CreationDate = DateTime.Now.AddMonths(1), Status = "Запланирован" });
            Report updatedReport = new Report { Id = reportId, Type = "Аналитический", Content = "Анализ рыночных тенденций", UserId = 789, CreationDate = DateTime.Now.AddDays(-7), Status = "Завершен" }; 

            bool result = _reportManager.UpdateReport(updatedReport);
            Assert.IsTrue(result);
        }

        [Test] 
        public void GetReportById_ExistingReportId_ReturnsReport()
        {
            int reportId = 1;
            Report report = new Report { Id = 1, Type = "Финансовый", Content = "Ежеквартальный отчет о доходах", UserId = 456, CreationDate = DateTime.Now, Status = "Завершен" };
            Report report2 = new Report { Id = 2, Type = "Аналитический", Content = "Анализ рыночных тенденций", UserId = 789, CreationDate = DateTime.Now.AddDays(-7), Status = "В процессе" };
            Report report3 = new Report { Id = 3, Type = "Маркетинговый", Content = "План маркетинговой кампании на следующий квартал", UserId = 222, CreationDate = DateTime.Now.AddMonths(1), Status = "Запланирован" };
            _reportManager.CreateReport(report); 

            Report retrievedReport = _reportManager.GetReportById(reportId); 
            Assert.IsNotNull(retrievedReport);
            Assert.AreEqual(reportId, retrievedReport.Id); 
        }

        [Test] 
        public void GetAllReports_ReportsExist_ReturnsListOfReports()
        {
            _reportManager.CreateReport(new Report { Id = 1, Type = "Финансовый", Content = "Ежеквартальный отчет о доходах", UserId = 456, CreationDate = DateTime.Now, Status = "Завершен" });
            _reportManager.CreateReport(new Report { Id = 2, Type = "Аналитический", Content = "Анализ рыночных тенденций", UserId = 789, CreationDate = DateTime.Now.AddDays(-7), Status = "В процессе" });
            _reportManager.CreateReport(new Report { Id = 3, Type = "Маркетинговый", Content = "План маркетинговой кампании на следующий квартал", UserId = 222, CreationDate = DateTime.Now.AddMonths(1), Status = "Запланирован" });

            List<Report> reports = _reportManager.GetAllReports();
            Assert.IsNotNull(reports); 
            Assert.IsTrue(reports.Count >= 3); 
        }

        [Test] 
        public void GetReportsByUser_ExistingUserId_ReturnsListOfReports()
        {
            int userId = 222;
            _reportManager.CreateReport(new Report { Id = 1, Type = "Финансовый", Content = "Ежеквартальный отчет о доходах", UserId = 456, CreationDate = DateTime.Now, Status = "Завершен" });
            _reportManager.CreateReport(new Report { Id = 2, Type = "Аналитический", Content = "Анализ рыночных тенденций", UserId = 789, CreationDate = DateTime.Now.AddDays(-7), Status = "В процессе" });
            _reportManager.CreateReport(new Report { Id = 3, Type = "Маркетинговый", Content = "План маркетинговой кампании на следующий квартал", UserId = 222, CreationDate = DateTime.Now.AddMonths(1), Status = "Запланирован" });

            List<Report> reports = _reportManager.GetReportsByUser(userId); 
            Assert.IsNotNull(reports);
            Assert.IsTrue(reports.All(r => r.UserId == userId));
        }

        [Test]
        public void GetReportsByType_ExistingType_ReturnsListOfReports()
        {
            string reportType = "Маркетинговый";
            _reportManager.CreateReport(new Report { Id = 1, Type = "Финансовый", Content = "Ежеквартальный отчет о доходах", UserId = 456, CreationDate = DateTime.Now, Status = "Завершен" });
            _reportManager.CreateReport(new Report { Id = 2, Type = "Аналитический", Content = "Анализ рыночных тенденций", UserId = 789, CreationDate = DateTime.Now.AddDays(-7), Status = "В процессе" });
            _reportManager.CreateReport(new Report { Id = 3, Type = "Маркетинговый", Content = "План маркетинговой кампании на следующий квартал", UserId = 222, CreationDate = DateTime.Now.AddMonths(1), Status = "Запланирован" });

            List<Report> reports = _reportManager.GetReportsByType(reportType);
            Assert.IsNotNull(reports);
            Assert.IsTrue(reports.All(r => r.Type == reportType));
        }

        [Test]
        public void GetReportCount_ReportsExist_ReturnsCorrectCount()
        {
            _reportManager.CreateReport(new Report { Id = 1, Type = "Финансовый", Content = "Ежеквартальный отчет о доходах", UserId = 456, CreationDate = DateTime.Now, Status = "Завершен" });
            _reportManager.CreateReport(new Report { Id = 2, Type = "Аналитический", Content = "Анализ рыночных тенденций", UserId = 789, CreationDate = DateTime.Now.AddDays(-7), Status = "В процессе" });
            _reportManager.CreateReport(new Report { Id = 3, Type = "Маркетинговый", Content = "План маркетинговой кампании на следующий квартал", UserId = 222, CreationDate = DateTime.Now.AddMonths(1), Status = "Запланирован" });

            int count = _reportManager.GetReportCount();
            Assert.AreEqual(3, count);
        }

        [Test]
        public void GetReportsByCreationDate_ExistingDate_ReturnsListOfReports()
        {
            DateTime creationDate = DateTime.Now.Date;
            _reportManager.CreateReport(new Report { Id = 1, Type = "Финансовый", Content = "Ежеквартальный отчет о доходах", UserId = 456, CreationDate = DateTime.Now, Status = "Завершен" });
            _reportManager.CreateReport(new Report { Id = 2, Type = "Аналитический", Content = "Анализ рыночных тенденций", UserId = 789, CreationDate = DateTime.Now.AddDays(-7), Status = "В процессе" });
            _reportManager.CreateReport(new Report { Id = 3, Type = "Маркетинговый", Content = "План маркетинговой кампании на следующий квартал", UserId = 222, CreationDate = DateTime.Now.AddMonths(1), Status = "Запланирован" });

            List<Report> reports = _reportManager.GetReportsByCreationDate(creationDate);
            Assert.IsNotNull(reports);
            Assert.IsTrue(reports.All(r => r.CreationDate.Date == creationDate));
        }

        [Test]
        public void GetReportsByStatus_ExistingStatus_ReturnsListOfReports()
        {
            string status = "В процессе";
            _reportManager.CreateReport(new Report { Id = 1, Type = "Финансовый", Content = "Ежеквартальный отчет о доходах", UserId = 456, CreationDate = DateTime.Now, Status = "Завершен" });
            _reportManager.CreateReport(new Report { Id = 2, Type = "Аналитический", Content = "Анализ рыночных тенденций", UserId = 789, CreationDate = DateTime.Now.AddDays(-7), Status = "В процессе" });
            _reportManager.CreateReport(new Report { Id = 3, Type = "Маркетинговый", Content = "План маркетинговой кампании на следующий квартал", UserId = 222, CreationDate = DateTime.Now.AddMonths(1), Status = "Запланирован" });

            List<Report> reports = _reportManager.GetReportsByStatus(status);
            Assert.IsNotNull(reports);
            Assert.IsTrue(reports.All(r => r.Status == status));
            

        }

    }

}
