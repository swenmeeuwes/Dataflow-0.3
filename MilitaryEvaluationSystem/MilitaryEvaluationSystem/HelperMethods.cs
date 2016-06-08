using MilitaryEvaluationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryEvaluationSystem {
    class HelperMethods {

        private List<ShirtData> data;

        public HelperMethods(List<ShirtData> data) {
            this.data = data;
        }

        public DateTime ConvertUnitToDate(int unixTimeStamp) {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public float Remap(float value, float from1, float to1, float from2, float to2) {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

        public int CalculateHeartRateVariaty() {
            int variaty = 0;
            for (int i = 0; i < data.Count; i++) {
                if (i % 2 == 0)
                    variaty += data[i].interbeatInterval;
                else
                    variaty -= data[i].interbeatInterval;
            }
            return Math.Abs(variaty);
        }

        public float CalculateStressLevel(int heartBeat, float bodyTemp) {
            float mappedHeartRate = Remap(heartBeat, 50, 180, 1, 10);
            float mappedBodyTemp = Remap(bodyTemp, 22, 27, 1, 10);

            int heartRateVariaty = CalculateHeartRateVariaty();

            //Console.WriteLine(heartRateVariaty);
            float mappedVariaty = Math.Abs(Remap(heartRateVariaty, 0, 1000, 1, 10) - 10);

            Console.WriteLine(mappedVariaty);

            return (mappedHeartRate * 0.2f) + (mappedBodyTemp * 0.2f) + (mappedVariaty * 0.6f);
        }
    }
}
