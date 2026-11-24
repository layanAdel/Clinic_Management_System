using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Management_System.Domain.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int PatientId { get; set; }

        public Patient? Patient { get; set; }

        public int AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }
     
    }
}
