using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryEvaluationSystem.Models {
    public class DataModel {
        public float temperature { get; set; }
        public int rawHeartRate { get; set; }
        public int beatsPerMinute { get; set; }
        public int interbeatInterval { get; set; }
        public int pulse { get; set; }

        public override string ToString() {
            return string.Format("DataModel: [temperature: {0}, rawHeartRate: {1}]", temperature, rawHeartRate);
        }
    }
}
