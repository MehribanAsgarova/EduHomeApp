
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHomeApp.Models;

namespace EduHomeApp.ViewModels
{
    public class SpeakerEventVM
    {
        public Event Event { get; set; }
        public int[] speakerIds { get; set; }
    }
}
