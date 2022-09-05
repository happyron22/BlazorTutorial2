using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.web.Pages
{
    public class DataBindingDemoBase:ComponentBase
    {
        protected string Name { get; set; } = "Ron";

        protected string Gender { get; set; } = "Male";
        protected string Color { get; set; } = "background-color:Yellow";

        protected string Description { get; set; } = string.Empty;
    }
}
