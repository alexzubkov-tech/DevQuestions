namespace DevQuestions.Domain.Reports;

public class Report
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    
    public Guid ReportedUserId { get; set; }
    
    public Guid? ResolvedByUserId { get; set; }

    public required string Reason { get; set; }

    public ReportStatus ReportStatus { get; set; } = ReportStatus.OPEN;
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
}