using Microsoft.AspNetCore.Mvc;
using Play.App.Service.Models;
namespace Play.App.Service.Controllers;

[ApiController]
[Route("[controller]")]
public class ExamManagement : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly UniversityManagementContext managementContext;

    public ExamManagement(UniversityManagementContext mgContext)
    {
        this.managementContext=mgContext;
    }

    [HttpGet("GetAll Students")]
    public IActionResult GetALL()
    {
        var student=this.managementContext.Students.ToList();
        return Ok(student);
    }

    [HttpGet("GetStudentByCode/{code}")]
    public IActionResult GetbyCode(int code)
    {
        var matricid = this.managementContext.Students.FirstOrDefault(o=>o.MatriculationId==code);
        return Ok(matricid);
    }

    //[Authorize(Roles ="admin")]
    [HttpPost("Insert Student")]
    public IActionResult Create([FromBody] Student _student)
    {
        var insert = this.managementContext.Students.FirstOrDefault(o=>o.MatriculationId==_student.MatriculationId);
        if (insert!=null){
            insert.Surname=_student.Surname;
            insert.Name=_student.Name;
            insert.DateOfBirth=_student.DateOfBirth;
            insert.RegistrationDate=_student.RegistrationDate;
        }
        else{
            this.managementContext.Students.Add(_student);
            this.managementContext.SaveChanges();
        }
        return Ok(true);
    }


    [HttpGet("GetAll Teachers")]
    public IActionResult GetALLteach()
    {
        var teacher=this.managementContext.Teachers.ToList();
        return Ok(teacher);
    }

    [HttpPost("Insert Teacher")]
    public IActionResult CreateTeacher([FromBody] Teacher _teacher)
    {
        var insert = this.managementContext.Teachers.FirstOrDefault(o=>o.Id==_teacher.Id);
        if (insert!=null){
            insert.Surname=_teacher.Surname;
            insert.Name=_teacher.Name;
        }
        else{
            this.managementContext.Teachers.Add(_teacher);
            this.managementContext.SaveChanges();
        }
        return Ok(true);
    }

   [HttpGet("GetAll Courses")]
    public IActionResult GetALLcourse()
    {
        var course=this.managementContext.Courses.ToList();
        return Ok(course);
    }

     [HttpGet("GetCourseByTeacher/{code}")]
    public IActionResult GetCoursebyCode(int code)
    {
        var id = this.managementContext.Courses.FirstOrDefault(o=>o.Teacher==code);
        return Ok(id);
    }

    [HttpPost("Insert Course")]
    public IActionResult CreateCourse([FromBody] Course _course)
    {
        var insert = this.managementContext.Courses.FirstOrDefault(o=>o.CourseId==_course.CourseId);
        if (insert!=null){
            insert.Name=_course.Name;
            insert.Teacher=_course.Teacher;
            insert.Cfu=_course.Cfu;
        }
        else{
            this.managementContext.Courses.Add(_course);
            this.managementContext.SaveChanges();
        }
        return Ok(true);
    }

    [HttpGet("GetExamByCode/{code}")]
    public IActionResult GetExambyCode(int code)
    {
        var matricid = this.managementContext.Exams.FirstOrDefault(o=>o.StudentId==code);
        return Ok(matricid);
    }


    [HttpPost("Add Exam")]
    public IActionResult CreateExam([FromBody] Exam _exam)
    {
        var insert = this.managementContext.Exams.FirstOrDefault(o=>o.StudentId==_exam.StudentId);
        if (insert!=null){
            insert.CourseId=_exam.CourseId;
            insert.Date=_exam.Date;
            insert.Grade=_exam.Grade;
            insert.Honors=_exam.Honors;
        }
        else{
            this.managementContext.Exams.Add(_exam);
            this.managementContext.SaveChanges();
        }
        return Ok(true);
    }
    [HttpGet("display average")]
    public IActionResult GetAvgGradeByStudent(int code)
    {
        var avg=this.managementContext.Exams.Where(o=>o.StudentId==code).Average(o=>o.Grade);
        return Ok(avg);
    }

    [HttpGet("display average by course")]
    public IActionResult GetAvgGradeByCourse(int code)
    {
        var avg=this.managementContext.Exams.Where(o=>o.CourseId==code).Average(o=>o.Grade);
        return Ok(avg);
    }

   [HttpPost("display Cfu")]
    public IActionResult GetCFU(Course _course)
    {
        var cfu=managementContext.Exams.Join(managementContext.Students, o=>o.StudentId, j=>j.MatriculationId, (o,j)=> new{
            id=o.StudentId
        });
        return Ok(cfu);
    }

    [HttpGet("display Field")]
    public IActionResult GetField(int code)
    {
        var GRAPHQL=managementContext.Exams.FirstOrDefault(o=>o.StudentId==code);
        return Ok(GRAPHQL);
    }
   

}

public class Examql: ObjectGraphType<Exam>
{
    public IActionResult GetStudentInfo()
    {
        Field(x => x.Id);
        Field(x => x.Surname);
        Field(x => x.Name);
    }
}

public class StudentQuery : ObjectGraphType
{
    public StudentQuery(IStudentInform studentInform)
    {
        Field<ListGraphType<ProductType>>(Name = "Students", resolve: x => productProvider.GetProducts());
        Field<ProductType>(Name = "Name", 
            arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "Matriculationid" }),
            resolve: x => productProvider.GetProducts().FirstOrDefault(p => p.Id == x.GetArgument<int>("Matriculationid")));
    }
}

public class StudentSchema : Schema
{
    public StudentSchema(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Query = serviceProvider.GetRequiredService<StudentQuery>();
        Mutation = serviceProvider.GetRequiredService<StudentMutation>();
    }
}

