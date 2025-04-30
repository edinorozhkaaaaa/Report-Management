using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportManagement
{
    public class Report
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public string Status { get; set; }
    }

    public class ReportManager
    {
        private List<Report> _reports = new List<Report>();

        public bool CreateReport(Report report)
        {
            if (report == null) return false; //Проверка на null

            _reports.Add(report);
            return true;
        }

        public bool DeleteReport(int reportId) //Удаляет отчет по ID
        {
            Report reportToRemove = _reports.FirstOrDefault(r => r.Id == reportId);
            if (reportToRemove != null)
            {
                _reports.Remove(reportToRemove);
                return true;
            }
            return false;
        }

        public bool UpdateReport(Report updatedReport)  //Обновляет информацию об отчете
        {
            Report existingReport = _reports.FirstOrDefault(r => r.Id == updatedReport.Id);
            if (existingReport != null)
            {
                existingReport.Type = updatedReport.Type;
                existingReport.Content = updatedReport.Content;
                existingReport.UserId = updatedReport.UserId;
                existingReport.CreationDate = updatedReport.CreationDate;
                existingReport.Status = updatedReport.Status;
                return true;
            }
            return false;
        }

        public Report GetReportById(int reportId) //Получает отчет по ID
        {
            return _reports.FirstOrDefault(r => r.Id == reportId);
        }

        public List<Report> GetAllReports() //Получает список всех отчетов
        {
            return _reports;
        }

        public List<Report> GetReportsByUser(int userId)  //Получает список отчетов по ID пользователя
        {
            return _reports.Where(r => r.UserId == userId).ToList();
        }

        public List<Report> GetReportsByType(string reportType) //Получает список отчетов по типу
        {
            return _reports.Where(r => r.Type == reportType).ToList();
        }

        public int GetReportCount() //Возвращает общее количество отчетов
        {
            return _reports.Count;
        }

        public List<Report> GetReportsByCreationDate(DateTime creationDate) //Возвращает отчеты, созданные в указанную дату
        {
            return _reports.Where(r => r.CreationDate.Date == creationDate.Date).ToList();
        }

        public List<Report> GetReportsByStatus(string status) //Возвращает отчеты с указанным статусом
        {
            return _reports.Where(r => r.Status == status).ToList();
        }
    }
}
