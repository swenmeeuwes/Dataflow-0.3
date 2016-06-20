using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryEvaluationSystem.Models {
    public class ShirtData {
        public float temperature { get; set; }
        public int rawHeartRate { get; set; }
        public int beatsPerMinute { get; set; }
        public int interbeatInterval { get; set; }
        public int pulse { get; set; }
        public long milliseconds { get; set; }
        public float stressLevel { get; set; }

        public void FromDataModelToShirtData(DataModel dm, long milliseconds, float stressLevel) {
            this.temperature = dm.temperature;
            this.rawHeartRate = dm.rawHeartRate;
            this.beatsPerMinute = dm.beatsPerMinute;
            this.interbeatInterval = dm.interbeatInterval;
            this.pulse = dm.pulse;
            this.milliseconds = milliseconds;
            this.stressLevel = stressLevel;
        }
    }
}
