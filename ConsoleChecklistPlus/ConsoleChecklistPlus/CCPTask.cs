using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// test
namespace ConsoleChecklistPlus
{
    [Serializable]
    public class CCPTask
    {
        public int ID { get; set;  }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool State { get; set; }
        public int Priority { get; set; }

        public CCPTask(int id, string name, string description, bool state, int priority)
        {
            this.ID = id;
            this.Name = name;
            this.Description = description;
            this.State = state;
            this.Priority = priority;
        }
    }
}
