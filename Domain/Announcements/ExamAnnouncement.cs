using SmartCampus.Domain.Enums;

namespace SmartCampus.Domain.Announcements;

public sealed class ExamAnnouncement : Announcement
{
    public string CourseCode { get; }
    public DateTime NewExamDate { get; }

    public ExamAnnouncement(string courseCode, DateTime newExamDate)
        : base($"Exam Update: {courseCode}")
    {
        CourseCode = courseCode;
        NewExamDate = newExamDate;
    }

    public override AnnouncementType Type => AnnouncementType.Exam;

    public override string BuildMessage() =>
        $"[EXAM] {CourseCode} exam has been rescheduled to {NewExamDate:dd MMM yyyy, HH:mm}.";
}
