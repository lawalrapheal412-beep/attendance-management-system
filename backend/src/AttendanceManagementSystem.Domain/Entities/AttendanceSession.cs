namespace AttendanceManagementSystem.Domain.Entities;

public class AttendanceSession
{
    public Guid Id { get; private set; }

    public Guid CourseId { get; private set; }

    public Guid LecturerId { get; private set; }

    public Guid SemesterId { get; private set; }

    public Guid AcademicSessionId { get; private set; }

    public DateOnly SessionDate { get; private set; }

    public TimeOnly StartTime { get; private set; }

    public TimeOnly EndTime { get; private set; }

    public bool IsOpen { get; private set; }

    public Course Course { get; private set; } = null!;

    public Lecturer Lecturer { get; private set; } = null!;

    public Semester Semester { get; private set; } = null!;

    public AcademicSession AcademicSession { get; private set; } = null!;

    public ICollection<AttendanceRecord> AttendanceRecords { get; private set; }
        = new List<AttendanceRecord>();

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    private AttendanceSession() { }

    public AttendanceSession(
        Guid courseId,
        Guid lecturerId,
        Guid semesterId,
        Guid academicSessionId,
        DateOnly sessionDate,
        TimeOnly startTime,
        TimeOnly endTime)
    {
        Id = Guid.NewGuid();
        CourseId = courseId;
        LecturerId = lecturerId;
        SemesterId = semesterId;
        AcademicSessionId = academicSessionId;
        SessionDate = sessionDate;
        StartTime = startTime;
        EndTime = endTime;
        IsOpen = true;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }

    public void Open()
{
    IsOpen = true;
    UpdatedAt = DateTime.UtcNow;
}

public void Close()
{
    IsOpen = false;
    UpdatedAt = DateTime.UtcNow;
}
}