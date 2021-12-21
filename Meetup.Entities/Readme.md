1. Open nuget package manager console
2. set mettup.entities project as default project
3. run command  Add-Migration Initial -Context MeetupDbContext
4. run command Update-Database -Context MeetupDbContext
5. Add-migration -Context MeetupDbContext AddMeetupFeedback
6. run command Update-Database -Context MeetupDbContext
7. Add-migration -Context MeetupDbContext UpdateMeetupFeedbackSchema
8. run command Update-Database -Context MeetupDbContext


Scaffold-DbContext "Server=Server=(localdb)\MSSQLLocalDB;Database=PointsDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities