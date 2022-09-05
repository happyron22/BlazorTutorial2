using EmployeeManagement.Models;
using EmployeeManagement.web.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.web.Pages
{
    public class EmployeeDisplayBase : ComponentBase
    {
        protected bool IsSelected { get; set; }

        [Parameter]
        public Employee Employee { get; set; }
        
        [Parameter]
        public bool ShowFooter { get; set; }


        [Parameter]
        public EventCallback<bool> OnEmpoyeeSelection { get; set; }

        [Parameter]
        public EventCallback<int> OnEmpoyeeDeleted { get; set; }


        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected Innohive.Components.ConfirmBase DeleteConfirmation { get; set; }

        protected async Task CheckBoxChanged(ChangeEventArgs e)
        {
            IsSelected = (bool)e.Value;
            await OnEmpoyeeSelection.InvokeAsync(IsSelected);
        }


        protected async Task ConfirmDelete_Click(bool deleteConfirmed)
        {
            if (deleteConfirmed)
            {
                await EmployeeService.DeleteEmployee(Employee.EmployeeId);
                await OnEmpoyeeDeleted.InvokeAsync(Employee.EmployeeId);
            }
        }

        protected void Delete_Click() 
        {
            DeleteConfirmation.Show();
            //NavigationManager.NavigateTo("/", true);
        }

       
    }
}
