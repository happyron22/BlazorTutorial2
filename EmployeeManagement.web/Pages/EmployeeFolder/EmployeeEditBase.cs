using AutoMapper;
using EmployeeManagement.Models;
using EmployeeManagement.web.Models;
using EmployeeManagement.web.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.web.Pages
{
    public class EmployeeEditBase : ComponentBase
    {


        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        private Employee Employee { get; set; } = new Employee();
        public EmployeeEditModel EmployeeEditModel { get; set; } = new EmployeeEditModel();

        [Inject]
        public IDepartmentService DepartmentService { get; set; }
        public List<Department> Departments { get; set; } = new List<Department>(); 

      // public string DepartmentID { get; set; }

        [Parameter]
        public string Id { get; set; }

        public string PageHeader { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }


        [Inject]
        public NavigationManager NavigationManager { get; set; }


        protected async override Task OnInitializedAsync()
        {

            int.TryParse(Id, out int employeeId);

            if (employeeId !=0)
            {
                PageHeader = "Edit Employee";
                Employee = await EmployeeService.GetEmployee(int.Parse(Id));
            }
            else
            {

                PageHeader = "Create Employee";
                Employee = new Employee
                {
                    DepartmentId = 1,
                    DateOfBrith = DateTime.Now,
                    PhotoPath = "images/NoPhoto.png"
                };
            }

          
            Departments = (await DepartmentService.GetDepartments()).ToList();

            Mapper.Map(Employee, EmployeeEditModel);
            //DepartmentID = Employee.DepartmentId.ToString();

            //EmployeeEditModel.EmployeeId = Employee.EmployeeId;
            //EmployeeEditModel.FirstName = Employee.FirstName;
            //EmployeeEditModel.LastName = Employee.LastName;
            //EmployeeEditModel.Email = Employee.Email;
            //EmployeeEditModel.ConfirmEmail = Employee.Email;
            //EmployeeEditModel.DateOfBrith = Employee.DateOfBrith;
            //EmployeeEditModel.Gender = Employee.Gender;
            //EmployeeEditModel.PhotoPath = Employee.PhotoPath;
            //EmployeeEditModel.DepartmentId = Employee.DepartmentId;
            //EmployeeEditModel.Department = Employee.Department;
        }

        protected async Task HandleValidSubmit()
        {
            Mapper.Map(EmployeeEditModel, Employee);

         

                Employee result = null;

            if (Employee.EmployeeId !=0)
            {

                result = await EmployeeService.UpdateEmployee(Employee);
            }
            else
            {
                 result = await EmployeeService.CreateEmployee(Employee);
            }

            if (result != null)
            {
                NavigationManager.NavigateTo("/");
            }
        }

        protected async Task Delete_Click()
        {
          await  EmployeeService.DeleteEmployee(Employee.EmployeeId);
            NavigationManager.NavigateTo("/");;
        }

    }

}
