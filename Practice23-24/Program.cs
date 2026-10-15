using System.Linq;

static void TryAddStudent(Course course, Student student)
{
    try
    {
        course.AddStudent(student);
    }
    catch (StudentAlreadyEnrolledException ex)
    {
        Console.WriteLine(
            $"[ENROLLMENT ERROR]: {ex.Message}");
    }
    catch (CourseCapacityException ex)
    {
        Console.WriteLine(
            $"[CAPACITY ERROR]: {ex.Message}");

        Console.WriteLine(
            $"Seats occupied: {ex.CurrentEnrolledCount}/{ex.Capacity}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Unexpected exception: {ex.Message}");
    }
}

var course = new Course("OOP", 2);
var student1 = new Student("Artem");
var student2 = new Student("Artem");
var student3 = new Student("Vlada");


Console.WriteLine("1. Succesful Script");
TryAddStudent(course, student1);
TryAddStudent(course, student2);

Console.WriteLine("2. Error script");
TryAddStudent(course, student1);

Console.WriteLine("3. Error script");
TryAddStudent(course, student3);

public class CourseCapacityException : Exception
{
    public int CurrentEnrolledCount { get; }
    public int Capacity { get; }

    public CourseCapacityException(string message, int currentEnrolledCount, int capacity)
        : base(message)
    {
        CurrentEnrolledCount = currentEnrolledCount;
        Capacity = capacity;
    }
}

public class StudentAlreadyEnrolledException : Exception
{
    public string StudentName { get; }
    public string CourseTitle { get; }

    public StudentAlreadyEnrolledException(string message, string studentName, string courseTitle)
        : base(message)
    {
        StudentName = studentName;
        CourseTitle = courseTitle;
    }
}

class Student
{
    public string Name
    {
        get;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Name cannot be empty.", nameof(value));
            }
            field = value;
        }
    }
    public Student(string name)
    {
        Name = name;
    }
}

class Course
{
    public string Title { get; }
    public int Capacity { get; }
    private List<Student> _students = new();
    public Course(string title, int capacity)
    {
        if (string.IsNullOrWhiteSpace(title)) 
             throw new ArgumentNullException("title cannot be empty.", nameof(title));
        if (capacity <= 0) 
            throw new ArgumentException("Course capcacity should be bigger than 0.", nameof(capacity));

        Title = title;
        Capacity = capacity;
    }

    public void AddStudent(Student student)
    {
        ArgumentNullException.ThrowIfNull(student);

        if (_students.Any(s => ReferenceEquals(s, student)))
        {
            throw new StudentAlreadyEnrolledException(
                $"Student {student.Name} is already enrolled in course {Title}.",
                student.Name,
                Title
            );
        }

        if (_students.Count >= Capacity) 
            { 
                throw new CourseCapacityException(
                    $"Cannot add {student.Name}: course {Title} fully booked.",
                    _students.Count,
                    Capacity
                    ); 
            }

        _students.Add(student);
        Console.WriteLine($"Student {student.Name} added to course {Title}.");
    }
}