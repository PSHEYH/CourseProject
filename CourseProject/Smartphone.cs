using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public class Smartphone
    {
        public (int,int) Resolution { get; set; }
        public int Weight { get; set; }
        public int MaxFrequency { get; set; }
        public int SizeOfStorage { get; set; }
        public int RAM { get; set; }
        public int BateryVolume { get; set; }
        public int MatrixOfCamera { get; set; }
        public TypeOfSpeaker Speaker{ get; set; }
        public double Price { get; set; }
    }
}
