using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagementSystem.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "Location", "Name" },
                values: new object[,]
                {
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Tanta", "Finance Department" },
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Tanta", "Quality Department" }
                });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ProjectId", "Description", "Name", "Status" },
                values: new object[,]
                {
                    { new Guid("6b676cb8-2fa0-4b77-a33d-c94cf69253d0"), null, "Task Payment", false },
                    { new Guid("b46df07b-e650-4a70-9475-b85f84274855"), null, "Task CheckOut", false }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "ManagerId", "Address", "DateOfBirth", "DateOfJoin", "DepartmentId", "Gender", "Name", "Position", "Salary" },
                values: new object[] { new Guid("0a0ca5a3-9c97-4bd4-82c5-2994b1ba1637"), "Tanta", new DateTime(2000, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 14, 23, 9, 37, 83, DateTimeKind.Local).AddTicks(2618), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Male", "Mohamed Apo Treka", "Manager", 9000m });

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "ManagerId", "Address", "DateOfBirth", "DateOfJoin", "DepartmentId", "Gender", "Name", "Position", "Salary" },
                values: new object[] { new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), "Cairo", new DateTime(2000, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 14, 23, 9, 37, 83, DateTimeKind.Local).AddTicks(2611), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Male", "Hussein El Shahat", "Manager", 9000m });

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "ManagerId", "Address", "DateOfBirth", "DateOfJoin", "DepartmentId", "Gender", "Name", "Position", "Salary" },
                values: new object[] { new Guid("f0e10c08-873e-41a3-b545-0466a2a8b1b2"), "Cairo", new DateTime(2000, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 14, 23, 9, 37, 83, DateTimeKind.Local).AddTicks(2623), new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Male", "Mohamed Awad", "Manager", 9000m });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Address", "DateOfBirth", "DateOfJoin", "DepartmentId", "Gender", "ManagerId", "Name", "Position", "Salary" },
                values: new object[] { new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), "Cairo", new DateTime(2000, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 14, 23, 9, 37, 82, DateTimeKind.Local).AddTicks(8916), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Male", new Guid("0a0ca5a3-9c97-4bd4-82c5-2994b1ba1637"), "Hussein El Shahat", "Developer", 9000m });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Address", "DateOfBirth", "DateOfJoin", "DepartmentId", "Gender", "ManagerId", "Name", "Position", "Salary" },
                values: new object[] { new Guid("86dba8c0-d178-41e7-938c-ed49778fb51a"), "Tanta", new DateTime(2000, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 14, 23, 9, 37, 82, DateTimeKind.Local).AddTicks(8934), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Male", new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), "Mohamed Apo Treka", "Developer", 9000m });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Address", "DateOfBirth", "DateOfJoin", "DepartmentId", "Gender", "ManagerId", "Name", "Position", "Salary" },
                values: new object[] { new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), "Cairo", new DateTime(2000, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 14, 23, 9, 37, 82, DateTimeKind.Local).AddTicks(8939), new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Male", new Guid("f0e10c08-873e-41a3-b545-0466a2a8b1b2"), "Mohamed Awad", "Developer", 9000m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("80abbca8-664d-4b20-b5de-024705497d4a"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("86dba8c0-d178-41e7-938c-ed49778fb51a"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"));

            migrationBuilder.DeleteData(
                table: "Project",
                keyColumn: "ProjectId",
                keyValue: new Guid("6b676cb8-2fa0-4b77-a33d-c94cf69253d0"));

            migrationBuilder.DeleteData(
                table: "Project",
                keyColumn: "ProjectId",
                keyValue: new Guid("b46df07b-e650-4a70-9475-b85f84274855"));

            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "ManagerId",
                keyValue: new Guid("0a0ca5a3-9c97-4bd4-82c5-2994b1ba1637"));

            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "ManagerId",
                keyValue: new Guid("80abbca8-664d-4b20-b5de-024705497d4a"));

            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "ManagerId",
                keyValue: new Guid("f0e10c08-873e-41a3-b545-0466a2a8b1b2"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
        }
    }
}
